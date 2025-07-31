using DataAccessLayer.Abstract;
using DataAccessLayer.Conrete;
using EntityLayer.Concrete;
using NuGet.Protocol.Core.Types;

namespace BusinessLayer.Concrete
{
    public class CategoryManager
    {
        private readonly IRepository _Repository;
        public CategoryManager(IRepository repository)
        {
            _Repository = repository;
        }
        public void Create(Category category)
        {
            var categoryy = new Category
            {
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            _Repository.Insert(categoryy);
        }

        public List<Category> GetList()
        {
            var query = _Repository.List<Category>();
            return query.ToList();
        }
        public void Update(Category category) 
        {
            _Repository.Update(category);
        }
        
    }
}
