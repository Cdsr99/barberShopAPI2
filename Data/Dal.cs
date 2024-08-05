using Microsoft.EntityFrameworkCore;

namespace BarberShopAPI2.Data;

public class Dal<T> where T : class
{
    private readonly BarberShopContext _context;

    public Dal(BarberShopContext context)
    {
        _context = context;
    }

    #region Show

    public IEnumerable<T> Show()
    {
        return _context.Set<T>().ToList();
    }

    #endregion

    #region Adding

    public void Add(T objeto)
    {
        _context.Set<T>().Add(objeto);
        _context.SaveChanges();
    }

    #endregion

    #region Adding 2

    public async Task AddRanger(IEnumerable<T> objeto)
    {
        try
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto), "The object to add cannot be null.");

            await _context.Set<T>().AddRangeAsync(objeto);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Error occurred while adding object: {ex}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while adding object: {ex}");
        }
    }

    #endregion

    #region Update

    public void Update(T objeto)
    {
        _context.Set<T>().Update(objeto);
        _context.SaveChanges();
    }

    #endregion

    #region Delete

    public void Delete(T objeto)
    {
        _context.Set<T>().Remove(objeto);
        _context.SaveChanges();
    }

    #endregion

    #region SearchFor

    public T? SearchFor(Func<T, bool> condicao)
    {
        return _context.Set<T>().FirstOrDefault(condicao);
    }

    #endregion

    #region SearchForDay

    public IEnumerable<T> SearchForDay(Func<T, bool> condicao)
    {
        return _context.Set<T>().Where(condicao).ToList();
    }

    #endregion

    #region SearchForAvailabelDays

    public IEnumerable<T> SearchForAvailableDaysAsync(Func<T, bool> condition)
    {
        return _context.Set<T>().Where(condition).ToList();
    }

    #endregion
}