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
        //He creado dos listas para no tener que crear Puntuacion1/Puntuacion 2 en el modelo
        private ObservableCollection<clsPersonajeConPuntuacion> personajesIzquierda;

        private ObservableCollection<clsPersonajeConPuntuacion> personajesDerecha;

        public ObservableCollection<clsPersonajeConPuntuacion> PersonajesIzquierda
        {
            get { return personajesIzquierda; }
            set
            {
                personajesIzquierda = value;
                NotifyPropertyChanged(); 
            }
        }

        public ObservableCollection<clsPersonajeConPuntuacion> PersonajesDerecha
        {
            get { return personajesDerecha; }
            set
            {
                personajesDerecha = value;
                NotifyPropertyChanged(); 
            }
        }

        private DelegateCommand puntuacionCommand;

        public DelegateCommand PuntuacionCommand
        {
            get { return puntuacionCommand; }
        }

        /// <summary>
        /// //Execute del boton que hara la logica y comprobaciones de el ejercicio
        /// </summary>
        private async void PuntuacionCommandExecuted()
        {
            try
            {
                //Compruebo que hay 2 personajes seleccionados, uno en cada lista/ que no hay mas de un slider activo
                var personajeIzquierdaValido = PersonajesIzquierda.Count(p => p.Puntuacion != 0);
                var personajeDerechaValido = PersonajesDerecha.Count(p => p.Puntuacion != 0);

                if (personajeIzquierdaValido == 1 && personajeDerechaValido == 1)
                {
                    var personajeIzquierda = PersonajesIzquierda.First(p => p.Puntuacion != 0);
                    var personajeDerecha = PersonajesDerecha.First(p => p.Puntuacion != 0);

                    //Compruiebo que no sean el mismo personaje
                    if (personajeIzquierda.Id == personajeDerecha.Id)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No puedes seleccionar el mismo personaje en ambas listas.", "OK");
                        return;  
                    }

                    var combateExistente = new clsCombate(
                        DateTime.UtcNow.Date ,
                        personajeIzquierda.Id,
                        personajeDerecha.Id,
                        personajeIzquierda.Puntuacion,
                        personajeDerecha.Puntuacion
                    );

                    if (clsDalBDD.insertarCombate(combateExistente))
                    {
                        await App.Current.MainPage.DisplayAlert("Éxito", "Se ha puntuado correctamente.", "OK");
                    }
                    else
                    {
                        if (clsDalBDD.updateCombate(combateExistente))
                        {
                            await App.Current.MainPage.DisplayAlert("Éxito", "Se han sumado las puntuaciones correctamente.", "OK");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error al sumar las puntuaciones al combate existente.", "OK");
                        }
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Introduzca un combate valido, asegurese de que no hay mas de un slider activo en la misma lista.", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Ha ocurrido un error: {ex.Message}", "OK");
            }
        }



        /// <summary>
        /// Cargo los listados de forma asincrona, aun asi a veces falla la carga y se queda pillado
        /// </summary>
        /// <returns></returns>
        public async Task cargarListado()
        {
            var personajesDesdeDB = clsDalBDD.ObtenerPersonajes();

            PersonajesIzquierda = new ObservableCollection<clsPersonajeConPuntuacion>(
                personajesDesdeDB.Select(p => new clsPersonajeConPuntuacion
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Foto = p.Foto,
                    Puntuacion = 0 
                }).ToList()
            );

            PersonajesDerecha = new ObservableCollection<clsPersonajeConPuntuacion>(
                personajesDesdeDB.Select(p => new clsPersonajeConPuntuacion
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Foto = p.Foto,
                    Puntuacion = 0 
                }).ToList()
            );
        }

        public clsViewModelPuntuarCombate()
        {
            _ = cargarListado(); 
            puntuacionCommand = new DelegateCommand(PuntuacionCommandExecuted, PuntuacionCommandCanExecute); 
        }

        private bool PuntuacionCommandCanExecute()
        {

            //Hacer comprobacion aqui quizas?
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
