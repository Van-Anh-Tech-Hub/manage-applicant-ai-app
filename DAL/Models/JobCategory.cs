using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    [BsonIgnoreExtraElements]
    public class JobCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("isDel")] // Sửa lại ánh xạ cho trường này
        public bool IsDel { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        public JobCategory() { }

        public JobCategory(string name, bool isDel)
        {
            Name = name;
            IsDel = isDel;
        }
    }
}
