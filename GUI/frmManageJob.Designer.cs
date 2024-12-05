namespace GUI
{
    partial class frmManageJob
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbxDanhMuc = new System.Windows.Forms.ComboBox();
            this.txtJob = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDeleteJob = new System.Windows.Forms.Button();
            this.radioButtonShowDeleted = new System.Windows.Forms.RadioButton();
            this.radioButtonShowActive = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(733, 147);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(160, 41);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbxDanhMuc
            // 
            this.cbxDanhMuc.FormattingEnabled = true;
            this.cbxDanhMuc.Location = new System.Drawing.Point(396, 154);
            this.cbxDanhMuc.Name = "cbxDanhMuc";
            this.cbxDanhMuc.Size = new System.Drawing.Size(236, 28);
            this.cbxDanhMuc.TabIndex = 3;
            // 
            // txtJob
            // 
            this.txtJob.Location = new System.Drawing.Point(46, 154);
            this.txtJob.Name = "txtJob";
            this.txtJob.Size = new System.Drawing.Size(241, 26);
            this.txtJob.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-1, 226);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1507, 496);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // btnDeleteJob
            // 
            this.btnDeleteJob.Location = new System.Drawing.Point(973, 147);
            this.btnDeleteJob.Name = "btnDeleteJob";
            this.btnDeleteJob.Size = new System.Drawing.Size(165, 40);
            this.btnDeleteJob.TabIndex = 5;
            this.btnDeleteJob.Text = "Xóa công việc";
            this.btnDeleteJob.UseVisualStyleBackColor = true;
            this.btnDeleteJob.Click += new System.EventHandler(this.btnDeleteJob_Click);
            // 
            // radioButtonShowDeleted
            // 
            this.radioButtonShowDeleted.AutoSize = true;
            this.radioButtonShowDeleted.Location = new System.Drawing.Point(1211, 112);
            this.radioButtonShowDeleted.Name = "radioButtonShowDeleted";
            this.radioButtonShowDeleted.Size = new System.Drawing.Size(238, 24);
            this.radioButtonShowDeleted.TabIndex = 6;
            this.radioButtonShowDeleted.TabStop = true;
            this.radioButtonShowDeleted.Text = "Hiển thị các công việc đã xóa";
            this.radioButtonShowDeleted.UseVisualStyleBackColor = true;
            this.radioButtonShowDeleted.CheckedChanged += new System.EventHandler(this.radioButtonShowDeleted_CheckedChanged);
            // 
            // radioButtonShowActive
            // 
            this.radioButtonShowActive.AutoSize = true;
            this.radioButtonShowActive.Location = new System.Drawing.Point(1211, 178);
            this.radioButtonShowActive.Name = "radioButtonShowActive";
            this.radioButtonShowActive.Size = new System.Drawing.Size(255, 24);
            this.radioButtonShowActive.TabIndex = 7;
            this.radioButtonShowActive.TabStop = true;
            this.radioButtonShowActive.Text = "Hiển thị các công việc chưa xóa";
            this.radioButtonShowActive.UseVisualStyleBackColor = true;
            this.radioButtonShowActive.CheckedChanged += new System.EventHandler(this.radioButtonShowActive_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tên công việc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(409, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Ngành";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arrus-Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(235, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(864, 33);
            this.label2.TabIndex = 11;
            this.label2.Text = "DANH SÁCH CÔNG VIỆC ĐƯỢC ĐĂNG BỞI NHÀ TUYỂN DỤNG";
            // 
            // frmManageJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1505, 721);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonShowActive);
            this.Controls.Add(this.radioButtonShowDeleted);
            this.Controls.Add(this.btnDeleteJob);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cbxDanhMuc);
            this.Controls.Add(this.txtJob);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmManageJob";
            this.Text = "frmManageJob";
            this.Load += new System.EventHandler(this.frmManageJob_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbxDanhMuc;
        private System.Windows.Forms.TextBox txtJob;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDeleteJob;
        private System.Windows.Forms.RadioButton radioButtonShowDeleted;
        private System.Windows.Forms.RadioButton radioButtonShowActive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}