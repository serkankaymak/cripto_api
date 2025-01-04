using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InternalServices;
public interface ICryptoDataService
{
    Task FetchAndStoreCryptoDataAsync();
}

