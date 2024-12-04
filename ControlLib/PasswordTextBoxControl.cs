using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlLib
{
    public class PasswordTextBoxControl : UserControl
    {
        private TextBox txtPassword;
        private PictureBox pbEye;

        public PasswordTextBoxControl()
        {
            txtPassword = new TextBox
            {
                PasswordChar = '*',
                Width = 200,
                Location = new Point(0, 0)
            };

            pbEye = new PictureBox
            {
                Width = 35,
                Height = 35,
                Cursor = Cursors.Hand,
                Image = Properties.Resources.eye_close,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            pbEye.Location = new Point(txtPassword.Width, (txtPassword.Height - pbEye.Height) / 2);

            pbEye.Click += PbEye_Click;

            this.Controls.Add(txtPassword);
            this.Controls.Add(pbEye);

            this.Width = txtPassword.Width + pbEye.Width;
            this.Height = txtPassword.Height;
        }

        private void PbEye_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = '\0';
                pbEye.Image = Properties.Resources.eye_open;
            }
            else
            {
                txtPassword.PasswordChar = '*';
                pbEye.Image = Properties.Resources.eye_close;
            }
        }

        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }
    }
}
