using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class clsDalBDD
    {
        public static List<clsPersonaje> ObtenerPersonajes()
        {
            SqlConnection miConexion;
            SqlDataReader miLector;
            SqlCommand miComando = new SqlCommand();
            clsPersonaje oPersonaje;
            List<clsPersonaje> listadoPersonajes = new List<clsPersonaje>();

            try
            {
                miConexion = clsConexion.getConexion(); 
                miComando.CommandText = "SELECT * FROM Personaje";
                miComando.Connection = miConexion;
                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        int id = (int)miLector["Id"];
                        string nombre = miLector["Nombre"] != DBNull.Value ? (string)miLector["Nombre"] : "";

                        oPersonaje = new clsPersonaje(id, nombre);
                        listadoPersonajes.Add(oPersonaje);
                    }
                }

                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return listadoPersonajes;
        }
        public static bool insertarCombate(clsCombate combateNuevo)
        {
            SqlConnection miConexion;
            SqlCommand miComando = new SqlCommand();
            bool resultado = false;
            try
            {
                miConexion = clsConexion.getConexion(); 
                miComando.CommandText = "INSERT INTO Combate (IdPersonaje1, IdPersonaje2, Puntuacion, FechaCombate) " +
                                        "VALUES (@idPersonaje1, @idPersonaje2, @puntuacion, @fechaCombate)";
                miComando.Connection = miConexion;
                miComando.Parameters.AddWithValue("@idPersonaje1", combateNuevo.IdPersonaje1);
                miComando.Parameters.AddWithValue("@idPersonaje2", combateNuevo.IdPersonaje2);
                miComando.Parameters.AddWithValue("@puntacion1", combateNuevo.Puntuacion1);
                miComando.Parameters.AddWithValue("@puntacion2", combateNuevo.Puntuacion2);
                miComando.Parameters.AddWithValue("@fechaCombate", combateNuevo.FechaCombate);



                int filasAfectadas = miComando.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    resultado = true;
                }

                miConexion.Close(); 
            }
            catch (SqlException ex)
            {
 
                throw ex;
            }
            return resultado; 
        }


    }
}
