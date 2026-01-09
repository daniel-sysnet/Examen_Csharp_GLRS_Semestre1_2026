using examen_csharp_sur_table.Models;
using Microsoft.EntityFrameworkCore;

namespace examen_csharp_sur_table.Repositories;

public interface IInscriptionRepository : IRepository<Inscription>
{
    // Récupère toutes les inscriptions avec les détails de l'étudiant, classe et année scolaire
    Task<IEnumerable<Inscription>> GetToutesLesInscriptionsAvecDetailsAsync();
    
    // Récupère les inscriptions d'une classe spécifique
    Task<IEnumerable<Inscription>> GetInscriptionsParClasseAsync(int classeId);
    
    // Récupère une inscription avec tous ses détails associés
    Task<Inscription?> GetInscriptionWithDetailsAsync(int id);
    
    // Récupère la liste des étudiants disponibles pour une inscription
    Task<IEnumerable<Etudiant>> GetEtudiantsDisponiblesAsync();
    
    // Récupère la liste de toutes les classes
    Task<IEnumerable<Classe>> GetClassesAsync();
    
    // Récupère les années scolaires actuelles en cours
    Task<IEnumerable<AnneeScolaire>> GetAnneesScolaresActuelsAsync();
}
