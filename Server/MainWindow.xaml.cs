using System.Windows;

namespace Server
{
    /// <summary>
    /// Клас екранної форми, який відповідає за головне вікно програми
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор, що ініціалізує віконний компонент
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод, до завершення якого вікно не відкриєтся.
        /// Корисно використовувати у випадках, коли необхідно щось підгрузити
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StaticConstants.CreateConnection(Properties.Settings.Default.db_connection);
        }

        /// <summary>
        /// Обробник подій для меню, який відкриває вікно з товарами Lb4
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new Products().Show();
        }

        /// <summary>
        /// Обробник подій для меню, який відкриває вікно з користувачами Lb4
        /// </summary>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            new Users().Show();
        }

        /// <summary>
        /// Обробник подій для меню, який відкриває вікно з замовленнями Lb4
        /// </summary>
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            new Orders().Show();
        }

        /// <summary>
        /// Обробник подій для меню, який відкриває вікно з відгуками Lb4
        /// </summary>
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            new Reviews().Show();
        }

        /// <summary>
        /// Обробник подій для меню, який відкриває вікно з актом купівлі Lb5
        /// </summary>
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            new PurchaseDeed().Show();
        }

        /// <summary>
        /// Обробник подій для меню, який відкриває вікно зі статистикою Lb6
        /// </summary>
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            new Statistics().Show();
        }
    }
}
