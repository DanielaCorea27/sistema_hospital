using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Models
{
    public class RecetaModel
    {
        public int RecetaID { get; set; }
        public DateTime FechaEmision { get; set; }
        public int PacienteID { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public int MedicoID { get; set; }
        public string NombreMedico { get; set; }
        public string ApellidoMedico { get; set; }
        public int EstadoRecetaID { get; set; }
        public string NombreEstadoReceta { get; set; }
        public int ConsultaID { get; set; }
        public int EstadoID { get; set; }
        public string Indicaciones { get; set; }

        public int MedicamentosID { get; set; }
        public string NombreMedicamento { get; set; }
        public int Cantidad { get; set; }
        public RecetaModel() { }
    }
}
