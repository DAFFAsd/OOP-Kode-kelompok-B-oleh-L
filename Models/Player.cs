using System.Text.Json.Serialization;

//app/vegie/player.cs
public class Player : Character
{
    
    private static readonly Lazy<Player> _instance = new Lazy<Player>(() => new Player());

    public int Experience
    {
        get => experience;
        private set => experience = value;
    }

    public int Level
    {
        get => level;
        private set => level = value;
    }

    private bool isGuarding = false;
    private int level = 1;
    private int experience = 0;
    private int experienceToNextLevel = 100;

    private static int defaultHealth = 50;
    private static int defaultAttackLevel = 100;
    private static int defaultLuck = 5;

    // Singleton pattern
    public static Player Instance => _instance.Value;
    
    public void AddWeapon(Weapon weapon)
    {
        CurrentWeapon = weapon;
        Console.WriteLine($"Obtained new weapon: {weapon.Name}");
    }
    public Weapon CurrentWeapon { get; private set; }

    public Inventory Inventory { get; private set; } = new Inventory();

    private Player() : base("Chubbo", defaultHealth, defaultAttackLevel, defaultLuck)
    {
        InitializePlayer();
    }
    [JsonConstructor]
    public Player(int experience, int level, Weapon currentWeapon, Inventory inventory, string name, int maxHealth, int currentHealth, int attackLevel, int luck)
        : base(name, maxHealth, attackLevel, luck)  // Call the base constructor
    {
        Experience = experience;
        Level = level;
        CurrentWeapon = currentWeapon;
        Inventory = inventory;
    }

    private void InitializePlayer()
    {
        CurrentWeapon = Weapon.CreateWeapon(WeaponType.Fists);
    }

    public void Guard()
    {
        isGuarding = true;
        Console.WriteLine($"{Name} takes a defensive stance!");
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public bool IsGuarding()
    {
        return isGuarding;
    }

    public int CalculateGuardedDamage(int incomingDamage)
    {
        if (isGuarding)
        {
            Random random = new Random();
            double reduction = random.Next(50, 76) / 100.0; // 50-75% damage reduction
            int reducedDamage = (int)(incomingDamage * (1 - reduction));
            Console.WriteLine($"{Name} blocks {(int)(reduction * 100)}% of the damage!");
            isGuarding = false;

            GainExperience(reducedDamage);
            return reducedDamage;
        }
        GainExperience(incomingDamage);
        return incomingDamage;
    }

    private void LevelUp()
    {
        level++;
        Luck += 2;
        experience -= experienceToNextLevel;
        experienceToNextLevel = level * 50;

        MaxHealth += 20;
        CurrentHealth = MaxHealth;

        // Upgrade weapon based on level
        if (CurrentWeapon.Type != WeaponType.HolyTabascoSauce){
            CurrentWeapon = level switch
            {
                1 => Weapon.CreateWeapon(WeaponType.Fists),
                3 => Weapon.CreateWeapon(WeaponType.FryGun),
                5 => Weapon.CreateWeapon(WeaponType.SodaSprayer),
                7 => Weapon.CreateWeapon(WeaponType.PizzaSlicer),
                10 => Weapon.CreateWeapon(WeaponType.SugarRushRifle),
                _ => CurrentWeapon
            };
        }
        Console.WriteLine($"\n{Name} reached level {level}!");
        Console.WriteLine($"Max Health increased to {MaxHealth}!");
        if (level == 3)
        {
            Console.WriteLine("New Skill Unlocked: Critical Strike!");
            Console.WriteLine("You can now perform powerful critical hits with a 30% chance!");
        }
        
        if (level == 5)
        {
            Console.WriteLine("New Skill Unlocked: Healing Aura!");
            Console.WriteLine("You can now heal yourself during battle!");
        }
        
        if (level == 8)
        {
            Console.WriteLine("New Skill Unlocked: Area Attack!");
            Console.WriteLine("You can now damage multiple enemies at once!");
        }
        if (CurrentWeapon != null)
        {
            Console.WriteLine($"Obtained new weapon: {CurrentWeapon.Name}!");
        }

        Console.WriteLine("Press any key to continue...");

        Console.ReadKey();
    }

    public void GainExperience(int amount)
    {
        experience += amount;
        Console.WriteLine($"{Name} gained {amount} experience!");

        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    public int GetExperience()
    {
        return experience;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetExpereinceToNextLevel()
    {
        return experienceToNextLevel;
    }

    public void Heal(int amount)
    {
        if (CurrentHealth + amount > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            return;
        }
        CurrentHealth += amount;
    }

    void takeDamage()
    {

    }

    public void Attack(Vegie vegie)
    {
        int damage = GetDamage();
        Console.WriteLine(Name + " use attack on " + vegie.Name);
        vegie.CurrentHealth -= damage;

        // sleep for 1 second
        Thread.Sleep(1000);

        Console.WriteLine("It dealts " + damage + " damage!");
        Console.WriteLine(Name + " gained " + 5 + " experience!");

        Console.WriteLine("Press any key to continue...");

        Console.ReadKey();
    }

    //fitur tambahan skill
    public void CriticalStrike(Vegie target)
    {
        if (Level < 3)
        {
            Console.WriteLine("Critical Strike not available. Requires Level 3.");
            Attack(target);
            return;
        }

        Random random = new Random();
        if (random.Next(100) < 30) // 30% chance of critical strike
        {
            int criticalDamage = GetDamage() * 2;
            Console.WriteLine($"Critical Strike! {Name} deals {criticalDamage} damage to {target.Name}!");
            target.TakeDamage(criticalDamage);
            GainExperience(criticalDamage);
        }
        else
        {
            Attack(target);
        }
    }

    public void AreaAttack(List<Vegie> vegies)
    {
        if (Level < 8)
        {
            Console.WriteLine("Area Attack not available. Requires Level 8.");
            Attack(vegies.FirstOrDefault());
            return;
        }
        
        int damage = GetDamage();
        Console.WriteLine($"{Name} uses Area Attack!");
        
        foreach (var vegie in vegies.Where(v => !v.IsDead()))
        {
            vegie.TakeDamage(damage);
            Console.WriteLine($"Deals {damage} damage to {vegie.Name}!");
            GainExperience(damage);
        }
    }

    public void HealingAura()
    {
        if (Level < 5)
        {
            Console.WriteLine("Healing Aura not available. Requires Level 5.");
            return;
        }
        int healAmount = 20 + (Level * 5);
        Console.WriteLine($"{Name} activates Healing Aura!");
        
        Heal(healAmount);
        Console.WriteLine($"Heals {healAmount} HP!");
    }
    public int GetDamage()
    {
        if (CurrentWeapon == null)
        {
            return AttackLevel;
        }

        return CurrentWeapon.AttackLevel + AttackLevel;
    }
}