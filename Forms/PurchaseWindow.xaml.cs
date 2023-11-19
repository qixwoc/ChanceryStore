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

namespace ChanceryStore.Forms
{
    /// <summary>
    /// Логика взаимодействия для PurchaseWindow.xaml
    /// </summary>
    public partial class PurchaseWindow : Window
    {
        public ObservableCollection<Purchase> PurchaseObsCol { get; set; }
        int PurchaseId; // выбранная закупка
        public enum States { Actual, Outdate }; // состояния
        States state = States.Actual;
        int count;

        public PurchaseWindow()
        {
            InitializeComponent();
            PurchaseObsCol = new ObservableCollection<Purchase>();
            PurchasesLb.ItemsSource = PurchaseObsCol;
            UpdatePurchase(state);
        }

        // заполнение динамической коллекции пользователями
        private void UpdatePurchase(States state)
        {
            string outdate = "NO";

            if (state == States.Outdate) // если сейчас устаревшее
            { outdate = "YES"; }


            var purchases = Purchase.GetPurchase(outdate, out count);
            PurchaseObsCol.Clear();
            foreach (Purchase p in purchases)
            {
                PurchaseObsCol.Add(p);
            }

            MessageTbl.Text = "количество записей: " + count.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DocumentWindow documentWindow = new DocumentWindow();
            documentWindow.Show();
        }

        private void archieveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addArchieveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectPurchase_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
