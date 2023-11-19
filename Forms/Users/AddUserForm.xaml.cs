using ChanceryStore.models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUserForm : Window
    {
        string imgPath; // путь к изображению
        string dateOfBirths;
        byte[] imgData = null; // изображение в байтах

        ObservableCollection<Type> TypesObsCol { get; set; }

        public AddUserForm()
        {
            InitializeComponent();
            promt(); //подсказки
            TypesObsCol = new ObservableCollection<Type>();
            combobox(); // заполнение комбобокса
        }

        /// <summary>
        /// подсказки
        /// </summary>
        void promt()
        {
            typePromtLbl.Content = "*";
            firstNamePromtLbl.Content = "*";
            lastNamePromtLbl.Text = "*";
        }

        void combobox()
        {
            var types = Type.GetTypes();
            typeCb.ItemsSource = types;
            typeCb.DisplayMemberPath = "Name";
            typeCb.SelectedValuePath = "Id";
        }

        // обработка ввода
        private int inputProcessing()
        {
            int result = 1;  // флаг все хорошо

            typePromtLbl.Content = "*";
            firstNamePromtLbl.Content = "*";
            lastNamePromtLbl.Text = "*";


            // если не вставлено изображение
            if (imgPath == null)
            { imgPath = "images/notImageUser.jpg"; } // то картинка по умолчанию

            imgData = ImageFunc.ConvertStringToByte(imgPath);

            // если не заполнено поле дата
            if (dateOfBirthDp.Text != "")
            { dateOfBirths = dateOfBirthDp.Text; }

            else { dateOfBirths = "1999-01-02"; }

            // если не заполнено поле роль
            if (typeCb.SelectedValue == null)
            {
                typePromtLbl.Content = "* выберете роль";
                result = 0;
            }

            // если не заполнено поле имя
            if (firstNameTb.Text == "")
            {
                firstNamePromtLbl.Content = "* заполните имя";
                result = 0;
            }

            // если не заполнено поле фамилия
            if (lastNameTb.Text == "")
            {
                lastNamePromtLbl.Text = "* заполните фамилию";
                result = 0;
            }

            if (UserSql.checkLoginHas(loginTb.Text)==false && loginTb.Text != "")
            {
                LoginMessageLabel.Content = "Такой логин не занят";
                LoginMessageGrid.Visibility = Visibility.Collapsed;
                result = 1;
            }

            else {
                LoginMessageGrid.Visibility = Visibility.Visible;
                string loginGenerated1 = UserBL.generateLogin(loginTb.Text);
                string loginGenerated2 = UserBL.generateLogin(loginTb.Text);
                while(loginGenerated1 == loginGenerated2)
                {
                    loginGenerated2 = UserBL.generateLogin(loginTb.Text);
                }
                loginGeneratedLb1.Text = loginGenerated1;
                loginGeneratedLb2.Text = loginGenerated2;
                result = 0;
            }

            return result;
        }

     

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (inputProcessing() == 1)
            { addUser();

                UserBL.SaveFileUser(firstNameTb.Text, lastNameTb.Text, loginTb.Text, passwordTb.Text);
                this.Close(); 
            }
            
        }

        void addUser()
        {
            UserSql userSql = new UserSql();
            userSql.user.FirstName = firstNameTb.Text;
            userSql.user.LastName = lastNameTb.Text;
            userSql.user.Login = loginTb.Text;
            userSql.user.Password = passwordTb.Text;
            userSql.user.Image = imgData;
            userSql.user.DateOfBirth = Convert.ToDateTime(dateOfBirths);
            userSql.user.TypeId = Convert.ToInt32(typeCb.SelectedValue);
            userSql.AddUser();

        }

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
                imageEllipse.Fill = new ImageBrush { ImageSource = ImageFunc.ConvertStringToBitmap(imgPath) };

            }
        }

        private void loginGeneratedLb1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            loginTb.Text = loginGeneratedLb1.Text;
            LoginMessageGrid.Visibility = Visibility.Collapsed;

        }

        private void loginGeneratedLb2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            loginTb.Text = loginGeneratedLb2.Text;
            LoginMessageGrid.Visibility = Visibility.Collapsed;
        }

        private void repeatLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string loginGenerated1 = UserBL.generateLogin(loginTb.Text);
            string loginGenerated2 = UserBL.generateLogin(loginTb.Text);
            while (loginGenerated1 == loginGenerated2)
            {
                loginGenerated2 = UserBL.generateLogin(loginTb.Text);
            }
            loginGeneratedLb1.Text = loginGenerated1;
            loginGeneratedLb2.Text = loginGenerated2;
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
    }
}
