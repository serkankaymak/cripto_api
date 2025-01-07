
using Application.Events;
using Application.Services.InternalServices;
using Application.Settings;
using Infastructure;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.EventBus;
using Shared.Events;
using System;
using System.Threading.Tasks;

namespace api.Jobs;
public interface IPeriodicJob { public Task ExecuteAsync(); }

public class FetchCriptosPeriodiclyJob : IPeriodicJob, IEventPublisher
{
    static int runCount = 0;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly ICryptoDataService _cryptoDataService;
    private readonly ILogger<FetchCriptosPeriodiclyJob> _logger;

    public FetchCriptosPeriodiclyJob(ICryptoDataService cryptoDataService, ILogger<FetchCriptosPeriodiclyJob> logger, IEventDispatcher eventDispatcher)
    {
        _cryptoDataService = cryptoDataService;
        _logger = logger;
        _eventDispatcher = eventDispatcher;
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine($"Joıb {++runCount} kez çalıştı");
        try
        {
            _logger.LogInformation("CryptoDataJob başladı.");
            await _cryptoDataService.FetchAndStoreCryptoDataAsync();
            await _eventDispatcher.Publish(this, new TickersFetchedEvent());
            _logger.LogInformation("CryptoDataJob başarıyla tamamlandı.");
        }
        catch (Exception ex)
        {
            await _eventDispatcher.Publish(this, new TickerFetchFailedEvent(ex.Message));
        }
    }
}
