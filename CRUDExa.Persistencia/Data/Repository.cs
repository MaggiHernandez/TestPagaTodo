using CRUDExa.Persistencia.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUDExa.Persistencia.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly string _urlBase = "";

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Método para agregar una nueva entidad al DbSet.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity) => _dbSet.Add(entity);

        /// <summary>
        /// Método para obtener una entidad por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id) => _dbSet.Find(id);

        /// <summary>
        /// Método para obtener una lista de entidades con opciones de filtro, ordenación y propiedades incluidas.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null) query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            if (orderBy != null) return orderBy(query).ToList();

            return query;
        }

        /// <summary>
        /// Método para obtener la primera entidad que coincide con un filtro, con opciones para incluir propiedades relacionadas.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public T? GetFirtsOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null) query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (string item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Método para eliminar una entidad por su ID.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            T entityToRemove = _dbSet.Find(id);
            Remove(entityToRemove);
        }

        /// <summary>
        /// Método para eliminar una entidad específica.
        /// </summary>
        /// <param name="entityToRemove"></param>
        public void Remove(T entityToRemove) => _dbSet.Remove(entityToRemove);
    }
}
