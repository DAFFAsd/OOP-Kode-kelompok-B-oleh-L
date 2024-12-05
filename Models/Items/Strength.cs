public class StrengthPotion : Item
{
    public int AttackBoost { get; private set; }
    public int Duration { get; private set; }

    public StrengthPotion(string name, int quantity, int attackBoost, int duration) 
        : base(name, quantity)
    {
        AttackBoost = attackBoost;
        Duration = duration;
    }

    public override void Use()
    {
        Player player = Player.Instance;
        player.AddBuff(new Buff("Strength", Duration, attackBoost: AttackBoost));
        Console.WriteLine($"Gained {AttackBoost} attack for {Duration} turns!");
        DecreaseQuantity(1);
    }
}