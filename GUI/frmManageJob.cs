using BLL;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.Models;
using System.Text.Json;
namespace GUI
{
    public partial class frmManageJob : Form
    {
        private readonly JobBLL _jobBLL;
        private readonly JobCategoryBLL _jobCategoryBLL;
        private readonly JobTypeBLL _jobTypeBLL;
        public frmManageJob()
        {
            InitializeComponent();
            _jobBLL = new JobBLL();
            _jobCategoryBLL = new JobCategoryBLL();
            _jobTypeBLL = new JobTypeBLL();
            LoadData();
        }

        private async void LoadData()
        {
            await LoadCities();
            await LoadCategories();
        }

        private async void frmManageJob_Load(object sender, EventArgs e)
        {
            await LoadJobs(isDeleted: false);
        }

        private async Task LoadCities()
        {
            var apiClient = new HttpClient();
            var response = await apiClient.GetStringAsync("https://provinces.open-api.vn/api/?depth=2");

            if (string.IsNullOrEmpty(response))
            {
                MessageBox.Show("Không nhận được dữ liệu từ API.");
                return;
            }

            var cities = System.Text.Json.JsonSerializer.Deserialize<List<City>>(response);

            if (cities != null && cities.Count > 0)
            {
                cbxThanhPho.DataSource = cities;
                cbxThanhPho.DisplayMember = "Name";
                cbxThanhPho.ValueMember = "Code";
            }
            else
            {
                MessageBox.Show("Không có dữ liệu thành phố.");
            }
        }

        private async Task LoadCategories()
        {
            var categories = await _jobCategoryBLL.GetJobCategories(c => !c.IsDel);
            cbxDanhMuc.DataSource = categories;
            cbxDanhMuc.DisplayMember = "Name";
            cbxDanhMuc.ValueMember = "Id";
        }

        private async Task LoadJobs(bool isDeleted)
        {
            var jobs = await _jobBLL.GetJobs(job => job.IsDel == isDeleted);

            dataGridView1.DataSource = jobs.Select(job => new
            {
                job.Id,
                job.Title,
                job.Salary,
                job.Deadline,
                job.Headcount,
                job.CreatedAt
            }).ToList();
        }


        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string title = txtJob.Text.Trim();
            string categoryId = cbxDanhMuc.SelectedValue?.ToString();
            string locationId = cbxThanhPho.SelectedValue?.ToString();

            var jobs = await _jobBLL.GetJobs(
                job => (string.IsNullOrEmpty(title) || job.Title.Contains(title)) &&
                       (string.IsNullOrEmpty(categoryId) || job.CategoryId == categoryId) &&
                       (string.IsNullOrEmpty(locationId) || job.LocationId == locationId)
            );

            dataGridView1.DataSource = jobs.Select(job => new
            {
                job.Id,
                job.Title,
                job.Salary,
                job.Deadline,
                job.Headcount,
                job.CreatedAt
            }).ToList();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var jobId = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                var detailForm = new frmJobDetail(jobId);
                detailForm.ShowDialog();
            }
        }

        public class City
        {
            public int Code { get; set; }
            public string Name { get; set; }
        }

        private async void btnDeleteJob_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn công việc để xóa.");
                return;
            }

            var jobId = dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString();

            var deletedJob = await _jobBLL.DeleteJob(jobId);

            if (deletedJob != null)
            {
                MessageBox.Show("Công việc đã được đánh dấu là xóa (IsDel = true).");
                await LoadJobs(isDeleted: false);
            }
            else
            {
                MessageBox.Show("Không tìm thấy công việc để xóa.");
            }
        }

        private async void radioButtonShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonShowActive.Checked)
            {
                await LoadJobs(isDeleted: false);
            }
        }

        private async void radioButtonShowActive_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonShowDeleted.Checked)
            {
                await LoadJobs(isDeleted: true);
            }
        }
    }
}
