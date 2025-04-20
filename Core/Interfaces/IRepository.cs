using Domain.BaseEntities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>: IEnumerable<T> where T : IUniqueId
    {
        public Task<T> GetByIdAsync(Guid id);
        public Task AddAsync(T obj);
        public Task UpdateAsync(T obj);
        public Task DeleteByIdAsync(Guid id);
        public Task DeleteAsync(T obj);
    }
}
