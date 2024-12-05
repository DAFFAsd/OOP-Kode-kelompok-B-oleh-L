using System;
using System.Collections.Generic;

public class Quest
{
    // Properti untuk menyimpan nama quest
    public string Name { get; private set; }
    // Properti untuk mengecek apakah quest sudah selesai
    public bool IsCompleted { get; private set; }
    // Delegasi untuk mengecek kondisi penyelesaian quest
    public Func<bool> CompletionCheck { get; private set; }
    // Delegasi untuk memberikan reward saat quest selesai
    public Action OnCompletionReward { get; private set; }

    // Konstruktor untuk menginisialisasi quest
    public Quest(string name, Func<bool> completionCheck, Action onCompletionReward)
    {
        Name = name;
        CompletionCheck = completionCheck;
        OnCompletionReward = onCompletionReward;
        IsCompleted = false;
    }

    // Metode untuk mencoba menyelesaikan quest
    public void TryComplete()
    {
        if (!IsCompleted && CompletionCheck())
        {
            IsCompleted = true;
            OnCompletionReward?.Invoke();
        }
    }
}

public class WiseDuck
{
    // Singleton pattern untuk memastikan hanya ada satu instance WiseDuck
    private static readonly Lazy<WiseDuck> _instance = new Lazy<WiseDuck>(() => new WiseDuck());
    public static WiseDuck Instance => _instance.Value;

    // Properti untuk menyimpan daftar quest
    public List<Quest> Quests { get; private set; }

    // Konstruktor privat untuk menginisialisasi quest
    private WiseDuck()
    {
        InitializeQuests();
    }

    // Metode untuk menginisialisasi daftar quest
    private void InitializeQuests()
    {
        Quests = new List<Quest>
        {
            new Quest(
                "Defeat General Greenhorn", 
                () => Game.Instance.GetLastDefeatedMiniBoss() == "General Greenhorn",
                () =>
                {
                    Console.WriteLine("Wise Duck says: 'Ah, you've proven your worth! Here's the Holy Tabasco Sauce!'");
                    Player.Instance.AddWeapon(Weapon.CreateWeapon(WeaponType.HolyTabascoSauce));
                }
            )
        };
    }

    // Metode untuk berinteraksi dengan pemain
    public void InteractWithPlayer()
    {
        Console.WriteLine("Wise Duck waddles up to you...");
        Console.WriteLine("Wise Duck says: 'Greetings, brave adventurer! I have a quest for you.'");
        
        foreach (var quest in Quests)
        {
            if (!quest.IsCompleted)
            {
                Console.WriteLine($"Current Quest: {quest.Name}");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}

// Metode ekstensi untuk kelas Game untuk melacak miniboss terakhir yang dikalahkan
public static class GameExtensions
{
    private static string _lastDefeatedMiniBoss;

    // Metode untuk mengatur miniboss terakhir yang dikalahkan
    public static void SetLastDefeatedMiniBoss(this Game game, string miniBossName)
    {
        _lastDefeatedMiniBoss = miniBossName;
    }

    // Metode untuk mendapatkan miniboss terakhir yang dikalahkan
    public static string GetLastDefeatedMiniBoss(this Game game)
    {
        return _lastDefeatedMiniBoss;
    }
}