using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MongoFluent.Abstractions.Abstractions
{
    public interface IProjection<T>
    {
        IProjection<T> Include<TField>(Expression<Func<T, TField>> field);
        IProjection<T> Exclude<TField>(Expression<Func<T, TField>> field);
        object Build();
    }
}
