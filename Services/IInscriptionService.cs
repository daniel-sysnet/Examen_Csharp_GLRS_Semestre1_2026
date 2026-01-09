using examen_csharp_sur_table.Models;

namespace examen_csharp_sur_table.Services;

public interface IInscriptionService
{
    Task<IEnumerable<Inscription>> GetInscriptionsParClasseAsync(int classeId);
    Task<Inscription?> GetInscriptionParIdAsync(int id);
    Task<IEnumerable<Inscription>> GetToutesLesInscriptionsAsync();
    Task AjouterInscriptionAsync(Inscription inscription);
    Task SupprimerInscriptionAsync(int id);
    Task<IEnumerable<Etudiant>> GetEtudiantsDisponiblesAsync();
    Task<IEnumerable<Classe>> GetClassesAsync();
    Task<IEnumerable<AnneeScolaire>> GetAnneesScolaresActuelsAsync();
}
