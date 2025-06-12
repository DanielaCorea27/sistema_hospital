using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.ManejarRoles
{
    public static class RolesConfigurar
    {
        public static readonly Dictionary<int, Dictionary<string, bool>> PermisosPorRol = new Dictionary<int, Dictionary<string, bool>>
    {
        {
            1, // Rol 1
            new Dictionary<string, bool>
            {
                { "btnPacientes", true },
                { "btnHistorialesMedicos", true },
                { "btnMedicos", true },
                { "btnConsultas", true },
                { "btnRecetas", true },
                { "btnCitas", true },
                { "btnExamenes", true },
                { "btnReportes", true },
                { "btnUsuarios", true },
            }
        },
        {
            2, // Rol 2
            new Dictionary<string, bool>
            {
                { "btnPacientes", true },
                { "btnHistorialesMedicos", true },
                { "btnMedicos", false },
                { "btnConsultas", true },
                { "btnRecetas", false },
                { "btnCitas", true },
                { "btnExamenes", false },
                { "btnReportes", false },
                { "btnUsuarios", false },
            }
        },
        {
            3, // Rol 3
            new Dictionary<string, bool>
            {
                { "btnPacientes", true },
                { "btnHistorialesMedicos", true },
                { "btnMedicos", false },
                { "btnConsultas", true },
                { "btnRecetas", true },
                { "btnCitas", false },
                { "btnExamenes", true },
                { "btnReportes", false },
                { "btnUsuarios", false },
            }
        }
    };
    }
}
