# Diagramme des Relations et Architecture

## Diagramme Entité-Relation (E-R)

```
┌──────────────────────────────────────────────────────────────────┐
│                                                                  │
│                         Diagramme EF Core                        │
│                                                                  │
└──────────────────────────────────────────────────────────────────┘

    ┌─────────────────┐
    │   Étudiant      │
    ├─────────────────┤
    │ • Id (PK)       │
    │ • Matricule     │
    │ • Nom           │
    │ • Prénom        │
    └────────┬────────┘
             │
             │ 1 ──────────────────── N
             │
             ▼
    ┌─────────────────┐
    │  Inscription    │◀───────────────────┐
    ├─────────────────┤                    │
    │ • Id (PK)       │                    │
    │ • Date          │                    │
    │ • Montant       │                    │
    │ • EtudiantId    │ (FK)              │
    │ • ClasseId      │ (FK)              │
    │ • AnneeScoId    │ (FK)              │
    └────────┬────────┘                    │
             │                             │
             ├─────────────┬───────────────┘
             │             │
    1 ◀──────┘             └──────▶ 1
             │                     │
             ▼                     ▼
    ┌─────────────────┐   ┌─────────────────┐
    │    Classe       │   │ AnneeScolaire   │
    ├─────────────────┤   ├─────────────────┤
    │ • Id (PK)       │   │ • Id (PK)       │
    │ • Code          │   │ • Code          │
    │ • Libellé       │   │ • Libellé       │
    └─────────────────┘   │ • Statut        │
                          └────────┬────────┘
                                   │
                                   │ 1 ──────────────────── N
                                   │
                          ┌────────▼────────┐
                          │    Statut       │
                          ├─────────────────┤
                          │ • EnCours       │
                          │ • Cloturee      │
                          └─────────────────┘


    Relations:
    ──────────
    • Étudiant (1) ──────────────────── Inscription (N)   [OneToMany]
    • Classe (1) ──────────────────── Inscription (N)     [OneToMany]
    • AnneeScolaire (1) ──────────────────── Inscription (N)  [OneToMany]
    • Statut (1) ──────────────────── AnneeScolaire (N)   [OneToMany, Unidirectionnelle]
```

## Architecture en Couches

```
┌────────────────────────────────────────────────────────────┐
│                   Couche Présentation (UI)                │
│  ┌──────────────────────────────────────────────────────┐ │
│  │  Views (Razor)                                       │ │
│  │  • Creer.cshtml                                      │ │
│  │  • Lister.cshtml                                     │ │
│  │  • ListerParClasse.cshtml                            │ │
│  │  • Supprimer.cshtml                                  │ │
│  └──────────────────────────────────────────────────────┘ │
└────────────────────┬─────────────────────────────────────┘
                     │
                     ▼
┌────────────────────────────────────────────────────────────┐
│            Couche Contrôleur (Controllers)                │
│  ┌──────────────────────────────────────────────────────┐ │
│  │  InscriptionController                               │ │
│  │  • Creer()                                            │ │
│  │  • Lister()                                           │ │
│  │  • ListerParClasse()                                  │ │
│  │  • Supprimer()                                        │ │
│  └──────────────────────────────────────────────────────┘ │
└────────────────────┬─────────────────────────────────────┘
                     │
                     ▼
┌────────────────────────────────────────────────────────────┐
│         Couche Métier (Services/Business Logic)           │
│  ┌──────────────────────────────────────────────────────┐ │
│  │  IInscriptionService (Interface)                     │ │
│  │  └─ InscriptionService (Implémentation)              │ │
│  │     • GetInscriptionsParClasseAsync()                │ │
│  │     • GetInscriptionParIdAsync()                     │ │
│  │     • GetToutesLesInscriptionsAsync()                │ │
│  │     • AjouterInscriptionAsync()                      │ │
│  │     • SupprimerInscriptionAsync()                    │ │
│  │     • GetEtudiantsDisponiblesAsync()                 │ │
│  │     • GetClassesAsync()                              │ │
│  │     • GetAnneesScolaresActuelsAsync()                │ │
│  └──────────────────────────────────────────────────────┘ │
└────────────────────┬─────────────────────────────────────┘
                     │
                     ▼
┌────────────────────────────────────────────────────────────┐
│     Couche Data Access (Entity Framework Core)            │
│  ┌──────────────────────────────────────────────────────┐ │
│  │  ApplicationDbContext                                │ │
│  │  • DbSet<Étudiant>                                   │ │
│  │  • DbSet<Classe>                                     │ │
│  │  • DbSet<AnneeScolaire>                              │ │
│  │  • DbSet<Inscription>                                │ │
│  │                                                      │ │
│  │  OnModelCreating()                                   │ │
│  │  • Configuration des relations                       │ │
│  │  • Seeding des données                               │ │
│  └──────────────────────────────────────────────────────┘ │
└────────────────────┬─────────────────────────────────────┘
                     │
                     ▼
┌────────────────────────────────────────────────────────────┐
│            Couche Base de Données (SQL Server)            │
│  ┌──────────────────────────────────────────────────────┐ │
│  │  Examen_CSHARP_DB                                    │ │
│  │  • Table: Etudiants                                  │ │
│  │  • Table: Classes                                    │ │
│  │  • Table: AnneesScolaires                            │ │
│  │  • Table: Inscriptions                               │ │
│  │  • Index et contraintes de clés étrangères           │ │
│  └──────────────────────────────────────────────────────┘ │
└────────────────────────────────────────────────────────────┘
```

## Flux de Données

### Créer une Inscription
```
User
  │
  ▼ (POST /Inscription/Creer)
─────────────────────────────────────
  │
  ▼ InscriptionController.Creer()
  │ • Valide le modèle
  │ • Appelle le service
  │
  ▼ IInscriptionService.AjouterInscriptionAsync()
  │ • Valide les règles métier
  │ • Ajoute à DbContext
  │
  ▼ ApplicationDbContext.SaveChangesAsync()
  │ • Exécute INSERT SQL
  │
  ▼ SQL Server
  │ • INSERT INTO Inscriptions
  │
  ▼ Retour au View
  │
  ▼ Affichage du message de succès
```

### Lister les Inscriptions
```
User (GET /Inscription/Lister)
  │
  ▼ InscriptionController.Lister()
  │ • Appelle le service
  │
  ▼ IInscriptionService.GetToutesLesInscriptionsAsync()
  │ • Récupère les données
  │ • Charge les relations (Etudiant, Classe, AnneeScolaire)
  │
  ▼ ApplicationDbContext.Inscriptions
  │ • Exécute SELECT avec INCLUDE
  │
  ▼ SQL Server
  │ • SELECT avec JOINs
  │
  ▼ Retour List<Inscription>
  │
  ▼ Lister.cshtml
  │ • Affiche le tableau
  │ • Boutons de filtrage
```

### Lister par Classe
```
User (GET /Inscription/ListerParClasse?classeId=1)
  │
  ▼ InscriptionController.ListerParClasse(int classeId)
  │
  ▼ IInscriptionService.GetInscriptionsParClasseAsync(classeId)
  │ • Filtre par ClasseId
  │
  ▼ SQL Server
  │ • SELECT WHERE ClasseId = @classeId
  │
  ▼ ListerParClasse.cshtml
  │ • Affiche la classe et ses inscriptions
```

## Dépendances et Injection

```
Program.cs
    │
    ├─ builder.Services.AddDbContext<ApplicationDbContext>()
    │  └─ SQL Server Connection
    │
    └─ builder.Services.AddScoped<IInscriptionService, InscriptionService>()
       │
       ├─ InscriptionService(ApplicationDbContext context)
       │  │
       │  └─ ApplicationDbContext
       │     └─ SQL Server Database
       │
       └─ Injection dans InscriptionController
          │
          └─ InscriptionController(IInscriptionService service)
```

## Modèle de Données Détaillé

### Table: Etudiants
```sql
CREATE TABLE Etudiants (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Matricule NVARCHAR(MAX) NOT NULL,
    Nom NVARCHAR(MAX) NOT NULL,
    Prenom NVARCHAR(MAX) NOT NULL
);
```

### Table: Classes
```sql
CREATE TABLE Classes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(MAX) NOT NULL,
    Libelle NVARCHAR(MAX) NOT NULL
);
```

### Table: AnneesScolaires
```sql
CREATE TABLE AnneesScolaires (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(MAX) NOT NULL,
    Libelle NVARCHAR(MAX) NOT NULL,
    Statut INT NOT NULL  -- 0 = EnCours, 1 = Cloturee
);
```

### Table: Inscriptions
```sql
CREATE TABLE Inscriptions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Date DATETIME2 NOT NULL,
    Montant DECIMAL(18,2) NOT NULL,
    EtudiantId INT NOT NULL FOREIGN KEY REFERENCES Etudiants(Id) ON DELETE CASCADE,
    ClasseId INT NOT NULL FOREIGN KEY REFERENCES Classes(Id) ON DELETE CASCADE,
    AnneeScolaireId INT NOT NULL FOREIGN KEY REFERENCES AnneesScolaires(Id) ON DELETE CASCADE,
    
    INDEX IX_Inscriptions_EtudiantId (EtudiantId),
    INDEX IX_Inscriptions_ClasseId (ClasseId),
    INDEX IX_Inscriptions_AnneeScolaireId (AnneeScolaireId)
);
```

## Énumération Statut

```csharp
public enum Statut
{
    EnCours = 0,      // L'année scolaire est en cours
    Cloturee = 1      // L'année scolaire est terminée
}
```

Utilisée dans `AnneeScolaire.Statut` pour filtrer les années disponibles lors de la création d'une inscription.

---

**Format**: Plantuml/ASCII
**Mise à jour**: 9 janvier 2026
