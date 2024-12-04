using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL.Models;

namespace GUI
{
    public partial class frmJobDetail : Form
    {
        private readonly JobBLL _jobBLL;
        private readonly string _jobId;

        public frmJobDetail(string jobId)
        {
            InitializeComponent();
            _jobBLL = new JobBLL();
            _jobId = jobId;
            LoadJobDetail();
        }

        private async void LoadJobDetail()
        {
            var job = await _jobBLL.GetJobs(j => j.Id == _jobId);
            if (job != null)
            {
                lblTitle.Text = job.First().Title;
                lblDescription.Text = job.First().Description;
                lblSalary.Text = $"Salary: {job.First().Salary:C}";
                lblExperience.Text = $"Experience: {job.First().Experience} years";
                lblDeadline.Text = $"Deadline: {job.First().Deadline.ToString("dd/MM/yyyy")}";
                lblCreatedAt.Text = $"Created at: {job.First().CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")}";
            }
        }
    }
}
