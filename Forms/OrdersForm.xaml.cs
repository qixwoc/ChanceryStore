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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class OrdersForm : Window
    {
        ObservableCollection<Order> AllOrdersObc;
        ObservableCollection<Order> CanceledOrdersObc;
        ObservableCollection<Order> CreatedOrdersObc;
        ObservableCollection<Order> InProgressOrdersObcs;
        ObservableCollection<Order> ReadyOrdersObc;
        ObservableCollection<Order> CompletedOrdersObc;

        public enum States { Actual, OutDate }; // состояния
        States state = States.Actual;

        int count; // количесвто записей

        public OrdersForm()
        {
            InitializeComponent();

            AllOrdersObc = new ObservableCollection<Order>();
            CanceledOrdersObc = new ObservableCollection<Order>();
            CreatedOrdersObc = new ObservableCollection<Order>();
            InProgressOrdersObcs = new ObservableCollection<Order>();
            ReadyOrdersObc = new ObservableCollection<Order>();
            CompletedOrdersObc = new ObservableCollection<Order>();


            UpdateOrder();
            AllLb.ItemsSource = AllOrdersObc;
            CanceledLb.ItemsSource = CanceledOrdersObc;
            CreatedLb.ItemsSource = CreatedOrdersObc;
            InProgressLb.ItemsSource = InProgressOrdersObcs;
            ReadyLb.ItemsSource = ReadyOrdersObc;
            CompletedLb.ItemsSource = CompletedOrdersObc;
        }

        private void archieveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        // заполнение динамической коллекции пользователями
        private void UpdateOrder()
        {

            var orders = Order.GetOrders( out count);
            AllOrdersObc.Clear();
            foreach (Order o in orders)
            {
                o.DateTimeView = o.DateTime.ToString("dd.MM.yyyy hh:mm");
                AllOrdersObc.Add(o);
            }

            var ordersCanceled = Order.GetOrders("cancel",out count);
            CanceledOrdersObc.Clear();
            foreach (Order o in ordersCanceled)
            {
                o.DateTimeView = o.DateTime.ToString("dd.MM.yyyy hh:mm");
                CanceledOrdersObc.Add(o);
            }

            var ordersCreated = Order.GetOrders("Created", out count);
            CreatedOrdersObc.Clear();
            foreach (Order o in ordersCreated)
            {
                o.DateTimeView = o.DateTime.ToString("dd.MM.yyyy hh:mm");
                CreatedOrdersObc.Add(o);
            }

            var ordersInProgress = Order.GetOrders("InProgress", out count);
            InProgressOrdersObcs.Clear();
            foreach (Order o in ordersInProgress)
            {
                o.DateTimeView = o.DateTime.ToString("dd.MM.yyyy hh:mm");
                InProgressOrdersObcs.Add(o);
            }

            var ordersReady = Order.GetOrders("Ready", out count);
            ReadyOrdersObc.Clear();
            foreach (Order o in ordersReady)
            {
                o.DateTimeView = o.DateTime.ToString("dd.MM.yyyy hh:mm");
                ReadyOrdersObc.Add(o);
            }

            var ordersCompleted = Order.GetOrders("Completed", out count);
            CompletedOrdersObc.Clear();
            foreach (Order o in ordersCompleted)
            {
                o.DateTimeView = o.DateTime.ToString("dd.MM.yyyy hh:mm");
                CompletedOrdersObc.Add(o);
            }

        }

        private void addArchieveBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
