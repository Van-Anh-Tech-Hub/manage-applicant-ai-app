using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLib
{
    public class NumberOnlyTextBox : TextBox
    {
        public bool IsDecimal { get; set; } = false;

        public NumberOnlyTextBox()
        {
            this.KeyPress += new KeyPressEventHandler(NumberOnlyTextBox_KeyPress);
        }

        private void NumberOnlyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                if (IsDecimal && e.KeyChar == '.')
                {
                    if (this.Text.Contains("."))
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}
