using System;
using System.Linq;
using System.Runtime.Serialization;

namespace CodeJewelApi.Models
{
    [DataContract(Name = "CodeJewel")]
    public class CodeJewelModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name="sourceCode")]
        public string CodeJewel { get; set; }

        [DataMember(Name = "postedBy")]
        public string AuthorName { get; set; }
    }
}