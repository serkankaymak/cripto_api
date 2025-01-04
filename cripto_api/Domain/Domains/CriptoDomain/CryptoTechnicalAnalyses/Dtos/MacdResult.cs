
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses.Dtos
;
public class MacdResult : ITechnicalResult, IDto
{
    public DateTime Date { get; set; }
    public double Macd { get; set; }
    public double Signal { get; set; }
    public double Hist { get; set; }
}


public class MacdIndicator
{
    public List<MacdResult> results { get; protected set; }

    public MacdIndicator(List<MacdResult> results)
    {
        this.results = results;
    }
}
