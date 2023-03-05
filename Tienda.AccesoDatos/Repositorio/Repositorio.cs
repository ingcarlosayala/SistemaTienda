using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tienda.AccesoDatos.Data;
using Tienda.AccesoDatos.Repositorio.IRepositorio;
using Tienda.Models;

namespace Tienda.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }
        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedad = null)
        {
            IQueryable<T> query = dbSet.AsQueryable();

            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            else if (incluirPropiedad != null)
            {
                foreach (var item in incluirPropiedad.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            else if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirstOrdeFault(Expression<Func<T, bool>> filtro = null, string incluirPropiedad = null)
        {
            IQueryable<T> query = dbSet.AsQueryable();

            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            else if (incluirPropiedad != null)
            {
                foreach (var item in incluirPropiedad.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remover(int id)
        {
            T entidad = dbSet.Find(id);
            Remover(entidad);
        }

        public void Remover(T item)
        {
            dbSet.Remove(item);
        }
    }
}
