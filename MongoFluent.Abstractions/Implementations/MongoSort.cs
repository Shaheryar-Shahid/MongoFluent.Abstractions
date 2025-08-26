using MongoDB.Driver;
using MongoFluent.Abstractions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoFluent.Abstractions.Implementations
{
    public class MongoSort<T> : ISort<T>
    {
        private SortDefinition<T> _sort = Builders<T>.Sort.Ascending("_id");

        public ISort<T> Asc<TField>(Expression<Func<T, TField>> field)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _sort = Builders<T>.Sort.Ascending(fieldDef);
            return this;
        }

        public ISort<T> Desc<TField>(Expression<Func<T, TField>> field)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _sort = Builders<T>.Sort.Descending(fieldDef);
            return this;
        }

        public object Build() => _sort;
    }
}
