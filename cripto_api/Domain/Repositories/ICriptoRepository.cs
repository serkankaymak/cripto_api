using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories;
public interface ICriptoRepository : IGenericRepository<Crypto>
{
    Task<Crypto> GetCryptoWithTickers(int criptoId, DateTime dateTime);
    Task<IEnumerable<Crypto>> GetCriptosWithTickers(DateTime dateTime);
}
