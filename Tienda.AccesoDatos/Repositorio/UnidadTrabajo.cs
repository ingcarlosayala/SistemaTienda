using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda.AccesoDatos.Data;
using Tienda.AccesoDatos.Repositorio.IRepositorio;

namespace Tienda.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext db;

        public ICategoriaRepositorio Categoria { get; private set; }
        public IMarcaRepositorio Marca { get; private set; }
        public IArticuloRepositorio Articulo { get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            this.db = db;
            Categoria = new CategoriaRepositorio(db);
            Marca = new MarcaRepositorio(db);
            Articulo = new ArticuloRepositorio(db);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void Guardar()
        {
            db.SaveChanges();
        }
    }
}
