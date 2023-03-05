using Microsoft.AspNetCore.Mvc;
using Tienda.AccesoDatos.Repositorio.IRepositorio;
using Tienda.Models;
using Tienda.Models.ViewsModels;

namespace Tienda.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;
        private readonly IWebHostEnvironment hostEnvironment;

        public ArticulosController(IUnidadTrabajo unidadTrabajo,IWebHostEnvironment hostEnvironment)
        {
            this.unidadTrabajo = unidadTrabajo;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Crear()
        {
            ArticuloVM articuloVM = new ArticuloVM()
            {
                Articulo = new Articulo(),
                ListaCategorias = unidadTrabajo.Categoria.GetListaCategorias(),
                ListaMarcas = unidadTrabajo.Marca.GetListaMarcas()
            };
            return View(articuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(ArticuloVM articuloVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (articuloVM.Articulo.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subida = Path.Combine(rutaPrincipal, @"imagenes\articulo");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStrems = new FileStream(Path.Combine(subida,nombreArchivo + extension),FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStrems);
                    }

                    articuloVM.Articulo.FechaCreacion = DateTime.Now.ToString("d");
                    articuloVM.Articulo.ImagenUrl = @"\imagenes\articulo\" + nombreArchivo + extension;

                    unidadTrabajo.Articulo.Add(articuloVM.Articulo);
                    unidadTrabajo.Guardar();
                    return RedirectToAction(nameof(Index));
                }
            }
            articuloVM.ListaCategorias = unidadTrabajo.Categoria.GetListaCategorias();
            articuloVM.ListaMarcas = unidadTrabajo.Marca.GetListaMarcas();

            return View(articuloVM);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            ArticuloVM articuloVM = new ArticuloVM()
            {
                Articulo = new Articulo(),
                ListaCategorias = unidadTrabajo.Categoria.GetListaCategorias(),
                ListaMarcas = unidadTrabajo.Marca.GetListaMarcas()
            };

            if (id == 0)
            {
                return NotFound();
            }
            articuloVM.Articulo = unidadTrabajo.Articulo.Get(id);
            return View(articuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ArticuloVM articuloVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var ArticuloDB = unidadTrabajo.Articulo.Get(articuloVM.Articulo.Id);

                if (archivos.Count() > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subida = Path.Combine(rutaPrincipal, @"imagenes\articulo");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var Nuevaextension = Path.GetExtension(archivos[0].FileName);

                    var ImagenDB = Path.Combine(rutaPrincipal, ArticuloDB.ImagenUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(ImagenDB))
                    {
                        System.IO.File.Delete(ImagenDB);
                    }

                    using (var fileStrims = new FileStream(Path.Combine(subida, nombreArchivo + Nuevaextension),FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStrims);
                    }

                    articuloVM.Articulo.ImagenUrl = @"\imagenes\articulo\"+ nombreArchivo + Nuevaextension;

                    unidadTrabajo.Articulo.Actualizar(articuloVM.Articulo);
                    unidadTrabajo.Guardar();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    articuloVM.Articulo.ImagenUrl = ArticuloDB.ImagenUrl;
                }
                unidadTrabajo.Articulo.Actualizar(articuloVM.Articulo);
                unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            articuloVM.ListaCategorias = unidadTrabajo.Categoria.GetListaCategorias();
            articuloVM.ListaMarcas = unidadTrabajo.Marca.GetListaMarcas();
            return View(articuloVM);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = unidadTrabajo.Articulo.GetAll(incluirPropiedad: "Categoria,Marca") });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return Json(new {success = false, message = "Error al eliminar el articulo"});
            }

            string rutaPrincipal = hostEnvironment.WebRootPath;
            var articuloDB = unidadTrabajo.Articulo.Get(id.GetValueOrDefault());
            var imagenDB = Path.Combine(rutaPrincipal, articuloDB.ImagenUrl.TrimStart('\\'));

            if (System.IO.File.Exists(imagenDB))
            {
                System.IO.File.Delete(imagenDB);
            }

            unidadTrabajo.Articulo.Remover(articuloDB);
            unidadTrabajo.Guardar();
            return Json(new {success = true, message = "Articulo Eliminido Correctamente"});
        }
        #endregion
    }
}
