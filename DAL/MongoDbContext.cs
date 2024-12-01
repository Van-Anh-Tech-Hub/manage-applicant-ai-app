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

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");

    }
}
