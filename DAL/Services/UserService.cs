using MongoDB.Driver;
using MongoDB.Driver.Linq;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly MongoDbContext _dbContext;

        public UserService()
        {
            _dbContext = new MongoDbContext();
            _users = _dbContext.Users;
            var indexKeys = Builders<User>.IndexKeys.Ascending(user => user.email);
            var indexOptions = new CreateIndexOptions { Unique = true };
            _users.Indexes.CreateOne(new CreateIndexModel<User>(indexKeys, indexOptions));
        }

        public async Task<List<User>> GetUsers(
            Expression<Func<User, bool>> filter = null,
            Func<IMongoQueryable<User>, IOrderedQueryable<User>> orderBy = null)
        {
            var query = _users.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = (IMongoQueryable<User>)orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<User> GetUser(Expression<Func<User, bool>> filter = null)
        {
            var query = _users.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> UpdateUser(string id, User updatedUser)
        {
            var existingUser = await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                updatedUser.Id = id; 
                await _users.ReplaceOneAsync(c => c.Id == id, updatedUser);
                return updatedUser;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> DeleteUser(string id)
        {
            var userToDelete = await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

            if (userToDelete != null)
            {
                await _users.DeleteOneAsync(c => c.Id == id);
                return userToDelete;
            }
            else
            {
                return null;
            }
        }

    }
}
