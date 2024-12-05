using System.Text.Json.Serialization;
using System;
using System.Text.Json;

// Kelas konverter untuk mengubah WeaponType ke dan dari JSON
public class WeaponTypeConverter : JsonConverter<WeaponType>
{
    // Metode untuk membaca WeaponType dari JSON
    public override WeaponType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string value = reader.GetString();
        return value switch
        {
            "FryGun" => WeaponType.FryGun,
            "SodaSprayer" => WeaponType.SodaSprayer,
            "PizzaSlicer" => WeaponType.PizzaSlicer,
            "SugarRushRifle" => WeaponType.SugarRushRifle,
            "HolyTabascoSauce" => WeaponType.HolyTabascoSauce,
            "Fists" => WeaponType.Fists,
            _ => throw new JsonException($"Unknown weapon type: {value}")
        };
    }

    // Metode untuk menulis WeaponType ke JSON
    public override void Write(Utf8JsonWriter writer, WeaponType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

// Kelas Weapon yang merepresentasikan senjata dalam game
public class Weapon
{
    // Properti nama senjata
    public string Name { get; private set; }
    // Properti level serangan senjata
    public int AttackLevel { get; private set; }
    // Properti tipe senjata dengan konverter JSON
    [JsonConverter(typeof(WeaponTypeConverter))]
    public WeaponType Type { get; private set; }

    // Konstruktor untuk deserialisasi JSON
    [JsonConstructor]
    private Weapon(string name, int attackLevel, WeaponType type)
    {
        Name = name;
        AttackLevel = attackLevel;
        Type = type;
    }
    
    // Metode untuk membuat senjata berdasarkan tipe senjata
    public static Weapon CreateWeapon(WeaponType weaponType)
    {
        return weaponType switch
        {
            WeaponType.FryGun => new Weapon("FryGun", 10, WeaponType.FryGun),
            WeaponType.SodaSprayer => new Weapon("SodaSprayer", 15, WeaponType.SodaSprayer),
            WeaponType.PizzaSlicer => new Weapon("PizzaSlicer", 20, WeaponType.PizzaSlicer),
            WeaponType.SugarRushRifle => new Weapon("SugarRushRifle", 18, WeaponType.SugarRushRifle),
            WeaponType.HolyTabascoSauce => new Weapon("Holy Tabasco Sauce", 25, WeaponType.HolyTabascoSauce),
            _ => new Weapon("Fists", 5, WeaponType.Fists)  // Senjata default
        };
    }
}
