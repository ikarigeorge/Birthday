using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdaysReminder.Models
{
    [DataTable("Birthdays")]
    public class Birthdays
    {
        [JsonProperty]
        public string Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public DateTime Birthday { get; set; }

        [JsonProperty]
        public string Year { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
