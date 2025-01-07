using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions;

public static class StringExtension
{
    public static string GetMaskedValue(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        int length = input.Length;
        if (length <= 4)
            return new string('*', length);

        string masked = new string('*', length - 4) + input.Substring(length - 4);
        return masked;
    }

}
