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
    public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {
        private readonly ApplicationDbContext db;

        public MarcaRepositorio(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Actualizar(Marca marca)
        {
            var MarcaDB = db.Marca.FirstOrDefault(m => m.Id.Equals(marca.Id));
            if (MarcaDB != null)
            {
                MarcaDB.Nombre = marca.Nombre;
            }
        }

        public IEnumerable<SelectListItem> GetListaMarcas()
        {
            return db.Marca.Select(m => new SelectListItem
            {
                Text = m.Nombre,
                Value = m.Id.ToString()
            });
        }
    }
}
