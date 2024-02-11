using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trading.Core;

namespace Trading.Report.DAL
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        void Add(TEntity entity);
        IEnumerable<TEntity> GetAll();
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
