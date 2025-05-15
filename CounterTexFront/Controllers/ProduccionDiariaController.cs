using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CounterTexFront.Models;

namespace CounterTexFront.Controllers
{
    public class ProduccionDiariaController : BaseController
    {
        public ActionResult ProduccionDiaria()
        {
            return View();
        }

        // Simulación de una lista de producción (en una aplicación real, esto vendría de una base de datos)
        private static List<ProduccionDiariaViewModel> _producciones = new List<ProduccionDiariaViewModel>()
        {
            new ProduccionDiariaViewModel { Id = "#PD-001", TipoPrenda = "Camiseta", Color = "Azul", Modelo = "Caballero", Talla = "M", Operacion = "Costura", Cantidad = 120, Fecha = new DateTime(2023, 6, 15), Estado = "Completado" },
            new ProduccionDiariaViewModel { Id = "#PD-002", TipoPrenda = "Jogger", Color = "Negro", Modelo = "Dama", Talla = "S", Operacion = "Corte", Cantidad = 85, Fecha = new DateTime(2023, 6, 15), Estado = "En Proceso" },
            new ProduccionDiariaViewModel { Id = "#PD-003", TipoPrenda = "Camisa Crop Top", Color = "Rosado", Modelo = "Dama", Talla = "L", Operacion = "Planchado", Cantidad = 64, Fecha = new DateTime(2023, 6, 14), Estado = "Completado" },
            new ProduccionDiariaViewModel { Id = "#PD-004", TipoPrenda = "Short Drill", Color = "Hueso", Modelo = "Niño", Talla = "XS", Operacion = "Empaque", Cantidad = 42, Fecha = new DateTime(2023, 6, 14), Estado = "En Proceso" },
            new ProduccionDiariaViewModel { Id = "#PD-005", TipoPrenda = "Leggings Juvenil", Color = "Blanco", Modelo = "Dama", Talla = "M", Operacion = "Control de Calidad", Cantidad = 37, Fecha = new DateTime(2023, 6, 13), Estado = "Rechazado" }
        };

        public async Task<ActionResult> IndexAsync()
        {
            // En una aplicación real, aquí podrías realizar operaciones asíncronas
            // como consultas a bases de datos o llamadas a APIs.
            await Task.Delay(1); // Simulación de una operación asíncrona muy corta
            return View(_producciones);
        }

        // GET: /ProduccionDiaria/Create
        public ActionResult Create()
        {
            // Pasar datos para los dropdowns (si no se manejan de otra forma en la vista)
            ViewBag.Colores = new List<string> { "Rosado", "Hueso", "Azul", "Negro", "Blanco" };
            ViewBag.TiposPrenda = new List<string> { "Camiseta", "Jogger", "Camisa Crop Top", "Short Drill", "Leggings Juvenil" };
            ViewBag.Modelos = new List<string> { "Caballero", "Dama", "Niño", "Unisex" };
            ViewBag.Tallas = new List<string> { "XS", "S", "M", "L", "XL", "XXL" };
            ViewBag.Operaciones = new List<string> { "Corte", "Costura", "Planchado", "Empaque", "Control de Calidad" };

            return View();
        }

        // POST: /ProduccionDiaria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ProduccionDiariaViewModel produccion)
        {
            if (ModelState.IsValid)
            {
                // Simular la creación de un ID (en una base de datos sería autogenerado)
                produccion.Id = $"#PD-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}";
                _producciones.Add(produccion);
                // En una aplicación real, aquí podrías guardar en la base de datos de forma asíncrona
                await Task.Delay(1); // Simulación de una operación asíncrona muy corta
                // Redirigir a la página principal o a una confirmación
                return RedirectToAction(nameof(IndexAsync));
            }

            // Si el modelo no es válido, volver a la vista con los errores
            ViewBag.Colores = new List<string> { "Rosado", "Hueso", "Azul", "Negro", "Blanco" };
            ViewBag.TiposPrenda = new List<string> { "Camiseta", "Jogger", "Camisa Crop Top", "Short Drill", "Leggings Juvenil" };
            ViewBag.Modelos = new List<string> { "Caballero", "Dama", "Niño", "Unisex" };
            ViewBag.Tallas = new List<string> { "XS", "S", "M", "L", "XL", "XXL" };
            ViewBag.Operaciones = new List<string> { "Corte", "Costura", "Planchado", "Empaque", "Control de Calidad" };
            return View(produccion);
        }

        // GET: /ProduccionDiaria/Edit/{id}
        public async Task<ActionResult> EditAsync(string id)
        {
            var produccion = _producciones.FirstOrDefault(p => p.Id == id);
            if (produccion == null)
            {
                return new HttpNotFoundResult();
            }

            // En una aplicación real, aquí podrías obtener los datos de forma asíncrona
            await Task.Delay(1); // Simulación de una operación asíncrona muy corta

            ViewBag.Colores = new List<string> { "Rosado", "Hueso", "Azul", "Negro", "Blanco" };
            ViewBag.TiposPrenda = new List<string> { "Camiseta", "Jogger", "Camisa Crop Top", "Short Drill", "Leggings Juvenil" };
            ViewBag.Modelos = new List<string> { "Caballero", "Dama", "Niño", "Unisex" };
            ViewBag.Tallas = new List<string> { "XS", "S", "M", "L", "XL", "XXL" };
            ViewBag.Operaciones = new List<string> { "Corte", "Costura", "Planchado", "Empaque", "Control de Calidad" };

            return View(produccion);
        }

        // POST: /ProduccionDiaria/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(string id, ProduccionDiariaViewModel produccion)
        {
            if (id != produccion.Id)
            {
                return new HttpNotFoundResult();
            }

            if (ModelState.IsValid)
            {
                var existingProduccion = _producciones.FirstOrDefault(p => p.Id == id);
                if (existingProduccion == null)
                {
                    return new HttpNotFoundResult();
                }

                existingProduccion.Color = produccion.Color;
                existingProduccion.TipoPrenda = produccion.TipoPrenda;
                existingProduccion.Modelo = produccion.Modelo;
                existingProduccion.Talla = produccion.Talla;
                existingProduccion.Operacion = produccion.Operacion;
                existingProduccion.Cantidad = produccion.Cantidad;
                existingProduccion.Observaciones = produccion.Observaciones;
                existingProduccion.Estado = produccion.Estado;

                // En una aplicación real, aquí podrías guardar los cambios en la base de datos de forma asíncrona
                await Task.Delay(1); // Simulación de una operación asíncrona muy corta

                return RedirectToAction(nameof(IndexAsync));
            }

            ViewBag.Colores = new List<string> { "Rosado", "Hueso", "Azul", "Negro", "Blanco" };
            ViewBag.TiposPrenda = new List<string> { "Camiseta", "Jogger", "Camisa Crop Top", "Short Drill", "Leggings Juvenil" };
            ViewBag.Modelos = new List<string> { "Caballero", "Dama", "Niño", "Unisex" };
            ViewBag.Tallas = new List<string> { "XS", "S", "M", "L", "XL", "XXL" };
            ViewBag.Operaciones = new List<string> { "Corte", "Costura", "Planchado", "Empaque", "Control de Calidad" };
            return View(produccion);
        }

        // GET: /ProduccionDiaria/Delete/{id}
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var produccion = _producciones.FirstOrDefault(p => p.Id == id);
            if (produccion == null)
            {
                return new HttpNotFoundResult();
            }
            // En una aplicación real, aquí podrías obtener los datos de forma asíncrona
            await Task.Delay(1); // Simulación de una operación asíncrona muy corta
            return View(produccion);
        }

        // POST: /ProduccionDiaria/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync(string id)
        {
            var produccion = _producciones.FirstOrDefault(p => p.Id == id);
            if (produccion != null)
            {
                _producciones.Remove(produccion);
                // En una aplicación real, aquí podrías eliminar de la base de datos de forma asíncrona
                await Task.Delay(1); // Simulación de una operación asíncrona muy corta
            }
            return RedirectToAction(nameof(IndexAsync));
        }

        // GET: /ProduccionDiaria/Details/{id}
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var produccion = _producciones.FirstOrDefault(p => p.Id == id);
            if (produccion == null)
            {
                return new HttpNotFoundResult();
            }
            // En una aplicación real, aquí podrías obtener los datos de forma asíncrona
            await Task.Delay(1); // Simulación de una operación asíncrona muy corta
            return View(produccion);
        }
    }
}