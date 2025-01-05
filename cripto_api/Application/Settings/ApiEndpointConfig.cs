using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Settings;

public class ApiEndpointConfig
{
    /**
      "host": "https://api.coingecko.com/",
      "route": "api/v3/coins/markets",
      "query": "?vs_currency=",
      "targetCurrency": "usd",
      "secret": "CG-kLDwuFqeuEr2qVB7C7EdMAgz"
     */
    public required string Host { get; set; }
    public required string Route { get; set; }
    public required string Query { get; set; }
    public required string TargetCurrency { get; set; }
    public required string Secret { get; set; }

}