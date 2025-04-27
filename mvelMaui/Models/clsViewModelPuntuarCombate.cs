using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ApiMaui.Resources;
using DAL;
using ENT;

namespace mvelMaui.Models
{
    public class clsViewModelPuntuarCombate: INotifyPropertyChanged
    {
        private clsPersonaje personaje1;
        private clsPersonaje personaje2;
        private int puntuacion1;
        private int puntuacion2;
        private List<clsPersonaje> personajes;
        private DelegateCommand puntuacionCommand;
        private clsCombate combateExistente;

        public List<clsPersonaje> Personajes
        {
            get { return personajes; }
            private set
            {
                personajes = value;
                //NotifyPropertyChanged();
            }

        }
        public void cargarListado()
        {
            Personajes = clsDalBDD.ObtenerPersonajes();
        }
        public clsViewModelPuntuarCombate()
        {
            cargarListado();
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
