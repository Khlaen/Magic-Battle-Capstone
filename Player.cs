using System;
using System.Collections.Generic;

namespace Magic_battles
{
    public class Player : Character
    {
        public List<Spell> SpellBook { get; set; }
        public int Gold { get; set; }
        public int MaxMana { get; set; }
        public bool ShieldActive { get; set; }
        public bool DragonActive { get; set; }

        public Player(string name) : base(name, 100, 50)
        {
            SpellBook = new List<Spell>();
            Gold = 0;
            MaxMana = 50;
        }

        public void AddSpell(Spell spell)
        {
            foreach (Spell s in SpellBook)
            {
                if (s.Name == spell.Name)
                {
                    Console.WriteLine("You already own this spell.");
                    return;
                }
            }

            SpellBook.Add(spell);
            Console.WriteLine($"You learned {spell.Name}!");
        }

        public void RegenerateMana()
        {
            Mana += 15;
            if (Mana > MaxMana)
                Mana = MaxMana;

            Console.WriteLine("You regenerated 15 mana.");
        }

        public void CastSpell(int index, Character enemy)
        {
            if (index < 1 || index > SpellBook.Count)
                return;

            Spell spell = SpellBook[index - 1];

            if (spell.Name == "World Breaker")
            {
                Mana = 0;
                enemy.Health = 1;
                Console.WriteLine("WORLD BREAKER used!");
                return;
            }

            if (spell.Name == "Dragon Summon")
            {
                if (Mana >= spell.ManaCost)
                {
                    UseMana(spell.ManaCost);
                    DragonActive = true;
                    Console.WriteLine("Dragon summoned!");
                }
                return;
            }

            if (spell.Name == "Restoration")
            {
                if (Mana >= spell.ManaCost)
                {
                    UseMana(spell.ManaCost);
                    Health += 20;
                    if (Health > 100) Health = 100;
                    Console.WriteLine("You healed 20 HP.");
                }
                return;
            }

            if (spell.Name == "Shield")
            {
                if (Mana >= spell.ManaCost)
                {
                    UseMana(spell.ManaCost);
                    ShieldActive = true;
                    Console.WriteLine("Shield activated!");
                }
                return;
            }

            if (Mana >= spell.ManaCost)
            {
                UseMana(spell.ManaCost);
                enemy.TakeDamage(spell.Damage);
                Console.WriteLine($"{Name} casts {spell.Name}!");
            }
        }

        public void TakeEnemyDamage(int damage)
        {
            if (ShieldActive)
            {
                Console.WriteLine("Shield blocked attack!");
                ShieldActive = false;
                return;
            }

            TakeDamage(damage);
        }

        public void DragonAttack(Character enemy)
        {
            if (DragonActive)
            {
                Console.WriteLine("Dragon deals 35 damage!");
                enemy.TakeDamage(35);
            }
        }

        public void UpgradeMana()
        {
            if (Gold >= 100)
            {
                Gold -= 100;
                MaxMana += 25;
                Mana = MaxMana; // refill
                Console.WriteLine("Max mana increased by 25!");
            }
            else
            {
                Console.WriteLine("Not enough gold.");
            }
        }

        public void RestoreAfterBattle()
        {
            Health = 100;
            Mana = MaxMana;
            ShieldActive = false;
            DragonActive = false;
        }
    }
}
