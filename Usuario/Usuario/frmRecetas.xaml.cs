using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
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
    /// Lógica de interacción para frmRecetas.xaml
    /// </summary>
    public partial class frmRecetas : Window
    {
        private int EnviarRol;
        public frmRecetas(int VerificarRol)
        {
            InitializeComponent();
            MostrarConsultas();
            MostrarMedicamentos();
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
        bool AgregandoMed = false;

        //Variable para almacenar el id del usuario
        int recetaid = 0; //almacenar el id del usuario actual
        int consultaid = 0; //almacenar el id del usuario actual
        int medicamentoid = 0; //almacenar el id del usuario actual
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

        #region METODO PERSONALIZADO PARA MOSTRAR Consultas
        void MostrarConsultas()
        {
            dgConsultas.ItemsSource = DatoReceta.MuestraConsultasReceta();
        }
        void MostrarMedicamentos()
        {
            dgMedicamentos.ItemsSource = DatoReceta.MuestraMedicamentos();
        }
        #endregion

        #region METODO PERSONALIZADO HABILITAR OBJETOS
        //Metodo para habilitar y deshabilitar objetos del formulario
        void HabilitarObjetos(bool accion)
        {
            dtFechaReceta.IsEnabled = accion;
            
            dgConsultas.IsEnabled = accion;

            //minimizar codigo
            if (accion == true)
            {
                txtBuscarConsulta.IsEnabled = false;
                txtBuscarMedicamento.IsEnabled = false;

                dgMedicamentos.IsEnabled = false;
            }
            else
            {
                txtBuscarConsulta.IsEnabled = true;
                txtBuscarMedicamento.IsEnabled = true;

                dgMedicamentos.IsEnabled = true;
            }
        }
        #endregion

        #region METODO PERSONALIZADO LIMPIAR
        void LimpiarObjetos()
        {
            txtCodigoPaciente.Clear();
            txtNombrePaciente.Clear();
            txtApellidoPaciente.Clear();
            txtCodigoMedico.Clear();
            txtNombreMedico.Clear();
            txtApellidoMedico.Clear();
            txtConsulta.Clear();
            txtMedicamentos.Clear();
            txtIndicaciones.Clear();
            dtFechaReceta.SelectedDate = null;
            txtBuscarConsulta.Clear();
            txtBuscarMedicamento.Clear();
        }
        #endregion

        #region METODO PERSONALIZADO CONTROL DEL TOOLBAR
        //Metodo para controlar la ToolBar
        void controlToolBar()
        {
            //Si el DataGrid no tiene registros
            if (dgConsultas.Items.Count == 0)
            {
                btnNuevaReceta.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnCancelar.Visibility = Visibility.Collapsed;

                btnAgregarMedicamento.Visibility = Visibility.Collapsed;
                btnGuardarMed.Visibility = Visibility.Collapsed;

                txtMedicamentos.Visibility = Visibility.Collapsed;
                txtIndicaciones.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Si el DataGrid tienepor lo menos un  registro
                btnNuevaReceta.Visibility = Visibility.Visible;

                btnGuardar.Visibility = Visibility.Collapsed;
                btnCancelar.Visibility = Visibility.Collapsed;

                btnAgregarMedicamento.Visibility = Visibility.Collapsed;
                btnGuardarMed.Visibility = Visibility.Collapsed;
                btnVerRecetas.Visibility = Visibility.Visible;

                txtMedicamentos.Visibility = Visibility.Collapsed;
                txtIndicaciones.Visibility = Visibility.Collapsed;
            }
            if (Agregando)
            {
                // Si estoy AGREGANDO UN REGISTRO
                btnNuevaReceta.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Visible;

                btnAgregarMedicamento.Visibility = Visibility.Collapsed;
                btnGuardarMed.Visibility = Visibility.Collapsed;
                btnVerRecetas.Visibility = Visibility.Collapsed;

                txtMedicamentos.Visibility = Visibility.Collapsed;
                txtIndicaciones.Visibility = Visibility.Collapsed;
            }
            if (Agregando== false && AgregandoMed==true)
            {
                // Si estoy AGREGANDO UN REGISTRO
                btnNuevaReceta.Visibility = Visibility.Collapsed;
                btnGuardar.Visibility = Visibility.Collapsed;
                btnCancelar.Visibility = Visibility.Collapsed;

                btnAgregarMedicamento.Visibility = Visibility.Visible;
                btnGuardarMed.Visibility = Visibility.Collapsed;
                btnVerRecetas.Visibility = Visibility.Collapsed;

                txtMedicamentos.Visibility = Visibility.Visible;
                txtIndicaciones.Visibility = Visibility.Visible;
                txtIndicaciones.IsEnabled = true;
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
            //txtNombrw
            if (string.IsNullOrEmpty(txtNombreMedico.Text))
            {
                estado = false;
                mensaje += "Nombre de Medico\n";
            }
            //txtApellido
            if (string.IsNullOrEmpty(txtApellidoMedico.Text))
            {
                estado = false;
                mensaje += "Apellido de Medico\n";
            }
            //txtConsulta
            if (string.IsNullOrEmpty(txtConsulta.Text))
            {
                estado = false;
                mensaje += "Codigo de Consulta\n";
            }
            
            if (dtFechaReceta.SelectedDate == null)
            {
                estado = false;
                mensaje += "Fecha Examen\n";
            }
            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos: \n" + mensaje, "Validacion de formularios", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return estado;
        }
        #endregion

        #region METODO PERSONALIZADO VALIDACION
        //metodo para validar el formualrio
        bool ValidarFormularioMed()
        {
            bool estado = true;
            string mensaje = null;

            
            //txtConsulta
            if (string.IsNullOrEmpty(txtMedicamentos.Text))
            {
                estado = false;
                mensaje += "Medicamentos\n";
            }
            //txtConsulta
            if (string.IsNullOrEmpty(txtIndicaciones.Text))
            {
                estado = false;
                mensaje += "Indicaciones\n";
            }
            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos: \n" + mensaje, "Validacion de formularios", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return estado;
        }
        #endregion

        #region WINDOW
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Deshabilitar objetos
            HabilitarObjetos(false);
            //Habilitar botones
            controlToolBar();
        }
        #endregion

        #region DATA CONSULTAS
        private void dgConsultas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecetaModel consulta = (RecetaModel)dgConsultas.SelectedItem;
            if (consulta == null)
            {
                return;
            }

            //llenar las cajas de texto con lo que traemos del DG
            consultaid = consulta.ConsultaID;
            txtCodigoPaciente.Text = consulta.PacienteID.ToString();
            txtNombrePaciente.Text = consulta.NombrePaciente;
            txtApellidoPaciente.Text = consulta.ApellidoPaciente;
            txtCodigoMedico.Text = consulta.MedicoID.ToString();
            txtNombreMedico.Text = consulta.NombreMedico;
            txtApellidoMedico.Text = consulta.ApellidoMedico;
            txtConsulta.Text = consulta.ConsultaID.ToString();
        }
        #endregion

        #region DATA MEDICAMENTOS
        private void dgMedicamentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecetaModel medicamento = (RecetaModel)dgMedicamentos.SelectedItem;
            if (medicamento == null)
            {
                return;
            }

            //llenar las cajas de texto con lo que traemos del DG
            medicamentoid = medicamento.MedicamentosID;
            txtMedicamentos.Text = medicamento.NombreMedicamento;
        }
        #endregion

        #region BUSQUEDA DE CONSULTAS
        private void txtBuscarConsulta_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarConsulta.Text;

            int id;
            List<RecetaModel> consultasfiltradas;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                consultasfiltradas = DatoReceta.BuscarConsultasReceta(id: id);
            }
            else
            {
                consultasfiltradas = DatoReceta.BuscarConsultasReceta();
            }

            dgConsultas.ItemsSource = new ObservableCollection<RecetaModel>(consultasfiltradas);
        }
        #endregion

        #region BUSQUEDA DE RECETAS
        private void txtBuscarMedicamento_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarMedicamento.Text;

            int id;
            List<RecetaModel> medicamentosfiltradas;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                medicamentosfiltradas = DatoReceta.BuscarMedicamentos(id: id);
            }
            else
            {
                medicamentosfiltradas = DatoReceta.BuscarMedicamentos();
            }

            dgMedicamentos.ItemsSource = new ObservableCollection<RecetaModel>(medicamentosfiltradas);
        }
        #endregion

        #region BOTON CANCELAR
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            // pedir confirmacion
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

        #region BOTON NUEVO
        private void btnNuevaReceta_Click(object sender, RoutedEventArgs e)
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

        #region BOTON VER RECETAS
        private void btnVerRecetas_Click(object sender, RoutedEventArgs e)
        {
            frmRegistroRecetas ventana = new frmRegistroRecetas(EnviarRol);
            ventana.Show();
            this.Hide();
        }
        #endregion

        #region BOTON GUARDAR
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Variable para mensajes de estado
            string mensaje = null;

            if (ValidarFormulario())
            {

                RecetaModel receta = new RecetaModel();
                
                //Combinar Fecha y Hora
                DateTime fechaSeleccionada = dtFechaReceta.SelectedDate ?? DateTime.Now.Date;
                DateTime fechaConHoraActual = fechaSeleccionada.Date.Add(DateTime.Now.TimeOfDay);

                // Formatear la date
                receta.FechaEmision = fechaConHoraActual;
                receta.PacienteID = Convert.ToInt32(txtCodigoPaciente.Text);
                receta.MedicoID = Convert.ToInt32(txtCodigoMedico.Text);
                receta.ConsultaID = Convert.ToInt32(txtConsulta.Text);
                consultaid = Convert.ToInt32(txtConsulta.Text);
                // Evaluar si está agregando
                if (Agregando)
                {
                    //Llamar al metodo de insercion
                    recetaid = DatoReceta.InsertaReceta(receta);
                    mensaje = "¿Desea Agregar Medicamentos?";
                }
                // Evaluar si se ingresó el registro
                if (recetaid > 0)
                {
                    // Mensaje de confirmación
                    if (MessageBox.Show(mensaje, "Validación del formulario", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Agregando = false;
                        AgregandoMed = true;
                    }
                    else
                    {
                        RecetaModel consulta = new RecetaModel();
                        consulta.ConsultaID = consultaid;
                        DatoReceta.CambiarDisponibilidad(consulta);
                    }
                   // Limpiar cajas de texto
                   LimpiarObjetos();

                    // Recargar el DataGrid
                    MostrarConsultas();

                    // Reiniciar las variables de estado
                    Agregando = false;

                    // Deshabilitar las cajas de texto
                    HabilitarObjetos(false);

                    // Manejar los botones
                    controlToolBar();
                }

                //actualizar mi datagrid
                MostrarConsultas();
                MostrarMedicamentos();
                controlToolBar();
                HabilitarObjetos(false);
                //variables de estado
                Agregando = false;
            }
        }
        #endregion

        #region BOTNO GUARDAR MEDICAMENTOS
        private void btnAgregarMedicamento_Click(object sender, RoutedEventArgs e)
        {
            // Variable para mensajes de estado
            string mensaje = null;

            if (ValidarFormularioMed())
            {

                RecetaModel medicamento = new RecetaModel();

                medicamento.RecetaID = recetaid;
                medicamento.MedicamentosID = medicamentoid;
                medicamento.Indicaciones = txtIndicaciones.Text;

                // Evaluar si está agregando
                if (AgregandoMed)
                {
                    //Llamar al metodo de insercion
                    medicamentoid = DatoReceta.InsertaMedicamento(medicamento);
                    mensaje = "¿Desea Agregar Más Medicamentos?";
                }
                // Evaluar si se ingresó el registro
                if (medicamentoid > 0)
                {
                    // Mensaje de confirmación
                    if (MessageBox.Show(mensaje, "Validación del formulario", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        LimpiarObjetos();
                        Agregando = false;
                        AgregandoMed = true;
                    }
                    else
                    {
                        RecetaModel consulta = new RecetaModel();
                        consulta.ConsultaID = consultaid;
                        DatoReceta.CambiarDisponibilidad(consulta);

                        // Limpiar cajas de texto
                        LimpiarObjetos();

                        // Recargar el DataGrid
                        MostrarConsultas();
                        MostrarMedicamentos();

                        // Reiniciar las variables de estado
                        Agregando = false;
                        AgregandoMed = false;

                        // Deshabilitar las cajas de texto
                        HabilitarObjetos(false);

                        // Manejar los botones
                        controlToolBar();
                    }
                }

                //actualizar mi datagrid
                MostrarConsultas();
                MostrarMedicamentos();
                controlToolBar();
                HabilitarObjetos(false);
                //variables de estado
                Agregando = false;
            }
        }




        #endregion

        private void btnRecetas_Click(object sender, RoutedEventArgs e)
        {
            frmRecetas ventana = new frmRecetas(EnviarRol);
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

        private void btnCitas_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmCitas ventana = new frmCitas(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnHistorialesMedicos_Click(object sender, RoutedEventArgs e)
        {
            frmHistorialPacientes ventana = new frmHistorialPacientes(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnPacientes_Click(object sender, RoutedEventArgs e)
        {
            frmPacientes ventana = new frmPacientes(EnviarRol);
            ventana.Show();
            this.Close();
        }
    }
}
