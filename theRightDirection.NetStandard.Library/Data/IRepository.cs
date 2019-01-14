using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace theRightDirection.Data
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> Entities { get; }
        Task<bool> AddEntities(IEnumerable<T> newEntities);
        Task AddEntity(T newEntity);
        /// <summary>
        /// clear the list of entities
        /// </summary>
        /// <param name="saveData">if set to <c>true</c>all data will be saved as well</param>
        Task ClearEntities(bool saveData = false);
        Task<IEnumerable<T>> GetEntities();
        /// <summary>
        /// save an updated entity to file, in case entity does not exist, it will be added
        /// </summary>
        /// <param name="updatedEntity">The updated entity.</param>
        /// <returns>true if the transaction is succesfully completed</returns>
        Task<bool> UpdateEntity(T updatedEntity);
        T SearchEntity(Func<T, bool> predicate);
        Task DeleteEntity(T entityToDelete);
    }
}