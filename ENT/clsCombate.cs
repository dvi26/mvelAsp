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
        private int puntuacion1;
        private int puntuacion2;

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
        public int Puntuacion1
        {
            get { return puntuacion1; }
            set { puntuacion1 = value; }
        }
        public int Puntuacion2
        {
            get { return puntuacion2; }
            set { puntuacion2 = value; }
        }
        
        public clsCombate(int idCombate, DateTime fechaCombate, int idPersonaje1, int idPersonaje2, int puntuacion1,int puntuacion2)
        {
            this.idCombate = idCombate;
            this.fechaCombate = fechaCombate;
            this.idPersonaje1 = idPersonaje1;
            this.idPersonaje2 = idPersonaje2;
            this.puntuacion1 = puntuacion1;
            this.puntuacion2 = puntuacion2;

        }
        public clsCombate()
        {

        }
    }
}
