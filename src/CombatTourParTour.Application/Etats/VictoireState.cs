using CombatTourParTour.Application.Combat;

namespace CombatTourParTour.Application.Etats
{
    public class VictoireState : ICombatState
    {
        public void Entrer(CombatContext contexte)
        {
            // Fin du jeu globale célébrée
        }

        public void Executer(CombatContext contexte) { }

        public ICombatState? DéterminerEtatSuivant(CombatContext contexte)
        {
            return null;
        }
    }
}
