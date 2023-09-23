using System.Data.SqlClient; // либа для работы с sql server

namespace Login_Window
{
    internal class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-SMPQJOU3\SQLSERV;Initial Catalog=Авторизация;Integrated Security=True"); // строка подключения

        public void OpenConnection() // метод для открытия подключения
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed) // если связь с БД закрыта, то открываем
            {
                sqlConnection.Open();

            }
        }

        public void CloseConnection() // здесь наоборот
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();

            }
        }

        public SqlConnection getConnection() // метод возвращает строку подключения
        {
            return sqlConnection; // возвращаем объект строки подключения
        }
    }
}
