using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace FinalProgramacionII.Models
{
    public partial class SellPointContext : DbContext
    {
        public SellPointContext()
        {
        }

        public SellPointContext(DbContextOptions<SellPointContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Entidade> Entidades { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["ConnectionString"];
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entidade>(entity =>
            {
                entity.HasKey(e => e.IdEntidades)
                    .HasName("PK__Entidade__E0C75AC7AC508131");

                entity.HasIndex(e => new { e.Descripcion, e.TipoEntidad, e.TipoDocumento, e.NumeroDocumento }, "EntidadesIndex");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Comentario).HasColumnType("text");

                entity.Property(e => e.CoordenadasGps)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CoordenadasGPS");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion).HasColumnType("text");

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.Localidad)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordEntidad)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.RolUserEntidad)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('User')");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Status_")
                    .HasDefaultValueSql("('Activa')");

                entity.Property(e => e.Telefonos)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('RNC')");

                entity.Property(e => e.TipoEntidad)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Juridica')");

                entity.Property(e => e.Urlfacebook)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("URLFacebook");

                entity.Property(e => e.Urlinstagram)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("URLInstagram");

                entity.Property(e => e.UrlpaginaWeb)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("URLPaginaWeb");

                entity.Property(e => e.UrltikTok)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("URLTikTok");

                entity.Property(e => e.Urltwitter)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("URLTwitter");

                entity.Property(e => e.UserNameEntidad)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
