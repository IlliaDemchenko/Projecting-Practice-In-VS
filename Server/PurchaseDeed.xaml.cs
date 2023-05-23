using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Server
{
    /// <summary>
    /// Клас екранної форми для взаємодії з документом "Купівельний акт"
    /// </summary>
    public partial class PurchaseDeed : Window
    {
        /// <summary>
        /// Конструктор, який встановлює у компонент DataGrid дані з бази даних
        /// </summary>
        public PurchaseDeed()
        {
            InitializeComponent();
            DataGridOrders.ItemsSource = StaticConstants.database.Orders.GetNewBindingList();
        }

        /// <summary>
        /// Обробник подій при обранні запису з бази даних
        /// </summary>
        private void DataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Якщо елемент обрано й він не порожній
            if (DataGridOrders.SelectedItem != null)
            {
                try
                {
                    // Поточне замовлення
                    ClassLibrary.Orders CurrentOrder = (ClassLibrary.Orders)DataGridOrders.SelectedItem;
                    // Отримання користувача
                    var query = from tempuser in StaticConstants.database.Users
                                where tempuser.id == CurrentOrder.user_id
                                select tempuser;
                    ClassLibrary.Users user = query.FirstOrDefault();
                    // Отримання товару
                    var query2 = from tempproduct in StaticConstants.database.Products
                                where tempproduct.id == CurrentOrder.product_id
                                select tempproduct;
                    ClassLibrary.Products product = query2.FirstOrDefault();

                    // Зміна контексту даних
                    DataContext = new
                    {
                        id = CurrentOrder.id.ToString(),
                        date = CurrentOrder.date.ToString().Split(' ')[0],
                        buyer = user.surname + " " + user.name + " " + user.patronymic,
                        product.name,
                        CurrentOrder.product_count,
                        product.cost,
                        sum = (product.cost * CurrentOrder.product_count)
                    };
                }
                catch
                {
                    // Якщо виникла помилка - повідомити користувача
                    MessageBox.Show("Не вдалося відновити акт. Спробуйте ще раз.", "Виникла помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Обробник події при натисканні на пункт меню "Роздрукування"
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Викликати метод Print (друку) у компоненті, що представляє собою документ
            FlowDocumentPurchaseDeed.Print();
        }
    }
}
