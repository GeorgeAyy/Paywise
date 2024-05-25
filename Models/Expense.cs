using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyMvcApp.Models
{
    public class Expense
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string CategoryId { get; set; }
    }
}
