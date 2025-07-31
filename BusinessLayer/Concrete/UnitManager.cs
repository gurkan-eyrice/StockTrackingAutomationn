using DataAccessLayer.Abstract;
using DataAccessLayer.Conrete;
using EntityLayer.Concrete;
using NuGet.Protocol.Core.Types;

namespace BusinessLayer.Concrete
{
    public class UnitManager
    {
        private readonly IRepository _Repository;
        public UnitManager(IRepository repository)
        {
            _Repository = repository;
        }
        public void Create(Unit unit)
        {
            var unitt = new Unit
            {
                UnitName = unit.UnitName,
                Description = unit.Description
            };
            _Repository.Insert(unitt);
        }

        public List<Unit> GetList()
        {
            var query = _Repository.List<Unit>();
            return query.ToList();
        }
        public void Update(Unit unit)
        {
            _Repository.Update(unit);
        }

    }
}