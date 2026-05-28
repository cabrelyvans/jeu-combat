using CombatTourParTour.Application.Combat;
using CombatTourParTour.Application.Commands;
using CombatTourParTour.Application.Services;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Etats
{
    public class TourJoueurState : ICombatState
    {
        private readonly ICombatUiService _uiService;
        private readonly List<ICommand> _commandes;
        private bool _actionExécutee;

        public TourJoueurState(ICombatUiService uiService, List<ICommand> commandes)
        {
            _uiService = uiService;
            _commandes = commandes;
            _actionExécutee = false;
        }

        public void Entrer(CombatContext contexte)
        {
            _actionExécutee = false;
            _uiService.AfficherEtatCombat(contexte);
        }

        public void Executer(CombatContext contexte)
        {
            ICommand commandeChoisie = _uiService.ChoisirCommande(_commandes, contexte);

            if (commandeChoisie is AttaquerCommand attaquerCmd)
            {
                Ennemi cible = _uiService.ChoisirCible(contexte.Ennemis.Where(e => e.EstVivant).ToList());
                attaquerCmd.DefinirCible(cible);
            }
            else if (commandeChoisie is UtiliserCompetenceCommand competenceCmd)
            {
                Ennemi cible = _uiService.ChoisirCible(contexte.Ennemis.Where(e => e.EstVivant).ToList());
                competenceCmd.DefinirCible(cible);
            }

            CombatResult resultat = commandeChoisie.Executer(contexte);

            _uiService.AfficherResultat(resultat);
            
            _actionExécutee = true;
        }

        public ICombatState DéterminerEtatSuivant(CombatContext contexte)
        {
            if (!_actionExécutee) return this;

            if (contexte.Ennemis.All(e => !e.EstVivant))
            {
                if (contexte.VagueActuelle == 3)
                {
                    return new VictoireState(); 
                }
                return new EntreVaguesState(null); 
            }

            return new TourEnnemiState(_uiService);
        }
    }
}