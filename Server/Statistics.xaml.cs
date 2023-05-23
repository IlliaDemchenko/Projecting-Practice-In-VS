using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using AxisPosition = OxyPlot.Axes.AxisPosition;

namespace Server
{
    /// <summary>
    /// Клас екранної форми для побудови та збереження графіків
    /// </summary>
    public partial class Statistics : Window
    {
        /// <summary>
        /// Конструктор, який ініціалізує вікно та будує графік
        /// </summary>
        public Statistics()
        {
            InitializeComponent();
            UpdateChart();
        }

        /// <summary>
        /// Метод для оновлення графіку
        /// </summary>
        private void UpdateChart()
        {
            // Якщо компоненти не нульові, були попередньо вже встановлені
            if (StartDate != null && EndDate != null && plotView != null)
            {
                // Створення моделі графіку
                var model = new PlotModel { Title = "Кількість замовлень за обраний час" };

                // Створення осей
                var xAxis = new CategoryAxis { Position = AxisPosition.Bottom, Title = "Дати" };
                var yAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Кількість" };

                // Додавання осей у модель графіка
                model.Axes.Add(xAxis);
                model.Axes.Add(yAxis);

                // Створення серії даних
                var series = new LineSeries();

                // Запит до бази даних для групування даних за датою
                var query = from item in StaticConstants.database.Orders
                            where item.date.Date >= StartDate.SelectedDate && item.date.Date <= EndDate.SelectedDate
                            group item by item.date into g
                            select new
                            {
                                Date = g.Key,
                                Count = g.Count()
                            };

                // Заповнення списків даних для побудови графіку
                int i = 0;
                foreach (var q in query)
                {
                    series.Points.Add(new DataPoint(i++, q.Count));
                    xAxis.Labels.Add(q.Date.ToString().Split(' ')[0]);
                }

                // Додавання серії даних у модель графіка
                model.Series.Add(series);

                // Встановлення моделі графіка для елемента PlotView
                plotView.Model = model;
            }
        }

        /// <summary>
        /// Обробник подій при зміні дати
        /// </summary>
        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        /// <summary>
        /// Обробник подій при натисканні на кнопку збереження графіку
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Створення вікна діалогу
            PrintDialog printDialog = new PrintDialog();

            // Його демонстрація та перевірка на те, чи натискув користувач кнопку збереження
            if (printDialog.ShowDialog() == true)
            {
                // Створюємо скріншот PlotView
                var bitmap = new RenderTargetBitmap((int)plotView.ActualWidth, (int)plotView.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                bitmap.Render(plotView);

                // Створюємо Image з використанням скріншота
                var image = new Image { Source = bitmap };

                // Друкуємо Image
                printDialog.PrintVisual(image, "Роздруковуємо PlotView");
            }
        }
    }
}
