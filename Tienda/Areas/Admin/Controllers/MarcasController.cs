using Microsoft.AspNetCore.Mvc;
using Tienda.AccesoDatos.Repositorio.IRepositorio;
using Tienda.Models;

namespace Tienda.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcasController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public MarcasController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Marca marca)
        {
            if (ModelState.IsValid)
            {
                unidadTrabajo.Marca.Add(marca);
                unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }

            return View(marca);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var MarcaDB = unidadTrabajo.Marca.Get(id);
            if (MarcaDB is null)
            {
                return NotFound();
            }
            return View(MarcaDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Marca marca)
        {
            if (ModelState.IsValid)
            {
                unidadTrabajo.Marca.Actualizar(marca);
                unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }

            return View(marca);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data = unidadTrabajo.Marca.GetAll()});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return Json(new {success = false, message = "Error al eliminar la marca"});
            }

            var MarcaDB = unidadTrabajo.Marca.Get(id.GetValueOrDefault());

            unidadTrabajo.Marca.Remover(MarcaDB);
            unidadTrabajo.Guardar();
            return Json(new {success = true, message = "Marca Eliminada Correctamente"});
        }
        #endregion
    }
}
