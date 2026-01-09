using Microsoft.AspNetCore.Mvc;
using examen_csharp_sur_table.Models;
using examen_csharp_sur_table.Services;

namespace examen_csharp_sur_table.Controllers;

public class InscriptionController : Controller
{
    private readonly IInscriptionService _inscriptionService;
    private readonly ILogger<InscriptionController> _logger;

    public InscriptionController(IInscriptionService inscriptionService, ILogger<InscriptionController> logger)
    {
        _inscriptionService = inscriptionService;
        _logger = logger;
    }

    public async Task<IActionResult> Creer()
    {
        try
        {
            var etudiants = await _inscriptionService.GetEtudiantsDisponiblesAsync();
            var classes = await _inscriptionService.GetClassesAsync();
            var annees = await _inscriptionService.GetAnneesScolaresActuelsAsync();

            ViewBag.Etudiants = etudiants;
            ViewBag.Classes = classes;
            ViewBag.Annees = annees;

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement du formulaire d'inscription");
            TempData["Error"] = "Erreur lors du chargement du formulaire.";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Creer(Inscription inscription)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _inscriptionService.AjouterInscriptionAsync(inscription);
                TempData["Success"] = "Inscription créée avec succès.";
                return RedirectToAction("Lister");
            }

            var etudiants = await _inscriptionService.GetEtudiantsDisponiblesAsync();
            var classes = await _inscriptionService.GetClassesAsync();
            var annees = await _inscriptionService.GetAnneesScolaresActuelsAsync();

            ViewBag.Etudiants = etudiants;
            ViewBag.Classes = classes;
            ViewBag.Annees = annees;

            return View(inscription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création d'une inscription");
            TempData["Error"] = "Erreur lors de la création de l'inscription.";
            return RedirectToAction("Creer");
        }
    }

    public async Task<IActionResult> ListerParClasse(int classeId)
    {
        try
        {
            var inscriptions = await _inscriptionService.GetInscriptionsParClasseAsync(classeId);
            var classes = await _inscriptionService.GetClassesAsync();
            var classe = classes.FirstOrDefault(c => c.Id == classeId);

            if (classe == null)
            {
                TempData["Error"] = "Classe non trouvée.";
                return RedirectToAction("Lister");
            }

            ViewBag.Classe = classe;
            ViewBag.Classes = classes;

            return View(inscriptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement des inscriptions par classe");
            TempData["Error"] = "Erreur lors du chargement des inscriptions.";
            return RedirectToAction("Lister");
        }
    }

    public async Task<IActionResult> Lister()
    {
        try
        {
            var inscriptions = await _inscriptionService.GetToutesLesInscriptionsAsync();
            var classes = await _inscriptionService.GetClassesAsync();
            ViewBag.Classes = classes;

            return View(inscriptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement de toutes les inscriptions");
            TempData["Error"] = "Erreur lors du chargement des inscriptions.";
            return RedirectToAction("Index", "Home");
        }
    }

    public async Task<IActionResult> Supprimer(int id)
    {
        try
        {
            var inscription = await _inscriptionService.GetInscriptionParIdAsync(id);
            if (inscription == null)
            {
                TempData["Error"] = "Inscription non trouvée.";
                return RedirectToAction("Lister");
            }

            return View(inscription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement de l'inscription à supprimer");
            TempData["Error"] = "Erreur lors du chargement de l'inscription.";
            return RedirectToAction("Lister");
        }
    }

    [HttpPost]
    [ActionName("Supprimer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SupprimerConfirme(int id)
    {
        try
        {
            await _inscriptionService.SupprimerInscriptionAsync(id);
            TempData["Success"] = "Inscription supprimée avec succès.";
            return RedirectToAction("Lister");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression de l'inscription");
            TempData["Error"] = "Erreur lors de la suppression de l'inscription.";
            return RedirectToAction("Lister");
        }
    }
}
    {
        try
        {
            var etudiants = await _inscriptionService.GetEtudiantsDisponiblesAsync();
            var classes = await _inscriptionService.GetClassesAsync();
            var annees = await _inscriptionService.GetAnneesScolaresActuelsAsync();

            ViewBag.Etudiants = etudiants;
            ViewBag.Classes = classes;
            ViewBag.Annees = annees;

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement du formulaire d'inscription");
            TempData["Error"] = "Erreur lors du chargement du formulaire.";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Creer(Inscription inscription)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _inscriptionService.AjouterInscriptionAsync(inscription);
                TempData["Success"] = "Inscription créée avec succès.";
                return RedirectToAction("Lister");
            }

            var etudiants = await _inscriptionService.GetEtudiantsDisponiblesAsync();
            var classes = await _inscriptionService.GetClassesAsync();
            var annees = await _inscriptionService.GetAnneesScolaresActuelsAsync();

            ViewBag.Etudiants = etudiants;
            ViewBag.Classes = classes;
            ViewBag.Annees = annees;

            return View(inscription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création d'une inscription");
            TempData["Error"] = "Erreur lors de la création de l'inscription.";
            return RedirectToAction("Creer");
        }
    }

    public async Task<IActionResult> ListerParClasse(int classeId)
    {
        try
        {
            var inscriptions = await _inscriptionService.GetInscriptionsParClasseAsync(classeId);
            var classes = await _inscriptionService.GetClassesAsync();
            var classe = classes.FirstOrDefault(c => c.Id == classeId);

            if (classe == null)
            {
                TempData["Error"] = "Classe non trouvée.";
                return RedirectToAction("Lister");
            }

            ViewBag.Classe = classe;
            ViewBag.Classes = classes;

            return View(inscriptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement des inscriptions par classe");
            TempData["Error"] = "Erreur lors du chargement des inscriptions.";
            return RedirectToAction("Lister");
        }
    }

    public async Task<IActionResult> Lister()
    {
        try
        {
            var inscriptions = await _inscriptionService.GetToutesLesInscriptionsAsync();
            var classes = await _inscriptionService.GetClassesAsync();
            ViewBag.Classes = classes;

            return View(inscriptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement de toutes les inscriptions");
            TempData["Error"] = "Erreur lors du chargement des inscriptions.";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: Inscription/Supprimer/5
    {
        try
        {
            var inscription = await _inscriptionService.GetInscriptionParIdAsync(id);
            if (inscription == null)
            {
                TempData["Error"] = "Inscription non trouvée.";
                return RedirectToAction("Lister");
            }

            return View(inscription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors du chargement de l'inscription à supprimer");
            TempData["Error"] = "Erreur lors du chargement de l'inscription.";
            return RedirectToAction("Lister");
        }
    }

    // POST: Inscription/Supprimer/5
    [ActionName("Supprimer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SupprimerConfirme(int id)
    {
        try
        {
            await _inscriptionService.SupprimerInscriptionAsync(id);
            TempData["Success"] = "Inscription supprimée avec succès.";
            return RedirectToAction("Lister");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression de l'inscription");
            TempData["Error"] = "Erreur lors de la suppression de l'inscription.";
            return RedirectToAction("Lister");
        }
    }
}
