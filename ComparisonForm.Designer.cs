namespace SoftwareManagerApp
{
    partial class ComparisonForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comparisonDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.comparisonDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // comparisonDataGridView
            // 
            this.comparisonDataGridView.AllowUserToAddRows = false;
            this.comparisonDataGridView.AllowUserToDeleteRows = false;
            this.comparisonDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.comparisonDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True; // Включаем перенос строк
            this.comparisonDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.comparisonDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comparisonDataGridView.Location = new System.Drawing.Point(0, 0);
            this.comparisonDataGridView.Name = "comparisonDataGridView";
            this.comparisonDataGridView.ReadOnly = true;
            this.comparisonDataGridView.RowTemplate.Height = 24; // Базовая высота
            this.comparisonDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells; // Авто-высота строк
            this.comparisonDataGridView.Size = new System.Drawing.Size(800, 450);
            this.comparisonDataGridView.TabIndex = 0;
            // 
            // ComparisonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comparisonDataGridView);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "ComparisonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сравнение программ";
            this.Load += new System.EventHandler(this.ComparisonForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.comparisonDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView comparisonDataGridView;
    }
}