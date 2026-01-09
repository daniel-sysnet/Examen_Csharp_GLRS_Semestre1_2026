# Guide de Démarrage Rapide

## Prérequis
- .NET 8.0 SDK ([Télécharger](https://dotnet.microsoft.com/download/dotnet/8.0))
- Visual Studio Code ou Visual Studio
- SQL Server (LocalDB est inclus avec Visual Studio)

## Installation Rapide

### 1. Restaurer les dépendances
```bash
cd /home/daniel/Documents/Etudes_&_Projets/C#/examen_csharp_sur_table
dotnet restore
```

### 2. Créer la base de données
Les migrations sont appliquées automatiquement au démarrage. Sinon, exécutez:
```bash
dotnet ef database update
```

### 3. Lancer l'application
```bash
dotnet run
```

L'application s'ouvrira automatiquement à:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000

## Navigation

### Page d'Accueil
- Accès via http://localhost:5000 (ou https://localhost:5001)
- Deux boutons principaux:
  - "Créer une Inscription"
  - "Consulter les Inscriptions"

### Créer une Inscription
1. Cliquez sur "Créer une Inscription"
2. Remplissez le formulaire:
   - **Étudiant**: Sélectionnez dans la liste déroulante
   - **Classe**: Sélectionnez une classe
   - **Année Scolaire**: Seules les années "En cours" sont disponibles
   - **Montant**: Entrez le montant (format décimal: 500.00)
3. Cliquez sur "Créer l'Inscription"

### Lister les Inscriptions
1. Cliquez sur "Consulter"
2. Utilisez les boutons "Filtrer par Classe" pour:
   - "Toutes les Classes" - voir toutes les inscriptions
   - Cliquer sur un code classe (L1-INFO, L2-INFO, M1-MIAGE)
3. Cliquez sur "Supprimer" pour supprimer une inscription

### Supprimer une Inscription
1. Depuis la liste, cliquez sur "Supprimer"
2. Vérifiez les détails affichés
3. Cliquez sur "Confirmer la Suppression"

## Structure des Fichiers Importants

```
examen_csharp_sur_table/
├── Models/                      # Modèles de données
│   ├── Etudiant.cs
│   ├── Classe.cs
│   ├── AnneeScolaire.cs
│   ├── Inscription.cs
│   └── Statut.cs
├── Services/                    # Couche métier
│   ├── IInscriptionService.cs   # Interface
│   └── InscriptionService.cs    # Implémentation
├── Data/
│   └── ApplicationDbContext.cs  # Contexte Entity Framework
├── Controllers/
│   └── InscriptionController.cs
├── Views/Inscription/           # Vues pour les inscriptions
│   ├── Creer.cshtml
│   ├── Lister.cshtml
│   ├── ListerParClasse.cshtml
│   └── Supprimer.cshtml
├── Migrations/                  # Migrations Entity Framework
├── Program.cs                   # Configuration de l'application
├── appsettings.json             # Configuration (chaîne de connexion)
└── examen_csharp_sur_table.csproj
```

## Données de Test

Des données sont pré-chargées dans la base:

**5 Étudiants:**
- STD001: Jean Dupont
- STD002: Marie Martin
- STD003: Pierre Bernard
- STD004: Sophie Thomas
- STD005: Luc Robert

**3 Classes:**
- L1-INFO: Licence 1 Informatique
- L2-INFO: Licence 2 Informatique
- M1-MIAGE: Master 1 MIAGE

**2 Années Scolaires:**
- 2023-2024 (Cloturée)
- 2024-2025 (En cours)

**5 Inscriptions d'exemple**

## Dépannage

### La base de données ne se crée pas
```bash
# Supprimer les migrations locales
dotnet ef migrations remove

# Exécuter la migration initiale
dotnet ef database update
```

### Port 5000/5001 déjà utilisé
Modifier `Properties/launchSettings.json`:
```json
"applicationUrl": "https://localhost:YOUR_PORT;http://localhost:YOUR_PORT"
```

### Erreur de connexion SQL Server
Vérifier la chaîne de connexion dans `appsettings.json`:
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=examen_csharp_db;Trusted_Connection=true;"
```

## Raccourcis Utiles

- **Recompiler**: `dotnet build`
- **Nettoyer**: `dotnet clean`
- **Lancer les tests**: `dotnet test`
- **Voir les services**: `dotnet run --verbose`

## Documentation Complète

Voir [README.md](README.md) pour la documentation complète du projet.
