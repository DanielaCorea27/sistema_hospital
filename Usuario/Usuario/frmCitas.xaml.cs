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
using System.Globalization;
using Usuario.ManejarRoles;

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmCitas.xaml
    /// </summary>
    public partial class frmCitas : Window
    {
        private int EnviarRol;
        public frmCitas(int VerificarRol)
        {
            InitializeComponent();
            CargarDatos();
            EnviarRol = VerificarRol;
            FiltrarRoles(VerificarRol);
        }



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

        #region Metodos extras
        private void Limpiar()
        {
            // Limpiar los ComboBox
            cmbMedico.SelectedIndex = -1;
            cmbPaciente.SelectedIndex = -1;
            cmbEspecialidad.SelectedIndex = -1;
            cmbConsultorio.SelectedIndex = -1;
            cmbHora.SelectedIndex = -1;
            cmbDuracion.SelectedIndex = -1;
            cmbEstadoCita.SelectedIndex = -1;

            // Limpiar los TextBox
            txtMotivo.Clear();
            txtComentarios.Clear();

            // Limpiar el DatePicker
            dpFecha.SelectedDate = null;
        }

        private bool Validar(CitasModel cita)
        {
            if (cita.FechaCita == DateTime.MinValue)
            {
                MessageBox.Show("La fecha de la cita es obligatoria.", "Error de Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cita.Hora))
            {
                MessageBox.Show("La hora de la cita es obligatoria.", "Error de Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cita.Duracion <= 0)
            {
                MessageBox.Show("La duracion de la cita es obligatoria.", "Error de Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(cita.MotivoCita))
            {
                MessageBox.Show("El motivo de la cita es obligatorio.", "Error de Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cita.PacienteID <= 0)
            {
                MessageBox.Show("Selecciona un paciente.", "Error de Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cita.MedicoID <= 0)
            {
                MessageBox.Show("Selecciona un medico.", "Error de Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cita.EspecialidadID <= 0)
            {
                MessageBox.Show("Selecciona una especialidad.", "Error de Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cita.ConsultorioID <= 0)
            {
                MessageBox.Show("Selecciona un consultorio.", "Error de Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true; //Todos los campos son validos
        }


        private void CargarDatos()
        {
            CargarMedicos(cmbMedico);
            CargarPacientes(cmbPaciente);
            CargarConsultorios(cmbConsultorio);
            CargarEstadoCitas(cmbEstadoCita);
            CargarCitas();
        }
        #endregion

        #region Botones de Cita
        private void btnVisualizar_Click(object sender, RoutedEventArgs e)
        {
            int? medicoID = cmbMedico.SelectedValue as int?;
            DateTime? fechaSeleccionada = dpFecha.SelectedDate;
            int? pacienteID = cmbPaciente.SelectedValue as int?;

            dgCitas.ItemsSource = null;

            List<CitasModel> citas = DatoCitas.CargarCitas(medicoID, fechaSeleccionada, pacienteID);

            if (citas.Count > 0)
            {
                dgCitas.ItemsSource = citas;
            }
            else
            {
                MessageBox.Show("No hay citas agendadas con los filtros seleccionados.");
            }
        }

        private void btnProgramar_Click(object sender, RoutedEventArgs e)
        {
            int duracion = 0;
            if (cmbDuracion.SelectedItem is ComboBoxItem selectedItem)
            {
                duracion = Convert.ToInt32(selectedItem.Tag);
            }

            CitasModel nuevaCita = new CitasModel
            {
                FechaCita = dpFecha.SelectedDate.Value,
                Hora = cmbHora.SelectedValue?.ToString() ?? string.Empty,
                Duracion = duracion,
                MotivoCita = txtMotivo.Text,
                PacienteID = (cmbPaciente.SelectedValue as int?) ?? 0,
                MedicoID = (cmbMedico.SelectedValue as int?) ?? 0,
                EspecialidadID = (cmbEspecialidad.SelectedValue as int?) ?? 0,
                ConsultorioID = (cmbConsultorio.SelectedValue as int?) ?? 0,
                Comentarios = txtComentarios.Text
            };

            if (!Validar(nuevaCita))
            {
                return;
            }

            DatoCitas.GuardarCita(nuevaCita);

            CargarHoras(cmbHora, nuevaCita.MedicoID, nuevaCita.FechaCita);

            Limpiar();
            CargarCitas();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            CargarDatos();
        }

        private void btnReprogramar_Click(object sender, RoutedEventArgs e)
        {
            if (dgCitas.SelectedItem is CitasModel citaSeleccionada)
            {
                CargarComboBoxDesdeCita(citaSeleccionada);

                if (!dpFecha.SelectedDate.HasValue)
                {
                    MessageBox.Show("Por favor, selecciona una fecha para la cita.");
                    return;
                }

                try
                {
                    // Obtener la duración del ComboBox
                    int duracion = 0;
                    if (cmbDuracion.SelectedItem is ComboBoxItem selectedItem)
                    {
                        duracion = Convert.ToInt32(selectedItem.Tag);
                    }

                    // Crear el objeto de la cita actualizada
                    CitasModel citaActualizada = new CitasModel
                    {
                        CitaID = citaSeleccionada.CitaID,
                        FechaCita = dpFecha.SelectedDate.Value,
                        Hora = cmbHora.SelectedValue?.ToString() ?? string.Empty,  // Obtener hora como string
                        Duracion = duracion,
                        MotivoCita = txtMotivo.Text,
                        PacienteID = (cmbPaciente.SelectedValue as int?) ?? 0,
                        MedicoID = (cmbMedico.SelectedValue as int?) ?? 0,
                        EspecialidadID = (cmbEspecialidad.SelectedValue as int?) ?? 0,
                        ConsultorioID = (cmbConsultorio.SelectedValue as int?) ?? 0,
                        Comentarios = txtComentarios.Text
                    };

                    // Actualizar la cita
                    int resultado = DatoCitas.ActualizarCita(citaActualizada);

                    if (resultado > 0)
                    {
                        MessageBox.Show("Cita reprogramada exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo reprogramar la cita.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    Limpiar();
                    CargarDatos();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Error en el formato de datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al reprogramar la cita: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una cita para reprogramar.");
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgCitas.SelectedItem is CitasModel citaSeleccionada)
            {
                DatoCitas.EliminarCita(citaSeleccionada.CitaID);
                Limpiar();
                CargarDatos();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una cita para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Cargar ComboBox 

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

        public static void CargarPacientes(ComboBox cmbPaciente)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "EXEC SPCargarComboBox @TipoComboBox = 'Pacientes'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cmbPaciente.ItemsSource = dt.DefaultView;
                        cmbPaciente.DisplayMemberPath = "Paciente"; //el alias que le puse a la base de datos
                        cmbPaciente.SelectedValuePath = "PacienteID";
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron pacientes.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar pacientes: " + ex.Message);
                }
            }
        }

        public static void CargarConsultorios(ComboBox cmbConsultorio)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "EXEC SPCargarComboBox @TipoComboBox = 'Consultorios'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cmbConsultorio.ItemsSource = dt.DefaultView;
                        cmbConsultorio.DisplayMemberPath = "NombreConsultorio";
                        cmbConsultorio.SelectedValuePath = "ConsultorioID";
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron consultorios.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar consultorios: " + ex.Message);
                }
            }
        }

        public static void CargarEstadoCitas(ComboBox cmbEstadoCita)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "EXEC SPCargarComboBox @TipoComboBox = 'EstadoCita'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cmbEstadoCita.ItemsSource = dt.DefaultView;
                        cmbEstadoCita.DisplayMemberPath = "EstadoCita";
                        cmbEstadoCita.SelectedValuePath = "EstadoCitaID";
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron estados de cita.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar estados de cita: " + ex.Message);
                }
            }
        }

        public static void CargarEspecialidades(ComboBox cmbEspecialidad, int? medicoID)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "EXEC SPCargarComboBox @TipoComboBox = 'Especialidades', @MedicoID = @MedicoID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MedicoID", medicoID.HasValue ? (object)medicoID.Value : DBNull.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cmbEspecialidad.ItemsSource = dt.DefaultView;
                        cmbEspecialidad.DisplayMemberPath = "NombreEspecialidad";
                        cmbEspecialidad.SelectedValuePath = "EspecialidadID";
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron especialidades.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar especialidades: " + ex.Message);
                }
            }
        }

        private void CargarComboBoxDesdeCita(CitasModel citaSeleccionada)
        {
            dpFecha.SelectedDate = citaSeleccionada.FechaCita;
            txtMotivo.Text = citaSeleccionada.MotivoCita;
            txtComentarios.Text = citaSeleccionada.Comentarios;

            cmbPaciente.SelectedValue = citaSeleccionada.PacienteID;
            cmbMedico.SelectedValue = citaSeleccionada.MedicoID;
            cmbEspecialidad.SelectedValue = citaSeleccionada.EspecialidadID;
            cmbConsultorio.SelectedValue = citaSeleccionada.ConsultorioID;
            cmbEstadoCita.SelectedValue = citaSeleccionada.EstadoCitaID;

            foreach (ComboBoxItem item in cmbDuracion.Items)
            {
                if (item.Tag.ToString() == citaSeleccionada.Duracion.ToString())
                {
                    cmbDuracion.SelectedItem = item;
                    break;
                }
            }
        }


        #endregion

        #region Metodo de Horas 

        public static void CargarHoras(ComboBox cmbHora, int medicoID, DateTime fechaCita, string horaSeleccionada = null)
        {
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    conn.Open();
                    string query = "EXEC SPCargarHorasMedico @MedicoID, @FechaCita";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MedicoID", medicoID);
                    cmd.Parameters.AddWithValue("@FechaCita", fechaCita);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cmbHora.ItemsSource = dt.DefaultView;
                        cmbHora.DisplayMemberPath = "Hora";
                        cmbHora.SelectedValuePath = "Hora";

                        if (!string.IsNullOrEmpty(horaSeleccionada))
                        {
                            cmbHora.SelectedValue = horaSeleccionada;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron horas disponibles.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar horas: " + ex.Message);
                }
            }
        }

        private void ActualizarHoras()
        {
            if (cmbMedico.SelectedItem != null && dpFecha.SelectedDate.HasValue)
            {
                int medicoID = (cmbMedico.SelectedValue as int? ?? 0);
                DateTime fechaSeleccionada = dpFecha.SelectedDate.Value;

                CargarHoras(cmbHora, medicoID, fechaSeleccionada);
                CargarEspecialidades(cmbEspecialidad, medicoID);

                cmbHora.IsEnabled = true;
            }
            else
            {
                cmbHora.IsEnabled = false;
            }
        }

        #endregion

        #region Cargar DataGrid 
        private void CargarCitas()
        {
            List<CitasModel> todasLasCitas = DatoCitas.CargarCitas();
            if (todasLasCitas.Count > 0)
            {
                dgCitas.ItemsSource = todasLasCitas; // Asigna la lista de citas al DataGrid
            }
            else
            {
                MessageBox.Show("No hay citas registradas.");
            }
        }


        #endregion

        #region SelectionChanged

        private void cmbMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualizarHoras();
        }

        private void dpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualizarHoras();
        }

        private void dgCitas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCitas.SelectedItem != null)
            {
                CitasModel citaSeleccionada = (CitasModel)dgCitas.SelectedItem;

                CargarComboBoxDesdeCita(citaSeleccionada);
                CargarHoras(cmbHora, citaSeleccionada.MedicoID, citaSeleccionada.FechaCita, citaSeleccionada.Hora);
            }




        }
        #endregion

        #region Botones Menu
        private void btnMedicos_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmMedicos ventana = new frmMedicos(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnConsultas_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmConsultas ventana = new frmConsultas(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnRecetas_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmRecetas ventana = new frmRecetas(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void btnExamenes_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmMenuExamen ventana = new frmMenuExamen(EnviarRol);
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

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmInicio ventana = new frmInicio(EnviarRol);
            ventana.Show();
            this.Close();
        }
        #endregion

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
