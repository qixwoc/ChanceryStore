using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore.models
{
    public class Order : INotifyPropertyChanged
    {

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Outdate { get; set; }
        public int ClientId { get; set; }
        public int ProductCode { get; set; }
        public string Status { get; set; }

        public string DateTimeView { get; set; }

        Client client = new Client();
        public Client Client
        {
            get { return client; }
            set
            {
                client = value;
                OnPropertyChanged("Client");
            }
        }

        Product product = new Product();
        public Product Product
        {
            get { return product; }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        /// <summary>
        /// Строка запроса, где выбрать информацию заказа , название роли где id роли равен typeId пользователя
        /// </summary>
        static string queryGetOrdersAndClientsAndProducts = "SELECT [Orders].[Id],[Orders].[DateTime],[Orders].[Outdate], [Orders].[ClientId]," +
                $"[Orders].[ProductCode],[Orders].[Status]," +
            $"[Clients].[Id],[Clients].[FirstName],[Clients].[LastName] ,[Clients].[Outdate], " +
            $"[Products].[Code],[Products].[Name],[Products].[Price],[Products].[Amount] ," +
            $"[Products].[Barcode],[Products].[Image],[Products].[ProviderId] ,[Products].[Outdate],[Products].[CategoryId]" +
                $"  FROM [Orders], [Products] , [Clients]  WHERE [Orders].[ClientId] =  [Clients].[Id] AND [Orders].[ProductCode] =  [Products].[Code]";


        private SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);


        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        static public Order[] GetOrders(out int count)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Order[] orders;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand(queryGetOrdersAndClientsAndProducts, MyConnection);
            SqlCommand select_count = new SqlCommand("Select Count(*) FROM Orders", MyConnection);

            count = 0;
            MyConnection.Open();
            try
            {
                count = Convert.ToInt32(select_count.ExecuteScalar());
                orders = new Order[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {

                    orders[i] = new Order();
                    orders[i].Id = (int)reader[0];
                    orders[i].DateTime = (DateTime)reader[1];
                    orders[i].DateTimeView = ((DateTime)reader[1]).ToString("dd.MM.yyyy hh:mm");
                    orders[i].Outdate = (string)reader[2];
                    orders[i].ClientId = (int)reader[3];
                    orders[i].ProductCode = (int)reader[4];
                    orders[i].Status = (string)reader[5];

                    orders[i].Client.Id = (int)reader[6];
                    orders[i].Client.FirstName = (string)reader[7];
                    orders[i].Client.LastName = (string)reader[8];
                    orders[i].Client.Outdate = (string)reader[9];

                    orders[i].Product.Code = (int)reader[10];
                    orders[i].Product.Name = (string)reader[11];
                    orders[i].Product.Price = (double)reader[12];
                    i++;
                }
            }
            catch { orders = null; }
            MyConnection.Close();
            return orders;
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        static public Order[] GetOrders(string tab, out int count)
        {
            string status = "Canceled";
            
            if (tab == "cancel")
            {
              status = "Canceled";
            }

            else if(tab == "Created")
            { status = "Created"; }

            else if (tab == "InProgress")
            { status = "InProgress"; }

            else if (tab == "Ready")
            { status = "Ready"; }

            else if (tab == "Completed")
            { status = "Completed"; }


            string  query = "SELECT [Orders].[Id],[Orders].[DateTime],[Orders].[Outdate], [Orders].[ClientId]," +
                $"[Orders].[ProductCode],[Orders].[Status]," +
            $"[Clients].[Id],[Clients].[FirstName],[Clients].[LastName] ,[Clients].[Outdate], " +
            $"[Products].[Code],[Products].[Name],[Products].[Price],[Products].[Amount] ," +
            $"[Products].[Barcode],[Products].[Image],[Products].[ProviderId] ,[Products].[Outdate],[Products].[CategoryId]" +
                $"  FROM [Orders], [Products] , [Clients]  WHERE [Orders].[ClientId] =  [Clients].[Id] AND [Orders].[ProductCode] =  [Products].[Code] AND [Orders].[Status] = '{status}'";


            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Order[] orders;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand(query, MyConnection);
            SqlCommand select_count = new SqlCommand($"Select Count(*) FROM Orders WHERE [Orders].[Status] = '{status}'", MyConnection);

            count = 0;
            MyConnection.Open();
            try
            {
                count = Convert.ToInt32(select_count.ExecuteScalar());
                orders = new Order[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {

                    orders[i] = new Order();
                    orders[i].Id = (int)reader[0];
                    orders[i].DateTime = (DateTime)reader[1];
                    orders[i].DateTimeView = ((DateTime)reader[1]).ToString("dd.MM.yyyy hh:mm");
                    orders[i].Outdate = (string)reader[2];
                    orders[i].ClientId = (int)reader[3];
                    orders[i].ProductCode = (int)reader[4];
                    orders[i].Status = (string)reader[5];

                    orders[i].Client.Id = (int)reader[6];
                    orders[i].Client.FirstName = (string)reader[7];
                    orders[i].Client.LastName = (string)reader[8];
                    orders[i].Client.Outdate = (string)reader[9];

                    orders[i].Product.Code = (int)reader[10];
                    orders[i].Product.Name = (string)reader[11];
                    orders[i].Product.Price = (double)reader[12];
                    i++;
                }
            }
            catch { orders = null; }
            MyConnection.Close();
            return orders;
        }

        /// <summary>
        /// Добавление заказа в БД
        /// </summary>
        public void AddOrder()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Orders ([DateTime] ,[ClientId],[ProductCode]) "
                   + "values( @DateTime,@ClientId,@ProductCode)", MyConnection);

            cmd.Parameters.Add(new SqlParameter("@DateTime", DateTime));
            cmd.Parameters.Add(new SqlParameter("@ClientId", ClientId));
            cmd.Parameters.Add(new SqlParameter("@ProductCode", ProductCode));

            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
