using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClinicManagementDataAccess.Models;

public partial class Entities : DbContext
{
    public Entities()
    {
    }

    public Entities(DbContextOptions<Entities> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile("appsettings.json")
          .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    public virtual DbSet<Disease> Disease { get; set; }

    public virtual DbSet<Doctor> Doctor { get; set; }

    public virtual DbSet<Patient> Patient { get; set; }

    public virtual DbSet<Visit> Visit { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Disease__3214EC073667B9C4");

            entity.Property(e => e.CreateDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiseaseDetail)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.DiseaseName)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Visit).WithMany(p => p.Disease)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK__Disease__VisitId__38996AB5");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctor__3214EC0792D8F8EB");

            entity.Property(e => e.CreateDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient__3214EC07D0667887");

            entity.Property(e => e.Address)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.CreateDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DoB).HasColumnType("date");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Visit__3214EC0746D16EF0");

            entity.Property(e => e.CreateDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VisitDetail)
                .HasMaxLength(512)
                .IsUnicode(false);

            entity.HasOne(d => d.Doctor).WithMany(p => p.Visit)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Visit__DoctorId__2C3393D0");

            entity.HasOne(d => d.Patient).WithMany(p => p.Visit)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Visit__PatientId__35BCFE0A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
