using DAL;
using ENT;

namespace mvelAsp.Models
{
    public class clsPersonajeConPuntuacion : clsPersonaje
    {
        private int totalPuntuaciones;
        private List<clsCombate> listaCombates;

        public int TotalPuntuaciones
        {
            get { return totalPuntuaciones; }
            set { totalPuntuaciones = value; }
        }

        public int ListaCombates
        {
            get { return totalPuntuaciones; }
            //set { totalPuntuaciones = value; }
        }
        public clsPersonajeConPuntuacion(int id, string nombre) : base(id, nombre)
        {
            this.listaCombates = clsDalBDD.ObtenerCombates();

            var combatesPersonaje = this.listaCombates.Where(c => c.IdPersonaje1 == id || c.IdPersonaje2 == id).ToList();

            TotalPuntuaciones = combatesPersonaje.Sum(c =>
                (c.IdPersonaje1 == id ? c.Puntuacion1 : c.Puntuacion2));
        }
        public clsPersonajeConPuntuacion()
        {
            this.listaCombates = clsDalBDD.ObtenerCombates();
        }
    }
}