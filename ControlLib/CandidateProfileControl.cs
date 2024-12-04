using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ControlLib
{
    public partial class CandidateProfileControl : UserControl
    {
        private string beUrl = "http://localhost:5000";
        private string selectedCvLink;

        public CandidateProfileControl()
        {
            InitializeComponent();

            var contextMenu = new ContextMenuStrip();
            var viewPdfMenuItem = new ToolStripMenuItem("View PDF");

            contextMenu.Items.Add(viewPdfMenuItem);

            dgvCvLinks.ContextMenuStrip = contextMenu;

            viewPdfMenuItem.Click += ViewPdfMenuItem_Click;

            dgvCvLinks.CellClick += DgvCvLinks_CellClick;
            dgvCvLinks.CellMouseClick += DgvCvLinks_CellMouseClick;
        }

        private void DgvCvLinks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvCvLinks.ClearSelection();
                dgvCvLinks.Rows[e.RowIndex].Selected = true;

                string cvLink = dgvCvLinks.Rows[e.RowIndex].Cells[0].Value.ToString();

                selectedCvLink = cvLink;
            }
        }

        private void DgvCvLinks_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    selectedCvLink = dgvCvLinks.Rows[e.RowIndex].Cells[0].Value.ToString();
                    dgvCvLinks.ClearSelection();
                    dgvCvLinks.Rows[e.RowIndex].Selected = true;

                    dgvCvLinks.ContextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void ViewPdfMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedCvLink))
            {
                try
                {
                    string pdfUrl = $"{beUrl}{selectedCvLink}";

                    Process.Start(pdfUrl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể mở file PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một liên kết CV.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetDataSourceForGrid<T>(DataGridView dgv, IEnumerable<T> dataSource)
        {
            if (dataSource == null)
            {
                MessageBox.Show("Dữ liệu không hợp lệ.");
                return;
            }

            if (dataSource is IEnumerable<DataRow> dataRows)
            {
                DataTable dataTable = dataRows.CopyToDataTable();
                dgv.DataSource = dataTable;
            }
            else
            {
                if (dataSource is IEnumerable<string> stringList)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("CvLink");


                    foreach (var cvLink in stringList)
                    {
                        dataTable.Rows.Add(cvLink);
                    }

                    dgv.DataSource = dataTable;
                    dgv.Columns["CvLink"].Width = 300;

                    if (stringList.Any())
                    {
                        selectedCvLink = stringList.First();
                    }
                }
                else
                {
                    dgv.DataSource = dataSource.ToList();
                }
            }
        }

        public void SetDataCvLinks(IEnumerable<string> cvLinks)
        {
            SetDataSourceForGrid(dgvCvLinks, cvLinks);
        }

        public void SetDataSkills<T>(IEnumerable<T> dataSource)
        {
            SetDataSourceForGrid(dgvSkills, dataSource);
        }
    }
}
