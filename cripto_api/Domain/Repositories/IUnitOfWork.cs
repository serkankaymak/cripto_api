using Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories;

public interface IUnitOfWork : IDisposable
{
   
    ICriptoRepository criptos { get; }
    Task<int> CommitAsync();
    new void Dispose();
}
