using System;
using System.Linq.Expressions;

namespace MongoFluent.Abstractions.Abstractions
{
    /// <summary>
    /// Defines projection operations for MongoDB queries.
    /// </summary>
    /// <typeparam name="T">The type of the document.</typeparam>
    public interface IProjection<T>
    {
        /// <summary>
        /// Includes the specified field in the result set.
        /// </summary>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field to include.</param>
        /// <returns>The updated projection definition.</returns>
        IProjection<T> Include<TField>(Expression<Func<T, TField>> field);

        /// <summary>
        /// Excludes the specified field from the result set.
        /// </summary>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field to exclude.</param>
        /// <returns>The updated projection definition.</returns>
        IProjection<T> Exclude<TField>(Expression<Func<T, TField>> field);

        /// <summary>
        /// Builds the projection definition object.
        /// </summary>
        /// <returns>The projection definition object.</returns>
        object Build();
    }
}