using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BloggingSystem.WebApi.Models
{
    [DataContract]
    public class FullTagModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "posts")]
        public IEnumerable<PostModel> PostsModels { get; set; }

        public FullTagModel()
        {
            this.PostsModels = new HashSet<PostModel>();
        }
    }
}