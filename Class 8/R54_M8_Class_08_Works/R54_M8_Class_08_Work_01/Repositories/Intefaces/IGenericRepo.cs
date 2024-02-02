using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace R54_M8_Class_08_Work_01.Repositories.Intefaces
{
    public interface IGenericRepo<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        Task<T?> GetById(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        Task InsertAsync (T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<K?> ExecuteSqlSingle<K>(string sql) where K:class;
        Task<IEnumerable<K>> ExecuteSqlCollection<K>(string sql) where K : class;
    }
}
