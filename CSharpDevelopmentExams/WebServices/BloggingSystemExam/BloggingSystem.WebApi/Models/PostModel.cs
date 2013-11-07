using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BloggingSystem.WebApi.Models
{
    [DataContract]
    public class PostModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "postedBy")]
        public string PostedBy { get; set; }

        [DataMember(Name = "postDate")]
        public DateTime PostDate { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "tags", EmitDefaultValue = false)]
        public IEnumerable<string> Tags { get; set; }

        [DataMember(Name = "comments")]
        public IEnumerable<CommentModel> Comments { get; set; }

        public PostModel()
        {
            this.Tags = new HashSet<string>();
            this.Comments = new HashSet<CommentModel>();
        }
    }
}