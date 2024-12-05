//fitur tambahan buff/debuff

// Class Buff dan Debuff
public class Buff
{
    // Field - Field yang ada pada class Buff
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public int AttackBoost { get; private set; }
    public int DefenseBoost { get; private set; }

    // Constructor untuk class Buff
    public Buff(string name, int duration, int attackBoost = 0, int defenseBoost = 0)
    {
        Name = name;
        Duration = duration;
        AttackBoost = attackBoost;
        DefenseBoost = defenseBoost;
    }

    // Method untuk mengurangi durasi buff
    public bool Tick()
    {
        Duration--;
        return Duration <= 0;
    }
}

// Class Debuff
public class Debuff
{
    // Field - Field yang ada pada class Debuff
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public int AttackReduction { get; private set; }
    public int DefenseReduction { get; private set; }

    // Constructor untuk class Debuff
    public Debuff(string name, int duration, int attackReduction = 0, int defenseReduction = 0)
    {
        Name = name;
        Duration = duration;
        AttackReduction = attackReduction;
        DefenseReduction = defenseReduction;
    }

    // Method untuk mengurangi durasi debuff
    public bool Tick()
    {
        Duration--;
        return Duration <= 0;
    }
}