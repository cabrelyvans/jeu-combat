using CombatTourParTour.Application.Combat;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Actions
{
    public class AttaqueBasiqueAction : ICombatAction
    {
        // On récupère le personnage qui possède cette action (un héros ou un ennemi)
        private readonly Personnage _lanceur;

        public string Nom => "Attaque de base";

        public AttaqueBasiqueAction(Personnage lanceur)
        {
            _lanceur = lanceur;
        }

        public bool PeutExecuter(CombatContext contexte)
        {
            // Une attaque basique est toujours possible tant que le lanceur est vivant
            return _lanceur.EstVivant;
        }

        public CombatResult Executer(CombatContext contexte, Personnage cible)
        {
            int degats = _lanceur.AttaqueDeBase;
            
            // On applique les dégâts sur la cible (méthode de notre domaine !)
            cible.RecevoirDegats(degats);

            string message = $"{_lanceur.Nom} attaque {cible.Nom} et inflige {degats} points de dégâts !";
            
            return new CombatResult(_lanceur, cible, message, degats, estUnSoin: false);
        }
    }
}