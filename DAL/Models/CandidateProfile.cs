using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class CandidateProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("resume")]
        public Resume resume { get; set; }

        public bool isDel { get; set; }

        [BsonElement("__v")]
        public int version { get; set; }

        public CandidateProfile()
        {
        }

        public CandidateProfile(string id, Resume resume, bool isDel)
        {
            this.id = id;
            this.resume = resume;
            this.isDel = isDel;
        }
    }

    public class Resume
    {
        [BsonElement("cvLinks")]
        public List<string> CvLinks { get; set; }

        [BsonElement("skills")]
        public List<Skill> Skills { get; set; }

        public Resume()
        {
            CvLinks = new List<string>();
            Skills = new List<Skill>();
        }

        public Resume(List<string> cvLinks, List<Skill> skills)
        {
            CvLinks = cvLinks;
            Skills = skills;
        }
    }

    public class Skill
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("experience")]
        public int Experience { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public Skill()
        {
        }

        public Skill(string name, int experience, string id)
        {
            Name = name;
            Experience = experience;
            this.id = id;
        }
    }
}
