namespace SoftwareManagerApp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.categoriesLabel = new System.Windows.Forms.Label();
            this.categoriesComboBox = new System.Windows.Forms.ComboBox();
            this.softwareLabel = new System.Windows.Forms.Label();
            this.softwareDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToCollectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.collectionsComboBox = new System.Windows.Forms.ComboBox();
            this.btnManageCollections = new System.Windows.Forms.Button();
            this.groupBoxViewMode = new System.Windows.Forms.GroupBox();
            this.radioViewByCollection = new System.Windows.Forms.RadioButton();
            this.radioViewByCategory = new System.Windows.Forms.RadioButton();
            this.btnChangeUser = new System.Windows.Forms.Button();
            this.cmbLicenseFilter = new System.Windows.Forms.ComboBox();
            this.labelLicense = new System.Windows.Forms.Label();
            this.btnManageUsers = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.softwareDataGridView)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.groupBoxViewMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // categoriesLabel
            // 
            this.categoriesLabel.AutoSize = true;
            this.categoriesLabel.Location = new System.Drawing.Point(12, 15);
            this.categoriesLabel.Name = "categoriesLabel";
            this.categoriesLabel.Size = new System.Drawing.Size(127, 15);
            this.categoriesLabel.TabIndex = 0;
            this.categoriesLabel.Text = "Выберите категорию:";
            // 
            // categoriesComboBox
            // 
            this.categoriesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoriesComboBox.FormattingEnabled = true;
            this.categoriesComboBox.Location = new System.Drawing.Point(145, 12);
            this.categoriesComboBox.Name = "categoriesComboBox";
            this.categoriesComboBox.Size = new System.Drawing.Size(260, 23);
            this.categoriesComboBox.TabIndex = 1;
            this.categoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
            // 
            // softwareLabel
            // 
            this.softwareLabel.AutoSize = true;
            this.softwareLabel.Location = new System.Drawing.Point(12, 107);
            this.softwareLabel.Name = "softwareLabel";
            this.softwareLabel.Size = new System.Drawing.Size(73, 15);
            this.softwareLabel.TabIndex = 2;
            this.softwareLabel.Text = "Программы:";
            // 
            // softwareDataGridView
            // 
            this.softwareDataGridView.AllowUserToAddRows = false;
            this.softwareDataGridView.AllowUserToDeleteRows = false;
            this.softwareDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.softwareDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.softwareDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.softwareDataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.softwareDataGridView.Location = new System.Drawing.Point(12, 125);
            this.softwareDataGridView.MultiSelect = true;
            this.softwareDataGridView.Name = "softwareDataGridView";
            this.softwareDataGridView.ReadOnly = true;
            this.softwareDataGridView.RowTemplate.Height = 25;
            this.softwareDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.softwareDataGridView.Size = new System.Drawing.Size(776, 383);
            this.softwareDataGridView.TabIndex = 3;
            this.softwareDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SoftwareDataGridView_CellDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToCollectionMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(193, 26);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // addToCollectionMenuItem
            // 
            this.addToCollectionMenuItem.Name = "addToCollectionMenuItem";
            this.addToCollectionMenuItem.Size = new System.Drawing.Size(192, 22);
            this.addToCollectionMenuItem.Text = "Добавить в подборку";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(12, 523);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Добавить...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(118, 523);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 30);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Редактировать...";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(244, 523);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnCompare
            // 
            this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompare.Location = new System.Drawing.Point(562, 523);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(110, 30);
            this.btnCompare.TabIndex = 7;
            this.btnCompare.Text = "Сравнить...";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.BtnCompare_Click);
            // 
            // btnReports
            // 
            this.btnReports.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReports.Location = new System.Drawing.Point(678, 523);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(110, 30);
            this.btnReports.TabIndex = 8;
            this.btnReports.Text = "Отчеты...";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.BtnReports_Click);
            // 
            // collectionsComboBox
            // 
            this.collectionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.collectionsComboBox.Enabled = false;
            this.collectionsComboBox.FormattingEnabled = true;
            this.collectionsComboBox.Location = new System.Drawing.Point(145, 50);
            this.collectionsComboBox.Name = "collectionsComboBox";
            this.collectionsComboBox.Size = new System.Drawing.Size(260, 23);
            this.collectionsComboBox.TabIndex = 9;
            this.collectionsComboBox.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
            // 
            // btnManageCollections
            // 
            this.btnManageCollections.Location = new System.Drawing.Point(411, 50);
            this.btnManageCollections.Name = "btnManageCollections";
            this.btnManageCollections.Size = new System.Drawing.Size(26, 23);
            this.btnManageCollections.TabIndex = 10;
            this.btnManageCollections.Text = "...";
            this.btnManageCollections.UseVisualStyleBackColor = true;
            this.btnManageCollections.Click += new System.EventHandler(this.BtnManageCollections_Click);
            // 
            // groupBoxViewMode
            // 
            this.groupBoxViewMode.Controls.Add(this.radioViewByCollection);
            this.groupBoxViewMode.Controls.Add(this.radioViewByCategory);
            this.groupBoxViewMode.Location = new System.Drawing.Point(461, 12);
            this.groupBoxViewMode.Name = "groupBoxViewMode";
            this.groupBoxViewMode.Size = new System.Drawing.Size(200, 80);
            this.groupBoxViewMode.TabIndex = 11;
            this.groupBoxViewMode.TabStop = false;
            this.groupBoxViewMode.Text = "Режим просмотра";
            // 
            // radioViewByCollection
            // 
            this.radioViewByCollection.AutoSize = true;
            this.radioViewByCollection.Location = new System.Drawing.Point(6, 47);
            this.radioViewByCollection.Name = "radioViewByCollection";
            this.radioViewByCollection.Size = new System.Drawing.Size(102, 19);
            this.radioViewByCollection.TabIndex = 1;
            this.radioViewByCollection.Text = "По подборке";
            this.radioViewByCollection.UseVisualStyleBackColor = true;
            this.radioViewByCollection.CheckedChanged += new System.EventHandler(this.RadioView_CheckedChanged);
            // 
            // radioViewByCategory
            // 
            this.radioViewByCategory.AutoSize = true;
            this.radioViewByCategory.Checked = true;
            this.radioViewByCategory.Location = new System.Drawing.Point(6, 22);
            this.radioViewByCategory.Name = "radioViewByCategory";
            this.radioViewByCategory.Size = new System.Drawing.Size(99, 19);
            this.radioViewByCategory.TabIndex = 0;
            this.radioViewByCategory.TabStop = true;
            this.radioViewByCategory.Text = "По категории";
            this.radioViewByCategory.UseVisualStyleBackColor = true;
            this.radioViewByCategory.CheckedChanged += new System.EventHandler(this.RadioView_CheckedChanged);
            // 
            // btnChangeUser
            // 
            this.btnChangeUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeUser.Location = new System.Drawing.Point(678, 12);
            this.btnChangeUser.Name = "btnChangeUser";
            this.btnChangeUser.Size = new System.Drawing.Size(110, 23);
            this.btnChangeUser.TabIndex = 12;
            this.btnChangeUser.Text = "Выйти";
            this.btnChangeUser.UseVisualStyleBackColor = true;
            this.btnChangeUser.Click += new System.EventHandler(this.BtnChangeUser_Click);
            // 
            // cmbLicenseFilter
            // 
            this.cmbLicenseFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLicenseFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLicenseFilter.FormattingEnabled = true;
            this.cmbLicenseFilter.Location = new System.Drawing.Point(678, 98);
            this.cmbLicenseFilter.Name = "cmbLicenseFilter";
            this.cmbLicenseFilter.Size = new System.Drawing.Size(110, 23);
            this.cmbLicenseFilter.TabIndex = 13;
            this.cmbLicenseFilter.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
            // 
            // labelLicense
            // 
            this.labelLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLicense.AutoSize = true;
            this.labelLicense.Location = new System.Drawing.Point(609, 101);
            this.labelLicense.Name = "labelLicense";
            this.labelLicense.Size = new System.Drawing.Size(63, 15);
            this.labelLicense.TabIndex = 14;
            this.labelLicense.Text = "Лицензия:";
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnManageUsers.Location = new System.Drawing.Point(350, 523);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(150, 30);
            this.btnManageUsers.TabIndex = 15;
            this.btnManageUsers.Text = "Упр. пользователями";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.BtnManageUsers_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 565);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.labelLicense);
            this.Controls.Add(this.cmbLicenseFilter);
            this.Controls.Add(this.btnChangeUser);
            this.Controls.Add(this.groupBoxViewMode);
            this.Controls.Add(this.btnManageCollections);
            this.Controls.Add(this.collectionsComboBox);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.softwareDataGridView);
            this.Controls.Add(this.softwareLabel);
            this.Controls.Add(this.categoriesComboBox);
            this.Controls.Add(this.categoriesLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "Form1";
            this.Text = "Информационная система: Аналоги ПО";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.softwareDataGridView)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.groupBoxViewMode.ResumeLayout(false);
            this.groupBoxViewMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label categoriesLabel;
        private ComboBox categoriesComboBox;
        private Label softwareLabel;
        private DataGridView softwareDataGridView;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnCompare;
        private Button btnReports;
        private ComboBox collectionsComboBox;
        private Button btnManageCollections;
        private GroupBox groupBoxViewMode;
        private RadioButton radioViewByCollection;
        private RadioButton radioViewByCategory;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem addToCollectionMenuItem;
        private Button btnChangeUser;
        private ComboBox cmbLicenseFilter;
        private Label labelLicense;
        private Button btnManageUsers;
    }
}