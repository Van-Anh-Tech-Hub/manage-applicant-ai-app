using DAL;
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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;
            this.FormClosed += FrmMain_FormClosed;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmDangNhap loginForm = new frmDangNhap();
            //loginForm.Show();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            RenderMainMenu();
        }

        private void RenderMainMenu()
        {
            MainMenu mainMenu = new MainMenu();

            //if (UserSession.LoggedInUser.role == E_Role.admin)
            //{
            //}
                MenuItem manageUser = new MenuItem("Quản lý người dùng");

                manageUser.Click += new EventHandler(ManageUsers_Click);

                mainMenu.MenuItems.Add(manageUser);

            this.Menu = mainMenu;
        }

        private void ManageUsers_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            frmManageUser frm = new frmManageUser();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }
        private void CloseAllChildForms()
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }
    }
}

