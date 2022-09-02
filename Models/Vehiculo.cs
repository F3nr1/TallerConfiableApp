using System;
using System.Collections.Generic;

namespace TallerMecanicoCApp.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Diagnosticos = new HashSet<Diagnostico>();
            Soats = new HashSet<Soat>();
        }

        public int VehiculoId { get; set; }
        public string? Placa { get; set; }
        public string? TipoVehiculo { get; set; }
        public string? Marca { get; set; }
        public int? CapacidadPasajeros { get; set; }
        public int? Cilindrada { get; set; }
        public int ClienteId { get; set; }
        public int MecanicoId { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Mecanico Mecanico { get; set; } = null!;
        public virtual ICollection<Diagnostico> Diagnosticos { get; set; }
        public virtual ICollection<Soat> Soats { get; set; }
    }
}
