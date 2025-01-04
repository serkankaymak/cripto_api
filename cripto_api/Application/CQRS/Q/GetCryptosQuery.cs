using Application.Dtos;
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
        return ValidationResult.Success();
    }
}
