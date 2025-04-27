using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class clsDalBDD
    {
        /// <summary>
        /// Obtiene una lista de personajes desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos clsPersonaje.</returns>
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
                miComando.CommandText = "SELECT Id, Nombre, Foto FROM Personaje";  
                miComando.Connection = miConexion;
                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        int id = (int)miLector["Id"];
                        string nombre = miLector["Nombre"] != DBNull.Value ? (string)miLector["Nombre"] : "";
                        string foto = miLector["Foto"] != DBNull.Value ? (string)miLector["Foto"] : "";  

                        oPersonaje = new clsPersonaje(id, nombre, foto);  
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




        /// <summary>
        /// Obtiene una lista de combates desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos clsCombate.</returns>
        public static List<clsCombate> ObtenerCombates()
        {
            SqlConnection miConexion;
            SqlDataReader miLector;
            SqlCommand miComando = new SqlCommand();
            clsCombate oCombate;
            List<clsCombate> listadoCombates = new List<clsCombate>();

            try
            {
                miConexion = clsConexion.getConexion();
                miComando.CommandText = "SELECT * FROM Combate";
                miComando.Connection = miConexion;
                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        int idCombate = (int)miLector["IdCombate"];
                        DateTime fechaCombate = (DateTime)miLector["FechaCombate"];
                        int idPersonaje1 = (int)miLector["IdPersonaje1"];
                        int idPersonaje2 = (int)miLector["IdPersonaje2"];
                        int puntuacion1 = (int)miLector["Puntuacion1"];
                        int puntuacion2 = (int)miLector["Puntuacion2"];

                        oCombate = new clsCombate(idCombate, fechaCombate, idPersonaje1, idPersonaje2, puntuacion1, puntuacion2);
                        listadoCombates.Add(oCombate);
                    }
                }

                miLector.Close();
                miConexion.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return listadoCombates;
        }

        /// <summary>
        /// Inserta un nuevo combate en la base de datos si no existe un combate similar en el mismo día.
        /// </summary>
        /// <param name="combateNuevo">Objeto clsCombate con los detalles del combate a insertar.</param>
        /// <returns>Valor booleano indicando si la inserción fue exitosa.</returns>
        public static bool insertarCombate(clsCombate combateNuevo)
        {
            SqlConnection miConexion;
            SqlCommand miComando = new SqlCommand();
            bool resultado = false;

            miComando.Parameters.AddWithValue("@idPersonaje1", combateNuevo.IdPersonaje1);
            miComando.Parameters.AddWithValue("@idPersonaje2", combateNuevo.IdPersonaje2);
            miComando.Parameters.AddWithValue("@puntuacion1", combateNuevo.Puntuacion1);
            miComando.Parameters.AddWithValue("@puntuacion2", combateNuevo.Puntuacion2);
            miComando.Parameters.AddWithValue("@fechaCombate", combateNuevo.FechaCombate.Date);


            try
            {
                miConexion = clsConexion.getConexion();
                miComando.CommandText = @"
IF NOT EXISTS (
    SELECT 1 FROM Combate 
    WHERE 
        ((IdPersonaje1 = @idPersonaje1 AND IdPersonaje2 = @idPersonaje2) OR 
         (IdPersonaje1 = @idPersonaje2 AND IdPersonaje2 = @idPersonaje1)) 
       AND FechaCombate = @fechaCombate
)
BEGIN
    INSERT INTO Combate (IdPersonaje1, IdPersonaje2, Puntuacion1, Puntuacion2, FechaCombate)
    VALUES (@idPersonaje1, @idPersonaje2, @puntuacion1, @puntuacion2, @fechaCombate)
END";

                miComando.Connection = miConexion;

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

        /// <summary>
        /// Actualiza los puntajes de un combate existente en la base de datos, si se encuentra un combate para los personajes en el mismo día.
        /// </summary>
        /// <param name="combateAActualizar">Objeto clsCombate con los detalles del combate a actualizar.</param>
        /// <returns>Valor booleano indicando si la actualización fue exitosa.</returns>
        public static bool updateCombate(clsCombate combateAActualizar)
        {
            bool resultado = false;
            SqlConnection miConexion;
            SqlCommand comprobarComando = new SqlCommand();
            SqlCommand miComando = new SqlCommand();

            try
            {
                miConexion = clsConexion.getConexion();

                comprobarComando.CommandText = @"
        SELECT IdPersonaje1, IdPersonaje2 
        FROM Combate 
        WHERE 
            ((IdPersonaje1 = @id1 AND IdPersonaje2 = @id2) OR 
             (IdPersonaje1 = @id2 AND IdPersonaje2 = @id1))
            AND DAY(FechaCombate) = @fecha";

                comprobarComando.Parameters.AddWithValue("@id1", combateAActualizar.IdPersonaje1);
                comprobarComando.Parameters.AddWithValue("@id2", combateAActualizar.IdPersonaje2);
                comprobarComando.Parameters.AddWithValue("@fecha", combateAActualizar.FechaCombate.Day);

                comprobarComando.Connection = miConexion;

                int personaje1BD = 0, personaje2BD = 0;

                using (SqlDataReader reader = comprobarComando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        personaje1BD = (int)reader["IdPersonaje1"];
                        personaje2BD = (int)reader["IdPersonaje2"];
                    }
                    else
                    {
                        reader.Close();
                        miConexion.Close();
                        return false;
                    }
                }

                int puntuacion1, puntuacion2;
                if (combateAActualizar.IdPersonaje1 == personaje1BD)
                {
                    puntuacion1 = combateAActualizar.Puntuacion1;
                    puntuacion2 = combateAActualizar.Puntuacion2;
                }
                else
                {
                    puntuacion1 = combateAActualizar.Puntuacion2;
                    puntuacion2 = combateAActualizar.Puntuacion1;
                }

                miComando.CommandText = @"
        UPDATE Combate 
        SET Puntuacion1 = Puntuacion1 + @p1, 
            Puntuacion2 = Puntuacion2 + @p2 
        WHERE IdPersonaje1 = @idP1 AND IdPersonaje2 = @idP2 AND DAY(FechaCombate) = @fecha";
                miComando.Connection = miConexion;

                miComando.Parameters.AddWithValue("@p1", puntuacion1);
                miComando.Parameters.AddWithValue("@p2", puntuacion2);
                miComando.Parameters.AddWithValue("@idP1", personaje1BD);
                miComando.Parameters.AddWithValue("@idP2", personaje2BD);
                miComando.Parameters.AddWithValue("@fecha", combateAActualizar.FechaCombate.Day);

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
