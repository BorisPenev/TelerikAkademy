using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.System.UserProfile;
using Windows.UI.Notifications;
using TheHiddenTruth.Library.Model;
using TheHiddenTruth.Library.Services;
using TheHiddenTruth.Library.Utils;

namespace TheHiddenTruth.BackgroundTasks
{
    public static class ClockTileScheduler
    {
        private static IDictionary<string, string> TileModels { get; set; }

        public static void CreateSchedule()
        {
            if (TileModels == null)
            {
                TileModels = new Dictionary<string, string>(ServiceManager.TileModels);
            }


            //foreach (SiteModel siteModel in ServiceManager.Sites)
            //{
            //    foreach (PageModel pageModel in siteModel.Pages)
            //    {
            //        foreach (var itemModel in pageModel.Items)
            //        {
            //            if (!string.IsNullOrEmpty(itemModel.ImagePath))
            //            {
            //                TileModels.AddSafe(itemModel.Title, itemModel.ImagePath);
            //            }
            //        }
            //    }
            //}

            try
            {

                var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
                var plannedUpdated = tileUpdater.GetScheduledTileNotifications();

                string language = GlobalizationPreferences.Languages.First();
                CultureInfo cultureInfo = new CultureInfo(language);

                DateTime now = DateTime.Now;
                DateTime planTill = now.AddHours(4);

                DateTime updateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0).AddMinutes(1);
                if (plannedUpdated.Count > 0)
                    updateTime = plannedUpdated.Select(x => x.DeliveryTime.DateTime).Union(new[] {updateTime}).Max();


                const string xml = @"<tile><visual version=""2"" addImageQuery=""true"">
                                        <binding template=""TileSquare150x150Image"" fallback=""TileSquareImage"">
                                          <image id=""1"" src=""{0}"" alt=""{1}"" />
                                        </binding>
                                        <binding template=""TileWide310x150ImageAndText01"" fallback=""TileWideImageAndText01"">
                                          <image id=""1"" src=""{0}"" alt=""{1}"" />
                                          <text id=""1"">{1}</text>
                                        </binding>
                                        <binding template=""TileSquare310x310Image"">
                                          <image id=""1"" src=""{0}"" alt=""{1}"" />
                                        </binding>
                                   </visual></tile>";
                foreach (var tileModel in TileModels)
                {
                    var tileXmlNow = string.Format(xml, tileModel.Value, tileModel.Key);
                    XmlDocument documentNow = new XmlDocument();
                    documentNow.LoadXml(tileXmlNow);

                    tileUpdater.Update(new TileNotification(documentNow)
                    {
                        ExpirationTime = now.AddMinutes(1)
                    });

                    for (var startPlanning = updateTime;
                        startPlanning < planTill;
                        startPlanning = startPlanning.AddMinutes(1))
                    {
                        Debug.WriteLine(startPlanning);
                        Debug.WriteLine(planTill);

                        try
                        {
                            var tileXml = string.Format(xml, tileModel.Value, tileModel.Key);
                            XmlDocument document = new XmlDocument();
                            document.LoadXml(tileXml);

                            ScheduledTileNotification scheduledNotification = new ScheduledTileNotification(document,
                                new DateTimeOffset(startPlanning))
                            {
                                ExpirationTime = startPlanning.AddMinutes(1)
                            };
                            tileUpdater.AddToSchedule(scheduledNotification);

                            Debug.WriteLine("schedule for: " + startPlanning);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("exception: " + e.Message);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
