using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Projects.EFCore.Models;

#nullable disable

namespace Projects.EFCore.Data
{
    public partial class ProjectsContext : DbContext
    {
        public ProjectsContext()
        {
        }

        public ProjectsContext(DbContextOptions<ProjectsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CityForecast> CityForecasts { get; set; }
        public virtual DbSet<CitySearchZip> CitySearchZips { get; set; }
        public virtual DbSet<CitySearchZipView> CitySearchZipViews { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CityForecast>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("CityForecast");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Json).HasColumnName("JSON");

                entity.Property(e => e.RecordEntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordLastDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Live')");
            });

            modelBuilder.Entity<CitySearchZip>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("CitySearchZip");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Json).HasColumnName("JSON");

                entity.Property(e => e.RecordEntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordLastDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Live')");
            });

            modelBuilder.Entity<CitySearchZipView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CitySearchZip_View");

                entity.Property(e => e.City).HasMaxLength(4000);

                entity.Property(e => e.CityKey).HasMaxLength(4000);

                entity.Property(e => e.CountryCode).HasMaxLength(4000);

                entity.Property(e => e.GeoPositionLatitude).HasMaxLength(4000);

                entity.Property(e => e.GeoPositionLongitude).HasMaxLength(4000);

                entity.Property(e => e.RecordEntryDate).HasColumnType("datetime");

                entity.Property(e => e.RecordId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RecordID");

                entity.Property(e => e.RecordLastDate).HasColumnType("datetime");

                entity.Property(e => e.RecordStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegionCode).HasMaxLength(4000);

                entity.Property(e => e.StateCode).HasMaxLength(4000);

                entity.Property(e => e.ZipCode).HasMaxLength(4000);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
