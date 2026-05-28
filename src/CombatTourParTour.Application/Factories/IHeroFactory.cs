using CombatTourParTour.Domain.Entites;
using CombatTourParTour.Domain.Enums;

namespace CombatTourParTour.Application.Factories
{
    public interface IHeroFactory
    {
        Heros CreerHeros(string nom, ClasseHeros classe);
    }
}
