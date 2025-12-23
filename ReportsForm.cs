using Npgsql;
using System.Data;

namespace SoftwareManagerApp
{
    public partial class ReportsForm : Form
    {
        private readonly string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=postgres;Database=software_analogues;";

        public ReportsForm()
        {
            InitializeComponent();
        }

        // Заполнение списка доступных отчетов.
        private void ReportsForm_Load(object sender, EventArgs e)
        {
            cmbReportType.Items.Add("Количество программ по категориям");
            cmbReportType.Items.Add("Программы по разработчикам");
            cmbReportType.SelectedIndex = 0;
        }

        // Отображение/скрытие дополнительных параметров в зависимости от типа отчета.
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

        // Загрузка списка разработчиков для отчета.
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

        // Выбор и запуск формирования нужного отчета.
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            string selectedReport = cmbReportType.SelectedItem.ToString();

            if (selectedReport == "Количество программ по категориям")
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

        // Формирование отчета по количеству программ в категориях.
        private void GenerateCategoryCountReport()
        {
            // LEFT JOIN используется, чтобы включить в отчет категории без программ (с 0).
            string sql = @"
                SELECT c.category_name AS ""Категория"", COUNT(s.software_id) AS ""Количество программ""
                FROM Categories c
                LEFT JOIN Software s ON c.category_id = s.category_id
                GROUP BY c.category_name
                ORDER BY ""Количество программ"" DESC;";

            ExecuteReportQuery(sql);
        }

        // Формирование отчета по программам указанного разработчика.
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

        // Общий метод для выполнения SQL-запроса и вывода результата в DataGridView.
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
    }
}