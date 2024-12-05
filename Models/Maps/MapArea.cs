using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class MapsConverter : JsonConverter<Maps>
{
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

    public override void Write(Utf8JsonWriter writer, Maps value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}



public class MapArea
{
    private static MapArea _instance;

    // Public property to get the current active map
    public static MapArea Instance => _instance;

    [JsonConverter(typeof(MapsConverter))] // Add this attribute if needed
    public Maps Map { get; set; }

    public List<Vegie> Vegies { get; set; }

    // Protected constructor to restrict instantiation
    protected MapArea(Maps map, List<Vegie> vegies)
    {
        Map = map;
        Vegies = vegies;
    }
    public virtual List<Vegie> GetMiniBosses()
    {
        // Default implementation, bisa di-override di setiap map
        return new List<Vegie>();
    }

    // Method to set the active map dynamically
    public static void SetActiveMap(MapArea map)
    {
        _instance = map;
    }

    // Generate a random Vegie from the map
    public Vegie GenerateVegie()
    {
        int randomIndex = new Random().Next(0, Vegies.Count);
        Vegie prototype = Vegies[randomIndex];
        return new Vegie(prototype.Name, prototype.MaxHealth, prototype.AttackLevel, prototype.Luck);
    }

    // Virtual method for area descriptions
    public virtual void DisplayDescription()
    {
        Console.WriteLine("Gedagedigedagedaoo");
    }
}
