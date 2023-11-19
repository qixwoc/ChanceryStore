using ChanceryStore.Forms;
using ChanceryStore.Forms.TestWindow;
using ChanceryStore.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        Login log = new Login();
        User user;

        MenuItem notActive;
        Window activeForm;

        public enum States { Up, Down, Open, Closed };
        States state;

        public AdminForm(User user)
        {
            InitializeComponent();
            notActive = new MenuItem();
            this.user = user;
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();

            // добавление в историю входа
            UserSql.AddLastEntryDateTime(user.Id);
            this.Close();

            Coloring(sender);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Users_Click(object sender, RoutedEventArgs e)
        {
            UsersForm usersForm = new UsersForm();
            usersForm.Owner = this;
            OpenChildForm(usersForm, sender);

            Coloring(sender);
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductsForm productsForm = new ProductsForm();
            OpenChildForm(productsForm, sender);

            Coloring(sender);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();

            Coloring(sender);
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            DocumentWindow documentWindow = new DocumentWindow();
            OpenChildForm(documentWindow, sender);

            Coloring(sender);
        }

        private void Timetable_Click(object sender, RoutedEventArgs e)
        {
            TimetableWindow timetableWindow = new TimetableWindow();
            OpenChildForm(timetableWindow, sender);

            Coloring(sender);
        }

        private void mi1_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow();
            OpenChildForm(userWindow, sender);

            Coloring(sender);
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            OrdersForm ordersForm = new OrdersForm();
            OpenChildForm(ordersForm, sender);

            Coloring(sender);
        }

        private void OpenChildForm(Window childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;

            childForm.Show();
        }

        void Coloring(object sender)
        {
            notActive.Background = new SolidColorBrush(Colors.Transparent);
            notActive.Foreground = new SolidColorBrush(Color.FromRgb(33, 33, 33));

            // активная
            (sender as MenuItem).Background = new SolidColorBrush(Color.FromRgb(159, 195, 167));
            (sender as MenuItem).Foreground = new SolidColorBrush(Colors.White);

            notActive = sender as MenuItem;
        }
    }
}
