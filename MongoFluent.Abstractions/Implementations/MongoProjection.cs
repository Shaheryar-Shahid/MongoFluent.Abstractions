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
    public class MongoProjection<T> : IProjection<T>
    {
        private ProjectionDefinition<T> _projection = Builders<T>.Projection.Exclude("_id");

        public IProjection<T> Include<TField>(Expression<Func<T, TField>> field)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            var include = Builders<T>.Projection.Include(fieldDef);
            _projection = _projection == null ? include : Builders<T>.Projection.Combine(_projection, include);
            return this;
        }
        public IProjection<T> Exclude<TField>(Expression<Func<T, TField>> field)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            var exclude = Builders<T>.Projection.Exclude(fieldDef);
            _projection = _projection == null ? exclude : Builders<T>.Projection.Combine(_projection, exclude);
            return this;
        }
        public object Build() => _projection;
    }
}
