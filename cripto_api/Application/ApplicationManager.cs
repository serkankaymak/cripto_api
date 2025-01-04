using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public class ApplicationManager
{
    public static bool removeDatabaseOnProgramRestart { get; set; } = true;
    public static bool startHangfireHob { get; set; } = false;
    public static bool isDeveloping { get; set; } = true;
    public static bool useMySql = false;
    public static bool configureDbPathFromApi = true;
}
