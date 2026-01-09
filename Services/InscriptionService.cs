using Microsoft.EntityFrameworkCore;
using examen_csharp_sur_table.Data;
using examen_csharp_sur_table.Models;

namespace examen_csharp_sur_table.Services;

public class InscriptionService : IInscriptionService
{
    private readonly ApplicationDbContext _context;

    public InscriptionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inscription>> GetInscriptionsParClasseAsync(int classeId)
    {
        return await _context.Inscriptions
            .Where(i => i.ClasseId == classeId)
            .Include(i => i.Etudiant)
            .Include(i => i.Classe)
            .Include(i => i.AnneeScolaire)
            .ToListAsync();
    }

    public async Task<Inscription?> GetInscriptionParIdAsync(int id)
    {
        return await _context.Inscriptions
            .Include(i => i.Etudiant)
            .Include(i => i.Classe)
            .Include(i => i.AnneeScolaire)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Inscription>> GetToutesLesInscriptionsAsync()
    {
        return await _context.Inscriptions
            .Include(i => i.Etudiant)
            .Include(i => i.Classe)
            .Include(i => i.AnneeScolaire)
            .ToListAsync();
    }

    public async Task AjouterInscriptionAsync(Inscription inscription)
    {
        if (inscription == null)
            throw new ArgumentNullException(nameof(inscription));

        inscription.Date = DateTime.Now;
        _context.Inscriptions.Add(inscription);
        await _context.SaveChangesAsync();
    }

    public async Task SupprimerInscriptionAsync(int id)
    {
        var inscription = await _context.Inscriptions.FindAsync(id);
        if (inscription == null)
            throw new KeyNotFoundException($"L'inscription avec l'id {id} n'a pas été trouvée.");

        _context.Inscriptions.Remove(inscription);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Etudiant>> GetEtudiantsDisponiblesAsync()
    {
        return await _context.Etudiants.OrderBy(e => e.Nom).ToListAsync();
    }

    public async Task<IEnumerable<Classe>> GetClassesAsync()
    {
        return await _context.Classes.OrderBy(c => c.Code).ToListAsync();
    }

    public async Task<IEnumerable<AnneeScolaire>> GetAnneesScolaresActuelsAsync()
    {
        return await _context.AnneesScolaires
            .Where(a => a.Statut == Statut.EnCours)
            .OrderBy(a => a.Code)
            .ToListAsync();
    }
}
