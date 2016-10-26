using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasis.DataAccess.Contracts;

namespace Oasis.DataAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();


        public UnitOfWork(DbContext context)
        {
            _context = context;

            _context.Configuration.LazyLoadingEnabled = false;
            _context.Configuration.ProxyCreationEnabled = false;
        }

        public IRepository<TEntity> GetBaseRepository<TEntity>() where TEntity : class
        {
            var typeName = typeof(TEntity).Name;

            if (_repositories.ContainsKey(typeName))
                return (IRepository<TEntity>)_repositories[typeName];

            var repositoryType = typeof(Repository<>);
            var genericRepoType = repositoryType.MakeGenericType(typeof(TEntity));
            _repositories.Add(typeName, Activator.CreateInstance(genericRepoType, _context));
            return (IRepository<TEntity>)_repositories[typeName];
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
