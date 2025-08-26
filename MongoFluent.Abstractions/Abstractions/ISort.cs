using System;
using System.Linq.Expressions;

namespace MongoFluent.Abstractions.Abstractions
{
    /// <summary>
    /// Defines sorting operations for MongoDB queries.
    /// </summary>
    /// <typeparam name="T">The type of the document.</typeparam>
    public interface ISort<T>
    {
        /// <summary>
        /// Sorts the results in ascending order by the specified field.
        /// </summary>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field to sort by.</param>
        /// <returns>The updated sort definition.</returns>
        ISort<T> Asc<TField>(Expression<Func<T, TField>> field);

        /// <summary>
        /// Sorts the results in descending order by the specified field.
        /// </summary>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field to sort by.</param>
        /// <returns>The updated sort definition.</returns>
        ISort<T> Desc<TField>(Expression<Func<T, TField>> field);

        /// <summary>
        /// Builds the sort definition object.
        /// </summary>
        /// <returns>The sort definition object.</returns>
        object Build();
    }
}
