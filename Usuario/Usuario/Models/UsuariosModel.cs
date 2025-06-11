using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Models
{
    public class UsuariosModel
    {
        //atributos
        public int usuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public int Rol { get; set; }
        public string Role { get; set; }
        public string Clave { get; set; }
        public int Estado { get; set; }

        //metodos
        //Constructor vacio
        public UsuariosModel() { }
    }
}
