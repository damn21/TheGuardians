using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
            //modelBuilder.Entity<Agendum>(entity =>
            //{
            //    entity.Property(e => e.HeroeId).ValueGeneratedNever();

            //    entity.HasOne(d => d.Heroe)
            //        .WithOne(p => p.Agendum)
            //        .HasForeignKey<Agendum>(d => d.HeroeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Agenda_Heroe");
            //});

            //modelBuilder.Entity<Combate>(entity =>
            //{
            //    entity.HasOne(d => d.Heroe)
            //        .WithMany()
            //        .HasForeignKey(d => d.HeroeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Combate_Heroe");

            //    entity.HasOne(d => d.Villano)
            //        .WithMany()
            //        .HasForeignKey(d => d.VillanoId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Combate_Villano");
            //});

            //modelBuilder.Entity<ContactoPersonal>(entity =>
            //{
            //    entity.HasOne(d => d.Heroe)
            //        .WithMany(p => p.ContactoPersonals)
            //        .HasForeignKey(d => d.HeroeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_ContactoPersonal_Heroe");
            //});

            //modelBuilder.Entity<Heroe>(entity =>
            //{
            //    entity.HasOne(d => d.IdPersonaNavigation)
            //        .WithMany(p => p.Heroes)
            //        .HasForeignKey(d => d.IdPersona)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Heroe_Persona");
            //});

            //modelBuilder.Entity<Patrocinador>(entity =>
            //{
            //    entity.HasOne(d => d.Heroe)
            //        .WithMany(p => p.Patrocinadors)
            //        .HasForeignKey(d => d.HeroeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Patrocinador_Heroe");
            //});

            //modelBuilder.Entity<Villano>(entity =>
            //{
            //    entity.HasOne(d => d.Persona)
            //        .WithMany(p => p.Villanos)
            //        .HasForeignKey(d => d.PersonaId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Villano_Persona");
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
