using System;
using System.Runtime.Serialization;

namespace TheHiddenTruth.Library.Model
{
    [DataContract]
    public class ItemModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string ImagePath { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string OriginalUrl { get; set; }
        [DataMember]
        public dynamic OriginalItem { get; set; }
        //[DataMember]
        //public PageModel Parent { get; set; }
        [DataMember]
        public string CommentUrl { get; set; }

        public ItemModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
