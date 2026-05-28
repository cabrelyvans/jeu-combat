using CombatTourParTour.Application.Actions;
using CombatTourParTour.Application.Combat;
using CombatTourParTour.Application.Services;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Etats
{
    public class TourEnnemiState : ICombatState
    {
        private readonly ICombatUiService _uiService;

        public TourEnnemiState(ICombatUiService uiService)
        {
            _uiService = uiService;
        }

        public void Entrer(CombatContext contexte)
        {
          
        }

        public void Executer(CombatContext contexte)
        {
            foreach (var ennemi in contexte.Ennemis.Where(e => e.EstVivant))
            {
                if (!contexte.Heros.EstVivant) break;

                ICombatAction attaqueEnnemi = new AttaqueBasiqueAction(ennemi);
                CombatResult resultat = attaqueEnnemi.Executer(contexte, contexte.Heros);

                _uiService.AfficherResultat(resultat);
                
                
                Thread.Sleep(1000); 
            }

            if (contexte.Heros.EstVivant)
            {
                contexte.Heros.ModifierCooldown(-1);
                contexte.NumeroTour++; 
            }
        }

        public ICombatState DéterminerEtatSuivant(CombatContext contexte)
        {
            if (!contexte.Heros.EstVivant)
            {
                return new DefaiteState();
            }

            return null; 
        }
    }
}