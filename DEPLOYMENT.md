# Instructions de Compilation et Déploiement

## Prérequis Système

- **OS**: Windows, Linux, macOS
- **.NET SDK**: 8.0 ou supérieur
- **SQL Server**: LocalDB (inclus avec Visual Studio) ou serveur SQL existant
- **RAM**: Minimum 2 GB
- **Espace disque**: ~500 MB pour le projet

## Vérification des Prérequis

```bash
# Vérifier la version de .NET
dotnet --version

# Doit afficher: 8.0.x ou supérieur
```

## Étapes de Compilation

### 1. Naviguer vers le répertoire du projet
```bash
cd /home/daniel/Documents/Etudes_&_Projets/C#/examen_csharp_sur_table
```

### 2. Restaurer les dépendances NuGet
```bash
dotnet restore
```
Cette commande récupère les packages NuGet nécessaires depuis le référentiel en ligne.

### 3. Nettoyer les fichiers de compilation antérieurs (optionnel)
```bash
dotnet clean
```

### 4. Compiler le projet
```bash
dotnet build
```
Compile le projet et détecte les erreurs de compilation.

### 5. Créer/Mettre à jour la base de données
```bash
# Option 1: Application des migrations (recommandé)
dotnet ef database update

# Option 2: Suppression et recréation (si problèmes)
dotnet ef database drop --force
dotnet ef database update
```

## Lancement de l'Application

### En mode Développement
```bash
dotnet run
```

L'application démarre automatiquement à:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001

### En mode Release (Optimisé pour la production)
```bash
dotnet run --configuration Release
```

### En arrière-plan (Linux/Mac)
```bash
dotnet run &
```

## Vérification de la Base de Données

### Avec SQL Server Management Studio (SSMS)
```
Serveur: (localdb)\mssqllocaldb
Base de données: examen_csharp_db
Authentification: Windows
```

### En ligne de commande
```bash
# Affiche les informations sur les migrations
dotnet ef migrations list

# Affiche le script SQL pour une migration
dotnet ef migrations script --idempotent > migration.sql
```

## Testing de la Fonctionnalité

### Test 1: Créer une Inscription
1. Naviguer vers http://localhost:5000/Inscription/Creer
2. Remplir le formulaire:
   - Étudiant: Jean Dupont
   - Classe: L1-INFO
   - Année Scolaire: 2024-2025
   - Montant: 500.00
3. Cliquer "Créer l'Inscription"
4. Vérifier le message de succès

### Test 2: Lister les Inscriptions
1. Naviguer vers http://localhost:5000/Inscription/Lister
2. Vérifier que 6 inscriptions s'affichent (5 initiales + 1 créée)
3. Cliquer sur les filtres de classe
4. Vérifier que les inscriptions sont correctement filtrées

### Test 3: Lister par Classe
1. Depuis la liste, cliquer sur "L1-INFO"
2. Vérifier que l'inscription créée s'affiche
3. Naviguer entre les autres classes

### Test 4: Supprimer une Inscription
1. Cliquer sur "Supprimer" pour une inscription
2. Vérifier les détails affichés
3. Cliquer "Confirmer la Suppression"
4. Vérifier que l'inscription disparaît

## Correction des Problèmes Courants

### Erreur: "Cannot connect to SQL Server"
```bash
# Vérifier que le service SQL Server est en cours d'exécution
# Windows:
services.msc

# Vérifier la chaîne de connexion dans appsettings.json
```

### Erreur: "The database already exists"
```bash
# Supprimer la base de données et la recréer
dotnet ef database drop --force
dotnet ef database update
```

### Erreur: "Port 5000 is already in use"
Modifier `Properties/launchSettings.json`:
```json
"applicationUrl": "https://localhost:5002;http://localhost:5001"
```

### Erreur: "EF Core Migrations not found"
```bash
# Recréer les migrations
dotnet ef migrations add InitialCreate --force
dotnet ef database update
```

## Compilation pour Production

### Créer un bundle standalone
```bash
# Pour Windows
dotnet publish -c Release -r win-x64 --self-contained

# Pour Linux
dotnet publish -c Release -r linux-x64 --self-contained

# Pour macOS
dotnet publish -c Release -r osx-x64 --self-contained
```

Le bundle de publication sera dans `bin/Release/net8.0/publish/`

### Taille attendue
```
Debug: ~150 MB (dossier bin)
Release: ~80 MB (optimisé)
Standalone: ~200 MB (incluant .NET Runtime)
```

## Performance et Optimisations

### Mode de Démarrage
- **Debug**: Plus lent mais meilleur pour le débogage
- **Release**: Plus rapide (~10-20x), recommandé pour la production

### Fichiers de Configuration
```json
// appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Monitoring et Logs

### Affichage des Logs au Démarrage
```bash
dotnet run --verbosity detailed
```

### Logs dans le fichier
Les logs sont affichés dans la console. Pour les sauvegarder:
```bash
dotnet run > app.log 2>&1
```

## Tests Unitaires (Si implémentés)

```bash
# Exécuter les tests
dotnet test

# Tests avec couverture de code
dotnet test /p:CollectCoverage=true
```

## Commit et Push (Git)

```bash
# Vérifier le statut
git status

# Ajouter les fichiers
git add .

# Committer les changements
git commit -m "Ajout de la gestion des inscriptions"

# Pousser vers le serveur
git push origin main
```

## Déploiement sur IIS (Windows)

1. Publier en mode Release:
```bash
dotnet publish -c Release
```

2. Installer le .NET Hosting Bundle
3. Configurer un Site Web dans IIS pointant vers le dossier `publish`
4. Configurer le Pool d'Applications en `.NET CLR` ou sans CLR managé

## Déploiement sur Linux

```bash
# Créer une publication
dotnet publish -c Release -r linux-x64

# Créer un service systemd
sudo nano /etc/systemd/system/examen-csharp.service

# Contenu:
[Unit]
Description=Examen CSHARP Application
After=network.target

[Service]
Type=notify
ExecStart=/path/to/examen_csharp_sur_table
WorkingDirectory=/path/to/
Restart=always
User=www-data

[Install]
WantedBy=multi-user.target

# Activer le service
sudo systemctl daemon-reload
sudo systemctl enable examen-csharp
sudo systemctl start examen-csharp
```

## Sauvegarde de la Base de Données

```bash
# SQL Server - Sauvegarde
sqlcmd -S (localdb)\mssqllocaldb -E -Q "BACKUP DATABASE examen_csharp_db TO DISK='backup.bak'"

# SQL Server - Restauration
sqlcmd -S (localdb)\mssqllocaldb -E -Q "RESTORE DATABASE examen_csharp_db FROM DISK='backup.bak'"
```

## Nettoyage des Fichiers Temporaires

```bash
# Supprimer les fichiers de build
dotnet clean

# Supprimer le cache NuGet
dotnet nuget locals all --clear

# Supprimer le dossier bin et obj
rm -r bin obj
```

## Checklist de Déploiement

- [ ] Code compilé sans erreurs (`dotnet build`)
- [ ] Base de données créée et seedée (`dotnet ef database update`)
- [ ] Application testée en développement (`dotnet run`)
- [ ] Toutes les fonctionnalités vérifiées
- [ ] Chaîne de connexion SQL Server configurée
- [ ] Logs activés et fonctionnels
- [ ] Messages d'erreur configurés
- [ ] Performance testée
- [ ] Base de données sauvegardée
- [ ] Code pushé vers le serveur Git

---

**Dernière mise à jour**: 9 janvier 2026
**Version .NET**: 8.0
**Environnement testé**: Windows/Linux/macOS
