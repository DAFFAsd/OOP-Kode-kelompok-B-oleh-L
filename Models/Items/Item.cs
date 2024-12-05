public abstract class Item
{
    // Properti untuk nama item
    public string Name { get; private set; }
    
    // Properti untuk jumlah item
    public int Quantity { get; private set; }

    // Konstruktor untuk item
    public Item(string name, int quantity = 1)
    {
        Name = name;
        Quantity = quantity;
    }

    // Metode virtual untuk menggunakan item
    public virtual void Use()
    {
        Console.WriteLine($"Using {Name}. No effect.");
    }

    // Metode untuk menambah jumlah item
    public void IncreaseQuantity(int amount)
    {
        Quantity += amount;
    }

    // Metode untuk mengurangi jumlah item
    public void DecreaseQuantity(int amount)
    {
        if (Quantity - amount < 0)
        {
            throw new InvalidOperationException("Not enough items.");
        }
        Quantity -= amount;
    }

    // Override metode ToString untuk menampilkan informasi item
    public override string ToString()
    {
        return $"{Name} (Quantity: {Quantity})";
    }
}
