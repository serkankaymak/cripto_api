using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Persistance.Repositories;

public class UnitOfWork : IUnitOfWork
{

    ICriptoRepository _criptos;
    ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, ICriptoRepository criptos)
    {
        _context = context;
        _criptos = criptos;
    }

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
    public void Dispose() => _context.Dispose();
    public ICriptoRepository criptos => _criptos;

}
