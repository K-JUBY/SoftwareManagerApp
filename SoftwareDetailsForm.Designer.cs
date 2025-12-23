namespace SoftwareManagerApp
{
    partial class SoftwareDetailsForm
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
            this.labelName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.labelDeveloper = new System.Windows.Forms.Label();
            this.cmbDeveloper = new System.Windows.Forms.ComboBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.labelSystemReq = new System.Windows.Forms.Label();
            this.txtSystemReq = new System.Windows.Forms.TextBox();
            this.labelWebsite = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.numSize = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxScreenshot = new System.Windows.Forms.GroupBox();
            this.btnDeleteImage = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.picScreenshot = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).BeginInit();
            this.groupBoxScreenshot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreenshot)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(13, 13);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(62, 15);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Название:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(150, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(622, 23);
            this.txtName.TabIndex = 1;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Location = new System.Drawing.Point(13, 42);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(66, 15);
            this.labelCategory.TabIndex = 2;
            this.labelCategory.Text = "Категория:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(150, 39);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(322, 23);
            this.cmbCategory.TabIndex = 3;
            // 
            // labelDeveloper
            // 
            this.labelDeveloper.AutoSize = true;
            this.labelDeveloper.Location = new System.Drawing.Point(13, 71);
            this.labelDeveloper.Name = "labelDeveloper";
            this.labelDeveloper.Size = new System.Drawing.Size(78, 15);
            this.labelDeveloper.TabIndex = 4;
            this.labelDeveloper.Text = "Разработчик:";
            // 
            // cmbDeveloper
            // 
            this.cmbDeveloper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeveloper.FormattingEnabled = true;
            this.cmbDeveloper.Location = new System.Drawing.Point(150, 68);
            this.cmbDeveloper.Name = "cmbDeveloper";
            this.cmbDeveloper.Size = new System.Drawing.Size(322, 23);
            this.cmbDeveloper.TabIndex = 5;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(13, 100);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(65, 15);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Описание:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(150, 97);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(322, 80);
            this.txtDescription.TabIndex = 7;
            // 
            // labelSystemReq
            // 
            this.labelSystemReq.AutoSize = true;
            this.labelSystemReq.Location = new System.Drawing.Point(13, 186);
            this.labelSystemReq.Name = "labelSystemReq";
            this.labelSystemReq.Size = new System.Drawing.Size(115, 15);
            this.labelSystemReq.TabIndex = 8;
            this.labelSystemReq.Text = "Сис. требования:";
            // 
            // txtSystemReq
            // 
            this.txtSystemReq.Location = new System.Drawing.Point(150, 183);
            this.txtSystemReq.Multiline = true;
            this.txtSystemReq.Name = "txtSystemReq";
            this.txtSystemReq.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSystemReq.Size = new System.Drawing.Size(322, 80);
            this.txtSystemReq.TabIndex = 9;
            // 
            // labelWebsite
            // 
            this.labelWebsite.AutoSize = true;
            this.labelWebsite.Location = new System.Drawing.Point(13, 272);
            this.labelWebsite.Name = "labelWebsite";
            this.labelWebsite.Size = new System.Drawing.Size(59, 15);
            this.labelWebsite.TabIndex = 10;
            this.labelWebsite.Text = "Веб-сайт:";
            // 
            // txtWebsite
            // 
            this.txtWebsite.Location = new System.Drawing.Point(150, 269);
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(322, 23);
            this.txtWebsite.TabIndex = 11;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(13, 301);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(73, 15);
            this.labelSize.TabIndex = 12;
            this.labelSize.Text = "Объем (МБ):";
            // 
            // numSize
            // 
            this.numSize.DecimalPlaces = 2;
            this.numSize.Location = new System.Drawing.Point(150, 299);
            this.numSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numSize.Name = "numSize";
            this.numSize.Size = new System.Drawing.Size(120, 23);
            this.numSize.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(556, 339);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(662, 339);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBoxScreenshot
            // 
            this.groupBoxScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScreenshot.Controls.Add(this.btnDeleteImage);
            this.groupBoxScreenshot.Controls.Add(this.btnLoadImage);
            this.groupBoxScreenshot.Controls.Add(this.picScreenshot);
            this.groupBoxScreenshot.Location = new System.Drawing.Point(478, 39);
            this.groupBoxScreenshot.Name = "groupBoxScreenshot";
            this.groupBoxScreenshot.Size = new System.Drawing.Size(294, 283);
            this.groupBoxScreenshot.TabIndex = 16;
            this.groupBoxScreenshot.TabStop = false;
            this.groupBoxScreenshot.Text = "Скриншот";
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteImage.Location = new System.Drawing.Point(188, 245);
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Size = new System.Drawing.Size(100, 32);
            this.btnDeleteImage.TabIndex = 2;
            this.btnDeleteImage.Text = "Удалить";
            this.btnDeleteImage.UseVisualStyleBackColor = true;
            this.btnDeleteImage.Click += new System.EventHandler(this.BtnDeleteImage_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadImage.Location = new System.Drawing.Point(6, 245);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(100, 32);
            this.btnLoadImage.TabIndex = 1;
            this.btnLoadImage.Text = "Загрузить...";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.BtnLoadImage_Click);
            // 
            // picScreenshot
            // 
            this.picScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picScreenshot.BackColor = System.Drawing.SystemColors.ControlLight;
            this.picScreenshot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picScreenshot.Location = new System.Drawing.Point(6, 22);
            this.picScreenshot.Name = "picScreenshot";
            this.picScreenshot.Size = new System.Drawing.Size(282, 217);
            this.picScreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picScreenshot.TabIndex = 0;
            this.picScreenshot.TabStop = false;
            // 
            // SoftwareDetailsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(784, 381);
            this.Controls.Add(this.groupBoxScreenshot);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numSize);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.txtWebsite);
            this.Controls.Add(this.labelWebsite);
            this.Controls.Add(this.txtSystemReq);
            this.Controls.Add(this.labelSystemReq);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.cmbDeveloper);
            this.Controls.Add(this.labelDeveloper);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.labelCategory);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labelName);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 420);
            this.Name = "SoftwareDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Данные о программе";
            this.Load += new System.EventHandler(this.SoftwareDetailsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
            this.groupBoxScreenshot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picScreenshot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelName;
        private TextBox txtName;
        private Label labelCategory;
        private ComboBox cmbCategory;
        private Label labelDeveloper;
        private ComboBox cmbDeveloper;
        private Label labelDescription;
        private TextBox txtDescription;
        private Label labelSystemReq;
        private TextBox txtSystemReq;
        private Label labelWebsite;
        private TextBox txtWebsite;
        private Label labelSize;
        private NumericUpDown numSize;
        private Button btnSave;
        private Button btnCancel;
        private GroupBox groupBoxScreenshot;
        private Button btnDeleteImage;
        private Button btnLoadImage;
        private PictureBox picScreenshot;
    }
}