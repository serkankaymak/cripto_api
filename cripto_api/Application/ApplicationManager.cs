using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public class ApplicationManager
{
    public static bool enableSensitiveDataLogging { get; set; } = true;
    public static bool removeDatabaseOnProgramRestart { get; set; } = false;
    public static bool startHangfireJob { get; set; } = false;
    public static bool isDeveloping { get; set; } = true;

}
