using System.Collections.Generic;
using BO;

namespace DAL
{
    public class DalGenerique<T> : ICrud<T> where T : class, IIdentifiable
    {
        private static DalGenerique<T> instance;

        private EntitySqlServerRepo<T> repo;

        private DalGenerique()
        {
            repo = EntitySqlServerRepo<T>.GetInstance();
        }

        public static DalGenerique<T> GetInstance()
        {
            return instance ?? (instance = new DalGenerique<T>());
        }

        public List<T> GetAll()
        {
            return repo.GetAll();
        }

        public T GetById(int? id)
        {
            return repo.GetById(id);
        }


        public T Create(T item)
        {
            return repo.Create(item);
        }


        public bool Update(T item)
        {
            return repo.Update(item);
        }


        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}