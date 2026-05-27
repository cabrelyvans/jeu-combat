using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Combat
{
    public class CombatContext
    {
        public Heros Heros { get; set; }
        public List<Ennemi> Ennemis { get; set; }
        public int NumeroTour { get; set; }
        public int VagueActuelle { get; set; }

        public CombatContext(Heros heros, List<Ennemi> ennemis, int vagueActuelle)
        {
            Heros = heros;
            Ennemis = ennemis;
            NumeroTour = 1;
            VagueActuelle = vagueActuelle;
        }
    }
}