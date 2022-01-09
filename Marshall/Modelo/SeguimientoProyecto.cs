using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Marshall.Clases
{
    class SeguimientoProyecto
    {

        public int Oci { get; set; }
        public String Cliente { get; set; }
        public String NombreGral { get; set; }
        public String Descripcion { get; set; }
        public String Tela { get; set; }

        public String Color { get; set; }
        public int CantidadRealOci { get; set; }
        public int CantidadPendiente { get; set; }
        public String Categoria { get; set; }
        public String Asignado { get; set; }
        public String EstimadoInicio { get; set; }
        public String EstimadoFinalizado { get; set; }

        public DateTime? EstimadoInicioFecha { get; set; }
        public DateTime? EstimadoFinalizadoFecha { get; set; }


        public int EstimadoDuracionDias { get; set; }
        public String RealInicio { get; set; }
        public String RealFinalizacion { get; set; }

        public DateTime? RealInicioFecha { get; set; }
        public DateTime? RealFinalizadoFecha { get; set; }
        
        public int RealDuracionDias { get; set; }
        public String Notas { get; set; }
        public String Prioridad { get; set; }
        public String Vendedor { get; set; }

    }
}
