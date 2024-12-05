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

namespace GUI
{
    public partial class frmManageCompany : Form
    {

        private static UserBLL _userBLL = new UserBLL();
        private static CompanyBLL _companyBLL = new CompanyBLL();
        private static LocationBLL _locationBLL = new LocationBLL();
        public frmManageCompany()
        {
            InitializeComponent();
            this.Load += frmManageCompany_Load;
            dgvCompanies.RowPrePaint += dgvCompanies_RowPrePaint;
        }

        private async void frmManageCompany_Load(object sender, EventArgs e)
        {
            string name = "";
            await LoadCompanies(name);
        }

        private async Task LoadCompanies(string name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Tên cty");
            dt.Columns.Add("Mô tả");
            dt.Columns.Add("Số lượng nhân viên");
            dt.Columns.Add("Lĩnh vực");
            dt.Columns.Add("isDel", typeof(bool));

            if (string.IsNullOrEmpty(name))
            {
                foreach (var company in await _companyBLL.GetCompanies())
                {
                    dt.Rows.Add(company.Id, company.Name, company.Description, company.Size, company.Field, company.IsDel);
                }
            }
            else
            {
                foreach (var company in await _companyBLL.GetCompanies(c => c.Name.Contains(name)))
                {
                    dt.Rows.Add(company.Id, company.Name, company.Description, company.Size, company.Field, company.IsDel);
                }
            }

            dgvCompanies.DataSource = dt;

            dgvCompanies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCompanies.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void dgvCompanies_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgvCompanies.Rows[e.RowIndex];

            if (row.Cells["isDel"].Value != null && (bool)row.Cells["isDel"].Value)
            {
                row.DefaultCellStyle.BackColor = Color.LightCoral;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private async void dgvCompanies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvCompanies.Rows[e.RowIndex];
            tb_name.Text = row.Cells[1].Value.ToString();
            rtb_description.Text = row.Cells[2].Value.ToString();
            tb_size.Text = row.Cells[3].Value.ToString();
            tb_field.Text = row.Cells[4].Value.ToString();

            var cpn = await _companyBLL.GetCompany(c => c.Id == row.Cells[0].Value.ToString());
            var location = await _locationBLL.GetLocations(l => l.Id == cpn.LocationId);

            tb_address.Text = location[0].Address;
            tb_city.Text = location[0].City;
            tb_country.Text = location[0].Country;

            var user = await _userBLL.GetUser(u => u.companyId == cpn.Id);
            if (user != null)
            {
                tb_owner.Text = string.IsNullOrEmpty(user.fullName) ? "Chưa có tên" : user.fullName;
            }
            else
            {
                tb_owner.Text = "User not found";
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvCompanies.CurrentRow != null)
            {
                string companyId = dgvCompanies.CurrentRow.Cells["Id"].Value.ToString();

                var company = await _companyBLL.GetCompany(c => c.Id == companyId);

                if (company != null)
                {
                    var updatedCompany = new DAL.Models.Company
                    {
                        Id = company.Id,
                        Name = company.Name,
                        Description = company.Description,
                        Size = company.Size,
                        Field = company.Field,
                        LocationId = company.LocationId,
                        IsDel = true
                    };
                    await _companyBLL.UpdateCompany(companyId, updatedCompany);
                    await LoadCompanies("");
                }
            }
        }

        private void btnResetAction_Click(object sender, EventArgs e)
        {
            tb_name.Text = "";
            rtb_description.Text = "";
            tb_size.Text = "";
            tb_field.Text = "";
            tb_address.Text = "";
            tb_city.Text = "";
            tb_country.Text = "";
            tb_owner.Text = "";
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tb_name.Text) ||
                    string.IsNullOrWhiteSpace(tb_address.Text) ||
                    string.IsNullOrWhiteSpace(tb_city.Text) ||
                    string.IsNullOrWhiteSpace(tb_country.Text) ||
                    string.IsNullOrWhiteSpace(tb_field.Text) ||
                    string.IsNullOrWhiteSpace(tb_size.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(tb_size.Text, out int size))
                {
                    MessageBox.Show("Số lượng nhân viên phải là số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LocationBLL locationBLL = new LocationBLL();
                DAL.Models.Location location = new DAL.Models.Location
                {
                    Address = tb_address.Text,
                    City = tb_city.Text,
                    Country = tb_country.Text
                };
                await locationBLL.CreateLocation(location);

                CompanyBLL companyBLL = new CompanyBLL();
                DAL.Models.Company company = new DAL.Models.Company
                {
                    Name = tb_name.Text,
                    Description = rtb_description.Text,
                    Size = size,
                    Field = tb_field.Text,
                    LocationId = location.Id
                };
                await companyBLL.CreateCompany(company);

                MessageBox.Show("Thêm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadCompanies("");

                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputFields()
        {
            tb_name.Clear();
            rtb_description.Clear();
            tb_size.Clear();
            tb_field.Clear();
            tb_address.Clear();
            tb_city.Clear();
            tb_country.Clear();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string search = tb_nameSearch.Text;
            await LoadCompanies(search);
        }
        private async void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompanies.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn công ty cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(tb_name.Text) ||
                    string.IsNullOrWhiteSpace(tb_address.Text) ||
                    string.IsNullOrWhiteSpace(tb_city.Text) ||
                    string.IsNullOrWhiteSpace(tb_country.Text) ||
                    string.IsNullOrWhiteSpace(tb_field.Text) ||
                    string.IsNullOrWhiteSpace(tb_size.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(tb_size.Text, out int size))
                {
                    MessageBox.Show("Số lượng nhân viên phải là số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string companyId = dgvCompanies.CurrentRow.Cells["Id"].Value.ToString();

                var company = await _companyBLL.GetCompany(c => c.Id == companyId);
                if (company == null)
                {
                    MessageBox.Show("Không tìm thấy công ty!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var location = await _locationBLL.GetLocations(l => l.Id == company.LocationId);
                if (location.Any())
                {
                    var updatedLocation = location.First();
                    updatedLocation.Address = tb_address.Text.Trim();
                    updatedLocation.City = tb_city.Text.Trim();
                    updatedLocation.Country = tb_country.Text.Trim();
                    await _locationBLL.UpdateLocation(updatedLocation.Id, updatedLocation);
                }

                var updatedCompany = new DAL.Models.Company
                {
                    Id = companyId,
                    Name = tb_name.Text.Trim(),
                    Description = rtb_description.Text.Trim(),
                    Size = size,
                    Field = tb_field.Text.Trim(),
                    LocationId = company.LocationId,
                    IsDel = company.IsDel
                };
                await _companyBLL.UpdateCompany(companyId, updatedCompany);

                MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadCompanies("");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
