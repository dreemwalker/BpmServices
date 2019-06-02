using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BpmContactsApp.Models.HttpServices;
namespace BpmContactsApp.Models
{
    public class ContactsHttpRepository : IRepository<Contact>
    {
        private IDataService _dataService;
        private AppContext _contextBpm;
        public ContactsHttpRepository(IDataService dataService)
        {
            _dataService = dataService;
        }
        public void Create(Contact item)
        {
            _dataService.InsertContact(item);
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
            throw new NotImplementedException();
        }
       


    }
}
