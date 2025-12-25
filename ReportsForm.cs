using Npgsql;
using System.Data;
using System.IO;
using System.Text;

namespace SoftwareManagerApp
{
    public partial class ReportsForm : Form
    {
        private readonly string connectionString = DbConnectionManager.ConnectionString;

        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            cmbReportType.Items.Add("Все программы в базе данных");
            cmbReportType.Items.Add("Количество программ по категориям");
            cmbReportType.Items.Add("Программы по разработчикам");
            cmbReportType.SelectedIndex = 0;
        }

        // Управляет видимостью дополнительных полей в зависимости от типа отчета.
        private void CmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem.ToString() == "Программы по разработчикам")
            {
                lblParameter.Text = "Разработчик:";
                lblParameter.Visible = true;
                cmbParameter.Visible = true;
                LoadDevelopers();
            }
            else
            {
                lblParameter.Visible = false;
                cmbParameter.Visible = false;
            }
        }

        // Загружает список разработчиков для фильтра отчета.
        private void LoadDevelopers()
        {
            cmbParameter.Items.Clear();
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT developer_id, developer_name FROM Developers ORDER BY developer_name;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbParameter.Items.Add(new ComboBoxItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки разработчиков: {ex.Message}");
            }
        }

        // Запускает формирование выбранного отчета.
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            string selectedReport = cmbReportType.SelectedItem.ToString();

            if (selectedReport == "Все программы в базе данных")
            {
                GenerateAllSoftwareReport();
            }
            else if (selectedReport == "Количество программ по категориям")
            {
                GenerateCategoryCountReport();
            }
            else if (selectedReport == "Программы по разработчикам")
            {
                if (cmbParameter.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите разработчика.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int developerId = ((ComboBoxItem)cmbParameter.SelectedItem).Id;
                GenerateDeveloperSoftwareReport(developerId);
            }
        }

        // Формирует отчет со списком всех программ.
        private void GenerateAllSoftwareReport()
        {
            string sql = @"
                SELECT 
                    s.name AS ""Название"",
                    c.category_name AS ""Категория"",
                    d.developer_name AS ""Разработчик"",
                    CASE WHEN s.is_free THEN 'Бесплатная' ELSE 'Платная' END AS ""Лицензия"",
                    s.size_mb AS ""Объем (МБ)"",
                    s.website AS ""Сайт""
                FROM Software s
                LEFT JOIN Categories c ON s.category_id = c.category_id
                LEFT JOIN Developers d ON s.developer_id = d.developer_id
                ORDER BY s.name;";

            ExecuteReportQuery(sql);
        }

        // Формирует отчет с подсчетом программ в каждой категории.
        private void GenerateCategoryCountReport()
        {
            string sql = @"
                SELECT c.category_name AS ""Категория"", COUNT(s.software_id) AS ""Количество программ""
                FROM Categories c
                LEFT JOIN Software s ON c.category_id = s.category_id
                GROUP BY c.category_name
                ORDER BY ""Количество программ"" DESC;";

            ExecuteReportQuery(sql);
        }

        // Формирует отчет со списком программ указанного разработчика.
        private void GenerateDeveloperSoftwareReport(int developerId)
        {
            string sql = $@"
                SELECT s.name AS ""Название"", c.category_name AS ""Категория"", s.website AS ""Сайт""
                FROM Software s
                JOIN Categories c ON s.category_id = c.category_id
                WHERE s.developer_id = {developerId}
                ORDER BY c.category_name, s.name;";

            ExecuteReportQuery(sql);
        }

        // Универсальный метод для выполнения запроса и вывода результата.
        private void ExecuteReportQuery(string sql)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var adapter = new NpgsqlDataAdapter(sql, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        reportDataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}");
            }
        }

        // Экспортирует данные из DataGridView в CSV файл.
        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            if (reportDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV файл (*.csv)|*.csv";
                saveFileDialog.Title = "Сохранить отчет как CSV";
                saveFileDialog.FileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var sb = new StringBuilder();

                        // Формирование заголовков.
                        var headers = reportDataGridView.Columns.Cast<DataGridViewColumn>();
                        sb.AppendLine(string.Join(";", headers.Select(column => $"\"{column.HeaderText}\"")));

                        // Формирование строк с данными.
                        foreach (DataGridViewRow row in reportDataGridView.Rows)
                        {
                            var cells = row.Cells.Cast<DataGridViewCell>();
                            sb.AppendLine(string.Join(";", cells.Select(cell => $"\"{cell.Value}\"")));
                        }

                        // Сохранение в файл с кодировкой UTF-8 для поддержки кириллицы.
                        File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);

                        MessageBox.Show("Отчет успешно экспортирован!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}