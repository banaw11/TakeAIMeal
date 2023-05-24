using System.Linq.Expressions;

namespace TakeAIMeal.Data.Repositories.Interfaces
{
    /// <summary>
    /// Represents a repository for accessing user product exclusion views.
    /// </summary>
    public interface IUserProductExclusionViewRepository 
    {
        /// <summary>
        /// Retrieves a user product exclusion view based on the specified expression.
        /// </summary>
        /// <param name="expression">The expression used to filter the user product exclusion views.</param>
        /// <returns>The user product exclusion view that matches the specified expression.</returns>
        UserProductExclusionView Get(Expression<Func<UserProductExclusionView, bool>> expression);

        /// <summary>
        /// Retrieves a collection of user product exclusion views based on the specified expression.
        /// </summary>
        /// <param name="expression">The expression used to filter the user product exclusion views.</param>
        /// <returns>A queryable collection of user product exclusion views that match the specified expression.</returns>
        IQueryable<UserProductExclusionView> Where(Expression<Func<UserProductExclusionView, bool>> expression);
    }
}
