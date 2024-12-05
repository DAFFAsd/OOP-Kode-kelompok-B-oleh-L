public static class MainMenu
{
    // Mendapatkan instance dari game dan player
    private static Game game = Game.Instance;
    private static Player player = Player.Instance;

    // Menyimpan tingkat kesulitan saat ini
    private static Difficulty currentDifficulty = Difficulty.Normal;

    // Metode untuk menampilkan menu utama
    public static void StartMenu()
    {
        Console.Clear();
        // Menampilkan informasi hari saat ini
        Console.WriteLine("[CAMP]" + " Day " + game.GetCurrentDay());
        Console.WriteLine("Hello Chubbo!, What would you like to do now?");
        // Menampilkan opsi menu
        Console.WriteLine("1. Start Game");
        Console.WriteLine("2. Load Last Game Save File");
        Console.WriteLine("3. Show Stats");
        Console.WriteLine("4. Set Difficulty");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option: ");
        var option = Console.ReadLine();
        switch (option)
        {
            case "1":
                game.StartDay();
                break;
            case "2":
                game.LoadGame();
                break;
            case "3":
                ShowStats();
                break;
            case "4":
                SetDifficulty();
                break;
            case "5":
                break;
            default:
                Console.WriteLine("Invalid option");
                break;
        }
    }

    // Metode untuk menampilkan statistik pemain
    public static void ShowStats()
    {
        Console.Clear();
        Console.WriteLine("Your Current Stats: \n");
        // Menampilkan informasi statistik pemain
        Console.WriteLine("Name: " + player.Name);
        Console.WriteLine("Weapon: " + player.CurrentWeapon.Name + " (Attack: " + player.CurrentWeapon.AttackLevel + ")");
        Console.WriteLine("Level: " + player.Level);
        Console.WriteLine("Health: " + player.CurrentHealth);
        Console.WriteLine("Attack Level: " + player.AttackLevel);
        Console.WriteLine("Luck: " + player.Luck);
        Console.WriteLine("Experience: " + player.GetExperience() + "/" + player.GetExpereinceToNextLevel());
        Console.WriteLine("Difficulty: " + currentDifficulty);
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        StartMenu();
    }

    // Metode untuk mengatur tingkat kesulitan
    public static void SetDifficulty()
    {
        Console.Clear();
        Console.WriteLine("Select a difficulty level:");
        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Normal");
        Console.WriteLine("3. Hard");
        Console.Write("Choose an option: ");
        var option = Console.ReadLine();
        switch (option)
        {
            case "1":
                currentDifficulty = Difficulty.Easy;
                break;
            case "2":
                currentDifficulty = Difficulty.Normal;
                break;
            case "3":
                currentDifficulty = Difficulty.Hard;
                break;
            default:
                Console.WriteLine("Invalid option, defaulting to Normal.");
                currentDifficulty = Difficulty.Normal;
                break;
        }

        Console.WriteLine($"Difficulty has been set to {currentDifficulty}.");
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
        StartMenu();
    }

    // Metode untuk mendapatkan tingkat kesulitan saat ini
    public static Difficulty GetCurrentDifficulty()
    {
        return currentDifficulty;
    }
}

