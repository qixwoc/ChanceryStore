using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore.models
{
   public class Provider
    {
        int id;
        string name;
        string address;
        string phone;


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

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Получение ролей пользователей
        /// </summary>
        static public Provider[] GetProviders()
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Provider[] providers;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"SELECT * FROM Providers", MyConnection);
            SqlCommand select_count = new SqlCommand($"SELECT COUNT(*) FROM Providers ", MyConnection);

            MyConnection.Open();
            try
            {
                int count = Convert.ToInt32(select_count.ExecuteScalar());
                providers = new Provider[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {
                    providers[i] = new Provider
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        Address = (string)reader[2],
                        Phone = (string)reader[3],
                    };
                    i++;
                }
            }
            catch { providers = null; }
            MyConnection.Close();
            return providers;
        }

        /// <summary>
        /// Получить поставщика по id
        /// </summary>
        /// <returns></returns>
        static public Provider GetProvider(int providerid)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Provider provider = null;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"SELECT * FROM Providers WHERE Id = {providerid} ", MyConnection);

            MyConnection.Open();
            try
            {
                reader = select_values.ExecuteReader();
            int i = 0;
            while (reader.Read())
                {
                provider = new Provider();
                provider.Id = (int)reader[0];
                provider.Name = (string)reader[1];
                provider.Address = (string)reader[2];
                provider.Phone = (string)reader[3];
                i++;
            }
            }
            catch
            {
                provider = null;
        }
        MyConnection.Close();
            return provider;
        }

    }
}
