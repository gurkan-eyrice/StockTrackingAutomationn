using DataAccessLayer.Abstract;
using DataAccessLayer.Conrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository : IRepository
    {
        private readonly Context _context;
        public GenericRepository(Context context)
        {
            _context = context;
        }

        public void Delete<T>(T p) where T : class
        {
            _context.Set<T>().Remove(p);
            _context.SaveChanges();
        }

        public void Insert<T>(T p) where T : class
        {
            _context.Set<T>().Add(p);
            _context.SaveChanges();
        }
        public IQueryable<T> List<T>(Expression<Func<T, bool>> filter = null) where T : class
        {
            return filter == null ? _context.Set<T>().AsQueryable() : _context.Set<T>().Where(filter);
        }

        public void Update<T>(T p) where T : class
        {
            _context.Set<T>().Update(p);
            _context.SaveChanges();
        }
        public T GetById<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>();
        }
    }
}
