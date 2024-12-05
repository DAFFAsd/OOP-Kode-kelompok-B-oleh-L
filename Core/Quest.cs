using System;
using System.Collections.Generic;

public class Quest
{
    public string Name { get; private set; }
    public bool IsCompleted { get; private set; }
    public Func<bool> CompletionCheck { get; private set; }
    public Action OnCompletionReward { get; private set; }

    public Quest(string name, Func<bool> completionCheck, Action onCompletionReward)
    {
        Name = name;
        CompletionCheck = completionCheck;
        OnCompletionReward = onCompletionReward;
        IsCompleted = false;
    }

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
    private static readonly Lazy<WiseDuck> _instance = new Lazy<WiseDuck>(() => new WiseDuck());
    public static WiseDuck Instance => _instance.Value;

    public List<Quest> Quests { get; private set; }

    private WiseDuck()
    {
        InitializeQuests();
    }

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

// Extension method for Game class to track last defeated miniboss
public static class GameExtensions
{
    private static string _lastDefeatedMiniBoss;

    public static void SetLastDefeatedMiniBoss(this Game game, string miniBossName)
    {
        _lastDefeatedMiniBoss = miniBossName;
    }

    public static string GetLastDefeatedMiniBoss(this Game game)
    {
        return _lastDefeatedMiniBoss;
    }
}