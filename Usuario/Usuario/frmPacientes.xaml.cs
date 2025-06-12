using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Usuario.ManejarRoles;
using Usuario.Models;
using Usuario.Services;

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmPacientes.xaml
    /// </summary>
    public partial class frmPacientes : Window
    {
        private int EnviarRol = 0;
        public frmPacientes(int VerificarRol)
        {
            InitializeComponent();
            MostrarPacientes();
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
        bool AgregarCel = false;

        //Variable para almacenar el id del usuario
        int pacienteID = 0; //almacenar el id del usuario actual
        string email = "";
        string telefono = "";
        #endregion

        #region METODO PERSONALIZADO PARA MOSTRAR PACIENTES
        void MostrarPacientes()
        {
            PacientesModel model = new PacientesModel();
            dgPacientes.ItemsSource = model.MostrarPacientes();
            dgTelefonos.ItemsSource = model.ObtenerTelefonosPorPaciente(pacienteID);
        }
        #endregion

        #region METODO PERSONALIZADO HABILITAR OBJETOS
        //Metodo para habilitar y deshabilitar objetos del formulario
        void HabilitarObjetos(bool accion)
        {
            txtNombre.IsEnabled = accion;
            txtApellido.IsEnabled = accion;
            txtIdentificacion.IsEnabled = accion;
            dpFechaNacimiento.IsEnabled = accion;
            txtTelefono.IsEnabled = accion;
            txtEmail.IsEnabled = accion;
            txtResidencia.IsEnabled = accion;
            cmbDepa.IsEnabled = accion;
            cmbMunici.IsEnabled = accion;
            cmbGenero.IsEnabled = accion;

            //minimizar codigo
            if (accion == true)
            {
                txtBuscar.IsEnabled = false;
                //dgPacientes.IsEnabled = false;
            }
            else
            {
                txtBuscar.IsEnabled = true;
                //dgPacientes.IsEnabled = false;
            }
        }
        #endregion

        #region METODO PERSONALIZADO LIMPIAR
        void LimpiarObjetos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtIdentificacion.Clear();
            dpFechaNacimiento.SelectedDate = null;
            txtTelefono.Clear();
            txtEmail.Clear();
            txtResidencia.Clear();
            cmbDepa.SelectedItem = null;
            cmbMunici.SelectedItem = null;
            cmbGenero.SelectedItem = null;
            txtBuscar.Clear();
        }
        #endregion

        #region METODO PERSONALIZADO CONTROL DEL TOOLBAR
        //Metodo para controlar la ToolBar
        void controlToolBar()
        {
            //Si el DataGrid no tiene registros
            if (dgPacientes.Items.Count == 0)
            {
                btnAgregarCel.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnGuardarCel.Visibility = Visibility.Collapsed;
                btnEditar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Collapsed;
                btnEliminar.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Si el DataGrid tienepor lo menos un  registro
                btnAgregarCel.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnGuardarCel.Visibility = Visibility.Collapsed;
                btnEditar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Collapsed;
                btnEliminar.Visibility = Visibility.Visible;
            }
            if (Agregando || Editando || (!string.IsNullOrEmpty(txtNombre.Text)))
            {
                // Si estoy AGREGANDO O EDITANDO UN REGISTRO
                btnGuardarCel.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Visible;
                //btnNewUser.Visibility = Visibility.Collapsed;
                btnEditar.Visibility = Visibility.Collapsed;
                btnEliminar.Visibility = Visibility.Collapsed;
                if ((!string.IsNullOrEmpty(txtNombre.Text)))
                {
                    btnCancelar.Visibility = Visibility.Collapsed;
                }
                if (Editando)
                {
                    btnCancelar.Visibility = Visibility.Visible;
                    if (DetenerTelefonos)
                    {
                        btnAgregarCel.Visibility = Visibility.Visible;
                        if (AgregarCel)
                        {
                            btnGuardarCel.Visibility = Visibility.Visible;
                            btnGuardar.Visibility = Visibility.Collapsed;
                            btnAgregarCel.Visibility = Visibility.Collapsed;
                        }
                    }
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

            //txtNombrw
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                estado = false;
                mensaje += "Nombre del paciente \n";
            }
            //txtApellido
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                estado = false;
                mensaje += "Apellido del paciente \n";
            }
            //txtIdentificacion
            if (string.IsNullOrEmpty(txtIdentificacion.Text))
            {
                estado = false;
                mensaje += "Identificacion del paciente \n";
            }
            //dpFechaNacimiento
            if (string.IsNullOrEmpty(dpFechaNacimiento.Text))
            {
                estado = false;
                mensaje += "Fecha de nacimiento del paciente \n";
            }
            //txtEmail
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                estado = false;
                mensaje += "Email\n";
            }
            //txtRecidencia
            if (string.IsNullOrEmpty(txtResidencia.Text))
            {
                estado = false;
                mensaje = "Recidencia \n";
            }
            //cmbDepa
            if (cmbDepa.SelectedItem == null)
            {
                estado = false;
                mensaje += "Departamento \n";
            }
            //cmbMunici
            if (cmbMunici.SelectedItem == null)
            {
                estado = false;
                mensaje += "Municipio \n";
            }
            //cmbGenero
            if (cmbDepa.SelectedItem == null)
            {
                estado = false;
                mensaje += "Genero \n";
            }
            //txtTelefono
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                estado = false;
                mensaje += "Telefono del paciente\n";
            }

            //validar telefono
            if (estado)
            {
                int longitud = txtTelefono.Text.Length;
                if (longitud != 8)
                {
                    estado = false;
                    mensaje += "Telefono debe llevar 8 digitos\n";
                }
            }
            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos: \n" + mensaje, "Validacion de formularios", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return estado;
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

            // Variable para mensajes de estado
            string mensaje = null;

            if (ValidarFormulario())
            {
                // Recuperar valores del formulario
                PacientesModel paciente = new PacientesModel();
                paciente.Email = txtEmail.Text;
                paciente.Identificacion = txtIdentificacion.Text;
                paciente.GeneroID = int.Parse(cmbGenero.SelectedValue.ToString());
                paciente.FechaNacimiento = Convert.ToDateTime(dpFechaNacimiento.Text);
                paciente.Recidencia = txtResidencia.Text;
                paciente.Telefono = txtTelefono.Text;
                paciente.MunicipioID = int.Parse(cmbMunici.SelectedValue.ToString());

                PacientesModel pacientee = new PacientesModel();
                pacientee.Nombre = txtNombre.Text;
                pacientee.Apellido = txtApellido.Text;
                pacientee.Email = txtEmail.Text;
                pacientee.Identificacion = txtIdentificacion.Text;
                pacientee.GeneroID = int.Parse(cmbGenero.SelectedValue.ToString());
                pacientee.Recidencia = txtResidencia.Text;
                pacientee.FechaNacimiento = Convert.ToDateTime(dpFechaNacimiento.Text);
                pacientee.Telefono = txtTelefono.Text;
                pacientee.MunicipioID = int.Parse(cmbMunici.SelectedValue.ToString());

                // Evaluar si está agregando
                if (Agregando)
                {
                    AgregandoRepeat = true;
                    // Llamar al método de inserción
                    pacienteID = paciente.InsertatPacientes(paciente, paciente.Email);
                    mensaje = "Registro almacenado correctamente";
                }
                else
                {
                    EditandoRepeat = true;
                    // Llamar al estado para editar
                    pacienteID = pacientee.ModificarPaciente(pacientee, email, telefono);
                    mensaje = "Registro modificado exitosamente";
                }
                // Evaluar si se ingresó el registro
                if (pacienteID > 0)
                {
                    // Mensaje de confirmación
                    MessageBox.Show(mensaje, "Validación del formulario", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Limpiar cajas de texto
                    LimpiarObjetos();

                    // Recargar el DataGrid
                    MostrarPacientes();

                    // Reiniciar las variables de estado
                    Agregando = false;
                    Editando = false;

                    // Deshabilitar las cajas de texto
                    HabilitarObjetos(false);

                    // Manejar los botones
                    controlToolBar();
                }
                else
                {
                    if (pacienteID == 0)
                    {
                        if (AgregandoRepeat == true)
                        {
                            HabilitarObjetos(true);
                            //variables de estado
                            Agregando = true;
                        }
                        else if (EditandoRepeat == true)
                        {
                            HabilitarObjetos(true);
                            //variables de estado
                            Editando = true;
                        }
                    }
                    else
                    {
                        HabilitarObjetos(false);
                        //variables de estado
                        Agregando = false;
                        Editando = false;
                    }
                }
                //actualizar mi datagrid
                MostrarPacientes();
                controlToolBar();
            }
        }
        #endregion

        #region METODO BOTON EDITAR
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            //Bloquear objetos
            HabilitarObjetos(true);

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
            PacientesModel pacientesModel = new PacientesModel();
            //validar si tenemos registros para eliminar
            if (dgPacientes.Items.Count > 0)
            {
                if (MessageBox.Show("¿Desea eliminar el registro #" + pacienteID + " ?",
                "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    UsuariosModel Deamil = new UsuariosModel();
                    Deamil.Email = email;

                    PacientesModel DeamilP = new PacientesModel();
                    DeamilP.Email = email;

                    //Proceder con la eliminacion
                    if (pacientesModel.EliminarPaciente(DeamilP) > 0)
                    {
                        //DatoUsuario.EliminarUsuario(Deamil);

                        MessageBox.Show("Registro eliminado correctamente",
                        "Validacion", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar el formulario
                        LimpiarObjetos();

                        //Recargar el datagrid
                        MostrarPacientes();

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

        #region METODO WINDOW
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public bool bandera { get; set; }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = Nombre;
            txtApellido.Text = Apellido;
            txtEmail.Text = Email;
            bool accion = bandera;

            if (accion)
            {
                Agregando = true;
                Editando = false;
            }
            else
            {
                Agregando = false;
                Editando = false;
            }

            //Deshabilitar objetos
            if ((!string.IsNullOrEmpty(txtNombre.Text)))
            {
                txtNombre.IsEnabled = false;
                txtApellido.IsEnabled = false;
                txtEmail.IsEnabled = false;
                txtBuscar.IsEnabled = false;
                txtIdentificacion.IsEnabled = true;
                dpFechaNacimiento.IsEnabled = true;
                txtTelefono.IsEnabled = true;
                txtResidencia.IsEnabled = true;
                cmbDepa.IsEnabled = true;
                cmbMunici.IsEnabled = true;
                cmbGenero.IsEnabled = true;
                //dgPacientes.IsEnabled = false;
            }
            else
            {
                HabilitarObjetos(false);
            }
            //Habilitar botones
            controlToolBar();
            CargarGeneros();
            // Cargar especialidades en el ComboBox al abrir el formulario
            CargarDepartamento();

        }
        #endregion

        #region METODO DEL DATAGRID
        private void dgPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PacientesModel paciente = (PacientesModel)dgPacientes.SelectedItem;
            if (paciente == null)
            {
                return;
            }

            //llenar las cajas de texto con lo que traemos del DG
            pacienteID = paciente.Id;
            txtNombre.Text = paciente.Nombre;
            txtApellido.Text = paciente.Apellido;
            txtIdentificacion.Text = paciente.Identificacion;
            dpFechaNacimiento.Text = paciente.FechaNacimiento.ToString();
            txtEmail.Text = paciente.Email;
            txtResidencia.Text = paciente.Recidencia;
            cmbDepa.Text = paciente.Departamento;
            cmbMunici.Text = paciente.Municipio;
            cmbGenero.Text = paciente.Genero;
            email = paciente.Email;//EDITADO
            MessageBox.Show("Ahora para editar seleccione el numero a editar");
            if (dgPacientes.SelectedItem != null)
            {
                PacientesModel seleccionado = (PacientesModel)dgPacientes.SelectedItem;
                CargarTelefonos(seleccionado.Id);
            }
        }
        #endregion

        #region VENTANAS
        private void cmbDepa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbMunici.ItemsSource = null;
            CargarMunicipio();

        }

        private void btnHistorialesMedicos_Click(object sender, RoutedEventArgs e)
        {
            frmHistorialPacientes ventana = new frmHistorialPacientes(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frmPacientes pacientes = new frmPacientes(EnviarRol);
            pacientes.Show();
            this.Close();
        }

        private void btnHome_Click_1(object sender, RoutedEventArgs e)
        {
            frmInicio ventana = new frmInicio(EnviarRol);
            ventana.Show();
            this.Close();
        }


        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmUsuarios ventana = new frmUsuarios(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnAgregarCel_Click(object sender, RoutedEventArgs e)
        {
            LimpiarObjetos();
            txtNombre.IsEnabled = false;
            txtApellido.IsEnabled = false;
            txtIdentificacion.IsEnabled = false;
            dpFechaNacimiento.IsEnabled = false;
            txtTelefono.IsEnabled = true;
            txtEmail.IsEnabled = false;
            txtResidencia.IsEnabled = false;
            cmbDepa.IsEnabled = false;
            cmbMunici.IsEnabled = false;
            cmbGenero.IsEnabled = false;
            AgregarCel = true;
            controlToolBar();
        }

        private void btnGuardarCel_Click(object sender, RoutedEventArgs e)
        {
            PacientesModel pacientes = new PacientesModel();
            string telefono = txtTelefono.Text;
            string mensaje = "";
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                mensaje = "El telefono esta vacio, si no agregara telefono cancele";
            }
            else
            {
                pacientes.AgregarTelefono(pacienteID, telefono);
                mensaje = "Agregado exitosamente";
                HabilitarObjetos(false);
                LimpiarObjetos();
                //variables de estado
                Agregando = false;
                Editando = false;
                AgregarCel = false;
                MostrarPacientes();
                controlToolBar();
            }
            MessageBox.Show(mensaje, "Validacion de Telefono", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            PacientesModel model = new PacientesModel();
            string filtro = txtBuscar.Text;

            int id;
            List<PacientesModel> pacienteF;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                pacienteF = model.BuscarPaciente(id: id);
            }
            else
            {
                pacienteF = model.BuscarPaciente();
            }

            dgPacientes.ItemsSource = new ObservableCollection<PacientesModel>(pacienteF);
        }

        private void dgTelefonos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PacientesModel pa = (PacientesModel)dgTelefonos.SelectedItem;
            if (pa == null)
            {
                return;
            }
            txtTelefono.Text = pa.Telefono;
            telefono = pa.Telefono;
        }

        private void btnMedicos_Click(object sender, RoutedEventArgs e)
        {
            frmMedicos ventana = new frmMedicos(EnviarRol);
            ventana.Show();
            this.Close();
        }
        #endregion

        #region CARGAR COMBO BOX
        void CargarDepartamento()
        {
            PacientesModel pacienteM = new PacientesModel();

            // Llamar al método CargarEspecialidades
            pacienteM.CargarDepartamento(cmbDepa);
        }

        void CargarMunicipio()
        {
            PacientesModel pacienteModel = new PacientesModel();

            //var x = cmbDepa.SelectedItem;
            //var y = cmbDepa.SelectedValue;
            //var m = cmbDepa.SelectedIndex;
            // Llamar al método CargarEspecialidades
            pacienteModel.CargarMunicipios(cmbMunici, Convert.ToInt32(cmbDepa.SelectedValue));
        }

        void CargarGeneros()
        {
            PacientesModel pacienteMod = new PacientesModel();

            // Llamar al método CargarEspecialidades
            pacienteMod.CargarGeneros(cmbGenero);
        }







        #endregion

        #region Cargar Telefonos
        PacientesModel pacientesModel1 = new PacientesModel();
        private bool DetenerTelefonos = true;

        private void btnConsultas_Click(object sender, RoutedEventArgs e)
        {
            frmConsultas ventana = new frmConsultas(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnRecetas_Click(object sender, RoutedEventArgs e)
        {
            frmRecetas ventana = new frmRecetas(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnCitas_Click(object sender, RoutedEventArgs e)
        {
            frmCitas ventana = new frmCitas(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnExamenes_Click(object sender, RoutedEventArgs e)
        {
            frmMenuExamen ventana = new frmMenuExamen(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            frmReportes ventana = new frmReportes(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void CargarTelefonos(int pacienteID)
        {
            var telefonos = pacientesModel1.ObtenerTelefonosPorPaciente(pacienteID);

            // Verificar si hay más de un registro
            if (telefonos.Count == 2)
            {
                DetenerTelefonos = false;
            }

            // Actualizar el DataGrid con los teléfonos
            dgTelefonos.ItemsSource = telefonos;
        }


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

    }
}
