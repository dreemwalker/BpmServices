﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpmContactsApp.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        //public string Dear { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
