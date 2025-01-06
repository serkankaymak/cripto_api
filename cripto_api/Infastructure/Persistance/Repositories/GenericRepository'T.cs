using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Persistance.Repositories;



public abstract class AGenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected AGenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// Retrieves entities based on an optional predicate with includes and soft delete filtering.
    /// If predicate is null, returns all entities.
    /// </summary>
    /// <param name="predicate">Optional filter expression.</param>
    /// <param name="includes">Optional includes for related entities.</param>
    /// <param name="includeSoftDeleted">Whether to include soft-deleted entities.</param>
    /// <returns>An enumerable of entities.</returns>
    public virtual async Task<IEnumerable<T>> WhereAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IQueryable<T>>[]? includes = null,
        bool includeSoftDeleted = false)
    {
        IQueryable<T> query = _dbSet;

        // Apply predicate if provided
        if (predicate != null) query = query.Where(predicate);

        // Apply includes
        if (includes != null)
        {
            foreach (var include in includes) query = include(query);

        }

        // Apply soft delete filter if applicable
        if (typeof(ISoftDeleteable).IsAssignableFrom(typeof(T)))
        {
            query = includeSoftDeleted
                ? query.Where(e => EF.Property<bool>(e, "IsDeleted"))
                : query.Where(e => !EF.Property<bool>(e, "IsDeleted"));
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Retrieves a paged list of entities with optional filtering and includes.
    /// </summary>
    /// <param name="page">Page number (1-based).</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="predicate">Optional filter expression.</param>
    /// <param name="includes">Optional includes for related entities.</param>
    /// <param name="includeSoftDeleted">Whether to include soft-deleted entities.</param>
    /// <returns>A PagedResult containing the data and pagination info.</returns>
    public virtual async Task<PagedResult<T>> GetPagedAsync(
        int page,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IQueryable<T>>[]? includes = null,
        bool includeSoftDeleted = false)
    {
        if (page <= 0) throw new ArgumentException("Page number should be greater than 0.", nameof(page));
        if (pageSize <= 0) throw new ArgumentException("Page size should be greater than 0.", nameof(pageSize));

        IQueryable<T> query = _dbSet;

        // Apply predicate if provided
        if (predicate != null) query = query.Where(predicate);


        // Apply includes
        if (includes != null)
        {
            foreach (var include in includes) query = include(query);
        }

        // Apply soft delete filter if applicable
        if (typeof(ISoftDeleteable).IsAssignableFrom(typeof(T)))
        {
            query = includeSoftDeleted
                ? query.Where(e => EF.Property<bool>(e, "IsDeleted"))
                : query.Where(e => !EF.Property<bool>(e, "IsDeleted"));
        }

        var totalRecords = await query.CountAsync();

        var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Data = data,
            Page = page,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };
    }

    /// <summary>
    /// Retrieves an entity by its primary key.
    /// </summary>
    /// <param name="id">The primary key.</param>
    /// <returns>The entity if found and not soft-deleted.</returns>
    /// <exception cref="NotFoundException">Thrown if entity not found or is soft-deleted.</exception>
    public virtual async Task<T> GetByIdAsync(object id)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));

        var entity = await _dbSet.FindAsync(id);

        if (entity == null) throw ExceptionFactory.NotFound($"Entity of type {typeof(T).Name} with ID {id} not found.");

        // Check soft delete
        if (entity is ISoftDeleteable softDeleteEntity && softDeleteEntity.IsDeleted) throw ExceptionFactory.NotFound($"Entity of type {typeof(T).Name} with ID {id} not found.");
        return entity;
    }

    /// <summary>
    /// Adds a new entity to the context.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public virtual async Task AddAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// Updates an existing entity in the context.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public virtual void Update(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    /// <summary>
    /// Removes an entity from the context. Performs a soft delete if supported.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    public virtual void Remove(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

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

