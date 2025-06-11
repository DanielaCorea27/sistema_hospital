using System;
using System.Collections.Generic;
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

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmInicio.xaml
    /// </summary>
    public partial class frmInicio : Window
    {
        private int EnviarRol;
        public frmInicio(int VerificarRol)
        {
            InitializeComponent();
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

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmUsuarios usuarios = new frmUsuarios(EnviarRol);
            usuarios.Show();
            this.Close();
        }

        private void btnMedicos_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmMedicos ventana = new frmMedicos(EnviarRol);
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

        private void btnConsultas_Click(object sender, RoutedEventArgs e)
        {
            //instanciar el formulario de inicio
            frmConsultas ventana = new frmConsultas(EnviarRol);
            ventana.Show();
            this.Close();
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

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            frmReportes ventana = new frmReportes(EnviarRol);
            ventana.Show();
            this.Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow ventana = new MainWindow();
            ventana.Show();
            this.Close();
        }
    }
}
