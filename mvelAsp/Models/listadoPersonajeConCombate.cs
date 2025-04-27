using DAL;
using ENT;

namespace mvelAsp.Models
{
    public class listadoPersonajeConCombate : clsCombate
    {
        private List<clsPersonaje> listaPersonaje;
        public List<clsPersonaje> ListaPersonaje
        {
            get { return listaPersonaje; }
            //set { listaPersonaje = value; }
        }


        public listadoPersonajeConCombate(int idCombate, DateTime fechaCombate, int idPersonaje1, int idPersonaje2, int puntuacion1, int puntuacion2) : base(idCombate, fechaCombate, idPersonaje1, idPersonaje2, puntuacion1, puntuacion2)
        {
            this.listaPersonaje = clsDalBDD.ObtenerPersonajes();
        }

        public listadoPersonajeConCombate()
        {
            this.listaPersonaje = clsDalBDD.ObtenerPersonajes();
        }


    }
}