using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;
using mvelAsp.Models;

namespace mvelAsp.Controllers
{
    public class CombateController : Controller
    {
        public ActionResult puntuarCombate()
        {
            listadoPersonajeConCombate personajeConCombate = new listadoPersonajeConCombate();
            return View(personajeConCombate);
        }

        [HttpPost]
        public ActionResult puntuarCombate(clsCombate combateActual)
        {
            combateActual.FechaCombate = DateTime.Now;
            if (clsDalBDD.insertarCombate(combateActual))
            {
                //return RedirectToAction("Home/Index");
            }
            else
            {
                return RedirectToAction("Shared/Error");
            }

            listadoPersonajeConCombate personajeConCombate = new listadoPersonajeConCombate(combateActual.IdCombate,combateActual.FechaCombate,combateActual.IdPersonaje1,combateActual.IdPersonaje2,combateActual.Puntuacion1,combateActual.Puntuacion2);
            return View(personajeConCombate);

        }
        

    }
}
