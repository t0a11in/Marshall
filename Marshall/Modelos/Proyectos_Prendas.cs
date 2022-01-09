//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Marshall.Modelos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Proyectos_Prendas
    {
        public int id { get; set; }
        public int ProyectosId { get; set; }
        public int PrendaId { get; set; }
        public int TelaId { get; set; }
        public Nullable<int> CantidaReal { get; set; }
        public Nullable<int> CantidadPendiente { get; set; }
        public Nullable<System.DateTime> EstimadoInicio { get; set; }
        public Nullable<System.DateTime> EstimadoFinalizado { get; set; }
        public Nullable<System.DateTime> RealInicio { get; set; }
        public Nullable<System.DateTime> RealFinalizado { get; set; }
        public Nullable<int> EstadoId { get; set; }
    
        public virtual Estados Estados { get; set; }
        public virtual Prendas Prendas { get; set; }
        public virtual Proyectos Proyectos { get; set; }
        public virtual Telas Telas { get; set; }
    }
}
