using Application.Dtos;
using Application.Services.ExternalServices;
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
public sealed class GetAuthorizationTokenQueryHandler : ARequestHandler<GetAuthorizationTokenQuery, TokenDto>
{
    readonly IIdentityService identityService;

    public GetAuthorizationTokenQueryHandler(IIdentityService identityService)
    {
        this.identityService = identityService;
    }

    protected override async Task<TokenDto> HandleRequestAsync(GetAuthorizationTokenQuery request)
    {
        var token = await identityService.AuthenticateAsync(request.Email, request.Password);
        return new TokenDto(token);
    }
}
