using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models;

public partial class dbContext : DbContext
{
    public dbContext()
    {
    }

    public dbContext(DbContextOptions<dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudio> Estudios { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Profesion> Profesions { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:masiv.database.windows.net,1433;Initial Catalog=elevetor;Persist Security Info=False;User ID=atpenapena;Password=masiv2023*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudio>(entity =>
        {
            entity.HasKey(e => new { e.IdProf, e.CcPer }).HasName("pk_estudios");

            entity.HasOne(d => d.CcPerNavigation).WithMany(p => p.Estudios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudio_persona_fk");

            entity.HasOne(d => d.IdProfNavigation).WithMany(p => p.Estudios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudio_profesion_fk");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.Property(e => e.Cc).ValueGeneratedNever();
            entity.Property(e => e.Genero).IsFixedLength();
        });

        modelBuilder.Entity<Profesion>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasOne(d => d.DuenioNavigation).WithMany(p => p.Telefonos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_telefono_persona");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
