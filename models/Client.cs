using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore.models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Outdate { get; set; }


        private SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

        /// <summary>
        /// Добавление клиента в БД
        /// </summary>
        public void AddClient()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Clients ([FirstName] ,[LastName]) "
                   + "values( @FirstName,@LastName)", MyConnection);

            cmd.Parameters.Add(new SqlParameter("@FirstName", FirstName));
            cmd.Parameters.Add(new SqlParameter("@LastName", LastName));

            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();
        }

        /// <summary>
        /// Получить продукт по id
        /// </summary>
        /// <param name="outdate"> устаревшее или актуальное</param>
        /// <returns></returns>
        static public Client GetClient(string firstname, string lastname)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Client client = null;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"Select * from Clients where FirstName = '{firstname}' AND LastName = '{lastname}'", MyConnection);

            MyConnection.Open();
            try
            {
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    client = new Client
                    {
                        Id = (int)reader[0],
                        FirstName = (string)reader[1],
                        LastName = (string)reader[2],
                    };
                    i++;
                }
            }
            catch
            {
                client = null;
            }
            MyConnection.Close();
            return client;
        }

    }
}
