using System;
using System.Windows;

namespace Server
{
    /// <summary>
    /// Клас екранної форми, який реалізує функціонал таблиці користувачів з бази даних
    /// </summary>
    public partial class Users : Window
    {
        /// <summary>
        /// Конструктор, що ініціалізує віконний компонент
        /// </summary>
        public Users()
        {
            InitializeComponent();
            // Встановлення даних, які необхідно отримати з бази даних
            DataGridUsers.ItemsSource = StaticConstants.database.Users.GetNewBindingList();
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
