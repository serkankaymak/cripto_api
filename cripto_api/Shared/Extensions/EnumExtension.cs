using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions;

public static class EnumExtension
{
    public static string[] GetNames(this Enum enumValue)
    {
        return Enum.GetNames(enumValue.GetType());
    }

    /// <summary>
    /// Belirtilen Enum türündeki tüm değerleri döndürür.
    /// </summary>
    /// <typeparam name="T">Enum türü.</typeparam>
    /// <returns>Enum türündeki tüm değerlerin dizisi.</returns>
    public static T[] GetValues<T>() where T : Enum => (T[])Enum.GetValues(typeof(T));

}
