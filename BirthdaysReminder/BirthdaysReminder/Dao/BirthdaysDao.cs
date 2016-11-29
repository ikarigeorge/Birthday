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

        public async Task<IEnumerable<Birthdays>> GetBirthdays()
        {
            return await table.ToEnumerableAsync();
        }

        public Task AddBirthday(Birthdays birthday)
        {
            if (!string.IsNullOrWhiteSpace(birthday.Year))
            {
                birthday.Birthday = new DateTime(DateTime.Now.Year - int.Parse(birthday.Year), birthday.Birthday.Month, birthday.Birthday.Day);
                birthday.Year = "" + (DateTime.Now.Year - int.Parse(birthday.Year));
            }
            return table.InsertAsync(birthday);
        }

        public Task UpdateBirthday(Birthdays birthday)
        {
            if (!string.IsNullOrWhiteSpace(birthday.Year))
            {
                birthday.Birthday = new DateTime(DateTime.Now.Year - int.Parse(birthday.Year), birthday.Birthday.Month, birthday.Birthday.Day);
                birthday.Year = "" + (DateTime.Now.Year - int.Parse(birthday.Year));
            }
            return table.UpdateAsync(birthday);
        }

        public Task DeleteBirthday(Birthdays birthday)
        {
            return table.DeleteAsync(birthday);
        }

        
        
    }
}
