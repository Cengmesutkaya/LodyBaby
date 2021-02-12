using DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public class Repository<T> : IReporsitory<T> where T : class
    {
        private DbSet<T> _objectSet;
        private readonly Db _dbContext = new Db();
        public Repository()
        {
            _objectSet = _dbContext.Set<T>();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }
        public T GetById(int? Id)
        {
            return _objectSet.Find(Id);
        }
        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        public int Update(T obj)
        {
            return Save();
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where);
        }
    }
}
