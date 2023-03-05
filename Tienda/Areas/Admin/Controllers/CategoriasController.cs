using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tienda.AccesoDatos.Repositorio.IRepositorio;
using Tienda.Models;
using Tienda.Utilidades;

namespace Tienda.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Admin)]
    public class CategoriasController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public CategoriasController(IUnidadTrabajo unidadTrabajo)
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
        public IActionResult Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                unidadTrabajo.Categoria.Add(categoria);
                unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var CategoriaDB = unidadTrabajo.Categoria.Get(id);
            if (CategoriaDB is null)
            {
                return NotFound();
            }
            return View(CategoriaDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                unidadTrabajo.Categoria.Actualizar(categoria);
                unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = unidadTrabajo.Categoria.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return Json(new {success = false, message = "Error al eliminar la categoria"});
            }
            var CategoriaDB = unidadTrabajo.Categoria.Get(id.GetValueOrDefault());

            unidadTrabajo.Categoria.Remover(CategoriaDB);
            unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria Eliminada Correctamente" });
        }
        #endregion
    }
}
