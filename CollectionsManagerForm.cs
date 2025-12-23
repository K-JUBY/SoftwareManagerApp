using Microsoft.VisualBasic; // Для Interaction.InputBox
using Npgsql;
using System.Data;

namespace SoftwareManagerApp
{
    public partial class CollectionsManagerForm : Form
    {
        private readonly string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=postgres;Database=software_analogues;";

        public CollectionsManagerForm()
        {
            InitializeComponent();
        }

        private void CollectionsManagerForm_Load(object sender, EventArgs e)
        {
            LoadCollections();
        }

        // Загрузка списка подборок из БД.
        private void LoadCollections()
        {
            listCollections.Items.Clear();
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
                            listCollections.Items.Add(new ComboBoxItem
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки подборок: {ex.Message}");
            }
        }

        // Создание новой подборки.
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Использование стандартного диалога для ввода текста.
            string newName = Interaction.InputBox("Введите название новой подборки:", "Создание подборки");
            if (string.IsNullOrWhiteSpace(newName)) return;

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO Collections (collection_name) VALUES (@name);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", newName);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadCollections();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания подборки: {ex.Message}");
            }
        }

        // Переименование существующей подборки.
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

        // Удаление подборки.
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
                    // Связанные записи из Software_Collections удаляются автоматически благодаря ON DELETE CASCADE в схеме БД.
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