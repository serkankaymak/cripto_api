using api.Hubs;
using Application.Dtos.Hubs;
using Application.Services.InternalServices;
using Application.Services.MobilePushNotificationService;
using Infastructure.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shared.ApiResponse;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    IHubContext<CriptoAnalysesHub, ICriptoAnalysesHub> hub;
    private readonly IMobilePushNotificationService mobilePushNotificationService;
    private readonly ICryptoDataService cryptoDataService;

    public TestController(
        ICryptoDataService cryptoDataService,
        IMobilePushNotificationService mobilePushNotificationService,
        IHubContext<CriptoAnalysesHub, ICriptoAnalysesHub> hub)
    {
        this.cryptoDataService = cryptoDataService;
        this.mobilePushNotificationService = mobilePushNotificationService;
        this.hub = hub;
    }


    [HttpGet("FetchTickers")]
    public async Task<IActionResult> FetchTickers()
    {
        await cryptoDataService.FetchAndStoreCryptoDataAsync();
        return Ok();
    }


    [HttpGet("SendPushNotification")]
    public async Task<IActionResult> SendPushNotification(string clientToken)
    {
        await mobilePushNotificationService.SendPushNotification(clientToken, "title", "body", new object { });
        return Ok();
    }

    [HttpGet("ThrowException")]
    public IActionResult ThrowException()
    {
        if (false) throw ExceptionFactory.InternalServerError();
        if (false) throw ExceptionFactory.BadRequest();
        if (true) throw ExceptionFactory.BusinessRuleViolation();
        return Ok();
    }



    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] MessageDto message)
    {
        await hub.Clients.All.SendMessage(message);
        return Ok(message);
    }


}
