using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TheHiddenTruth.Library.Model.AlterInformation
{
    [DataContract]
    public class Entry
    {
        [DataMember]
        public List<MediaGroup> MediaGroups { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string PublishedDate { get; set; }
        [DataMember]
        public string ContentSnippet { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public List<string> Categories { get; set; }
    }
}