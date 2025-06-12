using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Usuario.Models
{
    internal class HistorialPacientesModel
    {
        // ATRIBUTOS DE LA CLASE
        public int ID { get; set; }
        public int PacienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string MotivoConsulta { get; set; }
        public string Padecimientos { get; set; }
        public string Traumatismos { get; set; }
        public string CirugiasPrevias { get; set; }
        public string MedicacionActual { get; set; }
        public string AntecedentesFamiliares { get; set; }
        public string Discapacidad { get; set; }
        public string Alergia { get; set; }
        public string EnfermedadCronica { get; set; }
        public string Observaciones { get; set; }


        // CONSTRUCTOR VACIO
        public HistorialPacientesModel() { }


        // METODOS DE LA CLASE

        #region METODO PARA CARGAR EL DATAGRID
        // METODO PARA CARGAR EL DATAGRID
        public List<HistorialPacientesModel> MostrarHistorial()
        {
            List<HistorialPacientesModel> lstHistorialPacientes = new List<HistorialPacientesModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARHISTORIALPACIENTES";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                HistorialPacientesModel historial = new HistorialPacientesModel
                                {
                                    ID = Convert.ToInt32(dr["HistorialPacienteID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    MotivoConsulta = dr["MotivoConsulta"].ToString(),
                                    Nombre = dr["NombrePaciente"].ToString(),
                                    Apellido = dr["ApellidoPaciente"].ToString(),
                                    Padecimientos = dr["Padecimientos"].ToString(),
                                    Traumatismos = dr["Traumatismos"].ToString(),
                                    CirugiasPrevias = dr["CirugiasPrevias"].ToString(),
                                    MedicacionActual = dr["MedicacionActual"].ToString(),
                                    AntecedentesFamiliares = dr["AntecedentesFamiliares"].ToString(),
                                    Discapacidad = dr["Discapacidad"].ToString(),
                                    Alergia = dr["Alergia"].ToString(),
                                    EnfermedadCronica = dr["EnfermedadCronica"].ToString(),
                                    Observaciones = dr["Observaciones"].ToString()
                                };

                                // Agregar a la lista
                                lstHistorialPacientes.Add(historial);
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
            return lstHistorialPacientes;
        }
        #endregion

        #region METODO PARA INSERTAR
        // METODO PARA INSERTAR HISTORIAL
        public int InsertarHistorial(HistorialPacientesModel historial)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SPINSERTARHISTORIALPACIENTE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PacienteID", this.PacienteID);
                        cmd.Parameters.AddWithValue("@MotivoConsulta", this.MotivoConsulta);
                        cmd.Parameters.AddWithValue("@Padecimientos", this.Padecimientos);
                        cmd.Parameters.AddWithValue("@Traumatismos", this.Traumatismos);
                        cmd.Parameters.AddWithValue("@CirugiasPrevias", this.CirugiasPrevias);
                        cmd.Parameters.AddWithValue("@MedicacionActual", this.MedicacionActual);
                        cmd.Parameters.AddWithValue("@AntecedentesFamiliares", this.AntecedentesFamiliares);
                        cmd.Parameters.AddWithValue("@Discapacidad", this.Discapacidad);
                        cmd.Parameters.AddWithValue("@Alergia", this.Alergia);
                        cmd.Parameters.AddWithValue("@EnfermedadCronica", this.EnfermedadCronica);
                        cmd.Parameters.AddWithValue("@Observaciones", this.Observaciones);

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
        public int ModificarHistorial(HistorialPacientesModel h)
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
                        cmd.CommandText = "SPACTUALIZARHISTORIALPACIENTE";

                        // Parámetros para el procedimiento almacenado
                        cmd.Parameters.AddWithValue("@PacienteID", this.PacienteID);
                        cmd.Parameters.AddWithValue("@MotivoConsulta", this.MotivoConsulta);
                        cmd.Parameters.AddWithValue("@Padecimientos", Padecimientos);
                        cmd.Parameters.AddWithValue("@Traumatismos", this.Traumatismos);
                        cmd.Parameters.AddWithValue("@CirugiasPrevias", this.CirugiasPrevias);
                        cmd.Parameters.AddWithValue("@MedicacionActual", this.MedicacionActual);
                        cmd.Parameters.AddWithValue("@AntecedentesFamiliares", this.AntecedentesFamiliares);
                        cmd.Parameters.AddWithValue("@Discapacidad", this.Discapacidad);
                        cmd.Parameters.AddWithValue("@Alergia", Alergia);
                        cmd.Parameters.AddWithValue("@EnfermedadCronica", this.EnfermedadCronica);
                        cmd.Parameters.AddWithValue("@Observaciones", this.Observaciones);

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

        #region METODO BUSQUEDA PACIENTES
        public List<HistorialPacientesModel> BuscarHistorial(int? id = null)
        {
            List<HistorialPacientesModel> lstHistorial = new List<HistorialPacientesModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARHISTORIALPORPACIENTEID";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por ID
                        comand.Parameters.AddWithValue("@PacienteID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                HistorialPacientesModel h = new HistorialPacientesModel
                                {
                                    ID = Convert.ToInt32(dr["HistorialPacienteID"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    MotivoConsulta = dr["MotivoConsulta"].ToString(),
                                    Nombre = dr["NombrePaciente"].ToString(),
                                    Apellido = dr["ApellidoPaciente"].ToString(),
                                    Padecimientos = dr["Padecimientos"].ToString(),
                                    Traumatismos = dr["Traumatismos"].ToString(),
                                    CirugiasPrevias = dr["CirugiasPrevias"].ToString(),
                                    MedicacionActual = dr["MedicacionActual"].ToString(),
                                    AntecedentesFamiliares = dr["AntecedentesFamiliares"].ToString(),
                                    Discapacidad = dr["Discapacidad"].ToString(),
                                    Alergia = dr["Alergia"].ToString(),
                                    EnfermedadCronica = dr["EnfermedadCronica"].ToString(),
                                    Observaciones = dr["Observaciones"].ToString()
                                };

                                // Agregar a la lista
                                lstHistorial.Add(h);
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
            return lstHistorial;
        }
        #endregion

    }
}
