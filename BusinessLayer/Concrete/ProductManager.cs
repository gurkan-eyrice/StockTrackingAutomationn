using DataAccessLayer.Abstract;
using DataAccessLayer.Conrete;
using EntityLayer.Concrete;
using NuGet.Protocol.Core.Types;

namespace BusinessLayer.Concrete
{
    public class ProductManager
    {
        private readonly IRepository _Repository;
        public ProductManager(IRepository repository)
        {
            _Repository = repository;
        }
        public void Create(Product product)
        {
            var productt = new Product
            {
                ProductName = product.ProductName,
                BarcodeName = product.BarcodeName,
                Price = product.Price,
                Category = product.Category,
                Brand = product.Brand,
                Unit = product.Unit,
                Amount = product.Amount,
                Description = product.Description
            };
            _Repository.Insert(productt);
        }

        public List<Product> GetList()
        {
            var query = _Repository.List<Product>();
            return query.ToList();
        }
        public void Update(Product product)
        {
            _Repository.Update(product);
        }

    }
}
