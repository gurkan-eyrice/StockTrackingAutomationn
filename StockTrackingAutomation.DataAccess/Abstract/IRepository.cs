using System.Linq.Expressions;

namespace DataAccessLayer.Abstract
{
    public interface IRepository
    {
        void Insert<T>(T p) where T : class;
        void Delete<T>(T p) where T : class;
        void Update<T>(T p) where T : class;
        IQueryable<T> List<T>(Expression<Func<T, bool>> filter = null) where T : class;
        T GetById<T>(int id) where T : class;
        IQueryable<T> GetAll<T>() where T : class;

    }
}
