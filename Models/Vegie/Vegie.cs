public class Vegie : Character
{
    private Random random = new Random();

    public Vegie(string name, int maxHealth, int attackLevel, int luck)
        : base(name, maxHealth, attackLevel, luck)
    {
        CurrentHealth = MaxHealth;
    }


    private static int GetMaxHealth(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => 75,
            Difficulty.Normal => 100,
            Difficulty.Hard => 150,
            _ => 100 // Default to normal
        };
    }

    private static int GetAttackLevel(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => 10,
            Difficulty.Normal => 100,
            Difficulty.Hard => 15,
            _ => 10 // Default to normal
        };
    }

    public void Attack(Player player)
    {
        // Use the modified attack level from GetModifiedAttack()
        int damage = GetModifiedAttack(); // This comes from the Character base class
        string attackType = "";

        // 50% chance to hit or miss
        if (random.Next(2) == 0)
        {
            attackType = "swings at";
        }
        else
        {
            damage = 0;
            attackType = "misses";
        }

        Console.WriteLine($"{Name} {attackType} {player.Name}!");

        if (damage > 0)
        {
            if (player.IsGuarding())
            {
                damage = player.CalculateGuardedDamage(damage);
            }
            player.TakeDamage(damage);
        }

        // Process buffs and debuffs after attack
        ProcessBuffsAndDebuffs();

        Thread.Sleep(1000);
        Console.WriteLine($"{player.Name} takes {damage} damage!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
