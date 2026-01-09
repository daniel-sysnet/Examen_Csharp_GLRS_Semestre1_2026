namespace examen_csharp_sur_table.Models;

public class Classe
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;

    // Relation One-To-Many avec Inscription
    public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}
