using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Usuario.ManejarRoles;


namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region DECLARACION DE VARIABLES LOCALES
        //conexion a la base de datos
        SqlConnection conDB = new SqlConnection(Properties.Settings.Default.conexionDB);

        //variable para consultas SQL
        string consultaSQL = null;
        #endregion

        #region OBTENER ROL
        public int ObtenerRolPorEmail(string email)
        {
            int rolId = 0;

            // Abrir la conexión a la base de datos
            if (conDB.State == ConnectionState.Closed)
            {
                conDB.Open();
            }

            using (SqlCommand command = new SqlCommand("SPBUSCARUSUARIOROL", conDB))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Agregar el parámetro de entrada para el email
                command.Parameters.AddWithValue("@Email", email);

                // Configurar el parámetro de salida para RolID
                SqlParameter rolIdParam = new SqlParameter("@RolID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(rolIdParam);

                // Ejecutar el comando
                command.ExecuteNonQuery();

                // Verificar y asignar el valor del parámetro de salida
                if (command.Parameters["@RolID"].Value != DBNull.Value)
                {
                    rolId = (int)command.Parameters["@RolID"].Value;
                }
            }

            // Cerrar la conexión a la base de datos
            if (conDB.State == ConnectionState.Open)
            {
                conDB.Close();
            }

            return rolId;
        }
        #endregion


        #region METODOS PERSONALIZADOS
        void EncontrarUsuario()
        {
            int resultado = 0;
            int pacienteID = 0;

            //aperturar la BD
            if (conDB.State == ConnectionState.Closed)
            {
                conDB.Open();

                //Consulta para buscar el usuario
                consultaSQL = null;
                consultaSQL = "SELECT dbo.FNENCONTRARUSUARIO(@User,@Password)";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;
                //enviar valores por parametro
                sqlCmd.Parameters.AddWithValue("@User", txtCorreo.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password.Trim());

                //Ejecutar consulta SQL
                resultado = Convert.ToInt32(sqlCmd.ExecuteScalar());

                //Evaluar el resultado
                if (resultado == 1)
                {
                    string email = txtCorreo.Text;
                    int verificarRol = ObtenerRolPorEmail(email);
                    pacienteID = ObtenerPacienteIDPorEmail(email);
                    if (verificarRol==4)
                    {
                        //instanciar el formulario de pacientes
                        frmPrueba ventana = new frmPrueba(pacienteID);
                        ventana.Show();
                        this.Close();
                    }
                    else
                    {
                        //instanciar el formulario de inicio
                        frmInicio inicio = new frmInicio(verificarRol);
                        inicio.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //cerrar la base de datos o la conexion
                conDB.Close();
            }
        }
        #endregion

        #region LLAMAR ID PACIENTE
        private int ObtenerPacienteIDPorEmail(string email)
        {
            int pacienteID = 0;

            try
            {
                using (SqlCommand command = new SqlCommand(
                    "SELECT p.PacienteID " +
                    "FROM Pacientes p " +
                    "INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID " +
                    "WHERE u.Email = @Email", conDB))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    if (conDB.State == ConnectionState.Closed)
                    {
                        conDB.Open();
                    }

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        pacienteID = Convert.ToInt32(result);
                    }

                    conDB.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener PacienteID: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (conDB.State == ConnectionState.Open)
                {
                    conDB.Close();
                }
            }

            return pacienteID;
        }

        #endregion


        #region EVENTOS DEL FORMULARIO
        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            //Llamada al metodo para validar el ususario
            EncontrarUsuario();
        }
        #endregion


    }
}
