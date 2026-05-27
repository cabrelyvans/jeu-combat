namespace CombatTourParTour.Domain.Entites
{
    public abstract class Personnage
    {
        public string Nom { get; protected set; }
        public int PointsDeVieMax { get; protected set; }
        public int PointsDeVieActuels { get; protected set; }
        public int AttaqueDeBase { get; protected set; }
        public bool EstVivant => PointsDeVieActuels > 0;

        protected Personnage(string nom, int pvMax, int attaqueDeBase)
        {
            Nom = nom;
            PointsDeVieMax = pvMax;
            PointsDeVieActuels = pvMax; 
            AttaqueDeBase = attaqueDeBase;
        }
        
        public virtual void RecevoirDegats(int degats)
        {
            if (degats < 0) return;

            PointsDeVieActuels -= degats;
            
            if (PointsDeVieActuels < 0)
            {
                PointsDeVieActuels = 0;
            }
        }

        public virtual void SeSoigner(int montantSoin)
        {
            if (montantSoin < 0 || !EstVivant) return;

            PointsDeVieActuels += montantSoin;

            if (PointsDeVieActuels > PointsDeVieMax)
            {
                PointsDeVieActuels = PointsDeVieMax;
            }
        }
    }
}