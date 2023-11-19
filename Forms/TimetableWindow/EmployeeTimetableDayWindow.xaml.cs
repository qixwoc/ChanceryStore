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
    /// Логика взаимодействия для ShopAssistantTimetableWindow.xaml
    /// </summary>
    public partial class EmployeeTimetableDayWindow : Window
    {
        public EmployeeTimetableDayWindow(string dayOfWeek)
        {
            InitializeComponent();
            if(dayOfWeek == "Monday")
            { selectDateDp.Visibility = Visibility.Hidden;
              dayOfWeekLbl.Visibility = Visibility.Visible;
            }

            else if(dayOfWeek == "All")
            { selectDateDp.Visibility = Visibility.Visible;
              dayOfWeekLbl.Visibility = Visibility.Hidden;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TimeStartTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EmploymentBtn_Click(object sender, RoutedEventArgs e)
        {
            Timetable timetable = new Timetable();
            timetable.DateStartWeek =  Convert.ToDateTime("05.06.2023 " + TimeStartTb.Text + ":00:00");
            timetable.DateEndWeek = Convert.ToDateTime("05.06.2023 " + TimeEndTb.Text + ":00:00");
            timetable.UserId = UserBL.LoggedUser.Id;
            timetable.AddTimetableEmployment();
            MessageBox.Show("Временной промежуток отмечен как занятый");
        }
    }
}
