namespace SoftwareManagerApp
{
    // Вспомогательный класс для хранения элементов в ComboBox.
    // Позволяет хранить как видимое имя, так и скрытый ID.
    public class ComboBoxItem
    {
        // Уникальный идентификатор из базы данных.
        public int Id { get; set; }
        // Текстовое представление, видимое пользователю.
        public string Name { get; set; }

        // Переопределение метода ToString для корректного отображения имени в ComboBox.
        public override string ToString()
        {
            return Name;
        }
    }
}