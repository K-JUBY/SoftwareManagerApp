using Npgsql;
using System.Data;
using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace SoftwareManagerApp
{
    public partial class SoftwareDetailsForm : Form
    {
        private readonly string connectionString = DbConnectionManager.ConnectionString;
        private readonly int? softwareIdToEdit;
        private byte[]? currentImageData = null;
        private bool imageMarkedForDeletion = false;

        // Конструктор для режима добавления.
        public SoftwareDetailsForm()
        {
            InitializeComponent();
            this.softwareIdToEdit = null;
            this.Text = "Добавление новой программы";
        }

        // Конструктор для режима редактирования.
        public SoftwareDetailsForm(int softwareId)
        {
            InitializeComponent();
            this.softwareIdToEdit = softwareId;
            this.Text = "Редактирование программы";
        }

        private void SoftwareDetailsForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadDevelopers();

            if (softwareIdToEdit.HasValue)
            {
                LoadSoftwareData(softwareIdToEdit.Value);
                LoadScreenshot(softwareIdToEdit.Value);
            }
        }

        // Обработчик кнопки "Сохранить", основная бизнес-логика формы.
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            // Использование транзакции для обеспечения целостности данных.
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Получение ID для категории/разработчика. Если их нет - они создаются.
                        int categoryId = GetOrCreateId("Categories", "category_name", "category_id", cmbCategory.Text, conn, transaction);
                        int developerId = GetOrCreateId("Developers", "developer_name", "developer_id", cmbDeveloper.Text, conn, transaction);

                        int currentSoftwareId;
                        NpgsqlCommand cmd;

                        if (softwareIdToEdit == null) // Режим добавления.
                        {
                            string sqlInsert = @"INSERT INTO Software (name, description, system_requirements, size_mb, website, category_id, developer_id, is_free) 
                                               VALUES (@name, @description, @system_req, @size, @website, @cat_id, @dev_id, @is_free)
                                               RETURNING software_id;";
                            cmd = new NpgsqlCommand(sqlInsert, conn, transaction);
                        }
                        else // Режим редактирования.
                        {
                            string sqlUpdate = @"UPDATE Software SET name = @name, description = @description, system_requirements = @system_req,
                                                   size_mb = @size, website = @website, category_id = @cat_id, developer_id = @dev_id, is_free = @is_free
                                               WHERE software_id = @id;";
                            cmd = new NpgsqlCommand(sqlUpdate, conn, transaction);
                            cmd.Parameters.AddWithValue("@id", softwareIdToEdit.Value);
                        }

                        // Привязка параметров.
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@system_req", txtSystemReq.Text);
                        cmd.Parameters.AddWithValue("@size", numSize.Value);
                        cmd.Parameters.AddWithValue("@website", txtWebsite.Text);
                        cmd.Parameters.AddWithValue("@cat_id", categoryId);
                        cmd.Parameters.AddWithValue("@dev_id", developerId);
                        cmd.Parameters.AddWithValue("@is_free", chkIsFree.Checked);

                        if (softwareIdToEdit == null)
                        {
                            currentSoftwareId = (int)cmd.ExecuteScalar(); // Получение ID новой записи.
                        }
                        else
                        {
                            cmd.ExecuteNonQuery();
                            currentSoftwareId = softwareIdToEdit.Value;
                        }

                        // Обновление скриншота в рамках той же транзакции.
                        SaveScreenshot(conn, transaction, currentSoftwareId);

                        transaction.Commit();

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
                    }
                }
            }
        }

        // Универсальный метод для получения ID из справочника. Если запись не найдена, создает новую.
        private int GetOrCreateId(string tableName, string nameColumn, string idColumn, string nameValue, NpgsqlConnection conn, NpgsqlTransaction transaction)
        {
            // Попытка найти существующую запись.
            string sqlFind = $"SELECT {idColumn} FROM {tableName} WHERE {nameColumn} ILIKE @name LIMIT 1;";
            using (var cmdFind = new NpgsqlCommand(sqlFind, conn, transaction))
            {
                cmdFind.Parameters.AddWithValue("@name", nameValue);
                var result = cmdFind.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
            }

            // Создание новой записи, если она не была найдена.
            string sqlInsert = $"INSERT INTO {tableName} ({nameColumn}) VALUES (@name) RETURNING {idColumn};";
            using (var cmdInsert = new NpgsqlCommand(sqlInsert, conn, transaction))
            {
                cmdInsert.Parameters.AddWithValue("@name", nameValue);
                return (int)cmdInsert.ExecuteScalar();
            }
        }

        // Логика сохранения/обновления/удаления скриншота.
        private void SaveScreenshot(NpgsqlConnection conn, NpgsqlTransaction transaction, int softwareId)
        {
            if (imageMarkedForDeletion)
            {
                string sqlDelete = "DELETE FROM Screenshots WHERE software_id = @id;";
                using (var cmd = new NpgsqlCommand(sqlDelete, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", softwareId);
                    cmd.ExecuteNonQuery();
                }
            }
            else if (currentImageData != null)
            {
                string sqlCheck = "SELECT COUNT(*) FROM Screenshots WHERE software_id = @id;";
                long count;
                using (var cmd = new NpgsqlCommand(sqlCheck, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", softwareId);
                    count = (long)cmd.ExecuteScalar();
                }

                if (count > 0) // Обновление существующего.
                {
                    string sqlUpdate = "UPDATE Screenshots SET image_data = @data WHERE software_id = @id;";
                    using (var cmd = new NpgsqlCommand(sqlUpdate, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", softwareId);
                        cmd.Parameters.AddWithValue("@data", currentImageData);
                        cmd.ExecuteNonQuery();
                    }
                }
                else // Вставка нового.
                {
                    string sqlInsert = "INSERT INTO Screenshots (software_id, image_data) VALUES (@id, @data);";
                    using (var cmd = new NpgsqlCommand(sqlInsert, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", softwareId);
                        cmd.Parameters.AddWithValue("@data", currentImageData);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Загрузка списка категорий.
        private void LoadCategories()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT category_name FROM Categories ORDER BY category_name;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCategory.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}"); }
        }

        // Загрузка списка разработчиков.
        private void LoadDevelopers()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT developer_name FROM Developers ORDER BY developer_name;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbDeveloper.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка загрузки разработчиков: {ex.Message}"); }
        }

        // Загрузка данных программы для режима редактирования.
        private void LoadSoftwareData(int softwareId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT s.*, c.category_name, d.developer_name FROM Software s LEFT JOIN Categories c ON s.category_id = c.category_id LEFT JOIN Developers d ON s.developer_id = d.developer_id WHERE software_id = @id;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", softwareId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["name"].ToString();
                                txtDescription.Text = reader["description"].ToString();
                                txtSystemReq.Text = reader["system_requirements"].ToString();
                                txtWebsite.Text = reader["website"].ToString();
                                numSize.Value = Convert.ToDecimal(reader["size_mb"]);
                                chkIsFree.Checked = Convert.ToBoolean(reader["is_free"]);
                                cmbCategory.Text = reader["category_name"].ToString();
                                cmbDeveloper.Text = reader["developer_name"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных программы: {ex.Message}");
                this.Close();
            }
        }

        // Проверка заполнения обязательных полей.
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Поле 'Название' не может быть пустым.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbCategory.Text))
            {
                MessageBox.Show("Необходимо выбрать или ввести категорию.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbDeveloper.Text))
            {
                MessageBox.Show("Необходимо выбрать или ввести разработчика.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Загрузка и отображение скриншота.
        private void LoadScreenshot(int softwareId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT image_data FROM Screenshots WHERE software_id = @id LIMIT 1;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", softwareId);
                        var result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            byte[] imageData = (byte[])result;
                            this.currentImageData = imageData;

                            try
                            {
                                using (var imageSharp = SixLabors.ImageSharp.Image.Load<Rgba32>(imageData))
                                using (var ms = new MemoryStream())
                                {
                                    imageSharp.SaveAsBmp(ms);
                                    ms.Position = 0;
                                    picScreenshot.Image?.Dispose();
                                    picScreenshot.Image = new System.Drawing.Bitmap(ms);
                                }
                            }
                            catch { picScreenshot.Image = null; }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке скриншота: {ex.Message}");
            }
        }

        // Открытие диалога выбора файла.
        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите скриншот";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);

                        using (Image<Rgba32> imageSharp = SixLabors.ImageSharp.Image.Load<Rgba32>(fileBytes))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                imageSharp.SaveAsBmp(memoryStream);
                                memoryStream.Position = 0;
                                picScreenshot.Image?.Dispose();
                                picScreenshot.Image = new System.Drawing.Bitmap(memoryStream);
                            }
                        }

                        this.currentImageData = fileBytes;
                        this.imageMarkedForDeletion = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось открыть файл изображения: {ex.Message}");
                        currentImageData = null;
                        picScreenshot.Image?.Dispose();
                        picScreenshot.Image = null;
                    }
                }
            }
        }

        // Удаление изображения из PictureBox.
        private void BtnDeleteImage_Click(object sender, EventArgs e)
        {
            if (picScreenshot.Image != null)
            {
                picScreenshot.Image.Dispose();
                picScreenshot.Image = null;
                currentImageData = null;
                imageMarkedForDeletion = true;
            }
        }
    }
}