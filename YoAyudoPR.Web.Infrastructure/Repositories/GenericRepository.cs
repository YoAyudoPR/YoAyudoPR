using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using YouAyudoPR.Web.Domain.Repositories;
using YoAyudoPR.Web.Infrastructure.Extensions;

namespace YoAyudoPR.Web.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal DbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }




        // CREATE Methods
        public async Task AddAndSaveAsync(T entity, CancellationToken token = default)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync(token);
        }

        public async Task AddRangeAndSaveAsync(IEnumerable<T> entities, CancellationToken token = default)
        {
            dbSet.AddRange(entities);
            await context.SaveChangesAsync(token);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression, CancellationToken token = default)
        {
            return await dbSet.CountAsync(expression, token);
        }


        // READ Methods
        public T FirstByCondition(Expression<Func<T, bool>> predicate)
        {
            return dbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public async Task<T> FirstByConditionAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
        {
            return await dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, token);
        }

        public async Task<int?> FirstByConditionAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, int>> selector,
            CancellationToken token = default)
        {
            return await dbSet.AsNoTracking()
                .Where(predicate)
                .Select(selector)
                .FirstAsync(token);
        }

        public async Task<Guid?> FirstByConditionAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, Guid>> selector,
            CancellationToken token = default)
        {
            return await dbSet.AsNoTracking()
                .Where(predicate)
                .Select(selector)
                .FirstAsync(token);
        }

        public async Task<T> FirstByConditionTrackingAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
        {
            return await dbSet.FirstOrDefaultAsync(predicate, token);
        }

        public T FirstUsingIncludes(IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate)
        {
            return includes.Aggregate(
                    // The initial accumulator value.
                    seed: dbSet.AsQueryable(),
                    // An accumulator funcion to be invoked on each element.
                    func: (entity, navigationProperty) => entity.Include(navigationProperty)
                ).Where(predicate)
                .FirstOrDefault();
        }

        public async Task<T> FirstUsingIncludesAsync(IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate, CancellationToken token)
        {
            return await includes.Aggregate(
                    // The initial accumulator value.
                    seed: dbSet.AsQueryable(),
                    // An accumulator funcion to be invoked on each element.
                    func: (entity, navigationProperty) => entity.Include(navigationProperty)
                ).Where(predicate)
                .FirstOrDefaultAsync(token);
        }

        public async Task<T> FindByIdAsync(int id, CancellationToken token = default)
        {
            return await dbSet.FindAsync(new object[] { id }, token);
        }

        public async Task<T> FindByIdAsync(object[] keyValues, CancellationToken token = default)
        {
            return await dbSet.FindAsync(keyValues, token);
        }

        public async Task<T> FindByGuidAsync(Guid guid, CancellationToken token = default)
        {
            return await dbSet.FindAsync(new object[] { guid }, token);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate, CancellationToken token = default)
        {
            return dbSet.Where(predicate).AsNoTracking().ToList();
        }

        public IEnumerable<T> FindAll(CancellationToken token = default)
        {
            return dbSet.AsNoTracking().ToList();
        }

        public IQueryable<T> FindAllAsIQueryable(CancellationToken token = default)
        {
            return dbSet.AsNoTracking();
        }

        public async Task<IEnumerable<T>> FindAllAsync(CancellationToken token = default)
        {
            return await dbSet.AsNoTracking().ToListAsync(token);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync(token);
        }

        public IEnumerable<T> GetUsingIncludes(IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate)
        {
            return includes.Aggregate(
                    // The initial accumulator value.
                    seed: dbSet.AsQueryable(),
                    // An accumulator funcion to be invoked on each element.
                    func: (query, navigationProperty) => query.Include(navigationProperty)
                ).Where(predicate)
                .ToList();
        }

        public async Task<IEnumerable<T>> GetUsingIncludes(IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate, CancellationToken token)
        {
            return await includes.Aggregate(
                    // The initial accumulator value.
                    seed: dbSet.AsQueryable(),
                    // An accumulator funcion to be invoked on each element.
                    func: (query, navigationProperty) => query.Include(navigationProperty)
                ).Where(predicate)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<T>> GetUsingIncludesWithPagination(
            IEnumerable<Expression<Func<T, object>>> includes, Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderByExpression,
            int pageIndex,
            int pageSize,
            CancellationToken token
        )
        {
            return await includes.Aggregate(
                    // The initial accumulator value.
                    seed: dbSet.AsQueryable(),
                    // An accumulator funcion to be invoked on each element.
                    func: (query, navigationProperty) => query.Include(navigationProperty)
                ).Where(predicate)
                .OrderBy(orderByExpression)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
        }

        public IEnumerable<T> GetUsingIncludes(IEnumerable<string> includes, Expression<Func<T, bool>> predicate)
        {
            // convert the DbSet object into a Queryable to allow appending to the generated Select statement
            var query = dbSet.AsQueryable();

            // loop through all the navigation properties in the T entity and append the JOIN clause 
            // property name must be passed in correctly 
            foreach (string navigationProperty in includes)
            {
                query = query.Include(navigationProperty);
            }

            // return the final query
            return query.Where(predicate);
        }




        // UPDATE Methods

        public async Task UpdateAndSaveAsync(T entity, CancellationToken token = default)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(token);
        }

        public async Task UpdateAndSaveDiffAsync(T entity, T originalEntity, CancellationToken token = default)
        {
            var entry = context.Entry(entity);
            var ogEntry = context.Entry(originalEntity);
            var entityDB = entry.GetDatabaseValues(); // Old field values from the database

            // Filters properties that are not primary keys
            bool filterPrimaryKeys(IProperty property)
            {
                //Debug.WriteLine(property.Name + " is primary key? " + property.IsPrimaryKey());
                return property.IsPrimaryKey() == false;
            }

            List<IProperty> filteredProps = entry.OriginalValues.Properties.Where(filterPrimaryKeys).ToList();

            // Iterates through each property to be updated
            foreach (var property in filteredProps)
            {
                var dbValue = entityDB[property.Name];
                // Get the current value from posted edit page.
                var modifiedValue = entry.CurrentValues[property.Name];
                var unmodifieldValue = ogEntry.CurrentValues[property.Name];

                if (!Equals(dbValue, modifiedValue) && Equals(dbValue, unmodifieldValue))
                {
                    entry.Property(property.Name).IsModified = true;
                }
            }

            await context.SaveChangesAsync(token);
        }

        public async Task UpdateRangeAndSaveAsync(IEnumerable<T> entity, CancellationToken token = default)
        {
            context.Set<T>().UpdateRange(entity);
            await context.SaveChangesAsync(token);
        }




        // DELETE Methods

        public void RemoveAndSave(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public async Task RemoveAndSaveAsync(T entity, CancellationToken token = default)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync(token);
        }

        public void RemoveRangeAndSave(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            context.SaveChanges();
        }

        public async Task RemoveRangeAndSaveAsync(IEnumerable<T> entity, CancellationToken token = default)
        {
            context.Set<T>().RemoveRange(entity);
            await context.SaveChangesAsync(token);
        }

        // IDisposable implementation
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<T>> GetWithPaginationAscSorting(Expression<Func<T, bool>> predicate,
            string columnName,
            int pageIndex,
            int pageSize,
            CancellationToken token
        )
        {
            return await dbSet.Where(predicate)
                .OrderBy(columnName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<T>> GetWithPaginationDescSorting(Expression<Func<T, bool>> predicate,
            string columnName,
            int pageIndex,
            int pageSize,
            CancellationToken token
        )
        {
            return await dbSet.Where(predicate)
                .OrderByDescending(columnName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<T>> GetWithIncludesPaginationAscSorting(
            IEnumerable<Expression<Func<T, object>>> includes,
            Expression<Func<T, bool>> predicate,
            string columnName,
            int pageIndex,
            int pageSize,
            CancellationToken token
        )
        {
            return await includes.Aggregate(
                    // The initial accumulator value.
                    seed: dbSet.AsQueryable(),
                    // An accumulator funcion to be invoked on each element.
                    func: (query, navigationProperty) => query.Include(navigationProperty)
                ).Where(predicate)
                .OrderBy(columnName)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<T>> GetWithIncludesPaginationDescSorting(
            IEnumerable<Expression<Func<T, object>>> includes,
            Expression<Func<T, bool>> predicate,
            string columnName,
            int pageIndex,
            int pageSize,
            CancellationToken token
        )
        {
            return await includes.Aggregate(
                    // The initial accumulator value.
                    seed: dbSet.AsQueryable(),
                    // An accumulator funcion to be invoked on each element.
                    func: (query, navigationProperty) => query.Include(navigationProperty)
                ).Where(predicate)
                .OrderByDescending(columnName)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(token);
        }
    }
}
