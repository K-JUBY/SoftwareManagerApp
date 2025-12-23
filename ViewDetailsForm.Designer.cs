namespace SoftwareManagerApp
{
    partial class ViewDetailsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblProgramName = new System.Windows.Forms.Label();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDeveloperName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSystemReq = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxScreenshot = new System.Windows.Forms.GroupBox();
            this.picScreenshot = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxScreenshot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreenshot)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProgramName
            // 
            this.lblProgramName.AutoSize = true;
            this.lblProgramName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProgramName.Location = new System.Drawing.Point(12, 9);
            this.lblProgramName.Name = "lblProgramName";
            this.lblProgramName.Size = new System.Drawing.Size(155, 25);
            this.lblProgramName.TabIndex = 0;
            this.lblProgramName.Text = "Program Name";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.lblWebsite);
            this.groupBoxInfo.Controls.Add(this.label6);
            this.groupBoxInfo.Controls.Add(this.lblSize);
            this.groupBoxInfo.Controls.Add(this.label5);
            this.groupBoxInfo.Controls.Add(this.lblDeveloperName);
            this.groupBoxInfo.Controls.Add(this.label3);
            this.groupBoxInfo.Controls.Add(this.lblCategoryName);
            this.groupBoxInfo.Controls.Add(this.label1);
            this.groupBoxInfo.Location = new System.Drawing.Point(12, 49);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(460, 100);
            this.groupBoxInfo.TabIndex = 1;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Основная информация";
            // 
            // lblWebsite
            // 
            this.lblWebsite.AutoSize = true;
            this.lblWebsite.Location = new System.Drawing.Point(120, 72);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(12, 15);
            this.lblWebsite.TabIndex = 7;
            this.lblWebsite.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(6, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Веб-сайт:";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(120, 49);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(12, 15);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Объем (МБ):";
            // 
            // lblDeveloperName
            // 
            this.lblDeveloperName.AutoSize = true;
            this.lblDeveloperName.Location = new System.Drawing.Point(293, 26);
            this.lblDeveloperName.Name = "lblDeveloperName";
            this.lblDeveloperName.Size = new System.Drawing.Size(12, 15);
            this.lblDeveloperName.TabIndex = 3;
            this.lblDeveloperName.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(203, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Разработчик:";
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new System.Drawing.Point(82, 26);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(12, 15);
            this.lblCategoryName.TabIndex = 1;
            this.lblCategoryName.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Категория:";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescription.Location = new System.Drawing.Point(12, 179);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(460, 120);
            this.txtDescription.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Описание:";
            // 
            // txtSystemReq
            // 
            this.txtSystemReq.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSystemReq.Location = new System.Drawing.Point(12, 329);
            this.txtSystemReq.Multiline = true;
            this.txtSystemReq.Name = "txtSystemReq";
            this.txtSystemReq.ReadOnly = true;
            this.txtSystemReq.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSystemReq.Size = new System.Drawing.Size(460, 120);
            this.txtSystemReq.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Системные требования:";
            // 
            // groupBoxScreenshot
            // 
            this.groupBoxScreenshot.Controls.Add(this.picScreenshot);
            this.groupBoxScreenshot.Location = new System.Drawing.Point(478, 49);
            this.groupBoxScreenshot.Name = "groupBoxScreenshot";
            this.groupBoxScreenshot.Size = new System.Drawing.Size(310, 250);
            this.groupBoxScreenshot.TabIndex = 6;
            this.groupBoxScreenshot.TabStop = false;
            this.groupBoxScreenshot.Text = "Скриншот";
            // 
            // picScreenshot
            // 
            this.picScreenshot.BackColor = System.Drawing.SystemColors.ControlLight;
            this.picScreenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picScreenshot.Location = new System.Drawing.Point(3, 19);
            this.picScreenshot.Name = "picScreenshot";
            this.picScreenshot.Size = new System.Drawing.Size(304, 228);
            this.picScreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picScreenshot.TabIndex = 0;
            this.picScreenshot.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(668, 419);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // ViewDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 461);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBoxScreenshot);
            this.Controls.Add(this.txtSystemReq);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.lblProgramName);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подробная информация";
            this.Load += new System.EventHandler(this.ViewDetailsForm_Load);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxScreenshot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picScreenshot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblProgramName;
        private GroupBox groupBoxInfo;
        private Label lblWebsite;
        private Label label6;
        private Label lblSize;
        private Label label5;
        private Label lblDeveloperName;
        private Label label3;
        private Label lblCategoryName;
        private Label label1;
        private TextBox txtDescription;
        private Label label2;
        private TextBox txtSystemReq;
        private Label label4;
        private GroupBox groupBoxScreenshot;
        private PictureBox picScreenshot;
        private Button btnClose;
    }
}