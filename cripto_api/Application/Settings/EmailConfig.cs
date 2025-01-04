using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Settings;

public class EmailConfig
{
    public string host { get; set; }
    public int port { get; set; }
    public bool enableSSL { get; set; }
    public string userName { get; set; }
    public string password { get; set; }
}
