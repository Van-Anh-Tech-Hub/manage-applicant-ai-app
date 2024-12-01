using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Services;

namespace BLL
{
    public class UserBLL
    {
        private readonly UserService _userService;
        public UserBLL()
        {
            _userService = new UserService();
        }

        public async Task<List<User>> GetUsers(
            Expression<Func<User, bool>> filter = null,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null)
        {
            return await _userService.GetUsers(filter, orderBy);
        }

        public async Task<User> GetUser(Expression<Func<User, bool>> filter = null)
        {
            return await _userService.GetUser(filter);
        }

        public async Task<User> CreateUser(User user)
        {
            return await _userService.CreateUser(user);
        }

        public async Task<User> UpdateUser(string id, User updatedUser)
        {
            return await _userService.UpdateUser(id, updatedUser);
        }

        public async Task<User> DeleteUser(string id)
        {
            return await _userService.DeleteUser(id);
        }

    }
}