namespace SoftwareManagerApp
{
    // Статический класс для хранения глобально доступных данных о текущем авторизованном пользователе.
    public static class CurrentUser
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }
    }
}