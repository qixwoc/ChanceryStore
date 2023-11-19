using ChanceryStore.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChanceryStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class UsersForm : Window
    {
        ObservableCollection<User> UsersObsCol { get; set; }
        BlurEffect blurEffect = new BlurEffect(); // инициализация эффекта размытия
        int userId; // выбранный пользователь
        public enum States {Actual, OutDate }; // состояния
        States state =  States.Actual;
        int count; // количесвто записей

        public UsersForm()
        {
           InitializeComponent();

            // инициализация коллекции
            UsersObsCol = new ObservableCollection<User>();
            UsersLb.ItemsSource = UsersObsCol;

            // нажатиие вкладки актуальные
            archieveBtn_Click(actualBtn, null);

            // скратие фильтра
            GridFiltr.Visibility = Visibility.Hidden;

            // заполнение комбобокса
            var type = Type.GetTypes();
            TypeCb.ItemsSource = type;
            TypeCb.DisplayMemberPath = "Name";
            TypeCb.SelectedValuePath = "Id";
        }

        // заполнение динамической коллекции пользователями
        private void UpdateUser(States state)
        {
            string outdate = "NO";

            if (state == States.OutDate) // если сейчас устаревшее
            {outdate = "YES"; }

            
            var users = UserSql.GetUsers(outdate,out count);
                UsersObsCol.Clear();
                foreach (User u in users)
                {
                    UsersObsCol.Add(u);
                }

            MessageTbl.Text = "количество пользователей: " + count.ToString();
        }

        // при выборе пользователя радиобатон
        private void SelectUser_Click(object sender, RoutedEventArgs e)
        {
            (sender as RadioButton).IsChecked = true;
            userId = Convert.ToInt32((sender as RadioButton).Tag);
        }

        // при нажатии добавить пользователя
        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm(); 
            addUserForm.Owner = this;

            BackBlur.Visibility = Visibility.Visible;
            WindowUsers.Effect = blurEffect;           

            addUserForm.ShowDialog();

            if (addUserForm.DialogResult == false)
            {
                BackBlur.Visibility = Visibility.Hidden;
                WindowUsers.Effect = null;
            }
            UpdateUser(state);
        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (userId == 0)
            {
                MessageBox.Show("выберете");
                return;
            }

            if (MessageBox.Show("Вы уверены что хотите удалить?", "Подтверждение удаления", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                UserSql.DeleteUser(userId);
                UpdateUser(state);
            }
        }


        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (userId == 0)
            {
                MessageBox.Show("выберете");
                return;
            }

            UserSql userSql = new UserSql();
            userSql.FindUserById(userId);

            EditUserForm editUserForm = new EditUserForm(userId, userSql.user.FirstName, userSql.user.LastName, userSql.user.Login, userSql.user.Password,
            userSql.user.TypeId, userSql.user.Image);
            editUserForm.Owner = this;
            BackBlur.Visibility = Visibility.Visible;
            WindowUsers.Effect = blurEffect;
            editUserForm.ShowDialog();

            if (editUserForm.DialogResult == false)
            {
                BackBlur.Visibility = Visibility.Hidden;
                WindowUsers.Effect = null;
            }

            UpdateUser(state);
        }

        private void serBtn_Copy_Click(object sender, RoutedEventArgs e)
        {  
        }


        // при нажатии на кнопку Добавление/ восстановление из архива
        private void addArchieveBtn_Click(object sender, RoutedEventArgs e)
        {           
            if(state == States.OutDate) // если сейчас устаревшее
            {
                UserSql.AddArchieve(Convert.ToInt32((sender as Button).Tag), "NO"); // восстанавливаем выбранный из архива
            }

            else if (state == States.Actual) // если сейчас актуальное
            {
                UserSql.AddArchieve(Convert.ToInt32((sender as Button).Tag), "YES"); // добавляем выбранный в архив
                
            }
            UpdateUser(state);  // обновляем 
        }

        // при нажатии на вкладку актульное/ архив
        private void archieveBtn_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button).Name == "actualBtn")  // при нажатии на вкладку актульное
            {
                state = States.Actual;
                actualBtn.Background = new SolidColorBrush(Color.FromRgb(110, 167, 139));
                actualBtn.Foreground = new SolidColorBrush(Colors.White);
                
                outdateBtn.Background = new SolidColorBrush(Color.FromRgb(187, 230, 209));
                outdateBtn.Foreground = new SolidColorBrush(Color.FromRgb(61, 107, 85));
            }
            else if((sender as Button).Name == "outdateBtn") // при нажатии на вкладку архив
            { state = States.OutDate;
                outdateBtn.Background = new SolidColorBrush(Color.FromRgb(110, 167, 139));
                outdateBtn.Foreground = new SolidColorBrush(Colors.White);

                actualBtn.Background = new SolidColorBrush(Color.FromRgb(187, 230, 209));
                actualBtn.Foreground = new SolidColorBrush(Color.FromRgb(61, 107, 85));
            }
            UpdateUser(state);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(GridFiltr.Visibility == Visibility.Hidden)
            GridFiltr.Visibility = Visibility.Visible;
            else if (GridFiltr.Visibility == Visibility.Visible)
            GridFiltr.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Текст radiobuttonа
        /// </summary>
        string radiobuttonName;

       
        string fullName;
        public string FullName { get { return fullName; } set { fullName = value; } }

        /// <summary>
        /// При нажатии переключателя услуг
        /// </summary>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            radiobuttonName = radioButton.Name.ToString();
        }

        /// <summary>
        ///  При выборе должности сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Поисковое поле
            if (UserSql.GetUserSearchField(radiobuttonName, findUserTb.Text, out count) != null)
            {

                UsersObsCol.Clear();
                
                var users = UserSql.FindUserGroup(TypeCb.SelectedValue.ToString(), radiobuttonName, findUserTb.Text,  out count);

                foreach (User u in users)
                {
                    UsersObsCol.Add(u);
                }
            }
            else
            {
                UsersObsCol.Clear();
                MessageTbl.Text = "Не найдено";
            }

            MessageTbl.Text = "количество пользователей: " + count.ToString();
        }


        /// <summary>
        /// При нажатии кнопки найти пациента
        /// </summary>
        private void findBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void findUserTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (findUserTb.Text == "")
            {
                UpdateUser(state);
                return;
            }

            var users = UserSql.GetUserSearchField(radiobuttonName, findUserTb.Text, out count);
            // Поисковое поле
            if (users != null)
            {
                UsersObsCol.Clear();
                foreach (User u in users)
                {UsersObsCol.Add(u);}

                MessageTbl.Text = "Количество пользователей: " + count.ToString();
            }
            else
            {
                UsersObsCol.Clear();
                MessageTbl.Text = "Не найдено";
            }
        }

        private void rbAgeNew_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            string oldOrNewRbName = radioButton.Name.ToString();

            UsersObsCol.Clear();
            var users = UserSql.SortDateNew(oldOrNewRbName);
            foreach (User u in users)
            {
                UsersObsCol.Add(u);
            }
        }


        // при выборе продукта выделение

        private void UsersLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersLb.SelectedItem is User user)
            { userId = user.Id; }
        }
    }
}
