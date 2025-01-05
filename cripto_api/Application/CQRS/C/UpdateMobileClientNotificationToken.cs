using MediatR;
using Shared.ApiResponse;
using Shared.Validasiton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.C;

public class UpdateMobileClientNotificationToken : ARequest<Unit>
{
    public required string Email { get; set; }
    public required string MobileClientToken { get; set; }

    public override ValidationResult Validate()
    {
        if (MobileClientToken == null || MobileClientToken.Length < 10)
            return ValidationResult.Fail("Invalid Token Value");

        return ValidationResult.Success();
    }
}
