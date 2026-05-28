using CombatTourParTour.Application.Combat;

namespace CombatTourParTour.Application.Etats
{
    public interface ICombatState
    {
        void Entrer(CombatContext contexte);

        void Executer(CombatContext contexte);

        ICombatState DéterminerEtatSuivant(CombatContext contexte);
    }
}