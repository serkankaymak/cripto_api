using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ExternalServices;

public interface ICriptoService
{
    Task<List<CryproAnalysesDto>> GetCryptosTechnicalAnalyses(DateTime? dateTime = null);
    Task<List<Crypto>> GetCryptosWithTickers(DateTime? dateTime=null);
    Task<CryproAnalysesDto> GetCryptoTechnicalAnalyses(int criptoId, DateTime? BeginAnalysesFromThisDate = null);
    Task<Crypto> GetCryptoWithTickers(int cryptoId, DateTime? BeginAnalysesFromThisDate=null);
}
