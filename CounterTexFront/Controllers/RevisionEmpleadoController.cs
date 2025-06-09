using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CounterTexFront.Controllers
{
    public class RevisionEmpleadoController : BaseController
    {
        // GET: RevisionEmpleado
        public ActionResult RevisionEmpleado()
        {
            return View();
        }
    

 
        // [HttpPost("Revisar")]
        //public async Task<ActionResult> RevisarPrenda([FromBody] RevisionPrendaDTO revision)
        //{
        //    var prenda = await _context.Prendas.FindAsync(revision.PrendaId);
        //    if (prenda == null)
        //        return NotFound();

        //    prenda.Estado = revision.Estado;
        //    prenda.Comentarios = revision.Comentarios;

        //    await _context.SaveChangesAsync();
        //    return Ok(prenda);
        //}

    }
}