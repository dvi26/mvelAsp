using DAL;
using ENT;
using Microsoft.AspNetCore.Mvc;
using mvelAsp.Models;

namespace mvelAsp.Controllers
{
    public class CombateController : Controller
    {
        /// <summary>
        /// Muestra la clasificación de los personajes ordenada por la puntuación total de forma descendente.
        /// </summary>
        /// <returns>Vista con la lista de personajes ordenados por su puntuación total.</returns>
        public ActionResult verClasificacion()
        {
            try
            {
                var listaPersonajesConPuntuacion = clsDalBDD.ObtenerPersonajes()
                    .Select(p => new clsPersonajeConPuntuacion(p.Id, p.Nombre))
                    .OrderByDescending(p => p.TotalPuntuaciones)
                    .ToList();

                return View(listaPersonajesConPuntuacion);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un error al cargar la clasificación: " + ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Muestra la vista para puntuar un combate, inicializando un nuevo objeto de tipo listadoPersonajeConCombate.
        /// </summary>
        /// <returns>Vista para puntuar un combate.</returns>
        public ActionResult puntuarCombate()
        {
            listadoPersonajeConCombate personajeConCombate = new listadoPersonajeConCombate();
            return View(personajeConCombate);
        }

        /// <summary>
        /// Recibe la puntuación de un combate y la procesa para ser guardada o actualizada en la base de datos.
        /// Verifica si los personajes son el mismo y si no lo son, inserta o actualiza los datos de la puntuación.
        /// </summary>
        /// <param name="combateActual">Objeto clsCombate con los detalles del combate a puntuar.</param>
        /// <returns>Vista de resultados con mensaje de éxito o error.</returns>
        [HttpPost]
        public ActionResult puntuarCombate(clsCombate combateActual)
        {
            combateActual.FechaCombate = DateTime.Now;
            listadoPersonajeConCombate personajeConCombate = new listadoPersonajeConCombate(combateActual.IdCombate, combateActual.FechaCombate, combateActual.IdPersonaje1, combateActual.IdPersonaje2, combateActual.Puntuacion1, combateActual.Puntuacion2);
            try
            {
                if (combateActual.IdPersonaje1 == combateActual.IdPersonaje2)
                {
                    ViewBag.Error = "Un personaje no puede luchar consigo mismo.";
                }
                else
                {
                    if (clsDalBDD.insertarCombate(combateActual))
                    {
                        ViewBag.Exito = "Se ha puntuado correctamente.";
                    }
                    else
                    {
                        if (clsDalBDD.updateCombate(combateActual))
                        {
                            ViewBag.Exito = "Se han sumado las puntuaciones correctamente.";
                        }
                        else
                        {
                            ViewBag.Error = "Ha ocurrido un error al sumar las puntuaciones al combate existente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View(personajeConCombate);
            }

            return View(personajeConCombate);
        }
    }
}
