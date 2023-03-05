using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T,bool>> filtro = null,
            Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedad = null
        );
        T GetFirstOrdeFault(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedad = null
        );
        void Add(T item);
        void Remover(int id);
        void Remover(T item);
    }
}
