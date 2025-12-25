namespace SoftwareManagerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Основной цикл приложения, обеспечивающий возможность смены пользователя.
            while (true)
            {
                // Отображение формы входа как модального диалога.
                using (var loginForm = new LoginForm())
                {
                    // Если форма входа была закрыта без успешной авторизации, прерываем цикл.
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                }

                // После успешной авторизации создается и запускается главная форма.
                using (var mainForm = new Form1())
                {
                    Application.Run(mainForm);

                    // Проверка флага после закрытия главной формы.
                    // Если пользователь не нажал "Выйти", значит, он закрыл приложение.
                    if (!mainForm.IsUserChanged)
                    {
                        break;
                    }
                }
                // Если флаг IsUserChanged установлен, цикл продолжается, и снова открывается форма входа.
            }
        }
    }
}