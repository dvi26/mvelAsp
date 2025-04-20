using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsCombate
    {
        private int idCombate;
        private DateTime fechaCombate;
        private int idPersonaje1;
        private int idPersonaje2;
        private int puntuacion;

        public int IdCombate
        {
            get { return idCombate; }
            set { idCombate = value; }
        }
        public DateTime FechaCombate
        {
            get { return fechaCombate; }
            set { fechaCombate = value; }
        }
        public int IdPersonaje1
        {
            get { return idPersonaje1; }
            set { idPersonaje1 = value; }
        }
        public int IdPersonaje2
        {
            get { return idPersonaje2; }
            set { idPersonaje2 = value; }
        }
        public int Puntuacion
        {
            get { return puntuacion; }
            set { puntuacion = value; }
        }
        public clsCombate(int idCombate, DateTime fechaCombate, int idPersonaje1, int idPersonaje2, int puntuacion)
        {
            this.idCombate = idCombate;
            this.fechaCombate = fechaCombate;
            this.idPersonaje1 = idPersonaje1;
            this.idPersonaje2 = idPersonaje2;
            this.puntuacion = puntuacion;
        }
    }
}
