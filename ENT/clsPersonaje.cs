using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsPersonaje
    {
        private int id;
        private string nombre;
        private string foto; 

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        public clsPersonaje(int id, string nombre, string foto)
        {
            this.id = id;
            this.nombre = nombre;
            this.foto = foto;
        }

        public clsPersonaje(int id, string nombre )
        {
            this.id = id;
            this.nombre = nombre;
        }

        public clsPersonaje()
        {

        }
    }
}
