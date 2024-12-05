using System.Text;

public class Inventory
{
    public List<Item> items = new List<Item>();

    public Inventory()
    {
        AddItem(new Potion("Health Potion", 3, 50)); // Reduced quantity for balance
        AddItem(new StrengthPotion("Strength Potion", 2, 20, 2)); // Attack boost of 20 for 2 turns
        AddItem(new WeakeningPotion("Weakening Potion", 2)); // 2 weakening potions
    }

    public void AddItem(Item item)
    {
        // Check if item already exists
        var existingItem = items.FirstOrDefault(i => i.Name == item.Name);
        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(item.Quantity);
        }
        else
        {
            items.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    // Change ToString to a method that returns a string or just display inventory.
    public void DisplayInventory()
    {
        Console.Clear();
        if (items.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        foreach (Item item in items)
        {
            Console.WriteLine(item.ToString());  // Calls the ToString method of Item
        }
    }
}
