namespace examen_csharp_sur_table.Models;

public class Inscription
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Montant { get; set; }

    // Clés étrangères
    public int EtudiantId { get; set; }
    public int ClasseId { get; set; }
    public int AnneeScolaireId { get; set; }

    // Références de navigation
    public Etudiant? Etudiant { get; set; }
    public Classe? Classe { get; set; }
    public AnneeScolaire? AnneeScolaire { get; set; }
}
