using CombatTourParTour.Application.Combat;
using CombatTourParTour.Application.Commands;
using CombatTourParTour.Application.Etats;
using CombatTourParTour.Application.Factories;
using CombatTourParTour.Application.Services;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Combat
{
    public class CombatEngine
    {
        private readonly ICombatUiService _uiService;
        private readonly IEnnemiFactory _ennemiFactory;
        private ICombatState? _etatCourant;
        private readonly List<ICommand> _commandesJoueur;

        public CombatEngine(
            ICombatUiService uiService,
            IEnnemiFactory ennemiFactory,
            List<ICommand> commandesJoueur
        )
        {
            _uiService = uiService;
            _ennemiFactory = ennemiFactory;
            _commandesJoueur = commandesJoueur;
        }

        public void DemarrerCombat(Heros heros, List<Ennemi> ennemisInitiaux)
        {
            var contexte = new CombatContext(heros, ennemisInitiaux, vagueActuelle: 1);

            _etatCourant = new TourJoueurState(_uiService, _commandesJoueur);

            _etatCourant.Entrer(contexte);

            while (_etatCourant != null)
            {
                _etatCourant.Executer(contexte);

                ICombatState prochainEtat = _etatCourant.DéterminerEtatSuivant(contexte);

                if (prochainEtat is TourJoueurState)
                {
                    _etatCourant = new TourJoueurState(_uiService, _commandesJoueur);
                    _etatCourant.Entrer(contexte);
                }
                else if (prochainEtat is EntreVaguesState)
                {
                    _etatCourant = new EntreVaguesState(_ennemiFactory);
                    _etatCourant.Entrer(contexte);
                }
                else
                {
                    _etatCourant = prochainEtat;
                    if (_etatCourant != null)
                    {
                        _etatCourant.Entrer(contexte);
                    }
                }
            }
        }
    }
}
