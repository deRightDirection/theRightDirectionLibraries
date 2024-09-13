namespace theRightDirection.KvKConnector.Model;
public class GeoData
{
    /// <summary>
    /// Unieke BAG id
    /// </summary>
    public string AddresseerbaarObjectId { get; set; }

    /// <summary>
    /// Unieke BAG nummeraanduiding id.
    /// </summary>
    public string NummerAanduidingId { get; set; }

    /// <summary>
    /// Lengtegraad.
    /// </summary>
    public double? GpsLatitude { get; set; }

    /// <summary>
    /// Breedtegraad.
    /// </summary>
    public double? GpsLongitude { get; set; }

    /// <summary>
    /// Rijksdriehoek X-coördinaat.
    /// </summary>
    public double? RijksdriehoekX { get; set; }

    /// <summary>
    /// Rijksdriehoek Y-coördinaat.
    /// </summary>
    public double? RijksdriehoekY { get; set; }

    /// <summary>
    /// Rijksdriehoek Z-coördinaat.
    /// </summary>
    public double? RijksdriehoekZ { get; set; }
}
