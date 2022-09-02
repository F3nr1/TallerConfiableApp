using System;
using System.Collections.Generic;

namespace TallerMecanicoCApp.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }

        public int ClienteId { get; set; }
        public string? Correo { get; set; }
        public int? PersonaId { get; set; }

        public virtual Persona? Persona { get; set; }
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
