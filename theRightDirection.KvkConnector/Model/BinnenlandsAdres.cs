using HR.KvkConnector.Model;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace theRightDirection.KvKConnector.Model;
public record BinnenlandsAdres
{  /// <summary>
   /// Correspondentieadres en/of bezoekadres.
   /// </summary>
    public Adrestype? Type { get; set; }

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
                Type = Enum.GetValues(typeof(Adrestype))
                    .Cast<Adrestype?>()
                    .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
            }
        }
    }

    /// <summary>
    /// Indicatie of het adres is afgeschermd.
    /// </summary>
    public JaNeeIndicatie? IndAfgeschermd { get; set; }

    [DataMember(Name = "indAfgeschermd")]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    protected string IndAfgeschermdString
    {
        get => IndAfgeschermd?.GetStringValue();
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                IndAfgeschermd = null;
            }
            else
            {
                IndAfgeschermd = Enum.GetValues(typeof(JaNeeIndicatie))
                    .Cast<JaNeeIndicatie?>()
                    .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
            }
        }
    }

    public string VolledigAdres { get; set; }

    public string Straatnaam { get; set; }

    public int? Huisnummer { get; set; }

    public string HuisnummerToevoeging { get; set; }

    public string Huisletter { get; set; }

    public string AanduidingBijHuisnummer { get; set; }

    public string ToevoegingAdres { get; set; }

    public string Postcode { get; set; }

    public int? Postbusnummer { get; set; }

    public string Plaats { get; set; }

    public string StraatHuisnummer { get; set; }

    public string PostcodeWoonplaats { get; set; }

    public string Regio { get; set; }

    public string Land { get; set; }

    /// <summary>
    /// Basisregistratie Adressen en Gebouwen gegevens uit het kadaster.
    /// </summary>
    public GeoData GeoData { get; set; }
}
