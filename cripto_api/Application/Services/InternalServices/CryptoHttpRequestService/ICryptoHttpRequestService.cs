using Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InternalServices.CryptoHttpRequestService;

public interface ICryptoHttpRequestService
{
    Task<List<CryptoData>> GetCryptoDataAsync(ApiEndpointConfig setting);

}
