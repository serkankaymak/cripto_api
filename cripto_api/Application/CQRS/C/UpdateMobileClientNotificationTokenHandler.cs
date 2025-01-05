using Application.Dtos;
using Application.Services.ExternalServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.C;

public class UpdateMobileClientNotificationTokenHandler : ARequestHandler<UpdateMobileClientNotificationToken, Unit>
{
    IIdentityService identityService;

    public UpdateMobileClientNotificationTokenHandler(IIdentityService identityService)
    {
        this.identityService = identityService;
    }

    protected override async Task<Unit> HandleRequestAsync(UpdateMobileClientNotificationToken request)
    {
        await identityService.UpdateMobilePushNotificationTokenOfUser(request.Email, request.MobileClientToken);
        return Unit.Value;
    }
}
