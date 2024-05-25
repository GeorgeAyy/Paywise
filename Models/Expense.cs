using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyMvcApp.Models
{
    public class Expense
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Amount")]
        public decimal Amount { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("CategoryId")]
        public string CategoryId { get; set; }
    }
}
