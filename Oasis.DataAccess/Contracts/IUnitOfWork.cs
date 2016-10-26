using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        IRepository<TEntity> GetBaseRepository<TEntity>() where TEntity : class;
    }
}
