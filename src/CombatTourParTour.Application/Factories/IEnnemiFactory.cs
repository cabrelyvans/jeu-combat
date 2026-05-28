using CombatTourParTour.Domain.Entites;
using CombatTourParTour.Domain.Enums;

namespace CombatTourParTour.Application.Factories
{
    public interface IEnnemiFactory
    {
        Ennemi CreerEnnemi(TypeEnnemi type);
    }
}
