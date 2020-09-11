using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Asiakkaiden_Tilaukset.Models
{
    public partial class MyyntiDBContext : DbContext
    {
        public MyyntiDBContext()
        {
        }

        public MyyntiDBContext(DbContextOptions<MyyntiDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asiakas> Asiakas { get; set; }
        public virtual DbSet<Kommentti> Kommentti { get; set; }
        public virtual DbSet<Tilaus> Tilaus { get; set; }
        public virtual DbSet<TilausRivi> TilausRivi { get; set; }
        public virtual DbSet<Tuote> Tuote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=DESKTOP-F5NJUA7\\MSSQLSERVER01;database=MyyntiDB;trusted_connection=true;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asiakas>(entity =>
            {
                entity.Property(e => e.AsiakasId)
                    .HasColumnName("asiakas_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Etunimi)
                    .HasColumnName("etunimi")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Luottoraja).HasColumnName("luottoraja");

                entity.Property(e => e.Osoite)
                    .HasColumnName("osoite")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Postinumero)
                    .HasColumnName("postinumero")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Postitoimipaikka)
                    .HasColumnName("postitoimipaikka")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sukunimi)
                    .HasColumnName("sukunimi")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Syntymäaika)
                    .HasColumnName("syntymäaika")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Kommentti>(entity =>
            {
                entity.Property(e => e.KommenttiId)
                    .HasColumnName("Kommentti_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Kommenttiteksti)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LuontiPvm)
                    .HasColumnName("Luonti_pvm")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nimi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Otsikko)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.KommenttiNavigation)
                    .WithOne(p => p.Kommentti)
                    .HasForeignKey<Kommentti>(d => d.KommenttiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tuote_kommentti");
            });

            modelBuilder.Entity<Tilaus>(entity =>
            {
                entity.Property(e => e.TilausId)
                    .HasColumnName("tilaus_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AsiakasId).HasColumnName("asiakas_id");

                entity.Property(e => e.TilausPvm)
                    .HasColumnName("tilausPvm")
                    .HasColumnType("date");

                entity.Property(e => e.Tilaussumma)
                    .HasColumnName("tilaussumma")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ToimitusPvm)
                    .HasColumnName("toimitusPvm")
                    .HasColumnType("date");

                entity.HasOne(d => d.Asiakas)
                    .WithMany(p => p.Tilaus)
                    .HasForeignKey(d => d.AsiakasId)
                    .HasConstraintName("FK_tilaus_asiakas");
            });

            modelBuilder.Entity<TilausRivi>(entity =>
            {
                entity.HasKey(e => new { e.TilausId, e.Rivinro });

                entity.Property(e => e.TilausId).HasColumnName("tilaus_id");

                entity.Property(e => e.Rivinro).HasColumnName("rivinro");

                entity.Property(e => e.Ahinta)
                    .HasColumnName("ahinta")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Alennus)
                    .HasColumnName("alennus")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.TilausLkm).HasColumnName("tilausLkm");

                entity.Property(e => e.TuoteId).HasColumnName("tuote_id");

                entity.HasOne(d => d.Tilaus)
                    .WithMany(p => p.TilausRivi)
                    .HasForeignKey(d => d.TilausId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TilausRivi_Tilaus");

                entity.HasOne(d => d.Tuote)
                    .WithMany(p => p.TilausRivi)
                    .HasForeignKey(d => d.TuoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TilausRivi_Tuote");
            });

            modelBuilder.Entity<Tuote>(entity =>
            {
                entity.Property(e => e.TuoteId)
                    .HasColumnName("tuote_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Hinta)
                    .HasColumnName("hinta")
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Nimi)
                    .HasColumnName("nimi")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tuoteryhmä)
                    .HasColumnName("tuoteryhmä")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tyyppi)
                    .HasColumnName("tyyppi")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
