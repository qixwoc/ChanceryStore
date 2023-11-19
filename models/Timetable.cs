using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceryStore.models
{
    public class Timetable : INotifyPropertyChanged
    {
        int id;
        DateTime timeStart;
        DateTime timeEnd;
        List<User> usersOneTimeView;
        string outdate;
        int userId;

        User user = new User();

        // view
        string timeStartView;
        string timeEndView;


        // для удобства
        DateTime dateStartWeek;
        DateTime dateEndWeek;
        string dayOfWeek;

        // Свойства
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public DateTime TimeStart
        {
            get { return timeStart; }
            set
            {
                timeStart = value;
                OnPropertyChanged("TimeStart");
            }
        }
        public DateTime TimeEnd
        {
            get { return timeEnd; }
            set
            {
                timeEnd = value;
                OnPropertyChanged("TimeEnd");
            }
        }
        public DateTime DateStartWeek
        {
            get { return dateStartWeek; }
            set
            {
                dateStartWeek = value;
                OnPropertyChanged("DateStartWeek");
            }
        }
        public DateTime DateEndWeek
        {
            get { return dateEndWeek; }
            set
            {
                dateEndWeek = value;
                OnPropertyChanged("DateEndWeek");
            }
        }
        public string DayOfWeek
        {
            get { return dayOfWeek; }
            set
            {
                dayOfWeek = value;
                OnPropertyChanged("DayOfWeek");
            }
        }
        public string Outdate
        {
            get { return outdate; }
            set
            {
                outdate = value;
                OnPropertyChanged("Outdate");
            }
        }
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }
        
        public List<User> UsersOneTimeView
        {
            get { return usersOneTimeView; }
            set
            {
                usersOneTimeView = value;
                OnPropertyChanged("UsersOneTimeView");
            }
        }
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }


        public string TimeStartView
        {
            get { return timeStartView; }
            set
            {
                timeStartView = value;
                OnPropertyChanged("TimeStartView");
            }
        }
        public string TimeEndView
        {
            get { return timeEndView; }
            set
            {
                timeEndView = value;
                OnPropertyChanged("TimeEndView");
            }
        }

        /// <summary>
        /// Получить все расписания из бд
        /// </summary>
        /// <returns></returns>
        static public Timetable[] GetTimetables()
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            Timetable[] timetables;
            SqlDataReader reader;
            SqlCommand select_values = new SqlCommand("SELECT Id, TimeStart,TimeEnd,Outdate,UserId FROM Timetable ", MyConnection);
            SqlCommand select_count = new SqlCommand("Select Count(*) FROM Timetable", MyConnection);

            int count = 0;
            MyConnection.Open();
            try
            {
                count = Convert.ToInt32(select_count.ExecuteScalar());
                timetables = new Timetable[count];
                reader = select_values.ExecuteReader();
                int i = 0;
                while (reader.Read() && i < count)
                {
                    timetables[i] = new Timetable();
                    timetables[i].Id = (int)reader[0];
                    timetables[i].TimeStart = (DateTime)reader[1];
                    timetables[i].TimeEnd = (DateTime)reader[2];
                    timetables[i].Outdate = (string)reader[3];
                    timetables[i].UserId = (int)reader[4];

                    timetables[i].TimeStartView = ((DateTime)reader[1]).ToString("hh:mm");
                    timetables[i].TimeEndView = ((DateTime)reader[2]).ToString("hh:mm");

                    timetables[i].DayOfWeek = ((DateTime)reader[1]).DayOfWeek.ToString();
                    i++;
                }
            }
            catch { timetables = null; }
            MyConnection.Close();
            return timetables;
        }

        /// <summary>
        /// Получить расписания View 
        /// </summary>
        /// <returns></returns>
        static public List<Timetable> GetTimetablesView(string dayOfWeek)
        {
            List<Timetable> timetablesView = new List<Timetable>(); // расписание для дня недели 
            Timetable[] timetables = GetTimetables(); // все расписания
            
            int i = 0;
            try
            {
                List<DateTime> was = new List<DateTime>();
                foreach (Timetable t in timetables) // перебор всех расписаний
                {
                    
                    List<User> selectedUsersOneTime = new List<User>(); // список работников на одно время

                    if (t.DayOfWeek == dayOfWeek) // если дата равна дню недели в параметре
                    {// то добавить всех работников на эту дату
                        if (!was.Contains(t.TimeStart))
                        { 
                        foreach (Timetable t1 in timetables) 
                        {
                            if (t.TimeStart == t1.TimeStart ) 
                            {
                                t1.User = UserSql.GetUser(t1.UserId);
                                selectedUsersOneTime.Add(t1.User);
                                was.Add(t.TimeStart);
                            }
                        }
                        

                        timetablesView.Add(t);

                    timetablesView[i].TimeStart = t.timeStart;
                    timetablesView[i].timeEnd = t.timeEnd;
                    timetablesView[i].usersOneTimeView = selectedUsersOneTime;
                    i++;

                        }
                    }
            }
        }
            catch { timetablesView = null; }
            return timetablesView;
        }

        /// <summary>
        /// Удаление продукта в БД
        /// </summary>
        static public void DeleteTimetable(int id)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

            SqlCommand cmd = new SqlCommand("DELETE FROM Timetable WHERE Id = @id ", MyConnection);
            cmd.Parameters.Add(new SqlParameter("@id", id));

            MyConnection.Open();
            try { cmd.ExecuteNonQuery(); }
            catch { }
            MyConnection.Close();
        }
        SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);

        /// <summary>
        /// Добавление расписания в БД
        /// </summary>
        public void AddTimetable()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Timetable ([TimeStart]," +
                   "[TimeEnd] ,[UserId]) "
                   + "values( @TimeStart,@TimeEnd,@UserId)", MyConnection);
            cmd.Parameters.Add(new SqlParameter("@TimeStart", TimeStart));
            cmd.Parameters.Add(new SqlParameter("@TimeEnd", TimeEnd));
            cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();
        }

        /// <summary>
        /// Добавление расписания в БД
        /// </summary>
        public void AddTimetableEmployment()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO EmploymentTimetable ([TimeStart]," +
                   "[TimeEnd] ,[UserId]) "
                   + "values( @TimeStart,@TimeEnd,@UserId)", MyConnection);
            cmd.Parameters.Add(new SqlParameter("@TimeStart", TimeStart));
            cmd.Parameters.Add(new SqlParameter("@TimeEnd", TimeEnd));
            cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
            MyConnection.Open();
            cmd.ExecuteNonQuery();
            MyConnection.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TimeStart"></param>
        /// <param name="TimeEnd"></param>
        /// <param name="UserId"></param>
        /// <returns>занят true, свободен false</returns>
        static public bool CheckTimetable(DateTime TimeStart, DateTime TimeEnd, int UserId)
        {
            SqlConnection MyConnection = new SqlConnection(Connection.ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM EmploymentTimetable WHERE TimeStart = @TimeStart AND TimeEnd = TimeEnd AND UserId = @UserId", MyConnection);
            cmd.Parameters.Add(new SqlParameter("@TimeStart", TimeStart));
            cmd.Parameters.Add(new SqlParameter("@TimeEnd", TimeEnd));
            cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
            MyConnection.Open();

            if (Convert.ToInt32(cmd.ExecuteScalar()) >= 1)
            { return true; } 
            MyConnection.Close();
            return false;
        }


        
        /// <summary>
        /// Получить расписание View 
        /// </summary>
        /// <returns></returns>
        //static public List<Timetable> GetTimetableView(Timetable timetablesView)
        //{
        //    foreach (Timetable t in timetablesView) // перебор всех расписаний
        //    {
        //        dateDay = t.TimeStartView;

        //    }

        //        return timetablesView;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
