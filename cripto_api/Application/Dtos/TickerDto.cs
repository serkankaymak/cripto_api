using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class TickerDto : IDto
{
    public int CryptoId { get; set; }
    public string CryptoName { get; set; }
    public decimal Price { get; set; }
    public string TargetCurrency { get; set; } = "USD";
    public decimal High24H { get; set; }
    public decimal Low24H { get; set; }
    public decimal Candle { get; set; }
    public long TimeStamp { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
}
