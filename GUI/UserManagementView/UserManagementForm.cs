using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace GUI.UserManagementView
{
    public partial class UserManagementForm : Form
    {
        private readonly UserBLL _userBll;

        public UserManagementForm()
        {
            InitializeComponent();
            _userBll = new UserBLL();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _userBll.GetAllUsers();
            if (users == null || users.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
            }
            dataGridViewUsers.DataSource = users;
        }
    

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var user = new UserDTO
            {
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                Role = cmbRole.SelectedItem.ToString()
            };
            _userBll.AddUser(user);
            LoadUsers();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            var id = dataGridViewUsers.SelectedRows[0].Cells["Id"].Value.ToString();
            _userBll.DeleteUser(id);
            LoadUsers();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {

        }
    }
}
