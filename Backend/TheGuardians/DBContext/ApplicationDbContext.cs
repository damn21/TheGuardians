using Microsoft.EntityFrameworkCore;
using TheGuardians.Models;

namespace TheGuardians.DBContext
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agendum> Agenda { get; set; }
        public virtual DbSet<Combate> Combates { get; set; }
        public virtual DbSet<ContactoPersonal> ContactoPersonals { get; set; }
        public virtual DbSet<Heroe> Heroes { get; set; }
        public virtual DbSet<Patrocinador> Patrocinadors { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Villano> Villanos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=TheGuardians;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendum>(entity =>
            {
                entity.HasKey(e => e.HeroeId);

                entity.Property(e => e.HeroeId)
                    .ValueGeneratedNever()
                    .HasColumnName("heroe_id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.HasOne(d => d.Heroe)
                    .WithOne(p => p.Agendum)
                    .HasForeignKey<Agendum>(d => d.HeroeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agenda_Heroe");
            });

            modelBuilder.Entity<Combate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Combate");

                entity.Property(e => e.HeroeId).HasColumnName("heroe_id");

                entity.Property(e => e.Resultado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("resultado");

                entity.Property(e => e.VillanoId).HasColumnName("villano_id");

                entity.HasOne(d => d.Heroe)
                    .WithMany()
                    .HasForeignKey(d => d.HeroeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Combate_Heroe");

                entity.HasOne(d => d.Villano)
                    .WithMany()
                    .HasForeignKey(d => d.VillanoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Combate_Villano");
            });

            modelBuilder.Entity<ContactoPersonal>(entity =>
            {
                entity.ToTable("ContactoPersonal");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HeroeId).HasColumnName("heroe_id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.TipoR)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Heroe)
                    .WithMany(p => p.ContactoPersonals)
                    .HasForeignKey(d => d.HeroeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactoPersonal_Heroe");
            });

            modelBuilder.Entity<Heroe>(entity =>
            {
                entity.ToTable("Heroe");

                entity.Property(e => e.HeroeId).HasColumnName("heroe_id");

                entity.Property(e => e.IdPersona).HasColumnName("id_persona");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Heroes)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Heroe_Persona");
            });

            modelBuilder.Entity<Patrocinador>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.ToTable("Patrocinador");

                entity.Property(e => e.PId).HasColumnName("p_id");

                entity.Property(e => e.HeroeId).HasColumnName("heroe_id");

                entity.Property(e => e.Monto).HasColumnName("monto");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.OrigenDinero)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("origen_dinero");

                entity.HasOne(d => d.Heroe)
                    .WithMany(p => p.Patrocinadors)
                    .HasForeignKey(d => d.HeroeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patrocinador_Heroe");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Apodo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apodo");

                entity.Property(e => e.Debilidad)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("debilidad");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.HabilidadPoder)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("habilidad_poder");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Pais)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pais");
            });

            modelBuilder.Entity<Villano>(entity =>
            {
                entity.ToTable("Villano");

                entity.Property(e => e.VillanoId).HasColumnName("villano_id");

                entity.Property(e => e.Origen)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("origen");

                entity.Property(e => e.PersonaId).HasColumnName("persona_id");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Villanos)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Villano_Persona");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
