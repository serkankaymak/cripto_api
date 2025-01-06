using Application.Mapping;
using Application.Mapping.abstractions;
using Application.Services.InternalServices;
using Application.Services.InternalServices.CryptoHttpRequestService;
using Application.Settings;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.ApiResponse;

namespace Infastructure.Persistance.Repositories;

public class CryptoDataService : ICryptoDataService
{
    private readonly IMapperFacade _mapper;
    private readonly CryptoApiConfig _apiSettings;
    private readonly ICryptoHttpRequestService _cryptoService;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CryptoDataService> _logger;
    private readonly Object _lock = new Object();

    public CryptoDataService(
        IOptions<CryptoApiConfig> apiSettings,
        ICryptoHttpRequestService cryptoService,
        ApplicationDbContext context,
        ILogger<CryptoDataService> logger,
        IMapperFacade mapper)
    {
        _mapper = mapper;
        _apiSettings = apiSettings.Value;
        _cryptoService = cryptoService;
        _context = context;
        _logger = logger;
    }

    public async Task FetchAndStoreCryptoDataAsync()
    {
        foreach (var endpoint in _apiSettings.ApiEndpoints)
        {
            try
            {
                var dataList = await _cryptoService.GetCryptoDataAsync(endpoint);
                if (dataList == null || dataList.Count == 0) throw new Exception("Apiden yanıt alındı ancak , criptolar maplenemedi");

                foreach (var item in dataList)
                {

                    var ticker = _mapper.Map<CryptoData, Ticker>(item);
                    var crypto = _context.Cryptos.FirstOrDefault(x => x.Name.Trim().Equals(item.name.Trim()));


                    if (crypto == null) Console.WriteLine("cripto update ediliyor.");
                    else Console.WriteLine("cripto ekleniyor.");

                    if (crypto == null) { crypto = _mapper.Map<CryptoData, Crypto>(item); _context.Cryptos.Add(crypto); }
                    crypto.Tickers.Add(ticker);

                }


                if (dataList.GroupBy(x => x.name).Any(g => g.Count() > 1))
                    throw new Exception("aynı isimde 1 den fazla cripto var ??? ");

                await _context.SaveChangesAsync();
                _logger.LogInformation("Veriler başarıyla kaydedildi. Zaman: {time}", DateTimeOffset.Now);
                return;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex, $"API çağrısı sırasında hata oluştu: {endpoint.Host}{endpoint.Route}{endpoint.Query}{endpoint.TargetCurrency}");
                throw ExceptionFactory.InternalServerError(ex.Message);
            }
        }
    }
}

