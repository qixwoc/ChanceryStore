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

namespace ChanceryStore.Forms.TimetableWindow
{
    /// <summary>
    /// Логика взаимодействия для AddTimetableWindow.xaml
    /// </summary>
    public partial class AddTimetableWindow : Window
    {
        public AddTimetableWindow()
        {
            InitializeComponent();

            var users = UserSql.GetUsers("NO", out int count);
            usersCb.ItemsSource = users;
            usersCb.SelectedValuePath = "Id";

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
             DateTime timeStartView = Convert.ToDateTime("05.06.2023 " + TimeStartTb.Text + ":00:00");
             DateTime timeEndView = Convert.ToDateTime("05.06.2023 " + TimeEndTb.Text + ":00:00");


                Timetable timetable = new Timetable
            {
                TimeStart = Convert.ToDateTime(timeStartView),
                TimeEnd = Convert.ToDateTime(timeEndView),
                UserId = Convert.ToInt32(usersCb.SelectedValue)
            };

            timetable.AddTimetable();
            this.Close();
        }

        private void TimeStartTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<User> usl = new List<User>();
            

            var users = UserSql.GetUsers("NO", out int count);

            if(TimeStartTb.Text !="" || TimeEndTb.Text != "") { 
            DateTime timeStartView = Convert.ToDateTime("05.06.2023 " + TimeStartTb.Text + ":00:00");
            DateTime timeEndView = Convert.ToDateTime("05.06.2023 " + TimeEndTb.Text + ":00:00");
            

            foreach (User u in users)
            {
                if (!Timetable.CheckTimetable(timeStartView, timeEndView, u.Id))
                {
                    usl.Add(u);
                    
                }
            }
            }

            usersCb.ItemsSource = usl;

            usersCb.SelectedValuePath = "Id";
        }
    }
}
