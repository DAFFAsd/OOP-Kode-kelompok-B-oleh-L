public class Potion : Item
{
    public int HealAmount { get; private set; }

    public Potion(string name, int quantity, int healAmount) 
        : base(name, quantity)
    {
        HealAmount = healAmount;
    }

    public override void Use()
    {
        // Heals the player by HealAmount
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
