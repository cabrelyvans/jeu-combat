using CombatTourParTour.Application.Combat;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Actions
{
    public class CompetenceGuerrierAction : ICombatAction
    {
        private readonly Heros _heros;

        public string Nom => "Frappe lourde (Compétence)";

        public CompetenceGuerrierAction(Heros heros)
        {
            _heros = heros;
        }

        public bool PeutExecuter(CombatContext contexte)
        {
            // La compétence est utilisable si le héros est vivant et son cooldown est à 0
            return _heros.EstVivant && _heros.CooldownCompetence == 0;
        }

        public CombatResult Executer(CombatContext contexte, Personnage cible)
        {
            int degats = (int)(_heros.AttaqueDeBase * 1.5);
            
            cible.RecevoirDegats(degats);

            _heros.DefinirCooldown(2);

            string message = $"{_heros.Nom} lance sa Frappe lourde sur {cible.Nom} et inflige de lourds dégâts ({degats} PV) !";
            
            return new CombatResult(_heros, cible, message, degats);
        }
    }
}