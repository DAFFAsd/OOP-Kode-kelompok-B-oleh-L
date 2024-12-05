using System.Text;

public class Inventory
{
    public List<Item> items = new List<Item>();

    // Konstruktor untuk menginisialisasi inventaris dengan item default
    public Inventory()
    {
        AddItem(new Potion("Health Potion", 3, 50)); // Mengurangi jumlah untuk keseimbangan
        AddItem(new StrengthPotion("Strength Potion", 2, 20, 2)); // Peningkatan serangan sebesar 20 untuk 2 giliran
        AddItem(new WeakeningPotion("Weakening Potion", 2)); // 2 weakening potions
    }

    // Metode untuk menambahkan item ke dalam inventaris
    public void AddItem(Item item)
    {
        // Periksa apakah item sudah ada
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

    // Metode untuk menghapus item dari inventaris
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    // Ubah ToString menjadi metode yang mengembalikan string atau hanya menampilkan inventaris.
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
            Console.WriteLine(item.ToString());  // Memanggil metode ToString dari Item
        }
    }
}
