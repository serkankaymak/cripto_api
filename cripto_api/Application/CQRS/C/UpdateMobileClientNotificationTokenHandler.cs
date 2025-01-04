using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.C;

public class UpdateMobileClientNotificationTokenHandler : ARequestHandler<UpdateMobileClientNotificationToken, Unit>
{
    protected override Task<Unit> HandleRequestAsync(UpdateMobileClientNotificationToken request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
