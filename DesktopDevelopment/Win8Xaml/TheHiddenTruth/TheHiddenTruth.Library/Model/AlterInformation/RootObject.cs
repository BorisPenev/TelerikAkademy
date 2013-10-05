using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TheHiddenTruth.Library.Model.AlterInformation
{
    [DataContract]
    public class RootObject
    {
        [DataMember]
        public ResponseData ResponseData { get; set; }
        [DataMember]
        public object ResponseDetails { get; set; }
        [DataMember]
        public int ResponseStatus { get; set; }
    }
}
