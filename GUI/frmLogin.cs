using BLL;
using DAL.Models;
using DAL.Services;
using DAL.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmLogin : Form
    {
        private static UserBLL _userBLL = new UserBLL();
        public frmLogin()
        {
            InitializeComponent();
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
                System.Windows.Forms.Application.Exit();
        }

        private void txt_Username_Click(object sender, EventArgs e)
        {
            txt_Email.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = SystemColors.Control;
            txt_Password.BackColor = SystemColors.Control;
        }

        private void txt_Password_Click(object sender, EventArgs e)
        {
            txt_Password.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel4.BackColor = SystemColors.Control;
            txt_Email.BackColor = SystemColors.Control;
        }

        private async void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                //User loggedInUser = await _userBLL.GetUser(u => u.email == txt_Email.Text);

                //if (loggedInUser == null)
                //{
                //    MessageBox.Show("Email hoặc mật khẩu không đúng!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txt_Password.Text = string.Empty;
                //    return;
                //}
                //bool isVerify = PasswordHasher.VerifyPassword(txt_Password.Text, loggedInUser.password);

                //if (!isVerify)
                //{
                //    MessageBox.Show("Email hoặc mật khẩu không đúng!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txt_Password.Text = string.Empty;
                //    return;
                //}
                User loggedInUser = new User
                {
                    email = "test@gmail.com",
                    role = E_Role.admin
                };
                UserSession.LoggedInUser = loggedInUser;

                // Chuyển sang frmMain
                frmMain frm = new frmMain();
                frm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex);
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txt_Email.Focus();
        }

        private void txt_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btn_Login_Click(sender, e);
            }
        }
    }
}
