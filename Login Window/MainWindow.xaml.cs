using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Login_Window
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataBase database = new DataBase(); // создаем экземпляр класса DataBase для работы
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void BTN_Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var loginUserLOGIN = TB_Username.Text;
                var passwordUserLOGIN = TB_Password.Password;

                if (string.IsNullOrEmpty(loginUserLOGIN) || string.IsNullOrEmpty(passwordUserLOGIN))
                {
                    MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.", "Пустые поля!", MessageBoxButton.OK);
                    return; // выходим из метода, так как введены неполные данные
                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable(); // экземпляры классов для работы с БД

                string queryString = $"select id_user, login_user, password_user, is_Admin from register where login_user = '{loginUserLOGIN}' and password_user = '{passwordUserLOGIN}'"; // строка запроса к БД

                SqlCommand sqlCommand = new SqlCommand(queryString, database.getConnection()); // для того, чтобы отработала строка запроса. Передаем команду и строку подключения

                adapter.SelectCommand = sqlCommand;
                adapter.Fill(table); // создаем табл., в к-рую будем заносить данные

                if (table.Rows.Count == 1)
                {
                    MessageBox.Show("Вы успешно вошли", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Window2 main = new Window2();
                    this.Hide();
                    main.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Пользователь с такими данными не найден!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка!", MessageBoxButton.OK);
            }
        }

        private void BTN_Create_Account_Click(object sender, RoutedEventArgs e)
        {
            Window1 sign_in = new Window1();
            this.Hide();
            sign_in.ShowDialog();
            this.Show();

        }
    }
}
