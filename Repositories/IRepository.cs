namespace examen_csharp_sur_table.Repositories;

public interface IRepository<T> where T : class
{
    // Récupère un élément par son ID
    Task<T?> GetByIdAsync(int id);
    
    // Récupère tous les éléments
    Task<IEnumerable<T>> GetAllAsync();
    
    // Cherche les éléments qui correspondent à une condition
    Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate);
    
    // Ajoute un nouvel élément en base de données
    Task AddAsync(T entity);
    
    // Met à jour un élément existant
    Task UpdateAsync(T entity);
    
    // Supprime un élément par son ID
    Task DeleteAsync(int id);
    
    // Enregistre les modifications en base de données
    Task SaveChangesAsync();
}
