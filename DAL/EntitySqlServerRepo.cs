using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BO;

namespace DAL
{

    public class EntitySqlServerRepo<T> : ICrud<T> where T : class, IIdentifiable
    {
        private static EntitySqlServerRepo<T> instance;

        private readonly Context db;

        private EntitySqlServerRepo()
        {
            db = GetDbContext.GetContext();
        }

        public static EntitySqlServerRepo<T> GetInstance()
        {
            return instance ?? (instance = new EntitySqlServerRepo<T>());
        }

        public T Create(T item)
        {
            try
            {
                db.Set<T>().Add(item);
                db.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public bool Delete(int id)
        {
            try
            {
                db.Set<T>().Remove(GetById(id));
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return db.Set<T>().ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        
        public T GetById(int? id)
        {
            try
            {
                return db.Set<T>().FirstOrDefault(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(T item)
        {
            try
            {
                db.Set<T>();
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }      


    }
}