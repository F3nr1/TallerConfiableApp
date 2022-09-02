using System;
using System.Collections.Generic;

namespace TallerMecanicoCApp.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Clientes = new HashSet<Cliente>();
            Mecanicos = new HashSet<Mecanico>();
        }

        public int PersonaId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Telefono { get; set; }
        public int DireccionId { get; set; }

        public virtual Direccion Direccion { get; set; } = null!;
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Mecanico> Mecanicos { get; set; }
    }
}
