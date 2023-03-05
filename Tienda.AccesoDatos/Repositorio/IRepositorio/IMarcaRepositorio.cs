using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tienda.Models;

namespace Tienda.AccesoDatos.Repositorio.IRepositorio
{
    public interface IMarcaRepositorio:IRepositorio<Marca>
    {
        void Actualizar(Marca marca);
        IEnumerable<SelectListItem> GetListaMarcas();
    }
}
