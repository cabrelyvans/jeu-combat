using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Combat
{
    public class CombatResult
    {
        public Personnage Lanceur { get; private set; }
        public Personnage Cible { get; private set; }
        public string Message { get; private set; }
        public int Valeur { get; private set; }
        public bool EstUnSoin { get; private set; }

        public CombatResult(Personnage lanceur, Personnage cible, string message, int valeur, bool estUnSoin = false)
        {
            Lanceur = lanceur;
            Cible = cible;
            Message = message;
            Valeur = valeur;
            EstUnSoin = estUnSoin;
        }
    }
}