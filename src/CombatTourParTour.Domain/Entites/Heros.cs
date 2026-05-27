using System.Runtime.InteropServices.Marshalling;
using CombatTourParTour.Domain.Enums;
namespace CombatTourParTour.Domain.Entites
{
    public class Heros : Personnage
    {
        public ClasseHeros Classe { get; private set; }
        public int SoinsRestants { get; private set; }
        public int CooldownCompetence { get; private set; }
        public const int MaxSoins = 2;

        public Heros(string nom, int pvMax, int attaqueDeBase, ClasseHeros classe) 
        : base(nom, pvMax, attaqueDeBase)
        {
            Classe = classe;
            SoinsRestants = MaxSoins;
            CooldownCompetence = 0;
        }
        public void UtiliserSoin()
        {
            if (SoinsRestants <= 0) return;

            SeSoigner(25); 
            SoinsRestants--;
        }

        public void ModifierCooldown(int valeur)
        {
            CooldownCompetence += valeur;
            if (CooldownCompetence < 0) CooldownCompetence = 0;
        }
        
        public void DefinirCooldown(int tours)
        {
            if (tours >= 0) CooldownCompetence = tours;
        }

    }
}
