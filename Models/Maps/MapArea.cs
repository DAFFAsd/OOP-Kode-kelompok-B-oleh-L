using System;
using System.Text.Json;
using System.Text.Json.Serialization;

// Konverter untuk enum Maps ke JSON
public class MapsConverter : JsonConverter<Maps>
{
    // Membaca nilai JSON dan mengonversinya ke enum Maps
    public override Maps Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string value = reader.GetString();
        
        return value switch
        {
            "Leafy_Lagoon" => Maps.Leafy_Lagoon,
            "Veggie_Valley" => Maps.Veggie_Valley,
            "Fruit_Field" => Maps.Fruit_Field,
            "Mushroom_Meadow" => Maps.Mushroom_Meadow,
            "The_Salad_Bar" => Maps.The_Salad_Bar,
            _ => throw new JsonException($"Unknown map type: {value}")
        };
    }

    // Menulis nilai enum Maps ke JSON
    public override void Write(Utf8JsonWriter writer, Maps value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

// Kelas dasar untuk semua area peta
public class MapArea
{
    private static MapArea _instance;

    // Properti publik untuk mendapatkan peta aktif saat ini
    public static MapArea Instance => _instance;

    [JsonConverter(typeof(MapsConverter))] // Menambahkan atribut ini jika diperlukan
    public Maps Map { get; set; }

    public List<Vegie> Vegies { get; set; }

    // Konstruktor yang dilindungi untuk membatasi instansiasi
    protected MapArea(Maps map, List<Vegie> vegies)
    {
        Map = map;
        Vegies = vegies;
    }

    // Implementasi default untuk mendapatkan mini boss, bisa di-override di setiap map
    public virtual List<Vegie> GetMiniBosses()
    {
        return new List<Vegie>();
    }

    // Metode untuk mengatur peta aktif secara dinamis
    public static void SetActiveMap(MapArea map)
    {
        _instance = map;
    }

    // Menghasilkan Vegie secara acak dari peta
    public Vegie GenerateVegie()
    {
        int randomIndex = new Random().Next(0, Vegies.Count);
        Vegie prototype = Vegies[randomIndex];
        return new Vegie(prototype.Name, prototype.MaxHealth, prototype.AttackLevel, prototype.Luck);
    }

    // Metode virtual untuk deskripsi area
    public virtual void DisplayDescription()
    {
        Console.WriteLine("Gedagedigedagedaoo");
    }
}
