namespace SoftwareManagerApp
{
    partial class PasswordResetForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.btnRunUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRunUpdate
            // 
            this.btnRunUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRunUpdate.Location = new System.Drawing.Point(50, 60);
            this.btnRunUpdate.Name = "btnRunUpdate";
            this.btnRunUpdate.Size = new System.Drawing.Size(300, 50);
            this.btnRunUpdate.TabIndex = 0;
            this.btnRunUpdate.Text = "Обновить хеши паролей";
            this.btnRunUpdate.UseVisualStyleBackColor = true;
            this.btnRunUpdate.Click += new System.EventHandler(this.BtnRunUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Это одноразовый инструмент. Нажмите кнопку ниже, чтобы\r\nзашифровать пароли в базе данных, а затем закройте программу.";
            // 
            // PasswordResetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 141);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRunUpdate);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PasswordResetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обновление паролей";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private Button btnRunUpdate;
        private Label label1;
    }
}