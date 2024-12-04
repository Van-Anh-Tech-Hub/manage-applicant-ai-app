using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DAL.Models
{
    public class Company
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("size")]
        public int Size { get; set; }

        [BsonElement("field")]
        public string Field { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("locationId")]
        public string LocationId { get; set; }

        [BsonElement("isDel")]
        public bool IsDel { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        public Location Location { get; set; }

        public Company() { }

        public Company(string name, string description, int size, string field, string locationId, bool isDel)
        {
            Name = name;
            Description = description;
            Size = size;
            Field = field;
            LocationId = locationId;
            IsDel = isDel;
        }
    }
}
