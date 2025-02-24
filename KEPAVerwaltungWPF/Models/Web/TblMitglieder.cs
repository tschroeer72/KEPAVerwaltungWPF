using System;
using System.Collections.Generic;

namespace KEPAVerwaltungWPF.Models.Web;

public partial class TblMitglieder
{
    public int Id { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public string? Spitzname { get; set; }

    public string? Straße { get; set; }

    public string? Plz { get; set; }

    public string? Ort { get; set; }

    public DateTime? Geburtsdatum { get; set; }

    public DateTime MitgliedSeit { get; set; }

    public DateTime? PassivSeit { get; set; }

    public DateTime? AusgeschiedenAm { get; set; }

    public ulong Ehemaliger { get; set; }

    public string? Notizen { get; set; }

    public string? Bemerkungen { get; set; }

    public string? Anrede { get; set; }

    public string? Email { get; set; }

    public string? TelefonPrivat { get; set; }

    public string? TelefonFirma { get; set; }

    public string? TelefonMobil { get; set; }

    public string? Fax { get; set; }

    public int? SpAnz { get; set; }

    public int? SpGew { get; set; }

    public int? SpUn { get; set; }

    public int? SpVerl { get; set; }

    public int? HolzGes { get; set; }

    public int? HolzMax { get; set; }

    public int? HolzMin { get; set; }

    public int? Punkte { get; set; }

    public string? Platz { get; set; }

    public int? TurboDbnummer { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Tbl9erRatten> Tbl9erRattens { get; set; } = new List<Tbl9erRatten>();

    public virtual ICollection<TblSpiel6TageRennen> TblSpiel6TageRennenSpielerId1Navigations { get; set; } = new List<TblSpiel6TageRennen>();

    public virtual ICollection<TblSpiel6TageRennen> TblSpiel6TageRennenSpielerId2Navigations { get; set; } = new List<TblSpiel6TageRennen>();

    public virtual ICollection<TblSpielBlitztunier> TblSpielBlitztunierSpielerId1Navigations { get; set; } = new List<TblSpielBlitztunier>();

    public virtual ICollection<TblSpielBlitztunier> TblSpielBlitztunierSpielerId2Navigations { get; set; } = new List<TblSpielBlitztunier>();

    public virtual ICollection<TblSpielKombimeisterschaft> TblSpielKombimeisterschaftSpielerId1Navigations { get; set; } = new List<TblSpielKombimeisterschaft>();

    public virtual ICollection<TblSpielKombimeisterschaft> TblSpielKombimeisterschaftSpielerId2Navigations { get; set; } = new List<TblSpielKombimeisterschaft>();

    public virtual ICollection<TblSpielMeisterschaft> TblSpielMeisterschaftSpielerId1Navigations { get; set; } = new List<TblSpielMeisterschaft>();

    public virtual ICollection<TblSpielMeisterschaft> TblSpielMeisterschaftSpielerId2Navigations { get; set; } = new List<TblSpielMeisterschaft>();

    public virtual ICollection<TblSpielPokal> TblSpielPokals { get; set; } = new List<TblSpielPokal>();

    public virtual ICollection<TblSpielSargKegeln> TblSpielSargKegelns { get; set; } = new List<TblSpielSargKegeln>();

    public virtual ICollection<TblTeilnehmer> TblTeilnehmers { get; set; } = new List<TblTeilnehmer>();
}
