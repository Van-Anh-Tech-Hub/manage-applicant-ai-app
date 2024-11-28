using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        private readonly IMongoCollection<UserDTO> _users;

        public UserDAL()
        {
            _users = DatabaseContext.GetCollection<UserDTO>("users");
        }

        public List<UserDTO> GetAllUsers()
        {
            return _users.Find(user => !user.IsDeleted).ToList();
        }

        public void AddUser(UserDTO user)
        {
            _users.InsertOne(user);
        }

        public void UpdateUser(string id, UserDTO updatedUser)
        {
            _users.ReplaceOne(user => user.Id == id, updatedUser);
        }

        public void DeleteUser(string id)
        {
            var update = Builders<UserDTO>.Update.Set(u => u.IsDeleted, true);
            _users.UpdateOne(user => user.Id == id, update);
        }
    }
}
