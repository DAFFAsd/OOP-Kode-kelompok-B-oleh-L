public abstract class Item
{
    public string Name { get; private set; }
    public int Quantity { get; private set; }

    public Item(string name, int quantity = 1)
    {
        Name = name;
        Quantity = quantity;
    }

    public virtual void Use()
    {
        Console.WriteLine($"Using {Name}. No effect.");
    }

    public void IncreaseQuantity(int amount)
    {
        Quantity += amount;
    }

    public void DecreaseQuantity(int amount)
    {
        if (Quantity - amount < 0)
        {
            throw new InvalidOperationException("Not enough items.");
        }
        Quantity -= amount;
    }

    public override string ToString()
    {
        return $"{Name} (Quantity: {Quantity})";
    }
}
