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
    public class DatoReceta
    {
        public DatoReceta()
        {
        }
        #region METODO PARA CARGAR EL DATAGRID DE CONSULTAS EN RECET
        // Método para cargar el DataGrid
        public static List<RecetaModel> MuestraConsultasReceta()
        {
            List<RecetaModel> lstConsulta = new List<RecetaModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARCONSULTASRECETAS";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                RecetaModel consulta = new RecetaModel
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

        #region METODO BUSQUEDA CONSULTAS EN EXAMEN
        public static List<RecetaModel> BuscarConsultasReceta(int? id = null)
        {
            List<RecetaModel> lstConsulta = new List<RecetaModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARCONSULTASRECETAS";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@ConsultaID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                RecetaModel consulta = new RecetaModel
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

        #region METODO PARA CARGAR EL DATAGRID DE MEDICAMENTOS
        // Método para cargar el DataGrid
        public static List<RecetaModel> MuestraMedicamentos()
        {
            List<RecetaModel> lstMedicamento = new List<RecetaModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARMEDICAMENTOS";

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                RecetaModel medicamento = new RecetaModel
                                {
                                    MedicamentosID = Convert.ToInt32(dr["MedicamentosID"]),
                                    NombreMedicamento = dr["NombreMedicamento"].ToString(),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"])
                                };

                                // Agregar a la lista
                                lstMedicamento.Add(medicamento);
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
            return lstMedicamento;
        }
        #endregion

        #region METODO BUSQUEDA MEDICAMENTOS
        public static List<RecetaModel> BuscarMedicamentos(int? id = null)
        {
            List<RecetaModel> lstMedicamento = new List<RecetaModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARMEDICAMENTOS";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@MedicamentosID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                RecetaModel medicamento = new RecetaModel
                                {
                                    MedicamentosID = Convert.ToInt32(dr["MedicamentosID"]),
                                    NombreMedicamento = dr["NombreMedicamento"].ToString(),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"])
                                };

                                // Agregar a la lista
                                lstMedicamento.Add(medicamento);
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
            return lstMedicamento;
        }
        #endregion

        #region METODO PARA CAMBIAR DISPONIBILIDAD CONSULTA
        //Metodo para eliminar medico
        public static int CambiarDisponibilidad(RecetaModel consulta)
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
                        comand.CommandText = "SPCAMBIARDISPONIBILIDADCONSULTA";
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

        //GuardarReceta
        #region METODO PARA INSERTAR
        // Método para insertar médico
        public static int InsertaReceta(RecetaModel receta)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SPINSERTARRECETA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaEmision", receta.FechaEmision);
                        cmd.Parameters.AddWithValue("@PacienteID", receta.PacienteID);
                        cmd.Parameters.AddWithValue("@MedicoID", receta.MedicoID);
                        cmd.Parameters.AddWithValue("@ConsultaID", receta.ConsultaID);

                        // Utilizamos ExecuteScalar para capturar el ID recién insertado
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            res = Convert.ToInt32(result); // Convertimos el resultado al tipo int
                        }
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

        //Guardar Medicamentos
        #region METODO PARA INSERTAR
        // Método para insertar médico
        public static int InsertaMedicamento(RecetaModel receta)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SPINSERTARMEDICAMENTORECETA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RecetaID", receta.RecetaID);
                        cmd.Parameters.AddWithValue("@MedicamentosID", receta.MedicamentosID);
                        cmd.Parameters.AddWithValue("@Indicaciones", receta.Indicaciones);

                        res = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al insertar registro" + ex, "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return res;
        }
        // Fin del método insertar
        #endregion

        //Camiar estado a entregado
        #region METODO PARA ELIMINAR
        //Metodo para eliminar receta
        public static int CambiarEstadoReceta(RecetaModel receta)
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
                        comand.CommandText = "SPCAMBIARESTADORECETA";
                        comand.Parameters.AddWithValue("@RecetaID", receta.RecetaID);

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

        //EliminarReceta
        #region METODO PARA ELIMINAR
        //Metodo para eliminar receta
        public static int EliminarReceta(RecetaModel receta)
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
                        comand.CommandText = "SPELIMINARRECETA";
                        comand.Parameters.AddWithValue("@RecetaID", receta.RecetaID);

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

        //Mostrar Recetas
        #region METODO PARA CARGAR EL DATAGRID DE RECETAS
        // Método para cargar el DataGrid
        public static List<RecetaModel> MuestraReceta()
        {
            List<RecetaModel> lstReceta = new List<RecetaModel>();

            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var command = new SqlCommand("SPMOSTRARRECETAS", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var dr = command.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                RecetaModel receta = new RecetaModel
                                {
                                    RecetaID = Convert.ToInt32(dr["RecetaID"]),
                                    FechaEmision = Convert.ToDateTime(dr["FechaEmision"]),
                                    PacienteID = Convert.ToInt32(dr["PacienteID"]),
                                    MedicoID = Convert.ToInt32(dr["MedicoID"]),
                                    ConsultaID = Convert.ToInt32(dr["ConsultaID"]),
                                    NombreEstadoReceta = dr["NombreEstadoReceta"].ToString()
                                };

                                // Agregar a la lista
                                lstReceta.Add(receta);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Error de base de datos: " + sqlEx.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return lstReceta;
        }
        #endregion

        //Buscar Recetas
        #region METODO BUSQUEDA CONSULTAS EN RECETA
        public static List<RecetaModel> BuscarReceta(int? id = null)
        {
            List<RecetaModel> lstReceta = new List<RecetaModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARRECETAS";  // Llama al procedimiento almacenado

                        // Parámetro para buscar por MedicoID
                        comand.Parameters.AddWithValue("@RecetaID", id.HasValue ? (object)id.Value : DBNull.Value);

                        // Ejecutar el procedimiento y leer los resultados
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                RecetaModel receta = new RecetaModel
                                {
                                    RecetaID = dr.GetInt32(dr.GetOrdinal("RecetaID")),
                                    FechaEmision = Convert.ToDateTime(dr["FechaEmision"]),
                                    PacienteID = dr.GetInt32(dr.GetOrdinal("PacienteID")),
                                    MedicoID = dr.GetInt32(dr.GetOrdinal("MedicoID")),
                                    ConsultaID = dr.GetInt32(dr.GetOrdinal("ConsultaID")),
                                    NombreEstadoReceta = dr["NombreEstadoReceta"].ToString()
                                };

                                // Agregar a la lista
                                lstReceta.Add(receta);
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
            return lstReceta;
        }
        #endregion

        #region MEDICAMENTOS POR RECETAS
        public static List<RecetaModel> ObtenerMedicamentosPorReceta(int RecetaID)
        {
            List<RecetaModel> medicamento = new List<RecetaModel>();

            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT mr.MedicamentoRecetaID, m.NombreMedicamento, mr.Indicaciones " +
                                      "FROM Medicamentos m " +
                                      "INNER JOIN MedicamentoReceta mr ON m.MedicamentosID = mr.MedicamentosID " +
                                      "WHERE mr.RecetaID = @RecetaID";
                    cmd.Parameters.AddWithValue("@RecetaID", RecetaID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            medicamento.Add(new RecetaModel
                            {
                                MedicamentosID = Convert.ToInt32(reader["MedicamentoRecetaID"]),
                                NombreMedicamento = reader["NombreMedicamento"].ToString(),
                                Indicaciones = reader["Indicaciones"].ToString()
                            });
                        }
                    }
                }
            }

            return medicamento;
        }


        #endregion


    }
}
