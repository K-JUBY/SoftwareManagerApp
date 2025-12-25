using Microsoft.VisualBasic;
using Npgsql;
using System.Data;

namespace SoftwareManagerApp
{
    public partial class CollectionsManagerForm : Form
    {
        private readonly string connectionString = DbConnectionManager.ConnectionString;

        public CollectionsManagerForm()
        {
            InitializeComponent();
        }

        private void CollectionsManagerForm_Load(object sender, EventArgs e)
        {
            LoadCollections();
        }

        // Загружает список подборок, принадлежащих текущему пользователю.
        private void LoadCollections()
        {
            listCollections.Items.Clear();
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
                                listCollections.Items.Add(new ComboBoxItem
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки подборок: {ex.Message}");
            }
        }

        // Создает новую подборку для текущего пользователя.
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string newName = Interaction.InputBox("Введите название новой подборки:", "Создание подборки");
            if (string.IsNullOrWhiteSpace(newName)) return;

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO Collections (collection_name, user_id) VALUES (@name, @userId);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", newName);
                        cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadCollections(); // Обновление списка.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания подборки: {ex.Message}");
            }
        }

        // Переименовывает выбранную подборку.
        private void BtnRename_Click(object sender, EventArgs e)
        {
            if (listCollections.SelectedItem == null)
            {
                MessageBox.Show("Выберите подборку для переименования.");
                return;
            }

            var selectedCollection = (ComboBoxItem)listCollections.SelectedItem;
            string newName = Interaction.InputBox("Введите новое название:", "Переименование", selectedCollection.Name);
            if (string.IsNullOrWhiteSpace(newName)) return;

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE Collections SET collection_name = @name WHERE collection_id = @id;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedCollection.Id);
                        cmd.Parameters.AddWithValue("@name", newName);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadCollections();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка переименования: {ex.Message}");
            }
        }

        // Удаляет выбранную подборку.
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listCollections.SelectedItem == null)
            {
                MessageBox.Show("Выберите подборку для удаления.");
                return;
            }

            var selectedCollection = (ComboBoxItem)listCollections.SelectedItem;
            var result = MessageBox.Show($"Вы уверены, что хотите удалить подборку '{selectedCollection.Name}'?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    // Связанные записи из Software_Collections удаляются каскадно.
                    string sql = "DELETE FROM Collections WHERE collection_id = @id;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedCollection.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadCollections();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }
    }
}