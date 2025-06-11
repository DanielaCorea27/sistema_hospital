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
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Usuario.Services
{
    public class DatoConsulta
    {
        public DatoConsulta() { }

        #region Cargar Consultorios
        public void CargarConsultorios(ComboBox cmbConsultorio)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ConsultorioID, NombreConsultorio FROM Consultorio";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se llenó correctamente el DataTable
                    if (dt.Rows.Count > 0)
                    {
                        cmbConsultorio.ItemsSource = dt.DefaultView;
                        cmbConsultorio.DisplayMemberPath = "NombreConsultorio"; // El campo que quieres mostrar
                        cmbConsultorio.SelectedValuePath = "ConsultorioID"; // El valor que quieres usar como clave
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron consultorios.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar consultorios: " + ex.Message);
                }
            }
        }
        #endregion

        //REGISTRAR CONSULTAS
        #region METODO PARA CARGAR EL DATAGRID DE CITAS EN CONSULTA
        // Método para cargar el DataGrid
        public static List<ConsultaModel> MuestraCitasConsulta()
        {
            List<ConsultaModel> lstCitas = new List<ConsultaModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARCITASCONSULTA";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ConsultaModel cita = new ConsultaModel
                                {
                                    CitaID = Convert.ToInt32(dr["CitaID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    NombrePaciente = dr["NombrePaciente"].ToString(),
                                    ApellidoPaciente = dr["ApellidoPaciente"].ToString(),
                                    MedicoID = Convert.ToInt32(dr["MedicoID"]),
                                    NombreMedico = dr["NombreMedico"].ToString(),
                                    ApellidoMedico = dr["ApellidoMedico"].ToString(),
                                    NombreConsultorio = dr["NombreConsultorio"].ToString()

                                };

                                // Agregar a la lista
                                lstCitas.Add(cita);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Error relacionado con SQL
                MessageBox.Show("Error de base de datos: " + sqlEx.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Otros tipos de errores
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstCitas;
        }
        #endregion

        #region METODO BUSQUEDA CITAS EN CONSULTAS
        public static List<ConsultaModel> BuscarCitasConsulta(int? id = null)
        {
            List<ConsultaModel> lstCita = new List<ConsultaModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARCITASCONSULTAS";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@CitaID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ConsultaModel cita = new ConsultaModel
                                {
                                    CitaID = Convert.ToInt32(dr["CitaID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    NombrePaciente = dr["NombrePaciente"].ToString(),
                                    ApellidoPaciente = dr["ApellidoPaciente"].ToString(),
                                    MedicoID = Convert.ToInt32(dr["MedicoID"]),
                                    NombreMedico = dr["NombreMedico"].ToString(),
                                    ApellidoMedico = dr["ApellidoMedico"].ToString(),
                                    NombreConsultorio = dr["NombreConsultorio"].ToString()
                                };

                                // Agregar a la lista
                                lstCita.Add(cita);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Error relacionado con SQL
                MessageBox.Show("Error de base de datos: " + sqlEx.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Otros tipos de errores
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstCita;
        }
        #endregion

        #region METODO PARA INSERTAR CONSULTA
        // Método para insertar médico
        public static int AltaConsulta(ConsultaModel consulta)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SPINSERTARCONSULTA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteID", consulta.PacienteID);
                        cmd.Parameters.AddWithValue("@MedicoID", consulta.MedicoID);
                        cmd.Parameters.AddWithValue("@FechaConsulta", consulta.FechaConsulta);
                        cmd.Parameters.AddWithValue("@MotivoConsulta", consulta.MotivoConsulta);
                        cmd.Parameters.AddWithValue("@Examen", consulta.Examen);
                        cmd.Parameters.AddWithValue("@ConsultorioID", consulta.ConsultorioID);


                        res = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al insertar registro", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return res;
        }
        // Fin del método insertar
        #endregion

        #region METODO PARA CAMBIAR CONSULTA
        //Metodo para eliminar medico
        public static int CambiarConsulta(ConsultaModel consulta)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPCAMBIARESTADOCITA";
                        comand.Parameters.AddWithValue("@CitaID", consulta.CitaID);

                        res = comand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alerta: " + ex.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }
        #endregion

        //CONSULTAS REGISTRADAS

        #region METODO PARA CARGAR EL DATAGRID DE CONSULTAS
        // Método para cargar el DataGrid
        public static List<ConsultaModel> MuestraConsultas()
        {
            List<ConsultaModel> lstConsultas = new List<ConsultaModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARCONSULTAS";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ConsultaModel consulta = new ConsultaModel
                                {
                                    ConsultaID = Convert.ToInt32(dr["ConsultaID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    NombrePaciente = dr["NombrePaciente"].ToString(),
                                    ApellidoPaciente = dr["ApellidoPaciente"].ToString(),
                                    MedicoID = Convert.ToInt32(dr["MedicoID"]),
                                    NombreMedico = dr["NombreMedico"].ToString(),
                                    ApellidoMedico = dr["ApellidoMedico"].ToString(),
                                    NombreConsultorio = dr["NombreConsultorio"].ToString(),
                                    FechaConsulta = Convert.ToDateTime(dr["FechaConsulta"]),
                                    Examen = Convert.ToBoolean(dr["Examen"]),
                                    MotivoConsulta = dr["MotivoConsulta"].ToString()

                                };

                                // Agregar a la lista
                                lstConsultas.Add(consulta);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Error relacionado con SQL
                MessageBox.Show("Error de base de datos: " + sqlEx.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Otros tipos de errores
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstConsultas;
        }
        #endregion

        #region METODO BUSQUEDA CONSULTAS
        public static List<ConsultaModel> BuscarConsulta(int? id = null)
        {
            List<ConsultaModel> lstConsulta = new List<ConsultaModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARCONSULTAS";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@ConsultaID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ConsultaModel consulta = new ConsultaModel
                                {
                                    ConsultaID = Convert.ToInt32(dr["ConsultaID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    NombrePaciente = dr["NombrePaciente"].ToString(),
                                    ApellidoPaciente = dr["ApellidoPaciente"].ToString(),
                                    MedicoID = Convert.ToInt32(dr["MedicoID"]),
                                    NombreMedico = dr["NombreMedico"].ToString(),
                                    ApellidoMedico = dr["ApellidoMedico"].ToString(),
                                    NombreConsultorio = dr["NombreConsultorio"].ToString(),
                                    FechaConsulta = Convert.ToDateTime(dr["FechaConsulta"]),
                                    Examen = Convert.ToBoolean(dr["Examen"]),
                                    MotivoConsulta = dr["MotivoConsulta"].ToString()
                                };

                                // Agregar a la lista
                                lstConsulta.Add(consulta);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Error relacionado con SQL
                MessageBox.Show("Error de base de datos: " + sqlEx.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Otros tipos de errores
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstConsulta;
        }
        #endregion

        #region METODO PARA ELIMINAR CONSULTA
        //Metodo para eliminar medico
        public static int EliminarConsulta(ConsultaModel consulta)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPELIMINARCONSULTA";
                        comand.Parameters.AddWithValue("@ConsultaID", consulta.ConsultaID);

                        res = comand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alerta: " + ex.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }
        #endregion
    }
}
