using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Models
{
    public class ConsultaModel
    {
        public int ConsultaID { get; set; }
        public int CitaID { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string MotivoConsulta { get; set; }
        public int PacienteID { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public int MedicoID { get; set; }
        public string NombreMedico { get; set; }
        public string ApellidoMedico { get; set; }
        public int EstadoID { get; set; }
        public int ConsultorioID { get; set; }
        public string NombreConsultorio { get; set; }
        public bool Examen { get; set; }

        public ConsultaModel() { }
    }
}
