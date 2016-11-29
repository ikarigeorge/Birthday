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
        public ObservableCollection<Birthdays> BirthdaysCol { get; set; }

        public MainPage()
        {
            BirthdaysCol = new ObservableCollection<Birthdays>();
            birthdaysDao = new BirthdaysDao();
            InitializeComponent();
        }

        public async void Load(object sender = null, EventArgs e = null)
        {
            this.IsBusy = true;

            try
            {
                var result = await birthdaysDao.GetBirthdays();
                BirthdaysCol.Clear();

                foreach (var i in result)
                {
                    BirthdaysCol.Add(i);
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

        async void OnDelete(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Birthdays birthday = item.CommandParameter as Birthdays;
            if (birthday != null)
            {
                if (await this.DisplayAlert("Delete Entry?",
                    "Are you sure you want to delete the entry '"
                        + birthday.Name + "'?", "Yes", "Cancel") == true)
                {
                    await birthdaysDao.DeleteBirthday(birthday);
                    Load();
                }
            }
        }
    }
}
