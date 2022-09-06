using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TallerMecanicoCApp.Models
{
    public partial class DBTallerMContext : DbContext
    {
        public DBTallerMContext()
        {
        }

        public DBTallerMContext(DbContextOptions<DBTallerMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrativo> Administrativos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Diagnostico> Diagnosticos { get; set; } = null!;
        public virtual DbSet<Direccion> Direccions { get; set; } = null!;
        public virtual DbSet<Mecanico> Mecanicos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Soat> Soats { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=CARLOS-PC;Database=DBTaller; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrativo>(entity =>
            {
                entity.HasKey(e => e.AdmId);

                entity.ToTable("Administrativo");

                entity.Property(e => e.AdmId).HasColumnName("AdmID");

                entity.Property(e => e.NivelEstudio)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.HasIndex(e => e.PersonaId, "IX_Cliente_PersonaID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Correo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaId).HasColumnName("PersonaID");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.PersonaId)
                    .HasConstraintName("FK_Cliente_Persona");
            });

            modelBuilder.Entity<Diagnostico>(entity =>
            {
                entity.HasKey(e => e.DiagId);

                entity.ToTable("Diagnostico");

                entity.HasIndex(e => e.VehiculoId, "IX_Diagnostico_VehiculoID");

                entity.Property(e => e.DiagId).HasColumnName("DiagID");

                entity.Property(e => e.Repuesto)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RevisionNiveles)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoRepuesto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoRevision)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");

                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.Diagnosticos)
                    .HasForeignKey(d => d.VehiculoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Diagnostico_Vehiculo");
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.ToTable("Direccion");

                entity.Property(e => e.DireccionId).HasColumnName("DireccionID");

                entity.Property(e => e.Num1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Num2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Num3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoCalle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zona)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mecanico>(entity =>
            {
                entity.ToTable("Mecanico");

                entity.HasIndex(e => e.PersonaId, "IX_Mecanico_PersonaID");

                entity.Property(e => e.MecanicoId).HasColumnName("MecanicoID");

                entity.Property(e => e.NivelEstudios)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaId).HasColumnName("PersonaID");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Mecanicos)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mecanico_Persona");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.HasIndex(e => e.DireccionId, "IX_Persona_DireccionID");

                entity.Property(e => e.PersonaId).HasColumnName("PersonaID");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionId).HasColumnName("DireccionID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Direccion)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.DireccionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Direccion_Persona");
            });

            modelBuilder.Entity<Soat>(entity =>
            {
                entity.ToTable("Soat");

                entity.HasIndex(e => e.VehiculoId, "IX_Soat_VehiculoID");

                entity.Property(e => e.SoatId).HasColumnName("SoatID");

                entity.Property(e => e.FechaVencimiento).HasColumnType("date");

                entity.Property(e => e.PuedeTransitar)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");

                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.Soats)
                    .HasForeignKey(d => d.VehiculoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Soat_Vehiculo");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculo");

                entity.HasIndex(e => e.ClienteId, "IX_Vehiculo_ClienteID");

                entity.HasIndex(e => e.MecanicoId, "IX_Vehiculo_MecanicoID");

                entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Marca)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MecanicoId).HasColumnName("MecanicoID");

                entity.Property(e => e.Placa)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TipoVehiculo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculo_Cliente");

                entity.HasOne(d => d.Mecanico)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.MecanicoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculo_Mecanico");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
