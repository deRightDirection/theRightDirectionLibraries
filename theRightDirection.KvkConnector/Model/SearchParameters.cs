using HR.KvkConnector.Model.Zoeken;
using Refit;

namespace theRightDirection.KvKConnector.Model;
public class SearchParameters
{
    /// <summary>
    /// Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.
    /// </summary>
    [AliasAs("kvkNummer")]
    public string KvkNummer { get; set; }

    /// <summary>
    /// Rechtspersonen Samenwerkingsverbanden Informatie Nummer.
    /// </summary>
    [AliasAs("rsin")]
    public string Rsin { get; set; }

    /// <summary>
    /// Vestigingsnummer: uniek nummer dat bestaat uit 12 cijfers.
    /// </summary>
    [AliasAs("vestigingsnummer")]
    public string Vestigingsnummer { get; set; }

    /// <summary>
    /// De naam waaronder een vestiging of rechtspersoon handelt.
    /// </summary>
    [AliasAs("naam")]
    public string Handelsnaam { get; set; }

    [AliasAs("straatnaam")]
    public string Straatnaam { get; set; }

    /// <summary>
    /// Mag alleen in combinatie met Postcode gezocht worden.
    /// </summary>
    [AliasAs("huisnummer")]
    public string Huisnummer { get; set; }

    /// <summary>
    /// Mag alleen in combinatie met Postcode gezocht worden.
    /// </summary>
    [AliasAs("huisletter")]
    public string Huisletter { get; set; }

    /// <summary>
    /// Mag alleen in combinatie met Huisnummer gezocht worden.
    /// </summary>
    [AliasAs("postcode")]
    public string Postcode { get; set; }

    [AliasAs("postbusnummer")]
    public string PostbusNummer { get; set; }

    [AliasAs("plaats")]
    public string Plaats { get; set; }

    /// <summary>
    /// Filter op type: hoofdvestiging, nevenvestiging en/of rechtspersoon.
    /// </summary>
    public Vestigingstype? Type { get; set; }

    [AliasAs("type")]
    protected string TypeString
    {
        get => Type?.GetStringValue();
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                Type = null;
            }
            else
            {
                Type = Enum.GetValues(typeof(Vestigingstype))
                    .Cast<Vestigingstype?>()
                    .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
            }
        }
    }

    /// <summary>
    /// Inclusief inactieve registraties: true, false.
    /// </summary>
    [AliasAs("inclusiefInactieveRegistraties")]
    public bool? InclusiefInactieveRegistraties { get; set; }

    /// <summary>
    /// Paginanummer, minimaal 1 en maximaal 1000.
    /// Default value: 1
    /// </summary>
    [AliasAs("pagina")]
    public int? Pagina { get; set; } = 1;

    /// <summary>
    /// Kies het aantal resultaten per pagina, minimaal 1 en maximaal 100.
    /// Default value: 10
    /// </summary>
    [AliasAs("resultatenPerPagina")]
    public int? Aantal { get; set; } = 100;
}