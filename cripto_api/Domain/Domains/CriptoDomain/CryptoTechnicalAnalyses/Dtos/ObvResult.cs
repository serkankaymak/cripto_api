
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses.Dtos
;

public class ObvResult : ITechnicalResult, IDto
{
    public DateTime Date { get; set; }
    public double ObvValue { get; set; }
}



public class ObvIndicator
{
    public List<ObvResult> results { get; protected set; }

    public ObvIndicator(List<ObvResult> results)
    {
        this.results = results;
    }
}
