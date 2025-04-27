using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using ENT;

namespace mvelMaui.Models
{
    public class clsViewModelTablaClasificacion
    {
        public ObservableCollection<clsPersonajeConPuntuacion> TablaClasificacion { get; set; }

        public clsViewModelTablaClasificacion()
        {
            CargarTablaClasificacion();
        }

        private void CargarTablaClasificacion()
        {
            var personajes = clsDalBDD.ObtenerPersonajes();
            var combates = clsDalBDD.ObtenerCombates(); 

            var clasificacion = personajes.Select(p => new clsPersonajeConPuntuacion
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Foto = p.Foto,
                Puntuacion = combates
                    .Where(c => c.IdPersonaje1 == p.Id || c.IdPersonaje2 == p.Id)
                    .Sum(c => (c.IdPersonaje1 == p.Id ? c.Puntuacion1 : c.Puntuacion2)) 
            })
            .OrderByDescending(p => p.Puntuacion) 
            .ToList();

            TablaClasificacion = new ObservableCollection<clsPersonajeConPuntuacion>(clasificacion);
        }
    }
}
