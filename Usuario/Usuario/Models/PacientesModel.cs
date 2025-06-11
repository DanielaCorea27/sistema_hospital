using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Usuario.Models
{
    internal class PacientesModel
    {
        // ATRIBUTOS DE LA CLASE
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public string Genero { get; set; }
        public int GeneroID { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Recidencia { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public int MunicipioID { get; set; }
        public int EstadoID { get; set; }


        // CONSTRUCTOR VACIO
        public PacientesModel() { }


        // METODOS DE LA CLASE

        #region METODO PARA CARGAR EL DATAGRID
        // METODO PARA CARGAR EL DATAGRID
        public List<PacientesModel> MostrarPacientes()
        {
            List<PacientesModel> lstPacientes = new List<PacientesModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARPACIENTES";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                PacientesModel paciente = new PacientesModel
                                {
                                    Id = Convert.ToInt32(dr["ID"]),
                                    Nombre = dr["Nombre"].ToString(), // Cambiado a "NombreMedico"
                                    Apellido = dr["Apellido"].ToString(), // Cambiado a "ApellidoMedico"
                                    Identificacion = dr["Identificacion"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                                    Telefono = dr["Telefono"] != DBNull.Value ? dr["Telefono"].ToString() : string.Empty, // Manejo de null
                                    Email = dr["Email"].ToString(), // Cambiado a "EmailMedico"
                                    Recidencia = dr["Residencia"].ToString(),
                                    Departamento = dr["Departamento"].ToString(),
                                    Municipio = dr["Municipio"].ToString(),
                                    Genero = dr["Genero"].ToString()
                                };

                                // Agregar a la lista
                                lstPacientes.Add(paciente);
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
        // METODO PARA INSERTAR MEDICO
        public int InsertatPacientes(PacientesModel paciente, string email)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SPINSERTARPACIENTE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", this.Email);
                        cmd.Parameters.AddWithValue("@Identificacion", this.Identificacion);
                        cmd.Parameters.AddWithValue("@GeneroID", this.GeneroID);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", this.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@Residencia", this.Recidencia);
                        cmd.Parameters.AddWithValue("@Telefono", this.Telefono);
                        cmd.Parameters.AddWithValue("@MunicipioID", this.MunicipioID);

                        res = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: Ya existe un registro con  " + ex, "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return res;
        }
        // Fin del método insertar
        #endregion

        #region METODO PARA EDITAR
        // Método para editar médico
        public int ModificarPaciente(PacientesModel paciente, string emailActual, string telefonoActual)
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
                        cmd.CommandText = "SPACTUALIZARPACIENTE";

                        // Parámetros para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@Nombre", this.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", this.Apellido);
                        cmd.Parameters.AddWithValue("@EmailActual", emailActual); // Email viejo
                        cmd.Parameters.AddWithValue("@NuevoEmail", this.Email); // Nuevo email
                        cmd.Parameters.AddWithValue("@Identificacion", this.Identificacion);
                        cmd.Parameters.AddWithValue("@GeneroID", this.GeneroID);
                        cmd.Parameters.AddWithValue("@Residencia", this.Recidencia);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", this.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@TelefonoActual", telefonoActual); //Telefono Viejo
                        cmd.Parameters.AddWithValue("@NuevoTelefono", this.Telefono); // Nuevo Telefono
                        cmd.Parameters.AddWithValue("@MunicipioID", this.MunicipioID);

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
                    MessageBox.Show("Error: Ya existe un registro con " + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            return res;
        }
        // Fin del método editar
        #endregion

        #region METODO PARA ELIMINAR
        //Metodo para eliminar medico
        public int EliminarPaciente(PacientesModel paciente)
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
                        comand.CommandText = "SPELIMINARPACIENTE";
                        comand.Parameters.AddWithValue("@Email", paciente.Email);

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

        #region CARGAR COMBOBOX

        // Metodo para ComboBox Departamentos
        public void CargarDepartamento(ComboBox cmbDepa)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Departamentos";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se llenó correctamente el DataTable
                    if (dt.Rows.Count > 0)
                    {
                        cmbDepa.ItemsSource = dt.DefaultView;
                        cmbDepa.DisplayMemberPath = "Departamento"; // El campo que quieres mostrar
                        cmbDepa.SelectedValuePath = "DepartamentoID"; // El valor que quieres usar como clave
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron Departamentos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los departamentos: " + ex.Message);
                }
            }
        }


        // Metodo para ComboBox Municipios
        public void CargarMunicipios(ComboBox cmbMunici, int idDepartamento)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Municipios WHERE DepartamentoID = @DepartamentoID";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@DepartamentoID", idDepartamento);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se llenó correctamente el DataTable
                    if (dt.Rows.Count > 0)
                    {
                        cmbMunici.ItemsSource = dt.DefaultView;
                        cmbMunici.DisplayMemberPath = "Municipio"; // El campo que quieres mostrar
                        cmbMunici.SelectedValuePath = "MunicipioID"; // El valor que quieres usar como clave
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los municipios: " + ex.Message);
                }
            }
        }


        // Metodo para ComboBox Genero
        public void CargarGeneros(ComboBox cmbGenero)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Genero";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se llenó correctamente el DataTable
                    if (dt.Rows.Count > 0)
                    {
                        cmbGenero.ItemsSource = dt.DefaultView;
                        cmbGenero.DisplayMemberPath = "NombreGenero"; // El campo que quieres mostrar
                        cmbGenero.SelectedValuePath = "GeneroID"; // El valor que quieres usar como clave
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron Generos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los Generos: " + ex.Message);
                }
            }
        }
        #endregion

        #region METODO BUSQUEDA PACIENTES
        public List<PacientesModel> BuscarPaciente(int? id = null)
        {
            List<PacientesModel> lstPaciente = new List<PacientesModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARPACIENTES";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@PacienteID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                PacientesModel paciente = new PacientesModel
                                {
                                    Id = Convert.ToInt32(dr["PacienteID"]),
                                    Nombre = dr["Nombre"].ToString(),  // Asegúrate que coincida con el alias en el SP
                                    Apellido = dr["Apellido"].ToString(),  // Asegúrate que coincida con el alias en el SP
                                    Identificacion = dr["Identificacion"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]).Date,
                                    Telefono = dr["Telefono"] != DBNull.Value ? dr["Telefono"].ToString() : string.Empty, // Manejo de null
                                    Email = dr["Email"].ToString(),  // Asegúrate que coincida con el alias en el SP
                                    Recidencia = dr["Residencia"].ToString(),
                                    Municipio = dr["Municipio"].ToString(),
                                    Genero = dr["NombreGenero"].ToString()
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

        #region AgregarTelefono
        public int AgregarTelefono(int pacienteID, string telefono)
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
                        cmd.CommandText = "AgregarTelefonoPaciente"; // Nombre del procedimiento almacenado

                        // Parámetros para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@PacienteID", pacienteID);
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
                    MessageBox.Show("Error: El número de teléfono ya está asignado a otro paciente.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al intentar agregar el teléfono: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return res;
        }

        #endregion

        #region MOSTRAR TELEFONOS
        public List<PacientesModel> ObtenerTelefonosPorPaciente(int pacienteID)
        {
            List<PacientesModel> telefonos = new List<PacientesModel>();

            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT t.Telefono FROM Telefonos t " +
                                      "INNER JOIN PacienteTelefono pt ON t.TelefonoID = pt.TelefonoID " +
                                      "WHERE pt.PacienteID = @PacienteID";
                    cmd.Parameters.AddWithValue("@PacienteID", pacienteID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            telefonos.Add(new PacientesModel
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
