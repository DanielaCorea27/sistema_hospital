using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//INCLUIR LIBRERIAS DE SQL
using System.Data.SqlClient;
using System.Data;
using Usuario.Models;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;

namespace Usuario.Services
{
    public class DatoUsuario
    {
        //atributos 

        //metodos
        //constructor vacio 
        public DatoUsuario()
        {

        }

        #region METODO PARA CARGAR EL DATAGRID
        //METODO PARA CARGAR EL DATAGRID
        public static List<UsuariosModel> MuestraUsuario()
        {
            List<UsuariosModel> lstUsuarios = new List<UsuariosModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPMOSTRARUSUARIOS";
                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                UsuariosModel user = new UsuariosModel
                                {
                                    usuarioId = Convert.ToInt32(dr["UsuarioID"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    //Rol = Convert.ToInt32(dr["RolID"]),
                                    Role = dr["Rol"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    //Estado = Convert.ToInt32(dr["EstadoID"])
                                };

                                // Agregar a la lista
                                lstUsuarios.Add(user);
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
            return lstUsuarios;
        }


        #endregion

        #region METODO PARA INSERTAR
        //Metodo para insertar usuario
        public static int AltaUsuarios(UsuariosModel usuario)
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
                        comand.CommandText = "SPINSERTARUSUARIO";
                        comand.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        comand.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                        comand.Parameters.AddWithValue("@Email", usuario.Email);
                        comand.Parameters.AddWithValue("@RolID", usuario.Rol);
                        comand.Parameters.AddWithValue("@Clave", usuario.Clave);

                        res = comand.ExecuteNonQuery();

                        /*
                        * comand.ExecuteNonQuery();
                        * res = 1;
                        */
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Valor duplicado"))
                {
                    MessageBox.Show("Error: Ya existe un registro con el misma Email", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Guardar: Ocurrió un error al intentar insertar los registros: "+ ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return res;
        }
        #endregion

        #region METODO PARA EDITAR
        // Método para editar usuario
        public static int ModificarUsuario(UsuariosModel usuario, string email)
        {
            int res = 0;
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        // Establecer el tipo de comando como procedimiento almacenado
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPACTUALIZARUSUARIO";

                        // Parámetros para el procedimiento almacenado
                        comand.Parameters.AddWithValue("@EmailActual", email);
                        comand.Parameters.AddWithValue("@NuevoEmail", usuario.Email);
                        comand.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        comand.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                        comand.Parameters.AddWithValue("@RolID", usuario.Rol);
                        comand.Parameters.AddWithValue("@Clave", usuario.Clave);

                        // Ejecutar el comando
                        res = comand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show("Editado: Ocurrió un error al intentar modificar los registros: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }
        // Fin del método editar
        #endregion

        #region METODO PARA ELIMINAR
        //Metodo para eliminar usuario
        public static int EliminarUsuario(UsuariosModel usuario)
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
                        comand.Parameters.AddWithValue("@Email", usuario.Email);

                        res = comand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alerta:" + ex.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return res;
        }//fin del metodo eliminar
        #endregion

        #region METODO BUSQUEDA
        public static List<UsuariosModel> BuscarUsuarios(int? id = null)
        {
            List<UsuariosModel> lstUsuarios = new List<UsuariosModel>();
            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();
                    using (var comand = conn.CreateCommand())
                    {
                        comand.CommandType = CommandType.StoredProcedure;
                        comand.CommandText = "SPBUSCARUSUARIOS";

                        // Si el ID no es nulo, se pasa el valor, si es nulo, se pasa DBNull
                        comand.Parameters.AddWithValue("@UsuarioID", id.HasValue ? (object)id.Value : DBNull.Value);

                        using (DbDataReader dr = comand.ExecuteReader())
                        {
                            // Recorrer el DataReader
                            while (dr.Read())
                            {
                                UsuariosModel user = new UsuariosModel
                                {
                                    usuarioId = Convert.ToInt32(dr["UsuarioID"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    //Rol = Convert.ToInt32(dr["RolID"]),
                                    Role = dr["Rol"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    //Estado = Convert.ToInt32(dr["EstadoID"])
                                };

                                // Agregar a la lista
                                lstUsuarios.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar mostrar los registros: " + ex.Message, "Validación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return lstUsuarios;
        }

        #endregion

        #region Cargar Roles
        public void CargarRoles(ComboBox cmbRoles)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT RolID, Rol FROM Roles";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se llenó correctamente el DataTable
                    if (dt.Rows.Count > 0)
                    {
                        cmbRoles.ItemsSource = dt.DefaultView;
                        cmbRoles.DisplayMemberPath = "Rol";  // Mostrar el nombre del rol
                        cmbRoles.SelectedValuePath = "RolID"; // El valor que quieres usar como clave
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron roles.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar roles: " + ex.Message);
                }
            }
        }
        #endregion

    }
}