using CombatTourParTour.Application.Actions;
using CombatTourParTour.Application.Combat;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Commands
{
    public class UtiliserCompetenceCommand : ICommand
    {
        private readonly ICombatAction _competenceAction;
        private Ennemi _cible;

        public string Description => _competenceAction.Nom;

        public UtiliserCompetenceCommand(ICombatAction competenceAction)
        {
            _competenceAction = competenceAction;
        }

        public void DefinirCible(Ennemi cible)
        {
            _cible = cible;
        }

        public bool PeutExecuter(CombatContext contexte)
        {
            return _competenceAction.PeutExecuter(contexte);
        }

        public CombatResult Executer(CombatContext contexte)
        {
            if (_cible == null || !_cible.EstVivant)
            {
                throw new InvalidOperationException("Une cible vivante est requise pour cette compétence.");
            }

            return _competenceAction.Executer(contexte, _cible);
        }
    }
}