using CombatTourParTour.Application.Combat;
using CombatTourParTour.Application.Commands;
using CombatTourParTour.Application.Services;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Infrastructure.Console
{
    public class ConsoleCombatUiService : ICombatUiService
    {
        public void AfficherEtatCombat(CombatContext contexte)
        {
            System.Console.Clear();
            System.Console.WriteLine("══════════════════════════════════════════");
            System.Console.WriteLine(
                $"  VAGUE {contexte.VagueActuelle}/3 — Tour du joueur (Tour {contexte.NumeroTour})"
            );
            System.Console.WriteLine("══════════════════════════════════════════");

            // Affichage du Héros
            System.Console.WriteLine(
                $"  Héros : {contexte.Heros.Nom} ({contexte.Heros.Classe})     PV : {contexte.Heros.PointsDeVieActuels}/{contexte.Heros.PointsDeVieMax}"
            );
            System.Console.WriteLine(
                $"  Cooldown compétence : {contexte.Heros.CooldownCompetence} tour(s)"
            );
            System.Console.WriteLine($"  Soins restants : {contexte.Heros.SoinsRestants}");
            System.Console.WriteLine();

            // Affichage des Ennemis vivants
            System.Console.WriteLine("  Ennemis :");
            for (int i = 0; i < contexte.Ennemis.Count; i++)
            {
                var ennemi = contexte.Ennemis[i];
                if (ennemi.EstVivant)
                {
                    System.Console.WriteLine(
                        $"    [{i + 1}] {ennemi.Nom}     PV : {ennemi.PointsDeVieActuels}/{ennemi.PointsDeVieMax}"
                    );
                }
                else
                {
                    System.Console.WriteLine($"    [{i + 1}] {ennemi.Nom}     ❌ MORT");
                }
            }
            System.Console.WriteLine();
        }

        public ICommand ChoisirCommande(List<ICommand> commandesDisponibles, CombatContext contexte)
        {
            while (true)
            {
                System.Console.WriteLine("  Actions :");
                for (int i = 0; i < commandesDisponibles.Count; i++)
                {
                    var cmd = commandesDisponibles[i];
                    // Si la commande n'est pas disponible (ex: cooldown > 0), on l'indique
                    string statut = cmd.PeutExecuter(contexte) ? "" : " (Indisponible)";
                    System.Console.WriteLine($"    {i + 1}. {cmd.Description}{statut}");
                }

                System.Console.Write("  Votre choix : ");
                string saisie = System.Console.ReadLine();

                if (
                    int.TryParse(saisie, out int choix)
                    && choix >= 1
                    && choix <= commandesDisponibles.Count
                )
                {
                    var commandeSelectionnee = commandesDisponibles[choix - 1];

                    if (commandeSelectionnee.PeutExecuter(contexte))
                    {
                        return commandeSelectionnee;
                    }
                    else
                    {
                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine(
                            "  ❌ Action impossible pour le moment (cooldown ou limite atteinte).\n"
                        );
                        System.Console.ResetColor();
                    }
                }
                else
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(
                        "  ❌ Choix invalide. Veuillez entrer un nombre correct.\n"
                    );
                    System.Console.ResetColor();
                }
            }
        }

        public Ennemi ChoisirCible(List<Ennemi> ennemisVivants)
        {
            if (ennemisVivants.Count == 1)
            {
                return ennemisVivants[0]; // S'il n'y a qu'un seul ennemi, on le cible automatiquement
            }

            while (true)
            {
                System.Console.Write($"  Choisissez une cible (1 à {ennemisVivants.Count}) : ");
                string saisie = System.Console.ReadLine();

                if (
                    int.TryParse(saisie, out int choix)
                    && choix >= 1
                    && choix <= ennemisVivants.Count
                )
                {
                    return ennemisVivants[choix - 1];
                }

                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("  ❌ Cible invalide.");
                System.Console.ResetColor();
            }
        }

        public void AfficherResultat(CombatResult resultat)
        {
            System.Console.WriteLine();
            if (resultat.EstUnSoin)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.Yellow;
            }

            // Affiche le message explicite de l'action
            System.Console.WriteLine($"  » {resultat.Message}");
            System.Console.ResetColor();
            System.Console.WriteLine("\n  Appuyez sur Entrée pour continuer...");
            System.Console.ReadLine();
        }
    }
}
