using KoiShow.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KoiShow.Data.Base;

public class GenericRepository<T> where T : class
{
    protected FA24_SE1716_PRN231_G2_KoiShowContext _context;

    public GenericRepository()
    {
        _context ??= new FA24_SE1716_PRN231_G2_KoiShowContext();
    }

    public GenericRepository(FA24_SE1716_PRN231_G2_KoiShowContext context)
    {
        _context = context;
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public List<T> GetAllWithInclude(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query.ToList();
    }

    public async Task<List<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public List<T> GetByConditionWithInclude(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>().Where(condition);
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query.ToList();
    }

    public async Task<List<T>> GetByConditionWithIncludeAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>().Where(condition);
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.ToListAsync();
    }

    public void Create(T entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public async Task<int> CreateAsync(T entity)
    {
        _context.Add(entity);
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;
        _context.SaveChanges();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        var checker = _context.Set<T>().Local.FirstOrDefault(e => e == entity);

        if (checker != null)
        {
            _context.Entry(checker).State = EntityState.Detached;
        }

        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;

        return await _context.SaveChangesAsync();
    }

    public bool Remove(T entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
        return true;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public T GetById(int id)
    {
        var entity = _context.Set<T>().Find(id);

        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<T> GetByIdAsync(int id)   
    {
        var entity = await _context.Set<T>().FindAsync(id);

        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public T GetById(string code)
    {
        var entity = _context.Set<T>().Find(code);

        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<T> GetByIdAsync(string code)
    {
        var entity = await _context.Set<T>().FindAsync(code);

        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public T GetById(Guid code)
    {
        var entity = _context.Set<T>().Find(code);

        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<T> GetByIdAsync(Guid code)
    {
        var entity = await _context.Set<T>().FindAsync(code);

        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    #region Separating asign entity and save operators        

    public void PrepareCreate(T entity)
    {
        _context.Add(entity);
    }

    public void PrepareUpdate(T entity)
    {
        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;
    }

    public void PrepareRemove(T entity)
    {
        _context.Remove(entity);
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    #endregion Separating asign entity and save operators

    #region Range process method
    public void CreateRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (!_context.Set<T>().Local.Any(e => e == entity))
            {
                _context.Add(entity);
            }
            else
            {
                Update(entity);
            }
        }
        _context.SaveChanges();
    }

    public async Task<int> CreateRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (!_context.Set<T>().Local.Any(e => e == entity))
            {
                _context.Add(entity);
            }
            else
            {
                await UpdateAsync(entity);
            }
        }
        return await _context.SaveChangesAsync();
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (_context.Set<T>().Local.Any(e => e == entity))
            {
                Update(entity);
            }
            else
            {
                Create(entity);
            }
        }
        _context.SaveChanges();
    }

    public async Task<int> UpdateRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (_context.Set<T>().Local.Any(e => e == entity))
            {
                await UpdateAsync(entity);
            }
            else
            {
                await CreateAsync(entity);
            }
        }
        return await _context.SaveChangesAsync();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.RemoveRange(entities);
        _context.SaveChanges();
    }

    public async Task<int> RemoveRangeAsync(IEnumerable<T> entities)
    {
        _context.RemoveRange(entities);
        return await _context.SaveChangesAsync();
    }
    #endregion
}