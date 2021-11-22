using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace PawApi.Models
{
    public partial class PawHouseContext : DbContext
    {
        private IConfiguration configuration;

        public PawHouseContext()
        {
        }

        public PawHouseContext(DbContextOptions<PawHouseContext> options,IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        public virtual DbSet<Comodidad> Comodidads { get; set; }
        public virtual DbSet<Cuidador> Cuidadors { get; set; }
        public virtual DbSet<CuidadorMascota> CuidadorMascotas { get; set; }
        public virtual DbSet<Denuncium> Denuncia { get; set; }
        public virtual DbSet<Especie> Especies { get; set; }
        public virtual DbSet<Estadium> Estadia { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Foto> Fotos { get; set; }
        public virtual DbSet<FotoCuidador> FotoCuidadors { get; set; }
        public virtual DbSet<FotoSolicitud> FotoSolicituds { get; set; }
        public virtual DbSet<HogarDetalle> HogarDetalles { get; set; }
        public virtual DbSet<HogarTemporal> HogarTemporals { get; set; }
        public virtual DbSet<Metodo> Metodos { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Resenium> Resenia { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Solicitud> Solicituds { get; set; }
        public virtual DbSet<TipoHogar> TipoHogars { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(configuration.GetConnectionString("pawcontext"));
                optionsBuilder.UseSqlServer("Server=pawapidbserver.database.windows.net; Database=PawApi_db; User Id=pawadmin;Password=admin1234?;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Comodidad>(entity =>
            {
                entity.ToTable("Comodidad");

                entity.Property(e => e.Descripcion).IsUnicode(false);
            });

            modelBuilder.Entity<Cuidador>(entity =>
            {
                entity.ToTable("Cuidador");

                entity.Property(e => e.Puntaje).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Cuidadors)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_Cuidador_Usuario");

                entity.Property(e => e.FotoPerfil).IsUnicode(false);
                entity.Property(e => e.DescripcionServicio).IsUnicode(false);
            });

            modelBuilder.Entity<CuidadorMascota>(entity =>
            {
                entity.ToTable("Cuidador_Mascotas");

                entity.Property(e => e.CuidadorId).HasColumnName("Cuidador_Id");

                entity.Property(e => e.EspecieId).HasColumnName("Especie_Id");

                entity.Property(e => e.PrecioUnitarioDiario).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Cuidador)
                    .WithMany(p => p.CuidadorMascota)
                    .HasForeignKey(d => d.CuidadorId)
                    .HasConstraintName("FK_Cuidador_Mascotas_Cuidador");

                entity.HasOne(d => d.Especie)
                    .WithMany(p => p.CuidadorMascota)
                    .HasForeignKey(d => d.EspecieId)
                    .HasConstraintName("FK_Cuidador_Mascotas_Especie");
            });

            modelBuilder.Entity<Denuncium>(entity =>
            {
                entity.Property(e => e.CuidadorId).HasColumnName("Cuidador_Id");

                entity.Property(e => e.Denuncia).IsUnicode(false);

                entity.HasOne(d => d.Cuidador)
                    .WithMany(p => p.Denuncia)
                    .HasForeignKey(d => d.CuidadorId)
                    .HasConstraintName("FK_Denuncia_Cuidador");
            });

            modelBuilder.Entity<Especie>(entity =>
            {
                entity.ToTable("Especie");

                entity.Property(e => e.Especie1)
                    .IsUnicode(false)
                    .HasColumnName("Especie");
            });

            modelBuilder.Entity<Estadium>(entity =>
            {
                entity.Property(e => e.CuidadorId).HasColumnName("Cuidador_Id");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.HogarId).HasColumnName("Hogar_Id");

                entity.Property(e => e.PersonaId).HasColumnName("Persona_Id");

                entity.HasOne(d => d.Cuidador)
                    .WithMany(p => p.Estadia)
                    .HasForeignKey(d => d.CuidadorId)
                    .HasConstraintName("FK_Estadia_Cuidador");

                entity.HasOne(d => d.Hogar)
                    .WithMany(p => p.Estadia)
                    .HasForeignKey(d => d.HogarId)
                    .HasConstraintName("FK_Estadia_HogarTemporal");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Estadia)
                    .HasForeignKey(d => d.PersonaId)
                    .HasConstraintName("FK_Estadia_Persona");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("Estado");

                entity.Property(e => e.Estado1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Estado");
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.ToTable("Foto");

                entity.Property(e => e.Path).IsUnicode(false);
            });

            modelBuilder.Entity<FotoCuidador>(entity =>
            {
                entity.ToTable("Foto_Cuidador");

                entity.Property(e => e.CuidadorId).HasColumnName("Cuidador_Id");

                entity.Property(e => e.FotoId).HasColumnName("Foto_Id");

                entity.HasOne(d => d.Cuidador)
                    .WithMany(p => p.FotoCuidadors)
                    .HasForeignKey(d => d.CuidadorId)
                    .HasConstraintName("FK_Foto_Cuidador_Cuidador");

                entity.HasOne(d => d.Foto)
                    .WithMany(p => p.FotoCuidadors)
                    .HasForeignKey(d => d.FotoId)
                    .HasConstraintName("FK_Foto_Cuidador_Foto");
            });

            modelBuilder.Entity<FotoSolicitud>(entity =>
            {
                entity.ToTable("Foto_Solicitud");

                entity.Property(e => e.FotoId).HasColumnName("Foto_Id");

                entity.Property(e => e.SolicitudId).HasColumnName("Solicitud_Id");

                entity.HasOne(d => d.Foto)
                    .WithMany(p => p.FotoSolicituds)
                    .HasForeignKey(d => d.FotoId)
                    .HasConstraintName("FK_Foto_Solicitud_Foto");

                entity.HasOne(d => d.Solicitud)
                    .WithMany(p => p.FotoSolicituds)
                    .HasForeignKey(d => d.SolicitudId)
                    .HasConstraintName("FK_Foto_Solicitud_Solicitud");
            });

            modelBuilder.Entity<HogarDetalle>(entity =>
            {
                entity.ToTable("Hogar_Detalle");

                entity.Property(e => e.ComodidadId).HasColumnName("Comodidad_Id");

                entity.Property(e => e.HogarId).HasColumnName("Hogar_Id");

                entity.HasOne(d => d.Comodidad)
                    .WithMany(p => p.HogarDetalles)
                    .HasForeignKey(d => d.ComodidadId)
                    .HasConstraintName("FK_Hogar_Detalle_Comodidad");

                entity.HasOne(d => d.Hogar)
                    .WithMany(p => p.HogarDetalles)
                    .HasForeignKey(d => d.HogarId)
                    .HasConstraintName("FK_Hogar_Detalle_HogarTemporal");
            });

            modelBuilder.Entity<HogarTemporal>(entity =>
            {
                entity.ToTable("HogarTemporal");

                entity.Property(e => e.CuidadorId).HasColumnName("Cuidador_Id");

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.Localidad).IsUnicode(false);

                entity.Property(e => e.Provincia).IsUnicode(false);

                entity.Property(e => e.TipoHogarId).HasColumnName("TipoHogar_Id");

                entity.HasOne(d => d.Cuidador)
                    .WithMany(p => p.HogarTemporals)
                    .HasForeignKey(d => d.CuidadorId)
                    .HasConstraintName("FK_HogarTemporal_Cuidador");

                entity.HasOne(d => d.TipoHogar)
                    .WithMany(p => p.HogarTemporals)
                    .HasForeignKey(d => d.TipoHogarId)
                    .HasConstraintName("FK_HogarTemporal_TipoHogar");
            });

            modelBuilder.Entity<Metodo>(entity =>
            {
                entity.ToTable("Metodo");

                entity.Property(e => e.Descripcion).IsUnicode(false);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("Pago");

                entity.Property(e => e.Comision).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EstadiaId).HasColumnName("Estadia_Id");

                entity.Property(e => e.Estado).IsUnicode(false);

                entity.Property(e => e.FechaEmitido).HasColumnType("datetime");

                entity.Property(e => e.FechaPagado).HasColumnType("datetime");

                entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MetodoId).HasColumnName("Metodo_Id");

                entity.Property(e => e.NumeroRefencia).IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Estadia)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.EstadiaId)
                    .HasConstraintName("FK_Pago_Estadia");

                entity.HasOne(d => d.Metodo)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.MetodoId)
                    .HasConstraintName("FK_Pago_Metodo");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.Property(e => e.NompreApellido).IsUnicode(false);

                entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_Persona_Usuario");
            });

            modelBuilder.Entity<Resenium>(entity =>
            {
                entity.Property(e => e.CuidadorId).HasColumnName("Cuidador_Id");

                entity.Property(e => e.EstadiaId).HasColumnName("Estadia_Id");

                entity.Property(e => e.Puntuacion).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Resenia).IsUnicode(false);

                entity.HasOne(d => d.Cuidador)
                    .WithMany(p => p.Resenia)
                    .HasForeignKey(d => d.CuidadorId)
                    .HasConstraintName("FK_Resenia_Cuidador");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Rol1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Rol");
            });

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.ToTable("Solicitud");

                entity.Property(e => e.CuidadorId).HasColumnName("Cuidador_Id");
                entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

                entity.Property(e => e.DniSolicitud)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAprobacion).HasColumnType("datetime");

                entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");

                entity.Property(e => e.FechaVideoLlamada).HasColumnType("datetime");

                entity.Property(e => e.MotivoRechazo).IsUnicode(false);

                entity.Property(e => e.Observaciones).IsUnicode(false);

                entity.Property(e => e.Peticion).IsUnicode(false);

                entity.HasOne(d => d.Cuidador)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.CuidadorId)
                    .HasConstraintName("FK_Solicitud_Cuidador");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Solicituds)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_Solicitud_Usuario");

                entity.Property(e => e.Estado).IsUnicode(false);
            });

            modelBuilder.Entity<TipoHogar>(entity =>
            {
                entity.ToTable("TipoHogar");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo).IsUnicode(false);

                entity.Property(e => e.EstadoId).HasColumnName("Estado_Id");

                entity.Property(e => e.FechaDeCreacion).HasColumnType("datetime");

                entity.Property(e => e.Legajo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Usuario");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.EstadoId)
                    .HasConstraintName("FK_Usuario_Estado");
            });

            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.ToTable("Usuario_Rol");

                entity.Property(e => e.RolId).HasColumnName("Rol_Id");

                entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.UsuarioRols)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_Usuario_Rol_Rol");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.UsuarioRols)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_Usuario_Rol_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
