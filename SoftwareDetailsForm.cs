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
        private readonly string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=postgres;Database=software_analogues;";

        // ID редактируемой программы. null, если режим добавления.
        private readonly int? softwareIdToEdit;

        // Массив байт текущего изображения для сохранения в БД.
        private byte[]? currentImageData = null;
        // Флаг для отметки изображения на удаление при сохранении.
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

        // Инициализация формы: загрузка справочников и данных (если редактирование).
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

        #region Работа с изображением

        // Загрузка скриншота из БД.
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

                            // Декодирование изображения с помощью ImageSharp для надежности.
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
                            catch
                            {
                                picScreenshot.Image = null; // В случае ошибки декодирования.
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке скриншота: {ex.Message}");
            }
        }

        // Открытие диалога выбора файла и загрузка изображения.
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

                        // Загрузка через ImageSharp и конвертация в System.Drawing.Bitmap для отображения.
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

                        this.currentImageData = fileBytes; // Сохраняем исходные байты для записи в БД.
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

        // Очистка PictureBox и установка флага на удаление.
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

        #endregion

        #region Сохранение и загрузка данных

        // Обработчик кнопки "Сохранить".
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            // Использование транзакции для атомарности операций с основной таблицей и скриншотами.
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int currentSoftwareId;
                        NpgsqlCommand cmd;

                        // Режим добавления (INSERT).
                        if (softwareIdToEdit == null)
                        {
                            // RETURNING software_id позволяет получить ID новой записи.
                            string sqlInsert = @"INSERT INTO Software (name, description, system_requirements, size_mb, website, category_id, developer_id) 
                                               VALUES (@name, @description, @system_req, @size, @website, @cat_id, @dev_id)
                                               RETURNING software_id;";
                            cmd = new NpgsqlCommand(sqlInsert, conn, transaction);
                        }
                        // Режим редактирования (UPDATE).
                        else
                        {
                            string sqlUpdate = @"UPDATE Software SET name = @name, description = @description, system_requirements = @system_req,
                                                   size_mb = @size, website = @website, category_id = @cat_id, developer_id = @dev_id
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
                        cmd.Parameters.AddWithValue("@cat_id", ((ComboBoxItem)cmbCategory.SelectedItem).Id);
                        cmd.Parameters.AddWithValue("@dev_id", ((ComboBoxItem)cmbDeveloper.SelectedItem).Id);

                        if (softwareIdToEdit == null)
                        {
                            currentSoftwareId = (int)cmd.ExecuteScalar();
                        }
                        else
                        {
                            cmd.ExecuteNonQuery();
                            currentSoftwareId = softwareIdToEdit.Value;
                        }

                        // Сохранение или удаление скриншота в рамках той же транзакции.
                        SaveScreenshot(conn, transaction, currentSoftwareId);

                        transaction.Commit(); // Фиксация транзакции.

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Откат транзакции в случае ошибки.
                        MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
                    }
                }
            }
        }

        // Логика сохранения/обновления/удаления скриншота в БД.
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
                // Проверка на существование записи для выбора между INSERT и UPDATE.
                string sqlCheck = "SELECT COUNT(*) FROM Screenshots WHERE software_id = @id;";
                long count;
                using (var cmd = new NpgsqlCommand(sqlCheck, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", softwareId);
                    count = (long)cmd.ExecuteScalar();
                }

                if (count > 0)
                {
                    string sqlUpdate = "UPDATE Screenshots SET image_data = @data WHERE software_id = @id;";
                    using (var cmd = new NpgsqlCommand(sqlUpdate, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", softwareId);
                        cmd.Parameters.AddWithValue("@data", currentImageData);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
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

        // Загрузка списка категорий в ComboBox.
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
                        while (reader.Read())
                        {
                            cmbCategory.Items.Add(new ComboBoxItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}"); }
        }

        // Загрузка списка разработчиков в ComboBox.
        private void LoadDevelopers()
        {
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
                            cmbDeveloper.Items.Add(new ComboBoxItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
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
                    string sql = "SELECT * FROM Software WHERE software_id = @id;";
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
                                int categoryId = Convert.ToInt32(reader["category_id"]);
                                cmbCategory.SelectedItem = cmbCategory.Items.OfType<ComboBoxItem>().FirstOrDefault(item => item.Id == categoryId);
                                int developerId = Convert.ToInt32(reader["developer_id"]);
                                cmbDeveloper.SelectedItem = cmbDeveloper.Items.OfType<ComboBoxItem>().FirstOrDefault(item => item.Id == developerId);
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
            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать категорию.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbDeveloper.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать разработчика.", "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        #endregion
    }
}