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
using System.Windows.Shapes;
using Usuario.Models;
using Usuario.Services;

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmPrueba.xaml
    /// </summary>
    public partial class frmPrueba : Window
    {
        private int PacienteId;
        public frmPrueba(int pacienteId)
        {
            InitializeComponent();
            PacienteId = pacienteId;

            CargarMedicos(cmbMedico);
            CargarCitas();
        }


        #region Cargar Medicos 
        public static void CargarMedicos(ComboBox cmbMedico)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "EXEC SPCargarComboBox @TipoComboBox = 'Medicos'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cmbMedico.ItemsSource = dt.DefaultView;
                        cmbMedico.DisplayMemberPath = "Medico";
                        cmbMedico.SelectedValuePath = "MedicoID";
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron médicos.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar médicos: " + ex.Message);
                }
            }
        }

        #endregion


        #region Cargar DataGrid 
        private void CargarCitas(int? medicoID = null, DateTime? fechaCita = null)
        {

            List<CitasModel> citasDelPaciente = DatoCitas.CargarCitas(pacienteID: PacienteId, medicoID: medicoID, fechaCita: fechaCita);

            if (citasDelPaciente.Count > 0)
            {
                dgCitas.ItemsSource = citasDelPaciente;
            }
            else
            {
                MessageBox.Show("No hay citas registradas para este paciente.");
            }
        }

        #endregion

        #region Botones 
        private void btnVerAgenda_Click(object sender, RoutedEventArgs e)
        {
            int? medicoID = cmbMedico.SelectedValue as int?;
            DateTime? fechaCita = dpFecha.SelectedDate;

            CargarCitas(medicoID, fechaCita);
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            cmbMedico.SelectedIndex = -1;
            dpFecha.SelectedDate = null;
            CargarCitas();
        }

        #endregion
    }
}
