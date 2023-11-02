﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using personapi_dotnet.Models;

#nullable disable

namespace personapi_dotnet.Migrations
{
    [DbContext(typeof(dbContext))]
    partial class dbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("personapi_dotnet.Models.Entities.Estudio", b =>
                {
                    b.Property<int>("IdProf")
                        .HasColumnType("int")
                        .HasColumnName("id_prof");

                    b.Property<int>("CcPer")
                        .HasColumnType("int")
                        .HasColumnName("cc_per");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("fecha");

                    b.Property<string>("Univer")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("univer");

                    b.HasKey("IdProf", "CcPer")
                        .HasName("pk_estudios");

                    b.HasIndex("CcPer");

                    b.ToTable("estudios");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Entities.Persona", b =>
                {
                    b.Property<int>("Cc")
                        .HasColumnType("int")
                        .HasColumnName("cc");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("apellido");

                    b.Property<int?>("Edad")
                        .HasColumnType("int")
                        .HasColumnName("edad");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .HasColumnName("genero")
                        .IsFixedLength();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("nombre");

                    b.HasKey("Cc");

                    b.ToTable("persona");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Entities.Profesion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Des")
                        .HasColumnType("text")
                        .HasColumnName("des");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(90)
                        .IsUnicode(false)
                        .HasColumnType("varchar(90)")
                        .HasColumnName("nom");

                    b.HasKey("Id");

                    b.ToTable("profesion");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Entities.Telefono", b =>
                {
                    b.Property<string>("Num")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("num");

                    b.Property<int>("Duenio")
                        .HasColumnType("int")
                        .HasColumnName("duenio");

                    b.Property<string>("Oper")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("oper");

                    b.HasKey("Num");

                    b.HasIndex("Duenio");

                    b.ToTable("telefono");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Entities.Estudio", b =>
                {
                    b.HasOne("personapi_dotnet.Models.Entities.Persona", "CcPerNavigation")
                        .WithMany("Estudios")
                        .HasForeignKey("CcPer")
                        .IsRequired()
                        .HasConstraintName("estudio_persona_fk");

                    b.HasOne("personapi_dotnet.Models.Entities.Profesion", "IdProfNavigation")
                        .WithMany("Estudios")
                        .HasForeignKey("IdProf")
                        .IsRequired()
                        .HasConstraintName("estudio_profesion_fk");

                    b.Navigation("CcPerNavigation");

                    b.Navigation("IdProfNavigation");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Entities.Telefono", b =>
                {
                    b.HasOne("personapi_dotnet.Models.Entities.Persona", "DuenioNavigation")
                        .WithMany("Telefonos")
                        .HasForeignKey("Duenio")
                        .IsRequired()
                        .HasConstraintName("FK_telefono_persona");

                    b.Navigation("DuenioNavigation");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Entities.Persona", b =>
                {
                    b.Navigation("Estudios");

                    b.Navigation("Telefonos");
                });

            modelBuilder.Entity("personapi_dotnet.Models.Entities.Profesion", b =>
                {
                    b.Navigation("Estudios");
                });
#pragma warning restore 612, 618
        }
    }
}
