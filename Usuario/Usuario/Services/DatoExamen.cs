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
    public class DatoExamen
    {
        public DatoExamen() { }

        #region Cargar Especialidades
        public void CargarEspecialidades(ComboBox cmbEspecialidad)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT EspecialidadID, NombreEspecialidad FROM Especialidad";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se llenó correctamente el DataTable
                    if (dt.Rows.Count > 0)
                    {
                        cmbEspecialidad.ItemsSource = dt.DefaultView;
                        cmbEspecialidad.DisplayMemberPath = "NombreEspecialidad"; // El campo que quieres mostrar
                        cmbEspecialidad.SelectedValuePath = "EspecialidadID"; // El valor que quieres usar como clave
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron especialidades.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar especialidades: " + ex.Message);
                }
            }
        }
        #endregion

        #region Cargar Tipo Examen
        public void CargarTipoExamen(ComboBox cmbTipoExamen)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TipoExamenID, TipoExamen FROM TipoExamenes";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se llenó correctamente el DataTable
                    if (dt.Rows.Count > 0)
                    {
                        cmbTipoExamen.ItemsSource = dt.DefaultView;
                        cmbTipoExamen.DisplayMemberPath = "TipoExamen"; // El campo que quieres mostrar
                        cmbTipoExamen.SelectedValuePath = "TipoExamenID"; // El valor que quieres usar como clave
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron tipos de examenes.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar tipos de examenes: " + ex.Message);
                }
            }
        }
        #endregion

        //VENTANA AGENDAR EXAMEN CON CONSULTA
        #region METODO PARA INSERTAR EXAMEN AGENDADO
        // Método para insertar médico
        public static int AltaExamen(ExamenModel examen)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SPINSERTAREXAMENAGENDADO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteID", examen.PacienteID);
                        cmd.Parameters.AddWithValue("@MedicoID", examen.MedicoID);
                        cmd.Parameters.AddWithValue("@ConsultaID", examen.ConsultaID);
                        cmd.Parameters.AddWithValue("@EspecialidadID", examen.EspecialidadID);
                        cmd.Parameters.AddWithValue("@TipoExamenID", examen.TipoExamenID);
                        cmd.Parameters.AddWithValue("@FechaExamen", examen.FechaExamen);

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

        #region METODO PARA CARGAR EL DATAGRID DE CONSULTAS EN EXAMEN
        // Método para cargar el DataGrid
        public static List<ExamenModel> MuestraConsultasExamen()
        {
            List<ExamenModel> lstExamenes = new List<ExamenModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARCONSULTASEXAMEN";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ExamenModel examen = new ExamenModel
                                {
                                    ConsultaID = Convert.ToInt32(dr["ConsultaID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    NombrePaciente = dr["NombrePaciente"].ToString(),
                                    ApellidoPaciente = dr["ApellidoPaciente"].ToString(),
                                    MedicoID = Convert.ToInt32(dr["MedicoID"]),
                                    NombreMedico = dr["NombreMedico"].ToString(),
                                    ApellidoMedico = dr["ApellidoMedico"].ToString()

                                };

                                // Agregar a la lista
                                lstExamenes.Add(examen);
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
            return lstExamenes;
        }
        #endregion

        #region METODO BUSQUEDA CONSULTAS EN EXAMEN
        public static List<ExamenModel> BuscarConsultasExamen(int? id = null)
        {
            List<ExamenModel> lstConsulta = new List<ExamenModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARCONSULTASEXAMEN";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@ConsultaID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ExamenModel consulta = new ExamenModel
                                {
                                    ConsultaID = Convert.ToInt32(dr["ConsultaID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    NombrePaciente = dr["NombrePaciente"].ToString(),
                                    ApellidoPaciente = dr["ApellidoPaciente"].ToString(),
                                    MedicoID = Convert.ToInt32(dr["MedicoID"]),
                                    NombreMedico = dr["NombreMedico"].ToString(),
                                    ApellidoMedico = dr["ApellidoMedico"].ToString()
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

        //VENTANA AGENDAR EXAMENES SIN CONSULTA
        #region METODO PARA CARGAR EL DATAGRID
        // Método para cargar el DataGrid
        public static List<ExamenModel> MostrarPacientes()
        {
            List<ExamenModel> lstPacientes = new List<ExamenModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARPACIENTESAEXAMEN";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ExamenModel examen = new ExamenModel
                                {
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    NombrePaciente = dr["NombrePaciente"].ToString(),
                                    ApellidoPaciente = dr["ApellidoPaciente"].ToString()

                                };

                                // Agregar a la lista
                                lstPacientes.Add(examen);
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
            return lstPacientes;
        }
        #endregion

        #region METODO PARA INSERTAR
        // Método para insertar médico
        public static int InsertaExamen(ExamenModel examen)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SPINSERTAREXAMEN", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteID", examen.PacienteID);
                        cmd.Parameters.AddWithValue("@EspecialidadID", examen.EspecialidadID);
                        cmd.Parameters.AddWithValue("@TipoExamenID", examen.TipoExamenID);
                        cmd.Parameters.AddWithValue("@FechaExamen", examen.FechaExamen);

                        res = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al insertar registro"+ex, "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return res;
        }
        // Fin del método insertar
        #endregion

        #region METODO BUSQUEDA PACIENTES
        public static List<ExamenModel> BuscarPacientes(int? id = null)
        {
            List<ExamenModel> lstPaciente = new List<ExamenModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARPACIENTESEXAMEN";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@PacienteID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ExamenModel paciente = new ExamenModel
                                {
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    NombrePaciente = dr["NombrePaciente"].ToString(),  // Asegúrate que coincida con el alias en el SP
                                    ApellidoPaciente = dr["ApellidoPaciente"].ToString()
                                };

                                // Agregar a la lista
                                lstPaciente.Add(paciente);
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
            return lstPaciente;
        }
        #endregion

        //VENTANA REGISTRAR EXAMENES

        #region METODO PARA CARGAR EL DATAGRID
        // Método para cargar el DataGrid
        // Método para cargar el DataGrid
        public static List<ExamenModel> MuestraExamenes()
        {
            List<ExamenModel> lstExamenes = new List<ExamenModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRAREXAMENESAGENDADOS";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ExamenModel examen = new ExamenModel
                                {
                                    ExamenId = Convert.ToInt32(dr["ExamenID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    MedicoID = dr["MedicoID"] != DBNull.Value ? Convert.ToInt32(dr["MedicoID"]) : (int?)null,
                                    TipoExamen = dr["TipoExamenNombre"].ToString(),
                                    Especialidad = dr["EspecialidadNombre"].ToString(),
                                    FechaExamen = Convert.ToDateTime(dr["FechaExamen"])
                                };

                                // Agregar a la lista
                                lstExamenes.Add(examen);
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
            return lstExamenes;
        }

        #endregion

        #region METODO PARA REGISTRAR RESULTADOS DE EXAMEN
        // Método para insertar médico
        public static int RegistrarExamen(ExamenModel examen, int examenId)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SPREGISTRARRESULTADOSEXAMEN", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ExamenID", examenId);
                        cmd.Parameters.AddWithValue("@Resultado", examen.Resultado);

                        // Especificar el tipo de dato DateTime explícitamente
                        cmd.Parameters.Add("@FechaResultado", SqlDbType.DateTime).Value = examen.FechaResultado;

                        res = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al insertar registro: " + ex.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return res;
        }

        // Fin del método insertar
        #endregion

        #region METODO BUSQUEDA EXAMEN AGENDADO
        public static List<ExamenModel> BuscarExamenAgendado(int? id = null)
        {
            List<ExamenModel> lstExamen = new List<ExamenModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCAREXAMENAGENDADO";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@ExamenID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ExamenModel examen = new ExamenModel
                                {
                                    ExamenId = Convert.ToInt32(dr["ExamenID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    MedicoID = dr["MedicoID"] != DBNull.Value ? Convert.ToInt32(dr["MedicoID"]) : (int?)null,
                                    TipoExamen = dr["TipoExamenNombre"].ToString(),
                                    Especialidad = dr["EspecialidadNombre"].ToString(),
                                    FechaExamen = Convert.ToDateTime(dr["FechaExamen"])
                                };

                                // Agregar a la lista
                                lstExamen.Add(examen);
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
            return lstExamen;
        }
        #endregion

        //VENTANA PARA MOSTRAR EXAMENES REGISTRADOS
        #region METODO PARA CARGAR EL DATAGRID
        // Método para cargar el DataGrid
        public static List<ExamenModel> MuestraExamenesRegistrados()
        {
            List<ExamenModel> lstExamenes = new List<ExamenModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRAREXAMENESREGISTRADOS";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ExamenModel examen = new ExamenModel
                                {
                                    ExamenId = Convert.ToInt32(dr["ExamenID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    MedicoID = dr["MedicoID"] != DBNull.Value ? Convert.ToInt32(dr["MedicoID"]) : (int?)null,
                                    TipoExamen = dr["TipoExamenNombre"].ToString(),
                                    Especialidad = dr["EspecialidadNombre"].ToString(),
                                    FechaExamen = Convert.ToDateTime(dr["FechaExamen"]),
                                    FechaResultado = Convert.ToDateTime(dr["FechaResultado"]),

                                    Resultado = dr["Resultado"].ToString(),
                                    EstadoExamenNombre = dr["EstadoExamenNombre"].ToString()

                                };

                                // Agregar a la lista
                                lstExamenes.Add(examen);
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
            return lstExamenes;
        }
        #endregion

        #region METODO PARA ELIMINAR
        //Metodo para eliminar medico
        public static int EliminarExamen(ExamenModel examen)
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
                        comand.CommandText = "SPELIMINAREXAMEN";
                        comand.Parameters.AddWithValue("@ExamenID", examen.ExamenId);

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

        #region METODO PARA CAMBIAR ESTADO
        //Metodo para eliminar medico
        public static int CambiarEstado(ExamenModel examen)
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
                        comand.CommandText = "SPCAMBIARESTADOEXAMEN";
                        comand.Parameters.AddWithValue("@ExamenID", examen.ExamenId);

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

        #region METODO BUSQUEDA EXAMEN REGSITRADO
        public static List<ExamenModel> BuscarExamenRegistrado(int? id = null)
        {
            List<ExamenModel> lstExamen = new List<ExamenModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCAREXAMENREGISTRADO";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@ExamenID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                ExamenModel examen = new ExamenModel
                                {
                                    ExamenId = Convert.ToInt32(dr["ExamenID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    MedicoID = dr["MedicoID"] != DBNull.Value ? Convert.ToInt32(dr["MedicoID"]) : (int?)null,
                                    TipoExamen = dr["TipoExamenNombre"].ToString(),
                                    Especialidad = dr["EspecialidadNombre"].ToString(),
                                    FechaExamen = Convert.ToDateTime(dr["FechaExamen"]),
                                    FechaResultado = Convert.ToDateTime(dr["FechaResultado"]),
                                    Resultado = dr["Resultado"].ToString(),
                                    EstadoExamenNombre = dr["EstadoExamenNombre"].ToString()
                                };

                                // Agregar a la lista
                                lstExamen.Add(examen);
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
            return lstExamen;
        }
        #endregion
    }
}
