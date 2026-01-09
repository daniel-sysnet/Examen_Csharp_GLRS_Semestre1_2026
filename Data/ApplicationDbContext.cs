using Microsoft.EntityFrameworkCore;
using examen_csharp_sur_table.Models;

namespace examen_csharp_sur_table.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Etudiant> Etudiants { get; set; }
    public DbSet<AnneeScolaire> AnneesScolaires { get; set; }
    public DbSet<Classe> Classes { get; set; }
    public DbSet<Inscription> Inscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Inscription>()
            .HasOne(i => i.Etudiant)
            .WithMany(e => e.Inscriptions)
            .HasForeignKey(i => i.EtudiantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Inscription>()
            .HasOne(i => i.Classe)
            .WithMany(c => c.Inscriptions)
            .HasForeignKey(i => i.ClasseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Inscription>()
            .HasOne(i => i.AnneeScolaire)
            .WithMany(a => a.Inscriptions)
            .HasForeignKey(i => i.AnneeScolaireId)
            .OnDelete(DeleteBehavior.Cascade);

        InitializeData(modelBuilder);
    }

    private void InitializeData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnneeScolaire>().HasData(
            new AnneeScolaire { Id = 1, Code = "2023-2024", Libelle = "Année Scolaire 2023-2024", Statut = Statut.Cloturee },
            new AnneeScolaire { Id = 2, Code = "2024-2025", Libelle = "Année Scolaire 2024-2025", Statut = Statut.EnCours }
        );

        // Initialisation des classes
        modelBuilder.Entity<Classe>().HasData(
            new Classe { Id = 1, Code = "L1-INFO", Libelle = "Licence 1 Informatique" },
            new Classe { Id = 2, Code = "L2-INFO", Libelle = "Licence 2 Informatique" },
            new Classe { Id = 3, Code = "M1-MIAGE", Libelle = "Master 1 MIAGE" }
        );

        // Initialisation des étudiants
        modelBuilder.Entity<Etudiant>().HasData(
            new Etudiant { Id = 1, Matricule = "STD001", Nom = "Diallo", Prenom = "Moussa" },
            new Etudiant { Id = 2, Matricule = "STD002", Nom = "Ndiaye", Prenom = "Fatou" },
            new Etudiant { Id = 3, Matricule = "STD003", Nom = "Sow", Prenom = "Amadou" },
            new Etudiant { Id = 4, Matricule = "STD004", Nom = "Ba", Prenom = "Aïssatou" },
            new Etudiant { Id = 5, Matricule = "STD005", Nom = "Traore", Prenom = "Ibrahima" }
        );

        // Initialisation des inscriptions
        modelBuilder.Entity<Inscription>().HasData(
            new Inscription { Id = 1, EtudiantId = 1, ClasseId = 1, AnneeScolaireId = 2, Date = new DateTime(2024, 9, 15), Montant = 50000.00m },
            new Inscription { Id = 2, EtudiantId = 2, ClasseId = 1, AnneeScolaireId = 2, Date = new DateTime(2024, 9, 16), Montant = 50000.00m },
            new Inscription { Id = 3, EtudiantId = 3, ClasseId = 2, AnneeScolaireId = 2, Date = new DateTime(2024, 9, 17), Montant = 55000.00m },
            new Inscription { Id = 4, EtudiantId = 4, ClasseId = 3, AnneeScolaireId = 2, Date = new DateTime(2024, 9, 18), Montant = 60000.00m },
            new Inscription { Id = 5, EtudiantId = 5, ClasseId = 1, AnneeScolaireId = 1, Date = new DateTime(2023, 9, 10), Montant = 50000.00m }
        );
    }
}
