using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo:IDisposable
    {
        ICategoriaRepositorio Categoria { get; }
        IMarcaRepositorio Marca { get; }
        IArticuloRepositorio Articulo { get; }
        void Guardar();
    }
}
