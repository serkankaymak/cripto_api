using Application.CQRS.C;
using Application.CQRS.Q;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthendicationController : AController
{

    public AuthendicationController(IMediator mediator) : base(mediator) { }



    /// <summary>
    /// Login...
    /// </summary>
    /// <returns>Status of the operation.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] GetAuthorizationTokenQuery loginRequest)
    {
        var token = await _mediator.Send(loginRequest);
        return Ok(token);
    }



    /// <summary>
    /// Update Mobile Firabase Notification Token...
    /// </summary>
    /// <returns>Status of the operation.</returns>
    [HttpPost("UpdateMobileNotificationToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMobileNotificationToken([FromBody] UpdateMobileClientNotificationToken request)
    {
        var token = await _mediator.Send(request);
        return Ok(token);
    }





}
