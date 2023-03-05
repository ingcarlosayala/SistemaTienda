using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tienda.AccesoDatos.Repositorio.IRepositorio;
using Tienda.Models;
using Tienda.Models.ViewsModels;

namespace Tienda.Areas.Tienda.Controllers
{
    [Area("Tienda")]
    public class HomeController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public HomeController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                ListaArtciculos = unidadTrabajo.Articulo.GetAll(incluirPropiedad:"Categoria,Marca").Take(8),
                ListaCategoria = unidadTrabajo.Categoria.GetListaCategorias(),
                ListaMarcas = unidadTrabajo.Marca.GetAll()
            };
            return View(homeVM);
        }

        [HttpPost]
        public IActionResult Index(int categoria, int filtro)
        {
            HomeVM homeVM = new HomeVM();

            if (categoria > 0 && filtro == 0)
            {
                homeVM = new HomeVM()
                {
                    ListaMarcas = unidadTrabajo.Marca.GetAll(),
                    ListaCategoria = unidadTrabajo.Categoria.GetListaCategorias(),
                    ListaArtciculos = unidadTrabajo.Articulo.GetAll(incluirPropiedad: "Categoria,Marca")
                                                            .Where(c => c.IdCategoria == categoria)
                };
            }
            else if (categoria == 0 && filtro > 0)
            {
                homeVM = new HomeVM()
                {
                    ListaMarcas = unidadTrabajo.Marca.GetAll(),
                    ListaCategoria = unidadTrabajo.Categoria.GetListaCategorias(),
                    ListaArtciculos = unidadTrabajo.Articulo.GetAll(incluirPropiedad: "Categoria,Marca")
                                                            .Where(c => c.Codigo == filtro)
                };
            }
            else if (categoria > 0 && filtro > 0)
            {
                homeVM = new HomeVM()
                {
                    ListaMarcas = unidadTrabajo.Marca.GetAll(),
                    ListaCategoria = unidadTrabajo.Categoria.GetListaCategorias(),
                    ListaArtciculos = unidadTrabajo.Articulo.GetAll(incluirPropiedad: "Categoria,Marca")
                                                            .Where(c => c.Codigo == filtro && c.IdCategoria == categoria)
                };
            }
            else if (categoria == 0 && filtro == 0)
            {
                homeVM = new HomeVM()
                {
                    ListaMarcas = unidadTrabajo.Marca.GetAll(),
                    ListaCategoria = unidadTrabajo.Categoria.GetListaCategorias(),
                    ListaArtciculos = unidadTrabajo.Articulo.GetAll(incluirPropiedad: "Categoria,Marca").Take(8)
                };
            }

            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            Articulo articulo = new Articulo();

            articulo = unidadTrabajo.Articulo.Get(id.GetValueOrDefault());

            return View(articulo);
        }

        [HttpGet]
        public IActionResult Productos()
        {
            HomeVM homeVM = new HomeVM()
            {
                ListaArtciculos = unidadTrabajo.Articulo.GetAll(incluirPropiedad: "Categoria,Marca"),
                ListaCategoria = unidadTrabajo.Categoria.GetListaCategorias(),
                ListaMarcas = unidadTrabajo.Marca.GetAll(),
            };

            return View(homeVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}