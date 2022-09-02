using System;
using System.Collections.Generic;

namespace TallerMecanicoCApp.Models
{
    public partial class Diagnostico
    {
        public int DiagId { get; set; }
        public string? TipoRevision { get; set; }
        public string? RevisionNiveles { get; set; }
        public string? TipoRepuesto { get; set; }
        public string? Repuesto { get; set; }
        public int VehiculoId { get; set; }

        public virtual Vehiculo Vehiculo { get; set; } = null!;
    }
}
