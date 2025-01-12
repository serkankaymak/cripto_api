using Application.Dtos;
using Application.Mapping.abstractions;
using Application.Services.ExternalServices;
using Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses;
using Shared.Validasiton;

namespace Application.CQRS;
public class GetAnalysisOfCriptosQuery : ARequest<List<CryproAnalysesDto>>
{
    public DateTime? BeginAnalysesFromThisDate { get; set; }


    public override ValidationResult Validate()
    {
        if (BeginAnalysesFromThisDate < DateTime.UtcNow.AddYears(-10))
            return ValidationResult.Fail("BeginAnalysesFromThisDate cannot be older than 10 years.");
        return ValidationResult.Success();
    }
}

public sealed class GetAnalysisOfCriptosQueryHandler : ARequestHandler<GetAnalysisOfCriptosQuery, List<CryproAnalysesDto>>
{
    IMapperFacade _mapper;
    ICriptoService _dataService;
    ICryptoTechnicalAnalyser cryptoTechnicalAnalyses;

    public GetAnalysisOfCriptosQueryHandler(ICriptoService dataService, IMapperFacade mapper, ICryptoTechnicalAnalyser cryptoTechnicalAnalyses)
    {
        _dataService = dataService;
        _mapper = mapper;
        this.cryptoTechnicalAnalyses = cryptoTechnicalAnalyses;
    }

    protected override async Task<List<CryproAnalysesDto>> HandleRequestAsync(GetAnalysisOfCriptosQuery request)
    {
        var entities =  await _dataService.GetCryptosTechnicalAnalyses();
        return entities;

    }
}

