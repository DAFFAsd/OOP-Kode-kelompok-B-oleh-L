public class Vegie : Character
{
    private Random random = new Random();

    // Konstruktor untuk menginisialisasi objek Vegie
    public Vegie(string name, int maxHealth, int attackLevel, int luck)
        : base(name, maxHealth, attackLevel, luck)
    {
        CurrentHealth = MaxHealth;
    }

    // Metode statis untuk mendapatkan kesehatan maksimal berdasarkan kesulitan
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

    // Metode statis untuk mendapatkan level serangan berdasarkan kesulitan
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

    // Metode untuk menyerang pemain
    public void Attack(Player player)
    {
        // Gunakan level serangan yang dimodifikasi dari GetModifiedAttack()
        int damage = GetModifiedAttack(); // Ini berasal dari kelas dasar Character
        string attackType = "";

        // 50% kemungkinan untuk mengenai atau meleset
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

        // Proses buff dan debuff setelah serangan
        ProcessBuffsAndDebuffs();

        Thread.Sleep(1000);
        Console.WriteLine($"{player.Name} takes {damage} damage!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
