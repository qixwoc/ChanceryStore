using ChanceryStore.models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для AddProductForm.xaml
    /// </summary>
    public partial class AddProductForm : Window
    {
        string imgPath; // путь к изображению
        string dateOfBirths;
        byte[] imgData = null; // изображение в байтах

        ObservableCollection<Provider> ProvidersObsCol { get; set; }

        public AddProductForm()
        {

            InitializeComponent();
            
            promt(); // подсказки
            ProvidersObsCol = new ObservableCollection<Provider>();
            combobox();

        }

        void promt()
        {
            providerPromtLbl.Content = "выберете роль";
            NamePromtLbl.Content = "заполните имя";
            barcodePromtLbl.Content = "заполните штрихкод";

            providerPromtLbl.Visibility = Visibility.Hidden;
            NamePromtLbl.Visibility = Visibility.Hidden;
            barcodePromtLbl.Visibility = Visibility.Hidden;
        }

        void combobox()
        {
            var providers = Provider.GetProviders();
            providerCb.ItemsSource = providers;
            providerCb.DisplayMemberPath = "Name";
            providerCb.SelectedValuePath = "Id";
        }

        // обработка ввода
        private int inputProcessing()
        {
            int result = 1; // флаг все хорошо

            providerPromtLbl.Visibility = Visibility.Hidden;
            NamePromtLbl.Visibility = Visibility.Hidden;
            barcodePromtLbl.Visibility = Visibility.Hidden;

            if (imgPath == null) // если не вставлено изображение
            { imgPath = "images/pencilProduct.png"; } // то картинка по умолчанию

            imgData = ImageFunc.ConvertStringToByte(imgPath);

            // если не заполнено название
            if (NameTb.Text == "")
            {
                NamePromtLbl.Visibility = Visibility.Visible;
                result = 0;
            }

            // если не заполнен штрихкод
            if (barcodeTb.Text == "")
            {
                barcodePromtLbl.Visibility = Visibility.Visible;
                result = 0;
            }

            // если не выбран поставщик
            if (providerCb.SelectedValue == null)
            {
                providerPromtLbl.Visibility = Visibility.Visible;
                result = 0;
            }

            return result;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (inputProcessing() == 1)
            { addProduct(); }
            this.Close();
        }

        void addProduct()
        {
            ProductSql productSql = new ProductSql();
             productSql.product.Name = NameTb.Text;
            productSql.product.Price = Convert.ToDouble(PriceTb.Text);
            productSql.product.Amount = Convert.ToInt32(AmountTb.Text);
            productSql.product.Barcode = Convert.ToInt32(barcodeTb.Text);
            productSql.product.Image = imgData;
            productSql.product.ProviderId = Convert.ToInt32(providerCb.SelectedValue);
            productSql.AddProduct();
        }


        private void imageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPath = op.FileName;
                {

                    imageProduct.Source = ImageFunc.ConvertStringToBitmap(imgPath);
                }
            }
        }
    }
}
