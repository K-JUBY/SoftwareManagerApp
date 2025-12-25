using Npgsql;
using System.Data;
using System.ComponentModel;
using System.Text;

namespace SoftwareManagerApp
{
    public partial class Form1 : Form
    {
        private readonly string connectionString = DbConnectionManager.ConnectionString;
        // Флаг для управления циклом смены пользователя в Program.cs.
        public bool IsUserChanged { get; private set; } = false;

        public Form1()
        {
            InitializeComponent();
        }

        // Выполняется один раз при загрузке формы.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Настройка видимости элементов управления в зависимости от роли пользователя.
            if (CurrentUser.Role == "User")
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnReports.Visible = false;
                btnManageUsers.Visible = false;
            }

            this.Text = $"Информационная система - [{CurrentUser.Username}]";

            // Инициализация фильтра лицензий.
            cmbLicenseFilter.Items.Add("Все программы");
            cmbLicenseFilter.Items.Add("Бесплатные");
            cmbLicenseFilter.Items.Add("Платные");
            cmbLicenseFilter.SelectedIndex = 0;

            LoadCategories();
            ApplyFilters(); // Первичная загрузка данных.
        }

        // Загружает список категорий из БД.
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
                        // Добавление пункта "Все категории" для фильтрации.
                        categoriesComboBox.Items.Add(new ComboBoxItem { Id = -1, Name = "Все категории" });

                        while (reader.Read())
                        {
                            categoriesComboBox.Items.Add(new ComboBoxItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                        categoriesComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке категорий: {ex.Message}");
            }
        }

        // Загружает список подборок, принадлежащих текущему пользователю.
        private void LoadCollections()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT collection_id, collection_name FROM Collections WHERE user_id = @userId ORDER BY collection_name;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            collectionsComboBox.Items.Clear();
                            while (reader.Read())
                            {
                                collectionsComboBox.Items.Add(new ComboBoxItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                            }
                            if (collectionsComboBox.Items.Count > 0)
                            {
                                collectionsComboBox.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке подборок: {ex.Message}");
            }
        }

        // Центральный метод, применяющий все активные фильтры и обновляющий таблицу.
        private void ApplyFilters()
        {
            string licenseFilterSql = "";
            if (cmbLicenseFilter.SelectedIndex == 1) // Бесплатные
            {
                licenseFilterSql = " AND s.is_free = TRUE";
            }
            else if (cmbLicenseFilter.SelectedIndex == 2) // Платные
            {
                licenseFilterSql = " AND s.is_free = FALSE";
            }

            if (radioViewByCategory.Checked)
            {
                var selectedCategory = categoriesComboBox.SelectedItem as ComboBoxItem;
                if (selectedCategory != null)
                {
                    LoadSoftwareForCategory(selectedCategory.Id, licenseFilterSql);
                }
            }
            else if (radioViewByCollection.Checked)
            {
                var selectedCollection = collectionsComboBox.SelectedItem as ComboBoxItem;
                if (selectedCollection != null)
                {
                    LoadSoftwareForCollection(selectedCollection.Id, licenseFilterSql);
                }
                else
                {
                    // Очистка таблицы, если у пользователя нет подборок.
                    if (softwareDataGridView.DataSource != null) ((DataTable)softwareDataGridView.DataSource).Clear();
                }
            }
        }

        // Загружает программы для указанной категории с учетом фильтра лицензий.
        private void LoadSoftwareForCategory(int categoryId, string licenseFilter)
        {
            var whereClause = new StringBuilder("WHERE 1=1");
            if (categoryId != -1)
            {
                whereClause.Append(" AND s.category_id = @id");
            }
            whereClause.Append(licenseFilter);

            string sql = $@"SELECT s.software_id, s.name AS ""Название"", d.developer_name AS ""Разработчик"", s.website AS ""Сайт"", s.description AS ""Описание""
                           FROM Software s
                           JOIN Developers d ON s.developer_id = d.developer_id
                           {whereClause}
                           ORDER BY s.name;";

            ExecuteSoftwareQuery(sql, categoryId);
        }

        // Загружает программы для указанной подборки с учетом фильтра лицензий.
        private void LoadSoftwareForCollection(int collectionId, string licenseFilter)
        {
            string sql = $@"SELECT s.software_id, s.name AS ""Название"", d.developer_name AS ""Разработчик"", s.website AS ""Сайт"", s.description AS ""Описание""
                           FROM Software s
                           JOIN Developers d ON s.developer_id = d.developer_id
                           JOIN Software_Collections sc ON s.software_id = sc.software_id
                           WHERE sc.collection_id = @id {licenseFilter}
                           ORDER BY s.name;";

            ExecuteSoftwareQuery(sql, collectionId);
        }

        // Универсальный метод для выполнения SQL-запроса и заполнения DataGridView.
        private void ExecuteSoftwareQuery(string sql, int id)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        // Привязка параметра @id, если он используется в запросе (не равен -1).
                        if (id != -1)
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                        }

                        var dataAdapter = new NpgsqlDataAdapter(cmd);
                        var dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        softwareDataGridView.DataSource = dataTable;

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

        // Обработчик смены режима просмотра (Категория/Подборка).
        private void RadioView_CheckedChanged(object sender, EventArgs e)
        {
            categoriesComboBox.Enabled = radioViewByCategory.Checked;
            collectionsComboBox.Enabled = radioViewByCollection.Checked;

            if (radioViewByCollection.Checked)
            {
                LoadCollections();
            }
            ApplyFilters();
        }

        // Общий обработчик для всех фильтрующих ComboBox.
        private void Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // Открывает форму управления подборками.
        private void BtnManageCollections_Click(object sender, EventArgs e)
        {
            using (var managerForm = new CollectionsManagerForm())
            {
                managerForm.ShowDialog(this);
                // Обновление списка подборок после закрытия формы менеджера.
                if (radioViewByCollection.Checked)
                {
                    LoadCollections();
                }
            }
        }

        // Динамическое формирование контекстного меню перед его открытием.
        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            addToCollectionMenuItem.DropDownItems.Clear();
            if (softwareDataGridView.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT collection_id, collection_name FROM Collections WHERE user_id = @userId ORDER BY collection_name;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ToolStripMenuItem(reader.GetString(1));
                                item.Tag = reader.GetInt32(0); // Сохранение ID подборки в Tag.
                                item.Click += AddToCollection_Click;
                                addToCollectionMenuItem.DropDownItems.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка загрузки подборок для меню: {ex.Message}"); }
        }

        // Добавляет выбранные программы в указанную подборку.
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
                        // ON CONFLICT DO NOTHING предотвращает ошибку при дублировании записи.
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

        // Открывает форму детального просмотра по двойному клику.
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

        // Открывает форму добавления новой программы.
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var detailsForm = new SoftwareDetailsForm())
            {
                if (detailsForm.ShowDialog(this) == DialogResult.OK)
                {
                    ApplyFilters(); // Обновление данных после добавления.
                }
            }
        }

        // Открывает форму редактирования выбранной программы.
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (softwareDataGridView.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(softwareDataGridView.SelectedRows[0].Cells["software_id"].Value);
            using (var detailsForm = new SoftwareDetailsForm(id))
            {
                if (detailsForm.ShowDialog(this) == DialogResult.OK)
                {
                    ApplyFilters(); // Обновление данных после редактирования.
                }
            }
        }

        // Удаляет выбранную программу после подтверждения.
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
                    ApplyFilters(); // Обновление данных после удаления.
                }
                catch (Exception ex) { MessageBox.Show($"Ошибка удаления: {ex.Message}"); }
            }
        }

        // Открывает форму сравнения программ.
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

        // Открывает форму отчетов.
        private void BtnReports_Click(object sender, EventArgs e)
        {
            using (var reportsForm = new ReportsForm())
            {
                reportsForm.ShowDialog(this);
            }
        }

        // Открывает форму управления пользователями.
        private void BtnManageUsers_Click(object sender, EventArgs e)
        {
            using (var userManagerForm = new UserManagerForm())
            {
                userManagerForm.ShowDialog(this);
            }
        }

        // Устанавливает флаг смены пользователя и закрывает форму.
        private void BtnChangeUser_Click(object sender, EventArgs e)
        {
            this.IsUserChanged = true;
            this.Close();
        }
    }
}