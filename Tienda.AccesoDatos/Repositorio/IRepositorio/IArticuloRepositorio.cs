using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda.Models;

namespace Tienda.AccesoDatos.Repositorio.IRepositorio
{
    public interface IArticuloRepositorio:IRepositorio<Articulo>
    {
        void Actualizar(Articulo articulo);
    }
}
