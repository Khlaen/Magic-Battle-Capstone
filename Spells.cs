namespace Magic_battles
{
    public class Spell
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int ManaCost { get; set; }
        public bool IsSupport { get; set; }

        public Spell(string name, int damage, int manaCost, bool isSupport = false)
        {
            Name = name;
            Damage = damage;
            ManaCost = manaCost;
            IsSupport = isSupport;
        }
    }
}
