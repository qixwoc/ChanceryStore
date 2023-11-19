
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

namespace ChanceryStore.Forms
{
    /// <summary>
    /// Логика взаимодействия для EditProductForm.xaml
    /// </summary>
    public partial class EditProductForm : Window
    {
        int productId;

        byte[] productImage;
        string imgPath; // путь к изображению
        byte[] imgData = null; // изображение в байтах
        string editOrNot;

        ObservableCollection<Provider> ProvidersObsCol { get; set; }

        public EditProductForm(string editOrNot,int productId, string Name, double Price, int Amount, int Barcode, byte[] Image, int ProviderId)
        {
            this.editOrNot = editOrNot;
            productImage = Image;
            this.productId = productId;

            InitializeComponent();
            ProvidersObsCol = new ObservableCollection<Provider>();
            promt(); // подсказки  
            combobox();

            editOrNotSet();

            set();
            void set(){
                NameTb.Text = Name;
                PriceTb.Text = Price.ToString();
                amountTb.Text = Amount.ToString();
                barcodeTb.Text = Barcode.ToString();
                providerCb.SelectedValue = ProviderId;
                productIm.Source = ImageFunc.ConvertByteToBitmap(Image);

                NameLbl.Content = NameTb.Text;
                PriceLbl.Content = PriceTb.Text;
                AmountLbl.Content = amountTb.Text;
                BarcodeLbl.Content = barcodeTb.Text;
                ProviderLbl.Content = Provider.GetProvider((int)providerCb.SelectedValue).Name;
            }
        }

        void editOrNotSet()
        { if(editOrNot == "Edit")
         {
                // Если режим редактирования
                imageBtn.Visibility = Visibility.Visible;
                editBtn.Visibility = Visibility.Hidden;
                okBtn.Visibility = Visibility.Visible;
                // texbox видимы
                NameTb.Visibility = Visibility.Visible;
                PriceTb.Visibility = Visibility.Visible;
                amountTb.Visibility = Visibility.Visible;
                barcodeTb.Visibility = Visibility.Visible;
                providerCb.Visibility = Visibility.Visible;

                // label невидимы
                NameLbl.Visibility = Visibility.Hidden;
                PriceLbl.Visibility = Visibility.Hidden;
                AmountLbl.Visibility = Visibility.Hidden;
                BarcodeLbl.Visibility = Visibility.Hidden;
                ProviderLbl.Visibility = Visibility.Hidden;

                starPromt1.Visibility = Visibility.Visible;
                starPromt2.Visibility = Visibility.Visible;
            }

        else 
        {
                // Если режим просмотра

                imageBtn.Visibility = Visibility.Hidden;
                editBtn.Visibility = Visibility.Visible;
                okBtn.Visibility = Visibility.Hidden;
                // texbox видимы
                NameTb.Visibility = Visibility.Hidden;
                PriceTb.Visibility = Visibility.Hidden;
                amountTb.Visibility = Visibility.Hidden;
                barcodeTb.Visibility = Visibility.Hidden;
                providerCb.Visibility = Visibility.Hidden;

                // label невидимы
                NameLbl.Visibility = Visibility.Visible;
                PriceLbl.Visibility = Visibility.Visible;
                AmountLbl.Visibility = Visibility.Visible;
                BarcodeLbl.Visibility = Visibility.Visible;
                ProviderLbl.Visibility = Visibility.Visible;

                starPromt1.Visibility = Visibility.Hidden;
                starPromt2.Visibility = Visibility.Hidden;
            }
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

            // если не заполнено название
            if (NameTb.Text == "")
            {
                NamePromtLbl.Visibility = Visibility.Visible;
                result = 0;
            }

            else
            { NamePromtLbl.Visibility = Visibility.Hidden; }

            // если не заполнен штрихкод
            if (barcodeTb.Text == "")
            {
                barcodePromtLbl.Visibility = Visibility.Visible;
                result = 0;
            }

            else
            { barcodePromtLbl.Visibility = Visibility.Hidden; }

            // если не выбран поставщик
            if (providerCb.SelectedValue == null)
            {
                providerPromtLbl.Visibility = Visibility.Visible;
                result = 0;
            }

            else
            { providerPromtLbl.Visibility = Visibility.Hidden; }


            return result;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (inputProcessing() == 1)
            {
                ProductSql productSql = new ProductSql();
                editProduct(productSql);
                this.Close();
            }           
        }

        // редактирование пользователя
        void editProduct(ProductSql productSql)
        {
            productSql.product.Code = productId;
            productSql.product.Name = NameTb.Text;
            productSql.product.Price = Convert.ToDouble(PriceTb.Text);
            productSql.product.Amount = Convert.ToInt32(amountTb.Text);
            productSql.product.Barcode = Convert.ToInt32(barcodeTb.Text);
            productSql.product.ProviderId = Convert.ToInt32(providerCb.SelectedValue);
            
            if (imgPath != null)
            {productSql.product.Image = ImageFunc.ConvertStringToByte(imgPath);}

            else { productSql.product.Image = productImage; }

            productSql.EditProduct();
        }


        // выбрать изображение
        private void imageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выберете изображение";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            
            if (op.ShowDialog() == true)
            {
                imgPath = op.FileName;
                productIm.Source =  ImageFunc.ConvertStringToBitmap(imgPath) ;
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            editOrNot = "Edit";
            editOrNotSet();
        }
    }
}
