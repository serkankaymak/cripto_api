using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses.Dtos;
using TicTacTec.TA.Library;

namespace Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses;



public interface ICryptoTechnicalAnalyser
{
    public BollingerBandIndicator CalculateBollingerBandsWithDates(List<Ticker> tickers, int period = 20, double nbDevUp = 2, double nbDevDn = 2);
    MacdIndicator CalculateMacdWithDates(List<Ticker> tickers, int fastPeriod = 12, int slowPeriod = 26, int signalPeriod = 9);
    RsiIndicator CalculateRsiWithDates(List<Ticker> tickers, int period = 14);
    ObvIndicator CalculateObvWithDates(List<Ticker> tickers);
}

public class CryptoTechnicalAnalyses : ICryptoTechnicalAnalyser
{
    public RsiIndicator CalculateRsiWithDates(List<Ticker> tickers, int period = 14)
    {
        // Ticker'ları tarihine göre sıralıyoruz (Created, TimeStamp vb.):
        var sortedTickers = tickers.OrderBy(t => t.Created).ToList();

        // closeArray oluştur
        double[] closeArray = sortedTickers
            .Select(t => (double)t.Price)
            .ToArray();

        // TA-Lib çıktı dizisi
        double[] rsiValues = new double[closeArray.Length];

        int startIdx = 0;
        int endIdx = closeArray.Length - 1;

        // RSI hesapla
        var retCode = Core.Rsi(
            startIdx,
            endIdx,
            closeArray,
            period,
            out int outBegIdx,
            out int outNbElement,
            rsiValues
        );

        // Dönüş değeri
        var results = new List<RsiResult>();

        if (retCode == Core.RetCode.Success && outNbElement > 0)
        {
            // TA-Lib genellikle ilk (outBegIdx) barlara bir şey yazmaz (hesap için atlıyor).
            // Dolayısıyla  i = outBegIdx..(outBegIdx + outNbElement - 1)
            // aralığını alacağız ve bu indekslerdeki tarihle RSI değeri eşleyeceğiz.
            for (int i = outBegIdx; i < outBegIdx + outNbElement; i++)
            {
                results.Add(new RsiResult
                {
                    Date = sortedTickers[i].Created,   // veya TimeStamp -> DateTime'e çevir
                    RsiValue = rsiValues[i]
                });
            }
        }

        return new RsiIndicator(results);
    }
    public MacdIndicator CalculateMacdWithDates(List<Ticker> tickers, int fastPeriod = 12, int slowPeriod = 26, int signalPeriod = 9)
    {
        // Tarihe göre sırala
        var sortedTickers = tickers.OrderBy(t => t.Created).ToList();

        double[] closeArray = sortedTickers.Select(t => (double)t.Price).ToArray();

        // TA-Lib çıktı dizileri
        double[] macd = new double[closeArray.Length];
        double[] signal = new double[closeArray.Length];
        double[] hist = new double[closeArray.Length];

        int startIdx = 0;
        int endIdx = closeArray.Length - 1;

        var retCode = Core.Macd(
            startIdx,
            endIdx,
            closeArray,
            fastPeriod,
            slowPeriod,
            signalPeriod,
            out int outBegIdx,
            out int outNbElement,
            macd,
            signal,
            hist
        );

        var results = new List<MacdResult>();

        if (retCode == Core.RetCode.Success && outNbElement > 0)
        {
            for (int i = outBegIdx; i < outBegIdx + outNbElement; i++)
            {
                results.Add(new MacdResult
                {
                    Date = sortedTickers[i].Created,
                    Macd = macd[i],
                    Signal = signal[i],
                    Hist = hist[i]
                });
            }
        }

        return new MacdIndicator(results);
    }
    public BollingerBandIndicator CalculateBollingerBandsWithDates(List<Ticker> tickers, int period = 20, double nbDevUp = 2, double nbDevDn = 2)
    {
        var sortedTickers = tickers.OrderBy(t => t.Created).ToList();
        double[] closeArray = sortedTickers.Select(t => (double)t.Price).ToArray();
        double[] upperBand = new double[closeArray.Length];
        double[] middleBand = new double[closeArray.Length];
        double[] lowerBand = new double[closeArray.Length];

        int startIdx = 0;
        int endIdx = closeArray.Length - 1;

        var retCode = Core.Bbands(
            startIdx,
            endIdx,
            closeArray,
            period,
            nbDevUp,
            nbDevDn,
            Core.MAType.Sma,
            out int outBegIdx,
            out int outNbElement,
            upperBand,
            middleBand,
            lowerBand
        );

        var results = new List<BollingerBandsResult>();
        if (retCode == Core.RetCode.Success && outNbElement > 0)
        {
            for (int i = outBegIdx; i < outBegIdx + outNbElement; i++)
            {
                results.Add(new BollingerBandsResult
                {
                    Date = sortedTickers[i].Created,
                    Upper = upperBand[i],
                    Middle = middleBand[i],
                    Lower = lowerBand[i]
                });
            }
        }

        return new BollingerBandIndicator(results);
    }
    public ObvIndicator CalculateObvWithDates(List<Ticker> tickers)
    {
        // Tarihe göre sırala
        var sortedTickers = tickers.OrderBy(t => t.Created).ToList();

        // TA-Lib OBV fonksiyonu float[] istiyorsa, float'a dönüştürün
        float[] inClose = sortedTickers.Select(t => (float)t.Price).ToArray();
        float[] inVolume = sortedTickers.Select(t => (float)t.Volume).ToArray();

        double[] outReal = new double[inClose.Length];

        int startIdx = 0;
        int endIdx = inClose.Length - 1;
        int outBegIdx, outNbElement;

        var retCode = Core.Obv(
            startIdx,
            endIdx,
            inClose,
            inVolume,
            out outBegIdx,
            out outNbElement,
            outReal
        );

        var results = new List<ObvResult>();
        if (retCode == Core.RetCode.Success && outNbElement > 0)
        {
            for (int i = outBegIdx; i < outBegIdx + outNbElement; i++)
            {
                results.Add(new ObvResult
                {
                    Date = sortedTickers[i].Created,
                    ObvValue = outReal[i]
                });
            }
        }

        return new ObvIndicator(results);
    }
}