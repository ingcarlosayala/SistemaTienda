using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Models.ViewsModels
{
    public class HomeVM
    {
        public IEnumerable<Articulo> ListaArtciculos { get; set; }
        public IEnumerable<Marca> ListaMarcas { get; set; }
        public IEnumerable<SelectListItem> ListaCategoria { get; set; }
    }
}
