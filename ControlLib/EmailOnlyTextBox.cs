using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ControlLib
{
    public class EmailOnlyTextBox : TextBox
    {
        public event EventHandler InvalidEmailEntered;

        public EmailOnlyTextBox()
        {
            this.TextChanged += EmailOnlyTextBox_TextChanged;
        }

        private void EmailOnlyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidEmail(this.Text))
            {
                InvalidEmailEntered?.Invoke(this, e);
            }
        }

        public bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }

        public bool IsEmailValid => IsValidEmail(this.Text);
    }
}
