using BirthdaysReminder.Dao;
using BirthdaysReminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BirthdaysReminder
{
    public partial class AddEdit : ContentPage
    {

        public string PageTitle { get; set; }
        public string ButtonText { get; set; }
        public Birthdays Item { get; set; }
        private bool existingBirthday { get; set; }
        private MainPage mainPage;
        readonly BirthdaysDao birthdaysDao;

        public AddEdit(MainPage sender, BirthdaysDao birthdaysDao, Birthdays birthday = null)
        {
            mainPage = sender;
            this.birthdaysDao = birthdaysDao;
            existingBirthday = birthday != null;
            Item = existingBirthday ? birthday : new Birthdays();
            if (!existingBirthday)
            {
            Item.Birthday = DateTime.Now;
            }
            PageTitle = existingBirthday ? "Edit" : "Add";
            ButtonText = existingBirthday ? "Save Changes" : "Save";
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            this.IsBusy = true;
            try
            { 
                if (string.IsNullOrWhiteSpace(Item.Name))
                {
                    this.IsBusy = false;
                    await this.DisplayAlert("Missing Information",
                        "You must enter values for the Name and Date.",
                        "OK");
                }
                else
                {
                    if (existingBirthday)
                    {
                        await birthdaysDao.UpdateBirthday(Item);
                    }
                    else
                    {
                        await birthdaysDao.AddBirthday(Item);                        
                    }

                    Item = null;
                    mainPage.Load();
                    await Navigation.PopModalAsync();
                }
                
            }
            finally
            {
                this.IsBusy = false;
                button.IsEnabled = true;
            }
        }
    }
}
