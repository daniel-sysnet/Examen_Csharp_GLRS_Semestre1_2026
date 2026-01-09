namespace examen_csharp_sur_table.Models;

public class AnneeScolaire
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public Statut Statut { get; set; } = Statut.EnCours;

    public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}
