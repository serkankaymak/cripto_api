using Application.Dtos;
using Application.Repositories;
using Application.Services.InternalServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Persistance.Repositories;

public class DataService : IDataService
{

    IUnitOfWork _unitOfWork { get; }

    public DataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<List<Crypto>> GetCryptosWithTickers(DateTime dateTime)
    {
        var entities = await _unitOfWork.repository.GetCriptosWithTickers(dateTime);
        return entities.ToList();
    }


    public async Task<Crypto> GetCryptoWithTickers(int cryptoId, DateTime dateTime)
    {
        return await _unitOfWork.repository.GetCryptoWithTickers(cryptoId, dateTime);
    }
}

