using System;
using System.Collections.Generic;

namespace Magic_battles
{
    public class Enemy : Character
    {
        private static Random random = new Random();
        private List<Spell> enemySpells;

        public int GoldDrop { get; set; }
        public Spell SpellDrop { get; set; }

        public Enemy(string name, int health, int mana) : base(name, health, mana)
        {
            enemySpells = new List<Spell>
            {
                new Spell("Shadow Bolt", 15, 10),
                new Spell("Dark Flame", 20, 15)
            };

            GoldDrop = random.Next(20, 51);
        }

        public void CastRandomSpell(Player player)
        {
            Spell spell = enemySpells[random.Next(enemySpells.Count)];

            if (Mana >= spell.ManaCost)
            {
                UseMana(spell.ManaCost);
                player.TakeEnemyDamage(spell.Damage);
            }
            else
            {
                player.TakeEnemyDamage(10);
            }
        }

        public void DropLoot(Player player)
        {
            player.Gold += GoldDrop;
            Console.WriteLine($"You gained {GoldDrop} gold.");
        }
    }
}
