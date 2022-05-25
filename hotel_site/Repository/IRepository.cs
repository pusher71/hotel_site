using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotel_site
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetEntityList(); //получение всех объектов
        T GetEntity(int id); //получение одного объекта по id
        void Create(T entity); //создание объекта
        void Update(T entity); //обновление объекта
        void Delete(int id); //удаление объекта по id
        int GetNewId(); //получить новый id, который никем не используется
    }
}
