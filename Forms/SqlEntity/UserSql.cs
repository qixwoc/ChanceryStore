using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChanceryStore
{
    public class UserSql
    {
        public User user { get; set; }

        /// <summary>
        /// Строка запроса, где выбрать информацию пользователя , название роли где id роли равен typeId пользователя
        /// </summary>
        static string queryGetUsers = "SELECT [Users].[Id],[Users].[FirstName],[Users].[LastName], [Users].[Image]," +
                $"[Users].[Login],[Users].[Password],[Users].[DateOfBirth],[Users].[LastEntryDateTime],[Users].[Outdate] ,[Types].[Name], [Users].[TypeId]" +
                $"  FROM [Users], [Types]  WHERE [Users].[TypeId] =  [Types].[Id]";

        /// <summary>
        /// Конструктор 
        /// </summary>
        public UserSql(string login, string password)
        {user = new User(login, password);}

        /// <summary>
        /// Конструктор 
        /// </summary>
        public UserSql()
        {user = new User();}

        private SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
       
        /// <summary>
        /// Получение по логину пользователя его остальные данные
        /// </summary>
        public void Data_User(string outdate)
        {
            SqlCommand select_id = new SqlCommand($"SELECT Id from Users where Login = @login AND Outdate = '{outdate}'", MyConnection);
            SqlCommand select_fname = new SqlCommand($"SELECT FirstName from  Users where Login = @login AND Outdate = '{outdate}'", MyConnection);
            SqlCommand select_lname = new SqlCommand($"SELECT LastName from  Users where Login = @login AND Outdate = '{outdate}'", MyConnection);
            SqlCommand select_typeId = new SqlCommand($"SELECT TypeId from  Users where Login = @login AND Outdate = '{outdate}'", MyConnection);
            SqlCommand select_image = new SqlCommand($"SELECT Image from  Users where Login = @login AND Outdate = '{outdate}'", MyConnection);
            select_id.Parameters.Add(new SqlParameter("@login", user.Login));
            select_fname.Parameters.Add(new SqlParameter("@login", user.Login));
            select_lname.Parameters.Add(new SqlParameter("@login", user.Login));
            select_typeId.Parameters.Add(new SqlParameter("@login", user.Login));
            select_image.Parameters.Add(new SqlParameter("@login", user.Login));

            using (MyConnection)
            {
                user.Id = Convert.ToInt32(select_id.ExecuteScalar());
                user.FirstName = select_fname.ExecuteScalar().ToString();
                user.LastName = select_lname.ExecuteScalar().ToString();
                user.Image = (byte[])select_image.ExecuteScalar();
                user.TypeId = Convert.ToInt32(select_typeId.ExecuteScalar());
            }
            user.Type = Type.GetType(user.TypeId);
        }

        /// <summary>
        /// Проверить верно ли введен логин и пароль
        /// </summary>
        public bool checkPasswordAndLogin()
        { // запрос к БД на поиск логина и пароля 
            SqlCommand checkPasswordAndLogin = new SqlCommand("SELECT count(*) FROM Users where Login = @login and Password =  HASHBYTES('SHA2_256', convert(nvarchar(50), @password)) AND Outdate <> 'YES'", MyConnection); // комманда
            checkPasswordAndLogin.Parameters.Add(new SqlParameter("@login", user.Login));
            checkPasswordAndLogin.Parameters.Add(new SqlParameter("@password", user.Password));
            MyConnection.Open();

            if (Convert.ToInt32(checkPasswordAndLogin.ExecuteScalar()) == 1)
            {return true;}
            MyConnection.Close();
            return false;
        }

        /// <summary>
        /// Существует ли уже такой логин
        /// </summary>
        static public bool checkLoginHas(string login)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            // запрос к БД на поиск логина и пароля 
            SqlCommand checkLoginHas = new SqlCommand("SELECT count(*) FROM Users where Login = @login", MyConnection); // комманда
            checkLoginHas.Parameters.Add(new SqlParameter("@login", login));
            MyConnection.Open();

            if (Convert.ToInt32(checkLoginHas.ExecuteScalar()) == 1)
            {
                return true; // есть
            }
            MyConnection.Close();
            return false; // нету
        }


        /// <summary>
        /// Добавить/ восстановить из архива
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isOutdate">устаревшее или актуальное</param>
        public static void AddArchieve(int id, string isOutdate)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand($"UPDATE Users SET Outdate = '{isOutdate}' WHERE Id = @id ", MyConnection); // команда
            cmd.Parameters.Add(new SqlParameter("@id", id));
            MyConnection.Open();

            cmd.ExecuteNonQuery();

            MyConnection.Close();
        }

        /// <summary>
        /// Получить пользователей актуальных или в архиве
        /// </summary>
        /// <param name="outdate"> устаревшее или актуальное</param>
        /// <returns></returns>
        static public User[] GetUsers(string outdate, out int count)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            User[] users;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"{queryGetUsers} AND [Users].[Outdate] = '{outdate}'", MyConnection);
            SqlCommand select_count = new SqlCommand($"Select Count(*) FROM Users WHERE Outdate = '{outdate}' ", MyConnection);

            count = 0;
            MyConnection.Open();
            //try
            //{
                count = Convert.ToInt32(select_count.ExecuteScalar());
                users = new User[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {
                    users[i] = new User();
                    users[i].Id = (int)reader[0];
                    users[i].FirstName = (string)reader[1];
                    users[i].LastName = (string)reader[2];
                users[i].Image = (byte[])reader[3];
                users[i].Login = (string)reader[4];
                    //users[i].Password = (string)reader[5];
                    users[i].DateOfBirth = (DateTime)reader[6];
                    users[i].LastEntryDateTimeView = ((DateTime)reader[7]).ToString("dd.MM.yyyy hh:mm");
                    users[i].Outdate = (string)reader[8];
                    users[i].TypeName = (string)reader[9];
                    users[i].Type.Name = (string)reader[9];
                    users[i].TypeId = (int)reader[10];
                    users[i].Type.Id = (int)reader[10];

                    i++;
                }
            //}
            //catch { users = null; }
            MyConnection.Close();
            return users;
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns></returns>
        static public User[] GetUsers(out int count)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            User[] users;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand(queryGetUsers, MyConnection);
            SqlCommand select_count = new SqlCommand("Select Count(*) FROM Users", MyConnection);

            count = 0;
            MyConnection.Open();
            try
            {
                count = Convert.ToInt32(select_count.ExecuteScalar());
                users = new User[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {
                    users[i] = new User();
                    users[i].Id = (int)reader[0];
                    users[i].FirstName = (string)reader[1];
                    users[i].LastName = (string)reader[2];
                    users[i].Image = (byte[])reader[3];
                    users[i].Login = (string)reader[4];
                    //users[i].Password = (string)reader[5];
                    users[i].DateOfBirth = (DateTime)reader[6];
                    users[i].LastEntryDateTimeView = ((DateTime)reader[7]).ToString("dd.MM.yyyy hh:mm");
                    users[i].Outdate = (string)reader[8];
                    users[i].TypeName = (string)reader[9];
                    users[i].Type.Name = (string)reader[9];
                    users[i].TypeId = (int)reader[10];
                    users[i].Type.Id = (int)reader[10];
                    i++;
                }
            }
            catch { users = null; }
            MyConnection.Close();
            return users;
        }

        /// <summary>
        /// Получить продукт по id
        /// </summary>
        /// <returns></returns>
        static public User GetUser(int userid)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            User user = null;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"{queryGetUsers} AND [Users].Id = {userid} ", MyConnection);

            MyConnection.Open();
            try
            {
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    user = new User();
                    user.Id = (int)reader[0];
                    user.FirstName = (string)reader[1];
                    user.LastName = (string)reader[2];
                    user.Image = (byte[])reader[3];
                    user.Login = (string)reader[4];
                    //user.Password = (string)reader[5];
                    user.DateOfBirth = (DateTime)reader[6];
                    user.LastEntryDateTimeView = ((DateTime)reader[7]).ToString("dd.MM.yyyy hh:mm");
                    user.Outdate = (string)reader[8];
                    user.TypeName = (string)reader[9];
                    user.Type.Name = (string)reader[9];
                    user.TypeId = (int)reader[10];
                    user.Type.Id = (int)reader[10];
                    i++;
                }
            }
            catch
            {user = null;}
        MyConnection.Close();
            return user;
        }


        /// <summary>
        /// Добавление пользователя в БД
        /// </summary>
        public void AddUser()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Users ([FirstName]," +
                "[LastName] ,[Image],[Login],[Password]" +
                ",[LastEntryDateTime] ,[DateOfBirth] ,[TypeId]) "
                +
                "values( @firstName,@lastName,@image,@login,HASHBYTES('SHA2_256', convert(nvarchar(50), @password)),@lastEntryDateTime," +
                "@dateOfBirth, @typeId)", MyConnection);
            cmd.Parameters.Add(new SqlParameter("@firstName", user.FirstName));
            cmd.Parameters.Add(new SqlParameter("@lastName", user.LastName));
            cmd.Parameters.Add(new SqlParameter("@image", user.Image));
            cmd.Parameters.Add(new SqlParameter("@login", user.Login));
            cmd.Parameters.Add(new SqlParameter("@password", user.Password));
            cmd.Parameters.Add(new SqlParameter("@lastEntryDateTime", "1999-01-02"));
            cmd.Parameters.Add(new SqlParameter("@dateOfBirth", user.DateOfBirth));
            cmd.Parameters.Add(new SqlParameter("@typeId", user.TypeId));
            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();
        }

        /// <summary>
        /// Изменение пользователя в БД
        /// </summary>
        public void EditUser()
        {
            SqlCommand cmd = new SqlCommand("UPDATE Users SET firstName = @firstName, [lastName] = @lastName ," +
            "[image] = @image,[login] = @login,[password] = HASHBYTES('SHA2_256', convert(nvarchar(50), @password))," +
            "[dateOfBirth] = @dateOfBirth,[typeId] = @typeId WHERE Id = @id ", MyConnection);
            cmd.Parameters.Add(new SqlParameter("@id", user.Id));
            cmd.Parameters.Add(new SqlParameter("@firstName", user.FirstName));
            cmd.Parameters.Add(new SqlParameter("@lastName", user.LastName));
            cmd.Parameters.Add(new SqlParameter("@image", user.Image));
            cmd.Parameters.Add(new SqlParameter("@login", user.Login));
            cmd.Parameters.Add(new SqlParameter("@password", user.Password));
            cmd.Parameters.Add(new SqlParameter("@dateOfBirth", user.DateOfBirth));
            cmd.Parameters.Add(new SqlParameter("@typeId", user.TypeId));

            MyConnection.Open();
            try { cmd.ExecuteNonQuery(); }
            catch { MessageBox.Show("Не удалось добавить"); }
            MyConnection.Close();
        }

        /// <summary>
        /// Удаление пользователя в БД
        /// </summary>
        static public void DeleteUser(int id)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE id = @id ", MyConnection);
            cmd.Parameters.Add(new SqlParameter("@id", id));

            MyConnection.Open();
            try { cmd.ExecuteNonQuery(); }
            catch { }
            MyConnection.Close();
        }


        /// <summary>
        /// Нахождение пользователя по id в БД
        /// </summary>
        public void FindUserById(int id)
        {
            SqlCommand select_firstName = new SqlCommand($"select FirstName from  Users WHERE Id = {id} ", MyConnection);
            SqlCommand select_lastName = new SqlCommand($"select LastName from  Users WHERE Id = {id} ", MyConnection);
            SqlCommand select_image = new SqlCommand($"select Image from  Users WHERE Id = {id} ", MyConnection);
            SqlCommand select_login = new SqlCommand($"select Login from Users WHERE Id = {id} ", MyConnection);
            SqlCommand select_password = new SqlCommand($"select Password from Users WHERE Id = {id} ", MyConnection);
            SqlCommand select_lastEntryDateTime = new SqlCommand($"select LastEntryDateTime from Users WHERE Id = {id} ", MyConnection);
            SqlCommand select_dateOfBirth = new SqlCommand($"select DateOfBirth from Users WHERE Id = {id} ", MyConnection);
            SqlCommand select_typeId = new SqlCommand($"select TypeId from Users WHERE Id = {id} ", MyConnection);

            MyConnection.Open();

            user.FirstName = select_firstName.ExecuteScalar().ToString();
            user.LastName = select_lastName.ExecuteScalar().ToString();
            user.Image = (byte[])select_image.ExecuteScalar();
            user.Login = select_login.ExecuteScalar().ToString();
            user.Password = select_password.ExecuteScalar().ToString();
            user.LastEntryDateTime = (DateTime)select_lastEntryDateTime.ExecuteScalar();
            user.DateOfBirth = (DateTime)select_dateOfBirth.ExecuteScalar();
            user.TypeId = Convert.ToInt32(select_typeId.ExecuteScalar());
            MyConnection.Close();
        }

        /// <summary>
        /// Нахождение ФИО пользователя по полю
        /// </summary>
        /// <param name="radiobuttonName"> названия радиобатона </param>
        /// <param name="parameter"> текст в текстовом поле </param>
        /// <param name="count"> количество </param>
        /// <returns></returns>
        static public User[] GetUserSearchField(string radiobuttonName, string parameter, out int count)
        {
            string needField;

            // перевод из radiobutton name в sql поле
            switch (radiobuttonName) // проверка радиобатона
            {
                case "fioRb": // если выбрано фио
                    needField = "Concat(FirstName, LastName)";
                    break;

                case "loginRb": // если выбран логин
                    needField = "Login";
                    break;
                default: // по умолчанию фио или логин 
                    needField = "Concat(FirstName, LastName) like('%' + @parameter + '%') or Login "; 
                    break;
            }

            User[] users = GetUserQuery($"{queryGetUsers} AND {needField} like ('%' + @parameter + '%')",
                $"SELECT COUNT(*) FROM Users WHERE {needField} like ('%' + @parameter + '%')", parameter, out count);
            return users;
        }

        /// <summary>
        /// Нахождение ФИО пользователя по полю
        /// </summary>
        /// <param name="parametr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public User[] FindUserGroup(string typeid, string sqlParametr, string data, out int count)
        {
            User[] users;

            if ((sqlParametr == null || sqlParametr == "") && data == "")
            {
                users = GetUserQuery($"{queryGetUsers}  AND   TypeId = {typeid}",
              $"SELECT COUNT(*) FROM Users WHERE   TypeId = {typeid}", out count);
            }

            else if (data == "")
            {
                users = GetUserQuery($"{queryGetUsers}  AND   TypeId = {typeid}",
              $"SELECT COUNT(*) FROM Users WHERE   TypeId = {typeid}", out count);
            }

            else if(sqlParametr == null || sqlParametr == "") { 
            users = GetUserQuery($"{queryGetUsers}  AND  like ('%' + @parameter + '%') AND TypeId = {typeid}",
              $"SELECT COUNT(*) FROM Users   like ('%' + @parameter + '%') WHERE TypeId = {typeid}", data, out count);
            }

            else { 
            users = GetUserQuery($"{queryGetUsers}  AND {sqlParametr} like ('%' + @parameter + '%') AND TypeId = {typeid}",
  $"SELECT COUNT(*) FROM Users WHERE {sqlParametr} like ('%' + @parameter + '%') AND TypeId = {typeid}", data, out count);
            }

            return users;
        }


        /// <summary>
        /// Сортировка даты сначала новые или сначала старые.
        /// </summary>
        static public User[] SortDateNew(string oldOrNew_rb)
        {
            User[] users = null;
            if (oldOrNew_rb == "rbAgeNew") // сначала новые
            { users = GetUserQuery($"{queryGetUsers} order by [DateOfBirth] desc", "SELECT COUNT(*) from Users"); }

            else if (oldOrNew_rb == "rbAgeOld") // сначала старые
            { users = GetUserQuery($"{queryGetUsers} order by [DateOfBirth] asc", "SELECT COUNT(*) from Users "); }

            return users;
        }


        /// <summary>
        /// Получает пользователей по запросу с параметром
        /// </summary>
        static public User[] GetUserQuery(string queryString, string countQueryString, string parameter, out int count)
        {
            count = 0;
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd_query = new SqlCommand(queryString, MyConnection);
            SqlCommand cmd_count = new SqlCommand(countQueryString, MyConnection);
            cmd_query.Parameters.Add(new SqlParameter("@parameter", parameter));
            cmd_count.Parameters.Add(new SqlParameter("@parameter", parameter));

            SqlDataReader reader;
            User[] users;
            MyConnection.Open();
            try
            {
                count = Convert.ToInt32(cmd_count.ExecuteScalar());
                reader = cmd_query.ExecuteReader();
                users = new User[count];
                int i = 0;

                if (reader.HasRows == false)
                { users = null; }

                while (reader.Read() && i < count)
                {
                    users[i] = new User();
                    users[i].Id = (int)reader[0];
                    users[i].FirstName = (string)reader[1];
                    users[i].LastName = (string)reader[2];
                    users[i].Image = (byte[])reader[3];
                    users[i].Login = (string)reader[4];
                    users[i].Password = (string)reader[5];
                    users[i].DateOfBirth = (DateTime)reader[6];
                    users[i].LastEntryDateTimeView = ((DateTime)reader[7]).ToString("dd.MM.yyyy hh:mm");
                    users[i].Outdate = (string)reader[8];
                    users[i].TypeName = (string)reader[9];
                    users[i].Type.Name = (string)reader[9];
                    users[i].TypeId = (int)reader[10];
                    users[i].Type.Id = (int)reader[10];
                    i++;
                }
        }
            catch { users = null; }
    MyConnection.Close();
            return users;
        }


        /// <summary>
        /// Получает пользователей по запросу 
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="countQueryString"></param>
        /// <returns> массив обьектов пользователей </returns>
        static public User[] GetUserQuery(string queryString, string countQueryString, out int count)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd_query = new SqlCommand(queryString, MyConnection);
            SqlCommand cmd_count = new SqlCommand(countQueryString, MyConnection);

            SqlDataReader reader;
            User[] users;
            MyConnection.Open();
            try
            {
                
                count = Convert.ToInt32(cmd_count.ExecuteScalar());
                reader = cmd_query.ExecuteReader();
                users = new User[count];
                int i = 0;

                if (reader.HasRows == false)
                { users = null; }

                while (reader.Read() && i < count)
                {
                    users[i] = new User();
                    users[i].Id = (int)reader[0];
                    users[i].FirstName = (string)reader[1];
                    users[i].LastName = (string)reader[2];
                    users[i].Image = (byte[])reader[3];
                    users[i].Login = (string)reader[4];
                    users[i].Password = (string)reader[5];
                    users[i].DateOfBirth = (DateTime)reader[6];
                    users[i].LastEntryDateTimeView = ((DateTime)reader[7]).ToString("dd.MM.yyyy hh:mm");
                    users[i].Outdate = (string)reader[8];
                    users[i].TypeName = (string)reader[9];
                    users[i].Type.Name = (string)reader[9];
                    users[i].TypeId = (int)reader[10];
                    users[i].Type.Id = (int)reader[10];
                    i++;
                }
            }
            catch { users = null; count = 0; }
            MyConnection.Close();
            return users;
        }

        /// <summary>
        /// Получает пользователей по запросу 
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="countQueryString"></param>
        /// <returns> массив обьектов пользователей </returns>
        static public User[] GetUserQuery(string queryString, string countQueryString)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd_query = new SqlCommand(queryString, MyConnection);
            SqlCommand cmd_count = new SqlCommand(countQueryString, MyConnection);

            SqlDataReader reader;
            User[] users;
            MyConnection.Open();
            try
            {
                int count = Convert.ToInt32(cmd_count.ExecuteScalar());
                reader = cmd_query.ExecuteReader();
                users = new User[count];
                int i = 0;

                if (reader.HasRows == false)
                { users = null; }

                while (reader.Read() && i < count)
                {
                    users[i] = new User();
                    users[i].Id = (int)reader[0];
                    users[i].FirstName = (string)reader[1];
                    users[i].LastName = (string)reader[2];
                    users[i].Image = (byte[])reader[3];
                    users[i].Login = (string)reader[4];
                    users[i].Password = (string)reader[5];
                    users[i].DateOfBirth = (DateTime)reader[6];
                    users[i].LastEntryDateTimeView = ((DateTime)reader[7]).ToString("dd.MM.yyyy hh:mm");
                    users[i].Outdate = (string)reader[8];
                    users[i].TypeName = (string)reader[9];
                    users[i].Type.Name = (string)reader[9];
                    users[i].TypeId = (int)reader[10];
                    users[i].Type.Id = (int)reader[10];
                    i++;
                }
            }
            catch { users = null; }
            MyConnection.Close();
            return users;
        }

        /// <summary>
        /// Добавление в историю запись
        /// </summary>
        static public void AddLastEntryDateTime(int id)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

            DateTime current_date = DateTime.Now;

            // запрос к бд на поиск логина и пароля 
            SqlCommand AddHistory = new SqlCommand("UPDATE Users SET [LastEntryDateTime] =  @current_date WHERE ID = @id ", MyConnection); // комманда
            AddHistory.Parameters.Add(new SqlParameter("@id", id));
            AddHistory.Parameters.Add(new SqlParameter("@current_date", current_date));
            MyConnection.Open();
            AddHistory.ExecuteNonQuery();
            MyConnection.Close();
        }
    }
}
