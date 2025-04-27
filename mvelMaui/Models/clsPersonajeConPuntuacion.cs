using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENT;

namespace mvelMaui.Models
{
    public class clsPersonajeConPuntuacion : clsPersonaje, INotifyPropertyChanged
    {
        private int puntuacion;
        public int Puntuacion
        {
            get { return puntuacion; }
            set
            {
                puntuacion = value;
                NotifyPropertyChanged();
            }
        }
        public clsPersonajeConPuntuacion(int id, string nombre, int puntuacion)
        : base(id, nombre)
        {

            Puntuacion = puntuacion;
        }
        public clsPersonajeConPuntuacion()
        {
            
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
