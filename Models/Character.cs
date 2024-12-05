public abstract class Character
{
    public string Name { get; protected set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int AttackLevel { get; set; }
    public int Luck { get; set; }
    public List<Buff> Buffs { get; private set; } = new List<Buff>();
    public List<Debuff> Debuffs { get; private set; } = new List<Debuff>();

    // Metode untuk menambahkan buff ke karakter
    public void AddBuff(Buff buff)
    {
        Buffs.Add(buff);
    }

    // Metode untuk menambahkan debuff ke karakter
    public void AddDebuff(Debuff debuff)
    {
        Debuffs.Add(debuff);
    }

    // Metode untuk memproses buff dan debuff
    public void ProcessBuffsAndDebuffs()
    {
        // Hapus buff yang telah habis masa berlakunya
        Buffs.RemoveAll(b => b.Tick());

        // Hapus debuff yang telah habis masa berlakunya
        Debuffs.RemoveAll(d => d.Tick());
    }

    // Metode untuk mendapatkan level serangan yang dimodifikasi
    public int GetModifiedAttack()
    {
        int baseAttack = AttackLevel;
        
        // Terapkan buff
        baseAttack += Buffs.Sum(b => b.AttackBoost);
        
        // Terapkan debuff
        baseAttack -= Debuffs.Sum(d => d.AttackReduction);

        return Math.Max(0, baseAttack);
    }

    // Konstruktor untuk menginisialisasi objek Character
    public Character(string name, int maxHealth, int attackLevel, int luck)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        AttackLevel = attackLevel;
        Luck = luck;
    }

    // Metode untuk menerima kerusakan
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0) CurrentHealth = 0;
    }

    // Metode untuk memeriksa apakah karakter sudah mati
    public virtual bool IsDead() => CurrentHealth <= 0;
}