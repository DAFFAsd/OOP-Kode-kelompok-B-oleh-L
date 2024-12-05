public class WeakeningPotion : Item
{
    // Properti untuk menyimpan durasi efek potion
    public int Duration { get; private set; }

    // Konstruktor untuk menginisialisasi WeakeningPotion
    public WeakeningPotion(string name, int quantity, int duration = 1) 
        : base(name, quantity)
    {
        Duration = duration;
    }

    // Metode untuk menggunakan potion
    public override void Use()
    {
        // Meminta pengguna untuk memilih target Vegie
        var vegies = BattleMenu.CurrentVegies; // Anda perlu memodifikasi BattleMenu untuk melacak vegies saat ini
        
        Console.WriteLine("Choose a Vegie to weaken:");
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
            Vegie targetVegie = vegies[targetIndex - 1];
            
            // Menerapkan debuff yang mengurangi serangan menjadi 1
            targetVegie.AddDebuff(new Debuff("Weakened", Duration, attackReduction: targetVegie.AttackLevel - 1));
            
            Console.WriteLine($"{targetVegie.Name} has been weakened!");
            
            // Mengurangi jumlah item
            DecreaseQuantity(1);
        }
        else
        {
            Console.WriteLine("Invalid target!");
        }
    }
}