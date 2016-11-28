using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using BirthdaysReminder.Models;

namespace BirthdaysReminder.Dao
{
    public class BirthdaysDao
    {
        private IMobileServiceClient client;
        private IMobileServiceTable<Birthdays> table;

        public BirthdaysDao()
        {
            client = new MobileServiceClient("http://birthdaysreminder.azurewebsites.net");

            table = client.GetTable<Birthdays>();
        }

        public Task<IEnumerable<Birthdays>> GetBirthdays()
        {
            return table.ToEnumerableAsync();
        }

        public Task AddBirthday(Birthdays birthday)
        {
            if (birthday.Year != null)
            {
                birthday.Birthday = new DateTime(DateTime.Now.Year - int.Parse(birthday.Year), birthday.Birthday.Month, birthday.Birthday.Day);
            }
            return table.InsertAsync(birthday);
        }

        public Task UpdateBirthday(Birthdays birthday)
        {
            if (birthday.Year != null)
            {
                birthday.Birthday = new DateTime(DateTime.Now.Year - int.Parse(birthday.Year), birthday.Birthday.Month, birthday.Birthday.Day);
            }
            return table.UpdateAsync(birthday);
        }
        
    }
}
