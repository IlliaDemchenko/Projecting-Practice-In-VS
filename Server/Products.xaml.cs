using System;
using System.Windows;

namespace Server
{
    /// <summary>
    /// Клас екранної форми, який реалізує функціонал таблиці товарів з бази даних
    /// </summary>
    public partial class Products : Window
    {
        /// <summary>
        /// Конструктор, що ініціалізує віконний компонент
        /// </summary>
        public Products()
        {
            InitializeComponent();
            // Встановлення даних, які необхідно отримати з бази даних
            DataGridProducts.ItemsSource = StaticConstants.database.Products.GetNewBindingList();
        }

        /// <summary>
        /// Обробник подій, який виконується після закриття вікна користувачем
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {
            // Спробувати зберігти дані. Якщо помилка - повідомити користувача
            StaticConstants.TrySubmitChanges();
        }
    }
}
