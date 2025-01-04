using Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;
public class CryproAnalysesDto : IDto
{
    public CryptoDto CryptoDto { get; set; }

    public CryproAnalysesDto(CryptoDto cryptoDto, BollingerBandIndicator? bollingerBandsResult = null, RsiIndicator? rsiResult = null, MacdIndicator? macdResult = null, ObvIndicator? obvResult = null)
    {
        CryptoDto = cryptoDto;
        BollingerBandsResult = bollingerBandsResult;
        RsiResult = rsiResult;
        MacdResult = macdResult;
        ObvResult = obvResult;
    }

    public BollingerBandIndicator? BollingerBandsResult { get; private set; }
    public RsiIndicator? RsiResult { get; private set; }
    public MacdIndicator? MacdResult { get; private set; }
    public ObvIndicator? ObvResult { get; private set; }


}

