
using api.Hubs;
using Application.CQRS;
using Application.CQRS.Q;
using Application.Dtos;
using Application.Dtos.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace api.Controllers;


[Route("api")]
[ApiController]
public class HomeController : AController
{
    IHubContext<CriptoAnalysesHub, ICriptoAnalysesHub> hub;
    public HomeController(IMediator mediator, IHubContext<CriptoAnalysesHub, ICriptoAnalysesHub> hub) : base(mediator)
    {
        this.hub = hub;
    }

    /// <summary>
    /// Belirtilen tarihe göre kripto para listesini getirir.
    /// Eğer tarih belirtilmezse, varsayılan olarak bir yıl öncesi kullanılır.
    /// </summary>
    /// <param name="query">Kripto sorgu parametreleri</param>
    /// <returns>Kripto para verilerinin listesi</returns>
    [HttpGet("GetCriptos")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CryptoDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<CryptoDto>>> GetCriptos([FromQuery] GetCryptosQuery query)
    {
        var dtos = await _mediator.Send(query);
        return Ok(dtos);
    }

    /// <summary>
    /// Cripto nun indicatör analizlerini döndürür.
    /// </summary>
    /// <param name="query">Kripto id </param>
    /// <returns>Kripto para verisinin al sat analizleri</returns>
    [HttpGet("GetCriptoAnalyses")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CryproAnalysesDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CryproAnalysesDto>> GetCriptoAnalyses([FromQuery] GetAnalysisOfCriptoQuery query)
    {
        var dtos = await _mediator.Send(query);
        return Ok(dtos);
    }


}
