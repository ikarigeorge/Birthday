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
        readonly BirthdaysDao birthdaysDao;
        public ObservableCollection<Birthdays> Birthdays { get; set; }

        public MainPage()
        {
            Birthdays = new ObservableCollection<Birthdays>();
            birthdaysDao = new BirthdaysDao();
            InitializeComponent();
        }

        public async void Load(object sender = null, EventArgs e = null)
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

        async void OnAdd(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEdit(this, birthdaysDao));
        }

        async void OnEdit(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEdit(this, birthdaysDao, (Birthdays)e.Item));
        }
    }
}
