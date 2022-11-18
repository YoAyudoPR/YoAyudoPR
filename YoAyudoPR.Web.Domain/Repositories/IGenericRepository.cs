using System;
using System.Linq.Expressions;

namespace YoAyudoPR.Web.Domain.Repositories
{
    public interface IGenericRepository<T> : IDisposable
    {
        // CREATE Methods

        /// <summary>
        ///     Asynchronously inserts new <paramref name="entity"/> into the Repository and saves this transaction.
        /// </summary>
        /// <param name="entity">Entity to be inserted in Repository.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddAndSaveAsync(T entity, CancellationToken token = default);

        /// <summary>
        ///     Asynchronously inserts new collection of <paramref name="entities"/> into the Repository and saves this transaction.
        /// </summary>
        /// <param name="entities">Collection of entities to be inserted in Repository.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddRangeAndSaveAsync(IEnumerable<T> entities, CancellationToken token = default);
        //Task AddAsync(T entity);


        Task<int> CountAsync(Expression<Func<T, bool>> expression, CancellationToken token = default);


        // READ Methods

        /// <summary>
        ///     <para>
        ///         Finds the first entity that satisfies the given <paramref name="predicate"/>, or a default
        ///         value if no such entity is found.
        ///     </para>
        ///     <para></para>
        ///     <para>
        ///         NOTE: The returned entity is not tracked, so any changes performed on it will not persist in the database.
        ///     </para>
        /// </summary>
        /// <param name="predicate">Expression to which entities in Repository must conform to.</param>
        /// <returns>First entity that satisfies <paramref name="predicate"/> or default if none is found.</returns>
        T FirstByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        ///     <para>
        ///         Asynchronously finds and returns the first entity that satisfies the given <paramref name="predicate"/>, or a default
        ///         value if no such entity is found.
        ///     </para>
        ///     <para></para>
        ///     <para>
        ///         NOTE: The returned entity is not tracked, so any changes performed on it will not persist in the database.
        ///     </para>
        /// </summary>
        /// <param name="predicate">Expression to which entities in Repository must conform to.</param>
        /// <param name="token"></param>
        /// <returns>First entity that satisfies <paramref name="predicate"/> or default if none is found.</returns>
        Task<T> FirstByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken token = default);

        /// <summary>
        ///     <para>
        ///         Asynchronously finds and returns the ID of the first entity that satisfies the given <paramref name="predicate"/> and <paramref name="selector"/>, or a default
        ///         value if no such entity is found.
        ///     </para>
        ///     <para></para>
        ///     <para>
        ///         NOTE: The returned entity is not tracked, so any changes performed on it will not persist in the database.
        ///     </para>
        /// </summary>
        /// <param name="predicate">Expression to which entities in Repository must conform to.</param>
        /// <param name="token"></param>
        /// <returns>ID of the first entity that satisfies <paramref name="predicate"/> or default if none is found.</returns>
        Task<int?> FirstByConditionAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, int>> selector,
            CancellationToken token = default);

        /// <summary>
        ///     <para>
        ///         Asynchronously finds and returns the Guid of the first entity that satisfies the given <paramref name="predicate"/> and <paramref name="selector"/>, or a default
        ///         value if no such entity is found.
        ///     </para>
        ///     <para></para>
        /// </summary>
        /// <param name="predicate">Expression to which entities in Repository must conform to.</param>
        /// <param name="token"></param>
        /// <returns>Guid of the first entity that satisfies <paramref name="predicate"/> or default if none is found.</returns>
        Task<Guid?> FirstByConditionAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, Guid>> selector,
            CancellationToken token = default);

        /// <summary>
        ///     Asynchronously finds and returns the first entity that satisfies the given <paramref name="predicate"/>, or a default
        ///     value if no such entity is found.
        /// </summary>
        /// <param name="predicate">Expression to which entities in Repository must conform to.</param>
        /// <param name="token"></param>
        /// <returns>First entity that satisfies <paramref name="predicate"/> or default if none is found.</returns>
        Task<T> FirstByConditionTrackingAsync(Expression<Func<T, bool>> expression, CancellationToken token = default);

        /// <summary>
        ///     Synchronously finds and returns the first entity that satisfies the given <paramref name="predicate"/> 
        ///     and the related entities specified in <paramref name="includes"/>, or a default value if no such entity is found.
        /// </summary>
        /// <param name="includes">Related entities to include</param>
        /// <param name="predicate">Expression to which entities in Repository must conform to.</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        T FirstUsingIncludes(IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     Asynchronously finds and returns the first entity that satisfies the given <paramref name="predicate"/> 
        ///     and the related entities specified in <paramref name="includes"/>, or a default value if no such entity is found.
        /// </summary>
        /// <param name="includes">Related entities to include</param>
        /// <param name="predicate">Expression to which entities in Repository must conform to.</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        Task<T> FirstUsingIncludesAsync(IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate, CancellationToken token);

        /// <summary>
        ///     Finds and returns an entity with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Primary key for the entity to be found.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<T> FindByIdAsync(int id, CancellationToken token = default);

        /// <summary>
        ///     Finds and returns an entity with the given <paramref name="keyValues"/>.
        /// </summary>
        /// <param name="keyValues">Key values for the entity to be found</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<T> FindByIdAsync(object[] keyValues, CancellationToken token = default);

        /// <summary>
        ///     Finds and returns an entity with the given <paramref name="guid"/>.
        /// </summary>
        /// <param name="guid">GUID for the entity to be found.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<T> FindByGuidAsync(Guid guid, CancellationToken token = default);


        /// <summary>
        ///     <para>
        ///         Finds all entities that satisfy a given <paramref name="predicate"/>.
        ///     </para>
        ///     <para></para>
        ///     <para>
        ///         NOTE: The returned collection is not tracked, so any changes performed on it will not persist in the database.
        ///     </para>
        /// </summary>
        /// <param name="predicate">Expression to which entities in Repository must conform to.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, CancellationToken token = default);

        /// <summary>
        ///     <para></para>
        ///     <para>
        ///         NOTE: The returned collection is not tracked, so any changes performed on it will not persist in the database.
        ///     </para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IQueryable<T> FindAllAsIQueryable(CancellationToken token = default);


        /// <summary>
        ///     <para>
        ///         Asynchronously finds and returns all entities.
        ///     </para>
        ///     <para></para>
        ///     <para>
        ///         NOTE: The returned collection is not tracked, so any changes performed on it will not persist in the database.
        ///     </para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync(CancellationToken token = default);

        /// <summary>
        ///     <para>
        ///         Asynchronously finds and returns all entities that satisfy a given <paramref name="predicate"/>.
        ///     </para>
        ///     <para></para>
        ///     <para>
        ///         NOTE: The returned collection is not tracked, so any changes performed on it will not persist in the database.
        ///     </para>
        /// </summary>
        /// <param name="predicate">Expression to which entities in Repository must conform to.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);


        /// <summary>
        ///     Get entity and related entities by passing expression list containing the desired includes
        /// </summary>
        /// <param name="includes"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> GetUsingIncludes(IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate);

        /// <summary>
        ///     <para>
        ///         Asynchronously get entity and related entities by passing expression list containing the desired includes    
        ///     </para>        
        /// </summary>
        /// <param name="includes"></param>
        /// <param name="predicate"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetUsingIncludes(IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate, CancellationToken token);

        /// <summary>
        ///     <para>
        ///         Asynchronously get entity and related entities by passing expression list containing the desired includes    
        ///     </para>        
        /// </summary>
        /// <param name="includes"></param>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetUsingIncludesWithPagination(
            IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderByExpression,
            int pageIndex,
            int pageSize,
            CancellationToken token
        );


        /// <summary>
        ///     ???
        /// </summary>
        /// <param name="includes"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> GetUsingIncludes(IEnumerable<string> includes, Expression<Func<T, bool>> predicate);


        // UPDATE Methods

        /// <summary>
        ///     Asynchronously updates and saves all changes made to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Entity to be updated in Repository.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateAndSaveAsync(T entity, CancellationToken token = default);

        /// <summary>
        ///     Asynchronously updates and saves non-primary keys that were modified for the given <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Entity to be updated in Repository.</param>
        /// <param name="originalEntity">Original entity as it was within the Repository.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateAndSaveDiffAsync(T entity, T originalEntity, CancellationToken token = default);


        Task UpdateRangeAndSaveAsync(IEnumerable<T> entity, CancellationToken token = default);



        // DELETE Methods

        /// <summary>
        ///     Deletes the given <paramref name="entity"/> from the Repository.
        /// </summary>
        /// <param name="entity">Entity to be removed from the Repository.</param>
        void RemoveAndSave(T entity);

        /// <summary>
        ///     Asynchronously deletes the given <paramref name="entity"/> from the Repository.
        /// </summary>
        /// <param name="entity">Entity to be removed from the Repository.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RemoveAndSaveAsync(T entity, CancellationToken token = default);

        /// <summary>
        ///     Deletes the given collection of <paramref name="entities"/> from the Repository.
        /// </summary>
        /// <param name="entities">Collection of entities to be removed from the Repository.</param>
        void RemoveRangeAndSave(IEnumerable<T> entity);

        /// <summary>
        ///     Asynchronously deletes the given collection of <paramref name="entity"/> from the Repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RemoveRangeAndSaveAsync(IEnumerable<T> entity, CancellationToken token = default);

        /// <summary>
        ///     <para>
        ///         Asynchronously get entity with pagination and ascending order  
        ///     </para>        
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetWithPaginationAscSorting(Expression<Func<T, bool>> predicate,
            string columnName, int pageIndex, int pageSize, CancellationToken token = default);

        /// <summary>
        ///     <para>
        ///         Asynchronously get entity with pagination and descending order  
        ///     </para>        
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetWithPaginationDescSorting(Expression<Func<T, bool>> predicate,
            string columnName, int pageIndex, int pageSize, CancellationToken token = default);

        /// <summary>
        ///     <para>
        ///         Asynchronously get entity with includes, pagination and ascending order  
        ///     </para>        
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetWithIncludesPaginationAscSorting(IEnumerable<Expression<Func<T, object>>> includes,
            Expression<Func<T, bool>> predicate, string columnName, int pageIndex, int pageSize, CancellationToken token = default);

        /// <summary>
        ///     <para>
        ///         Asynchronously get entity with includes, pagination and descending order  
        ///     </para>        
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetWithIncludesPaginationDescSorting(IEnumerable<Expression<Func<T, object>>> includes,
            Expression<Func<T, bool>> predicate, string columnName, int pageIndex, int pageSize, CancellationToken token = default);
    }
}
