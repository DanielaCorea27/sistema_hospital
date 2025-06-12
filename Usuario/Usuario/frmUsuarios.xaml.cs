using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using Usuario.Models;
using Usuario.Services;
//La vista del reporte
using Usuario.Reports;

//Incluir librerias SQL
using System.Data;
using System.Collections.ObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Reflection;
using Usuario.ManejarRoles;

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmUsuarios.xaml
    /// </summary>
    public partial class frmUsuarios : Window
    {
        private int EnviarRol;
        public frmUsuarios(int VerificarRol)
        {
            InitializeComponent();
            //Mostrar usuarios
            MostrarUsuarios();
            EnviarRol = VerificarRol;
            FiltrarRoles(VerificarRol);
        }

        #region CONEXION A LA BD Y CONSULTAS CORTO
        //Conexion a la BD
        SqlConnection conDB = new SqlConnection(Properties.Settings.Default.conexionDB);

        //VARIABLE PARA CONSULTAS SQL
        string consultaSQL = null;
        #endregion

        #region DECLARACION DE VARIABLES LOCALES CORTO
        //Variables de estado
        bool Agregando = false, Editando = false;
        bool AgregandoRepeat = false, EditandoRepeat = false;

        //Variable para almacenar el id del usuario
        int usuarioid = 0; //almacenar el id del usuario actual

        string email = "";//EDITADO
        #endregion

        #region CONTROL DE BOTONES
        public void FiltrarRoles(int rolId)
        {
            if (RolesConfigurar.PermisosPorRol.ContainsKey(rolId))
            {
                var permisos = RolesConfigurar.PermisosPorRol[rolId];


                btnPacientes.Visibility = permisos["btnPacientes"] ? Visibility.Visible : Visibility.Collapsed;
                btnHistorialesMedicos.Visibility = permisos["btnHistorialesMedicos"] ? Visibility.Visible : Visibility.Collapsed;
                btnMedicos.Visibility = permisos["btnMedicos"] ? Visibility.Visible : Visibility.Collapsed;
                btnConsultas.Visibility = permisos["btnConsultas"] ? Visibility.Visible : Visibility.Collapsed;
                btnRecetas.Visibility = permisos["btnRecetas"] ? Visibility.Visible : Visibility.Collapsed;
                btnCitas.Visibility = permisos["btnCitas"] ? Visibility.Visible : Visibility.Collapsed;
                btnExamenes.Visibility = permisos["btnExamenes"] ? Visibility.Visible : Visibility.Collapsed;
                btnReportes.Visibility = permisos["btnReportes"] ? Visibility.Visible : Visibility.Collapsed;
                btnUsuarios.Visibility = permisos["btnUsuarios"] ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                btnPacientes.Visibility = Visibility.Collapsed;
                btnHistorialesMedicos.Visibility = Visibility.Collapsed;
                btnMedicos.Visibility = Visibility.Collapsed;
                btnConsultas.Visibility = Visibility.Collapsed;
                btnRecetas.Visibility = Visibility.Collapsed;
                btnCitas.Visibility = Visibility.Collapsed;
                btnExamenes.Visibility = Visibility.Collapsed;
                btnReportes.Visibility = Visibility.Collapsed;
                btnUsuarios.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #region METODO PERSONALIZADO PARA MOSTRAR USUARIOS CORTO
        void MostrarUsuarios()
        {
            dgUsuarios.ItemsSource = DatoUsuario.MuestraUsuario();
        }
        #endregion

        #region METODO PERSONALIZADO HABILITAR OBJETOS
        //Metodo para habilitar y deshabilitar objetos del formulario
        void HabilitarObjetos(bool accion)
        {
            //minimizar codigo
            if (accion == true)
            {
                //Habilitar los objetos
                txtNombre.IsEnabled = true; txtApellido.IsEnabled = true; txtEmail.IsEnabled = true; txtBuscar.IsEnabled = false; txtClave.IsEnabled = true; txtConfirmacion.IsEnabled = true; cmbRoles.IsEnabled = true;
            }
            else
            {
                //deshabilitar los objetos
                txtNombre.IsEnabled = false; txtApellido.IsEnabled = false; txtEmail.IsEnabled = false; txtBuscar.IsEnabled = true; txtClave.IsEnabled = false; txtConfirmacion.IsEnabled = false; cmbRoles.IsEnabled = false;
            }
        }
        #endregion

        #region METODO PERSONALIZADO LIMPIAR
        void LimpiarObjetos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtBuscar.Clear();
            txtClave.Clear();
            txtConfirmacion.Clear();
            cmbRoles.SelectedItem = null;
        }
        #endregion

        #region METODO PERSONALIZADO CONTROL DEL TOOLBAR
        //Metodo para controlar la ToolBar
        void controlToolBar()
        {
            //Si el DataGrid no tiene registros
            if (dgUsuarios.Items.Count == 0)
            {
                btnNewUser.Visibility = Visibility.Visible;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnEditar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Collapsed;
                btnEliminar.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Si el DataGrid tienepor lo menos un  registro
                btnNewUser.Visibility = Visibility.Visible;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnEditar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Collapsed;
                btnEliminar.Visibility = Visibility.Visible;
            }
            if (Agregando || Editando || (!string.IsNullOrEmpty(txtNombre.Text)))
            {
                // Si estoy AGREGANDO O EDITANDO UN REGISTRO
                btnGuardar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Visible;
                btnNewUser.Visibility = Visibility.Collapsed;
                btnEditar.Visibility = Visibility.Collapsed;
                btnEliminar.Visibility = Visibility.Collapsed;
                if ((!string.IsNullOrEmpty(txtNombre.Text)))
                {
                    btnCancelar.Visibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        #region METODO PERSONALIZADO VALIDACION
        //metodo para validar el formualrio
        bool ValidarFormulario()
        {
            bool estado = true;
            string mensaje = null;

            //txtNombre
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                estado = false;
                mensaje += "Nombre de usuario\n";
            }
            //txtApellido
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                estado = false;
                mensaje += "Apellido de usuario\n";
            }
            //txtCorreo
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                estado = false;
                mensaje += "Email\n";
            }
            //txtRol
            if (cmbRoles.SelectedItem == null)
            {
                estado = false;
                mensaje += "Roles\n";
            }
            //txtCalve
            if (string.IsNullOrEmpty(txtClave.Password))
            {
                estado = false;
                mensaje += "Clave\n";
            }
            //txtCponfrimacion
            if (string.IsNullOrEmpty(txtConfirmacion.Password))
            {
                estado = false;
                mensaje += "Confirmacion\n";
            }
            //validar contraseñas
            if (estado)// sit doas las cjas estan llenas
            {
                if (txtClave.Password != txtConfirmacion.Password)
                {
                    estado = false;
                    mensaje += "Las contraseñas no son iguales\n";
                }
            }
            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos: \n" + mensaje, "Validacion de formularios", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return estado;
        }
        #endregion

        #region METODO BOTON AGREGAR NUEVO
        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            //Habilitar objetos
            HabilitarObjetos(true);

            //Limpiar Objetos
            LimpiarObjetos();

            //Extablecer estado de AGREGADO
            Agregando = true;
            Editando = false;

            //Configurar toolbar
            controlToolBar();

            //Enviar el enfoque al txt de Nombre
            txtNombre.Focus();
        }
        #endregion

        #region METODO BOTON CANCELAR
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //pedir confirmacion
            if (MessageBox.Show("¿Desea cancelar la operacion?",
                "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //Limpiar Objetos
                LimpiarObjetos();

                //Bloquear objetos
                HabilitarObjetos(false);

                //Establecer valor de eitando o agregando en false
                Agregando = false;
                Editando = false;

                //Configurar toolbar
                controlToolBar();
            }
        }
        #endregion

        #region METODO BOTON GUARDAR
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Datos: "+ Agregando + AgregandoRepeat + Editando + EditandoRepeat + usuarioid);
            //variable para mensajes de estado
            string mensaje = null;
            if (ValidarFormulario())
            {
                //recuperar valores del formualrio
                UsuariosModel usuario = new UsuariosModel();
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.Email = txtEmail.Text;
                usuario.Rol = Convert.ToInt32(cmbRoles.SelectedValue);
                int RolEscogido = usuario.Rol;
                usuario.Clave = txtClave.Password;

                // Recuperar valores del formulario para médicos
                MedicosModel medico = new MedicosModel();
                medico.Nombre = txtNombre.Text;
                medico.Apellido = txtApellido.Text;
                medico.Email = txtEmail.Text;

                //evaluar si esta agregando
                if (Agregando)
                {
                    AgregandoRepeat = true;
                    EditandoRepeat = false;
                    //Llamar al metodo de insercion
                    usuarioid = DatoUsuario.AltaUsuarios(usuario);
                    mensaje = "Registro almacenado correctamente";
                }
                else
                {
                    EditandoRepeat = true;
                    AgregandoRepeat = false;
                    //Llamar al estado para editar
                    usuarioid = DatoUsuario.ModificarUsuario(usuario, email);
                    
                    mensaje = "Registro modificado exitosamente";
                }

                //Evaluar si s4e ingreo el registro
                if (usuarioid > 0)
                {
                    // Mensaje de confirmación
                    MessageBox.Show(mensaje, "Validación del formulario", MessageBoxButton.OK, MessageBoxImage.Information);
                    if ((VerificarUsuario(usuario.Email) == 0) || Agregando==true)
                    {
                        if (RolEscogido == 3)
                        {
                            // Mostrar mensaje y redirigir a frmMedicos
                            if (MessageBox.Show("Ve y agrega al medico", "Información", MessageBoxButton.OK) == MessageBoxResult.OK)
                            {
                                // Crear instancia de frmMedicos
                                frmMedicos medicos = new frmMedicos(EnviarRol);

                                // Pasar los valores a frmMedicos
                                medicos.Nombre = txtNombre.Text;
                                medicos.Apellido = txtApellido.Text;
                                medicos.Email = txtEmail.Text;
                                medicos.bandera = true;

                                // Mostrar frmMedicos
                                medicos.Show();
                                this.Hide(); // Oculta frmMedicos si lo deseas
                            }
                        }
                        else if (RolEscogido == 4)
                        {
                            // Mostrar mensaje y redirigir a frmMedicos
                            if (MessageBox.Show("Ve y agrega al paciente", "Información", MessageBoxButton.OK) == MessageBoxResult.OK)
                            {
                                // Crear instancia de pacientes
                                frmPacientes paciente = new frmPacientes(EnviarRol);

                                // Pasar los valores a frmMedicos
                                paciente.Nombre = txtNombre.Text;
                                paciente.Apellido = txtApellido.Text;
                                paciente.Email = txtEmail.Text;
                                paciente.bandera = true;

                                // Mostrar frmMedicos
                                paciente.Show();
                                this.Hide(); // Oculta frmMedicos si lo deseas
                            }
                        }
                    }

                    //limpiamos cajas de texto
                    LimpiarObjetos();

                    //recargar el datagrid
                    MostrarUsuarios();

                    //Reinicar las variables de estado
                    Agregando = false;
                    Editando = false;

                    //deshabilitamos las cajas de texto
                    HabilitarObjetos(false);

                    //manejar los botones
                    controlToolBar();
                }
                else
                {
                    if (usuarioid == 0)
                    {
                        if (AgregandoRepeat==true)
                        {
                            HabilitarObjetos(true);
                            //variables de estado
                            Agregando = true;
                            Editando = false;
                        }
                        else if (EditandoRepeat == true)
                        {
                            HabilitarObjetos(true);
                            //variables de estado
                            Agregando = false;
                            Editando = true;
                        }
                    }else{
                        HabilitarObjetos(false);
                        //variables de estado
                        Agregando = false;
                        Editando = false;
                    }
                }
                //actualizar mi datagrid
                MostrarUsuarios();
                controlToolBar();
            }
        }
        #endregion

        #region METODO BOTON EDITAR
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            //Bloquear objetos
            HabilitarObjetos(true);
            cmbRoles.IsEnabled = false;
            //Establecer valor de eitando o agregando en false
            Agregando = false;
            Editando = true;

            //Configurar toolbar
            controlToolBar();

            //Enviar el enfoque al txt de Nombre
            txtNombre.Focus();
        }
        #endregion

        #region METODO BOTON ELIMINAR
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //validar si tenemos registros para eliminar
            if (dgUsuarios.Items.Count > 0)
            {
                if (MessageBox.Show("¿Desea eliminar el registro #" + usuarioid + " ?",
                "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    UsuariosModel Deamil = new UsuariosModel();
                    Deamil.Email = email;

                    MedicosModel DeamilM = new MedicosModel();
                    DeamilM.Email = email;

                    //Proceder con la eliminacion
                    if (DatoUsuario.EliminarUsuario(Deamil) > 0)
                    {
                        DatoMedico.EliminarMedico(DeamilM);

                        MessageBox.Show("Registro eliminado correctamente",
                "Validacion", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar el formulario
                        LimpiarObjetos();

                        //Recargar el datagrid
                        MostrarUsuarios();

                        //Reiniciar variables de estado
                        Agregando = false;
                        Editando = false;

                        //habilitar botones
                        controlToolBar();
                    }
                }
            }
        }
        #endregion

        #region METODO DEL PROGRAMA

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarRol();

            //Deshabilitar objetos
            HabilitarObjetos(false);
            //Habilitar botones
            controlToolBar();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frmInicio inicio = new frmInicio(EnviarRol);
            inicio.Show();
            this.Close();
        }
        private void btnMedicos_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmMedicos ventana = new frmMedicos(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscar.Text;

            int id;
            List<UsuariosModel> usuariosFiltrados;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                usuariosFiltrados = DatoUsuario.BuscarUsuarios(id: id);
            }
            else
            {
                usuariosFiltrados = DatoUsuario.BuscarUsuarios();
            }

            dgUsuarios.ItemsSource = new ObservableCollection<UsuariosModel>(usuariosFiltrados);
        }

        #endregion

        #region METODO DEL DATAGRID
        private void dgUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UsuariosModel user = (UsuariosModel)dgUsuarios.SelectedItem;
            if (user == null)
            {
                return;
            }

            //llenar las cajas de texto con lo que traemos del DG
            usuarioid = user.usuarioId;
            txtNombre.Text = user.Nombre;
            txtApellido.Text = user.Apellido;
            txtEmail.Text = user.Email;
            email = user.Email;//EDITADO
            cmbRoles.Text = user.Role;
            txtClave.Password = user.Clave;
            txtConfirmacion.Password = user.Clave;
        }

        private void btnConsultas_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmConsultas inicio = new frmConsultas(EnviarRol);
            inicio.Show();
            this.Close();
        }

        private void btnExamenes_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmMenuExamen inicio = new frmMenuExamen(EnviarRol);
            inicio.Show();
            this.Close();
        }

        private void btnRecetas_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmRecetas inicio = new frmRecetas(EnviarRol);
            inicio.Show();
            this.Close();
        }

        private void btnCitas_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmCitas ventana = new frmCitas(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnPacientes_Click(object sender, RoutedEventArgs e)
        {
            frmPacientes ventana = new frmPacientes(EnviarRol);
            ventana.Show();
            this.Close();

        }

        private void btnHistorialesMedicos_Click(object sender, RoutedEventArgs e)
        {
            frmHistorialPacientes ventana = new frmHistorialPacientes(EnviarRol);
            ventana.Show();
            this.Close();
        }
        #endregion

        #region MostrarRoles
        void CargarRol()
        {
            DatoUsuario rol = new DatoUsuario();

            //Llamar al método CargarEspecialidades
            rol.CargarRoles(cmbRoles);
        }
        #endregion

        #region VERIFICARUSUARIO
        public int VerificarUsuario(string email)
        {
            int resultado = 0;

            //aperturar la BD
            if (conDB.State == ConnectionState.Closed)
            {
                conDB.Open();

                //Consulta para buscar el usuario
                consultaSQL = null;
                consultaSQL = "SELECT dbo.FNVERIFICARUSUARIO(@Email)";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;
                //enviar valores por parametro
                sqlCmd.Parameters.AddWithValue("@Email", email.Trim());

                //Ejecutar consulta SQL
                resultado = Convert.ToInt32(sqlCmd.ExecuteScalar());

                //Evaluar el resultado
                if (resultado == 1)
                {
                    return 1;
                }
                //cerrar la base de datos o la conexion
                conDB.Close();
            }
            return 0;
        }
        #endregion
    }
}
