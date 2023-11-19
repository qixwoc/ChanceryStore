using ChanceryStore.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChanceryStore
{
    public class Product: INotifyPropertyChanged
    {
        private int code;
        private string name;
        private double price;
        private int amount;
        private int barcode;
        private string outdate;
        private byte[] image;
        private int providerId;
        private int categoryId;

        public Provider Provider { get; set; }
        public Category Category { get; set; }

        [Key]
        public int Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public int Barcode
        {
            get { return barcode; }
            set
            {
                barcode = value;
                OnPropertyChanged("Barcode");
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

        public byte[] Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public int ProviderId
        {
            get { return providerId; }
            set
            {
                providerId = value;
                OnPropertyChanged("ProviderId");
            }
        }

        public int CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

       

    }
}
