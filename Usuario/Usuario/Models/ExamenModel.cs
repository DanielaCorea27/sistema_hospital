using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Models
{
    public class ExamenModel
    {
        //atributos
        public int ExamenId { get; set; }
        public int ConsultaID { get; set; }
        public int TipoExamenID { get; set; }
        public string TipoExamen { get; set; }
        public DateTime FechaExamen { get; set; }
        public DateTime FechaResultado { get; set; }
        public string Resultado { get; set; }
        public int EspecialidadID { get; set; }
        public string Especialidad { get; set; }
        public int PacienteID { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public int? MedicoID { get; set; }
        public string NombreMedico { get; set; }
        public string ApellidoMedico { get; set; }
        public int Estado { get; set; }
        public int EstadoExamen { get; set; }
        public string EstadoExamenNombre { get; set; }




        //metodos
        //Constructor vacio
        public ExamenModel() { }
    }
}
