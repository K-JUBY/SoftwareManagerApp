namespace SoftwareManagerApp
{
    // Статический класс, предоставляющий методы для хеширования и проверки паролей.
    public static class PasswordHasher
    {
        // Создает хеш пароля с использованием алгоритма BCrypt.
        // Соль генерируется автоматически и включается в состав результирующего хеша.
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Сравнивает пароль в открытом виде с существующим хешем.
        // Метод автоматически извлекает соль из хеша для корректной проверки.
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}