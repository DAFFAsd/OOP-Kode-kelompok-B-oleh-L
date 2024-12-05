using System.Reflection.PortableExecutable;

//app/ui/BattleMenu.cs
public static class BattleMenu
{   
    public static List<Vegie> CurrentVegies { get; private set; }
    public static void StartBattle(Player player, List<Vegie> vegies)
    {
        Console.Clear();
        Console.WriteLine("Wild Vegies appear!\n");
        CurrentVegies = vegies;
        Console.WriteLine("Press any key to start the battle...");
        Console.ReadKey();

        while (!player.IsDead() && !AreAllVegiesDead(vegies))
        {
            // Print player and vegies stats
            Console.Clear();
            Console.WriteLine("Wave: " + Game.Instance.currentWave + "\tTurn " + Combat.TurnNumber);
            Console.WriteLine(" Level: " + player.GetLevel() + "\t\t" + "Experience: " + player.GetExperience() + "/" + player.GetExpereinceToNextLevel());
            
            // Display player health
            Console.WriteLine(player.Name + " HP: " + player.CurrentHealth + " / " + player.MaxHealth);
            
            // Display all vegies' health
            for (int i = 0; i < vegies.Count; i++)
            {
                Console.WriteLine($"Vegie {i+1} ({vegies[i].Name}): HP {vegies[i].CurrentHealth} / {vegies[i].MaxHealth}");
            }

            Console.WriteLine("\n");

            Console.WriteLine("Player turn");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Skill");
            Console.WriteLine("3. Use Item");
            Console.WriteLine("4. Guard");
            Console.Write("Choose an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ChooseTargetAndAttack(player, vegies);
                    break;
                case "2":
                    UsePlayerSkill(player, vegies);
                    break;
                case "3":
                    ItemMenu(player);
                    break;
                case "4":
                    player.Guard();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

            // Vegie turns
            foreach (var vegie in vegies.Where(v => !v.IsDead()))
            {
                Console.WriteLine($"\n{vegie.Name}'s turn");
                vegie.Attack(player);
                
                if (player.IsDead())
                {
                    Combat.EndDay();
                    return;
                }
            }

            Combat.AddTurn();
        }

        // Battle won logic
        if (player.IsDead())
        {
            Combat.EndDay();
        }
        else
        {
            Console.WriteLine("You won!");
            player.GainExperience(50 * vegies.Count);
            Console.WriteLine("Saving game progress...");
            Game.Instance.SaveGame();
            AddRandomItemToInventory(player);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static bool AreAllVegiesDead(List<Vegie> vegies)
    {
        return vegies.All(v => v.IsDead());
    }

    private static void ChooseTargetAndAttack(Player player, List<Vegie> vegies)
    {
        Console.WriteLine("Choose a target:");
        for (int i = 0; i < vegies.Count; i++)
        {
            if (!vegies[i].IsDead())
            {
                Console.WriteLine($"{i + 1}. {vegies[i].Name} (HP: {vegies[i].CurrentHealth}/{vegies[i].MaxHealth})");
            }
        }

        if (int.TryParse(Console.ReadLine(), out int targetIndex) && 
            targetIndex > 0 && targetIndex <= vegies.Count && 
            !vegies[targetIndex - 1].IsDead())
        {
            player.Attack(vegies[targetIndex - 1]);
        }
        else
        {
            Console.WriteLine("Invalid target!");
        }
    }

    private static void UsePlayerSkill(Player player, List<Vegie> vegies)
    {
        Console.WriteLine("Choose a skill:");
        Console.WriteLine("1. Critical Strike");
        Console.WriteLine("2. Area Attack");
        Console.WriteLine("3. Healing Aura");

        if (int.TryParse(Console.ReadLine(), out int skillChoice))
        {
            switch (skillChoice)
            {
                case 1:
                    player.CriticalStrike(ChooseTargetVegie(vegies));
                    break;
                case 2:
                    player.AreaAttack(vegies);
                    break;
                case 3:
                    player.HealingAura();
                    break;
                default:
                    Console.WriteLine("Invalid skill!");
                    break;
            }
        }
    }

    private static Vegie ChooseTargetVegie(List<Vegie> vegies)
    {
        Console.WriteLine("Choose a target:");
        for (int i = 0; i < vegies.Count; i++)
        {
            if (!vegies[i].IsDead())
            {
                Console.WriteLine($"{i + 1}. {vegies[i].Name} (HP: {vegies[i].CurrentHealth}/{vegies[i].MaxHealth})");
            }
        }

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int targetIndex) && 
                targetIndex > 0 && targetIndex <= vegies.Count && 
                !vegies[targetIndex - 1].IsDead())
            {
                return vegies[targetIndex - 1];
            }
            Console.WriteLine("Invalid target!");
        }
    }
    public static void ItemMenu(Player player)
    {
        Console.Clear();
        Console.WriteLine("Inventory");
        player.Inventory.DisplayInventory();
        Console.WriteLine("Choose an item to use (enter the number):");

        if (int.TryParse(Console.ReadLine(), out int option) && option > 0 && option <= player.Inventory.items.Count)
        {
            Item selectedItem = player.Inventory.items[option - 1];

            // Here you can add logic to use the selected item
            Console.WriteLine($"You chose to use {selectedItem.Name}.");
            selectedItem.Use();
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter a valid number.");
        }

        Console.ReadKey();
    }
    private static void AddRandomItemToInventory(Player player)
{
    // Tentukan daftar item yang mungkin diberikan
    List<Item> possibleItems = new List<Item>
    {
        new Potion("Health Potion", 1, 50),
        new StrengthPotion("Strength Potion", 1, 20, 2),
        new WeakeningPotion("Weakening Potion", 1)
    };

    // Hitung peluang berdasarkan nilai luck pemain
    Random rand = new Random();
    int chance = rand.Next(0, 100);

    if (chance < player.Luck) // Luck menentukan probabilitas untuk mendapatkan item
    {
        // Pilih item secara acak dari daftar item yang mungkin
        Item randomItem = possibleItems[rand.Next(possibleItems.Count)];
        Console.WriteLine($"Congratulations! You found a {randomItem.Name}.");
        player.Inventory.AddItem(randomItem); // Menambahkan item ke inventory
    }
    else
    {
        Console.WriteLine("No items found right now...... Better luck next time!");
    }
}
}

