using BpmContactsApp.Models.HttpServices;
using System.Collections.Generic;
namespace BpmContactsApp.Models
{
    public class ContactsRepository : IRepository<Contact>
    {
        private IDataService _dataService;
      
        public ContactsRepository(IDataService dataService)
        {
            _dataService = dataService;
        }
        public bool Create(Contact item)
        {
            if (_dataService.InsertContact(item))
                return true;
            return false;
        }

        public void Delete(string id)
        {
            _dataService.DeleteContact(id);
        }

        public Contact GetItem(string id)
        {
            return _dataService.GetContactByID(id);
        }

        public IEnumerable<Contact> GetItems()
        {
            return _dataService.GetContacts();
        }

       

        public void Update(Contact item)
        {
            _dataService.UpdateContact(item);
        }
       


    }
}
