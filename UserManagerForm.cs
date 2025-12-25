using Npgsql;
using System.Data;

namespace SoftwareManagerApp
{
    public partial class UserManagerForm : Form
    {
        private readonly string connectionString = DbConnectionManager.ConnectionString;

        public UserManagerForm()
        {
            InitializeComponent();
        }

        private void UserManagerForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        // Загружает список всех пользователей, кроме их хешей паролей.
        private void LoadUsers()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    string sql = @"SELECT user_id, username AS ""Имя пользователя"", role AS ""Роль"" 
                                   FROM Users ORDER BY username;";
                    using (var adapter = new NpgsqlDataAdapter(sql, conn))
                    {
                        DataTable usersTable = new DataTable();
                        adapter.Fill(usersTable);
                        usersDataGridView.DataSource = usersTable;

                        if (usersDataGridView.Columns["user_id"] != null)
                        {
                            usersDataGridView.Columns["user_id"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}");
            }
        }

        // Создает нового пользователя.
        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewUsername.Text) || string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                MessageBox.Show("Имя пользователя и пароль не могут быть пустыми.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    // Проверка на уникальность имени пользователя.
                    string sqlCheck = "SELECT COUNT(*) FROM Users WHERE username ILIKE @user;";
                    using (var cmdCheck = new NpgsqlCommand(sqlCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@user", txtNewUsername.Text);
                        if ((long)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string sqlInsert = "INSERT INTO Users (username, password_hash, role) VALUES (@user, @hash, @role);";
                    using (var cmdInsert = new NpgsqlCommand(sqlInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@user", txtNewUsername.Text);
                        // Пароль хешируется перед сохранением.
                        cmdInsert.Parameters.AddWithValue("@hash", PasswordHasher.HashPassword(txtNewPassword.Text));
                        cmdInsert.Parameters.AddWithValue("@role", radioRoleAdmin.Checked ? "Admin" : "User");
                        cmdInsert.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Новый пользователь успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
                txtNewUsername.Clear();
                txtNewPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}");
            }
        }

        // Удаляет выбранного пользователя.
        private void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(usersDataGridView.SelectedRows[0].Cells["user_id"].Value);
            string username = usersDataGridView.SelectedRows[0].Cells["Имя пользователя"].Value.ToString();
            string role = usersDataGridView.SelectedRows[0].Cells["Роль"].Value.ToString();

            // Защита от удаления своей учетной записи.
            if (userId == CurrentUser.UserId)
            {
                MessageBox.Show("Вы не можете удалить свою собственную учетную запись.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Защита от удаления последнего администратора.
            if (role == "Admin")
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        string sqlCheck = "SELECT COUNT(*) FROM Users WHERE role = 'Admin';";
                        using (var cmdCheck = new NpgsqlCommand(sqlCheck, conn))
                        {
                            if ((long)cmdCheck.ExecuteScalar() <= 1)
                            {
                                MessageBox.Show("Нельзя удалить последнего администратора в системе.", "Операция запрещена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при проверке количества администраторов: {ex.Message}");
                    return;
                }
            }

            if (MessageBox.Show($"Вы уверены, что хотите удалить пользователя '{username}'?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        string sql = "DELETE FROM Users WHERE user_id = @id;";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", userId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Пользователь удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                }
            }
        }
    }
}