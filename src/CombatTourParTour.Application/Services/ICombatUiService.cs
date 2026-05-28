using CombatTourParTour.Application.Combat;
using CombatTourParTour.Application.Commands;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Services
{
    public interface ICombatUiService
    {
        void AfficherEtatCombat(CombatContext contexte);

        ICommand ChoisirCommande(List<ICommand> commandesDisponibles, CombatContext contexte);

        Ennemi ChoisirCible(List<Ennemi> ennemisVivants);

        void AfficherResultat(CombatResult resultat);
    }
}