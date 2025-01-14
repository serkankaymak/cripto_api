﻿using Application.Dtos;
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


    public async Task<Crypto> GetCryptoWithTickers(int cryptoId, DateTime? BeginAnalysesFromThisDate = null)
    {
        if (BeginAnalysesFromThisDate == null) BeginAnalysesFromThisDate = DateTime.UtcNow.AddYears(-1);
        return await _unitOfWork.criptos.GetCryptoWithTickers(cryptoId, BeginAnalysesFromThisDate.Value);
    }



    private CryproAnalysesDto _analyseCripto(Crypto cripto)
    {
        CryptoTechnicalAnalyses cryptoTechnicalAnalyses = new CryptoTechnicalAnalyses();
        var obv = cryptoTechnicalAnalyses.CalculateObvWithDates(cripto.Tickers.ToList());
        var macd = cryptoTechnicalAnalyses.CalculateMacdWithDates(cripto.Tickers.ToList());
        var bolling = cryptoTechnicalAnalyses.CalculateBollingerBandsWithDates(cripto.Tickers.ToList());
        var rsi = cryptoTechnicalAnalyses.CalculateRsiWithDates(cripto.Tickers.ToList());
        var criptoDto = _mapper.Map<Crypto, CryptoDto>(cripto);
        var analysesDto = new CryproAnalysesDto(criptoDto, bolling, rsi, macd, obv);

        return analysesDto;
    }

    public async Task<CryproAnalysesDto> GetCryptoTechnicalAnalyses(int criptoId, DateTime? BeginAnalysesFromThisDate = null)
    {
        CryptoTechnicalAnalyses cryptoTechnicalAnalyses = new CryptoTechnicalAnalyses();
        Crypto cripto = await GetCryptoWithTickers(criptoId,BeginAnalysesFromThisDate);
        return _analyseCripto(cripto);
    }

    public async Task<List<CryproAnalysesDto>> GetCryptosTechnicalAnalyses(DateTime? dateTime = null)
    {
        CryptoTechnicalAnalyses cryptoTechnicalAnalyses = new CryptoTechnicalAnalyses();

        var entites = await GetCryptosWithTickers(dateTime);
        List<CryproAnalysesDto> cryproAnalysesDtos = new List<CryproAnalysesDto>();
        entites.ForEach(cripto => { cryproAnalysesDtos.Add(_analyseCripto(cripto)); });
        return cryproAnalysesDtos;
    }
}
