using BirthdaysReminder.Dao;
using BirthdaysReminder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BirthdaysReminder
{
    public partial class MainPage : ContentPage
    {
        private BirthdaysDao birthdaysDao;
        public ObservableCollection<Birthdays> Birthdays { get; set; }

        public MainPage()
        {
            Birthdays = new ObservableCollection<Birthdays>();
            birthdaysDao = new BirthdaysDao();
            InitializeComponent();
        }

        public async void Load()
        {
            this.IsBusy = true;

            try
            {
                var result = await birthdaysDao.GetBirthdays();
                Birthdays.Clear();

                foreach (var i in result)
                {
                    Birthdays.Add(i);
                }
            }
            finally
            {
                this.IsBusy = false;
            }
            
        }
    }
}
