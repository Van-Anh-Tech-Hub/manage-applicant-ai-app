using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    public class JobType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("isDel")]
        public bool IsDel { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        public JobType() { }

        public JobType(string type, bool isDel)
        {
            Type = type;
            IsDel = isDel;
        }
    }
}
