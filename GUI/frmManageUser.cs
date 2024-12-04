using BLL;
using System;
using System.Windows.Forms;
using DAL.Types;
using DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
                        companyProfileCt.SetValues(company.Name, company.Description,company.Size,company.Field,company.Location.Address,company.Location.City,company.Location.Country);
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
    }
}
