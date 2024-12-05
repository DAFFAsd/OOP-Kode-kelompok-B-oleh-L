public class Potion : Item
{
    // Properti untuk menyimpan jumlah penyembuhan
    public int HealAmount { get; private set; }

    // Konstruktor untuk menginisialisasi Potion
    public Potion(string name, int quantity, int healAmount) 
        : base(name, quantity)
    {
        HealAmount = healAmount;
    }

    // Metode untuk menggunakan potion
    public override void Use()
    {
        // Menyembuhkan pemain sebesar HealAmount
        if (Quantity <= 0)
        {
            Console.WriteLine("You don't have any Potions left!");
            return;
        }

        Player.Instance.Heal(HealAmount);
        Console.WriteLine($"{Name} used! You have been healed by {HealAmount} points.");
        DecreaseQuantity(1);
    }
}
