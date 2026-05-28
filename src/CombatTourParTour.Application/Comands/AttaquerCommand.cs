using CombatTourParTour.Application.Actions;
using CombatTourParTour.Application.Combat;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Commands
{
    public class AttaquerCommand : ICommand
    {
        private readonly ICombatAction _attaqueAction;
        private Ennemi? _cible;

        public string Description => "Attaque de base";

        public AttaquerCommand(ICombatAction attaqueAction)
        {
            _attaqueAction = attaqueAction;
        }

        public void DefinirCible(Ennemi cible)
        {
            _cible = cible;
        }

        public bool PeutExecuter(CombatContext contexte)
        {
            return _attaqueAction.PeutExecuter(contexte);
        }

        public CombatResult Executer(CombatContext contexte)
        {
            if (_cible == null || !_cible.EstVivant)
            {
                throw new InvalidOperationException(
                    "Impossible d'attaquer : aucune cible valide n'a été sélectionnée."
                );
            }

            return _attaqueAction.Executer(contexte, _cible);
        }
    }
}
