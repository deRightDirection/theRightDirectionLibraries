using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;

public class Vestiging
{
    /// <summary>
    /// Vestigingsnummer: uniek nummer dat bestaat uit 12 cijfers.
    /// </summary>
//    public string Vestigingsnummer { get; set; }

    /// <summary>
    /// Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.
    /// </summary>
    [JsonPropertyName("kvkNummer")]
    public string KvkNummer { get; set; }

    /// <summary>
    /// De naam waaronder een onderneming of vestiging handelt.
    /// </summary>
    [JsonPropertyName("eersteHandelsnaam")]
    public string EersteHandelsnaam { get; set; }

    /// <summary>
    /// Rechtspersonen Samenwerkingsverbanden Informatie Nummer.
    /// </summary>
    [JsonPropertyName("rsin")]
    public string Rsin { get; set; }

    [JsonPropertyName("adressen")]
    public IEnumerable<Adres> Adressen { get; set; } = [];

    /// <summary>
    /// Hiermee geeft de onderneming aan geen ongevraagde reclame per post of verkoop aan de deur te willen ontvangen.
    /// </summary>
    //    public JaNeeIndicatie? IndNonMailing { get; set; }

    //protected string IndNonMailingString
    //{
    //    get => IndNonMailing?.GetStringValue();
    //    set
    //    {
    //        if (string.IsNullOrEmpty(value))
    //        {
    //            IndNonMailing = null;
    //        }
    //        else
    //        {
    //            IndNonMailing = Enum.GetValues(typeof(JaNeeIndicatie))
    //                .Cast<JaNeeIndicatie?>()
    //                .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
    //        }
    //    }
    //}

    /// <summary>
    /// Registratiedatum onderneming in HR.
    /// </summary>
    //    public DateTime? FormeleRegistratiedatum { get; set; }

    //    public MaterieleRegistratie MaterieleRegistratie { get; set; }


    /// <summary>
    /// Hoofdvestiging (Ja/Nee)
    /// </summary>
//    public JaNeeIndicatie? IndHoofdvestiging { get; set; }

    //protected string IndHoofdvestigingString
    //{
    //    get => IndHoofdvestiging?.GetStringValue();
    //    set
    //    {
    //        if (string.IsNullOrEmpty(value))
    //        {
    //            IndHoofdvestiging = null;
    //        }
    //        else
    //        {
    //            IndHoofdvestiging = Enum.GetValues(typeof(JaNeeIndicatie))
    //                .Cast<JaNeeIndicatie?>()
    //                .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
    //        }
    //    }
    //}

    /// <summary>
    /// Commerciele vestiging (Ja/Nee)
    /// </summary>
//    public JaNeeIndicatie? IndCommercieleVestiging { get; set; }

    //protected string IndCommercieleVestigingString
    //{
    //    get => IndCommercieleVestiging?.GetStringValue();
    //    set
    //    {
    //        if (string.IsNullOrEmpty(value))
    //        {
    //            IndCommercieleVestiging = null;
    //        }
    //        else
    //        {
    //            IndCommercieleVestiging = Enum.GetValues(typeof(JaNeeIndicatie))
    //                .Cast<JaNeeIndicatie?>()
    //                .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
    //        }
    //    }
    //}

    /// <summary>
    /// Aantal voltijd werkzame personen.
    /// </summary>
//    public int? VoltijdWerkzamePersonen { get; set; }

    /// <summary>
    /// Totaal aantal werkzame personen.
    /// </summary>
//    public int? TotaalWerkzamePersonen { get; set; }

    /// <summary>
    /// Aantal deeltijd werkzame personen.
    /// </summary>
//    public int? DeeltijdWerkzamePersonen { get; set; }


    //  public IEnumerable<string> Websites { get; set; } = Enumerable.Empty<string>();

    /// <summary>
    /// Code beschrijving van SBI activiteiten conform SBI 2008 (Standard Industrial Classification). 
    /// Er wordt geen maximering toegepast in de resultaten. Zie ook KVK.nl/sbi
    /// </summary>
    //    public IEnumerable<SbiActiviteit> SbiActiviteiten { get; set; } = Enumerable.Empty<SbiActiviteit>();

    //  public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();
}