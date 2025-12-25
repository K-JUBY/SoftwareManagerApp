namespace SoftwareManagerApp
{
    partial class UserManagerForm
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
            this.usersDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBoxAddUser = new System.Windows.Forms.GroupBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.radioRoleUser = new System.Windows.Forms.RadioButton();
            this.radioRoleAdmin = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).BeginInit();
            this.groupBoxAddUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // usersDataGridView
            // 
            this.usersDataGridView.AllowUserToAddRows = false;
            this.usersDataGridView.AllowUserToDeleteRows = false;
            this.usersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.usersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usersDataGridView.Location = new System.Drawing.Point(12, 12);
            this.usersDataGridView.MultiSelect = false;
            this.usersDataGridView.Name = "usersDataGridView";
            this.usersDataGridView.ReadOnly = true;
            this.usersDataGridView.RowTemplate.Height = 25;
            this.usersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersDataGridView.Size = new System.Drawing.Size(560, 200);
            this.usersDataGridView.TabIndex = 0;
            // 
            // groupBoxAddUser
            // 
            this.groupBoxAddUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAddUser.Controls.Add(this.btnAddUser);
            this.groupBoxAddUser.Controls.Add(this.radioRoleUser);
            this.groupBoxAddUser.Controls.Add(this.radioRoleAdmin);
            this.groupBoxAddUser.Controls.Add(this.label3);
            this.groupBoxAddUser.Controls.Add(this.txtNewPassword);
            this.groupBoxAddUser.Controls.Add(this.label2);
            this.groupBoxAddUser.Controls.Add(this.txtNewUsername);
            this.groupBoxAddUser.Controls.Add(this.label1);
            this.groupBoxAddUser.Location = new System.Drawing.Point(12, 252);
            this.groupBoxAddUser.Name = "groupBoxAddUser";
            this.groupBoxAddUser.Size = new System.Drawing.Size(560, 137);
            this.groupBoxAddUser.TabIndex = 1;
            this.groupBoxAddUser.TabStop = false;
            this.groupBoxAddUser.Text = "Добавить нового пользователя";
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(423, 87);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(131, 30);
            this.btnAddUser.TabIndex = 7;
            this.btnAddUser.Text = "Добавить";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.BtnAddUser_Click);
            // 
            // radioRoleUser
            // 
            this.radioRoleUser.AutoSize = true;
            this.radioRoleUser.Checked = true;
            this.radioRoleUser.Location = new System.Drawing.Point(197, 93);
            this.radioRoleUser.Name = "radioRoleUser";
            this.radioRoleUser.Size = new System.Drawing.Size(103, 19);
            this.radioRoleUser.TabIndex = 6;
            this.radioRoleUser.TabStop = true;
            this.radioRoleUser.Text = "Пользователь";
            this.radioRoleUser.UseVisualStyleBackColor = true;
            // 
            // radioRoleAdmin
            // 
            this.radioRoleAdmin.AutoSize = true;
            this.radioRoleAdmin.Location = new System.Drawing.Point(82, 93);
            this.radioRoleAdmin.Name = "radioRoleAdmin";
            this.radioRoleAdmin.Size = new System.Drawing.Size(109, 19);
            this.radioRoleAdmin.TabIndex = 5;
            this.radioRoleAdmin.Text = "Администратор";
            this.radioRoleAdmin.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Роль:";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(82, 58);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(260, 23);
            this.txtNewPassword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Пароль:";
            // 
            // txtNewUsername
            // 
            this.txtNewUsername.Location = new System.Drawing.Point(82, 26);
            this.txtNewUsername.Name = "txtNewUsername";
            this.txtNewUsername.Size = new System.Drawing.Size(260, 23);
            this.txtNewUsername.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя:";
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteUser.Location = new System.Drawing.Point(12, 218);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteUser.TabIndex = 2;
            this.btnDeleteUser.Text = "Удалить выбранного";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.BtnDeleteUser_Click);
            // 
            // UserManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 401);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.groupBoxAddUser);
            this.Controls.Add(this.usersDataGridView);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(600, 440);
            this.Name = "UserManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Управление пользователями";
            this.Load += new System.EventHandler(this.UserManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).EndInit();
            this.groupBoxAddUser.ResumeLayout(false);
            this.groupBoxAddUser.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private DataGridView usersDataGridView;
        private GroupBox groupBoxAddUser;
        private Button btnAddUser;
        private RadioButton radioRoleUser;
        private RadioButton radioRoleAdmin;
        private Label label3;
        private TextBox txtNewPassword;
        private Label label2;
        private TextBox txtNewUsername;
        private Label label1;
        private Button btnDeleteUser;
    }
}