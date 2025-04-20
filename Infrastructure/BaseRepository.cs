using Domain.Entities;
using Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using System.Collections.Concurrent;
using System.Collections;

namespace Infrastructure
{
    public class BaseRepository<T> : IRepository<T> where T : IUniqueId
    {
        private ConcurrentDictionary<Guid, T> _data;

        public BaseRepository()
        {
            _data = new ConcurrentDictionary<Guid, T>();
        }

        public Task AddAsync(T obj)
        {
            if (!_data.ContainsKey(obj.Id))
            {
                _data.GetOrAdd(obj.Id, obj);
                return Task.CompletedTask;
            }
            throw new ArgumentException($"Object with id = {obj.Id} already exist");
        }

        public Task DeleteAsync(T obj) => DeleteByIdAsync(obj.Id);

        public Task DeleteByIdAsync(Guid id)
        {
            if (_data.ContainsKey(id))
            {
                T? val;
                _data.Remove(id, out val);
                return Task.CompletedTask;
            }
            throw new ArgumentException($"Object with id {id} not found");
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            if (_data.ContainsKey(id))
            {
                return Task.FromResult(_data[id]);
            }
            throw new ArgumentException($"Object with id {id} not found");
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _data.Values)
            {
                yield return item;
            }
        }

        public Task UpdateAsync(T obj)
        {
            if (_data.ContainsKey(obj.Id))
            {
                _data[obj.Id] = obj;
                return Task.CompletedTask;
            }
            throw new ArgumentException($"Object with id {obj.Id} not found");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
