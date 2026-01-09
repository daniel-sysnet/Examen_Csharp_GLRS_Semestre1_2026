using Microsoft.EntityFrameworkCore;
using examen_csharp_sur_table.Data;
using examen_csharp_sur_table.Models;
using examen_csharp_sur_table.Repositories;

namespace examen_csharp_sur_table.Services;

public class InscriptionService : IInscriptionService
{
    private readonly IInscriptionRepository _repository;

    public InscriptionService(IInscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Inscription>> GetInscriptionsParClasseAsync(int classeId)
    {
        return await _repository.GetInscriptionsParClasseAsync(classeId);
    }

    public async Task<Inscription?> GetInscriptionParIdAsync(int id)
    {
        return await _repository.GetInscriptionWithDetailsAsync(id);
    }

    public async Task<IEnumerable<Inscription>> GetToutesLesInscriptionsAsync()
    {
        return await _repository.GetToutesLesInscriptionsAvecDetailsAsync();
    }

    public async Task AjouterInscriptionAsync(Inscription inscription)
    {
        if (inscription == null)
            throw new ArgumentNullException(nameof(inscription));

        inscription.Date = DateTime.Now;
        await _repository.AddAsync(inscription);
    }

    public async Task SupprimerInscriptionAsync(int id)
    {
        var inscription = await _repository.GetByIdAsync(id);
        if (inscription == null)
            throw new KeyNotFoundException($"L'inscription avec l'id {id} n'a pas été trouvée.");

        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Etudiant>> GetEtudiantsDisponiblesAsync()
    {
        return await _repository.GetEtudiantsDisponiblesAsync();
    }

    public async Task<IEnumerable<Classe>> GetClassesAsync()
    {
        return await _repository.GetClassesAsync();
    }

    public async Task<IEnumerable<AnneeScolaire>> GetAnneesScolaresActuelsAsync()
    {
        return await _repository.GetAnneesScolaresActuelsAsync();
    }
}
