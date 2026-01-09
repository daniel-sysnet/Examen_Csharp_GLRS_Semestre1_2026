using examen_csharp_sur_table.Models;
using Microsoft.EntityFrameworkCore;

namespace examen_csharp_sur_table.Repositories;

public interface IInscriptionRepository : IRepository<Inscription>
{
    Task<IEnumerable<Inscription>> GetToutesLesInscriptionsAvecDetailsAsync();
    Task<IEnumerable<Inscription>> GetInscriptionsParClasseAsync(int classeId);
    Task<Inscription?> GetInscriptionWithDetailsAsync(int id);
    Task<IEnumerable<Etudiant>> GetEtudiantsDisponiblesAsync();
    Task<IEnumerable<Classe>> GetClassesAsync();
    Task<IEnumerable<AnneeScolaire>> GetAnneesScolaresActuelsAsync();
}
