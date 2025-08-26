using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using MongoFluent.Abstractions.Abstractions;
using MongoFluent.Abstractions.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoFluent.Abstractions.Implementations
{
    public class MongoQueryFilter<T> : IQueryFilter<T>
    {
        private FilterDefinition<T> _filter = Builders<T>.Filter.Empty;

        #region Comparison Operations
        public IQueryFilter<T> Eq<TField>(Expression<Func<T, TField>> field, TField value)
        {
            _filter &= Builders<T>.Filter.Eq(field, value);
            return this;
        }

        public IQueryFilter<T> Ne<TField>(Expression<Func<T, TField>> field, TField value)
        {
            _filter &= Builders<T>.Filter.Ne(field, value);
            return this;
        }

        public IQueryFilter<T> Gt<TField>(Expression<Func<T, TField>> field, TField value)
        {
            _filter &= Builders<T>.Filter.Gt(field, value);
            return this;
        }

        public IQueryFilter<T> Gte<TField>(Expression<Func<T, TField>> field, TField value)
        {
            _filter &= Builders<T>.Filter.Gte(field, value);
            return this;
        }

        public IQueryFilter<T> Lt<TField>(Expression<Func<T, TField>> field, TField value)
        {
            _filter &= Builders<T>.Filter.Lt(field, value);
            return this;
        }

        public IQueryFilter<T> Lte<TField>(Expression<Func<T, TField>> field, TField value)
        {
            _filter &= Builders<T>.Filter.Lte(field, value);
            return this;
        }

        public IQueryFilter<T> In<TField>(Expression<Func<T, TField>> field, IEnumerable<TField> values)
        {
            _filter &= Builders<T>.Filter.In(field, values);
            return this;
        }

        public IQueryFilter<T> Nin<TField>(Expression<Func<T, TField>> field, IEnumerable<TField> values)
        {
            _filter &= Builders<T>.Filter.Nin(field, values);
            return this;
        }
        #endregion

        #region Logical Operators
        public IQueryFilter<T> And(params IQueryFilter<T>[] filters)
        {
            _filter &= Builders<T>.Filter.And(filters.Select(f => (FilterDefinition<T>)f.Build()));
            return this;
        }

        public IQueryFilter<T> Or(params IQueryFilter<T>[] filters)
        {
            _filter &= Builders<T>.Filter.Or(filters.Select(f => (FilterDefinition<T>)f.Build()));
            return this;
        }

        public IQueryFilter<T> Not(IQueryFilter<T> filter)
        {
            _filter &= Builders<T>.Filter.Not((FilterDefinition<T>)filter.Build());
            return this;
        }
        #endregion

        #region Element Operations
        public IQueryFilter<T> Exists<TField>(Expression<Func<T, TField>> field, bool exists = true)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _filter &= Builders<T>.Filter.Exists(fieldDef, exists);
            return this;
        }

        public IQueryFilter<T> Type<TField>(Expression<Func<T, TField>> field, string bsonType)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _filter &= Builders<T>.Filter.Type(fieldDef, bsonType);
            return this;
        }
        #endregion

        #region Evaluation Operations
        public IQueryFilter<T> Regex<TField>(Expression<Func<T, TField>> field, string pattern, string options = "")
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _filter &= Builders<T>.Filter.Regex(fieldDef, new MongoDB.Bson.BsonRegularExpression(pattern, options));
            return this;
        }
        #endregion

        #region Array Operations
        public IQueryFilter<T> All<TField>(Expression<Func<T, TField>> field, IEnumerable<TField> values)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _filter &= Builders<T>.Filter.All(fieldDef, values);
            return this;
        }

        public IQueryFilter<T> ElemMatch<TField>(Expression<Func<T, TField>> field, Func<IQueryFilter<TField>, IQueryFilter<TField>> filter)
        {
            var inner = new MongoQueryFilter<TField>();
            var built = filter(inner);
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _filter &= Builders<T>.Filter.ElemMatch(fieldDef, (FilterDefinition<TField>)built.Build());
            return this;
        }

        public IQueryFilter<T> Size<TField>(Expression<Func<T, TField>> field, int size)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _filter &= Builders<T>.Filter.Size(fieldDef, size);
            return this;
        }
        public IQueryFilter<T> GeoWithin<TField>(Expression<Func<T, TField>> field, object geometry)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            if (geometry is GeoJsonGeometry<GeoJson2DCoordinates>)
                _filter &= Builders<T>.Filter.GeoWithin(fieldDef, geometry as GeoJsonGeometry<GeoJson2DCoordinates>);
            else
                throw new ArgumentException(ExceptionConstants.UnsupportedType);
            return this;
        }

        public IQueryFilter<T> GeoIntersects<TField>(Expression<Func<T, TField>> field, object geometry)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            if (geometry is GeoJsonGeometry<GeoJson2DCoordinates>)
                _filter &= Builders<T>.Filter.GeoIntersects(fieldDef, geometry as GeoJsonGeometry<GeoJson2DCoordinates>);
            else
                throw new ArgumentException(ExceptionConstants.UnsupportedType);
            return this;
        }

        public IQueryFilter<T> Near<TField>(Expression<Func<T, TField>> field, double x, double y, double maxDistance = 0, double minDistance = 0)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _filter &= Builders<T>.Filter.Near(fieldDef, x, y, maxDistance, minDistance);
            return this;
        }

        public IQueryFilter<T> NearSphere<TField>(Expression<Func<T, TField>> field, double x, double y, double maxDistance = 0, double minDistance = 0)
        {
            FieldDefinition<T> fieldDef = new ExpressionFieldDefinition<T, TField>(field);
            _filter &= Builders<T>.Filter.NearSphere(fieldDef, x, y, maxDistance, minDistance);
            return this;
        }
        #endregion

        public object Build() => _filter;
    }
}
