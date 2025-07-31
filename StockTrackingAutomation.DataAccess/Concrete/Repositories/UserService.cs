using DataAccessLayer.Abstract;
using DataAccessLayer.Conrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        public UserService(Context context)
        {
            _context = context;
        }
        public UserAccount GetUserByEmail(string email)
        {
            return _context.UserAccounts.FirstOrDefault(u => u.Email == email);
        }
        public bool IsUsernameUnique(string username)
        {
            return !_context.UserAccounts.Any(u => u.UserName == username.ToLower());
        }
        public bool IsEmailUnique(string email)
        {
            return !_context.UserAccounts.Any(u => u.Email == email);
        }

        public void Insert(UserAccount account)
        {
            _context.UserAccounts.Add(account);
            _context.SaveChanges();
        }
        public UserAccount GetUserById(int id)
        {
            return _context.UserAccounts.Find(id);
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
        public UserAccount GetUserByUsername(string username)
        {
            return _context.UserAccounts.SingleOrDefault(u => u.UserName == username);
        }


    }
}
