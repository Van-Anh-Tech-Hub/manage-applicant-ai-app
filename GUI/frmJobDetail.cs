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
            ConfigureUI();
            LoadJobDetail();
        }

        private void ConfigureUI()
        {
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Text = "Job Detail";

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(10),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));


            Label lblTitleHeader = new Label
            {
                Text = "Title:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left
            };
            Label lblSalaryHeader = new Label
            {
                Text = "Salary:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left
            };
            Label lblExperienceHeader = new Label
            {
                Text = "Experience:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left
            };
            Label lblDeadlineHeader = new Label
            {
                Text = "Deadline:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left
            };
            Label lblCreatedAtHeader = new Label
            {
                Text = "Created At:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Left
            };

            lblTitle = new Label
            {
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            lblSalary = new Label
            {
                Font = new Font("Arial", 10),
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            lblExperience = new Label
            {
                Font = new Font("Arial", 10),
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            lblDeadline = new Label
            {
                Font = new Font("Arial", 10),
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            lblCreatedAt = new Label
            {
                Font = new Font("Arial", 10),
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };

            layout.Controls.Add(lblTitleHeader, 0, 0);
            layout.Controls.Add(lblTitle, 1, 0);
            layout.Controls.Add(lblSalaryHeader, 0, 1);
            layout.Controls.Add(lblSalary, 1, 1);
            layout.Controls.Add(lblExperienceHeader, 0, 2);
            layout.Controls.Add(lblExperience, 1, 2);
            layout.Controls.Add(lblDeadlineHeader, 0, 3);
            layout.Controls.Add(lblDeadline, 1, 3);
            layout.Controls.Add(lblCreatedAtHeader, 0, 4);
            layout.Controls.Add(lblCreatedAt, 1, 4);

            webBrowserDescription = new WebBrowser
            {
                Dock = DockStyle.Fill,
                MinimumSize = new System.Drawing.Size(500, 200)
            };
            Panel panelDescription = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            panelDescription.Controls.Add(webBrowserDescription);

            this.Controls.Add(layout);
            this.Controls.Add(panelDescription);

            panelDescription.Dock = DockStyle.Fill;
            layout.Dock = DockStyle.Top;
        }

        private async void LoadJobDetail()
        {
            var job = await _jobBLL.GetJobs(j => j.Id == _jobId);
            if (job != null)
            {
                var jobDetail = job.First();
                lblTitle.Text = jobDetail.Title;
                lblSalary.Text = $"Salary: {jobDetail.Salary:C}";
                lblExperience.Text = $"Experience: {jobDetail.Experience} years";
                lblDeadline.Text = $"Deadline: {jobDetail.Deadline:dd/MM/yyyy}";
                lblCreatedAt.Text = $"Created at: {jobDetail.CreatedAt:dd/MM/yyyy HH:mm:ss}";

                string htmlContent = jobDetail.Description;
                webBrowserDescription.DocumentText = htmlContent;
            }
            else
            {
                MessageBox.Show("Job not found!");
            }
        }
        private Label lblTitle;
        private Label lblSalary;
        private Label lblExperience;
        private Label lblDeadline;
        private Label lblCreatedAt;
        private WebBrowser webBrowserDescription;
    }
}
