using Application.Repositories;
using Application.Services.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Infastructue.Services;

public class CriptoService : ICriptoService
{
    IUnitOfWork _unitOfWork { get; }

    public CriptoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<List<Crypto>> GetCryptosWithTickers(DateTime dateTime)
    {
        var entities = await _unitOfWork.criptos.GetCriptosWithTickers(dateTime);
        return entities.ToList();
    }


    public async Task<Crypto> GetCryptoWithTickers(int cryptoId, DateTime dateTime)
    {
        return await _unitOfWork.criptos.GetCryptoWithTickers(cryptoId, dateTime);
    }
}
