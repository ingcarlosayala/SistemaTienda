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
    public class ArticuloRepositorio : Repositorio<Articulo>, IArticuloRepositorio
    {
        private readonly ApplicationDbContext db;

        public ArticuloRepositorio(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Actualizar(Articulo articulo)
        {
            var ArticuloDB = db.Articulo.FirstOrDefault(a => a.Id.Equals(articulo.Id));

            if (ArticuloDB != null)
            {
                ArticuloDB.Codigo= articulo.Codigo;
                ArticuloDB.Nombre= articulo.Nombre;
                ArticuloDB.Descripcion= articulo.Descripcion;
                ArticuloDB.Precio= articulo.Precio;
                ArticuloDB.ImagenUrl= articulo.ImagenUrl;
                ArticuloDB.IdCategoria= articulo.IdCategoria;
                ArticuloDB.IdMarca= articulo.IdMarca;
            }
        }
    }
}
