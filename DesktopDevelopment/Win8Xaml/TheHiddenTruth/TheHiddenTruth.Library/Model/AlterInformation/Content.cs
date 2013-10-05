using System.Runtime.Serialization;

namespace TheHiddenTruth.Library.Model.AlterInformation
{
    [DataContract]
    public class Content
    {
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Medium { get; set; }
        [DataMember]
        public string Title { get; set; }
    }
}