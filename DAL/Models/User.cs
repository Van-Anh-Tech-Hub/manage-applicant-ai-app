using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        [BsonRepresentation(BsonType.String)]
        public E_Role role { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string candidateId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string companyId { get; set; }
        public bool isDel { get; set; }

        // Bỏ qua trường không cần thiết (__v)
        [BsonElement("__v")]
        public int version { get; set; }

        public User()
        {

        }
        public User(string fullname, string email, string password, E_Role role, string candidateId, string companyId, bool isDel) : this()
        {
            this.fullName = fullname;
            this.email = email;
            this.password = password;
            this.role = role;
            this.isDel = isDel;
            this.candidateId = candidateId;
            this.companyId = companyId;
        }
    }
}
