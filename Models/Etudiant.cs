namespace examen_csharp_sur_table.Models;

public class Etudiant
{
    public int Id { get; set; }
    public string Matricule { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;

    public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}
