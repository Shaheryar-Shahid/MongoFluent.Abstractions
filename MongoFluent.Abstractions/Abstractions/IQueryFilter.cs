
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoFluent.Abstractions.Abstractions
{
    public interface IQueryFilter<T>
    {
        #region Comparison Operations

        IQueryFilter<T> Eq<TField>(Expression<Func<T, TField>> field, TField value);

        IQueryFilter<T> Ne<TField>(Expression<Func<T, TField>> field, TField value);

        IQueryFilter<T> Gt<TField>(Expression<Func<T, TField>> field, TField value);

        IQueryFilter<T> Gte<TField>(Expression<Func<T, TField>> field, TField value);

        IQueryFilter<T> Lt<TField>(Expression<Func<T, TField>> field, TField value);

        IQueryFilter<T> Lte<TField>(Expression<Func<T, TField>> field, TField value);

        IQueryFilter<T> In<TField>(Expression<Func<T, TField>> field, IEnumerable<TField> values);

        IQueryFilter<T> Nin<TField>(Expression<Func<T, TField>> field, IEnumerable<TField> values);

        #endregion Comparison Operations

        #region Logical Operations

        IQueryFilter<T> And(params IQueryFilter<T>[] filters);

        IQueryFilter<T> Or(params IQueryFilter<T>[] filters);

        IQueryFilter<T> Not(IQueryFilter<T> filter);

        #endregion Logical Operations

        #region Element Operations

        IQueryFilter<T> Exists<TField>(Expression<Func<T, TField>> field, bool exists = true);

        IQueryFilter<T> Type<TField>(Expression<Func<T, TField>> field, string bsonType);

        #endregion Element Operations

        #region Evaluation Operations

        IQueryFilter<T> Regex<TField>(Expression<Func<T, TField>> field, string pattern, string options = "");

        #endregion Evaluation Operations

        #region Array Operations

        IQueryFilter<T> All<TField>(Expression<Func<T, TField>> field, IEnumerable<TField> values);

        IQueryFilter<T> ElemMatch<TField>(Expression<Func<T, TField>> field, Func<IQueryFilter<TField>, IQueryFilter<TField>> filter);

        IQueryFilter<T> Size<TField>(Expression<Func<T, TField>> field, int size);

        #endregion Array Operations

        #region Geospatial Operations

        IQueryFilter<T> GeoWithin<TField>(Expression<Func<T, TField>> field, object geometry);

        IQueryFilter<T> GeoIntersects<TField>(Expression<Func<T, TField>> field, object geometry);

        IQueryFilter<T> Near<TField>(Expression<Func<T, TField>> field, double x, double y, double maxDistance = 0, double minDistance = 0);

        IQueryFilter<T> NearSphere<TField>(Expression<Func<T, TField>> field, double x, double y, double maxDistance = 0, double minDistance = 0);

        #endregion Geospatial Operations

        object Build();
    }
}