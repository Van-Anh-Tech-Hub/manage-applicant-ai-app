namespace GUI
{
    partial class frmJobDetail
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblSalary = new System.Windows.Forms.Label();
            this.lblExperience = new System.Windows.Forms.Label();
            this.lblDeadline = new System.Windows.Forms.Label();
            this.lblCreatedAt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(125, 39);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(276, 53);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label1";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(129, 137);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(276, 53);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "label2";
            // 
            // lblSalary
            // 
            this.lblSalary.Location = new System.Drawing.Point(129, 252);
            this.lblSalary.Name = "lblSalary";
            this.lblSalary.Size = new System.Drawing.Size(276, 53);
            this.lblSalary.TabIndex = 2;
            this.lblSalary.Text = "label3";
            // 
            // lblExperience
            // 
            this.lblExperience.Location = new System.Drawing.Point(129, 358);
            this.lblExperience.Name = "lblExperience";
            this.lblExperience.Size = new System.Drawing.Size(276, 53);
            this.lblExperience.TabIndex = 3;
            this.lblExperience.Text = "label4";
            // 
            // lblDeadline
            // 
            this.lblDeadline.Location = new System.Drawing.Point(125, 467);
            this.lblDeadline.Name = "lblDeadline";
            this.lblDeadline.Size = new System.Drawing.Size(276, 53);
            this.lblDeadline.TabIndex = 4;
            this.lblDeadline.Text = "label5";
            // 
            // lblCreatedAt
            // 
            this.lblCreatedAt.Location = new System.Drawing.Point(125, 585);
            this.lblCreatedAt.Name = "lblCreatedAt";
            this.lblCreatedAt.Size = new System.Drawing.Size(276, 53);
            this.lblCreatedAt.TabIndex = 5;
            this.lblCreatedAt.Text = "label6";
            // 
            // frmJobDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 663);
            this.Controls.Add(this.lblCreatedAt);
            this.Controls.Add(this.lblDeadline);
            this.Controls.Add(this.lblExperience);
            this.Controls.Add(this.lblSalary);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmJobDetail";
            this.Text = "frmJobDetail";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSalary;
        private System.Windows.Forms.Label lblExperience;
        private System.Windows.Forms.Label lblDeadline;
        private System.Windows.Forms.Label lblCreatedAt;
    }
}