using examen_csharp_sur_table.Data;
using examen_csharp_sur_table.Models;
using Microsoft.EntityFrameworkCore;

namespace examen_csharp_sur_table.Repositories;

public class InscriptionRepository : Repository<Inscription>, IInscriptionRepository
{
    public InscriptionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Inscription>> GetToutesLesInscriptionsAvecDetailsAsync()
    {
        return await _context.Inscriptions
            .Include(i => i.Etudiant)
            .Include(i => i.Classe)
            .Include(i => i.AnneeScolaire)
            .ToListAsync();
    }

    public async Task<IEnumerable<Inscription>> GetInscriptionsParClasseAsync(int classeId)
    {
        return await _context.Inscriptions
            .Include(i => i.Etudiant)
            .Include(i => i.Classe)
            .Include(i => i.AnneeScolaire)
            .Where(i => i.ClasseId == classeId)
            .ToListAsync();
    }

    public async Task<Inscription?> GetInscriptionWithDetailsAsync(int id)
    {
        return await _context.Inscriptions
            .Include(i => i.Etudiant)
            .Include(i => i.Classe)
            .Include(i => i.AnneeScolaire)
            .FirstOrDefaultAsync(i => i.Id == id);
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
