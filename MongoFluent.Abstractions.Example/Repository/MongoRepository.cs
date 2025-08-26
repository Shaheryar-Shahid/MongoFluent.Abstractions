using AMService.Persistence.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoFluent.Abstractions.Abstractions;
using MongoFluent.Abstractions.Common.Utilities;
using MongoFluent.Abstractions.Examples.Interfaces;
using MongoFluent.Abstractions.Examples.Interfaces.Repository;
using MongoFluent.Abstractions.Implementations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MongoFluent.Abstractions.Examples.Repository
{
    public class MongoRepository<T>: IMongoRepository<T> where T : ITenantFilter
    {
        private readonly IMongoCollection<T> _collection;
        private readonly Guid tenantId = Guid.NewGuid();

        public MongoRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<T>();
        }

        public async Task<List<T>> GetAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder,
            Func<IProjection<T>, IProjection<T>>? projection = null,
            Func<ISort<T>, ISort<T>>? sort = null,
            int? skip = null,
            int? limit = null,
            CancellationToken cancellationToken = default)
        {
            var filter = filterBuilder(new MongoQueryFilter<T>());
            var query = _collection.Find((FilterDefinition<T>)filter.Build());

            if (projection != null)
                query = query.Project<T>((ProjectionDefinition<T>)projection(new MongoProjection<T>()).Build());

            if (sort != null)
                query = query.Sort((SortDefinition<T>)sort(new MongoSort<T>()).Build());

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (limit.HasValue)
                query = query.Limit(limit.Value);

            return await query.ToListAsync(cancellationToken);
        }
        public async Task<long> GetCountAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, CancellationToken cancellationToken = default)
        {

            var _filterDefinition = filterBuilder(new MongoQueryFilter<T>());
            var filter = (FilterDefinition<T>)WithTenant(_filterDefinition).Build();
            return await _collection.CountDocumentsAsync(filter, null, cancellationToken);
        }
        public async Task<long> GetTotalCountAsync(CancellationToken cancellationToken = default)
        {
            return await _collection.EstimatedDocumentCountAsync(null, cancellationToken);
        }

        public async Task InsertOneAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is ITenantFilter)
                entity.TenantId = tenantId;
            Console.WriteLine(entity.ToJson());
            await _collection.InsertOneAsync(entity, null, cancellationToken);
        }
        public async Task InsertAsync(List<T> entities, CancellationToken cancellationToken = default)
        {
            entities.ForEach(item =>
            {
                if (item is ITenantFilter)
                    item.TenantId = tenantId;
            });
            await _collection.InsertManyAsync(entities, null, cancellationToken);
        }


        public async Task<bool> UpdateOneAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, Dictionary<string, object> updates, CancellationToken cancellationToken = default)
        {
            var _filterDefinition = filterBuilder(new MongoQueryFilter<T>());
            var filter = (FilterDefinition<T>)WithTenant(_filterDefinition).Build();
            var udatesDef = QueryFilterHelper.ToUpdateDefinition<T>(updates);
            var res = await _collection.UpdateOneAsync(filter, udatesDef, null, cancellationToken);
            return res.ModifiedCount > 0;
        }

        public async Task<bool> UpdateAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, Dictionary<string, object> updates, CancellationToken cancellationToken = default)
        {
            var _filterDefinition = filterBuilder(new MongoQueryFilter<T>());
            var filter = (FilterDefinition<T>)WithTenant(_filterDefinition).Build();
            var udatesDef = QueryFilterHelper.ToUpdateDefinition<T>(updates);
            var res = await _collection.UpdateManyAsync(filter, udatesDef, null, cancellationToken);
            return res.ModifiedCount > 0;
        }

        public async Task<bool> DeleteOneAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, CancellationToken cancellationToken = default)
        {
            var _filterDefinition = filterBuilder(new MongoQueryFilter<T>());
            var filter = (FilterDefinition<T>)WithTenant(_filterDefinition).Build();
            var res = await _collection.DeleteOneAsync(filter);
            return res.DeletedCount > 0;
        }

        public async Task<bool> DeleteAsync(Func<IQueryFilter<T>, IQueryFilter<T>> filterBuilder, CancellationToken cancellationToken = default)
        {
            var _filterDefinition = filterBuilder(new MongoQueryFilter<T>());
            var filter = (FilterDefinition<T>)WithTenant(_filterDefinition).Build();
            var res = await _collection.DeleteManyAsync(filter);
            return res.DeletedCount > 0;
        }

        private IQueryFilter<T> WithTenant(IQueryFilter<T> filter)
        {
            return filter.Eq(x => x.TenantId, tenantId);
        }
    }
}
