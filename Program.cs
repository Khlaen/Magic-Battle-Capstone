using System;

namespace Magic_battles
{
    class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("=== MAGIC BATTLE ===");
            Console.Write("Enter your name: ");
            Player player = new Player(Console.ReadLine());

            player.AddSpell(new Spell("Fireball", 20, 15));
            player.AddSpell(new Spell("Ice Shard", 15, 10));

            bool playing = true;
            int caveProgress = 0;

            while (playing && player.IsAlive())
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine("1. Dark Forest");
                Console.WriteLine("2. Ancient Cave");
                Console.WriteLine("3. Shop");
                Console.WriteLine("4. Quit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DarkForest(player);
                        break;

                    case "2":
                        AncientCave(player, ref caveProgress);
                        break;

                    case "3":
                        OpenShop(player);
                        break;

                    case "4":
                        playing = false;
                        break;
                }
            }

            Console.WriteLine("Game Over.");
        }

        static void DarkForest(Player player)
        {
            bool hunting = true;

            while (hunting && player.IsAlive())
            {
                Enemy enemy = new Enemy("Forest Beast", 90, 40);
                Battle(player, enemy);

                if (!player.IsAlive()) return;

                Console.WriteLine("\nContinue slaying?");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. Back");

                if (Console.ReadLine() != "1")
                    hunting = false;
            }
        }

        static void AncientCave(Player player, ref int progress)
        {
            while (player.IsAlive())
            {
                Console.WriteLine($"\nCave Progress: {progress}/3 toward Legendary Chest");
                Console.WriteLine("1. Continue deeper");
                Console.WriteLine("2. Back out");

                string input = Console.ReadLine();
                if (input == "2") return;

                Enemy enemy = new Enemy("Cave Troll", 120, 30);
                Battle(player, enemy);

                if (!player.IsAlive()) return;

                progress++;

                if (progress >= 3)
                {
                    Console.WriteLine("\nLEGENDARY CHEST FOUND!");
                    Console.WriteLine("1. World Breaker");
                    Console.WriteLine("2. Dragon Summon");

                    if (Console.ReadLine() == "1")
                        player.AddSpell(new Spell("World Breaker", 0, 0));
                    else
                        player.AddSpell(new Spell("Dragon Summon", 0, 40));

                    progress = 0;
                }
            }
        }

        static void OpenShop(Player player)
        {
            bool shopping = true;

            while (shopping)
            {
                Console.WriteLine("\n=== SHOP ===");
                Console.WriteLine($"Gold: {player.Gold}");
                Console.WriteLine("1. Buy Restoration (50g)");
                Console.WriteLine("2. Buy Shield (60g)");
                Console.WriteLine("3. Upgrade Mana Capacity (100g)");
                Console.WriteLine("4. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (player.Gold >= 50)
                        {
                            player.Gold -= 50;
                            player.AddSpell(new Spell("Restoration", 0, 15, true));
                        }
                        else
                            Console.WriteLine("Not enough gold.");
                        break;

                    case "2":
                        if (player.Gold >= 60)
                        {
                            player.Gold -= 60;
                            player.AddSpell(new Spell("Shield", 0, 15, true));
                        }
                        else
                            Console.WriteLine("Not enough gold.");
                        break;

                    case "3":
                        player.UpgradeMana(); // ✅ FIXED
                        break;

                    case "4":
                        shopping = false;
                        break;
                }
            }
        }

        static void Battle(Player player, Enemy enemy)
        {
            player.Mana = player.MaxMana;

            Console.WriteLine($"\nA wild {enemy.Name} appears!");

            while (enemy.IsAlive() && player.IsAlive())
            {
                Console.WriteLine("\n==============================");
                Console.WriteLine($"PLAYER: {player.Name}");
                Console.WriteLine($"HP: {player.Health}/100");
                Console.WriteLine($"Mana: {player.Mana}/{player.MaxMana}");
                Console.WriteLine($"Gold: {player.Gold}");
                Console.WriteLine("------------------------------");
                Console.WriteLine($"ENEMY: {enemy.Name}");
                Console.WriteLine($"HP: {enemy.Health}");
                Console.WriteLine($"Mana: {enemy.Mana}");
                Console.WriteLine("==============================");

                Console.WriteLine("\n1. Cast Spell");
                Console.WriteLine("2. Regenerate Mana");

                string action = Console.ReadLine();

                if (action == "1")
                {
                    Console.WriteLine("\n=== Your Spells ===");

                    for (int i = 0; i < player.SpellBook.Count; i++)
                    {
                        Spell s = player.SpellBook[i];

                        if (s.Name == "World Breaker")
                            Console.WriteLine($"{i + 1}. {s.Name} (Uses ALL Mana)");
                        else if (s.Name == "Dragon Summon")
                            Console.WriteLine($"{i + 1}. {s.Name} (Summons Dragon)");
                        else if (s.Name == "Restoration")
                            Console.WriteLine($"{i + 1}. {s.Name} (Heals)");
                        else if (s.Name == "Shield")
                            Console.WriteLine($"{i + 1}. {s.Name} (Blocks attack)");
                        else
                            Console.WriteLine($"{i + 1}. {s.Name} (Damage: {s.Damage} | Cost: {s.ManaCost})");
                    }

                    if (int.TryParse(Console.ReadLine(), out int spellChoice))
                    {
                        player.CastSpell(spellChoice, enemy);
                        player.DragonAttack(enemy);
                    }
                }
                else
                {
                    player.RegenerateMana();
                }

                if (enemy.IsAlive())
                {
                    enemy.CastRandomSpell(player);
                }
            }

            if (player.IsAlive())
            {
                Console.WriteLine($"\nYou defeated {enemy.Name}!");
                enemy.DropLoot(player);
                player.RestoreAfterBattle();
            }
        }
    }
}
