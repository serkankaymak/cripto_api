using Application.Dtos;
using Application.Mapping.abstractions;
using Application.Services.ExternalServices;
using MediatR;
using Shared.Validasiton;

namespace Application.CQRS.Q;

public class GetCryptosQuery : ARequest<List<CryptoDto>>
{
    private DateTime? _dateTime;

    public DateTime dateTime
    {
        get => _dateTime ?? DateTime.UtcNow.AddYears(-1); // Varsayılan tarih 1 yıl öncesi
        set => _dateTime = value;
    }

    public override ValidationResult Validate()
    {
        if (_dateTime != null && _dateTime < DateTime.UtcNow.AddYears(-10)) return ValidationResult.Fail("en fazla 10 sene önceki veriler sorgulanabilir");
        return ValidationResult.Success();
    }
}

public sealed class GetCryptosQueryHandler : ARequestHandler<GetCryptosQuery, List<CryptoDto>>
{
    IMapperFacade _mapper;
    ICriptoService _dataService;

    public GetCryptosQueryHandler(ICriptoService dataService, IMapperFacade mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }


    protected override async Task<List<CryptoDto>> HandleRequestAsync(GetCryptosQuery request)
    {
        var entities = await _dataService.GetCryptosWithTickers(request.dateTime);
        var dtos = entities.Select(x => _mapper.Map<Crypto, CryptoDto>(x)).ToList();
        return dtos;
    }
}
