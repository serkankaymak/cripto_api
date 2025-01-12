using Application.Dtos;
using Application.Mapping.abstractions;
using Application.Repositories;
using Application.Services.ExternalServices;
using Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses;
using Infastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Infastructue.Services;

public class CriptoService : ICriptoService
{
    IMapperFacade _mapper;
    IUnitOfWork _unitOfWork { get; }

    public CriptoService(IUnitOfWork unitOfWork, IMapperFacade mapper)
    {
        _unitOfWork = unitOfWork;
        this._mapper = mapper;
    }

    public async Task<List<Crypto>> GetCryptosWithTickers(DateTime? dateTime = null)
    {
        if (dateTime == null) dateTime = DateTime.UtcNow.AddYears(-1);
        var entities = await _unitOfWork.criptos.GetCriptosWithTickers(dateTime.Value);
        return entities.ToList();
    }


    public async Task<Crypto> GetCryptoWithTickers(int cryptoId, DateTime? dateTime = null)
    {



        var w = (UnitOfWork)_unitOfWork;
        var c = w.getContext();


        c.SaveChanges();

        c.Cryptos.Add(new Crypto("?")
        {
            ath=0,Image="",Name= new Random(10000).Next().ToString(),Price=0
        });
        var entities = c.Cryptos.Where(x => x.Id > 0).ToList();

    

        c.SaveChanges();



        if (dateTime == null) dateTime = DateTime.UtcNow.AddYears(-1);
        return await _unitOfWork.criptos.GetCryptoWithTickers(cryptoId, dateTime.Value);
    }


    public async Task<List<CryproAnalysesDto>> GetCryptosTechnicalAnalyses()
    {
        CryptoTechnicalAnalyses cryptoTechnicalAnalyses = new CryptoTechnicalAnalyses();

        var entites = await GetCryptosWithTickers();
        List<CryproAnalysesDto> cryproAnalysesDtos = new List<CryproAnalysesDto>();
        entites.ForEach(cripto =>
        {
            var obv = cryptoTechnicalAnalyses.CalculateObvWithDates(cripto.Tickers.ToList());
            var macd = cryptoTechnicalAnalyses.CalculateMacdWithDates(cripto.Tickers.ToList());
            var bolling = cryptoTechnicalAnalyses.CalculateBollingerBandsWithDates(cripto.Tickers.ToList());
            var rsi = cryptoTechnicalAnalyses.CalculateRsiWithDates(cripto.Tickers.ToList());
            var criptoDto = _mapper.Map<Crypto, CryptoDto>(cripto);
            var analysesDto = new CryproAnalysesDto(criptoDto, bolling, rsi, macd, obv);
            cryproAnalysesDtos.Add(analysesDto);
        });
        return cryproAnalysesDtos;
    }
}
