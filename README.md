# Application Web C# - Gestion des Inscriptions

## Description
Application web ASP.NET Core 8.0 pour gérer les inscriptions des étudiants à des classes, utilisant Entity Framework Core et SQL Server.

## Architecture
- **Modèle par couche** (Layered Architecture)
- **Principes SOLID** respectés
- **Patterns**: Repository implicite avec EF Core, Service Layer

## Structure du Projet

### Models (`/Models`)
- **Etudiant.cs**: Modèle d'étudiant avec propriétés matricule, nom, prénom
- **Classe.cs**: Modèle de classe avec code et libellé
- **AnneeScolaire.cs**: Modèle d'année scolaire avec statut (énumération)
- **Inscription.cs**: Modèle d'inscription reliant étudiant, classe et année scolaire
- **Statut.cs**: Énumération des statuts (EnCours, Cloturee)

### Services (`/Services`)
- **IInscriptionService.cs**: Interface définissant les contrats de service
- **InscriptionService.cs**: Implémentation du service d'inscription

### Data (`/Data`)
- **ApplicationDbContext.cs**: Contexte Entity Framework avec:
  - Configuration des relations
  - Initialisation des données (seeding)
  - Création des migrations

### Controllers (`/Controllers`)
- **InscriptionController.cs**: Gestion des actions liées aux inscriptions
  - `Creer`: Créer une nouvelle inscription
  - `Lister`: Lister toutes les inscriptions
  - `ListerParClasse`: Filtrer les inscriptions par classe
  - `Supprimer`: Supprimer une inscription

### Views (`/Views/Inscription`)
- **Creer.cshtml**: Formulaire de création d'inscription
- **Lister.cshtml**: Affichage de toutes les inscriptions avec filtrage par classe
- **ListerParClasse.cshtml**: Affichage des inscriptions filtrées par classe
- **Supprimer.cshtml**: Confirmation avant suppression d'une inscription

### Migrations (`/Migrations`)
- **20260109000000_InitialCreate.cs**: Migration initiale créant les tables
- **InitialCreateModelSnapshot.cs**: Snapshot du modèle

## Relations
```
Étudiant (1) ──────────── (N) Inscription
Classe (1) ──────────────── (N) Inscription
AnneeScolaire (1) ───────── (N) Inscription
Statut (1) ──────────────── (N) AnneeScolaire (unidirectionnelle)
```

## Données Initialisées

### Années Scolaires
- 2023-2024 (Cloturée)
- 2024-2025 (En cours)

### Classes
- L1-INFO: Licence 1 Informatique
- L2-INFO: Licence 2 Informatique
- M1-MIAGE: Master 1 MIAGE

### Étudiants
- STD001: Jean Dupont
- STD002: Marie Martin
- STD003: Pierre Bernard
- STD004: Sophie Thomas
- STD005: Luc Robert

### Inscriptions
5 inscriptions initiales pour tester le système

## Fonctionnalités

### 1. Créer une Inscription
- Sélectionner un étudiant
- Choisir une classe
- Sélectionner une année scolaire (filtrage sur statut "En cours")
- Entrer le montant
- Validation complète du formulaire

### 2. Lister les Inscriptions
- Affichage de toutes les inscriptions
- Filtrage par classe via boutons
- Affichage du détail: date, étudiant, classe, année, montant
- Possibilité de supprimer une inscription

### 3. Lister par Classe
- Affichage des inscriptions d'une classe spécifique
- Navigation entre les classes
- Total d'inscriptions affiché

## Configuration

### Chaîne de Connexion (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=examen_csharp_db;Trusted_Connection=true;"
  }
}
```

### NuGet Packages
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
```

## Installation et Lancement

1. **Restaurer les packages NuGet**
   ```bash
   dotnet restore
   ```

2. **Appliquer les migrations**
   ```bash
   dotnet ef database update
   ```
   
   Ou les migrations sont automatiquement appliquées au démarrage.

3. **Lancer l'application**
   ```bash
   dotnet run
   ```

4. **Accéder à l'application**
   - http://localhost:5000 (HTTP)
   - https://localhost:5001 (HTTPS)

## Routes Disponibles
- `/Inscription/Creer` - Créer une inscription
- `/Inscription/Lister` - Lister toutes les inscriptions
- `/Inscription/ListerParClasse/{classeId}` - Filtrer par classe
- `/Inscription/Supprimer/{id}` - Supprimer une inscription

## Principes SOLID Respectés

- **S**ingle Responsibility: Chaque classe a une unique responsabilité
- **O**pen/Closed: Architecture extensible sans modification
- **L**iskov Substitution: Interface IInscriptionService bien respectée
- **I**nterface Segregation: Interfaces spécifiques et concises
- **D**ependency Inversion: Injection de dépendances via DI container

## Gestion des Erreurs
- Try-catch dans les actions du contrôleur
- Messages d'erreur affichés à l'utilisateur
- Logging des erreurs
- Redirections appropriées en cas d'erreur
