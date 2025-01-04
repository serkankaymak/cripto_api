using Application.Dtos;
using MediatR;
using Shared.Validasiton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Q;

public class GetAuthorizationTokenQuery : ARequest<TokenDto>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public override ValidationResult Validate()
    {
        return ValidationResult.Success();
    }
}
