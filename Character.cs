using System;

namespace Magic_battles
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }

        public Character(string name, int health, int mana)
        {
            Name = name;
            Health = health;
            Mana = mana;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0;
        }

        public void UseMana(int amount)
        {
            Mana -= amount;
            if (Mana < 0)
                Mana = 0;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}
