using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda.AccesoDatos.Data;
using Tienda.AccesoDatos.Repositorio.IRepositorio;
using Tienda.Models;

namespace Tienda.AccesoDatos.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext db;

        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Actualizar(Categoria categoria)
        {
            var CategoriaDB = db.Categoria.FirstOrDefault(c => c.Id.Equals(categoria.Id));
            if (CategoriaDB != null)
            {
                CategoriaDB.Nombre = categoria.Nombre;
            }
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return db.Categoria.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });
        }
    }
}
