using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyMvcApp.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } // Reference to the user
    }
}
