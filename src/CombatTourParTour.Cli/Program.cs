using CombatTourParTour.Application.Actions;
using CombatTourParTour.Application.Combat;
using CombatTourParTour.Application.Commands;
using CombatTourParTour.Application.Factories;
using CombatTourParTour.Domain.Enums;
using CombatTourParTour.Infrastructure.Console;

// 1. Initialisation des services de l'Infrastructure et des Factories
var uiService = new ConsoleCombatUiService();
var heroFactory = new HeroFactory();
var ennemiFactory = new EnnemiFactory();

Console.Clear();
Console.WriteLine("══════════════════════════════════════════");
Console.WriteLine("       BIENVENUE DANS LE JEU DE COMBAT     ");
Console.WriteLine("══════════════════════════════════════════\n");

// 2. Saisie du nom du Héros
string nomHeros = "";
while (string.IsNullOrWhiteSpace(nomHeros))
{
    Console.Write(" Entrez le nom de votre héros : ");
    nomHeros = Console.ReadLine();
}

// 3. Choix de la classe du Héros
ClasseHeros classeChoisie = ClasseHeros.Guerrier;
bool choixClasseValide = false;

while (!choixClasseValide)
{
    Console.WriteLine("\n Choisissez votre classe :");
    Console.WriteLine("  1. Guerrier (120 PV / 18 ATQ / Frappe lourde)");
    Console.WriteLine("  2. Mage     (80 PV  / 12 ATQ / Éclair)");
    Console.WriteLine("  3. Voleur   (90 PV  / 14 ATQ / Coup critique)");
    Console.Write(" Votre choix (1-3) : ");

    string saisie = Console.ReadLine();
    if (saisie == "1")
    {
        classeChoisie = ClasseHeros.Guerrier;
        choixClasseValide = true;
    }
    else if (saisie == "2")
    {
        classeChoisie = ClasseHeros.Mage;
        choixClasseValide = true;
    }
    else if (saisie == "3")
    {
        classeChoisie = ClasseHeros.Voleur;
        choixClasseValide = true;
    }
    else
    {
        Console.WriteLine("❌ Choix invalide.");
    }
}

// 4. Fabrication du héros grâce à notre Factory
var heros = heroFactory.CreerHeros(nomHeros, classeChoisie);

// 5. Configuration dynamique du Pattern Strategy pour la compétence spéciale
ICombatAction competenceSpeciale = classeChoisie switch
{
    ClasseHeros.Guerrier => new CompetenceGuerrierAction(heros),
    ClasseHeros.Mage => new CompetenceMageAction(heros),
    ClasseHeros.Voleur => new CompetenceVoleurAction(heros),
    _ => throw new ArgumentException(),
};

// 6. Préparation du catalogue de Commandes pour le joueur
var commandesJoueur = new List<ICommand>
{
    new AttaquerCommand(new AttaqueBasiqueAction(heros)),
    new UtiliserCompetenceCommand(competenceSpeciale),
    new SoignerCommand(new SoinAction(heros)),
};

// 7. Initialisation de la Vague 1 (1 ennemi faible)
var ennemisInitiaux = new List<CombatTourParTour.Domain.Entites.Ennemi>
{
    ennemiFactory.CreerEnnemi(TypeEnnemi.GobelinFaible),
};

// 8. Instanciation du moteur et lancement de la partie !
var engine = new CombatEngine(uiService, ennemiFactory, commandesJoueur);
engine.DemarrerCombat(heros, ennemisInitiaux);

// 9. Message de fin de partie
Console.Clear();
if (heros.EstVivant)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n 🎉 FÉLICITATIONS ! Vous avez terrassé le Boss et sauvé le royaume ! 🎉");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\n 💀 GONE OVER... Votre héros a succombé à ses blessures. 💀");
}
Console.ResetColor();
Console.WriteLine("\n Merci d'avoir joué ! Appuyez sur une touche pour quitter.");
Console.ReadKey();
