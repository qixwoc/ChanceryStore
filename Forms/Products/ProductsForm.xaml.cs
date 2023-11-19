using ChanceryStore.Forms;
using ChanceryStore.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ChanceryStore
{
    /// <summary>
    /// Логика взаимодействия для Stationery.xaml
    /// </summary>
    public partial class ProductsForm : Window
    {
        public ObservableCollection<Product> ProductsObsCol { get; set; }
        System.Windows.Media.Effects.BlurEffect blurEffect = new System.Windows.Media.Effects.BlurEffect(); // инициализация эффекта размытия
        int productId; // выбранный продукт
        public enum States { Up, Down, Actual, OutDate }; // состояния
        States state = States.Actual;
        /// <summary>
        /// количество записей 
        /// </summary>
        int count;

        /// <summary>
        /// Имя выбранного radiobuttonа
        /// </summary>
        string radiobuttonNameSelected;


        public ProductsForm()
        {
            InitializeComponent();
            ProductsObsCol = new ObservableCollection<Product>();
            ProductsLb.ItemsSource = ProductsObsCol;
            archieveBtn_Click(actualBtn, null);

            if(Purchase.ProductsForPurchaseObsColToPur.Count > 1)
            { GoPurchaseBtn.Visibility = Visibility.Visible;
                textPur.Content = "В закупках " + Purchase.ProductsForPurchaseObsColToPur.Count + " продуктов";
                textPur.Visibility = Visibility.Visible;
            }
        }

        // заполнение динамической коллекции пользователями
        private void UpdateProduct(States state)
        {
            string outdate = "NO";

            if (state == States.OutDate) // если сейчас устаревшее
            {
                outdate = "YES";
            }

            var products = ProductSql.GetProducts(outdate); // получение списка данных из бд
            ProductsObsCol.Clear();
            foreach (Product u in products)
            {
                ProductsObsCol.Add(u);
            }

            MessageTbl.Text = "количество товаров: " + ProductsObsCol.Count.ToString();
        }

        // при выборе продукта выделение
        private void ProductsLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsLb.SelectedItem is Product product)
            { productId = product.Code; }
        }

        // при нажатии добавить продукт
        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm();
            addProductForm.Owner = this;

            BackBlur.Visibility = Visibility.Visible;
            WindowProducts.Effect = blurEffect;          

            addProductForm.ShowDialog();

            if (addProductForm.DialogResult == false)
            {
                BackBlur.Visibility = Visibility.Hidden;
                WindowProducts.Effect = null;
            }
            UpdateProduct(state);
        }
        // при нажатии удалить продукт
        private void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (productId == 0)
            {
                MessageBox.Show("Выберете товар");
                return;
            }

            if (MessageBox.Show("Вы уверены что хотите удалить?", "Подтверждение удаления", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                ProductSql.DeleteProduct(productId);
                UpdateProduct(state);
            }
        }
        // при нажатии изменить продукт
        private void EditProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (productId == 0)
            {
                MessageBox.Show("Выберете товар");
                return;
            }

            string editOrNot = (sender as Button).Tag.ToString();

            ProductSql productSql = new ProductSql();
            productSql.FindProductById(productId);

            EditProductForm editProductForm = new EditProductForm(editOrNot, productId, productSql.product.Name, productSql.product.Price, productSql.product.Amount, productSql.product.Barcode, productSql.product.Image, productSql.product.ProviderId);
            editProductForm.Owner = this;
            BackBlur.Visibility = Visibility.Visible;
            WindowProducts.Effect = blurEffect;
            editProductForm.ShowDialog();

            if (editProductForm.DialogResult == false)
            {
                BackBlur.Visibility = Visibility.Hidden;
                WindowProducts.Effect = null;
            }

            UpdateProduct(state);
        }

        // при нажатии на вкладку актульное/ архив
        private void archieveBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Name == "actualBtn") // при нажатии на вкладку актульное
            {
                state = States.Actual;
                actualBtn.Background = new SolidColorBrush(Color.FromRgb(110, 167, 139));
                actualBtn.Foreground = new SolidColorBrush(Colors.White);

                outdateBtn.Background = new SolidColorBrush(Color.FromRgb(187, 230, 209));
                outdateBtn.Foreground = new SolidColorBrush(Color.FromRgb(61, 107, 85));
            }
            else if ((sender as Button).Name == "outdateBtn") // при нажатии на вкладку архив
            { state = States.OutDate;
                outdateBtn.Background = new SolidColorBrush(Color.FromRgb(110, 167, 139));
                outdateBtn.Foreground = new SolidColorBrush(Colors.White);

                actualBtn.Background = new SolidColorBrush(Color.FromRgb(187, 230, 209));
                actualBtn.Foreground = new SolidColorBrush(Color.FromRgb(61, 107, 85));
            }
            UpdateProduct(state);
        }

        // при нажатии на кнопку Добавление/ восстановление из архива
        private void addArchieveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (state == States.OutDate) // если сейчас устаревшее
            {
                ProductSql.AddArchieve(Convert.ToInt32((sender as Button).Tag), "NO"); // восстанавливаем выбранный из архива
            }

            else if (state == States.Actual) // если сейчас актуальное
            {
                ProductSql.AddArchieve(Convert.ToInt32((sender as Button).Tag), "YES"); // добавляем выбранный в архив

            }
            UpdateProduct(state);  // обновляем 
        }


        // при выборе радиобатона на поисковое поле в фильтре
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            //if (radioButton.Sel == true)
            //{
            //    radioButton.IsChecked = false;
            //radiobuttonNameSelected = null;
            //}

            //else if(radioButton.IsChecked == false)
            // {
            //    radioButton.IsChecked = true;
            

            radiobuttonNameSelected = radioButton.Name.ToString();
            //}
        }

        string radiobuttonNameDownUpSelected;

        // при выборе радиобатона на сортировку в фильтре
        private void PriceRb_Checked(object sender, RoutedEventArgs e)
        {
             radiobuttonNameDownUpSelected = (sender as RadioButton).Name;
            ProductsObsCol.Clear();
            var products = ProductSql.SortProductUpDown(radiobuttonNameDownUpSelected, SearchFieldTb.Text);


            foreach (Product p in products)
            {
                ProductsObsCol.Add(p);
            }
        }

        // при набирании текста в поисковом поле
        private void SearchFieldTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchFieldTb.Text == "")
            {
                UpdateProduct(state);
                return;
            }

            var products = ProductSql.GetProductSearchField(radiobuttonNameSelected, SearchFieldTb.Text, radiobuttonNameDownUpSelected, out count);
            // Поисковое поле
            if (products != null)
            {
                ProductsObsCol.Clear();
                foreach (Product p in products)
                { ProductsObsCol.Add(p); }

                MessageTbl.Text = "Количество продуктов: " + count.ToString();
            }
            else
            {
                ProductsObsCol.Clear();
                MessageTbl.Text = "Не найдено";
            }
        }

        // при нажатии на кнопку фильтр
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GridFiltr.Visibility == Visibility.Hidden)
                GridFiltr.Visibility = Visibility.Visible;
            else if (GridFiltr.Visibility == Visibility.Visible)
                GridFiltr.Visibility = Visibility.Hidden;
        }

        // при нажатии на кнопку Отправить в закупку
        private void SendPurchaseBtn_Click(object sender, RoutedEventArgs e)
        { 
          // Продукт для отправки
            Product productForPurchase = ProductSql.GetProduct(productId);
            // Закупка для таблицы окна Закупки
            Purchase PurchaseToDg = new Purchase();
            PurchaseToDg.Product.Code = productForPurchase.Code;
            PurchaseToDg.Product.Name = productForPurchase.Name;
            PurchaseToDg.Product.Price = productForPurchase.Price;
            PurchaseToDg.ProductId = productForPurchase.Code;
            PurchaseToDg.Amount = 1;

            // коллекция закупок
            Purchase.ProductsForPurchaseObsColToPur.Add(PurchaseToDg);

            // появление кнопки и теста, когда в закупках появляются товары
            GoPurchaseBtn.Visibility = Visibility.Visible;
            textPur.Content = "В закупках " + Purchase.ProductsForPurchaseObsColToPur.Count + " продуктов";
            textPur.Visibility = Visibility.Visible;
        }


        // при нажатии на перейти к закупкам
        private void GoPurchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            DocumentWindow documentForm = new DocumentWindow();
            documentForm.Owner = this;
            documentForm.ShowDialog();
        }

        // при нажатии на категорию
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            string CategoryNameSelected = ((TreeViewItem)sender).Header.ToString();

            if(CategoryNameSelected == "Все")
            { UpdateProduct(state);
                return;
            }

            CategoryNameSelected = WordFunc.FirstLetterUpperToLower(CategoryNameSelected);

            // Поисковое поле
            if (ProductSql.GetProductCategory((int)Category.GetCategoryIdFromName(CategoryNameSelected),  out count) != null)
            {
                ProductsObsCol.Clear();

                var products = ProductSql.GetProductCategory((int)Category.GetCategoryIdFromName(CategoryNameSelected),  out count);

                foreach (Product p in products)
                {ProductsObsCol.Add(p);}

                MessageTbl.Text = "количество товаров: " + count.ToString();
            }

            else
            {
                ProductsObsCol.Clear();
                MessageTbl.Text = "Не найдено";
            }
        }

    }
}
