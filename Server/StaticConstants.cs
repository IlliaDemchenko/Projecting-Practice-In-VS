using System;
using ClassLibrary;
using System.Windows;

namespace Server
{
    /// <summary>
    /// Клас для спільного доступу з будь-яких вікон
    /// </summary>
    public static class StaticConstants
    {
        // Об'єкт бази даних
        public static InternetShop database { get; set; }

        /// <summary>
        /// Метод, який створює підключення до бази даних
        /// </summary>
        /// <param name="connection">Посилання для підключення до бази даних</param>
        public static void CreateConnection(string connection)
        {
            database = new InternetShop(connection);
            if (!database.DatabaseExists())
                database.CreateDatabase();
        }

        /// <summary>
        /// Метод, який видаляє базу даних
        /// </summary>
        /// <returns>true, якщо видалення успішне, інакше - false</returns>
        public static bool DropDatabase()
        {
            try
            {
                database.DeleteDatabase();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Метод, який дозволяє зберігти дані у базі даних, а якщо виникла помилка - повідомити про це користувача
        /// Цей метод може використовуватись будь-де у програмі
        /// </summary>
        public static void TrySubmitChanges()
        {
            try
            {
                database.SubmitChanges();
            }
            catch (Exception error)
            {
                // Необхідно оновити з'єднання, аби одна й та сама помилка не виникала кожного разу
                // навіть тоді, коли проблема вже виправлена
                database = new InternetShop(Server.Properties.Settings.Default.db_connection);
                // Відобразити повідомлення про помилку користувачеві
                MessageBox.Show("Не вдалося зберігти зміни у базі даних. Можливо, порушена цілісність даних, або є порожні поля. Спробуйте ще раз.\n\nПовний код помилки:\n\n" + error.Message, "Помилка збереження змін", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
