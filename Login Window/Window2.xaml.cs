using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Login_Window
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        DataBase database = new DataBase();
        public Window2()
        {
            InitializeComponent();

            database.OpenConnection();
            string queryString = "SELECT * FROM register"; // Из какой таблицы нужен вывод 
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable("register"); // В скобках указываем название таблицы
            adapter.Fill(table);


            UsersDataGrid.ItemsSource = table.DefaultView; // Сам вывод 
            database.CloseConnection();

        }

        private void BTN_View_Grid_Click(object sender, RoutedEventArgs e)
        {
 
        }
    }
}
