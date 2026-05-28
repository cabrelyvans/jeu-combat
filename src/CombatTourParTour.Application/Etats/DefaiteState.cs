using CombatTourParTour.Application.Combat;

namespace CombatTourParTour.Application.Etats
{
    public class DefaiteState : ICombatState
    {
        public void Entrer(CombatContext contexte)
        {
            // Fin du jeu globale célébrée
        }

        public void Executer(CombatContext contexte)
        {
            // Pas d'action particulière à exécuter, le combat s'arrête
        }

        public ICombatState? DéterminerEtatSuivant(CombatContext contexte)
        {
            return null;
        }
    }
}
