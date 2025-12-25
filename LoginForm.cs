using Npgsql;

namespace SoftwareManagerApp
{
    public partial class LoginForm : Form
    {
        private readonly string connectionString = DbConnectionManager.ConnectionString;

        public LoginForm()
        {
            InitializeComponent();
        }

        // Обработчик нажатия на кнопку "Войти".
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    // Поиск пользователя по имени.
                    string sql = "SELECT user_id, password_hash, role, username FROM Users WHERE username = @user;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", txtUsername.Text);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Пользователь с таким именем существует.
                            {
                                string hashedPasswordFromDb = reader.GetString(1);
                                string actualUsername = reader.GetString(3);

                                // Проверка соответствия введенного пароля хешу из БД.
                                if (PasswordHasher.VerifyPassword(txtPassword.Text, hashedPasswordFromDb))
                                {
                                    // Успешная авторизация: сохранение данных пользователя.
                                    CurrentUser.UserId = reader.GetInt32(0);
                                    CurrentUser.Role = reader.GetString(2);
                                    CurrentUser.Username = actualUsername;

                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}