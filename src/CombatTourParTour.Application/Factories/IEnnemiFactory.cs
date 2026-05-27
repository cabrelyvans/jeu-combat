using CombatTourParTour.Domain.Entites;
using CombatTourParTour.Domain.Enums;

namespace CombatTourParTour.Application.Factories
{
    public class EnnemiFactory : IEnnemiFactory
    {
        private const int GobelinFaiblePv = 30;
        private const int GobelinFaibleAttaque = 8;

        private const int GobelinArcherPv = 35;
        private const int GobelinArcherAttaque = 12;

        private const int BossOrcPv = 100;
        private const int BossOrcAttaque = 22;

        public Ennemi CreerEnnemi(TypeEnnemi type)
        {
            return type switch
            {
                TypeEnnemi.GobelinFaible => new Ennemi("Gobelin runique", GobelinFaiblePv, GobelinFaibleAttaque),
                TypeEnnemi.GobelinArcher => new Ennemi("Gobelin archer", GobelinArcherPv, GobelinArcherAttaque),
                TypeEnnemi.BossOrc => new Ennemi("Gromash le Destructeur (Boss)", BossOrcPv, BossOrcAttaque),
                
                _ => throw new ArgumentException($"Type d'ennemi inconnu : {type}")
            };
        }
    }
}