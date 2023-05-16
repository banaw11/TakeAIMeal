using System.Linq.Expressions;

namespace TakeAIMeal.Data.Repositories.Interfaces
{
    /// <summary>
    /// Represents a generic repository interface for accessing data of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of data that the repository can access.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Gets the data of type <typeparamref name="T"/> with the specified <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The expression to satisfy.</param>
        /// <returns>The data of type <typeparamref name="T"/> with the specified <paramref name="id"/> and satisfies the specified <paramref name="expression"/>.</returns>
        T Get(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Adds the specified <paramref name="entity"/> to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates the specified <paramref name="entity"/> in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the data of type <typeparamref name="T"/> with the specified <paramref name="entity"/> from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Gets all the data of type <typeparamref name="T"/> from the repository.
        /// </summary>
        /// <returns>A collection of all the data of type <typeparamref name="T"/> from the repository.</returns>
        ICollection<T> GetAll();

        /// <summary>
        /// Returns the data of type <typeparamref name="T"/> that satisfies the specified <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The expression to satisfy.</param>
        /// <returns>The data of type <typeparamref name="T"/> that satisfies the specified <paramref name="expression"/>.</returns>
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Saves all changes made to the repository.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Determines whether any elements in the collection satisfy the specified condition
        /// defined by the provided expression.
        /// </summary>
        /// <param name="expression">The expression that defines the condition.</param>
        /// <returns>
        ///   <c>true</c> if any elements in the collection satisfy the condition; otherwise, <c>false</c>.
        /// </returns>
        bool Any(Expression<Func<T, bool>> expression);
    }
}
