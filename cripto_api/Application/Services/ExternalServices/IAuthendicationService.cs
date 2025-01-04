using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ExternalServices;

public interface IAuthendicationService : IExternalService
{
    Task<string> AuthenticateAsync(string email, string password);
}