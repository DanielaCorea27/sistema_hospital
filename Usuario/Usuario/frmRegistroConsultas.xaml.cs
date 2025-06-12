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
    /// Lógica de interacción para frmRegistroConsultas.xaml
    /// </summary>
    public partial class frmRegistroConsultas : Window
    {
        private int EnviarRol = 0;
        public frmRegistroConsultas(int VerificarRol)
        {
            InitializeComponent();
            MostrarConsultas();
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
        int consultaid = 0; //almacenar el id del usuario actual
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
        void MostrarConsultas()
        {
            dgConsultas.ItemsSource = DatoConsulta.MuestraConsultas();
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
            txtMotivo.Clear();
            rdSi.IsChecked = null;
            rdNo.IsChecked = null;
            txtConsultorio.Clear();
            dtFechaConsulta.SelectedDate = null;
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
            if (string.IsNullOrEmpty(txtMotivo.Text))
            {
                estado = false;
                mensaje += "Motivo de Consulta\n";
            }
            //txtEspecialidad
            if (string.IsNullOrEmpty(txtConsultorio.Text))
            {
                estado = false;
                mensaje += "Consultorio\n";
            }
            //FechaExamen
            if (dtFechaConsulta.SelectedDate == null)
            {
                estado = false;
                mensaje += "Fecha Consulta\n";
            }
            //HoraFin
            if (rdSi.IsChecked == null && rdNo.IsChecked == null)
            {
                estado = false;
                mensaje += "Seleccione Agenda de Examen\n";
            }
            if (!estado)
            {
                MessageBox.Show("Debe completar o cumplir estos campos: \n" + mensaje, "Validacion de formularios", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            return estado;
        }
        #endregion

        #region BOTON ELIMINAR
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //validar si tenemos registros para eliminar
            if (dgConsultas.Items.Count > 0)
            {
                if (MessageBox.Show("¿Desea eliminar el registro #" + consultaid + " ?",
                "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ConsultaModel consulta = new ConsultaModel();
                    consulta.ConsultaID = consultaid;

                    //Proceder con la eliminacion
                    if (DatoConsulta.EliminarConsulta(consulta) > 0)
                    {
                        MessageBox.Show("Registro eliminado correctamente",
                "Validacion", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Limpiar el formulario
                        LimpiarObjetos();

                        //Recargar el datagrid
                        MostrarConsultas();

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
        private void dgConsultas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConsultaModel consulta = (ConsultaModel)dgConsultas.SelectedItem;
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
            txtConsultorio.Text = consulta.NombreConsultorio;
            dtFechaConsulta.Text = consulta.FechaConsulta.ToString();
            txtMotivo.Text = consulta.MotivoConsulta;
            if (consulta.Examen == true)
            {
                rdSi.IsChecked = true;
            }
            else
            {
                rdNo.IsChecked = true;
            }
        }


        #endregion

        #region BUSQUEDA
        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filtro = txtBuscar.Text;

            int id;
            List<ConsultaModel> consultasfiltradas;

            // Verificar si el filtro es numérico (para buscar por ID)
            if (int.TryParse(filtro, out id))
            {
                // Buscar por ID
                consultasfiltradas = DatoConsulta.BuscarConsulta(id: id);
            }
            else
            {
                consultasfiltradas = DatoConsulta.BuscarConsulta();
            }

            dgConsultas.ItemsSource = new ObservableCollection<ConsultaModel>(consultasfiltradas);
        }

        #endregion

        #region VENTANAS

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
            frmConsultas ventana = new frmConsultas(EnviarRol);
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
