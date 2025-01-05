using System;

namespace Application.Services.InternalServices.CryptoHttpRequestService
{
    public class CryptoData
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public decimal current_price { get; set; }

        /// <summary>
        /// Piyasa değeri: Kripto para biriminin toplam dolaşımdaki arzının güncel fiyatla çarpılmasıyla elde edilir.
        /// </summary>
        public decimal market_cap { get; set; }

        /// <summary>
        /// Piyasa değerine göre kripto para biriminin sıralaması.
        /// </summary>
        public int market_cap_rank { get; set; }

        /// <summary>
        /// Maksimum arzın tamamı dolaşıma girdiğinde kripto para biriminin piyasa değeri (nullable).
        /// </summary>
        public decimal? fully_diluted_valuation { get; set; }

        /// <summary>
        /// Son 24 saatte kripto para birimiyle yapılan toplam işlem hacmi.
        /// </summary>
        public decimal total_volume { get; set; }
        public decimal high_24h { get; set; }
        public decimal low_24h { get; set; }

        /// <summary>
        /// Son 24 saat içinde fiyat değişimi (miktar olarak).
        /// </summary>
        public decimal price_change_24h { get; set; }

        /// <summary>
        /// Son 24 saat içinde fiyat değişiminin yüzde olarak ifadesi.
        /// </summary>
        public decimal price_change_percentage_24h { get; set; }

        /// <summary>
        /// Piyasa değerinin son 24 saat içinde miktar olarak değişimi.
        /// </summary>
        public decimal market_cap_change_24h { get; set; }

        /// <summary>
        /// Piyasa değerindeki değişimin son 24 saatte yüzde olarak ifadesi.
        /// </summary>
        public decimal market_cap_change_percentage_24h { get; set; }

        /// <summary>
        /// Dolaşımdaki toplam kripto para birimi sayısı.
        /// </summary>
        public decimal circulating_supply { get; set; }

        /// <summary>
        /// Toplam arz: Üretilmiş toplam kripto para birimi sayısı (nullable).
        /// </summary>
        public decimal? total_supply { get; set; }

        /// <summary>
        /// Maksimum arz: Kripto para biriminin üretilebilecek maksimum coin sayısı (nullable).
        /// </summary>
        public decimal? max_supply { get; set; }

        /// <summary>
        /// Tüm zamanların en yüksek fiyatı (ATH - All-Time High).
        /// </summary>
        public decimal ath { get; set; }

        /// <summary>
        /// Güncel fiyatın, tüm zamanların en yüksek fiyatından yüzde olarak ne kadar düşük olduğu.
        /// </summary>
        public decimal ath_change_percentage { get; set; }

        /// <summary>
        /// Kripto para biriminin tüm zamanların en yüksek fiyatına ulaştığı tarih.
        /// </summary>
        public DateTime ath_date { get; set; }

        /// <summary>
        /// Tüm zamanların en düşük fiyatı (ATL - All-Time Low).
        /// </summary>
        public decimal atl { get; set; }

        /// <summary>
        /// Güncel fiyatın, tüm zamanların en düşük fiyatından yüzde olarak ne kadar yüksek olduğu.
        /// </summary>
        public decimal atl_change_percentage { get; set; }

        /// <summary>
        /// Kripto para biriminin tüm zamanların en düşük fiyatına ulaştığı tarih.
        /// </summary>
        public DateTime atl_date { get; set; }

        /// <summary>
        /// Yatırım getirisi bilgileri (ROI - Return on Investment).
        /// </summary>
        public Roi? roi { get; set; }

        /// <summary>
        /// Kripto para birimine ait verilerin en son güncellendiği tarih ve saat.
        /// </summary>
        public DateTime last_updated { get; set; }
    }
    /// <summary>
    /// Roi sınıfı, Return on Investment (Yatırım Getirisi)
    /// anlamına gelir ve kripto para biriminin belirli bir referans noktasına 
    /// (örneğin, ICO fiyatı) göre ne kadar kazanç veya kayıp sağladığını gösterir.
    /// Bu sınıf, yatırımın performansını değerlendirmek için önemli metrikleri içerir.
    /// </summary>
    public class Roi
    {
        /// <summary>
        /// Yatırımın kaç kat arttığını gösterir.
        /// </summary>
        public decimal times { get; set; }

        /// <summary>
        /// Getirinin ifade edildiği para birimi (örneğin, "btc", "eth", "usd").
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// Getirinin yüzde olarak ifadesi.
        /// </summary>
        public decimal percentage { get; set; }
    }
}
