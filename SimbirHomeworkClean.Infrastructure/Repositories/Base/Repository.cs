using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimbirHomeworkClean.Application.Contracts.Repositories.Base;
using SimbirHomeworkClean.Infrastructure.Data;

namespace SimbirHomeworkClean.Infrastructure.Repositories.Base
{
    /// <summary>
    /// Базовый класс репозитория
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public abstract class Repository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        protected readonly MainDbContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        protected Repository(MainDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public virtual async Task<List<T>> GetListAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        /// <inheritdoc />
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <inheritdoc />
        public virtual async Task<T> AddAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public virtual async Task<List<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            var list = entities.ToList();
            foreach (var entity in list)
                _context.Entry(entity).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return list;
        }

        /// <inheritdoc />
        public virtual async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _context.Entry(entity).State = EntityState.Deleted;
            
            await _context.SaveChangesAsync();
        }
    }
}
