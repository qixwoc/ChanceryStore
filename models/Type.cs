using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore
{
    public class Type
    {
        int id;
        string name;
        string responsibilities;

        public List<User> Users { get; set; }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Responsibilities
        {
            get { return responsibilities; }
            set
            {
                responsibilities = value;
                OnPropertyChanged("Responsibilities");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Получение ролей пользователей
        /// </summary>
        static public Type[] GetTypes()
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Type[] types;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"Select * from Types", MyConnection);
            SqlCommand select_count = new SqlCommand($"Select Count(*) from Types ", MyConnection);

            MyConnection.Open();
            try
            {
                int count = Convert.ToInt32(select_count.ExecuteScalar());
                types = new Type[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {
                    types[i] = new Type
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Responsibilities = (string)reader[2]
                    };
                    i++;
                }
            }
            catch { types = null; }
            MyConnection.Close();
            return types;
        }

        /// <summary>
        /// Получение роли по id
        /// </summary>
        static public Type GetType(int id)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Type type;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"Select * from Types where Id = {id} ", MyConnection);

            MyConnection.Open();
            try
            {
                type = new Type();
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    type = new Type
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Responsibilities = (string)reader[2]
                    };
                    i++;
                }
            }
            catch { type = null; }
            MyConnection.Close();
            return type;
        }
    }
}
