using Application.Dtos;
using Application.Services.InternalServices.JwtService;
using AutoMapper;
using Domain.Domains.IdentityDomain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Q;

public sealed class GetAuthorizationTokenQueryHandler : ARequestHandler<GetAuthorizationTokenQuery, TokenDto>
{
    readonly UserManager<UserIdentity> _userManager;
    readonly IJwtTokenService _jwtTokenService;

    public GetAuthorizationTokenQueryHandler(IJwtTokenService jwtTokenService, UserManager<UserIdentity> userManager)
    {
        this._jwtTokenService = jwtTokenService;
        _userManager = userManager;
    }


    protected override async Task<TokenDto> HandleRequestAsync(GetAuthorizationTokenQuery request, CancellationToken cancellationToken)
    {
        string token;
        if (request.Email == "string" && request.Password == "string")
        {
            token = _jwtTokenService.GenerateToken(new UserIdentity { Email = request.Email, PasswordHash = request.Password });
            return new TokenDto { Token = token };
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) throw new Exception("kullanıcı bulunamadı");
        token = _jwtTokenService.GenerateToken(user);
        return new TokenDto { Token = token };
    }
}
