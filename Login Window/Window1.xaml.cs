using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Login_Window
{
    public partial class Window1 : Window
    {
        DataBase dataBase = new DataBase();

        public Window1()
        {
            InitializeComponent();
        }

        private void BTN_Register_Click(object sender, RoutedEventArgs e)
        {
            int TB_UsernameMaxLength = 50;
            int TB_PasswordMaxLength = 50;


            var RegLoginUser = TB_Username2.Text;
            var RegPassUser = TB_Password2.Password;
            var RegConfirmPassUser = TB_ConfirmPassword.Password;

            if (checkUser())
            {
                return;
            }


            if (RegLoginUser.Length > TB_UsernameMaxLength || RegPassUser.Length > TB_PasswordMaxLength)
            {
                MessageBox.Show($"Логин не должен быть больше {TB_UsernameMaxLength} символов, а пароль {TB_PasswordMaxLength} символов", "Ошибка! Неверная длина логина или пароля", MessageBoxButton.OK);
            }
            else
            {

                // Проверка на совпадение паролей
                if (RegPassUser != RegConfirmPassUser)
                {
                    MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK);
                    TB_ConfirmPassword.Password = "";
                    return;
                }



                string queryString = $"INSERT INTO register (login_user, password_user, is_Admin) VALUES ('{RegLoginUser}', '{RegPassUser}', 0)";

                SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

                dataBase.OpenConnection();


                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!", "Успех", MessageBoxButton.OK);
                    MainWindow log_in = new MainWindow();
                    this.Hide();
                    log_in.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при создании аккаунта", "Ошибка", MessageBoxButton.OKCancel);
                }
                dataBase.CloseConnection();
            }
            
        }

        private bool checkUser()
        {
            var RegLoginUser = TB_Username2.Text;
            var RegPassUser = TB_Password2.Password;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"select id_user, login_user, password_user, is_Admin from register where login_user = '{RegLoginUser}' and password_user = '{RegPassUser}'";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь с такими данными уже существует!", "Ошибка!");
                return true;
            }
            else
            {
                return false;
            }

        }

        private void BTN_Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.ShowDialog();
            this.Show();
        }
    }
}
