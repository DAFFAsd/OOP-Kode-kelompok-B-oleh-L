using System.Text.Json.Serialization;

// Kelas Player yang merupakan turunan dari kelas Character
public class Player : Character
{
    // Singleton pattern untuk memastikan hanya ada satu instance Player
    private static readonly Lazy<Player> _instance = new Lazy<Player>(() => new Player());

    // Properti pengalaman pemain
    public int Experience
    {
        get => experience;
        private set => experience = value;
    }

    // Properti level pemain
    public int Level
    {
        get => level;
        private set => level = value;
    }

    // Properti dan variabel lain
    private bool isGuarding = false;
    private int level = 1;
    private int experience = 0;
    private int experienceToNextLevel = 100;
    private static int defaultHealth = 50;
    private static int defaultAttackLevel = 100;
    private static int defaultLuck = 5;

    // Properti singleton untuk mendapatkan instance Player
    public static Player Instance => _instance.Value;
    
    // Metode untuk menambahkan senjata ke pemain
    public void AddWeapon(Weapon weapon)
    {
        CurrentWeapon = weapon;
        Console.WriteLine($"Obtained new weapon: {weapon.Name}");
    }

    // Properti senjata saat ini
    public Weapon CurrentWeapon { get; private set; }

    // Properti inventaris pemain
    public Inventory Inventory { get; private set; } = new Inventory();

    // Konstruktor privat untuk singleton
    private Player() : base("Chubbo", defaultHealth, defaultAttackLevel, defaultLuck)
    {
        InitializePlayer();
    }

    // Konstruktor untuk deserialisasi JSON
    [JsonConstructor]
    public Player(int experience, int level, Weapon currentWeapon, Inventory inventory, string name, int maxHealth, int currentHealth, int attackLevel, int luck)
        : base(name, maxHealth, attackLevel, luck)  // Memanggil konstruktor base
    {
        Experience = experience;
        Level = level;
        CurrentWeapon = currentWeapon;
        Inventory = inventory;
    }

    // Metode untuk inisialisasi pemain
    private void InitializePlayer()
    {
        CurrentWeapon = Weapon.CreateWeapon(WeaponType.Fists);
    }

    // Metode untuk mengaktifkan mode bertahan
    public void Guard()
    {
        isGuarding = true;
        Console.WriteLine($"{Name} takes a defensive stance!");
    }

    // Metode untuk mereset kesehatan pemain
    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    // Metode untuk memeriksa apakah pemain sedang bertahan
    public bool IsGuarding()
    {
        return isGuarding;
    }

    // Metode untuk menghitung damage yang diterima saat bertahan
    public int CalculateGuardedDamage(int incomingDamage)
    {
        if (isGuarding)
        {
            Random random = new Random();
            double reduction = random.Next(50, 76) / 100.0; // Pengurangan damage 50-75%
            int reducedDamage = (int)(incomingDamage * (1 - reduction));
            Console.WriteLine($"{Name} blocks {(int)(reduction * 100)}% of the damage!");
            isGuarding = false;

            GainExperience(reducedDamage);
            return reducedDamage;
        }
        GainExperience(incomingDamage);
        return incomingDamage;
    }

    // Metode untuk menaikkan level pemain
    private void LevelUp()
    {
        level++;
        Luck += 2;
        experience -= experienceToNextLevel;
        experienceToNextLevel = level * 50;

        MaxHealth += 20;
        CurrentHealth = MaxHealth;

        // Upgrade senjata berdasarkan level
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

    // Metode untuk mendapatkan pengalaman
    public void GainExperience(int amount)
    {
        experience += amount;
        Console.WriteLine($"{Name} gained {amount} experience!");

        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    // Metode untuk mendapatkan pengalaman pemain
    public int GetExperience()
    {
        return experience;
    }

    // Metode untuk mendapatkan level pemain
    public int GetLevel()
    {
        return level;
    }

    // Metode untuk mendapatkan pengalaman yang dibutuhkan untuk level berikutnya
    public int GetExpereinceToNextLevel()
    {
        return experienceToNextLevel;
    }

    // Metode untuk menyembuhkan pemain
    public void Heal(int amount)
    {
        if (CurrentHealth + amount > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            return;
        }
        CurrentHealth += amount;
    }

    // Metode untuk menerima damage (belum diimplementasikan)
    void takeDamage()
    {

    }

    // Metode untuk menyerang Vegie
    public void Attack(Vegie vegie)
    {
        int damage = GetDamage();
        Console.WriteLine(Name + " use attack on " + vegie.Name);
        vegie.CurrentHealth -= damage;

        // Tidur selama 1 detik
        Thread.Sleep(1000);

        Console.WriteLine("It dealts " + damage + " damage!");
        Console.WriteLine(Name + " gained " + 5 + " experience!");

        Console.WriteLine("Press any key to continue...");

        Console.ReadKey();
    }

    // Metode tambahan untuk skill Critical Strike
    public void CriticalStrike(Vegie target)
    {
        if (Level < 3)
        {
            Console.WriteLine("Critical Strike not available. Requires Level 3.");
            Attack(target);
            return;
        }

        Random random = new Random();
        if (random.Next(100) < 30) // Peluang 30% untuk serangan kritis
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

    // Metode tambahan untuk skill Area Attack
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

    // Metode tambahan untuk skill Healing Aura
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

    // Metode untuk mendapatkan damage yang dihasilkan pemain
    public int GetDamage()
    {
        if (CurrentWeapon == null)
        {
            return AttackLevel;
        }

        return CurrentWeapon.AttackLevel + AttackLevel;
    }
}