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
using Usuario.Models;
using Usuario.Services;
using Usuario.Reports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Usuario.ManejarRoles;

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmExamenesRegistrados.xaml
    /// </summary>
    public partial class frmExamenesRegistrados : Window
    {
        private int EnviarRol = 0;
        public frmExamenesRegistrados(int VerificarRol)
        {
            InitializeComponent();
            MostrarExamenesRegistrados();
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

        #region METODO PERSONALIZADO PARA MOSTRAR EXAMENES
        void MostrarExamenesRegistrados()
        {
            dgExamenes.ItemsSource = DatoExamen.MuestraExamenesRegistrados();
        }
        #endregion

        #region METODO PERSONALIZADO LIMPIAR
        void LimpiarObjetos()
        {
            txtCodigoExamen.Clear();
            txtCodigoPaciente.Clear();
            txtCodigoMedico.Clear();
            txtResultados.Clear();
            txtEstadoExamen.Clear();
            cmbEspecialidad.Clear();
            cmbTipoExamen.Clear();
            dtFechaExamen.SelectedDate = null;
            dtFechaResultados.SelectedDate = null;
            txtBuscar.Clear();
        }
        #endregion

        #region METODO PERSONALIZADO VALIDACION
        //metodo para validar el formualrio
        bool ValidarFormulario()
        {
            bool estado = true;
            string mensaje = null;

            //txtNombrw
            if (string.IsNullOrEmpty(txtCodigoExamen.Text))
            {
                estado = false;
                mensaje += "Codigo de Examen\n";
            }
            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos: \n" + mensaje, "Validacion de formularios", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return estado;
        }
        #endregion

        #region BOTON CAMBIAR ESTADO
        private void btnEntregar_Click(object sender, RoutedEventArgs e)
        {
            //validar si tenemos registros para eliminar
            if (dgExamenes.Items.Count > 0)
            {
                if (MessageBox.Show("¿Desea entregar el registro #" + examenid + " ?",
                "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExamenModel examen = new ExamenModel();
                    examen.ExamenId = examenid;

                    //Proceder con la eliminacion
                    if (DatoExamen.CambiarEstado(examen) > 0)
                    {
                        //Instancia el reportede Crytsal Reports
                        rptExamenes rpt = new rptExamenes();
                        //instanciar el formulario
                        vsReporte visor = new vsReporte();

                        //Configurar la carga del reporte
                        rpt.Load(@"rptExamenes.rpt");

                        //Esatblecer un parametro al reporte
                        rpt.SetParameterValue("@ExamenID", examenid);

                        //Asignar el origen del reporte al visor
                        visor.crvReporte.ViewerCore.ReportSource = rpt;

                        //Mostrar el visor o la ventana con el reporte
                        visor.Show();

                        //MessageBox.Show("Registro entregado correctamente",
                        //"Validacion", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar el formulario
                        LimpiarObjetos();

                        //Recargar el datagrid
                        MostrarExamenesRegistrados();

                        //Reiniciar variables de estado
                        Agregando = false;
                    }
                }
            }
        }
        #endregion

        #region BOTON ELIMINAR
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //validar si tenemos registros para eliminar
            if (dgExamenes.Items.Count > 0)
            {
                if (MessageBox.Show("¿Desea eliminar el registro #" + examenid + " ?",
                "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExamenModel examen = new ExamenModel();
                    examen.ExamenId = examenid;

                    //Proceder con la eliminacion
                    if (DatoExamen.EliminarExamen(examen) > 0)
                    {
                        MessageBox.Show("Registro eliminado correctamente",
                "Validacion", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar el formulario
                        LimpiarObjetos();

                        //Recargar el datagrid
                        MostrarExamenesRegistrados();

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
        private void dgExamenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExamenModel examen = (ExamenModel)dgExamenes.SelectedItem;
            if (examen == null)
            {
                return;
            }

            //llenar las cajas de texto con lo que traemos del DG
            examenid = examen.ExamenId;
            txtCodigoExamen.Text = examen.ExamenId.ToString();
            txtCodigoPaciente.Text = examen.PacienteID.ToString();
            txtCodigoMedico.Text = examen.MedicoID.ToString();
            cmbEspecialidad.Text = examen.Especialidad;
            cmbTipoExamen.Text = examen.TipoExamen;
            dtFechaExamen.Text = examen.FechaExamen.ToString();
            dtFechaResultados.Text = examen.FechaResultado.ToString();
            txtResultados.Text = examen.Resultado;
            txtEstadoExamen.Text = examen.EstadoExamenNombre;
        }

        #endregion

        #region BUSQUEDA
        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscar.Text;

            int id;
            List<ExamenModel> examenesfiltrados;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                examenesfiltrados = DatoExamen.BuscarExamenRegistrado(id: id);
            }
            else
            {
                examenesfiltrados = DatoExamen.BuscarExamenRegistrado();
            }

            dgExamenes.ItemsSource = new ObservableCollection<ExamenModel>(examenesfiltrados);
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
