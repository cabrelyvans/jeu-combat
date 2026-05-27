using CombatTourParTour.Application.Combat;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Actions
{
    public interface ICombatAction
    {
        string Nom { get; }
        
        bool PeutExecuter(CombatContext contexte);
        
        CombatResult Executer(CombatContext contexte, Personnage cible);
    }
}