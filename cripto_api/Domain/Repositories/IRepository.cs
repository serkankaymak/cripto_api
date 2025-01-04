using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories;
public interface IRepository
{
    Task<Crypto> GetCryptoWithTickers(int criptoId, DateTime dateTime);
    Task<IEnumerable<Crypto>> GetCriptosWithTickers(DateTime dateTime);
}
