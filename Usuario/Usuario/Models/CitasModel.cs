using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Models
{
    public class CitasModel
    {
        //El mismo orden del data grid
        public string NombreEspecialidad { get; set; }
        public string Medico { get; set; }
        public string Paciente { get; set; }
        public string NombreConsultorio { get; set; }
        public string MotivoCita { get; set; }
        public string EstadoCita { get; set; }
        public string Comentarios { get; set; }

        public int HorarioCitaID { get; set; }
        public DateTime FechaCita { get; set; }
        public string Hora { get; set; }
        public int Duracion { get; set; }


        public int CitaID { get; set; }
        public int MedicoID { get; set; }
        public int PacienteID { get; set; }
        public int EspecialidadID { get; set; }
        public int ConsultorioID { get; set; }
        public int EstadoCitaID { get; set; }

        public CitasModel() { }
    }

}
