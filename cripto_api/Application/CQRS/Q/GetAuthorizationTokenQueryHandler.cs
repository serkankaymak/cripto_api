using Application.Dtos;
using Application.Services.ExternalServices;
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
    readonly IIdentityService identityService;

    public GetAuthorizationTokenQueryHandler(IIdentityService identityService)
    {
        this.identityService = identityService;
    }

    protected override async Task<TokenDto> HandleRequestAsync(GetAuthorizationTokenQuery request)
    {
        var result = await identityService.AuthenticateAsync(request.Email, request.Password);
        return new TokenDto { Token = result };
    }
}
