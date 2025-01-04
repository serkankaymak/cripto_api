using Application.Dtos;
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

