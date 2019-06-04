using System;

namespace BpmContactsApp.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string Dear { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; } 
        public string getStringDate()
        {
            if (BirthDate != DateTime.MinValue)
                return BirthDate.ToString("yyyy-MM-dd");
            else
                return "";
        }
    }
}
