using Application.Dtos;
using Application.Mapping.abstractions;
using Application.Services.ExternalServices;
using Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses;
using MediatR;
using Shared.Validasiton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS;
public class GetAnalysisOfCriptoQuery : ARequest<CryproAnalysesDto>
{
    public int CriptoId { get; set; }
    public DateTime? BeginAnalysesFromThisDate { get; set; }


    public override ValidationResult Validate()
    {
        if (CriptoId <= 0)
            return ValidationResult.Fail("CriptoId must be a positive number.");
        if (BeginAnalysesFromThisDate < DateTime.UtcNow.AddYears(-10))
            return ValidationResult.Fail("BeginAnalysesFromThisDate cannot be older than 10 years.");
        return ValidationResult.Success();
    }
}

public sealed class GetAnalysisOfCriptoQueryHandler : ARequestHandler<GetAnalysisOfCriptoQuery, CryproAnalysesDto>
{
    IMapperFacade _mapper;
    ICriptoService _dataService;
    ICryptoTechnicalAnalyser cryptoTechnicalAnalyses;

    public GetAnalysisOfCriptoQueryHandler(ICriptoService dataService, IMapperFacade mapper, ICryptoTechnicalAnalyser cryptoTechnicalAnalyses)
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

