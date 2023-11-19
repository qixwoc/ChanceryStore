
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
    /// Логика взаимодействия для EditUserForm.xaml
    /// </summary>
    public partial class EditUserForm : Window
    {
        int userId;
        byte[] userImage;
        string imgPath; // путь к изображению
     

        // динамическая коллекция ролей пользователей
        ObservableCollection<Type> Types { get; set; }

        public EditUserForm(int userId, string FirstName, string LastName, string Login, string Password, int TypeId, byte[] image)
        {
            userImage = image;
            this.userId = userId;

            InitializeComponent();
            Types = new ObservableCollection<Type>();
            combobox(); // заполнение комбобокса

            firstNameTb.Text = FirstName;
            lastNameTb.Text = LastName;
            loginTb.Text = Login;
            passwordTb.Text = Password;
            typeCb.SelectedValue = TypeId;

            imageEllipse.Fill = new ImageBrush { ImageSource = ImageFunc.ConvertByteToBitmap(image) };
        }

        void combobox()
        {
            var types = Type.GetTypes();
            typeCb.ItemsSource = types;
            typeCb.DisplayMemberPath = "Name";
            typeCb.SelectedValuePath = "Id";
        }

        // при нажатии ок на форме
        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (inputProcessing() != 1)
            { return; }

            UserSql userSql = new UserSql();
            editUser(userSql);
            
            if(UserBL.LoggedUser.Id == userSql.user.Id)
            { UserBL.LoggedUser = userSql.user; }

            UserBL.SaveFileUser(firstNameTb.Text, lastNameTb.Text, loginTb.Text, passwordTb.Text);

            this.Close();
        }

        // редактирование пользователя
        void editUser(UserSql userSql)
        {
            userSql.user.Id = userId;
            userSql.user.FirstName = firstNameTb.Text;
            userSql.user.LastName = lastNameTb.Text;
            userSql.user.Login = loginTb.Text;
            userSql.user.Password = passwordTb.Text;
            userSql.user.DateOfBirth = dateOfBirthDp.DisplayDate;
            userSql.user.TypeId = Convert.ToInt32(typeCb.SelectedValue);           

            if (imgPath != null)
            { userSql.user.Image = ImageFunc.ConvertStringToByte(imgPath); }

            else { userSql.user.Image = userImage; }

            userSql.EditUser();
        }

        private void imageBtn_Click(object sender, RoutedEventArgs e)
        {        
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Выберете изображение";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {imgPath = op.FileName;
               
                imageEllipse.Fill = new ImageBrush { ImageSource = ImageFunc.ConvertStringToBitmap(imgPath) };
            }      
        }

        // обработка ввода
        private int inputProcessing()
        {
            int result = 1;
            string dateOfBirths;

            // если не заполнено поле
            if (dateOfBirthDp.Text != "")
            { dateOfBirths = dateOfBirthDp.Text; }

            else { dateOfBirths = null; }

            if (typeCb.SelectedValue == null)
            {
                typePromtLbl.Visibility = Visibility.Visible;
                result = 0;
            }

            else
            { typePromtLbl.Visibility = Visibility.Hidden; }

            if (firstNameTb.Text == "")
            {
                firstNamePromtLbl.Visibility = Visibility.Visible;
                result = 0;
            }

            else
            { firstNamePromtLbl.Visibility = Visibility.Hidden; }

            if (lastNameTb.Text == "")
            {
                lastNamePromtLbl.Visibility = Visibility.Visible;
                result = 0;
            }

            else
            { lastNamePromtLbl.Visibility = Visibility.Hidden; }
            return result;
        }

        private void resetPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            passwordTb.Visibility = Visibility.Visible;
            resetPasswordBtn.Visibility = Visibility.Hidden;
        }
    }
}
