using System.IO;

namespace SoftwareManagerApp
{
    // Статический класс для централизованного чтения строки подключения из конфигурационного файла.
    public static class DbConnectionManager
    {
        // Приватное статическое поле для кеширования строки подключения после первого чтения.
        private static string _connectionString;

        // Публичное свойство, обеспечивающее доступ к строке подключения.
        public static string ConnectionString
        {
            get
            {
                // Ленивая загрузка: чтение из файла происходит только при первом обращении.
                if (string.IsNullOrEmpty(_connectionString))
                {
                    try
                    {
                        // Чтение содержимого файла db_connection.txt, находящегося в директории приложения.
                        _connectionString = File.ReadAllText("db_connection.txt").Trim();
                    }
                    catch (Exception ex)
                    {
                        // Обработка критической ошибки (файл не найден или недоступен).
                        MessageBox.Show(
                            $"Критическая ошибка: не удалось прочитать файл подключения 'db_connection.txt'.\n\nУбедитесь, что файл находится в папке с программой.\n\nОшибка: {ex.Message}",
                            "Ошибка подключения",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        // Завершение работы приложения, так как дальнейшая работа без БД невозможна.
                        Application.Exit();
                    }
                }
                return _connectionString;
            }
        }
    }
}