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
    public class DatoMedico
    {

        public DatoMedico() { }

        #region METODO PARA CARGAR EL DATAGRID
        // Método para cargar el DataGrid
        public static List<MedicosModel> MuestraMedicos()
        {
            List<MedicosModel> lstMedicos = new List<MedicosModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARMEDICO";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                MedicosModel medico = new MedicosModel
                                {
                                    MedicoId = Convert.ToInt32(dr["MedicoID"]),
                                    Nombre = dr["NombreMedico"].ToString(), // Cambiado a "NombreMedico"
                                    Apellido = dr["ApellidoMedico"].ToString(), // Cambiado a "ApellidoMedico"
                                    Identificacion = dr["Identificacion"].ToString(),
                                    Especialidad = dr["NombreEspecialidad"].ToString(),
                                    Telefono = dr["Telefono"] != DBNull.Value ? dr["Telefono"].ToString() : string.Empty, // Manejo de null
                                    Email = dr["EmailMedico"].ToString(), // Cambiado a "EmailMedico"
                                    HoraInicio = dr["NombreHoraInicio"].ToString(),
                                    HoraFin = dr["NombreHoraFin"].ToString(),
                                    Consultorio = dr["NombreConsultorio"].ToString()
                                };

                                // Agregar a la lista
                                lstMedicos.Add(medico);
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
            return lstMedicos;
        }
        #endregion

        #region METODO PARA INSERTAR
        // Método para insertar médico
        public static int AltaMedicos(MedicosModel medico, string email)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SPINSERTARMEDICO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Identificacion", medico.Identificacion);
                        cmd.Parameters.AddWithValue("@EspecialidadID", medico.EspecialidadID);
                        cmd.Parameters.AddWithValue("@HoraInicioID", medico.HoraInicioID);
                        cmd.Parameters.AddWithValue("@HoraFinID", medico.HoraFinID);
                        cmd.Parameters.AddWithValue("@ConsultorioID", medico.ConsultorioID);
                        cmd.Parameters.AddWithValue("@Telefono", medico.Telefono);

                        res = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: Ya existe un registro con el mismo Email, Teléfono o Identificación.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return res;
        }
        // Fin del método insertar
        #endregion

        #region METODO PARA EDITAR
        // Método para editar médico
        public static int ModificarMedico(MedicosModel medico, string emailActual, string telefonoActual)
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
                        cmd.CommandText = "SPACTUALIZARMEDICO";

                        // Parámetros para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@Nombre", medico.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", medico.Apellido);
                        cmd.Parameters.AddWithValue("@EmailActual", emailActual); // Email viejo
                        cmd.Parameters.AddWithValue("@NuevoEmail", medico.Email); // Nuevo email
                        cmd.Parameters.AddWithValue("@Identificacion", medico.Identificacion);
                        cmd.Parameters.AddWithValue("@EspecialidadID", medico.EspecialidadID);
                        cmd.Parameters.AddWithValue("@TelefonoActual", telefonoActual);
                        cmd.Parameters.AddWithValue("@NuevoTelefono", medico.Telefono);
                        cmd.Parameters.AddWithValue("@HoraInicioID", medico.HoraInicioID);
                        cmd.Parameters.AddWithValue("@HoraFinID", medico.HoraFinID);
                        cmd.Parameters.AddWithValue("@ConsultorioID", medico.ConsultorioID);

                        // Ejecutar el comando
                        res = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                if (ex.Message.Contains("Valor duplicado"))
                {
                    MessageBox.Show("Ocurrió un error al intentar actualizar los registros: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Error: Ya existe un registro con el mismo Email, Teléfono o Identificación.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            return res;
        }
        // Fin del método editar
        #endregion

        #region METODO PARA ELIMINAR
        //Metodo para eliminar medico
        public static int EliminarMedico(MedicosModel medico)
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
                        comand.CommandText = "SPELIMINAR"; 
                        comand.Parameters.AddWithValue("@Email", medico.Email); 

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

        #region Cargar HoraInicio
        public void CargarHoraInicio(ComboBox cbHoraInicio)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT HoraInicioID, NombreHoraInicio FROM HoraInicio";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se llenó correctamente el DataTable
                    if (dt.Rows.Count > 0)
                    {
                        cbHoraInicio.ItemsSource = dt.DefaultView;
                        cbHoraInicio.DisplayMemberPath = "NombreHoraInicio"; // El campo que quieres mostrar
                        cbHoraInicio.SelectedValuePath = "HoraInicioID"; // El valor que quieres usar como clave
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron horas iniciales.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar horas iniciales: " + ex.Message);
                }
            }
        }
        #endregion

        #region Cargar HoraFin
        public void CargarHoraFin(ComboBox cbHoraFin)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT HoraFinID, NombreHoraFin FROM HoraFin";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se llenó correctamente el DataTable
                    if (dt.Rows.Count > 0)
                    {
                        cbHoraFin.ItemsSource = dt.DefaultView;
                        cbHoraFin.DisplayMemberPath = "NombreHoraFin"; // El campo que quieres mostrar
                        cbHoraFin.SelectedValuePath = "HoraFinID"; // El valor que quieres usar como clave
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron horas finales.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar horas finales: " + ex.Message);
                }
            }
        }
        #endregion

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

        #region METODO BUSQUEDA MEDICOS
        public static List<MedicosModel> BuscarMedicos(int? id = null)
        {
            List<MedicosModel> lstMedicos = new List<MedicosModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARMEDICOS";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@MedicoID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                MedicosModel medico = new MedicosModel
                                {
                                    MedicoId = Convert.ToInt32(dr["MedicoID"]),
                                    Nombre = dr["NombreMedico"].ToString(),  // Asegúrate que coincida con el alias en el SP
                                    Apellido = dr["ApellidoMedico"].ToString(),  // Asegúrate que coincida con el alias en el SP
                                    Identificacion = dr["Identificacion"].ToString(),
                                    Especialidad = dr["NombreEspecialidad"].ToString(),
                                    Telefono = dr["Telefono"] != DBNull.Value ? dr["Telefono"].ToString() : string.Empty, // Manejo de null
                                    Email = dr["EmailMedico"].ToString(),  // Asegúrate que coincida con el alias en el SP
                                    HoraInicio = dr["NombreHoraInicio"].ToString(),
                                    HoraFin = dr["NombreHoraFin"].ToString(),
                                    Consultorio = dr["NombreConsultorio"].ToString()
                                };

                                // Agregar a la lista
                                lstMedicos.Add(medico);
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
            return lstMedicos;
        }
        #endregion

        #region AgregarTelefono
        public static int AgregarTelefono(int medicoID, string telefono)
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
                        cmd.CommandText = "AgregarTelefono"; // Nombre del procedimiento almacenado

                        // Parámetros para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@MedicoID", medicoID);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);

                        // Ejecutar el comando
                        res = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores
                if (ex.Message.Contains("El número de teléfono ya está asignado"))
                {
                    MessageBox.Show("Error: El número de teléfono ya está asignado a otro médico.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al intentar agregar el teléfono: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return res;
        }

        #endregion

        #region MostrarTelefonos
        public static List<MedicosModel> ObtenerTelefonosPorMedico(int medicoID)
        {
            List<MedicosModel> telefonos = new List<MedicosModel>();

            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT t.Telefono FROM Telefonos t " +
                                      "INNER JOIN MedicoTelefono mt ON t.TelefonoID = mt.TelefonoID " +
                                      "WHERE mt.MedicoID = @MedicoID";
                    cmd.Parameters.AddWithValue("@MedicoID", medicoID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            telefonos.Add(new MedicosModel
                            {
                                Telefono = reader["Telefono"].ToString()
                            });
                        }
                    }
                }
            }

            return telefonos;
        }

        #endregion
    }
}
