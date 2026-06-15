using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SossePizzerija_API.Models;

public partial class SossePizzerijaContext : DbContext
{
    public SossePizzerijaContext()
    {
    }

    public SossePizzerijaContext(DbContextOptions<SossePizzerijaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomPizzaSastojci> CustomPizzaSastojcis { get; set; }

    public virtual DbSet<CustomPizze> CustomPizzes { get; set; }

    public virtual DbSet<Korisnici> Korisnicis { get; set; }

    public virtual DbSet<Narudzbe> Narudzbes { get; set; }

    public virtual DbSet<Pizze> Pizzes { get; set; }

    public virtual DbSet<Sastojci> Sastojcis { get; set; }

    public virtual DbSet<StavkeNarudzbe> StavkeNarudzbes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=SossePizzerija;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomPizzaSastojci>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CustomPi__3214EC0756712990");

            entity.ToTable("CustomPizzaSastojci");

            entity.HasOne(d => d.CustomPizza).WithMany(p => p.CustomPizzaSastojcis)
                .HasForeignKey(d => d.CustomPizzaId)
                .HasConstraintName("FK__CustomPiz__Custo__5FB337D6");

            entity.HasOne(d => d.Sastojak).WithMany(p => p.CustomPizzaSastojcis)
                .HasForeignKey(d => d.SastojakId)
                .HasConstraintName("FK__CustomPiz__Sasto__60A75C0F");
        });

        modelBuilder.Entity<CustomPizze>(entity =>
        {
            entity.HasKey(e => e.CustomPizzaId).HasName("PK__CustomPi__A0B14B3DFB98FA38");

            entity.ToTable("CustomPizze");

            entity.Property(e => e.DatumKreiranja)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Naziv).HasMaxLength(100);
            entity.Property(e => e.Sos).HasMaxLength(50);
            entity.Property(e => e.Tijesto).HasMaxLength(50);
            entity.Property(e => e.UkupnaCijena).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Velicina).HasMaxLength(20);

            entity.HasOne(d => d.Korisnik).WithMany(p => p.CustomPizzes)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__CustomPiz__Koris__5BE2A6F2");
        });

        modelBuilder.Entity<Korisnici>(entity =>
        {
            entity.HasKey(e => e.KorisnikId).HasName("PK__Korisnic__80B06D416C603EE2");

            entity.ToTable("Korisnici");

            entity.HasIndex(e => e.Username, "UQ__Korisnic__536C85E4F1738136").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Korisnic__A9D10534899B8529").IsUnique();

            entity.Property(e => e.DatumRegistracije)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Ime).HasMaxLength(50);
            entity.Property(e => e.Lozinka).HasMaxLength(100);
            entity.Property(e => e.Prezime).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Narudzbe>(entity =>
        {
            entity.HasKey(e => e.NarudzbаId).HasName("PK__Narudzbe__1584F021F6AE2F41");

            entity.ToTable("Narudzbe");

            entity.Property(e => e.DatumNarudzbe)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Na cekanju");
            entity.Property(e => e.UkupnaCijena).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Narudzbes)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Narudzbe__Korisn__534D60F1");
        });

        modelBuilder.Entity<Pizze>(entity =>
        {
            entity.HasKey(e => e.PizzaId).HasName("PK__Pizze__0B6012DDF441ACB8");

            entity.ToTable("Pizze");

            entity.Property(e => e.Cijena).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Naziv).HasMaxLength(100);
            entity.Property(e => e.Opis).HasMaxLength(255);
            entity.Property(e => e.Slika).HasMaxLength(255);
        });

        modelBuilder.Entity<Sastojci>(entity =>
        {
            entity.HasKey(e => e.SastojakId).HasName("PK__Sastojci__114FC27F8FB74C76");

            entity.ToTable("Sastojci");

            entity.Property(e => e.Cijena).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Naziv).HasMaxLength(100);
        });

        modelBuilder.Entity<StavkeNarudzbe>(entity =>
        {
            entity.HasKey(e => e.StavkaId).HasName("PK__StavkeNa__ACCAA893934300D3");

            entity.ToTable("StavkeNarudzbe");

            entity.Property(e => e.Cijena).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Narudzbа).WithMany(p => p.StavkeNarudzbes)
                .HasForeignKey(d => d.NarudzbаId)
                .HasConstraintName("FK__StavkeNar__Narud__5812160E");

            entity.HasOne(d => d.Pizza).WithMany(p => p.StavkeNarudzbes)
                .HasForeignKey(d => d.PizzaId)
                .HasConstraintName("FK__StavkeNar__Pizza__59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
