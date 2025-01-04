using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validasiton;


public record ValidationResult(bool IsValid, string? ErrorMessage)
{
    public static ValidationResult Success()
        => new(true, null);

    public static ValidationResult Fail(string message)
        => new(false, message);
}
