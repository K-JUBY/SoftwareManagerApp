using Npgsql;
using System.Data;

namespace SoftwareManagerApp
{
    public partial class PasswordResetForm : Form
    {
        private readonly string connectionString = DbConnectionManager.ConnectionString;

        public PasswordResetForm()
        {
            InitializeComponent();
        }

        private void BtnRunUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var usersToUpdate = new List<(int id, string plainPassword)>();

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    // 1. Читаем всех пользователей с их паролями в открытом виде
                    string sqlSelect = "SELECT user_id, password_hash FROM Users;";
                    using (var cmdSelect = new NpgsqlCommand(sqlSelect, conn))
                    using (var reader = cmdSelect.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usersToUpdate.Add((reader.GetInt32(0), reader.GetString(1)));
                        }
                    }

                    // 2. Для каждого пользователя генерируем хеш и обновляем запись в БД
                    foreach (var user in usersToUpdate)
                    {
                        string hashedPassword = PasswordHasher.HashPassword(user.plainPassword);

                        string sqlUpdate = "UPDATE Users SET password_hash = @hash WHERE user_id = @id;";
                        using (var cmdUpdate = new NpgsqlCommand(sqlUpdate, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@id", user.id);
                            cmdUpdate.Parameters.AddWithValue("@hash", hashedPassword);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Хеши паролей успешно обновлены! Теперь закройте эту программу и верните файл Program.cs в исходное состояние.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnRunUpdate.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}