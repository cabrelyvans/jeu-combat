using CombatTourParTour.Application.Actions;
using CombatTourParTour.Application.Combat;

namespace CombatTourParTour.Application.Commands
{
    public class SoignerCommand : ICommand
    {
        private readonly ICombatAction _soinAction;

        public string Description => "Se soigner (+25 PV)";

        public SoignerCommand(ICombatAction soinAction)
        {
            _soinAction = soinAction;
        }

        public bool PeutExecuter(CombatContext contexte)
        {
            return _soinAction.PeutExecuter(contexte);
        }

        public CombatResult Executer(CombatContext contexte)
        {
            return _soinAction.Executer(contexte, contexte.Heros);
        }
    }
}