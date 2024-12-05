//fitur tambahan buff/debuff
public class Buff
{
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public int AttackBoost { get; private set; }
    public int DefenseBoost { get; private set; }

    public Buff(string name, int duration, int attackBoost = 0, int defenseBoost = 0)
    {
        Name = name;
        Duration = duration;
        AttackBoost = attackBoost;
        DefenseBoost = defenseBoost;
    }

    public bool Tick()
    {
        Duration--;
        return Duration <= 0;
    }
}

public class Debuff
{
    public string Name { get; private set; }
    public int Duration { get; private set; }
    public int AttackReduction { get; private set; }
    public int DefenseReduction { get; private set; }

    public Debuff(string name, int duration, int attackReduction = 0, int defenseReduction = 0)
    {
        Name = name;
        Duration = duration;
        AttackReduction = attackReduction;
        DefenseReduction = defenseReduction;
    }

    public bool Tick()
    {
        Duration--;
        return Duration <= 0;
    }
}