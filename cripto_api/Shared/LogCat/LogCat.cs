using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.LogCat;


public static class LogCat
{
    // Development ortamında mı değil mi bilgisi burada tutulur.
    private static bool _isDevelopment;

    /// <summary>
    /// Ortam bilgisini alarak LogCat'i konfigüre eder.
    /// </summary>
    /// <param name="isDevelopment">true ise Debug/Verbose aktif olur.</param>
    public static void Configure(bool isDevelopment)
    {
        _isDevelopment = isDevelopment;
    }

    /// <summary>
    /// Debug seviyesindeki loglar sadece Development ortamında yazılır.
    /// </summary>
    public static void Debug(string message)
    {
        if (_isDevelopment)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[DEBUG] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Verbose seviyesindeki loglar da sadece Development ortamında yazılır.
    /// </summary>
    public static void Verbose(string message)
    {
        if (_isDevelopment)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"[VERBOSE] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Error logları her zaman yazılsın (Production dahil).
    /// </summary>
    public static void Error(string message, Exception ex = null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        if (ex != null)
        {
            Console.WriteLine(ex);
        }
        Console.ResetColor();
    }
}

