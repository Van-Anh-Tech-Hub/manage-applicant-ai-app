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
            this.MinimumSize = new Size(800, 600); 
            this.Resize += frmManageJob_Resize;
            LoadData();
        }

        private void frmManageJob_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.ClientSize.Width - 20; 
            dataGridView1.Height = this.ClientSize.Height - 150; 
        }


        private async void LoadData()
        {
            await LoadCategories();
        }

        private async void frmManageJob_Load(object sender, EventArgs e)
        {
            await LoadJobs(isDeleted: false);
        }

      

        private async Task LoadCategories()
        {
            var categories = await _jobCategoryBLL.GetJobCategories(c => !c.IsDel);
            categories.Insert(0, new JobCategory { Id = string.Empty, Name = "Xem tất cả" });
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

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Title"].HeaderText = "Tiêu đề";
            dataGridView1.Columns["Salary"].HeaderText = "Lương";
            dataGridView1.Columns["Deadline"].HeaderText = "Hạn chót";
            dataGridView1.Columns["Headcount"].HeaderText = "Số lượng tuyển";
            dataGridView1.Columns["CreatedAt"].HeaderText = "Ngày tạo";

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
        }



        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string title = txtJob.Text.Trim();
            string categoryId = cbxDanhMuc.SelectedValue?.ToString();

            var jobs = await _jobBLL.GetJobs(
                job => (string.IsNullOrEmpty(title) || job.Title.Contains(title)) &&
                       (string.IsNullOrEmpty(categoryId) || job.CategoryId == categoryId || string.IsNullOrEmpty(job.CategoryId))
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
