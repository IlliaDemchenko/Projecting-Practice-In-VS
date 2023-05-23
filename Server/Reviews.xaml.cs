using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Server
{
    /// <summary>
    /// Клас екранної форми, який реалізує функціонал таблиці відгуків з бази даних
    /// </summary>
    public partial class Reviews : Window
    {
        /// <summary>
        /// Конструктор, що ініціалізує віконний компонент,
        /// а також отримує таблиці з бази даних, аби реалізувати функціонал компонентів ComboBox
        /// </summary>
        public Reviews()
        {
            // Отримання таблиць з бази даних та додавання їх до контексту
            DataContext = new
            {
                TableProducts = StaticConstants.database.Products,
                TableUsers = StaticConstants.database.Users
            };
            InitializeComponent();
            // Встановлення даних, які були отримані з бази даних
            DataGridReviews.ItemsSource = StaticConstants.database.Reviews.GetNewBindingList();
        }

        /// <summary>
        /// Обробник подій, який виконується після закриття вікна користувачем
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {
            StaticConstants.TrySubmitChanges();
        }

        // Реалізація интерфейсу INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Метод, який оновлює у базі даних дату при зміні значення у компоненті DatePicker
        /// </summary>
        /// <param name="propertyName">Нове значення типу рядок</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Обробник подій, який викликається тоді, коли користувач змінує значення у компоненті ComboBox,
        /// який відповідає за товар
        /// </summary>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridReviews.SelectedItem != null)
            {
                try
                {
                    // Отримати поточний компонент ComboBox
                    ComboBox comboBox = (ComboBox)sender;
                    // Отримати поточне замовлення
                    ClassLibrary.Reviews review = (ClassLibrary.Reviews)DataGridReviews.SelectedItem;
                    // Замінити ID товару
                    review.product_id = int.Parse(comboBox.SelectedValue.ToString());
                }
                catch { }
            }
        }

        /// <summary>
        /// Обробник подій, який викликається тоді, коли користувач змінує значення у компоненті ComboBox,
        /// який відповідає за користувача
        /// </summary>
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridReviews.SelectedItem != null)
            {
                try
                {
                    // Отримати поточний компонент ComboBox
                    ComboBox comboBox = (ComboBox)sender;
                    // Отримати поточне замовлення
                    ClassLibrary.Reviews review = (ClassLibrary.Reviews)DataGridReviews.SelectedItem;
                    // Замінити ID користувача
                    review.user_id = int.Parse(comboBox.SelectedValue.ToString());
                }
                catch { }
            }
        }
    }
}