using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Persistance.Repositories;

using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public abstract class AGenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public AGenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // Get all entities with optional filtered includes
    public virtual async Task<IEnumerable<T>> GetAllAsync(
        Func<IQueryable<T>, IQueryable<T>>[]? includes = null)
    {
        IQueryable<T> query = _dbSet;

        // Apply includes
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = include(query);
            }
        }

        // Apply soft delete filter if applicable
        if (typeof(ISoftDeleteable).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !EF.Property<bool>(e, "IsDeleted"));
        }

        return await query.ToListAsync();
    }

    // Get paged result with optional predicate and includes
    public virtual async Task<PagedResult<T>> GetPagedAsync(
        int page,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IQueryable<T>>[]? includes = null)
    {
        IQueryable<T> query = _dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        // Apply includes
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = include(query);
            }
        }

        // Apply soft delete filter if applicable
        if (typeof(ISoftDeleteable).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !EF.Property<bool>(e, "IsDeleted"));
        }

        var totalRecords = await query.CountAsync();
        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<T>
        {
            Data = data,
            Page = page,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };
    }

    // Get entity by ID with exception handling
    public virtual async Task<T> GetByIdAsync(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            // If entity supports soft delete, ensure it's not deleted
            if (entity is ISoftDeleteable softDeleteEntity && softDeleteEntity.IsDeleted)
            {
                throw ExceptionFactory.NotFound(nameof(id));
            }

            return entity;
        }
        throw ExceptionFactory.NotFound(nameof(id));
    }

    // Find entities based on predicate with optional filtered includes
    public virtual async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IQueryable<T>>[]? includes = null)
    {
        IQueryable<T> query = _dbSet.Where(predicate);

        // Apply includes
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = include(query);
            }
        }

        // Apply soft delete filter if applicable
        if (typeof(ISoftDeleteable).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !EF.Property<bool>(e, "IsDeleted"));
        }

        return await query.ToListAsync();
    }

    // Add a new entity
    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    // Update an existing entity
    public virtual void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    // Remove an entity (soft delete if supported)
    public virtual void Remove(T entity)
    {
        if (entity is ISoftDeleteable softDeleteEntity)
        {
            softDeleteEntity.IsDeleted = true;
            Update(entity);
        }
        else
        {
            _dbSet.Remove(entity);
        }
    }
}