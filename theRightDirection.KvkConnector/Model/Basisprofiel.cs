using HR.KvkConnector.Model;
using System.Text.Json.Serialization;

namespace theRightDirection.KvKConnector.Model;

public class Basisprofiel
{
    /// <summary>
    /// Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.
    /// </summary>
    public string KvkNummer { get; set; }

    ///// <summary>
    ///// Hiermee geeft de onderneming aan geen ongevraagde reclame per post of verkoop aan de deur te willen ontvangen.
    ///// </summary>
    //public JaNeeIndicatie? IndNonMailing { get; set; }
    //public string IndNonMailingString { get; set; }

    /// <summary>
    /// Naam onder Maatschappelijke Activiteit.
    /// </summary>
    //    public string Naam { get; set; }

    ///// <summary>
    ///// Registratiedatum onderneming in HR.
    ///// </summary>
    //public DateTime? FormeleRegistratiedatum { get; set; }

    //public MaterieleRegistratie MaterieleRegistratie { get; set; }

    /// <summary>
    /// Totaal aantal werkzame personen.
    /// </summary>
    //    public int? TotaalWerkzamePersonen { get; set; }

    /// <summary>
    /// De naam van de onderneming wanneer er statuten geregistreerd zijn.
    /// </summary>
    //    public string StatutaireNaam { get; set; }

    /// <summary>
    /// Alle namen waaronder een onderneming of vestiging handelt (op volgorde van registreren).
    /// </summary>
    public IEnumerable<Handelsnaam> Handelsnamen { get; set; } = Enumerable.Empty<Handelsnaam>();

    /// <summary>
    /// Code beschrijving van SBI activiteiten conform SBI 2008 (Standard Industrial Classification). 
    /// Er wordt geen maximering toegepast in de resultaten. Zie ook KVK.nl/sbi
    /// </summary>
    // public IEnumerable<SbiActiviteit> SbiActiviteiten { get; set; } = Enumerable.Empty<SbiActiviteit>();
    [JsonPropertyName("_embedded")]
    public EmbeddedContainer Embedded { get; set; }
}