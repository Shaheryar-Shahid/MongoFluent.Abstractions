using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MongoFluent.Abstractions.Abstractions
{
    public interface ISort<T>
    {
        ISort<T> Asc<TField>(Expression<Func<T, TField>> field);
        ISort<T> Desc<TField>(Expression<Func<T, TField>> field);
        object Build();
    }
}
