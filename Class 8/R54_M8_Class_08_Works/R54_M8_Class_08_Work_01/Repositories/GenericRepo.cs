using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using R54_M8_Class_08_Work_01.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.CompilerServices;

namespace R54_M8_Class_08_Work_01.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class, new()
    {
        DbContext db;
        DbSet<T> dbSet;
        public GenericRepo(DbContext db)
        {
            this.db = db;
            this.dbSet = this.db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> data = dbSet;
            if (includes != null) data = includes(data);
            return await data.ToListAsync();
        }

        public async Task<T?> GetById(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> data = dbSet;
            if (includes != null) data = includes(data);
            return await data.FirstOrDefaultAsync(predicate);
        }

        public async Task InsertAsync(T entity)
        {
            await db.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            this.db.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await dbSet.FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<K>> ExecuteSqlCollection<K>(string sql) where K : class
        {

            FormattableString q = FormattableStringFactory.Create(sql);
            var data = await this.db.Set<K>()
            .FromSql(q)
            .ToListAsync();
            if (data != null)
            {
                return  data;
            }
            return new List<K>();

        }

        public async Task<K?> ExecuteSqlSingle<K>(string sql) where K : class
        {
            FormattableString q = FormattableStringFactory.Create(sql);
            var data = await this.db.Set<K>()
            .FromSql(q)
            .FirstOrDefaultAsync();
            
            return data;
        }
    }
}
