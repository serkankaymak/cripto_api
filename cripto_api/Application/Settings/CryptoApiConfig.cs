using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Settings;
public class CryptoApiConfig
{
    public List<ApiEndpointConfig> ApiEndpoints { get; set; } = new List<ApiEndpointConfig>();

}