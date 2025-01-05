using Application.Dtos;
using Application.Mappings;
using Application.Repositories;
using Application.Services.ExternalServices;
using Application.Services.InternalServices;
using AutoMapper;
using Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses;
using MediatR;
using Shared.ApiResponse;

namespace Application.CQRS.Q;
public sealed class GetAnalysisOfCriptoQueryHandler : ARequestHandler<GetAnalysisOfCriptoQuery, CryproAnalysesDto>
{
    IMapperFacade _mapper;
    ICriptoService _dataService;
    ICryptoTechnicalAnalyses cryptoTechnicalAnalyses;

    public GetAnalysisOfCriptoQueryHandler(ICriptoService dataService, IMapperFacade mapper, ICryptoTechnicalAnalyses cryptoTechnicalAnalyses)
    {
        _dataService = dataService;
        _mapper = mapper;
        this.cryptoTechnicalAnalyses = cryptoTechnicalAnalyses;
    }

    protected override async Task<CryproAnalysesDto> HandleRequestAsync(GetAnalysisOfCriptoQuery request)
    {
        var cripto = await _dataService.GetCryptoWithTickers(request.CriptoId, request.BeginAnalysesFromThisDate ?? DateTime.UtcNow.AddYears(-1));
        var rsi = cryptoTechnicalAnalyses.CalculateRsiWithDates(cripto.Tickers.ToList());
        var macd = cryptoTechnicalAnalyses.CalculateMacdWithDates(cripto.Tickers.ToList());
        var obv = cryptoTechnicalAnalyses.CalculateObvWithDates(cripto.Tickers.ToList());
        var bollingerBands = cryptoTechnicalAnalyses.CalculateBollingerBandsWithDates(cripto.Tickers.ToList());

        var criptoDto = _mapper.Map<Crypto, CryptoDto>(cripto);
        criptoDto.tickers.Clear();
        return new CryproAnalysesDto(criptoDto, bollingerBands, rsi, macd, obv);

    }
}

