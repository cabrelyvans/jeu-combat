using CombatTourParTour.Application.Combat;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Actions
{
    public class CompetenceMageAction : ICombatAction
    {
        private readonly Heros _heros;
        private const int DegatsFixesEclair = 30;

        public string Nom => "Éclair (Compétence)";

        public CompetenceMageAction(Heros heros)
        {
            _heros = heros;
        }

        public bool PeutExecuter(CombatContext contexte)
        {
            return _heros.EstVivant && _heros.CooldownCompetence == 0;
        }

        public CombatResult Executer(CombatContext contexte, Personnage cible)
        {
            int degats = DegatsFixesEclair;
            
            cible.RecevoirDegats(degats);
            _heros.DefinirCooldown(3);

            string message = $"{_heros.Nom} invoque un Éclair sur {cible.Nom} pour {degats} points de dégâts magiques !";
            
            return new CombatResult(_heros, cible, message, degats);
        }
    }
}