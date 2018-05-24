using System.Collections.Generic;

namespace DAL
{
    public interface ICrud<T>
    {
        List<T> GetAll();

        T GetById(int? id);

        T Create(T item);

        bool Update(T item);

        bool Delete(int id);
    }
}
