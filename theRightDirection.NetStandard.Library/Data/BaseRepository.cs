using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theRightDirection.Data;
using theRightDirection.Exceptions;

namespace theRightDirection.Library.Data
{
    public abstract class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly string _fileName;
        protected readonly List<T> _items;
        private bool _isLoaded;

        public BaseRepository(string fileName)
        {
            _items = new List<T>();
            _fileName = fileName;
        }

        public IEnumerable<T> Entities
        {
            get
            {
                if (_isLoaded)
                {
                    return _items;
                }
                throw new RepositoryException("entities need to be loaded first by GetEntities");
            }
        }

        public async Task<bool> AddEntities(IEnumerable<T> newEntities)
        {
            if (!_isLoaded)
            {
                await LoadEntities();
            }
            if (newEntities.Count() > 0)
            {
                _items.AddRange(newEntities);
                return await Save();
            }
            return false;
        }

        /// <summary>
        /// add one entity and save the list of entities
        /// </summary>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        public async Task AddEntity(T newEntity)
        {
            if (!_isLoaded)
            {
                await LoadEntities();
            }
            if (newEntity != null)
            {
                _items.Add(newEntity);
                await Save();
            }
        }

        public async Task<IEnumerable<T>> GetEntities()
        {
            if (!_isLoaded)
            {
                await LoadEntities();
            }
            return _items;
        }

        public T SearchEntity(Func<T, bool> predicate)
        {
            return _items.FirstOrDefault(predicate);
        }

        /// <summary>
        /// save an updated entity to file, in case entity does not exist, it will be added
        /// </summary>
        /// <param name="updatedEntity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateEntity(T updatedEntity)
        {
            if (!_isLoaded)
            {
                await LoadEntities();
            }
            var indexOfOldEntity = FindIndex(updatedEntity.Identifier);
            if (indexOfOldEntity > -1)
            {
                _items[indexOfOldEntity] = updatedEntity;
            }
            else
            {
                _items.Add(updatedEntity);
            }
            return await Save();
        }

        public async Task ClearEntities(bool saveData = false)
        {
            if (!_isLoaded)
            {
                await LoadEntities();
            }
            _items.Clear();
            if (saveData)
            {
                await Save();
            }
        }

        private int FindIndex(Guid identifier)
        {
            if (_items.Count == 0)
            {
                return -1;
            }
            return _items.FindIndex(x => x.Identifier == identifier);
        }

        private async Task LoadEntities()
        {
            if (_isLoaded)
            {
                return;
            }
            try
            {
                var content = await GetJsonFromFile();
                var items = JsonConvert.DeserializeObject<List<T>>(content);
                if (items != null)
                {
                    _items.Clear();
                    _items.AddRange(items);
                }
                _isLoaded = true;
            }
            catch (Exception e)
            {
                throw new RepositoryException(e);
            }
        }

        protected abstract Task<string> GetJsonFromFile();
        protected abstract Task<bool> SaveJsonToFile(string json);

        private async Task<bool> Save()
        {
            var json = JsonConvert.SerializeObject(_items);
            return await SaveJsonToFile(json);
        }

        public async Task DeleteEntity(T entityToDelete)
        {
            var index = FindIndex(entityToDelete.Identifier);
            if (index > -1)
            {
                _items.RemoveAt(index);
                await Save();
            }
        }
    }
}
