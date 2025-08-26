using MongoFluent.Abstractions.Abstractions;
using MongoFluent.Abstractions.Examples.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MongoFluent.Abstractions.Examples.Interfaces.Repository
{
    public interface IMongoRepository<T> where T : ITenantFilter
    {
        public Task<List<T>> GetAsync(
            Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder,
            Func<IProjection<T>, IProjection<T>> projection = null,
            Func<ISort<T>, ISort<T>> sort = null,
            int? skip = null,
            int? limit = null,
            CancellationToken cancellationToken = default);
        public Task<long> GetCountAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, CancellationToken cancellationToken = default);
        public Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default);
        public Task InsertOneAsync(T entity, CancellationToken cancellationToken = default);
        public Task InsertAsync(List<T> entities, CancellationToken cancellationToken = default);
        public Task<bool> UpdateOneAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, Dictionary<string, object> updates, CancellationToken cancellationToken = default);
        public Task<bool> UpdateAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, Dictionary<string, object> updates, CancellationToken cancellationToken = default);
        public Task<bool> DeleteOneAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, CancellationToken cancellationToken = default);
        public Task<bool> DeleteAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, CancellationToken cancellationToken = default);
    }
}
