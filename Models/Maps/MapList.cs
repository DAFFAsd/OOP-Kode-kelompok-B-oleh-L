public class LeafyLagoon : MapArea
{
    public LeafyLagoon() : base(Maps.Leafy_Lagoon, new List<Vegie>
    {
        new VegieBuilder()
        .SetName("Letty the Lettuce")
        .SetMaxHealth(75)
        .SetAttackLevel(10)
        .SetDifficultyFromMenu()
        .SetLuck(5)
        .Build(),
        new VegieBuilder()
        .SetName("Broco Lee")
        .SetMaxHealth(100)
        .SetAttackLevel(15)
        .SetDifficultyFromMenu()
        .SetLuck(5)
        .Build(),
    })
    {
        // Constructor Masih Empty, Bisa ditambah suatu saat
    }

    public override void DisplayDescription()
    {
        Console.WriteLine("You are in the Leafy Lagoon... Don't let the still waters fool you!");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public override List<Vegie> GetMiniBosses()
    {
        return new List<Vegie>
        {
            new VegieBuilder()
            .SetName("Captain Cabbage")
            .SetMaxHealth(150)
            .SetAttackLevel(20)
            .SetDifficultyFromMenu()
            .SetLuck(10)
            .Build()
        };
    }
}

public class VeggieValley : MapArea
{
    public VeggieValley() : base(Maps.Veggie_Valley, new List<Vegie>
    {
        new VegieBuilder()
        .SetName("Carrot Cavalier")
        .SetMaxHealth(60)
        .SetAttackLevel(12)
        .SetDifficultyFromMenu()
        .SetLuck(4)
        .Build(),
        new VegieBuilder()
        .SetName("Radish Ranger")
        .SetMaxHealth(80)
        .SetAttackLevel(10)
        .SetDifficultyFromMenu()
        .SetLuck(6)
        .Build(),
    })
    {
    }

    public override void DisplayDescription()
    {
        Console.WriteLine("Welcome to Veggie Valley, where the terrain is as diverse as its inhabitants!");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public override List<Vegie> GetMiniBosses()
    {
        return new List<Vegie>
        {
            new VegieBuilder()
            .SetName("General Greenhorn")
            .SetMaxHealth(180)
            .SetAttackLevel(25)
            .SetDifficultyFromMenu()
            .SetLuck(8)
            .Build()
        };
    }
}

public class FruitField : MapArea
{
    public FruitField() : base(Maps.Fruit_Field, new List<Vegie>
    {
        new VegieBuilder()
        .SetName("Apple Archer")
        .SetMaxHealth(70)
        .SetAttackLevel(15)
        .SetDifficultyFromMenu()
        .SetLuck(5)
        .Build(),
        new VegieBuilder()
        .SetName("Berry Brawler")
        .SetMaxHealth(90)
        .SetAttackLevel(12)
        .SetDifficultyFromMenu()
        .SetLuck(7)
        .Build(),
    })
    {
    }

    public override void DisplayDescription()
    {
        Console.WriteLine("Fruit Field stretches before you, ripe with adventure and danger!");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public override List<Vegie> GetMiniBosses()
    {
        return new List<Vegie>
        {
            new VegieBuilder()
            .SetName("Orchard Overlord")
            .SetMaxHealth(200)
            .SetAttackLevel(30)
            .SetDifficultyFromMenu()
            .SetLuck(9)
            .Build()
        };
    }
}

public class MushroomMeadow : MapArea
{
    public MushroomMeadow() : base(Maps.Mushroom_Meadow, new List<Vegie>
    {
        new VegieBuilder()
        .SetName("Shiitake Sentry")
        .SetMaxHealth(85)
        .SetAttackLevel(18)
        .SetDifficultyFromMenu()
        .SetLuck(6)
        .Build(),
        new VegieBuilder()
        .SetName("Portobello Prowler")
        .SetMaxHealth(100)
        .SetAttackLevel(16)
        .SetDifficultyFromMenu()
        .SetLuck(5)
        .Build(),
    })
    {
    }

    public override void DisplayDescription()
    {
        Console.WriteLine("Mushroom Meadow looms, its misty landscape hiding untold mysteries!");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public override List<Vegie> GetMiniBosses()
    {
        return new List<Vegie>
        {
            new VegieBuilder()
            .SetName("Fungal Fiend")
            .SetMaxHealth(220)
            .SetAttackLevel(35)
            .SetDifficultyFromMenu()
            .SetLuck(10)
            .Build()
        };
    }
}

public class TheSaladBar : MapArea
{
    public TheSaladBar() : base(Maps.The_Salad_Bar, new List<Vegie>
    {
        new VegieBuilder()
        .SetName("Crouton Commando")
        .SetMaxHealth(110)
        .SetAttackLevel(25)
        .SetDifficultyFromMenu()
        .SetLuck(8)
        .Build(),
        new VegieBuilder()
        .SetName("Dressing Destroyer")
        .SetMaxHealth(130)
        .SetAttackLevel(22)
        .SetDifficultyFromMenu()
        .SetLuck(7)
        .Build(),
    })
    {
    }

    public override void DisplayDescription()
    {
        Console.WriteLine("You have reached The Salad Bar - the final battleground where only the freshest survive!");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public override List<Vegie> GetMiniBosses()
    {
        return new List<Vegie>
        {
            new VegieBuilder()
            .SetName("Chef Supreme")
            .SetMaxHealth(250)
            .SetAttackLevel(40)
            .SetDifficultyFromMenu()
            .SetLuck(12)
            .Build()
        };
    }
}