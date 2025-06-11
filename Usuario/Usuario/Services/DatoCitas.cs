using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Usuario.Models;

namespace Usuario.Services
{
    public class DatoCitas
    {
        public DatoCitas() { }

        #region Cargar Citas al data gid 
        public static List<CitasModel> CargarCitas(int? medicoID = null, DateTime? fechaCita = null, int? pacienteID = null)
        {
            List<CitasModel> citasList = new List<CitasModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    using (SqlCommand command = new SqlCommand("SPMOSTRARCITAS", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros si son proporcionados
                        command.Parameters.AddWithValue("@MedicoID", (object)medicoID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@FechaCita", (object)fechaCita ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PacienteID", (object)pacienteID ?? DBNull.Value);

                        conn.Open();

                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                CitasModel cita = new CitasModel
                                {
                                    CitaID = dr.GetInt32(0),

                                    EspecialidadID = dr.GetInt32(1),
                                    NombreEspecialidad = dr.GetString(2),

                                    MedicoID = dr.GetInt32(3),
                                    Medico = dr.GetString(4),

                                    PacienteID = dr.GetInt32(5),
                                    Paciente = dr.GetString(6),

                                    FechaCita = dr.GetDateTime(7),
                                    Hora = dr.GetString(8),
                                    Duracion = dr.GetInt32(9),

                                    ConsultorioID = dr.GetInt32(10),
                                    NombreConsultorio = dr.GetString(11),

                                    MotivoCita = dr.GetString(12),

                                    EstadoCitaID = dr.GetInt32(13),
                                    EstadoCita = dr.GetString(14),

                                    Comentarios = dr.IsDBNull(15) ? null : dr.GetString(15)
                                };

                                citasList.Add(cita);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar citas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return citasList;
        }

        #endregion

        #region Metodos de Cita 
        public static void GuardarCita(CitasModel cita)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("SPINSERTARCITAS", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FechaCita", cita.FechaCita);
                        command.Parameters.AddWithValue("@Hora", cita.Hora);
                        command.Parameters.AddWithValue("@Duracion", cita.Duracion);
                        command.Parameters.AddWithValue("@MotivoCita", cita.MotivoCita);
                        command.Parameters.AddWithValue("@PacienteID", cita.PacienteID);
                        command.Parameters.AddWithValue("@MedicoID", cita.MedicoID);
                        command.Parameters.AddWithValue("@EspecialidadID", cita.EspecialidadID);
                        command.Parameters.AddWithValue("@ConsultorioID", cita.ConsultorioID);
                        command.Parameters.AddWithValue("@Comentarios", cita.Comentarios);


                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cita guardada correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error al guardar la cita: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static int ActualizarCita(CitasModel cita)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SPACTUALIZARCITA";

                        // Parámetros para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@CitaID", cita.CitaID);
                        cmd.Parameters.AddWithValue("@FechaCita", cita.FechaCita);
                        cmd.Parameters.AddWithValue("@Hora", cita.Hora);
                        cmd.Parameters.AddWithValue("@Duracion", cita.Duracion);
                        cmd.Parameters.AddWithValue("@MotivoCita", cita.MotivoCita);
                        cmd.Parameters.AddWithValue("@PacienteID", cita.PacienteID);
                        cmd.Parameters.AddWithValue("@MedicoID", cita.MedicoID);
                        cmd.Parameters.AddWithValue("@EspecialidadID", cita.EspecialidadID);
                        cmd.Parameters.AddWithValue("@ConsultorioID", cita.ConsultorioID);
                        cmd.Parameters.AddWithValue("@Comentarios", cita.Comentarios);

                        // Ejecutar el comando
                        res = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar actualizar la cita: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }

        public static void EliminarCita(int citaID)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    using (var command = new SqlCommand("SPELIMINARCITA", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CitaID", citaID);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Cita eliminada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                        //recargar el DataGrid
                        CargarCitas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar la cita: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion



    }
}
