using System.Text.Json; // Pastikan ini diimpor
using System.Text.Json.Serialization;

public class Game
{
    private Player player;
    private int currentDay = 0;
    public int currentWave = 0;

    private static readonly Lazy<Game> _instance = new Lazy<Game>(() => new Game());
    public static Game Instance => _instance.Value;
    public void SaveGame()
    {
        GameState state = new GameState
        {
            CurrentDay = GetCurrentDay(),
            CurrentWave = currentWave,
            Player = player,
            Map = MapArea.Instance.Map
        };

        string json = JsonSerializer.Serialize(state, new JsonSerializerOptions
        {
            WriteIndented = true, // Agar output lebih terbaca
        });

        File.WriteAllText("savegame.json", json); // Simpan sebagai file JSON
        Console.WriteLine("Game saved successfully!");
    }

    public void LoadGame()
    {
        if (!File.Exists("savegame.json"))
        {
            Console.WriteLine("No saved game found.");
            return;
        }

        string json = File.ReadAllText("savegame.json");
        GameState state = JsonSerializer.Deserialize<GameState>(json);

        // Restore game state
        currentDay = state.CurrentDay;
        currentWave = state.CurrentWave - 1;
        player = state.Player;

        // Restore map
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
        public Game()
        {
            // Implement different Maps here
            MapArea.SetActiveMap(new LeafyLagoon());
            MapArea.Instance.DisplayDescription();
            player = Player.Instance;
        }
        public List<Vegie> GenerateVegies()
        {
            Random random = new Random();
            int enemyCount = random.Next(1, 4); // 1-3 enemies
            
            List<Vegie> vegies = new List<Vegie>();
            for (int i = 0; i < enemyCount; i++)
            {
                vegies.Add(MapArea.Instance.GenerateVegie());
            }
            
            return vegies;
        }

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
            // Track last defeated miniboss
            this.SetLastDefeatedMiniBoss(miniBossName);

            // Check quest completion after battle
            foreach (var quest in WiseDuck.Instance.Quests)
            {
                quest.TryComplete();
            }

            // Interact with Wise Duck after a miniboss battle
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
                    return; // Exit method to prevent recursive StartDay() call
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

    public void ResetGame()
    {
        currentDay = 0;
        currentWave = 0;
        player = Player.Instance; // Gunakan kembali instance pemain yang sudah ada
        MapArea.SetActiveMap(new LeafyLagoon()); // Mulai dari map awal
        Console.WriteLine("Game has been reset!");
    }

    private void EndGame()
    {
        Console.Clear();
        Console.WriteLine("Congratulations! You've conquered The Salad Bar!");
        Console.WriteLine($"Final Stats:");
        Console.WriteLine($"Days Survived: {currentDay}");
        Console.WriteLine($"Player Level: {player.GetLevel()}");
        Console.WriteLine($"Total Experience: {player.GetExperience()}");
        Console.WriteLine("\nThanks for playing Veggie Adventures!");
        
        // Option to restart or exit
        Console.WriteLine("\nPress R to Restart With NG+, or any other key to Exit");
        var key = Console.ReadKey();
        if (key.Key == ConsoleKey.R)
        {
            // Reset game logic here
            Game.Instance.ResetGame();
        }
        else
        {
            Environment.Exit(0);
        }
    }

    public void resetWave()
    {
        currentWave = 0;
    }

    public void AddWave()
    {
        currentWave++;
    }

    public void AddDay()
    {
        currentDay++;
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }
}
