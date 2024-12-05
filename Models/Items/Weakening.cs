public class WeakeningPotion : Item
{
    public int Duration { get; private set; }

    public WeakeningPotion(string name, int quantity, int duration = 1) 
        : base(name, quantity)
    {
        Duration = duration;
    }

    public override void Use()
    {
        // Prompt user to choose a target Vegie
        var vegies = BattleMenu.CurrentVegies; // You'll need to modify BattleMenu to track current vegies
        
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
            
            // Apply a debuff that reduces attack to 1
            targetVegie.AddDebuff(new Debuff("Weakened", Duration, attackReduction: targetVegie.AttackLevel - 1));
            
            Console.WriteLine($"{targetVegie.Name} has been weakened!");
            
            // Decrease item quantity
            DecreaseQuantity(1);
        }
        else
        {
            Console.WriteLine("Invalid target!");
        }
    }
}