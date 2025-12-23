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
        private readonly string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=postgres;Database=software_analogues;";
        private readonly int softwareIdToView;

        public ViewDetailsForm(int softwareId)
        {
            InitializeComponent();
            this.softwareIdToView = softwareId;
        }

        // Загрузка всей информации при открытии формы.
        private void ViewDetailsForm_Load(object sender, EventArgs e)
        {
            LoadSoftwareDetails();
            LoadScreenshot();
        }

        // Загрузка текстовых данных программы.
        private void LoadSoftwareDetails()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    // LEFT JOIN на случай, если у программы не указан разработчик или категория.
                    string sql = @"SELECT 
                                     s.name, s.description, s.system_requirements, s.size_mb, s.website,
                                     c.category_name, d.developer_name
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

        // Загрузка скриншота программы из БД.
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

                            // Декодирование с помощью ImageSharp для предотвращения ошибок GDI+.
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
    }
}