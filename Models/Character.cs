public abstract class Character
{
    public string Name { get; protected set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int AttackLevel { get; set; }
    public int Luck { get; set; }
    public List<Buff> Buffs { get; private set; } = new List<Buff>();
    public List<Debuff> Debuffs { get; private set; } = new List<Debuff>();

    public void AddBuff(Buff buff)
    {
        Buffs.Add(buff);
    }

    public void AddDebuff(Debuff debuff)
    {
        Debuffs.Add(debuff);
    }

    public void ProcessBuffsAndDebuffs()
    {
        // Remove expired buffs
        Buffs.RemoveAll(b => b.Tick());

        // Remove expired debuffs
        Debuffs.RemoveAll(d => d.Tick());
    }

    public int GetModifiedAttack()
    {
        int baseAttack = AttackLevel;
        
        // Apply buffs
        baseAttack += Buffs.Sum(b => b.AttackBoost);
        
        // Apply debuffs
        baseAttack -= Debuffs.Sum(d => d.AttackReduction);

        return Math.Max(0, baseAttack);
    }
    public Character(string name, int maxHealth, int attackLevel, int luck)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        AttackLevel = attackLevel;
        Luck = luck;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0) CurrentHealth = 0;
    }

    public virtual bool IsDead() => CurrentHealth <= 0;
}