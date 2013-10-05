using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace CatalogOfFreeContent
{
    public class Catalog : ICatalog
    {
        private readonly MultiDictionary<string, IContent> url;
        private readonly OrderedMultiDictionary<string, IContent> title;

        private int count = 0;
        public int Count
        {
            get
            {
                return this.title.KeyValuePairs.Count;
            }
            set
            {
                this.count = value;
            }
        }

        public Catalog()
        {
            this.title = new OrderedMultiDictionary<string, IContent>(true);
            this.url = new MultiDictionary<string, IContent>(true);
        }

        public void Add(IContent content)
        {
            this.title.Add(content.Title, content);
            this.url.Add(content.Url, content);
        }

        public IEnumerable<IContent> GetContentByTitle(string title, int numberOfContentElementsToList)
        {
            var result = from c in this.title[title]
                         select c;
            if (result == null)
            {
                throw new NullReferenceException("GetListContent return null selected list");
            }
            return result.Take(numberOfContentElementsToList);
        }

        public int UpdateUrl(string oldUrl, string newUrl)
        {
            var theElements = 0;
            var contentToList = this.url[oldUrl].ToList();

            foreach (Content content in contentToList)
            {
                this.title.Remove(content.Title, content);
                theElements++;
            }
            this.url.Remove(oldUrl);

            foreach (var content in contentToList)
            {
                content.Url = newUrl;
            }

            
            foreach (var content in contentToList)
            {
                this.title.Add(content.Title, content);
                this.url.Add(content.Url, content);
            }

            return theElements;
        }
    }
}