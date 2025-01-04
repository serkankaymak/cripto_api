namespace Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses.Dtos
;
public class BollingerBandsResult : ITechnicalResult, IDto
{
    public DateTime Date { get; set; }
    public double Upper { get; set; }
    public double Middle { get; set; }
    public double Lower { get; set; }
}

public class BollingerBandIndicator
{
    public List<BollingerBandsResult> results { get; protected set; }

    public BollingerBandIndicator(List<BollingerBandsResult> results)
    {
        this.results = results;
    }
}
