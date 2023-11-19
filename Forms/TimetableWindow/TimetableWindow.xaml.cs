using ChanceryStore.Forms.TimetableWindow;
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

namespace ChanceryStore.Forms.TestWindow
{
    /// <summary>
    /// Логика взаимодействия для TimetableWindow.xaml
    /// </summary>
    public partial class TimetableWindow : Window
    {
        System.Windows.Media.Effects.BlurEffect blurEffect = new System.Windows.Media.Effects.BlurEffect(); // инициализация эффекта размытия
        ObservableCollection<Timetable> DayOfWeekObs { get; set; }
        int timetableId; // выбранное расписание


        public TimetableWindow()
        {
            InitializeComponent();
            
            lvMonday.ItemsSource = UpdateTimetable("Monday");
            lvTuesday.ItemsSource = UpdateTimetable("Tuesday");
            lvWednesday.ItemsSource = UpdateTimetable("Wednesday");
            lvThursday.ItemsSource = UpdateTimetable("Thursday");
            lvFriday.ItemsSource = UpdateTimetable("Friday");
            lvSaturday.ItemsSource = UpdateTimetable("Saturday");
            lvSunday.ItemsSource = UpdateTimetable("Sunday");


            if (UserBL.LoggedUser.Type.Name == "админ")
            {
               RemoveBtn.Visibility = Visibility.Visible;
                EmploymentBtn.Visibility = Visibility.Hidden;
            }

            else
            {
                RemoveBtn.Visibility = Visibility.Hidden;
                EmploymentBtn.Visibility = Visibility.Visible;
            }
        }

        // заполнение динамической коллекции расписанием
        private ObservableCollection<Timetable> UpdateTimetable(string dayOfWeek)
        {
            DayOfWeekObs = new ObservableCollection<Timetable>();
            var timetables = Timetable.GetTimetablesView(dayOfWeek);

            DayOfWeekObs.Clear();
            foreach (Timetable t in timetables)
            {
                DayOfWeekObs.Add(t);
            }

            return DayOfWeekObs;
        }

        private void TimeTableReadLV_MouseDown(object sender, MouseButtonEventArgs e)
        {

           
        }

        private void WordBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PdfBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EmploymentBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeTimetableDayWindow employeeTimetableDayWindow = new EmployeeTimetableDayWindow("All");
            employeeTimetableDayWindow.Owner = this;
            employeeTimetableDayWindow.ShowDialog();
        }

        private void MaximiseBtn_Click(object sender, RoutedEventArgs e)
        {
            string day;
            if((sender as Button).Name == "MaximiseBtnMon")
            {
                day = "Monday";
            }

            else if ((sender as Button).Name == "MaximiseBtnMon")
            {
                day = "Tuesday";
            }

            else if ((sender as Button).Name == "MaximiseBtnWednesday")
            {
                day = "Wednesday";
            }

            else if ((sender as Button).Name == "MaximiseBtnThursday")
            {
                day = "Thursday";
            }

            else if ((sender as Button).Name == "MaximiseBtnFriday")
            {
                day = "Friday";
            }


            else if ((sender as Button).Name == "MaximiseBtnSaturday")
            {
                day = "Saturday";
            }

            else
            {
                day = "Sunday";
            }

            TimeTableDayWindow timeTableDayWindow = new TimeTableDayWindow(UpdateTimetable(day));
            timeTableDayWindow.Owner = this;

            BackBlur.Visibility = Visibility.Visible;
            WindowTimetable.Effect = blurEffect;

            if (UserBL.LoggedUser.Type.Name == "админ")
            {
                timeTableDayWindow.RemoveTimetableBtn.Visibility = Visibility.Visible;
                timeTableDayWindow.EditProductBtn.Visibility = Visibility.Visible;
                timeTableDayWindow.AddProductBtn.Visibility = Visibility.Visible;
                timeTableDayWindow.employmentBtn.Visibility = Visibility.Hidden;
            }

            else
            {
                timeTableDayWindow.RemoveTimetableBtn.Visibility = Visibility.Hidden;
                timeTableDayWindow.EditProductBtn.Visibility = Visibility.Hidden;
                timeTableDayWindow.AddProductBtn.Visibility = Visibility.Hidden;
                timeTableDayWindow.employmentBtn.Visibility = Visibility.Visible;
            }


            timeTableDayWindow.ShowDialog();

            if (timeTableDayWindow.DialogResult == false)
            {
                BackBlur.Visibility = Visibility.Hidden;
                WindowTimetable.Effect = null;
            }
        }
    }
}
