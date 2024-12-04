using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DAL.Models
{
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("salary")]
        public decimal Salary { get; set; }

        [BsonElement("experience")]
        public int Experience { get; set; }

        [BsonElement("deadline")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Deadline { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("headcount")]
        public int Headcount { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("companyId")]
        public string CompanyId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("jobTypeId")]
        public string JobTypeId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("categoryId")]
        public string CategoryId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("locationId")]
        public string LocationId { get; set; }

        [BsonElement("isDel")]
        public bool IsDel { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        public Job() { }

        public Job(string title, string description, decimal salary, int experience, DateTime deadline, int headcount,
                   string companyId, string jobTypeId, string categoryId, string locationId, bool isDel)
        {
            Title = title;
            Description = description;
            Salary = salary;
            Experience = experience;
            Deadline = deadline;
            Headcount = headcount;
            CompanyId = companyId;
            JobTypeId = jobTypeId;
            CategoryId = categoryId;
            LocationId = locationId;
            IsDel = isDel;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
