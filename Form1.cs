using Npgsql;
using System.Data;
using System.ComponentModel;

namespace SoftwareManagerApp
{
    public partial class Form1 : Form
    {
        // Строка подключения к базе данных PostgreSQL.
        private readonly string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=postgres;Database=software_analogues;";

        public Form1()
        {
            InitializeComponent();
            // Первичная загрузка категорий при запуске формы.
            LoadCategories();
        }

        #region Загрузка данных

        // Загружает список категорий из БД в соответствующий ComboBox.
        private void LoadCategories()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT category_id, category_name FROM Categories ORDER BY category_name;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        categoriesComboBox.Items.Clear();
                        while (reader.Read())
                        {
                            categoriesComboBox.Items.Add(new ComboBoxItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке категорий: {ex.Message}");
            }
        }

        // Загружает список подборок из БД в соответствующий ComboBox.
        private void LoadCollections()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT collection_id, collection_name FROM Collections ORDER BY collection_name;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        collectionsComboBox.Items.Clear();
                        while (reader.Read())
                        {
                            collectionsComboBox.Items.Add(new ComboBoxItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке подборок: {ex.Message}");
            }
        }

        // Загружает программы для указанной категории.
        private void LoadSoftwareForCategory(int categoryId)
        {
            string sql = @"SELECT s.software_id, s.name AS ""Название"", d.developer_name AS ""Разработчик"", s.website AS ""Сайт"", s.description AS ""Описание""
                           FROM Software s
                           JOIN Developers d ON s.developer_id = d.developer_id
                           WHERE s.category_id = @id
                           ORDER BY s.name;";
            ExecuteSoftwareQuery(sql, categoryId);
        }

        // Загружает программы для указанной подборки.
        private void LoadSoftwareForCollection(int collectionId)
        {
            // Используется JOIN с промежуточной таблицей Software_Collections.
            string sql = @"SELECT s.software_id, s.name AS ""Название"", d.developer_name AS ""Разработчик"", s.website AS ""Сайт"", s.description AS ""Описание""
                           FROM Software s
                           JOIN Developers d ON s.developer_id = d.developer_id
                           JOIN Software_Collections sc ON s.software_id = sc.software_id
                           WHERE sc.collection_id = @id
                           ORDER BY s.name;";
            ExecuteSoftwareQuery(sql, collectionId);
        }

        // Универсальный метод для выполнения запроса и заполнения DataGridView.
        private void ExecuteSoftwareQuery(string sql, int id)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        var dataAdapter = new NpgsqlDataAdapter(cmd);
                        var dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        softwareDataGridView.DataSource = dataTable;

                        // Скрытие служебной колонки с идентификатором программы.
                        if (softwareDataGridView.Columns["software_id"] != null)
                        {
                            softwareDataGridView.Columns["software_id"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке программ: {ex.Message}");
            }
        }
        #endregion

        #region Обработчики событий

        // Обработчик смены режима просмотра (Категория/Подборка).
        private void RadioView_CheckedChanged(object sender, EventArgs e)
        {
            categoriesComboBox.Enabled = radioViewByCategory.Checked;
            collectionsComboBox.Enabled = radioViewByCollection.Checked;

            if (radioViewByCollection.Checked)
            {
                LoadCollections();
            }

            // Очистка таблицы при смене режима.
            if (softwareDataGridView.DataSource != null)
            {
                ((DataTable)softwareDataGridView.DataSource).Clear();
            }
        }

        // Обработчик выбора элемента в списке категорий.
        private void CategoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCategory = categoriesComboBox.SelectedItem as ComboBoxItem;
            if (selectedCategory != null) LoadSoftwareForCategory(selectedCategory.Id);
        }

        // Обработчик выбора элемента в списке подборок.
        private void CollectionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCollection = collectionsComboBox.SelectedItem as ComboBoxItem;
            if (selectedCollection != null) LoadSoftwareForCollection(selectedCollection.Id);
        }

        // Открывает форму управления подборками.
        private void BtnManageCollections_Click(object sender, EventArgs e)
        {
            using (var managerForm = new CollectionsManagerForm())
            {
                managerForm.ShowDialog(this);
                if (radioViewByCollection.Checked)
                {
                    LoadCollections();
                }
            }
        }

        // Обработчик события перед открытием контекстного меню.
        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            addToCollectionMenuItem.DropDownItems.Clear();
            if (softwareDataGridView.SelectedRows.Count == 0)
            {
                e.Cancel = true; // Отменяем открытие меню, если ничего не выбрано.
                return;
            }

            // Динамическое формирование подменю со списком подборок.
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT collection_id, collection_name FROM Collections ORDER BY collection_name;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ToolStripMenuItem(reader.GetString(1));
                            item.Tag = reader.GetInt32(0); // Сохраняем ID в свойстве Tag.
                            item.Click += AddToCollection_Click;
                            addToCollectionMenuItem.DropDownItems.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка загрузки подборок для меню: {ex.Message}"); }
        }

        // Обработчик клика по подменю "Добавить в подборку".
        private void AddToCollection_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            int collectionId = (int)menuItem.Tag;

            var selectedSoftwareIds = new List<int>();
            foreach (DataGridViewRow row in softwareDataGridView.SelectedRows)
            {
                selectedSoftwareIds.Add(Convert.ToInt32(row.Cells["software_id"].Value));
            }

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (var softwareId in selectedSoftwareIds)
                    {
                        // ON CONFLICT DO NOTHING предотвращает ошибку при попытке добавить дубликат.
                        string sql = "INSERT INTO Software_Collections (software_id, collection_id) VALUES (@s_id, @c_id) ON CONFLICT DO NOTHING;";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@s_id", softwareId);
                            cmd.Parameters.AddWithValue("@c_id", collectionId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Программы добавлены в подборку!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления в подборку: {ex.Message}");
            }
        }

        // Обработчик двойного клика по ячейке для просмотра детальной информации.
        private void SoftwareDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int softwareId = Convert.ToInt32(softwareDataGridView.Rows[e.RowIndex].Cells["software_id"].Value);
                using (var viewForm = new ViewDetailsForm(softwareId))
                {
                    viewForm.ShowDialog(this);
                }
            }
        }
        #endregion

        #region Кнопки (CRUD, Сравнение, Отчеты)

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var detailsForm = new SoftwareDetailsForm())
            {
                if (detailsForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Обновление активного представления.
                    if (radioViewByCategory.Checked) CategoriesComboBox_SelectedIndexChanged(null, null);
                    if (radioViewByCollection.Checked) CollectionsComboBox_SelectedIndexChanged(null, null);
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (softwareDataGridView.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(softwareDataGridView.SelectedRows[0].Cells["software_id"].Value);
            using (var detailsForm = new SoftwareDetailsForm(id))
            {
                if (detailsForm.ShowDialog(this) == DialogResult.OK)
                {
                    if (radioViewByCategory.Checked) CategoriesComboBox_SelectedIndexChanged(null, null);
                    if (radioViewByCollection.Checked) CollectionsComboBox_SelectedIndexChanged(null, null);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (softwareDataGridView.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(softwareDataGridView.SelectedRows[0].Cells["software_id"].Value);
            string name = softwareDataGridView.SelectedRows[0].Cells["Название"].Value.ToString();

            if (MessageBox.Show($"Удалить '{name}'?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        string sql = "DELETE FROM Software WHERE software_id = @id;";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    if (radioViewByCategory.Checked) CategoriesComboBox_SelectedIndexChanged(null, null);
                    if (radioViewByCollection.Checked) CollectionsComboBox_SelectedIndexChanged(null, null);
                }
                catch (Exception ex) { MessageBox.Show($"Ошибка удаления: {ex.Message}"); }
            }
        }

        private void BtnCompare_Click(object sender, EventArgs e)
        {
            if (softwareDataGridView.SelectedRows.Count < 2)
            {
                MessageBox.Show("Выберите две или более программы для сравнения.");
                return;
            }
            var ids = new List<int>();
            foreach (DataGridViewRow row in softwareDataGridView.SelectedRows)
            {
                ids.Add(Convert.ToInt32(row.Cells["software_id"].Value));
            }
            using (var comparisonForm = new ComparisonForm(ids))
            {
                comparisonForm.ShowDialog(this);
            }
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            using (var reportsForm = new ReportsForm())
            {
                reportsForm.ShowDialog(this);
            }
        }
        #endregion
    }
}