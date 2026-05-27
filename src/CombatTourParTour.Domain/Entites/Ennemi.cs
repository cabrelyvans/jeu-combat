namespace CombatTourParTour.Domain.Entites
{
    public class Ennemi : Personnage
    {
        public Ennemi(string nom, int pvMax, int attaqueDeBase) 
            : base(nom, pvMax, attaqueDeBase)
        {
        }
    }
}