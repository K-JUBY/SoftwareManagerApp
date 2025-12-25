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
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageDetails = new System.Windows.Forms.TabPage();
            this.groupBoxScreenshot = new System.Windows.Forms.GroupBox();
            this.picScreenshot = new System.Windows.Forms.PictureBox();
            this.txtSystemReq = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.lblIsFree = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDeveloperName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageReviews = new System.Windows.Forms.TabPage();
            this.btnAddReview = new System.Windows.Forms.Button();
            this.reviewsDataGridView = new System.Windows.Forms.DataGridView();
            this.tabControlMain.SuspendLayout();
            this.tabPageDetails.SuspendLayout();
            this.groupBoxScreenshot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreenshot)).BeginInit();
            this.groupBoxInfo.SuspendLayout();
            this.tabPageReviews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reviewsDataGridView)).BeginInit();
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
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(672, 519);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageDetails);
            this.tabControlMain.Controls.Add(this.tabPageReviews);
            this.tabControlMain.Location = new System.Drawing.Point(12, 37);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(780, 476);
            this.tabControlMain.TabIndex = 8;
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.groupBoxScreenshot);
            this.tabPageDetails.Controls.Add(this.txtSystemReq);
            this.tabPageDetails.Controls.Add(this.label4);
            this.tabPageDetails.Controls.Add(this.txtDescription);
            this.tabPageDetails.Controls.Add(this.label2);
            this.tabPageDetails.Controls.Add(this.groupBoxInfo);
            this.tabPageDetails.Location = new System.Drawing.Point(4, 24);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetails.Size = new System.Drawing.Size(772, 448);
            this.tabPageDetails.TabIndex = 0;
            this.tabPageDetails.Text = "Подробная информация";
            this.tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // groupBoxScreenshot
            // 
            this.groupBoxScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScreenshot.Controls.Add(this.picScreenshot);
            this.groupBoxScreenshot.Location = new System.Drawing.Point(456, 117);
            this.groupBoxScreenshot.Name = "groupBoxScreenshot";
            this.groupBoxScreenshot.Size = new System.Drawing.Size(310, 325);
            this.groupBoxScreenshot.TabIndex = 11;
            this.groupBoxScreenshot.TabStop = false;
            this.groupBoxScreenshot.Text = "Скриншот";
            // 
            // picScreenshot
            // 
            this.picScreenshot.BackColor = System.Drawing.SystemColors.ControlLight;
            this.picScreenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picScreenshot.Location = new System.Drawing.Point(3, 19);
            this.picScreenshot.Name = "picScreenshot";
            this.picScreenshot.Size = new System.Drawing.Size(304, 303);
            this.picScreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picScreenshot.TabIndex = 0;
            this.picScreenshot.TabStop = false;
            // 
            // txtSystemReq
            // 
            this.txtSystemReq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSystemReq.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSystemReq.Location = new System.Drawing.Point(6, 298);
            this.txtSystemReq.Multiline = true;
            this.txtSystemReq.Name = "txtSystemReq";
            this.txtSystemReq.ReadOnly = true;
            this.txtSystemReq.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSystemReq.Size = new System.Drawing.Size(444, 144);
            this.txtSystemReq.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Системные требования:";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescription.Location = new System.Drawing.Point(6, 148);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(444, 120);
            this.txtDescription.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Описание:";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfo.Controls.Add(this.lblIsFree);
            this.groupBoxInfo.Controls.Add(this.label7);
            this.groupBoxInfo.Controls.Add(this.lblWebsite);
            this.groupBoxInfo.Controls.Add(this.label6);
            this.groupBoxInfo.Controls.Add(this.lblSize);
            this.groupBoxInfo.Controls.Add(this.label5);
            this.groupBoxInfo.Controls.Add(this.lblDeveloperName);
            this.groupBoxInfo.Controls.Add(this.label3);
            this.groupBoxInfo.Controls.Add(this.lblCategoryName);
            this.groupBoxInfo.Controls.Add(this.label1);
            this.groupBoxInfo.Location = new System.Drawing.Point(6, 6);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(760, 105);
            this.groupBoxInfo.TabIndex = 6;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Основная информация";
            // 
            // lblIsFree
            // 
            this.lblIsFree.AutoSize = true;
            this.lblIsFree.Location = new System.Drawing.Point(313, 49);
            this.lblIsFree.Name = "lblIsFree";
            this.lblIsFree.Size = new System.Drawing.Size(12, 15);
            this.lblIsFree.TabIndex = 9;
            this.lblIsFree.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(232, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Лицензия:";
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
            // tabPageReviews
            // 
            this.tabPageReviews.Controls.Add(this.btnAddReview);
            this.tabPageReviews.Controls.Add(this.reviewsDataGridView);
            this.tabPageReviews.Location = new System.Drawing.Point(4, 24);
            this.tabPageReviews.Name = "tabPageReviews";
            this.tabPageReviews.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReviews.Size = new System.Drawing.Size(772, 448);
            this.tabPageReviews.TabIndex = 1;
            this.tabPageReviews.Text = "Отзывы";
            this.tabPageReviews.UseVisualStyleBackColor = true;
            // 
            // btnAddReview
            // 
            this.btnAddReview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddReview.Location = new System.Drawing.Point(616, 412);
            this.btnAddReview.Name = "btnAddReview";
            this.btnAddReview.Size = new System.Drawing.Size(150, 30);
            this.btnAddReview.TabIndex = 1;
            this.btnAddReview.Text = "Оставить свой отзыв...";
            this.btnAddReview.UseVisualStyleBackColor = true;
            this.btnAddReview.Click += new System.EventHandler(this.BtnAddReview_Click);
            // 
            // reviewsDataGridView
            // 
            this.reviewsDataGridView.AllowUserToAddRows = false;
            this.reviewsDataGridView.AllowUserToDeleteRows = false;
            this.reviewsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reviewsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.reviewsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reviewsDataGridView.Location = new System.Drawing.Point(6, 6);
            this.reviewsDataGridView.Name = "reviewsDataGridView";
            this.reviewsDataGridView.ReadOnly = true;
            this.reviewsDataGridView.RowTemplate.Height = 25;
            this.reviewsDataGridView.Size = new System.Drawing.Size(760, 400);
            this.reviewsDataGridView.TabIndex = 0;
            // 
            // ViewDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblProgramName);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(820, 600);
            this.Name = "ViewDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подробная информация";
            this.Load += new System.EventHandler(this.ViewDetailsForm_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageDetails.ResumeLayout(false);
            this.tabPageDetails.PerformLayout();
            this.groupBoxScreenshot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picScreenshot)).EndInit();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.tabPageReviews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reviewsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblProgramName;
        private Button btnClose;
        private TabControl tabControlMain;
        private TabPage tabPageDetails;
        private GroupBox groupBoxScreenshot;
        private PictureBox picScreenshot;
        private TextBox txtSystemReq;
        private Label label4;
        private TextBox txtDescription;
        private Label label2;
        private GroupBox groupBoxInfo;
        private Label lblWebsite;
        private Label label6;
        private Label lblSize;
        private Label label5;
        private Label lblDeveloperName;
        private Label label3;
        private Label lblCategoryName;
        private Label label1;
        private TabPage tabPageReviews;
        private Button btnAddReview;
        private DataGridView reviewsDataGridView;
        private Label lblIsFree;
        private Label label7;
    }
}