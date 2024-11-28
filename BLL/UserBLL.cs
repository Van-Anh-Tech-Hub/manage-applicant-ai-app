using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL
{
    public class UserBLL
    {
        private readonly UserDAL _userDal;

        public UserBLL()
        {
            _userDal = new UserDAL();
        }

        public List<UserDTO> GetAllUsers()
        {
            return _userDal.GetAllUsers();
        }

        public void AddUser(UserDTO user)
        {
            // Add business logic (e.g., validate user)
            _userDal.AddUser(user);
        }

        public void UpdateUser(string id, UserDTO user)
        {
            // Add business logic (e.g., check role, etc.)
            _userDal.UpdateUser(id, user);
        }

        public void DeleteUser(string id)
        {
            _userDal.DeleteUser(id);
        }
    }
}
