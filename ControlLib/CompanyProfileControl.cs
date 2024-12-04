using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ControlLib
{
    public partial class CompanyProfileControl : UserControl
    {
        public CompanyProfileControl()
        {
            InitializeComponent();
        }
        public void SetValues(string name, string description, int size, string field, string street, string city, string country)
        {
            txtName.Text = name;
            txtDescription.Text = description;
            txtSize.Text = size.ToString();
            txtField.Text = field;
            txtStreet.Text = street;
            txtCity.Text = city;
            txtCountry.Text = country;
        }
        public (string name, string description, int size, string field, string street, string city, string country) GetValues()
        {
            string name = txtName.Text;
            string description = txtDescription.Text;
            int size = 0;
            int.TryParse(txtSize.Text, out size);
            string field = txtField.Text;
            string street = txtStreet.Text;
            string city = txtCity.Text;
            string country = txtCountry.Text;

            return (name, description, size, field, street, city, country);
        }
    }
}
