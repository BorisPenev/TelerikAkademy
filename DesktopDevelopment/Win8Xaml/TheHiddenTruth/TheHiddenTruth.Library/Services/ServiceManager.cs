using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Google.Apis.Blogger.v3;
using Google.Apis.Blogger.v3.Data;
using Google.Apis.Services;
using TheHiddenTruth.Library.Helpers;
using TheHiddenTruth.Library.Model;
using TheHiddenTruth.Library.Model.AlterInformation;
using TheHiddenTruth.Library.Utils;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace TheHiddenTruth.Library.Services
{
    public class ServiceManager : IServiceManager
    {
        private static ObservableCollection<SiteModel> _sites = new ObservableCollection<SiteModel>();
        public static IDictionary<string, string> TileModels { get; set; }

        public static ObservableCollection<SiteModel> Sites
        {
            get { return _sites; }
            set { _sites = value; }
        }

        static ServiceManager()
        {
            TileModels = new Dictionary<string, string>();
        }

        public async Task GetDataBlogZaSeriozniHora(string pageToken, Action<PageModel, Exception> callback)
        {
            string uri = "http://blogzaserioznihora.blogspot.com/";

            Exception err = null;
            var result = new PageModel();
            try
            {
                BloggerService _service = new BloggerService(new BaseClientService.Initializer()
                {
                    ApiKey = "AIzaSyCkyf5p4x_2tW-Bwmqt7Bbj-HhFIC_kXvw",
                });

                SiteModel site = Sites.FirstOrDefault(x => x.Title == "Блог за сериозни хора");
                if (site == null)
                {
                    var blog = await _service.Blogs.GetByUrl(uri).ExecuteAsync();
                    site = new SiteModel()
                    {
                        Title = "Блог за сериозни хора",
                        Id = blog.Id
                    };
                    
                    //group.ImagePath = blog.
                    Sites.Add(site);
                }

                PageModel page = site.Pages.FirstOrDefault(x => x.CurrentPageToken == pageToken);
                if (page != null)
                {
                    result = page;
                }
                else
                {
                    var getPosts = _service.Posts.List(site.Id);
                    getPosts.MaxResults = 8;
                    if (!string.IsNullOrEmpty(pageToken))
                    {
                        getPosts.PageToken = pageToken;
                        result.CurrentPageToken = pageToken;
                    }

                    var response = await getPosts.ExecuteAsync();
                    result.NextPageToken = response.NextPageToken;
                    result.PrevPageToken = response.PrevPageToken;
                    result.PageIndex = site.Pages.Count;

                    foreach (var item in response.Items)
                    {
                        var itemModel = new ItemModel()
                        {
                            Id = item.Id,
                            Title = item.Title,
                            OriginalItem = item,
                            //Parent = result,
                            OriginalUrl = item.Url,
                            CommentUrl = "http://www.blogger.com/comment.g?blogID=" + site.Id + "&postID="+ item.Id +"&isPopup=true"
                        };

                        var document = new HtmlDocument();
                        document.LoadHtml(WebContentHelper.WrapHtml(item.Content, 0, 0));

                        itemModel.Content = RefactorContent(document);

                        var img = document.DocumentNode.Descendants().FirstOrDefault(doc => doc.Name == "img" &&
                            (doc.Attributes["src"].Value.EndsWith(".jpg") || doc.Attributes["src"].Value.EndsWith(".gif") || doc.Attributes["src"].Value.EndsWith(".png")));
                        if (img != null)
                        {
                            itemModel.ImagePath = img.Attributes["src"].Value;
                        }

                        AddItemToTiles(itemModel);

                        result.Items.Add(itemModel);
                        if (string.IsNullOrEmpty(pageToken))
                        {
                            site.Items.Add(itemModel);
                        }
                    }
                    site.Pages.Add(result);
                }
            }
            catch (Exception ex)
            {
                err = ex;
            }

            callback(result, err);
        }

        private static void AddItemToTiles(ItemModel itemModel)
        {
            if (!string.IsNullOrEmpty(itemModel.ImagePath))
            {
                TileModels.AddSafe(EscapingText(itemModel.Title), EscapingText(itemModel.ImagePath));
            }
        }

        public async Task GetDataAlterInformation(int? pageToken, Action<PageModel, Exception> callback)
        {
            if (pageToken == null)
            {
                pageToken = 1;
            }
            HttpClient client = new HttpClient();
            string uri = "http://alterinformation.wordpress.com/feed/?paged=" + pageToken;

            Exception err = null;
            var result = new PageModel();
            
            try
            {
                var blog = await client.GetStringAsync("http://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=8&q=" + uri);
                var responseRootObject = JsonConvert.DeserializeObject<RootObject>(blog);

                SiteModel site = Sites.FirstOrDefault(x => x.Title == "Alter Information");
                if (site == null)
                {
                    site = new SiteModel()
                    {
                        Title = "Alter Information",
                        //Id = blog.Id
                    };

                    //group.ImagePath = blog.
                    Sites.Add(site);
                }

                PageModel page = site.Pages.FirstOrDefault(x => x.CurrentPageToken == pageToken.ToString());
                if (page != null)
                {
                    result = page;
                }
                else
                {
                    result.PageIndex = site.Pages.Count;
                    result.CurrentPageToken = pageToken.ToString();
                    result.NextPageToken = (pageToken + 1).ToString();
                    foreach (var item in responseRootObject.ResponseData.Feed.Entries)
                    {
                        var itemModel = new ItemModel()
                        {
                            Title = item.Title,
                            OriginalItem = item,
                            //Parent = result,
                            OriginalUrl = item.Link,
                            CommentUrl = item.Link +"/feed/"
                        };

                        var document = new HtmlDocument();
                        document.LoadHtml(WebContentHelper.WrapHtml(item.Content, 0, 0));

                        itemModel.Content = RefactorContent(document);

                        var img = document.DocumentNode.Descendants().FirstOrDefault(doc => doc.Name == "img");
                        if (img != null)
                        {
                            itemModel.ImagePath = img.Attributes["src"].Value;
                        }

                        result.Items.Add(itemModel);
                        if (pageToken < 2)
                        {
                            site.Items.Add(itemModel);
                        }
                    }
                    site.Pages.Add(result);
                }
            }
            catch (Exception ex)
            {
                err = ex;
            }

            callback(result, err);
        }

        public async Task Search(string searchQuery, Action<PageModel, Exception> callback)
        {
            string uri = "http://blogzaserioznihora.blogspot.com/";

            Exception err = null;
            var result = new PageModel();
            try
            {
                BloggerService _service = new BloggerService(new BaseClientService.Initializer()
                {
                    ApiKey = "AIzaSyCkyf5p4x_2tW-Bwmqt7Bbj-HhFIC_kXvw",
                });

                SiteModel site = Sites.FirstOrDefault(x => x.Title == "Блог за сериозни хора");
                if (site == null)
                {
                    var blog = await _service.Blogs.GetByUrl(uri).ExecuteAsync();
                    site = new SiteModel()
                    {
                        Title = "Блог за сериозни хора",
                        Id = blog.Id
                    };

                    //group.ImagePath = blog.
                    Sites.Add(site);
                }
                var response = await _service.Posts.Search(site.Id, searchQuery).ExecuteAsync();
                result.NextPageToken = response.NextPageToken;
                result.PrevPageToken = response.PrevPageToken;
                result.PageIndex = site.Pages.Count;

                foreach (var item in response.Items)
                {
                    var itemModel = new ItemModel()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        OriginalItem = item,
                        //Parent = result,
                        OriginalUrl = item.Url,
                        CommentUrl =
                            "http://www.blogger.com/comment.g?blogID=" + site.Id + "&postID=" + item.Id +
                            "&isPopup=true"
                    };

                    var document = new HtmlDocument();
                    document.LoadHtml(WebContentHelper.WrapHtml(item.Content, 0, 0));

                    itemModel.Content = RefactorContent(document);

                    var img = document.DocumentNode.Descendants().FirstOrDefault(doc => doc.Name == "img");
                    if (img != null)
                    {
                        itemModel.ImagePath = img.Attributes["src"].Value;
                    }

                    AddItemToTiles(itemModel);
                    result.Items.Add(itemModel);
                }
            }
            catch (Exception ex)
            {
                err = ex;
            }

            callback(result, err);
        }

        private static string RefactorContent(HtmlDocument document)
        {
            var iframes = document.DocumentNode.Descendants().Where(doc => doc.Name == "iframe");
            foreach (var htmlNode in iframes)
            {
                var url = htmlNode.Attributes["src"].Value;
                if (!url.StartsWith("http://"))
                {
                    url = CheckUrl(url);
                    htmlNode.Attributes["src"].Value = "http://" + url;
                }
            }

            var link = document.DocumentNode.Descendants().Where(doc => doc.Name == "a");
            foreach (var htmlNode in link)
            {
                if (htmlNode.Attributes["target"] == null)
                {
                    htmlNode.SetAttributeValue("target", "_blank");
                }
            }

            StringWriter stringWriter = new StringWriter();
            document.Save(stringWriter);
            return stringWriter.ToString(); 
        }

        public static string EscapingText(string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return source.Replace("\"", "").Replace("'", "").Replace("<", "").Replace(">", "").Replace("&", "");
        }

        public static async Task<SiteModel> GetSiteAsync(string siteId)
        {
            var matches = Sites.FirstOrDefault(x => x.Id == siteId);
            return matches;
        }

        public static async Task<PageModel> GetPageAsync(string itemId)
        {
            var matches = Sites.SelectMany(s => s.Pages).FirstOrDefault(x => x.Items.Any(c => c.Id == itemId));
            return matches;
        }

        public static async Task<ItemModel> GetItemAsync(string itemId)
        {
            var matches = Sites.SelectMany(group => group.Pages).SelectMany(p => p.Items).FirstOrDefault(x => x.Id == itemId);
            return matches;
        }

        private static string CheckUrl(string url)
        {
            if (url.StartsWith("//"))
            {
                url = url.Remove(0, 2);
            }
            //else if (!url.StartsWith("w"))
            //{
            //    url = url.Remove(0, 1);
            //    url = CheckUrl(url);
            //}
            return url;
        }

        /*
        public async void GetHeadlines(Action<List<PageModel>, Exception> callback)
        {

            // locally scoped exception var
            Exception err = null;
            List<PageModel> results = null;

            try
            {
                var t = await MakeAsyncRequest(uri);

                StringReader stringReader = new StringReader(t);
                using (var xmlReader = System.Xml.XmlReader.Create(stringReader))
                {
                    var doc = System.Xml.Linq.XDocument.Load(xmlReader);

                    //results = (from e in doc.Element("rss").Element("channel").Elements("item")
                    //           select
                    //               new Headline()
                    //               {
                    //                   Title = e.Element("title").Value,
                    //                   Description = e.Element("description").Value,
                    //                   Published = Convert.ToDateTime(e.Element("pubDate").Value),
                    //                   Url = e.Element("link").Value

                    //               }).ToList();
                }
            }
            catch (Exception ex)
            {
                // should do some other 
                // logging here. for now pass off
                // exception to callback on UI
                err = ex;
            }

            callback(results, err);

        }
        */

        private static Task<string> MakeAsyncRequest(string url)
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = "GET";

            var task = Task.Factory.FromAsync(request.BeginGetResponse,
                (asyncResult) => request.EndGetResponse(asyncResult),
                (object)null);

            return task.ContinueWith(t => ReadStreamFromResponse(t.Result));
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {
                var strContent = sr.ReadToEnd();
                return strContent;
            }
        }
         
    }
}
