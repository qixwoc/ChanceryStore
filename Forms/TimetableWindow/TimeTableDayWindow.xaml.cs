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

namespace ChanceryStore.Forms.TimetableWindow
{
    /// <summary>
    /// Логика взаимодействия для TimeTableDayWindow.xaml
    /// </summary>
    public partial class TimeTableDayWindow : Window
    {
        ObservableCollection<Timetable> DayOfWeekObs;
        public TimeTableDayWindow(ObservableCollection<Timetable> DayOfWeekObs)
        {
            InitializeComponent();
            this.DayOfWeekObs = DayOfWeekObs;
            lvMonday.ItemsSource = DayOfWeekObs;
        }

        int timetableId;
        private void lvMonday_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMonday.SelectedItem is Timetable timetable)
            { timetableId = timetable.Id; }
        }

        private void lvMondayUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lvUsMonday.SelectedItem is Timetable timetable)
            //{ timetableId = timetable.Id; }
        }

        private void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (timetableId == 0)
            {
                MessageBox.Show("выберете");
                return;
            }

            if (MessageBox.Show("Вы уверены что хотите удалить?", "Подтверждение удаления", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                Timetable.DeleteTimetable(timetableId);
                //UpdateProduct(state);
            }
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTimetableWindow addTimetableWindow = new AddTimetableWindow();
            addTimetableWindow.Owner = this;
            addTimetableWindow.ShowDialog();
        }

        private void EditProductBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void employmentBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeTimetableDayWindow employeeTimetableDayWindow = new EmployeeTimetableDayWindow("Monday");
            employeeTimetableDayWindow.Owner = this;
            employeeTimetableDayWindow.ShowDialog();
        }




    }
}
