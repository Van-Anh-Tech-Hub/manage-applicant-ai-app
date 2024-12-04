using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DAL.Models
{
    public class Application
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("jobId")]
        public string JobId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("candidateProfileId")]
        public string CandidateProfileId { get; set; }

        [BsonElement("selectedCvLink")]
        public string SelectedCvLink { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("evaluationAI")]
        public string EvaluationAI { get; set; }

        [BsonElement("appliedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime AppliedAt { get; set; }

        [BsonElement("isDel")]
        public bool IsDel { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        public Application() { }

        public Application(string jobId, string candidateProfileId, string selectedCvLink, string status, string evaluationAI, DateTime appliedAt, bool isDel)
        {
            JobId = jobId;
            CandidateProfileId = candidateProfileId;
            SelectedCvLink = selectedCvLink;
            Status = status;
            EvaluationAI = evaluationAI;
            AppliedAt = appliedAt;
            IsDel = isDel;
        }
    }
}
