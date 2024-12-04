using BLL;
using System;
using System.Windows.Forms;
using DAL.Types;
using DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL.Services;
using System.Linq.Expressions;

namespace GUI
{
    public partial class frmManageUser : Form
    {
        private static UserBLL _userBLL = new UserBLL();
        private static CompanyBLL _companyBLL = new CompanyBLL();
        private static CandidateProfileBLL _candidateProfileBLL = new CandidateProfileBLL();

        public frmManageUser()
        {
            InitializeComponent();
            this.Load += FrmManageUser_Load;
        }

        private async void FrmManageUser_Load(object sender, EventArgs e)
        {
            candidateProfileCt.Visible = false;
            companyProfileCt.Visible = false;

            ReloadDgvUser();
        }
        private async void ReloadDgvUser()
        {
            List<User> users = await _userBLL.GetUsers();
            LoadDgvUser(users);
        }
        private void LoadDgvUser(List<User> users)
        {
            if (users != null && users.Any())
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("fullName");
                dataTable.Columns.Add("email");
                dataTable.Columns.Add("password");
                dataTable.Columns.Add("role");
                dataTable.Columns.Add("candidateId");
                dataTable.Columns.Add("companyId");


                foreach (var user in users)
                {
                    dataTable.Rows.Add(user.Id, user.fullName, user.email, user.password, user.role, user.candidateId, user.companyId);
                }

                dgv_User.DataSource = dataTable;

                // Đặt lại tiêu đề cột
                dgv_User.Columns["Id"].Width = 150;
                dgv_User.Columns["fullName"].HeaderText = "Họ tên";
                dgv_User.Columns["fullName"].Width = 150;
                dgv_User.Columns["email"].HeaderText = "Email";
                dgv_User.Columns["password"].HeaderText = "Password";
                dgv_User.Columns["role"].HeaderText = "Vai trò";
                dgv_User.Columns["candidateId"].HeaderText = "Id ứng viên";
                dgv_User.Columns["companyId"].HeaderText = "Id công ty";
            }
            else
            {
                dgv_User.DataSource = null;
                dgv_User.Rows.Clear();
            }

        }

        private async void dgv_User_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgv_User.ClearSelection();
                dgv_User.Rows[e.RowIndex].Selected = true;

                DataTable dataTable = (DataTable)dgv_User.DataSource;

                string id = dataTable.Rows[e.RowIndex]["Id"].ToString();
                string fullName = dataTable.Rows[e.RowIndex]["fullName"].ToString();
                string email = dataTable.Rows[e.RowIndex]["email"].ToString();
                string password = dataTable.Rows[e.RowIndex]["password"].ToString();
                string role = dataTable.Rows[e.RowIndex]["role"].ToString();
                string candidateId = dataTable.Rows[e.RowIndex]["candidateId"].ToString();
                string companyId = dataTable.Rows[e.RowIndex]["companyId"].ToString();


                txtId.Text = id;
                txtFullName.Text = fullName;
                txtEmail.Text = email;
                txtPassword.Password = password;
                cboRole.Text = role;
                if (role == E_Role.candidate.ToString())
                {
                    candidateProfileCt.Visible = true;
                    companyProfileCt.Visible = false;
                    if (!String.IsNullOrEmpty(candidateId))
                    {
                        CandidateProfile candidate = await _candidateProfileBLL.GetCandidateProfile(c => c.id == candidateId);
                        candidateProfileCt.SetDataCvLinks(candidate.resume.CvLinks);
                        candidateProfileCt.SetDataSkills<Skill>(candidate.resume.Skills);
                    }
                }
                if (role == E_Role.recruiter.ToString())
                {
                    candidateProfileCt.Visible = false;
                    companyProfileCt.Visible = true;
                    if (!String.IsNullOrEmpty(companyId))
                    {
                        Company company = await _companyBLL.GetCompany(c => c.Id == companyId);
                        companyProfileCt.SetValues(company.Name, company.Description, company.Size, company.Field, company.Location.Address, company.Location.City, company.Location.Country);
                    }
                }
                if (role == E_Role.admin.ToString())
                {
                    candidateProfileCt.Visible = false;
                    companyProfileCt.Visible = false;
                }
            }
        }

        private void btnResetAction_Click(object sender, EventArgs e)
        {
            candidateProfileCt.Visible = false;
            companyProfileCt.Visible = false;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            E_Role role;
            if (Enum.TryParse(cboRole.Text, out role))
            {
            }
            else
            {
                MessageBox.Show("Invalid role selected.");
                return;
            }

            User existingUser = await _userBLL.GetUser(u => u.email == email);

            if (existingUser != null)
            {
                MessageBox.Show("Email này đã tồn tại!", "Trùng email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User newUse = new User
            {
                fullName = fullName,
                email = email,
                password = PasswordHasher.HashPassword(password),
                role = role
            };

            User userCreated = await _userBLL.CreateUser(newUse);
            if (userCreated != null)
            {
                MessageBox.Show("Thêm user thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadDgvUser();
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            string userId = txtId.Text.Trim();

            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Vui lòng chọn user để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc muốn xóa user này không?", "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                await _userBLL.DeleteUser(userId);
                MessageBox.Show("Xóa xe thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadDgvUser();
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            bool isPasswordChange = false;
            E_Role role;

            if (Enum.TryParse(cboRole.Text, out role))
            {
            }
            else
            {
                MessageBox.Show("Invalid role selected.");
                return;
            }

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn xe để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show("Bạn có chắc muốn cập nhật user này không?", "Xác Nhận Sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                User existingUser = await _userBLL.GetUser(u => u.Id == id);

                if (existingUser == null)
                {
                    MessageBox.Show("Không tìm thấy user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (existingUser.email != email)
                {
                    User userWithSameEmail = await _userBLL.GetUser(u => u.email == email);

                    if (userWithSameEmail != null)
                    {
                        MessageBox.Show("Email đã tồn tại!", "Trùng email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (existingUser.password != password)
                {
                    isPasswordChange = true;
                }

                User updatedUser = new User
                {
                    fullName = fullName,
                    email = email,
                    role = role
                };

                if (isPasswordChange)
                {
                    updatedUser.password = PasswordHasher.HashPassword(password);
                }

                // Update the user in the database
                await _userBLL.UpdateUser(id, updatedUser);
                MessageBox.Show("Sửa user thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadDgvUser();
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string fullName = txtSearchHoTen.Text;
            string email = txtSearchEmail.Text;

            Expression<Func<User, bool>> filter = user =>
            (string.IsNullOrEmpty(fullName) || user.fullName.Contains(fullName)) &&
            (string.IsNullOrEmpty(email) || user.email.Contains(email));

            List<User> users = await _userBLL.GetUsers(filter);
            LoadDgvUser(users);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ReloadDgvUser();
        }
    }
}
