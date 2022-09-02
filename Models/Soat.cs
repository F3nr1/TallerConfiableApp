using System;
using System.Collections.Generic;

namespace TallerMecanicoCApp.Models
{
    public partial class Soat
    {
        public int SoatId { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string? PuedeTransitar { get; set; }
        public int VehiculoId { get; set; }

        public virtual Vehiculo Vehiculo { get; set; } = null!;
    }
}
