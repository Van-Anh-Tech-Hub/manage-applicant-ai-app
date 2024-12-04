using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class MongoDbContext
    {
        private IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient(Config.MONGO_URL);
            _database = client.GetDatabase(Config.MongoName);
        }
        public MongoDbContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dbName);
        }

        public IMongoCollection<Application> Applications => _database.GetCollection<Application>("applications");
        public IMongoCollection<CandidateProfile> CandidateProfiles => _database.GetCollection<CandidateProfile>("candidateprofiles");
        public IMongoCollection<Company> Companies => _database.GetCollection<Company>("companies");
        public IMongoCollection<JobCategory> JobCategories => _database.GetCollection<JobCategory>("jobcategories");
        public IMongoCollection<Job> Jobs => _database.GetCollection<Job>("jobs");
        public IMongoCollection<JobType> JobTypes => _database.GetCollection<JobType>("jobtypes");
        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Location> Locations => _database.GetCollection<Location>("locations");
    }
}
