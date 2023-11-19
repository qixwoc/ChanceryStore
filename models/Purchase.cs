using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore.models
{
    public class Purchase : INotifyPropertyChanged
    {
        int code;
        string status;
        double summa;
        int amount;
        string outdate;
        int userId;
        int productId;
        
        User user = new User();
        Product product = new Product();

        string userNameView;

        public int Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        public string Status
        {
            get { return status; }
            set
            {   if (status == value)
                    return;
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
                OnPropertyChanged("Summa");
            }
        }

        public double Summa
        {
            get { return product.Price * Amount; }
            set
            {
                summa = product.Price * Amount;
                OnPropertyChanged("Summa");
            }
        }

        public string Outdate
        {
            get { return outdate; }
            set
            {
                outdate = value;
                OnPropertyChanged("Outdate");
            }
        }

        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public int ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                OnPropertyChanged("ProductId");
            }
        }

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        public Product Product
        {
            get { return product; }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }


        public string UserNameView
        {
            get { return userNameView; }
            set
            {
                userNameView = value;
                OnPropertyChanged("UserName");
            }
        }

        /// <summary>
        /// Список закупок в таблице datagrid
        /// </summary>
        /// 

        static public BindingList<Purchase> ProductsForPurchaseObsColToPur = new BindingList<Purchase>();


        /// <summary>
        /// Получить пользователей
        /// </summary>
        /// <param name="outdate"> устаревшее или актуальное</param>
        /// <returns></returns>
        static public Purchase[] GetPurchase(string status, string outdate, out int count)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Purchase[] purchases;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"SELECT [Purchases].[Code],[Purchases].[Status],[Purchases].[Summa],[Purchases].[Amount],[Users].[FirstName],[Users].[LasttName],[Products].[Name] FROM [Purchases], [Products], [Users]  WHERE [Purchases].[UserId] =  [Users].[Id] AND [Purchases].[ProductId] =  [Products].[Code] AND [Purchases].[Outdate] = '{outdate}'", MyConnection);
            SqlCommand select_count = new SqlCommand($"Select Count(*) FROM Purchases WHERE Status = '{status}' ", MyConnection);

            count = 0;
            MyConnection.Open();
            try
            {

                count = Convert.ToInt32(select_count.ExecuteScalar());
                purchases = new Purchase[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {
                    string nameUs = reader[4].ToString().Substring(0,1) + "."; ;
                    purchases[i] = new Purchase();

                    purchases[i].Code = (int)reader[0];
                    purchases[i].Status = (string)reader[1];
                    purchases[i].Summa = (double)reader[2];
                    purchases[i].Amount = (int)reader[3];
                    purchases[i].UserNameView = (string)reader[5] + " " + nameUs;
                    purchases[i].Product.Name = (string)reader[6];
                    
                    i++;
                }
            }
            catch { purchases = null; }
            MyConnection.Close();
            return purchases;
        }

        /// <summary>
        /// Получить закупки
        /// </summary>
        /// <param name="outdate"> устаревшее или актуальное</param>
        /// <returns></returns>
        static public Purchase[] GetPurchase(string outdate, out int count)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Purchase[] purchases;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"SELECT [Purchases].[Code],[Purchases].[Status],[Purchases].[Summa],[Purchases].[Amount],[Users].[FirstName],[Users].[LastName],[Products].[Name] FROM [Purchases], [Products], [Users]  WHERE [Purchases].[UserId] =  [Users].[Id] AND [Purchases].[ProductId] =  [Products].[Code] AND [Purchases].[Outdate] = '{outdate}'", MyConnection);
            SqlCommand select_count = new SqlCommand($"Select Count(*) FROM Purchases WHERE Outdate = '{outdate}' ", MyConnection);

            count = 0;
            MyConnection.Open();
            try
            {

                count = Convert.ToInt32(select_count.ExecuteScalar());
                purchases = new Purchase[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {
                    string nameUs = reader[4].ToString().Substring(0, 1) + "."; ;
                    purchases[i] = new Purchase();

                    purchases[i].Code = (int)reader[0];
                    purchases[i].Status = (string)reader[1];
                    purchases[i].Summa = (double)reader[2];
                    purchases[i].Amount = (int)reader[3];
                    purchases[i].UserNameView = (string)reader[5] + " " + nameUs;
                    purchases[i].Product.Name = (string)reader[6];

                    i++;
                }
            }
            catch { purchases = null; }
            MyConnection.Close();
            return purchases;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
    }
}
