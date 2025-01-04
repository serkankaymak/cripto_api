
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses.Dtos
;
public class RsiResult : ITechnicalResult, IDto
{
    public DateTime Date { get; set; }
    public double RsiValue { get; set; }
}



public class RsiIndicator
{
    public List<RsiResult> results { get; protected set; }

    public RsiIndicator(List<RsiResult> results)
    {
        this.results = results;
    }
}
