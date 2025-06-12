using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmHistorialPacientes.xaml
    /// </summary>
    public partial class frmHistorialPacientes : Window
    {
        private int EnviarRol;

        public frmHistorialPacientes(int VerificarRol)
        {
            InitializeComponent();
            MostrarHistorial();
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
        int historialID = 0; //almacenar el id del usuario actual
        string email = "";
        string telefono = "";
        #endregion

        #region METODO PERSONALIZADO PARA MOSTRAR PACIENTES
        void MostrarHistorial()
        {
            HistorialPacientesModel h = new HistorialPacientesModel();
            dgHistorial.ItemsSource = h.MostrarHistorial();

        }
        #endregion

        #region METODO PERSONALIZADO HABILITAR OBJETOS
        //Metodo para habilitar y deshabilitar objetos del formulario
        void HabilitarObjetos(bool accion)
        {
            txtPacienteID.IsEnabled = accion;
            txtMotivoConsulta.IsEnabled = accion;
            txtPadecimientos.IsEnabled = accion;
            txtTraumatismos.IsEnabled = accion;
            txtCirugiasPrevias.IsEnabled = accion;
            txtMedicacionActual.IsEnabled = accion;
            txtAntecedentesFamiliares.IsEnabled = accion;
            txtDiscapacidad.IsEnabled = accion;
            txtAlergia.IsEnabled = accion;
            txtEnfermedadCronica.IsEnabled = accion;
            txtObservaciones.IsEnabled = accion;

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

            txtPacienteID.Clear();
            txtMotivoConsulta.Clear();
            txtPadecimientos.Clear();
            txtTraumatismos.Clear();
            txtCirugiasPrevias.Clear();
            txtMedicacionActual.Clear();
            txtAntecedentesFamiliares.Clear();
            txtDiscapacidad.Clear();
            txtAlergia.Clear();
            txtEnfermedadCronica.Clear();
            txtObservaciones.Clear();
            txtBuscar.Clear();
        }
        #endregion

        #region METODO PERSONALIZADO CONTROL DEL TOOLBAR
        //Metodo para controlar la ToolBar
        void controlToolBar()
        {
            //Si el DataGrid no tiene registros
            if (dgHistorial.Items.Count == 0)
            {
                btnAgregar.Visibility = Visibility.Visible;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnEditar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Si el DataGrid tienepor lo menos un  registro
                btnAgregar.Visibility = Visibility.Visible;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnEditar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Collapsed;
            }
            if (Agregando || Editando || (!string.IsNullOrEmpty(txtPacienteID.Text)))
            {
                // Si estoy AGREGANDO O EDITANDO UN REGISTRO
                btnGuardar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Visible;
                //btnNewUser.Visibility = Visibility.Collapsed;
                btnEditar.Visibility = Visibility.Collapsed;
                btnAgregar.Visibility = Visibility.Visible;
                if ((!string.IsNullOrEmpty(txtPacienteID.Text)))
                {
                    btnCancelar.Visibility = Visibility.Collapsed;
                }
                if (Editando)
                {
                    btnCancelar.Visibility = Visibility.Visible;

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
            if (string.IsNullOrEmpty(txtPacienteID.Text))
            {
                estado = false;
                mensaje += "ID del paciente \n";
            }
            //txtApellido
            if (string.IsNullOrEmpty(txtMotivoConsulta.Text))
            {
                estado = false;
                mensaje += "Motivo de la consulta \n";
            }
            //txtIdentificacion
            if (string.IsNullOrEmpty(txtPadecimientos.Text))
            {
                estado = false;
                mensaje += "Padecimientos \n";
            }
            //dpFechaNacimiento
            if (string.IsNullOrEmpty(txtTraumatismos.Text))
            {
                estado = false;
                mensaje += "Traumatismos \n";
            }
            //txtEmail
            if (string.IsNullOrEmpty(txtCirugiasPrevias.Text))
            {
                estado = false;
                mensaje += "Cirugias previas\n";
            }
            //txtRecidencia
            if (string.IsNullOrEmpty(txtMedicacionActual.Text))
            {
                estado = false;
                mensaje = "Medicamento actual \n";
            }
            //txtRecidencia
            if (string.IsNullOrEmpty(txtAntecedentesFamiliares.Text))
            {
                estado = false;
                mensaje = "Antecedentes Familiares\n";
            }
            //txtRecidencia
            if (string.IsNullOrEmpty(txtDiscapacidad.Text))
            {
                estado = false;
                mensaje = "Discapacidades \n";
            }
            //txtRecidencia
            if (string.IsNullOrEmpty(txtAlergia.Text))
            {
                estado = false;
                mensaje = "Alergias\n";
            }
            //txtRecidencia
            if (string.IsNullOrEmpty(txtEnfermedadCronica.Text))
            {
                estado = false;
                mensaje = "Enfermedades cronicas \n";
            }
            //txtRecidencia
            if (string.IsNullOrEmpty(txtObservaciones.Text))
            {
                estado = false;
                mensaje = "Observaciones \n";
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



                HistorialPacientesModel historial = new HistorialPacientesModel();
                historial.PacienteID = Convert.ToInt32(txtPacienteID.Text);
                historial.MotivoConsulta = txtMotivoConsulta.Text;
                historial.Padecimientos = txtPadecimientos.Text;
                historial.Traumatismos = txtTraumatismos.Text;
                historial.CirugiasPrevias = txtCirugiasPrevias.Text;
                historial.MedicacionActual = txtMedicacionActual.Text;
                historial.AntecedentesFamiliares = txtAntecedentesFamiliares.Text;
                historial.Discapacidad = txtDiscapacidad.Text;
                historial.Alergia = txtAlergia.Text;
                historial.EnfermedadCronica = txtEnfermedadCronica.Text;
                historial.Observaciones = txtObservaciones.Text;

                // Evaluar si está agregando
                if (Agregando)
                {
                    AgregandoRepeat = true;
                    // Llamar al método de inserción
                    historialID = historial.InsertarHistorial(historial);
                    mensaje = "Registro almacenado correctamente";
                }
                else
                {
                    EditandoRepeat = true;
                    // Llamar al estado para editar
                    historialID = historial.ModificarHistorial(historial);
                    mensaje = "Registro modificado exitosamente";
                }
                // Evaluar si se ingresó el registro
                if (historialID > 0)
                {
                    // Mensaje de confirmación
                    MessageBox.Show(mensaje, "Validación del formulario", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Limpiar cajas de texto
                    LimpiarObjetos();

                    // Recargar el DataGrid
                    MostrarHistorial();

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
                    if (historialID == 0)
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
                MostrarHistorial();
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
            txtPacienteID.Focus();
        }


        #endregion

        #region METODO DEL DATAGRID
        private void dgHistorial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HistorialPacientesModel h = (HistorialPacientesModel)dgHistorial.SelectedItem;
            if (h == null)
            {
                return;
            }

            //llenar las cajas de texto con lo que traemos del DG
            historialID = h.ID;
            txtPacienteID.Text = h.PacienteID.ToString();
            txtMotivoConsulta.Text = h.MotivoConsulta;
            txtPadecimientos.Text = h.Padecimientos;
            txtTraumatismos.Text = h.Traumatismos;
            txtCirugiasPrevias.Text = h.CirugiasPrevias;
            txtMedicacionActual.Text = h.MedicacionActual;
            txtAntecedentesFamiliares.Text = h.AntecedentesFamiliares;
            txtDiscapacidad.Text = h.Discapacidad;
            txtAlergia.Text = h.Alergia;
            txtEnfermedadCronica.Text = h.EnfermedadCronica;
            txtObservaciones.Text = h.Observaciones;//EDITADO
            MessageBox.Show("Ahora para editar seleccione el numero a editar");
            if (dgHistorial.SelectedItem != null)
            {
                HistorialPacientesModel seleccionado = (HistorialPacientesModel)dgHistorial.SelectedItem;
                btnAgregar.Visibility = Visibility.Collapsed;

            }
        }


        #endregion

        #region WINDOW
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            HabilitarObjetos(false);
            //Habilitar botones
            controlToolBar();
        }


        #endregion

        #region VENTANAS
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frmInicio inicio = new frmInicio(EnviarRol);
            inicio.Show();
            this.Close();
        }

        private void btnPacientes_Click(object sender, RoutedEventArgs e)
        {
            frmPacientes ventana = new frmPacientes(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnMedicos_Click(object sender, RoutedEventArgs e)
        {
            frmMedicos ventana = new frmMedicos(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            frmUsuarios ventana = new frmUsuarios(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            HistorialPacientesModel model = new HistorialPacientesModel();
            string filtro = txtBuscar.Text;

            int id;
            List<HistorialPacientesModel> hf;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                hf = model.BuscarHistorial(id: id);
            }
            else
            {
                hf = model.BuscarHistorial();
            }

            dgHistorial.ItemsSource = new ObservableCollection<HistorialPacientesModel>(hf);
        }

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
        #endregion

        #region METODO BOTON AGREGAR
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //Bloquear objetos
            HabilitarObjetos(true);

            //Establecer valor de eitando o agregando en false
            Agregando = true;
            Editando = false;

            //Configurar toolbar
            controlToolBar();

            //Enviar el enfoque al txt de Nombre
            txtPacienteID.Focus();
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
