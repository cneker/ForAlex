using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryBase<T> where T : class
    {
        protected RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll(bool trackChanges) =>
            !trackChanges ?
                _context.Set<T>().AsNoTracking() :
                _context.Set<T>();
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
                _context.Set<T>().Where(expression).AsNoTracking() :
                _context.Set<T>().Where(expression);
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);

    }
}
