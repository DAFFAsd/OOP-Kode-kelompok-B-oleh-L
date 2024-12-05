using System.Text.Json.Serialization;
using System;
using System.Text.Json;

public class WeaponTypeConverter : JsonConverter<WeaponType>
{
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

    public override void Write(Utf8JsonWriter writer, WeaponType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}


public class Weapon
{
    public string Name { get; private set; }
    public int AttackLevel { get; private set; }
    [JsonConverter(typeof(WeaponTypeConverter))]  // Add JsonConverter here
    public WeaponType Type { get; private set; }

    // Use the JsonConstructor attribute to specify which constructor to use for deserialization
    [JsonConstructor]
    private Weapon(string name, int attackLevel, WeaponType type)
    {
        Name = name;
        AttackLevel = attackLevel;
        Type = type;
    }
    
    public static Weapon CreateWeapon(WeaponType weaponType)
    {
        return weaponType switch
        {
            WeaponType.FryGun => new Weapon("FryGun", 10, WeaponType.FryGun),
            WeaponType.SodaSprayer => new Weapon("SodaSprayer", 15, WeaponType.SodaSprayer),
            WeaponType.PizzaSlicer => new Weapon("PizzaSlicer", 20, WeaponType.PizzaSlicer),
            WeaponType.SugarRushRifle => new Weapon("SugarRushRifle", 18, WeaponType.SugarRushRifle),
            WeaponType.HolyTabascoSauce => new Weapon("Holy Tabasco Sauce", 25, WeaponType.HolyTabascoSauce),
            _ => new Weapon("Fists", 5, WeaponType.Fists)  // Default weapon
        };
    }
}
