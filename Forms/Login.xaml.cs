using ChanceryStore.models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            LoginPromt.Visibility = Visibility.Hidden;
            LoginPromtTb.Visibility = Visibility.Hidden;

            registrationPromt.Visibility = Visibility.Hidden;
            registrationPromtTb.Visibility = Visibility.Hidden;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {

            if (loginTb.Text == "" || passwordB.Password == "")
            {
                LoginPromtTb.Text = "Заполните все поля";
                LoginPromt.Visibility = Visibility.Visible;
                LoginPromtTb.Visibility = Visibility.Visible;
                return;
            }

            UserSql userSql = new UserSql(loginTb.Text, passwordTb.Text);


            //admintForm.Show();

            if (userSql.checkPasswordAndLogin() == true) // если верно ведены логин и пароль
            {
                userSql.Data_User("NO");

                // какая роль
                switch (userSql.user.Type.Id)
                {

                    case 1: // менеджер

                        UserBL.LoggedUser = userSql.user;
                        AdminForm adminForm = new AdminForm(userSql.user);
                        adminForm.Owner = this;

                        adminForm.imageMainEl.Fill = new ImageBrush { ImageSource = ImageFunc.ConvertByteToBitmap(userSql.user.Image) };
                        adminForm.FirstNameMainTbl.Text = userSql.user.FirstName;

                        adminForm.Show();
                        this.Hide();
                        break;

                    case 2: // админ

                        UserBL.LoggedUser = userSql.user;
                        AdminForm adminForm2 = new AdminForm(userSql.user);
                        adminForm2.Owner = this;

                        adminForm2.imageMainEl.Fill = new ImageBrush { ImageSource = ImageFunc.ConvertByteToBitmap(userSql.user.Image) };
                        adminForm2.FirstNameMainTbl.Text = userSql.user.FirstName;

                        adminForm2.Show();
                        this.Hide();
                        break;

                    case 3: // бухгалтер

                        UserBL.LoggedUser = userSql.user;
                        AdminForm adminForm3 = new AdminForm(userSql.user);
                        adminForm3.Owner = this;

                        adminForm3.imageMainEl.Fill = new ImageBrush { ImageSource = ImageFunc.ConvertByteToBitmap(userSql.user.Image) };
                        adminForm3.FirstNameMainTbl.Text = userSql.user.FirstName;

                        adminForm3.Show();
                        this.Hide();
                        break;

                }
            }

            else // если пароль неверный
            {
                LoginPromtTb.Text = "Неверный логин или пароль";
                LoginPromt.Visibility = Visibility.Visible;
                LoginPromtTb.Visibility = Visibility.Visible;
            }

        }

        private void registrationPromtBtn_Click(object sender, RoutedEventArgs e)
        {
            if (registrationPromt.Visibility == Visibility.Hidden)
            {
                registrationPromt.Visibility = Visibility.Visible;
                registrationPromtTb.Visibility = Visibility.Visible;
            }
            else if (registrationPromt.Visibility == Visibility.Visible)
            {
                registrationPromt.Visibility = Visibility.Hidden;
                registrationPromtTb.Visibility = Visibility.Hidden;
            }

        }

        private void visibleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (passwordB.Visibility == Visibility.Visible)
            {
                passwordB.Visibility = Visibility.Hidden;
                passwordTb.Visibility = Visibility.Visible;
            }

            else if (passwordB.Visibility == Visibility.Hidden)
            {
                passwordTb.Visibility = Visibility.Hidden;
                passwordB.Visibility = Visibility.Visible;
            }


        }

        private void passwordB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passwordTb.Text = passwordB.Password;
        }

        private void passwordTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordB.Password = passwordTb.Text;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        /// кнопки меню верхнего
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClosed_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximized_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            { WindowState = WindowState.Normal;
                btnMaximized.Content = "◻";
            }
            else if(WindowState == WindowState.Normal)
            { WindowState = WindowState.Maximized;
                btnMaximized.Content = "❐";
            }
           
        }


    }
}
