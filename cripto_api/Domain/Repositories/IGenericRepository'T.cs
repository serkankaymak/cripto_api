using Domain.Domains.IdentityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;


public class PagedResult<T>
{
    public required IEnumerable<T> Data { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages => (int)System.Math.Ceiling((double)TotalRecords / PageSize);
}




public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IQueryable<T>>[]? includes = null, bool includeSoftDeleted = false);
    Task<T> GetByIdAsync(object id);
    Task<PagedResult<T>> GetPagedAsync(int page, int pageSize, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IQueryable<T>>[]? includes = null, bool includeSoftDeleted = false);
    void Remove(T entity);
    void Update(T entity);
}