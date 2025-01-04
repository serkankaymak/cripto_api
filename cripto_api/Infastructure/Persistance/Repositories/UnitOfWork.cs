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

    IRepository _repository;
    ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, IRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
    public void Dispose() => _context.Dispose();
    public IRepository repository => _repository;

}
