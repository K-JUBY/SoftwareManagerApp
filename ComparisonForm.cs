using Npgsql;
using System.Data;
using System.Drawing;

namespace SoftwareManagerApp
{
    public partial class ComparisonForm : Form
    {
        private readonly string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=postgres;Database=software_analogues;";

        // Список ID программ, переданный с главной формы для сравнения.
        private readonly List<int> softwareIdsToCompare;

        public ComparisonForm(List<int> softwareIds)
        {
            InitializeComponent();
            this.softwareIdsToCompare = softwareIds;
        }

        // Запуск загрузки и отображения данных при открытии формы.
        private void ComparisonForm_Load(object sender, EventArgs e)
        {
            LoadAndDisplayComparisonData();
        }

        // Основной метод для загрузки и "транспонирования" данных для сравнения.
        private void LoadAndDisplayComparisonData()
        {
            try
            {
                // 1. Загрузка данных в стандартном виде (строки - программы, столбцы - характеристики).
                DataTable sourceData = new DataTable();
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    // ' = ANY(@ids)' - эффективный способ выборки по массиву ID в PostgreSQL.
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

                // 2. Создание "перевернутой" таблицы (строки - характеристики, столбцы - программы).
                DataTable comparisonTable = new DataTable();
                comparisonTable.Columns.Add("Характеристика");
                foreach (DataRow row in sourceData.Rows)
                {
                    // Названия программ становятся заголовками столбцов.
                    comparisonTable.Columns.Add(row["name"].ToString());
                }

                // 3. Перенос данных из исходной таблицы в транспонированную.
                AddComparisonRow(comparisonTable, sourceData, "Разработчик", "developer_name");
                AddComparisonRow(comparisonTable, sourceData, "Описание", "description");
                AddComparisonRow(comparisonTable, sourceData, "Системные требования", "system_requirements");
                AddComparisonRow(comparisonTable, sourceData, "Объем (МБ)", "size_mb");
                AddComparisonRow(comparisonTable, sourceData, "Веб-сайт", "website");

                // 4. Назначение подготовленной таблицы в качестве источника данных для DataGridView.
                comparisonDataGridView.DataSource = comparisonTable;

                // 5. Стилизация первого столбца для лучшей читаемости.
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

        // Вспомогательный метод для формирования одной строки в таблице сравнения.
        private void AddComparisonRow(DataTable comparisonTable, DataTable sourceData, string propertyName, string dbColumnName)
        {
            // Создание массива объектов для новой строки. Первым элементом идет название характеристики.
            List<object> rowData = new List<object> { propertyName };
            // Извлечение нужного значения из каждой строки исходных данных.
            foreach (DataRow sourceRow in sourceData.Rows)
            {
                rowData.Add(sourceRow[dbColumnName]);
            }
            comparisonTable.Rows.Add(rowData.ToArray());
        }
    }
}