using Npgsql;
using System.Data;
using System.Drawing;

namespace SoftwareManagerApp
{
    public partial class ComparisonForm : Form
    {
        private readonly string connectionString = DbConnectionManager.ConnectionString;
        // Список ID программ для сравнения, полученный из главной формы.
        private readonly List<int> softwareIdsToCompare;

        public ComparisonForm(List<int> softwareIds)
        {
            InitializeComponent();
            this.softwareIdsToCompare = softwareIds;
        }

        private void ComparisonForm_Load(object sender, EventArgs e)
        {
            LoadAndDisplayComparisonData();
        }

        // Загружает данные и преобразует их в транспонированный вид для сравнения.
        private void LoadAndDisplayComparisonData()
        {
            try
            {
                // 1. Загрузка данных в стандартную таблицу.
                DataTable sourceData = new DataTable();
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    // ' = ANY(@ids)' - эффективный способ выборки по массиву ID.
                    string sql = @"SELECT 
                                     s.name, d.developer_name, s.description, s.system_requirements, 
                                     s.size_mb, s.website 
                                   FROM Software s
                                   JOIN Developers d ON s.developer_id = d.developer_id
                                   WHERE s.software_id = ANY(@ids);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ids", softwareIdsToCompare);
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                        adapter.Fill(sourceData);
                    }
                }

                // 2. Создание финальной таблицы для сравнения.
                DataTable comparisonTable = new DataTable();
                comparisonTable.Columns.Add("Характеристика");
                foreach (DataRow row in sourceData.Rows)
                {
                    comparisonTable.Columns.Add(row["name"].ToString());
                }

                // 3. Перенос данных из исходной таблицы в таблицу для сравнения.
                AddComparisonRow(comparisonTable, sourceData, "Разработчик", "developer_name");
                AddComparisonRow(comparisonTable, sourceData, "Описание", "description");
                AddComparisonRow(comparisonTable, sourceData, "Системные требования", "system_requirements");
                AddComparisonRow(comparisonTable, sourceData, "Объем (МБ)", "size_mb");
                AddComparisonRow(comparisonTable, sourceData, "Веб-сайт", "website");

                comparisonDataGridView.DataSource = comparisonTable;

                // 4. Стилизация первого столбца.
                if (comparisonDataGridView.Columns.Count > 0)
                {
                    comparisonDataGridView.Columns[0].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                    comparisonDataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных для сравнения: {ex.Message}");
            }
        }

        // Вспомогательный метод для формирования строки в таблице сравнения.
        private void AddComparisonRow(DataTable comparisonTable, DataTable sourceData, string propertyName, string dbColumnName)
        {
            List<object> rowData = new List<object> { propertyName };
            foreach (DataRow sourceRow in sourceData.Rows)
            {
                rowData.Add(sourceRow[dbColumnName]);
            }
            comparisonTable.Rows.Add(rowData.ToArray());
        }
    }
}