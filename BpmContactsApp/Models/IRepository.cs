using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpmContactsApp.Models
{
    public interface IRepository<T> 
       where T : class
    {
        IEnumerable<T> GetItems(); // получение всех объектов
        T GetItem(int id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
