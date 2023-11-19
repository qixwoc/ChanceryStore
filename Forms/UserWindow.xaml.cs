using ChanceryStore.models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для BugchalterWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            imageEl.Fill = new ImageBrush { ImageSource = ImageFunc.ConvertByteToBitmap(UserBL.LoggedUser.Image) };
            nameTb.Text = UserBL.LoggedUser.FirstName;
            lastnameTb.Text = UserBL.LoggedUser.LastName;
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Timetable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {

        }



        private void Theme_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string style;
            if ((sender as Shape).Name == "LightTheme")
            {
                style = "Main";
            }

            else
            {
                style = "Dark";
            }

            // определяем путь к файлу ресурсов
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // Добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
