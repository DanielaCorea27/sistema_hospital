using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Models
{
    public class MedicosModel
    {

        //atributos
        public int MedicoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public int EspecialidadID { get; set; }
        public string Especialidad { get; set; }
        public int TelefonoID { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int HoraInicioID { get; set; }
        public string HoraInicio { get; set; }
        public int HoraFinID { get; set; }
        public string HoraFin { get; set; }
        public int ConsultorioID { get; set; }
        public string Consultorio { get; set; }
        public int Estado { get; set; }


        //metodos
        //Constructor vacio
        public MedicosModel() { }
    }
}
