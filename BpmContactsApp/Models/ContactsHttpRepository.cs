using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpmContactsApp.Models
{
    public class ContactsHttpRepository : IRepository<Contact>
    {
        private AppContext _contextBpm;

        public void Create(Contact item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Contact GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> GetItems()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Contact item)
        {
            throw new NotImplementedException();
        }
       


    }
}
