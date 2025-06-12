using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmMedicos.xaml
    /// </summary>
    public partial class frmMedicos : Window
    {
        private int EnviarRol = 0;
        public frmMedicos(int VerificarRol)
        {
            InitializeComponent();
            MostrarMedicos();
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
        int medicoid = 0; //almacenar el id del usuario actual
        string email = "";
        string telefono = "";
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

        #region METODO PERSONALIZADO PARA MOSTRAR MEDICOS
        void MostrarMedicos()
        {
            dgMedicos.ItemsSource = DatoMedico.MuestraMedicos();
            dgTelefonos.ItemsSource = DatoMedico.ObtenerTelefonosPorMedico(medicoid);
        }
        #endregion

        #region METODO PERSONALIZADO HABILITAR OBJETOS
        //Metodo para habilitar y deshabilitar objetos del formulario
        void HabilitarObjetos(bool accion)
        {
            txtNombre.IsEnabled = accion;
            txtApellido.IsEnabled = accion;
            txtIdentificacion.IsEnabled = accion;
            cmbEspecialidad.IsEnabled = accion;
            txtCelular.IsEnabled = accion;
            txtEmail.IsEnabled = accion;
            cbHoraInicioAM.IsEnabled = accion;
            cbHoraFinPM.IsEnabled = accion;
            cmbConsultorio.IsEnabled = accion;

            //minimizar codigo
            if (accion == true)
            {
                txtBuscar.IsEnabled = false;
                dgMedicos.IsEnabled = false;
            }
            else
            {
                txtBuscar.IsEnabled = true;
                dgMedicos.IsEnabled = true;
            }
        }
        #endregion

        #region METODO PERSONALIZADO LIMPIAR
        void LimpiarObjetos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtIdentificacion.Clear();
            cmbEspecialidad.SelectedItem = null;
            txtCelular.Clear();
            txtEmail.Clear();
            cbHoraInicioAM.SelectedItem = null;
            cbHoraFinPM.SelectedItem = null;
            cmbConsultorio.SelectedItem = null;
            txtBuscar.Clear();
        }
        #endregion

        #region METODO PERSONALIZADO CONTROL DEL TOOLBAR
        //Metodo para controlar la ToolBar
        void controlToolBar()
        {
            //Si el DataGrid no tiene registros
            if (dgMedicos.Items.Count == 0)
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
                mensaje += "Nombre de medico\n";
            }
            //txtApellido
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                estado = false;
                mensaje += "Apellido de medico\n";
            }
            //txtIdentificacion
            if (string.IsNullOrEmpty(txtIdentificacion.Text))
            {
                estado = false;
                mensaje += "Identificacion de medico\n";
            }
            //txtEspecialidad
            if (cmbEspecialidad.SelectedItem == null)
            {
                estado = false;
                mensaje += "Especialidad\n";
            }
            //txtTelefono
            if (string.IsNullOrEmpty(txtCelular.Text))
            {
                estado = false;
                mensaje += "Telefono de medico\n";
            }
            //txtEmail
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                estado = false;
                mensaje += "Email\n";
            }
            //HoraInicio
            if (cbHoraInicioAM.SelectedItem == null)
            {
                estado = false;
                mensaje += "Hora Inicio\n";
            }
            //HoraFin
            if (cbHoraFinPM.SelectedItem == null)
            {
                estado = false;
                mensaje += "Hora Fin\n";
            }
            //Consultorios
            if (cmbConsultorio.SelectedItem == null)
            {
                estado = false;
                mensaje += "Consultorio\n";
            }
            //validar telefono
            if (estado)// sit doas las cjas estan llenas
            {
                int longitud = txtCelular.Text.Length;
                if (longitud!=8)
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
                MedicosModel datos = new MedicosModel();
                datos.Email = txtEmail.Text;
                datos.Identificacion = txtIdentificacion.Text;
                datos.EspecialidadID = Convert.ToInt32(cmbEspecialidad.SelectedValue);
                datos.Telefono = txtCelular.Text;
                datos.HoraInicioID = Convert.ToInt32(cbHoraInicioAM.SelectedValue);
                datos.HoraFinID = Convert.ToInt32(cbHoraFinPM.SelectedValue);
                datos.ConsultorioID = Convert.ToInt32(cmbConsultorio.SelectedValue);

                MedicosModel medico = new MedicosModel();
                medico.Nombre = txtNombre.Text;
                medico.Apellido = txtApellido.Text;
                medico.Email = txtEmail.Text;
                medico.Identificacion = txtIdentificacion.Text;
                medico.EspecialidadID = Convert.ToInt32(cmbEspecialidad.SelectedValue);
                medico.Telefono = txtCelular.Text;
                medico.HoraInicioID = Convert.ToInt32(cbHoraInicioAM.SelectedValue);
                medico.HoraFinID = Convert.ToInt32(cbHoraFinPM.SelectedValue);
                medico.ConsultorioID = Convert.ToInt32(cmbConsultorio.SelectedValue);

                // Evaluar si está agregando
                if (Agregando)
                {
                    AgregandoRepeat = true;
                    // Llamar al método de inserción
                    medicoid = DatoMedico.AltaMedicos(datos, datos.Email);
                    mensaje = "Registro almacenado correctamente";
                }
                else
                {
                    EditandoRepeat = true;
                    // Llamar al estado para editar
                    medicoid = DatoMedico.ModificarMedico(medico, email, telefono);
                    mensaje = "Registro modificado exitosamente";
                }
                // Evaluar si se ingresó el registro
                if (medicoid > 0)
                {
                    // Mensaje de confirmación
                    MessageBox.Show(mensaje, "Validación del formulario", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Limpiar cajas de texto
                    LimpiarObjetos();

                    // Recargar el DataGrid
                    MostrarMedicos();

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
                    if (medicoid == 0)
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
                MostrarMedicos();
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
            //validar si tenemos registros para eliminar
            if (dgMedicos.Items.Count > 0)
            {
                if (MessageBox.Show("¿Desea eliminar el registro #" + medicoid + " ?",
                "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    UsuariosModel Deamil = new UsuariosModel();
                    Deamil.Email = email;

                    MedicosModel DeamilM = new MedicosModel();
                    DeamilM.Email = email;

                    //Proceder con la eliminacion
                    if (DatoMedico.EliminarMedico(DeamilM) > 0)
                    {
                        DatoUsuario.EliminarUsuario(Deamil);

                        MessageBox.Show("Registro eliminado correctamente",
                "Validacion", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar el formulario
                        LimpiarObjetos();

                        //Recargar el datagrid
                        MostrarMedicos();

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
        void Window_Loaded(object sender, RoutedEventArgs e)
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
                txtNombre.IsEnabled = false; txtApellido.IsEnabled = false; txtEmail.IsEnabled = false; txtBuscar.IsEnabled = false; txtIdentificacion.IsEnabled = true; txtCelular.IsEnabled = true; cmbEspecialidad.IsEnabled = true;
                cbHoraInicioAM.IsEnabled = true;
                cbHoraFinPM.IsEnabled = true;
                cmbConsultorio.IsEnabled = true;
                dgMedicos.IsEnabled = false;
            }
            else
            {
                HabilitarObjetos(false);
            }
            //Habilitar botones
            controlToolBar();

            // Cargar especialidades en el ComboBox al abrir el formulario
            CargarEspecialidad();
            CargarHorasIniciales();
            CargarHorasFinales();
            CargarConsultorio();
        }
        #endregion

        #region METODO DEL DATAGRID
        private void dgMedicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MedicosModel medico = (MedicosModel)dgMedicos.SelectedItem;
            if (medico == null)
            {
                return;
            }

            //llenar las cajas de texto con lo que traemos del DG
            medicoid = medico.MedicoId;
            txtNombre.Text = medico.Nombre;
            txtApellido.Text = medico.Apellido;
            txtIdentificacion.Text = medico.Identificacion;
            cmbEspecialidad.Text = medico.Especialidad;
            txtEmail.Text = medico.Email;
            cbHoraInicioAM.Text = medico.HoraInicio;
            cbHoraFinPM.Text = medico.HoraFin;
            cmbConsultorio.Text = medico.Consultorio;
            email = medico.Email;//EDITADO
            MessageBox.Show("Ahora para editar seleccione el numero a editar");
            if (dgMedicos.SelectedItem != null)
            {
                MedicosModel medicoSeleccionado = (MedicosModel)dgMedicos.SelectedItem;
                CargarTelefonos(medicoSeleccionado.MedicoId);
            }
        }
        #endregion

        #region Ventanas
        

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmInicio inicio = new frmInicio(EnviarRol);
            inicio.Show();
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
            cmbEspecialidad.IsEnabled = false;
            txtCelular.IsEnabled = true;
            txtEmail.IsEnabled = false;
            cbHoraInicioAM.IsEnabled = false;
            cbHoraFinPM.IsEnabled = false;
            AgregarCel = true;
            controlToolBar();

        }

        private void btnGuardarCel_Click(object sender, RoutedEventArgs e)
        {
            string celular = txtCelular.Text;
            string mensaje = "";
            if (string.IsNullOrEmpty(txtCelular.Text))
            {
                mensaje = "El telefono esta vacio, si no agregara telefono cancele";
            }
            else
            {
                DatoMedico.AgregarTelefono(medicoid, celular);
                mensaje = "Agregado exitosamente";
                HabilitarObjetos(false);
                LimpiarObjetos();
                //variables de estado
                Agregando = false;
                Editando = false;
                AgregarCel = false;
                MostrarMedicos();
                controlToolBar();
            }
            MessageBox.Show(mensaje, "Validacion de Telefono", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscar.Text;

            int id;
            List<MedicosModel> medicosFiltrados;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                medicosFiltrados = DatoMedico.BuscarMedicos(id: id);
            }
            else
            {
                medicosFiltrados = DatoMedico.BuscarMedicos();
            }

            dgMedicos.ItemsSource = new ObservableCollection<MedicosModel>(medicosFiltrados);
        }

        private void dgTelefonos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MedicosModel medico = (MedicosModel)dgTelefonos.SelectedItem;
            if (medico == null)
            {
                return;
            }
            txtCelular.Text = medico.Telefono;
            telefono=medico.Telefono;
        }


        #endregion

        #region MostrarEspecialidades
        void CargarEspecialidad()
        {
            DatoMedico datoMedico = new DatoMedico();

            // Llamar al método CargarEspecialidades
            datoMedico.CargarEspecialidades(cmbEspecialidad);
        }
        #endregion

        #region MostrarHorasIniciales
        void CargarHorasIniciales()
        {
            DatoMedico datoMedico = new DatoMedico();

            // Llamar al método CargarEspecialidades
            datoMedico.CargarHoraInicio(cbHoraInicioAM);
        }
        #endregion

        #region MostrarHorasFinales
        void CargarHorasFinales()
        {
            DatoMedico datoMedico = new DatoMedico();

            // Llamar al método CargarEspecialidades
            datoMedico.CargarHoraFin(cbHoraFinPM);
        }
        #endregion

        #region MOSTRAR CONSULTORIOS Y VENTANAS
        void CargarConsultorio()
        {
            DatoMedico datoMedico = new DatoMedico();

            // Llamar al método CargarConsultorios
            datoMedico.CargarConsultorios(cmbConsultorio);
        }

        private void btnConsultas_Click(object sender, RoutedEventArgs e)
        {
            frmConsultas ventana = new frmConsultas(EnviarRol);
            ventana.Show();
            this.Hide();
        }

        private void btnExamenes_Click(object sender, RoutedEventArgs e)
        {
            frmMenuExamen ventana = new frmMenuExamen(EnviarRol);
            ventana.Show();
            this.Hide();
        }

        private void btnRecetas_Click(object sender, RoutedEventArgs e)
        {
            frmRecetas ventana = new frmRecetas(EnviarRol);
            ventana.Show();
            this.Hide();
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

        #region Cargar Telefonos
        private bool DetenerTelefonos = true; 

        private void CargarTelefonos(int medicoID)
        {
            var telefonos = DatoMedico.ObtenerTelefonosPorMedico(medicoID);

            // Verificar si hay más de un registro
            if (telefonos.Count == 2)
            {
                DetenerTelefonos = false;
            }

            // Actualizar el DataGrid con los teléfonos
            dgTelefonos.ItemsSource = telefonos;
        }


        #endregion
    }
}
