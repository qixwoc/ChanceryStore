using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore.models
{
    public class Category : INotifyPropertyChanged
    {
        private int id;
        private string name;


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


        /// <summary>
        /// Получение категорий
        /// </summary>
        static public Category[] GetCategory()
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Category[] categories;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"SELECT * FROM Category", MyConnection);
            SqlCommand select_count = new SqlCommand($"SELECT COUNT(*) FROM Category ", MyConnection);

            MyConnection.Open();
            try
            {
                int count = Convert.ToInt32(select_count.ExecuteScalar());
                categories = new Category[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {
                    categories[i] = new Category
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1]
                    };
                    i++;
                }
            }
            catch { categories = null; }
            MyConnection.Close();
            return categories;
        }

        /// <summary>
        /// Получить категории по id
        /// </summary>
        /// <returns></returns>
        static public string GetCategoryNameFromId(int categoryId)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            string categoryName;
           
            SqlCommand select_values = new SqlCommand($"SELECT * FROM Category WHERE Id = {categoryId} ", MyConnection);

            MyConnection.Open();
            try
            { categoryName = select_values.ExecuteScalar().ToString();}
            catch
            { categoryName = null; }
            MyConnection.Close();
            return categoryName;
        }

        /// <summary>
        /// Получить  id категории
        /// </summary>
        /// <returns></returns>
        static public int? GetCategoryIdFromName(string providerName)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            int? categoryId;
           
            SqlCommand select_values = new SqlCommand($"SELECT * FROM Category WHERE Name = @providerName ", MyConnection);
            select_values.Parameters.Add(new SqlParameter("@providerName", providerName));

            MyConnection.Open();
            //try
            //{
                categoryId = Convert.ToInt32(select_values.ExecuteScalar());/*}*/
            //catch
            //{categoryId = null;}
            MyConnection.Close();
            return categoryId;
        }


        /// <summary>
        /// Получает категорию по запросу
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="countQueryString"></param>
        /// <returns> массив обьектов пользователей </returns>
        static public Category GetCategoryQuery(string queryString)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Category category;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand(queryString, MyConnection);

            MyConnection.Open();
            try
            {
                reader = select_values.ExecuteReader();
                int i = 0;
               
                    category = new Category();
                    category.Id = (int)reader[0];
                    category.Name = (string)reader[1];
                    i++;               
            }
            catch
            {
                category = null;
            }
            MyConnection.Close();
            return category;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    }
