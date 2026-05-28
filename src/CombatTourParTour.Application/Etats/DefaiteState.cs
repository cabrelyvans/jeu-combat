using CombatTourParTour.Application.Combat;

namespace CombatTourParTour.Application.Etats
{
    public class DefaiteState : ICombatState
    {
        public void Entrer(CombatContext contexte)
        {
            // Cet état n'affiche rien directement sur la console (respect du SRP)
            // Il sert juste à marquer la fin du jeu en mode défaite
        }

        public void Executer(CombatContext contexte)
        {
            // Pas d'action particulière à exécuter, le combat s'arrête
        }

        public ICombatState DéterminerEtatSuivant(CombatContext contexte)
        {
            // C'est un état terminal, il n'y a pas d'état après la défaite
            return null; 
        }
    }
}