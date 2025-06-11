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
using Usuario.Reports;
using Usuario.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmRegistroRecetas.xaml
    /// </summary>
    public partial class frmRegistroRecetas : Window
    {
        private int EnviarRol;
        public frmRegistroRecetas(int VerificarRol)
        {
            InitializeComponent();
            MostrarRecetas();
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
        int recetaid = 0; //almacenar el id del usuario actual
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

        #region METODO PERSONALIZADO PARA MOSTRAR EXAMENES
        void MostrarRecetas()
        {
            dgRecetas.ItemsSource = DatoReceta.MuestraReceta();
        }
        #endregion

        #region METODO PERSONALIZADO LIMPIAR
        void LimpiarObjetos()
        {
            txtCodigoPaciente.Clear();
            txtCodigoMedico.Clear();
            txtMedicamentos.Clear();
            txtIndicaciones.Clear();
            txtConsulta.Clear();
            dtFechaReceta.SelectedDate = null;
            txtBuscarReceta.Clear();
        }
        #endregion

        #region METODO PERSONALIZADO VALIDACION
        //metodo para validar el formualrio
        bool ValidarFormulario()
        {
            bool estado = true;
            string mensaje = null;

            //txtNombrw
            if (string.IsNullOrEmpty(txtCodigoPaciente.Text))
            {
                estado = false;
                mensaje += "Carnet de Paciente\n";
            }
            if (string.IsNullOrEmpty(txtCodigoMedico.Text))
            {
                estado = false;
                mensaje += "Carnet de Medico\n";
            }
            if (string.IsNullOrEmpty(txtConsulta.Text))
            {
                estado = false;
                mensaje += "Código Consulta\n";
            }
            //txtApellido
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
            //FechaExamen
            if (dtFechaReceta.SelectedDate == null)
            {
                estado = false;
                mensaje += "Fecha Receta\n";
            }
            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos: \n" + mensaje, "Validacion de formularios", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return estado;
        }
        #endregion

        #region BOTON ELIMINAR
        private void btnEliminarReceta_Click(object sender, RoutedEventArgs e)
        {
            //validar si tenemos registros para eliminar
            if (dgRecetas.Items.Count > 0)
            {
                if (MessageBox.Show("¿Desea eliminar el registro #" + recetaid + " ?",
                "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    RecetaModel receta = new RecetaModel();
                    receta.RecetaID = recetaid;

                    //Proceder con la eliminacion
                    if (DatoReceta.EliminarReceta(receta) > 0)
                    {
                        MessageBox.Show("Registro eliminado correctamente",
                "Validacion", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar el formulario
                        LimpiarObjetos();

                        //Recargar el datagrid
                        MostrarRecetas();
                        CargarMedicamentos(0);

                        //Reiniciar variables de estado
                        Agregando = false;
                    }
                }
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
            }
        }
        #endregion

        #region DATAGRID
        private void dgRecetas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecetaModel receta = (RecetaModel)dgRecetas.SelectedItem;
            if (receta == null)
            {
                return;
            }

            //llenar las cajas de texto con lo que traemos del DG
            recetaid = receta.RecetaID;
            txtCodigoPaciente.Text = receta.PacienteID.ToString();
            txtCodigoMedico.Text = receta.MedicoID.ToString();
            txtConsulta.Text = receta.ConsultaID.ToString();
            dtFechaReceta.Text = receta.FechaEmision.ToString();

            MessageBox.Show("Medicamentos seleccionar a la derecha");
            if (dgRecetas.SelectedItem != null)
            {
                RecetaModel recetaSeleccionada = (RecetaModel)dgRecetas.SelectedItem;
                CargarMedicamentos(recetaSeleccionada.RecetaID);
            }
        }
        #endregion

        #region BUSQUEDA
        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscarReceta.Text;

            int id;
            List<RecetaModel> recetasfiltradas;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                recetasfiltradas = DatoReceta.BuscarReceta(id: id);
            }
            else
            {
                recetasfiltradas = DatoReceta.BuscarReceta();
            }

            dgRecetas.ItemsSource = new ObservableCollection<RecetaModel>(recetasfiltradas);
        }

        #endregion

        //Filtrar Medicamentos por receta

        #region Cargar Medicamentos
        
        private void CargarMedicamentos(int RecetaID)
        {
            var medicamentos = DatoReceta.ObtenerMedicamentosPorReceta(RecetaID);

            dgMedicamentos.ItemsSource = medicamentos;
        }

        #endregion

        #region BOTON ENTREGAR
        private void btnEntregarReceta_Click(object sender, RoutedEventArgs e)
        {
            //validar si tenemos registros para eliminar
            if (dgRecetas.Items.Count > 0)
            {
                if (MessageBox.Show("¿Desea entregar el registro #" + recetaid + " ?",
                "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    RecetaModel receta = new RecetaModel();
                    receta.RecetaID = recetaid;

                    //Proceder con la eliminacion
                    if (DatoReceta.CambiarEstadoReceta(receta) > 0)
                    {
                        //Instancia el reportede Crytsal Reports
                        rptRecetas rpt = new rptRecetas();
                        //instanciar el formulario
                        vsReporte visor = new vsReporte();

                        //Configurar la carga del reporte
                        rpt.Load(@"rptRecetas.rpt");

                        //Configurar la carga del reporte
                        rpt.Load(@"Medicamentos.rpt");

                        // Esatblecer un parametro al reporte
                        rpt.SetParameterValue("@RecetaID", recetaid);
                        //Esatblecer un parametro al reporte
                        rpt.SetParameterValue("@RecetaID", recetaid, "Medicamentos.rpt");

                        //Asignar el origen del reporte al visor
                        visor.crvReporte.ViewerCore.ReportSource = rpt;

                        //Mostrar el visor o la ventana con el reporte
                        visor.Show();

                        //MessageBox.Show("Registro entregado correctamente",
                        //"Validacion", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar el formulario
                        LimpiarObjetos();

                        //Recargar el datagrid
                        MostrarRecetas();

                        //Reiniciar variables de estado
                        Agregando = false;
                    }
                }
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

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            frmRecetas ventana = new frmRecetas(EnviarRol);
            ventana.Show();
            this.Hide();
        }

        private void dgMedicamentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecetaModel medicamento = (RecetaModel)dgMedicamentos.SelectedItem;
            if (medicamento == null)
            {
                return;
            }
            txtMedicamentos.Text = medicamento.NombreMedicamento;
            txtIndicaciones.Text = medicamento.Indicaciones;
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