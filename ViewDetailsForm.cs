using Microsoft.VisualBasic;
using Npgsql;
using System.Data;
using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace SoftwareManagerApp
{
    public partial class ViewDetailsForm : Form
    {
        private readonly string connectionString = DbConnectionManager.ConnectionString;
        private readonly int softwareIdToView;

        public ViewDetailsForm(int softwareId)
        {
            InitializeComponent();
            this.softwareIdToView = softwareId;
        }

        private void ViewDetailsForm_Load(object sender, EventArgs e)
        {
            LoadSoftwareDetails();
            LoadScreenshot();
            LoadReviews();
        }

        // Загрузка основной информации о программе.
        private void LoadSoftwareDetails()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT 
                                     s.*, c.category_name, d.developer_name
                                   FROM Software s
                                   LEFT JOIN Categories c ON s.category_id = c.category_id
                                   LEFT JOIN Developers d ON s.developer_id = d.developer_id
                                   WHERE s.software_id = @id;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", softwareIdToView);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblProgramName.Text = reader["name"].ToString();
                                lblCategoryName.Text = reader["category_name"].ToString();
                                lblDeveloperName.Text = reader["developer_name"].ToString();
                                lblSize.Text = reader["size_mb"].ToString();
                                lblWebsite.Text = reader["website"].ToString();
                                lblIsFree.Text = Convert.ToBoolean(reader["is_free"]) ? "Бесплатная" : "Платная";
                                txtDescription.Text = reader["description"].ToString();
                                txtSystemReq.Text = reader["system_requirements"].ToString();
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

        // Загрузка скриншота.
        private void LoadScreenshot()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT image_data FROM Screenshots WHERE software_id = @id LIMIT 1;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", softwareIdToView);
                        var result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            byte[] imageData = (byte[])result;

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
                                picScreenshot.Image = null;
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

        // Загрузка отзывов к программе.
        private void LoadReviews()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    string sql = @"SELECT author AS ""Автор"", rating AS ""Оценка"", review_text AS ""Текст отзыва"", created_at AS ""Дата"" 
                                   FROM Reviews 
                                   WHERE software_id = @id 
                                   ORDER BY created_at DESC;";
                    using (var adapter = new NpgsqlDataAdapter(sql, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@id", softwareIdToView);
                        DataTable reviewsTable = new DataTable();
                        adapter.Fill(reviewsTable);
                        reviewsDataGridView.DataSource = reviewsTable;

                        if (reviewsDataGridView.Columns.Count > 0)
                        {
                            reviewsDataGridView.Columns["Автор"].FillWeight = 20;
                            reviewsDataGridView.Columns["Оценка"].FillWeight = 10;
                            reviewsDataGridView.Columns["Текст отзыва"].FillWeight = 50;
                            reviewsDataGridView.Columns["Дата"].FillWeight = 20;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отзывов: {ex.Message}");
            }
        }

        // Добавление нового отзыва.
        private void BtnAddReview_Click(object sender, EventArgs e)
        {
            string reviewText = Interaction.InputBox("Введите ваш отзыв:", "Добавление отзыва");
            if (string.IsNullOrWhiteSpace(reviewText)) return;

            string ratingText = Interaction.InputBox("Оцените программу от 1 до 5:", "Оценка");
            if (!int.TryParse(ratingText, out int rating) || rating < 1 || rating > 5)
            {
                MessageBox.Show("Оценка должна быть числом от 1 до 5.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO Reviews (software_id, author, review_text, rating) VALUES (@sw_id, @author, @text, @rating);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@sw_id", softwareIdToView);
                        cmd.Parameters.AddWithValue("@author", CurrentUser.Username);
                        cmd.Parameters.AddWithValue("@text", reviewText);
                        cmd.Parameters.AddWithValue("@rating", rating);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Ваш отзыв успешно добавлен!", "Спасибо!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadReviews();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении отзыва: {ex.Message}");
            }
        }
    }
}