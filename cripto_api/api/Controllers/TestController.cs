using api.Hubs;
using Application.Dtos.Hubs;
using Application.Services.InternalServices;
using Application.Services.InternalServices.EmailService;
using Application.Services.InternalServices.MobilePushNotificationService;
using Infastructure.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shared.ApiResponse;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    IEmailService emailService { get; set; }
    IHubContext<CriptoAnalysesHub, ICriptoAnalysesHub> hub;
    private readonly IMobilePushNotificationService mobilePushNotificationService;
    private readonly ICryptoDataService cryptoDataService;

    public TestController(
        ICryptoDataService cryptoDataService,
        IMobilePushNotificationService mobilePushNotificationService,
        IHubContext<CriptoAnalysesHub, ICriptoAnalysesHub> hub,
        IEmailService emailService)
    {
        this.cryptoDataService = cryptoDataService;
        this.mobilePushNotificationService = mobilePushNotificationService;
        this.hub = hub;
        this.emailService = emailService;
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
        await mobilePushNotificationService.SendPushNotificationAsync(clientToken, "title", "body", new object { });
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

    [HttpGet("SendEmail")]
    public async Task<IActionResult> SendEmail()
    {
        await emailService.sendEmailAsync("kaymak__serkan35@hotmail.com", "subject", "deneme");
        return Ok("email gönderimi başarılı");
    }


}
