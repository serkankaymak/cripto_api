using Application.Dtos;
using Application.Services.ExternalServices;
using Shared.Validasiton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.C;

public class RegisterCommand : ARequest<TokenDto>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public override ValidationResult Validate()
    {
        return ValidationResult.Success();
    }
}



class RegisterCommandHandler : ARequestHandler<RegisterCommand, TokenDto>
{
    IIdentityService identityService;
    protected override async Task<TokenDto> HandleRequestAsync(RegisterCommand request)
    {
        string token = await identityService.RegisterAsync(request.Email, request.Password);
        return new TokenDto(token);
    }
}
