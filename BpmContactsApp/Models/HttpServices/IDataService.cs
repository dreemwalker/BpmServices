using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpmContactsApp.Models.HttpServices
{
    public interface IDataService
    {
        
        List<Contact> GetContacts();
        Contact GetContactByID(string id);
        void InsertContact(Contact contact);
        void DeleteContact(string id);
        void UpdateContact(Contact contact);
    }
}
