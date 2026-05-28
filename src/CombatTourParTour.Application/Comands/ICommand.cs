using CombatTourParTour.Application.Combat;

namespace CombatTourParTour.Application.Commands
{
    public interface ICommand
    {
        string Description { get; }

        bool PeutExecuter(CombatContext contexte);

        CombatResult Executer(CombatContext contexte);
    }
}