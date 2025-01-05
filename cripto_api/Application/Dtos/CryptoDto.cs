using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class CryptoDto : IDto
{
    public List<TickerDto> tickers { get; set; } = new List<TickerDto>();

    public required string Symbol { get; set; } // Örneğin, "BTC", "ETH"
    public required string Name { get; set; } // Örneğin, "Bitcoin", "Ethereum"
    public required string Image { get; set; }
    public string TargetCurrency { get; set; } = "USD";
    public decimal Price { get; set; }
    #region ath
    public decimal? ath { get; set; }
    public decimal? ath_change_percentage { get; set; }
    public DateTime? ath_date { get; set; }
    #endregion

    #region atl
    public decimal? atl { get; set; }
    public decimal? atl_change_percentage { get; set; }
    public DateTime? atl_date { get; set; }
    #endregion

    #region info
    public decimal? market_cap { get; set; }
    public decimal? circulating_supply { get; set; }
    public decimal? total_supply { get; set; }
    public decimal? max_supply { get; set; }
    #endregion

}
