using System.Text.Json; // Pastikan ini diimpor
using System.Text.Json.Serialization;

// Class Game
public class Game
{
    // Field - Field yang ada pada class Game
    private Player player;
    private int currentDay = 0;
    public int currentWave = 0;

    private static readonly Lazy<Game> _instance = new Lazy<Game>(() => new Game());
    public static Game Instance => _instance.Value;

    // Field untuk menyimpan nama miniboss terakhir yang dikalahkan
    public void SaveGame()
    {
        // Membuat objek GameState untuk menyimpan state game saat ini
        GameState state = new GameState
        {
            CurrentDay = GetCurrentDay(),
            CurrentWave = currentWave,
            Player = player,
            Map = MapArea.Instance.Map
        };

        // Serialisasi objek GameState ke dalam format JSON
        string json = JsonSerializer.Serialize(state, new JsonSerializerOptions
        {
            WriteIndented = true, // Agar output lebih terbaca
        });

        // Menyimpan JSON ke dalam file
        File.WriteAllText("savegame.json", json); // Simpan sebagai file JSON
        Console.WriteLine("Game saved successfully!");
    }

    // Method untuk memuat game
    public void LoadGame()
    {
        // Memeriksa apakah file savegame.json ada
        if (!File.Exists("savegame.json"))
        {
            Console.WriteLine("No saved game found.");
            return;
        }

        // Membaca JSON dari file
        string json = File.ReadAllText("savegame.json");
        GameState state = JsonSerializer.Deserialize<GameState>(json);

        // Mengembalikan state game dari objek GameState
        currentDay = state.CurrentDay;
        currentWave = state.CurrentWave - 1;
        player = state.Player;

        // Mengembalikan state map
        switch (state.Map)
        {
            case Maps.Leafy_Lagoon:
                MapArea.SetActiveMap(new LeafyLagoon());
                break;
            case Maps.Veggie_Valley:
                MapArea.SetActiveMap(new VeggieValley());
                break;
            case Maps.Fruit_Field:
                MapArea.SetActiveMap(new FruitField());
                break;
            case Maps.Mushroom_Meadow:
                MapArea.SetActiveMap(new MushroomMeadow());
                break;
            case Maps.The_Salad_Bar:
                MapArea.SetActiveMap(new TheSaladBar());
                break;
            default:
                Console.WriteLine("Map not recognized. Loading default map.");
                MapArea.SetActiveMap(new LeafyLagoon());
                break;
        }
        
        Console.WriteLine("Game loaded successfully!");
        StartDay();
    }
        
    // Konstruktor untuk class Game
    public Game()
    {
        // Implementasi map awal
        MapArea.SetActiveMap(new LeafyLagoon());
        MapArea.Instance.DisplayDescription();
        player = Player.Instance;
    }

    // Method untuk menghasilkan vegie
    public List<Vegie> GenerateVegies()
    {
        Random random = new Random();
        int enemyCount = random.Next(1, 4); // 1-3 musuh
        
        List<Vegie> vegies = new List<Vegie>();
        for (int i = 0; i < enemyCount; i++)
        {
            vegies.Add(MapArea.Instance.GenerateVegie());
        }
        
        return vegies;
    }

    // Method untuk memulai hari
    public void StartDay()
    {
        AddWave();
        Combat.ResetTurn();

        // Tambahkan logika untuk miniboss setelah 3 wave
        if (currentWave == 4)
        {
            List<Vegie> miniBosses = MapArea.Instance.GetMiniBosses();
            BattleMenu.StartBattle(player, miniBosses);
            string miniBossName = miniBosses[0].Name;
            this.SetLastDefeatedMiniBoss(miniBossName);

            // Memeriksa apakah ada quest yang terpenuhi
            foreach (var quest in WiseDuck.Instance.Quests)
            {
                quest.TryComplete();
            }

            // Interaksi dengan WiseDuck setelah mengalahkan miniboss
            WiseDuck.Instance.InteractWithPlayer();

            // Setelah kalahkan miniboss, pindah ke map berikutnya
            switch (MapArea.Instance.Map)
            {
                case Maps.Leafy_Lagoon:
                    resetWave();
                    MapArea.SetActiveMap(new VeggieValley());
                    break;
                case Maps.Veggie_Valley:
                    resetWave();
                    MapArea.SetActiveMap(new FruitField());
                    break;
                case Maps.Fruit_Field:
                    resetWave();
                    MapArea.SetActiveMap(new MushroomMeadow());
                    break;
                case Maps.Mushroom_Meadow:
                    resetWave();
                    MapArea.SetActiveMap(new TheSaladBar());
                    break;
                case Maps.The_Salad_Bar:
                    EndGame();
                    return; // Keluar dari method
                    break;
                default:
                    Console.WriteLine("No next map available.");
                    break;
            }
        }
        else
        {
            List<Vegie> vegies = GenerateVegies();
            BattleMenu.StartBattle(player, vegies);
        }
        StartDay();
    }

    // Method untuk mereset game
    public void ResetGame()
    {
        currentDay = 0;
        currentWave = 0;
        player = Player.Instance; // Gunakan kembali instance pemain yang sudah ada
        MapArea.SetActiveMap(new LeafyLagoon()); // Mulai dari map awal
        Console.WriteLine("Game has been reset!");
    }

    // Method untuk mengakhiri game
    private void EndGame()
    {
        Console.Clear();
        Console.WriteLine("Congratulations! You've conquered The Salad Bar!");
        Console.WriteLine($"Final Stats:");
        Console.WriteLine($"Days Survived: {currentDay}");
        Console.WriteLine($"Player Level: {player.GetLevel()}");
        Console.WriteLine($"Total Experience: {player.GetExperience()}");
        Console.WriteLine("\nThanks for playing Veggie Adventures!");
        
        // Opsi untuk memulai ulang atau keluar
        Console.WriteLine("\nPress R to Restart With NG+, or any other key to Exit");
        var key = Console.ReadKey();
        if (key.Key == ConsoleKey.R)
        {
            // Logika reset game di sini
            Game.Instance.ResetGame();
        }
        else
        {
            Environment.Exit(0);
        }
    }

    // Method untuk mereset wave
    public void resetWave()
    {
        currentWave = 0;
    }

    // Method untuk menambah wave
    public void AddWave()
    {
        currentWave++;
    }

    // Method untuk menambah hari
    public void AddDay()
    {
        currentDay++;
    }

    // Method untuk mendapatkan hari saat ini
    public int GetCurrentDay()
    {
        return currentDay;
    }
}
