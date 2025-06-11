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
using Usuario.Reports;

namespace Usuario
{
    /// <summary>
    /// Lógica de interacción para frmReportes.xaml
    /// </summary>
    public partial class frmReportes : Window
    {
        private int EnviarRol;
        public frmReportes(int VerificarRol)
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

        private void btnExpedientePacientes_Click(object sender, RoutedEventArgs e)
        {
            // INICIAR EL REPORTE CON CRYSTAL REPORTS
            rptExpedientePaciente rpt = new rptExpedientePaciente();
            // INSTANCIAR EL FORMULARIO VISOR DEL REPORTE
            vsReporte visor = new vsReporte();
            // CONFIGURAR LA CARGA DEL REPORTE
            rpt.Load(@"rptExpedientePaciente.rpt");
            // ASIGNAR EL ORIGEN DEL REPORTE AL VISOR
            visor.crvReporte.ViewerCore.ReportSource = rpt;
            // MOSTRAR EL VISOR O LA VENTANA CON EL REPORTE
            visor.Show();
        }

        private void btnRecetasPacientes_Click(object sender, RoutedEventArgs e)
        {
            // INICIAR EL REPORTE CON CRYSTAL REPORTS
            rptRecetasPacientes rpt = new rptRecetasPacientes();
            // INSTANCIAR EL FORMULARIO VISOR DEL REPORTE
            vsReporte visor = new vsReporte();
            // CONFIGURAR LA CARGA DEL REPORTE
            rpt.Load(@"rptRecetasPacientes.rpt");
            // ASIGNAR EL ORIGEN DEL REPORTE AL VISOR
            visor.crvReporte.ViewerCore.ReportSource = rpt;
            // MOSTRAR EL VISOR O LA VENTANA CON EL REPORTE
            visor.Show();
        }

        private void btnExamenesPacientes_Click(object sender, RoutedEventArgs e)
        {
            // INICIAR EL REPORTE CON CRYSTAL REPORTS
            rptExamenesPacientes rpt = new rptExamenesPacientes();
            // INSTANCIAR EL FORMULARIO VISOR DEL REPORTE
            vsReporte visor = new vsReporte();
            // CONFIGURAR LA CARGA DEL REPORTE
            rpt.Load(@"rptExamenesPacientes.rpt");
            // ASIGNAR EL ORIGEN DEL REPORTE AL VISOR
            visor.crvReporte.ViewerCore.ReportSource = rpt;
            // MOSTRAR EL VISOR O LA VENTANA CON EL REPORTE
            visor.Show();
        }

        private void btnConsultasMedico_Click(object sender, RoutedEventArgs e)
        {
            // INICIAR EL REPORTE CON CRYSTAL REPORTS
            rptConsultaMedico rpt = new rptConsultaMedico();
            // INSTANCIAR EL FORMULARIO VISOR DEL REPORTE
            vsReporte visor = new vsReporte();
            // CONFIGURAR LA CARGA DEL REPORTE
            rpt.Load(@"rptConsultaMedico.rpt");
            // ASIGNAR EL ORIGEN DEL REPORTE AL VISOR
            visor.crvReporte.ViewerCore.ReportSource = rpt;
            // MOSTRAR EL VISOR O LA VENTANA CON EL REPORTE
            visor.Show();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frmInicio ventana = new frmInicio(EnviarRol);
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
