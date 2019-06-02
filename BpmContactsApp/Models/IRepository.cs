﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpmContactsApp.Models
{
    public interface IRepository<T> 
       where T : class
    {
        IEnumerable<T> GetItems(); // получение всех объектов
        T GetItem(string id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(string id); // удаление объекта по id
       
    }
}
