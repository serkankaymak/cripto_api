using Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Cryptos")]
public class Crypto : IEntity
{
    public virtual ICollection<Ticker> Tickers { get; set; }
    public Crypto() { Tickers = new HashSet<Ticker>(); }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string Symbol { get; set; } // Örneğin, "BTC", "ETH"

    [MaxLength(100)]
    public string Name { get; set; } // Örneğin, "Bitcoin", "Ethereum"
    public string Image { get; set; }

    [MaxLength(20)]
    public string TargetCurrency { get; set; } = "USD";
    public decimal Price { get; set; }


    #region ath
    /// <summary>
    /// Tüm zamanların en yüksek fiyatı (ATH - All-Time High).
    /// </summary>
    public decimal? ath { get; set; }

    /// <summary>
    /// Güncel fiyatın, tüm zamanların en yüksek fiyatından yüzde olarak ne kadar düşük olduğu.
    /// </summary>
    public decimal? ath_change_percentage { get; set; }

    /// <summary>
    /// Kripto para biriminin tüm zamanların en yüksek fiyatına ulaştığı tarih.
    /// </summary>
    public DateTime? ath_date { get; set; }


    #endregion

    #region atl
    /// <summary>
    /// Tüm zamanların en düşük fiyatı (ATL - All-Time Low).
    /// </summary>

    public decimal? atl { get; set; }

    /// <summary>
    /// Güncel fiyatın, tüm zamanların en düşük fiyatından yüzde olarak ne kadar yüksek olduğu.
    /// </summary>
    public decimal? atl_change_percentage { get; set; }

    /// <summary>
    /// Kripto para biriminin tüm zamanların en düşük fiyatına ulaştığı tarih.
    /// </summary>
    public DateTime? atl_date { get; set; }

    #endregion

    #region info

    /// <summary>
    /// Piyasa değeri: Kripto para biriminin toplam dolaşımdaki arzının güncel fiyatla çarpılmasıyla elde edilir.
    /// </summary>
    public decimal? market_cap { get; set; }

    /// <summary>
    /// Dolaşımdaki toplam kripto para birimi sayısı.
    /// </summary>
    public decimal? circulating_supply { get; set; }

    /// <summary>
    /// Toplam arz: Üretilmiş toplam kripto para birimi sayısı (nullable).
    /// </summary>
    public decimal? total_supply { get; set; }

    /// <summary>
    /// Maksimum arz: Kripto para biriminin üretilebilecek maksimum coin sayısı (nullable).
    /// </summary>
    public decimal? max_supply { get; set; }


    #endregion






}
