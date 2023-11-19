using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

 namespace ChanceryStore
{
    public class User : INotifyPropertyChanged
    {
        int id;
        string firstName;
        string lastName;
        byte[] image;
        string login;
        string password;    
        DateTime lastEntryDateTime;
        DateTime dateOfBirth;       
        string outdate;
        int typeId;

        Type type = new Type();

        string typeName;

        // View обработанные данные
        string lastEntryDateTimeView;
        string ageView;

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

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public byte[] Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public int TypeId
        {
            get { return typeId; }
            set
            {
                typeId = value;
                OnPropertyChanged("TypeId");
            }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public DateTime LastEntryDateTime
        {
            get { return lastEntryDateTime; }
            set
            {
                lastEntryDateTime = value;
                OnPropertyChanged("LastEntryDateTime");
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

        public Type Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public string TypeName
        {
            get { return typeName; }
            set
            {
                typeName = value;
                OnPropertyChanged("TypeName");
            }
        }


        public string LastEntryDateTimeView
        {
            get { return lastEntryDateTimeView; }
            set
            {
                lastEntryDateTimeView = value;
                OnPropertyChanged("LastEntryDateTimeView");
            }
        }
        public string AgeView
        {
            get { return ageView; }
            set
            {
                ageView = value;
                OnPropertyChanged("AgeView");
            }
        }


        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public User()
        {}

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
