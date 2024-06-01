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
        this._context.Set<T>().Add(objeto);
        this._context.SaveChanges();
    }
    #endregion
    
    #region Update
    public void Update(T objeto)
    {
        this._context.Set<T>().Update(objeto);
        this._context.SaveChanges();
    }
    #endregion
    
    #region Delete
    public void Delete(T objeto)
    {
        this._context.Set<T>().Remove(objeto);
        this._context.SaveChanges();
    }
    #endregion
    
    #region SearchFor
    public T? SearchFor(Func<T, bool> condicao)
    {
        return this._context.Set<T>().FirstOrDefault(condicao);
    }
    #endregion
    
    #region SearchForAvailabelDays
    public IEnumerable<T> SearchForAvailableDays(Func<T, bool> condition)
    {
        return this._context.Set<T>().Where(condition);
    }
    #endregion
}