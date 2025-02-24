using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KEPAVerwaltungWPF.Models.Local;

public partial class LocalDbContext : DbContext
{
    public LocalDbContext()
    {
    }

    public LocalDbContext(DbContextOptions<LocalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tbl9erRatten> Tbl9erRattens { get; set; }

    public virtual DbSet<TblDbchangeLog> TblDbchangeLogs { get; set; }

    public virtual DbSet<TblMeisterschaften> TblMeisterschaftens { get; set; }

    public virtual DbSet<TblMeisterschaftstyp> TblMeisterschaftstyps { get; set; }

    public virtual DbSet<TblMitglieder> TblMitglieders { get; set; }

    public virtual DbSet<TblSetting> TblSettings { get; set; }

    public virtual DbSet<TblSpiel6TageRennen> TblSpiel6TageRennens { get; set; }

    public virtual DbSet<TblSpielBlitztunier> TblSpielBlitztuniers { get; set; }

    public virtual DbSet<TblSpielKombimeisterschaft> TblSpielKombimeisterschafts { get; set; }

    public virtual DbSet<TblSpielMeisterschaft> TblSpielMeisterschafts { get; set; }

    public virtual DbSet<TblSpielPokal> TblSpielPokals { get; set; }

    public virtual DbSet<TblSpielSargKegeln> TblSpielSargKegelns { get; set; }

    public virtual DbSet<TblSpieltag> TblSpieltags { get; set; }

    public virtual DbSet<TblTeilnehmer> TblTeilnehmers { get; set; }

    public virtual DbSet<Vw9erRatten> Vw9erRattens { get; set; }

    public virtual DbSet<VwErgebnisKombimeisterschaft> VwErgebnisKombimeisterschafts { get; set; }

    public virtual DbSet<VwErgebnisKurztunier> VwErgebnisKurztuniers { get; set; }

    public virtual DbSet<VwErgebnisMeisterschaft> VwErgebnisMeisterschafts { get; set; }

    public virtual DbSet<VwSpiel6TageRennen> VwSpiel6TageRennens { get; set; }

    public virtual DbSet<VwSpielBlitztunier> VwSpielBlitztuniers { get; set; }

    public virtual DbSet<VwSpielKombimeisterschaft> VwSpielKombimeisterschafts { get; set; }

    public virtual DbSet<VwSpielMeisterschaft> VwSpielMeisterschafts { get; set; }

    public virtual DbSet<VwSpielPokal> VwSpielPokals { get; set; }

    public virtual DbSet<VwSpielSargKegeln> VwSpielSargKegelns { get; set; }

    public virtual DbSet<VwStatistik6TageRennen> VwStatistik6TageRennens { get; set; }

    public virtual DbSet<VwStatistik6TageRennenSpieler1> VwStatistik6TageRennenSpieler1s { get; set; }

    public virtual DbSet<VwStatistik6TageRennenSpieler2> VwStatistik6TageRennenSpieler2s { get; set; }

    public virtual DbSet<VwStatistik9er> VwStatistik9ers { get; set; }

    public virtual DbSet<VwStatistik9erRatten> VwStatistik9erRattens { get; set; }

    public virtual DbSet<VwStatistik9erRattenBak> VwStatistik9erRattenBaks { get; set; }

    public virtual DbSet<VwStatistikPokal> VwStatistikPokals { get; set; }

    public virtual DbSet<VwStatistikRatten> VwStatistikRattens { get; set; }

    public virtual DbSet<VwStatistikSarg> VwStatistikSargs { get; set; }

    public virtual DbSet<VwStatistikSpieler> VwStatistikSpielers { get; set; }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;attachdbfilename=|DataDirectory|\\KEPAVerwaltung.mdf;Integrated Security=true;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tbl9erRatten>(entity =>
        {
            entity.ToTable("tbl9erRatten");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");

            entity.HasOne(d => d.Spieler).WithMany(p => p.Tbl9erRattens)
                .HasForeignKey(d => d.SpielerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl9erRatten_tblMitglieder");

            entity.HasOne(d => d.Spieltag).WithMany(p => p.Tbl9erRattens)
                .HasForeignKey(d => d.SpieltagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl9erRatten_tblSpieltag");
        });

        modelBuilder.Entity<TblDbchangeLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tblDBChangeLog");

            entity.ToTable("tblDBChangeLog");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Zeitstempel).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblMeisterschaften>(entity =>
        {
            entity.ToTable("tblMeisterschaften");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Beginn).HasColumnType("datetime");
            entity.Property(e => e.Bezeichnung).HasMaxLength(100);
            entity.Property(e => e.Ende).HasColumnType("datetime");
            entity.Property(e => e.MeisterschaftstypId).HasColumnName("MeisterschaftstypID");
            entity.Property(e => e.TurboDbnummer).HasColumnName("TurboDBNummer");

            entity.HasOne(d => d.Meisterschaftstyp).WithMany(p => p.TblMeisterschaftens)
                .HasForeignKey(d => d.MeisterschaftstypId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblMeisterschaften_tblMeisterschaftstyp");
        });

        modelBuilder.Entity<TblMeisterschaftstyp>(entity =>
        {
            entity.ToTable("tblMeisterschaftstyp");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Meisterschaftstyp).HasMaxLength(100);
        });

        modelBuilder.Entity<TblMitglieder>(entity =>
        {
            entity.ToTable("tblMitglieder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Anrede).HasMaxLength(50);
            entity.Property(e => e.AusgeschiedenAm).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMail");
            entity.Property(e => e.Fax).HasMaxLength(50);
            entity.Property(e => e.Geburtsdatum).HasColumnType("datetime");
            entity.Property(e => e.MitgliedSeit).HasColumnType("datetime");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.Ort).HasMaxLength(50);
            entity.Property(e => e.PassivSeit).HasColumnType("datetime");
            entity.Property(e => e.Plz)
                .HasMaxLength(5)
                .HasColumnName("PLZ");
            entity.Property(e => e.Punkte).HasMaxLength(50);
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Straße).HasMaxLength(50);
            entity.Property(e => e.TelefonFirma).HasMaxLength(50);
            entity.Property(e => e.TelefonMobil).HasMaxLength(50);
            entity.Property(e => e.TelefonPrivat).HasMaxLength(50);
            entity.Property(e => e.TurboDbnummer).HasColumnName("TurboDBNummer");
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<TblSetting>(entity =>
        {
            entity.ToTable("tblSettings");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Computername).HasMaxLength(50);
            entity.Property(e => e.Parametername).HasMaxLength(100);
            entity.Property(e => e.Parameterwert).HasMaxLength(4000);
        });

        modelBuilder.Entity<TblSpiel6TageRennen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl6TageRennen");

            entity.ToTable("tblSpiel6TageRennen");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
            entity.Property(e => e.Spielnummer).HasDefaultValue(1);
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");

            entity.HasOne(d => d.SpielerId1Navigation).WithMany(p => p.TblSpiel6TageRennenSpielerId1Navigations)
                .HasForeignKey(d => d.SpielerId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpiel6TageRennen_tblMitglieder");

            entity.HasOne(d => d.SpielerId2Navigation).WithMany(p => p.TblSpiel6TageRennenSpielerId2Navigations)
                .HasForeignKey(d => d.SpielerId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpiel6TageRennen_tblMitglieder1");

            entity.HasOne(d => d.Spieltag).WithMany(p => p.TblSpiel6TageRennens)
                .HasForeignKey(d => d.SpieltagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpiel6TageRennen_tblSpieltag");
        });

        modelBuilder.Entity<TblSpielBlitztunier>(entity =>
        {
            entity.ToTable("tblSpielBlitztunier");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");

            entity.HasOne(d => d.SpielerId1Navigation).WithMany(p => p.TblSpielBlitztunierSpielerId1Navigations)
                .HasForeignKey(d => d.SpielerId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielBlitztunier_tblMitglieder");

            entity.HasOne(d => d.SpielerId2Navigation).WithMany(p => p.TblSpielBlitztunierSpielerId2Navigations)
                .HasForeignKey(d => d.SpielerId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielBlitztunier_tblMitglieder1");

            entity.HasOne(d => d.Spieltag).WithMany(p => p.TblSpielBlitztuniers)
                .HasForeignKey(d => d.SpieltagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielBlitztunier_tblSpieltag");
        });

        modelBuilder.Entity<TblSpielKombimeisterschaft>(entity =>
        {
            entity.ToTable("tblSpielKombimeisterschaft");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.HinRückrunde).HasComment("0 = Hinrunde; 1 = Rückrunde");
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");

            entity.HasOne(d => d.SpielerId1Navigation).WithMany(p => p.TblSpielKombimeisterschaftSpielerId1Navigations)
                .HasForeignKey(d => d.SpielerId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielKombimeisterschaft_tblMitglieder");

            entity.HasOne(d => d.SpielerId2Navigation).WithMany(p => p.TblSpielKombimeisterschaftSpielerId2Navigations)
                .HasForeignKey(d => d.SpielerId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielKombimeisterschaft_tblMitglieder1");

            entity.HasOne(d => d.Spieltag).WithMany(p => p.TblSpielKombimeisterschafts)
                .HasForeignKey(d => d.SpieltagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielKombimeisterschaft_tblSpieltag");
        });

        modelBuilder.Entity<TblSpielMeisterschaft>(entity =>
        {
            entity.ToTable("tblSpielMeisterschaft");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");

            entity.HasOne(d => d.SpielerId1Navigation).WithMany(p => p.TblSpielMeisterschaftSpielerId1Navigations)
                .HasForeignKey(d => d.SpielerId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielMeisterschaft_tblMitglieder");

            entity.HasOne(d => d.SpielerId2Navigation).WithMany(p => p.TblSpielMeisterschaftSpielerId2Navigations)
                .HasForeignKey(d => d.SpielerId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielMeisterschaft_tblMitglieder1");

            entity.HasOne(d => d.Spieltag).WithMany(p => p.TblSpielMeisterschafts)
                .HasForeignKey(d => d.SpieltagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielMeisterschaft_tblSpieltag");
        });

        modelBuilder.Entity<TblSpielPokal>(entity =>
        {
            entity.ToTable("tblSpielPokal");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");

            entity.HasOne(d => d.Spieler).WithMany(p => p.TblSpielPokals)
                .HasForeignKey(d => d.SpielerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielPokal_tblMitglieder");

            entity.HasOne(d => d.Spieltag).WithMany(p => p.TblSpielPokals)
                .HasForeignKey(d => d.SpieltagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielPokal_tblSpieltag");
        });

        modelBuilder.Entity<TblSpielSargKegeln>(entity =>
        {
            entity.ToTable("tblSpielSargKegeln");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");

            entity.HasOne(d => d.Spieler).WithMany(p => p.TblSpielSargKegelns)
                .HasForeignKey(d => d.SpielerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielSargKegeln_tblMitglieder");

            entity.HasOne(d => d.Spieltag).WithMany(p => p.TblSpielSargKegelns)
                .HasForeignKey(d => d.SpieltagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpielSargKegeln_tblSpieltag");
        });

        modelBuilder.Entity<TblSpieltag>(entity =>
        {
            entity.ToTable("tblSpieltag");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InBearbeitung).HasDefaultValue(true);
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");

            entity.HasOne(d => d.Meisterschafts).WithMany(p => p.TblSpieltags)
                .HasForeignKey(d => d.MeisterschaftsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSpieltag_tblMeisterschaften");
        });

        modelBuilder.Entity<TblTeilnehmer>(entity =>
        {
            entity.ToTable("tblTeilnehmer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");

            entity.HasOne(d => d.Meisterschafts).WithMany(p => p.TblTeilnehmers)
                .HasForeignKey(d => d.MeisterschaftsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblTeilnehmer_tblMeisterschaften");

            entity.HasOne(d => d.Spieler).WithMany(p => p.TblTeilnehmers)
                .HasForeignKey(d => d.SpielerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblTeilnehmer_tblMitglieder");
        });

        modelBuilder.Entity<Vw9erRatten>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw9erRatten");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
            entity.Property(e => e._9erRattenId).HasColumnName("9erRattenID");
        });

        modelBuilder.Entity<VwErgebnisKombimeisterschaft>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwErgebnisKombimeisterschaft");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Spieler1Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Vorname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Vorname).HasMaxLength(50);
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
        });

        modelBuilder.Entity<VwErgebnisKurztunier>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwErgebnisKurztunier");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Spieler1Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Vorname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Vorname).HasMaxLength(50);
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
        });

        modelBuilder.Entity<VwErgebnisMeisterschaft>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwErgebnisMeisterschaft");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Spieler1Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Vorname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Vorname).HasMaxLength(50);
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
        });

        modelBuilder.Entity<VwSpiel6TageRennen>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwSpiel6TageRennen");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Spieler1Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Vorname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Vorname).HasMaxLength(50);
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
            entity.Property(e => e._6tageRennenId).HasColumnName("6TageRennenID");
        });

        modelBuilder.Entity<VwSpielBlitztunier>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwSpielBlitztunier");

            entity.Property(e => e.BlitztunierId).HasColumnName("BlitztunierID");
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Spieler1Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Vorname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Vorname).HasMaxLength(50);
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
        });

        modelBuilder.Entity<VwSpielKombimeisterschaft>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwSpielKombimeisterschaft");

            entity.Property(e => e.KombimeisterschaftId).HasColumnName("KombimeisterschaftID");
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Spieler1Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Vorname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Vorname).HasMaxLength(50);
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
        });

        modelBuilder.Entity<VwSpielMeisterschaft>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwSpielMeisterschaft");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.SpielMeisterschaftId).HasColumnName("SpielMeisterschaftID");
            entity.Property(e => e.Spieler1Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler1Vorname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Nachname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Spitzname).HasMaxLength(50);
            entity.Property(e => e.Spieler2Vorname).HasMaxLength(50);
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
        });

        modelBuilder.Entity<VwSpielPokal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwSpielPokal");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielPokalId).HasColumnName("SpielPokalID");
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwSpielSargKegeln>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwSpielSargKegeln");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielSargKegelnId).HasColumnName("SpielSargKegelnID");
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwStatistik6TageRennen>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistik6TageRennen");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwStatistik6TageRennenSpieler1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistik6TageRennenSpieler1");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwStatistik6TageRennenSpieler2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistik6TageRennenSpieler2");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwStatistik9er>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistik9er");

            entity.Property(e => e.Beginn).HasColumnType("datetime");
            entity.Property(e => e.Bezeichnung).HasMaxLength(100);
            entity.Property(e => e.Ende).HasColumnType("datetime");
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwStatistik9erRatten>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistik9erRatten");

            entity.Property(e => e.Beginn).HasColumnType("datetime");
            entity.Property(e => e.Bezeichnung).HasMaxLength(100);
            entity.Property(e => e.Ende).HasColumnType("datetime");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Meisterschaftstyp).HasMaxLength(100);
            entity.Property(e => e.SpielerIdneunerkönig).HasColumnName("SpielerIDNeunerkönig");
            entity.Property(e => e.SpielerIdrattenorden).HasColumnName("SpielerIDRattenorden");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
        });

        modelBuilder.Entity<VwStatistik9erRattenBak>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistik9erRattenBAK");

            entity.Property(e => e.Beginn).HasColumnType("datetime");
            entity.Property(e => e.Bezeichnung).HasMaxLength(100);
            entity.Property(e => e.Ende).HasColumnType("datetime");
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Meisterschaftstyp).HasMaxLength(100);
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwStatistikPokal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistikPokal");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwStatistikRatten>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistikRatten");

            entity.Property(e => e.Beginn).HasColumnType("datetime");
            entity.Property(e => e.Bezeichnung).HasMaxLength(100);
            entity.Property(e => e.Ende).HasColumnType("datetime");
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwStatistikSarg>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistikSarg");

            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.SpielerId).HasColumnName("SpielerID");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
            entity.Property(e => e.SpieltagId).HasColumnName("SpieltagID");
            entity.Property(e => e.Spitzname).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<VwStatistikSpieler>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwStatistikSpieler");

            entity.Property(e => e.Bezeichnung).HasMaxLength(100);
            entity.Property(e => e.MeisterschaftsId).HasColumnName("MeisterschaftsID");
            entity.Property(e => e.Meisterschaftstyp).HasMaxLength(100);
            entity.Property(e => e.Spieler1).HasMaxLength(102);
            entity.Property(e => e.Spieler2).HasMaxLength(102);
            entity.Property(e => e.SpielerId1).HasColumnName("SpielerID1");
            entity.Property(e => e.SpielerId2).HasColumnName("SpielerID2");
            entity.Property(e => e.Spieltag).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
