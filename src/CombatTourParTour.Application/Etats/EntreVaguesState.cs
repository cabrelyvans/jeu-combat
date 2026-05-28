using CombatTourParTour.Application.Combat;
using CombatTourParTour.Application.Factories;
using CombatTourParTour.Domain.Enums;

namespace CombatTourParTour.Application.Etats
{
    public class EntreVaguesState : ICombatState
    {
        private readonly IEnnemiFactory _ennemiFactory;

        public EntreVaguesState(IEnnemiFactory ennemiFactory)
        {
            _ennemiFactory = ennemiFactory;
        }

        public void Entrer(CombatContext contexte)
        {
            double calculSoin = contexte.Heros.PointsDeVieMax * 0.20;
            int montantSoin = (int)Math.Ceiling(calculSoin);
            
            contexte.Heros.SeSoigner(montantSoin);

            contexte.VagueActuelle++;
            contexte.NumeroTour = 1;

            contexte.Ennemis.Clear();
            
            if (contexte.VagueActuelle == 2)
            {
                contexte.Ennemis.Add(_ennemiFactory.CreerEnnemi(TypeEnnemi.GobelinFaible));
                contexte.Ennemis.Add(_ennemiFactory.CreerEnnemi(TypeEnnemi.GobelinArcher));
            }
            else if (contexte.VagueActuelle == 3)
            {
                contexte.Ennemis.Add(_ennemiFactory.CreerEnnemi(TypeEnnemi.BossOrc));
            }
        }

        public void Executer(CombatContext contexte)
        {
            // La transition se fait automatiquement
        }

        public ICombatState DéterminerEtatSuivant(CombatContext contexte)
        {
            // Après la transition, on redonne immédiatement la main au joueur
            return new TourJoueurState();
        }
    }
}