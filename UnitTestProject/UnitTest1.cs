using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        // Текстовий рядок для зберігання посилання для підключення до тестової бази даних
        static string DBTest = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDBFilename=C:\\Users\\User\\Desktop\\Projecting-Practice-In-VS\\database_test.mdf";

        /// <summary>
        /// Тестовий метод, який перевіряє підключення до бази даних
        /// </summary>
        [TestMethod]
        public void TestConnectionToDB()
        {
            StaticConstants.CreateConnection(DBTest);
            Assert.IsNotNull(StaticConstants.database);
        }

        /// <summary>
        /// Тестовий метод, який тестує кастомне збереження даних з класу StaticConstants
        /// </summary>
        [TestMethod]
        public void TestTrySubmitChanges()
        {
            StaticConstants.CreateConnection(DBTest);

            ClassLibrary.Users NewUser = new ClassLibrary.Users
            {
                id = 1,
                name = "name",
                surname = "surname",
                patronymic = "patronymic",
                email = "email",
                password = "password",
                address = "address",
                telephone = "telephone"
            };

            Assert.AreEqual(StaticConstants.database.Users.Count(), 0);
            StaticConstants.database.Users.InsertOnSubmit(NewUser);
            StaticConstants.TrySubmitChanges();
            Assert.AreEqual(StaticConstants.database.Users.Count(), 1);
            StaticConstants.database.Users.DeleteOnSubmit(NewUser);
            StaticConstants.TrySubmitChanges();
            Assert.AreEqual(StaticConstants.database.Users.Count(), 0);
        }

        /// <summary>
        /// Тестовий метод, який тестує таблицю з товарами
        /// </summary>
        [TestMethod]
        public void TestTableProducts()
        {
            StaticConstants.CreateConnection(DBTest);

            ClassLibrary.Products NewProduct = new ClassLibrary.Products
            {
                id = 1,
                name = "Ігровий ноутбук",
                description = "Ігровий ноутбук",
                cost = 30000,
                count = 2
            };

            StaticConstants.database.Products.InsertOnSubmit(NewProduct);
            StaticConstants.TrySubmitChanges();
            Server.Products window = new Server.Products();

            Assert.AreEqual(StaticConstants.database.Products.Count(), 1);
            Assert.AreEqual(window.DataGridProducts.Items.Count, 2);
            Assert.AreEqual(((ClassLibrary.Products)window.DataGridProducts.Items.GetItemAt(0)).cost, 30000);

            StaticConstants.database.Products.DeleteOnSubmit(NewProduct);
            StaticConstants.TrySubmitChanges();
            Assert.AreEqual(StaticConstants.database.Products.Count(), 0);
        }

        /// <summary>
        /// Тестовий метод, який тестує таблицю з користувачами
        /// </summary>
        [TestMethod]
        public void TestTableUsers()
        {
            StaticConstants.CreateConnection(DBTest);

            ClassLibrary.Users NewUser = new ClassLibrary.Users
            {
                id = 1,
                name = "Test",
                surname = "User",
                patronymic = "Test Patronymic",
                email = "email",
                password = "password",
                address = "address",
                telephone = "telephone"
            };

            StaticConstants.database.Users.InsertOnSubmit(NewUser);
            StaticConstants.TrySubmitChanges();
            Server.Users window = new Server.Users();

            Assert.AreEqual(StaticConstants.database.Users.Count(), 1);
            Assert.AreEqual(window.DataGridUsers.Items.Count, 2);
            Assert.AreEqual(((ClassLibrary.Users)window.DataGridUsers.Items.GetItemAt(0)).patronymic, "Test Patronymic");

            StaticConstants.database.Users.DeleteOnSubmit(NewUser);
            StaticConstants.TrySubmitChanges();
            Assert.AreEqual(StaticConstants.database.Users.Count(), 0);
        }

        /// <summary>
        /// Тестовий метод, який тестує видалення бази даних
        /// </summary>
        [TestMethod]
        public void TestDropDataBase()
        {
            StaticConstants.CreateConnection("Data Source=(LocalDb)\\MSSQLLocalDB;AttachDBFilename=C:\\Users\\User\\Desktop\\Projecting-Practice-In-VS\\test_drop_database.mdf");
            Assert.IsTrue(StaticConstants.DropDatabase());
        }
    }
}
