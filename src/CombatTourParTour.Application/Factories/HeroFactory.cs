using CombatTourParTour.Domain.Entites;
using CombatTourParTour.Domain.Enums;

namespace CombatTourParTour.Application.Factories
{
    public class HeroFactory : IHeroFactory
    {
        private const int GuerrierPv = 120;
        private const int GuerrierAttaque = 18;
        private const int MagePv = 80;
        private const int MageAttaque = 12;
        private const int VoleurPv = 90;
        private const int VoleurAttaque = 14;

        public Heros CreerHeros(string nom, ClasseHeros classe)
        {
            return classe switch
            {
                ClasseHeros.Guerrier => new Heros(nom, GuerrierPv, GuerrierAttaque, classe),
                ClasseHeros.Mage => new Heros(nom, MagePv, MageAttaque, classe),
                ClasseHeros.Voleur => new Heros(nom, VoleurPv, VoleurAttaque, classe),
                _ => throw new ArgumentException($"Classe de héros non supportée : {classe}"),
            };
        }
    }
}
