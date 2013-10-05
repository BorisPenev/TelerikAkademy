using MongoDB.Bson;

namespace Dictionary.Data.Library
{
    public class Word
    {
        public ObjectId _id { get; set; }
        public string WordName { get; set; }
        public string Translation { get; set; }
        //public BsonDateTime PublishDate { get; set; }
    }
}
