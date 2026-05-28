using CombatTourParTour.Application.Combat;
using CombatTourParTour.Domain.Entites;

namespace CombatTourParTour.Application.Actions
{
    public class CompetenceVoleurAction : ICombatAction
    {
        private readonly Heros _heros;
        private readonly Random _random = new Random();

        public string Nom => "Coup critique (Compétence)";

        public CompetenceVoleurAction(Heros heros)
        {
            _heros = heros;
        }

        public bool PeutExecuter(CombatContext contexte)
        {
            return _heros.EstVivant && _heros.CooldownCompetence == 0;
        }

        public CombatResult Executer(CombatContext contexte, Personnage cible)
        {
            int degats = _heros.AttaqueDeBase;
            string messageAddon = "";

            if (_random.Next(1, 101) <= 30)
            {
                degats *= 2;
                messageAddon = " CRITIQUE ! Dégâts doublés !";
            }

            cible.RecevoirDegats(degats);
            _heros.DefinirCooldown(2);

            string message = $"{_heros.Nom} porte un Coup critique rapide sur {cible.Nom} et inflige {degats} points de dégâts !{messageAddon}";
            
            return new CombatResult(_heros, cible, message, degats);
        }
    }
}