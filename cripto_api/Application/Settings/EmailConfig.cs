using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Settings;

public class EmailConfig
{
    public required string host { get; set; }
    public required int port { get; set; }
    public required bool enableSSL { get; set; }
    public required string userName { get; set; }
    public required string password { get; set; }
    public required string fromEmail { get; set; }
}
