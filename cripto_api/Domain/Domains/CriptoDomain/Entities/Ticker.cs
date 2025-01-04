using Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Tickers")]
public class Ticker : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Kripto para birimiyle ilişki
    [ForeignKey("Crypto")]
    public int CryptoId { get; set; }
    public virtual Crypto Crypto { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    [MaxLength(20)]
    public string TargetCurrency { get; set; } = "USD";

    public decimal High24H { get; set; }
    public decimal Low24H { get; set; }
    public decimal Candle { get; set; }
    public long TimeStamp { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public float Volume { get; set; }
}
