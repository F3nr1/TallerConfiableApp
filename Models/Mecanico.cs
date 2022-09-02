using System;
using System.Collections.Generic;

namespace TallerMecanicoCApp.Models
{
    public partial class Mecanico
    {
        public Mecanico()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }

        public int MecanicoId { get; set; }
        public string? NivelEstudios { get; set; }
        public int PersonaId { get; set; }

        public virtual Persona Persona { get; set; } = null!;
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
