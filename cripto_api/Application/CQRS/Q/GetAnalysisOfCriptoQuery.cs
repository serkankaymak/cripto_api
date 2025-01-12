using Application.Dtos;
using Application.Mapping.abstractions;
using Application.Services.ExternalServices;
using Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses;
using Shared.Validasiton;

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
        return await _dataService.GetCryptoTechnicalAnalyses(request.CriptoId, request.BeginAnalysesFromThisDate);


    }
}

