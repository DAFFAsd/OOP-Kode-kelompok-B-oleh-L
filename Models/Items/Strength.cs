public class StrengthPotion : Item
{
    // Properti untuk menyimpan peningkatan serangan
    public int AttackBoost { get; private set; }
    // Properti untuk menyimpan durasi efek potion
    public int Duration { get; private set; }

    // Konstruktor untuk menginisialisasi StrengthPotion
    public StrengthPotion(string name, int quantity, int attackBoost, int duration) 
        : base(name, quantity)
    {
        AttackBoost = attackBoost;
        Duration = duration;
    }

    // Metode untuk menggunakan potion
    public override void Use()
    {
        Player player = Player.Instance;
        player.AddBuff(new Buff("Strength", Duration, attackBoost: AttackBoost));
        Console.WriteLine($"Gained {AttackBoost} attack for {Duration} turns!");
        DecreaseQuantity(1);
    }
}