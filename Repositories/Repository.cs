using examen_csharp_sur_table.Data;

namespace examen_csharp_sur_table.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Récupère un élément par son ID
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    // Récupère tous les éléments du type
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Task.FromResult(_context.Set<T>().ToList());
    }

    // Cherche tous les éléments qui correspondent au prédicat
    public async Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate)
    {
        return await Task.FromResult(_context.Set<T>().Where(predicate).ToList());
    }

    // Ajoute un nouvel élément et l'enregistre
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await SaveChangesAsync();
    }

    // Met à jour un élément et enregistre les changements
    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await SaveChangesAsync();
    }

    // Supprime un élément par son ID
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }
    }

    // Enregistre toutes les modifications en base de données
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
