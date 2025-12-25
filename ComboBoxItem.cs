namespace SoftwareManagerApp
{
    // Вспомогательный класс для хранения элементов в ComboBox.
    // Позволяет хранить как видимое имя, так и связанный с ним скрытый ID.
    public class ComboBoxItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Переопределенный метод для корректного отображения свойства Name в списке ComboBox.
        public override string ToString()
        {
            return Name;
        }
    }
}