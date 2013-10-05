using MongoDB.Bson;

namespace Bellini.Data.Library
{
    public class VendorExpenseMongoDB:VendorExpense
    {
        public ObjectId Id { get; set; }
    }
}
