using DataAccessLayer.Abstract;
using DataAccessLayer.Conrete;
using EntityLayer.Concrete;
using NuGet.Protocol.Core.Types;

namespace BusinessLayer.Concrete
{
    public class BrandManager
    {
        private readonly IRepository _Repository;
        public BrandManager(IRepository repository)
        {
            _Repository = repository;
        }
        public void Create(Brand brand)
        {
            var brandd = new Brand
            {
                BrandName = brand.BrandName,
                Description = brand.Description
            };
            _Repository.Insert(brandd);
        }

        public List<Brand> GetList()
        {
            var query = _Repository.List<Brand>();
            return query.ToList();
        }
        public void Update(Brand brand)
        {
            _Repository.Update(brand);
        }

    }
}