using api.Hubs;
using Application.Dtos.Hubs;
using Application.Services.ExternalServices;
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
    ICriptoService criptoService { get; set; }
    IEmailService emailService { get; set; }
    IHubContext<CriptoAnalysesHub, ICriptoAnalysesHub> hub;
    private readonly IAndroidPushNotificationService androidPushNotificationService;
    private readonly ICryptoDataService cryptoDataService;

    public TestController(
        ICryptoDataService cryptoDataService,
        IAndroidPushNotificationService mobilePushNotificationService,
        IHubContext<CriptoAnalysesHub, ICriptoAnalysesHub> hub,
        IEmailService emailService,
        ICriptoService criptoService)
    {
        this.cryptoDataService = cryptoDataService;
        this.androidPushNotificationService = mobilePushNotificationService;
        this.hub = hub;
        this.emailService = emailService;
        this.criptoService = criptoService;
    }



    [HttpGet("Deneme")]
    public async Task<IActionResult> Deneme(int id)
    {
        await criptoService.GetCryptoWithTickers(id);
        return Ok();
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
        await androidPushNotificationService.SendPushNotificationAsync(clientToken, "title", "body", new object { });
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
