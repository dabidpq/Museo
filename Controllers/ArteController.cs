using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Museo.Models;

namespace Museo.Controllers
{
    public class ArteController : Controller
    {
        private Service service;

        public ArteController()
        {
            service = new Service();
        }
        // GET: ArteController
        public ActionResult Index()
        {
            var obras = service.MostrarObras();
            return View(obras);
        }

        // GET: ArteController/Details/5
        public ActionResult Details(int id)
        {
            var obra = service.ObtenerObraPorId(id);
            if (obra == null)
            {
                return NotFound();
            }
            return View(obra);
        }

        // GET: ArteController/Create
        public ActionResult Create()
        {
          return View();
        }

        // POST: ArteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Arte obra)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (obra.FechaRegistro == default)
                    {
                        obra.FechaRegistro = DateOnly.FromDateTime(DateTime.Now);
                    }

                    // Asignar valores automaticos

                    obra.Codigo = obra.GenerarCodigo();
                    obra.PrecioFinal = obra.CalcularPrecioFinal();

                    service.AgregarObra(obra);

                    
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "Error al guardar: " + ex.Message);

 
            }
            return View(obra);
        }
        // GET: ArteController/Edit/5
        public ActionResult Edit(int id)
        {
            var obra = service.ObtenerObraPorId(id);
            if (obra == null)
            {
                return NotFound();
            }

            return View(obra);
        }

        // POST: ArteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Arte obra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    obra.Id = id;
     
                    // Recalcular precio final antes de guardar
                    obra.PrecioFinal = obra.CalcularPrecioFinal();

                    service.EditarObra(obra);
                    return RedirectToAction(nameof(Index));
                }

                return View(obra);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar: " + ex.Message);
                return View(obra);
            }
        }

        // GET: ArteController/Delete/5
        public ActionResult Delete(int id)
        {
            var obra = service.ObtenerObraPorId(id);
            if (obra == null)
            {
                return NotFound();
            }
            return View(obra);
        }

        // POST: ArteController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                service.EliminarObra(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
