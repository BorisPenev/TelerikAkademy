using System.Runtime.Serialization;

namespace TheHiddenTruth.Library.Model.AlterInformation
{
    [DataContract]
    public class ResponseData
    {
        [DataMember]
        public Feed Feed { get; set; }
    }
}