namespace theRightDirection.PDOKGeocoder;

public record Response
(
    int numFound,
    int start,
    double maxScore,
    bool numFoundExact,
    IReadOnlyList<GeocodeResult> docs
);