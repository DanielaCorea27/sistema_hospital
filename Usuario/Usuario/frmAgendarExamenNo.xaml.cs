using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Drawing;
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
    /// Lógica de interacción para frmAgendarExamenNo.xaml
    /// </summary>
    public partial class frmAgendarExamenNo : Window
    {
        private int EnviarRol = 0;
        public frmAgendarExamenNo(int VerificarRol)
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
        bool Agregando = false;

        //Variable para almacenar el id del usuario
        int examenid = 0; //almacenar el id del usuario actual
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

        #region METODO PERSONALIZADO PARA MOSTRAR PACIENTES
        void MostrarPacientes()
        {
            dgPacientes.ItemsSource = DatoExamen.MostrarPacientes();
        }
        #endregion

        #region METODO PERSONALIZADO HABILITAR OBJETOS
        //Metodo para habilitar y deshabilitar objetos del formulario
        void HabilitarObjetos(bool accion)
        {
            cmbEspecialidad.IsEnabled = accion;
            cmbTipoExamen.IsEnabled = accion;
            dtFechaExamen.IsEnabled = accion;
            dgPacientes.IsEnabled = accion;

            //minimizar codigo
            if (accion == true)
            {
                txtBuscar.IsEnabled = false;
            }
            else
            {
                txtBuscar.IsEnabled = true;
            }
        }
        #endregion

        #region METODO PERSONALIZADO LIMPIAR
        void LimpiarObjetos()
        {
            txtCodigoPaciente.Clear();
            txtNombrePaciente.Clear();
            txtApellidoPaciente.Clear();
            cmbEspecialidad.SelectedItem = null;
            cmbTipoExamen.SelectedItem = null;
            dtFechaExamen.SelectedDate = null;
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
                btnNewUser.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnCancelar.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Si el DataGrid tienepor lo menos un  registro
                btnNewUser.Visibility = Visibility.Visible;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnCancelar.Visibility = Visibility.Collapsed;
            }
            if (Agregando)
            {
                // Si estoy AGREGANDO UN REGISTRO
                btnNewUser.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Visible;
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
            if (string.IsNullOrEmpty(txtNombrePaciente.Text))
            {
                estado = false;
                mensaje += "Nombre de Paciente\n";
            }
            //txtApellido
            if (string.IsNullOrEmpty(txtApellidoPaciente.Text))
            {
                estado = false;
                mensaje += "Apellido de Paciente\n";
            }
            //txtEspecialidad
            if (cmbEspecialidad.SelectedItem == null)
            {
                estado = false;
                mensaje += "Especialidad\n";
            }
            //FechaExamen
            if (dtFechaExamen.SelectedDate == null)
            {
                estado = false;
                mensaje += "Fecha Examen\n";
            }
            //HoraFin
            if (cmbTipoExamen.SelectedItem == null)
            {
                estado = false;
                mensaje += "Tipo Examen\n";
            }
            //Consultorios
            if (cmbEspecialidad.SelectedItem == null)
            {
                estado = false;
                mensaje += "Especialidad\n";
            }
            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos: \n" + mensaje, "Validacion de formularios", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return estado;
        }
        #endregion

        #region MostrarEspecialidades
        void CargarEspecialidad()
        {
            DatoExamen datoExamen = new DatoExamen();

            // Llamar al método CargarEspecialidades
            datoExamen.CargarEspecialidades(cmbEspecialidad);
        }
        #endregion

        #region MostrarEspecialidades
        void CargarTipoExamenes()
        {
            DatoExamen datoExamen = new DatoExamen();

            // Llamar al método CargarExamenes
            datoExamen.CargarTipoExamen(cmbTipoExamen);
        }
        #endregion

        #region BOTON AGREGAR
        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            //pedir confirmacion
            if (MessageBox.Show("Por favor seleccione un registro",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                //Habilitar objetos
                HabilitarObjetos(true);

                //Limpiar objetos
                LimpiarObjetos();

                //Extablecer estado de AGREGANDO
                Agregando = true;

                //Configurar toolbar
                controlToolBar();
            }
        }
        #endregion

        #region BOTON GUARDAR
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Variable para mensajes de estado
            string mensaje = null;

            if (ValidarFormulario())
            {

                ExamenModel examen = new ExamenModel();
                examen.PacienteID = Convert.ToInt32(txtCodigoPaciente.Text);
                examen.EspecialidadID = Convert.ToInt32(cmbEspecialidad.SelectedValue);
                examen.TipoExamenID = Convert.ToInt32(cmbTipoExamen.SelectedValue);

                //Combinar Fecha y Hora
                DateTime fechaSeleccionada = dtFechaExamen.SelectedDate ?? DateTime.Now.Date;
                DateTime fechaConHoraActual = fechaSeleccionada.Date.Add(DateTime.Now.TimeOfDay);

                // Formatear la date
                examen.FechaExamen = fechaConHoraActual;

                // Evaluar si está agregando
                if (Agregando)
                {
                    //Llamar al metodo de insercion
                    examenid = DatoExamen.InsertaExamen(examen);
                    mensaje = "Registro almacenado correctamente";
                }
                // Evaluar si se ingresó el registro
                if (examenid > 0)
                {
                    // Mensaje de confirmación
                    MessageBox.Show(mensaje, "Validación del formulario", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Limpiar cajas de texto
                    LimpiarObjetos();

                    // Recargar el DataGrid
                    MostrarPacientes();

                    // Reiniciar las variables de estado
                    Agregando = false;

                    // Deshabilitar las cajas de texto
                    HabilitarObjetos(false);

                    // Manejar los botones
                    controlToolBar();
                }

                //actualizar mi datagrid
                MostrarPacientes();
                controlToolBar();
                HabilitarObjetos(false);
                //variables de estado
                Agregando = false;
            }
        }
        #endregion

        #region BOTON CANCELAR
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //pedir confirmacion
            if (MessageBox.Show("Desea cancelar la operación?",
                "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //Limpiar los objetos
                LimpiarObjetos();

                //Bloquear objetos
                HabilitarObjetos(false);

                //Establecer valor de editando o agregando en false
                Agregando = false;

                //Configurar el ToolBar
                controlToolBar();
            }
        }
        #endregion

        #region WINDOW
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Deshabilitar objetos
            HabilitarObjetos(false);
            //Habilitar botones
            controlToolBar();
            CargarEspecialidad();
            CargarTipoExamenes();
        }

        #endregion

        #region
        private void dgPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExamenModel examen = (ExamenModel)dgPacientes.SelectedItem;
            if (examen == null)
            {
                return;
            }

            //llenar las cajas de texto con lo que traemos del DG
            examenid = examen.ExamenId;
            txtCodigoPaciente.Text = examen.PacienteID.ToString();
            txtNombrePaciente.Text = examen.NombrePaciente;
            txtApellidoPaciente.Text = examen.ApellidoPaciente;
        }
        #endregion

        #region BUSQUEDA
        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscar.Text;

            int id;
            List<ExamenModel> pacientesFiltrados;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                pacientesFiltrados = DatoExamen.BuscarPacientes(id: id);
            }
            else
            {
                pacientesFiltrados = DatoExamen.BuscarPacientes();
            }

            dgPacientes.ItemsSource = new ObservableCollection<ExamenModel>(pacientesFiltrados);
        }
        #endregion

        #region VENTANAS
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            frmMenuExamen ventana = new frmMenuExamen(EnviarRol);
            ventana.Show();
            this.Hide();
        }
        

        private void btnMedicos_Click(object sender, RoutedEventArgs e)
        {
            frmMedicos ventana = new frmMedicos(EnviarRol);
            ventana.Show();
            this.Hide();
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

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            frmUsuarios ventana = new frmUsuarios(EnviarRol);
            ventana.Show();
            this.Hide();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frmInicio ventana = new frmInicio(EnviarRol);
            ventana.Show();
            this.Hide();
        }
        #endregion

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
    }
}
