using CombatTourParTour.Application.Combat;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Actions
{
    public class SoinAction : ICombatAction
    {
        private readonly Personnage _lanceur;

        public string Nom => "Se soigner (+25 PV)";

        public SoinAction(Personnage lanceur)
        {
            _lanceur = lanceur;
        }

        public bool PeutExecuter(CombatContext contexte)
        {
            // On vérifie si le lanceur est un héros, et s'il lui reste des soins
            if (_lanceur is Heros heros)
            {
                return heros.EstVivant && heros.SoinsRestants > 0;
            }
            
            return false;
        }

        public CombatResult Executer(CombatContext contexte, Personnage cible)
        {
            // On convertit le lanceur en Heros pour accéder à ses fonctionnalités spécifiques
            var heros = (Heros)_lanceur;

            // On mémorise les PV avant le soin pour calculer exactement combien de PV ont été rendus
            int pvAvantSoin = heros.PointsDeVieActuels;

            // On applique le soin (notre méthode métier du Domaine)
            heros.UtiliserSoin();

            int pvRendus = heros.PointsDeVieActuels - pvAvantSoin;

            string message = $"{heros.Nom} utilise un soin et récupère {pvRendus} PV ! (Soins restants : {heros.SoinsRestants})";

            // On retourne le résultat en précisant que c'est un soin (estUnSoin: true)
            return new CombatResult(heros, heros, message, pvRendus, estUnSoin: true);
        }
    }
}