using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUserService
    {
        UserAccount GetUserByEmail(string email);
        void Insert(UserAccount account);
        void Update<T>(T p) where T : class;
        IQueryable<T> List<T>(Expression<Func<T, bool>> filter = null) where T : class;
        bool IsUsernameUnique(string username);
        bool IsEmailUnique(string email);
        UserAccount GetUserById(int id);
        UserAccount GetUserByUsername(string username);
    }
}
