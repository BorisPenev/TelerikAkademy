using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TheHiddenTruth.Library.Model.AlterInformation
{
    [DataContract]
    public class Feed
    {
        [DataMember]
        public string FeedUrl { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public List<Entry> Entries { get; set; }
    }
}