using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChanceryStore.models
{
    public class ProductSql
    {

        public Product product { get; set; }

        /// <summary>
        /// Конструктор 
        /// </summary>
        public ProductSql()
        { product = new Product(); }

        private SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

        /// <summary>
        /// Получить продукты
        /// </summary>
        /// <param name="outdate"> устаревшее или актуальное</param>
        /// <returns></returns>
        static public Product[] GetProducts(string outdate)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Product[] products;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"Select * from Products where Outdate = '{outdate}' ", MyConnection);
            SqlCommand select_count = new SqlCommand($"Select Count(*) from Products where Outdate = '{outdate}' ", MyConnection);

            MyConnection.Open();
            try
            {
                int count = Convert.ToInt32(select_count.ExecuteScalar());
                products = new Product[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {
                    products[i] = new Product
                    {
                        Code = (int)reader[0],
                        Name = (string)reader[1],
                        Price = (double)reader[2],
                        Amount = (int)reader[3],
                        Barcode = (int)reader[4],
                        Image = (byte[])reader[5],
                        ProviderId = (int)reader[6],
                        Outdate = (string)reader[7]
                    };
                    i++;
                }
            }
            catch { products = null; }
            MyConnection.Close();
            return products;
        }


        /// <summary>
        /// Получить продукт по id
        /// </summary>
        /// <returns></returns>
        static public Product GetProduct(int productid)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Product product = null;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand($"Select * from Products where Code = {productid} ", MyConnection);

            MyConnection.Open();
            try
            {
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    product = new Product
                    {
                        Code = (int)reader[0],
                        Name = (string)reader[1],
                        Price = (double)reader[2],
                        Amount = (int)reader[3],
                        Barcode = (int)reader[4],
                        Image = (byte[])reader[5],
                        ProviderId = (int)reader[6],
                        Outdate = (string)reader[7]
                    };
                    i++;
                }
            }
            catch { product = null; }
            MyConnection.Close();
            return product;
        }

        /// <summary>
        /// Добавление продукта в БД
        /// </summary>
        public void AddProduct()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Products ([Name]," +
                   "[Price] ,[Amount],[Barcode],[Image]" +
                   ",[ProviderId]) "
                   + "values( @Name,@Price,@Amount,@Barcode,@Image,@ProviderId)", MyConnection);

            cmd.Parameters.Add(new SqlParameter("@Name", product.Name));
            cmd.Parameters.Add(new SqlParameter("@Price", product.Price));
            cmd.Parameters.Add(new SqlParameter("@Amount", product.Amount));
            cmd.Parameters.Add(new SqlParameter("@Barcode", product.Barcode));
            cmd.Parameters.Add(new SqlParameter("@Image", product.Image));
            cmd.Parameters.Add(new SqlParameter("@ProviderId", product.ProviderId));
            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();
        }

        /// <summary>
        /// Удаление продукта в БД
        /// </summary>
        static public void DeleteProduct(int code)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE Code = @code ", MyConnection);
            cmd.Parameters.Add(new SqlParameter("@code", code));

            MyConnection.Open();
            try { cmd.ExecuteNonQuery(); }
            catch { }
            MyConnection.Close();
        }

        /// <summary>
        /// Изменение продукта в БД
        /// </summary>
        public void EditProduct()
        {
            SqlCommand cmd = new SqlCommand("UPDATE Products SET [Name] = @name, [Price] = @price ," +
           "[Amount] = @amount,[Barcode] = @barcode,[Image] = @image," +
           "[ProviderId] = @providerId WHERE Code = @code ", MyConnection);
            cmd.Parameters.Add(new SqlParameter("@code", product.Code));
            cmd.Parameters.Add(new SqlParameter("@name", product.Name));
            cmd.Parameters.Add(new SqlParameter("@price", product.Price));
            cmd.Parameters.Add(new SqlParameter("@amount", product.Amount));
            cmd.Parameters.Add(new SqlParameter("@barcode", product.Barcode));
            cmd.Parameters.Add(new SqlParameter("@image", product.Image));
            cmd.Parameters.Add(new SqlParameter("@providerId", product.ProviderId));

            MyConnection.Open();
            try { cmd.ExecuteNonQuery(); }
            catch { MessageBox.Show("Не удалось добавить"); }
            MyConnection.Close();
        }

        /// <summary>
        /// Нахождение продукта по id в БД
        /// </summary>
        public void FindProductById(int code)
        {
            SqlCommand selectName = new SqlCommand($"SELECT [Name] FROM  Products WHERE Code = {code} ", MyConnection);
            SqlCommand selectPrice = new SqlCommand($"SELECT [Price] FROM  Products WHERE Code = {code} ", MyConnection);
            SqlCommand selectAmount = new SqlCommand($"SELECT [Amount] FROM  Products WHERE Code = {code} ", MyConnection);
            SqlCommand selectBarcode = new SqlCommand($"SELECT [Barcode] FROM  Products WHERE Code = {code} ", MyConnection);
            SqlCommand selectImage = new SqlCommand($"SELECT [Image] FROM  Products WHERE Code = {code} ", MyConnection);
            SqlCommand selectProviderId = new SqlCommand($"SELECT [ProviderId] FROM  Products WHERE Code = {code} ", MyConnection);

            MyConnection.Open();
            product.Name = selectName.ExecuteScalar().ToString();
            product.Price = (double)selectPrice.ExecuteScalar();
            product.Amount = (int)selectAmount.ExecuteScalar();
            product.Barcode = (int)selectBarcode.ExecuteScalar();
            product.Image = (byte[])selectImage.ExecuteScalar();
            product.ProviderId = (int)selectProviderId.ExecuteScalar();
            MyConnection.Close();
        }

        /// <summary>
        /// Добавить/ восстановить из архива
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isOutdate"> устаревшее или актуальное</param>
        public static void AddArchieve(int code, string isOutdate)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand($"UPDATE Products SET Outdate = '{isOutdate}' WHERE Code = @code ", MyConnection); // команда
            cmd.Parameters.Add(new SqlParameter("@code", code));
            MyConnection.Open();

            cmd.ExecuteNonQuery();

            MyConnection.Close();
        }


        /// <summary>
        /// Получение продуктов определенной категории
        /// </summary>
        /// <param name="parametr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Product[] GetProductCategory(int categoryid, out int count)
        {
            Product[] products;

            products = GetProductsQuery($"Select * from Products where  CategoryId = {categoryid}",
              $"SELECT COUNT(*) FROM Products WHERE  CategoryId = {categoryid}", out count);

            return products;
        }



        /// <summary>
        /// Нахождение продукта по полю
        /// </summary>
        /// <param name="parametr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Product[] FindProductGroup(int categoryid, string sqlParametr, string data, out int count)
        {
            Product[] products;

            //if (sqlParametr == null && data == "")
            //{
            products = GetProductsQuery($"Select * from Products where  CategoryId = {categoryid}",
              $"SELECT COUNT(*) FROM Products WHERE  CategoryId = {categoryid}", out count);
            //}

            //else if (sqlParametr == null)
            //{
            //users = GetProductsQuery($"{queryGetUsers}  AND  like ('%' + @parameter + '%') AND TypeId = {typeid}",
            //  $"SELECT COUNT(*) FROM Users   like ('%' + @parameter + '%') WHERE TypeId = {typeid}", data, out count);
            //}

            //else
            //{
            //          users = GetProductsQuery($"{queryGetUsers}  AND {sqlParametr} like ('%' + @parameter + '%') AND TypeId = {typeid}",
            //$"SELECT COUNT(*) FROM Users WHERE {sqlParametr} like ('%' + @parameter + '%') AND TypeId = {typeid}", data, out count);
            //}

            return products;
        }

        /// <summary>
        /// Получает пользователей по запросу 
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="countQueryString"></param>
        /// <returns> массив обьектов пользователей </returns>
        static public Product[] GetProductsQuery(string queryString, string countQueryString, out int count)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd_query = new SqlCommand(queryString, MyConnection);
            SqlCommand cmd_count = new SqlCommand(countQueryString, MyConnection);

            SqlDataReader reader;
            Product[] products;
            MyConnection.Open();
            try
            {
                count = Convert.ToInt32(cmd_count.ExecuteScalar());
                reader = cmd_query.ExecuteReader();
                products = new Product[count];
                int i = 0;

                if (reader.HasRows == false)
                { products = null; }

                while (reader.Read() && i < count)
                {
                    products[i] = new Product
                    {
                        Code = (int)reader[0],
                        Name = (string)reader[1],
                        Price = (double)reader[2],
                        Amount = (int)reader[3],
                        Barcode = (int)reader[4],
                        Image = (byte[])reader[5],
                        ProviderId = (int)reader[6],
                        Outdate = (string)reader[7]
                    };
                    i++;
                }
            }
            catch
            {
                products = null;
                count = 0;
            }
            MyConnection.Close();
            return products;
        }

        /// <summary>
        /// Получает пользователей по запросу с параметром
        /// </summary>
        static public Product[] GetProductsQuery(string queryString, string countQueryString, string parameter, out int count)
        {
            count = 0;
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd_query = new SqlCommand(queryString, MyConnection);
            SqlCommand cmd_count = new SqlCommand(countQueryString, MyConnection);
            cmd_query.Parameters.Add(new SqlParameter("@parameter", parameter));
            cmd_count.Parameters.Add(new SqlParameter("@parameter", parameter));

            SqlDataReader reader;
            Product[] products;
            MyConnection.Open();

            try
            {
                count = Convert.ToInt32(cmd_count.ExecuteScalar());
                reader = cmd_query.ExecuteReader();
                products = new Product[count];
                int i = 0;

                if (reader.HasRows == false)
                { products = null; }

                while (reader.Read() && i < count)
                {
                    products[i] = new Product
                    {
                        Code = (int)reader[0],
                        Name = (string)reader[1],
                        Price = (double)reader[2],
                        Amount = (int)reader[3],
                        Barcode = (int)reader[4],
                        Image = (byte[])reader[5],
                        ProviderId = (int)reader[6],
                        Outdate = (string)reader[7]
                    };
                    i++;
                }
            }
            catch { products = null; }
            MyConnection.Close();
            return products;
        }

        /// <summary>
        /// Получение продуктов по полю
        /// </summary>
        /// /// <param name="radiobuttonName"> поле по которому поиск</param>
        /// <param name="parametr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Product[] GetProductSearchField(string radiobuttonSearchFieldName, string SearchFieldText, string radiobuttonNameDownUpSelected, out int count)
        {
            string needField;
            // перевод из radiobutton name в sql поле
            switch (radiobuttonSearchFieldName) // проверка радиобатона
            {
                case "NameRb": // если выбрано название
                    needField = "Name";
                    break;

                case "loginRb": // если выбран логин
                    needField = "Login";
                    break;
                default: // по умолчанию фио или логин
                    needField = "Name";
                    break;
            }

            string query;
            string queryCount;

            if (radiobuttonNameDownUpSelected == null)
            {
                query = $"Select * from Products WHERE {needField} like ('%' + @parameter + '%')";
                queryCount = $"SELECT COUNT(*) FROM Products WHERE {needField} like ('%' + @parameter + '%')";
            }

            else if (radiobuttonNameDownUpSelected == "PriceUpRb")
            {

                query = $"Select * from Products WHERE {needField}  like ('%' + @parameter + '%') order by [Price] desc";
                queryCount = $"SELECT COUNT(*) FROM Products WHERE {needField} like ('%' + @parameter + '%') ";
            }

            else
            {
                query = $"Select * from Products  WHERE {needField}  like ('%' + @parameter + '%') order by [Price] asc";
                queryCount = $"SELECT COUNT(*) FROM Products WHERE {needField} like ('%' + @parameter + '%')";
            }


            Product[] products = GetProductsQuery(query, queryCount, SearchFieldText, out count);
            return products;
        }

        /// <summary>
        /// Сортировка даты сначала новые или сначала старые.
        /// </summary>
        static public Product[] SortProductUpDown(string oldOrNew_rb, string searchField)
        {
            Product[] products = null;

            string queryUp;
            string queryUpCount;

            string queryDown;
            string queryDownCount;

            if (searchField == "")
            {
                queryUp = $"Select * from Products  order by [Price] desc";
                queryUpCount = $"SELECT COUNT(*) FROM Products";

                queryDown = $"Select * from Products  order by [Price] asc";
                queryDownCount = $"SELECT COUNT(*) FROM Products";
            }

            else
            {
                queryUp = $"Select * from Products WHERE Name  like ('%' + @parameter + '%') order by [Price] desc";
                queryUpCount = $"SELECT COUNT(*) FROM Products WHERE Name like ('%' + @parameter + '%') ";

                queryDown = $"Select * from Products  WHERE Name  like ('%' + @parameter + '%') order by [Price] asc";
                queryDownCount = $"SELECT COUNT(*) FROM Products WHERE Name like ('%' + @parameter + '%')";
            }

            if (oldOrNew_rb == "PriceUpRb") // сначала дороже
            {
                products = GetProductsQuery(queryUp,
                  queryUpCount, searchField, out int count);
            }

            else if (oldOrNew_rb == "PriceDownRb") // сначала дешевле
            { products = GetProductsQuery(queryDown, queryDownCount, searchField, out int count); }

            return products;
        }

    }
}
