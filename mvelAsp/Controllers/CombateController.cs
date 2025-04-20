using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;

namespace mvelAsp.Controllers
{
    public class CombateController : Controller
    {
        public ActionResult puntuarCombate()
        {
            List<clsPersonaje> listadoPersonajes = clsDalBDD.ObtenerPersonajes();
            return View(listadoPersonajes);
        }


        public ActionResult enviarPuntuacion(int idPersonaje1, int idPersonaje2, int puntuacion1, int puntuacion2)
        {
            if (clsDalBDD.insertarCombate(idPersonaje1, idPersonaje2, puntuacion1, puntuacion2))
            {
                return RedirectToAction("Home/Index");
            }
            else
            {
                return RedirectToAction("Shared/Error");
            }
        }

    }
}
