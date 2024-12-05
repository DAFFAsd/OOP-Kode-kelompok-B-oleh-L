public class VegieBuilder
{
    private string _name = "Unknown Vegie";
    private int _maxHealth = 100;
    private int _attackLevel = 10;
    private int _luck = 5;

    public VegieBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public VegieBuilder SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        return this;
    }

    public VegieBuilder SetAttackLevel(int attackLevel)
    {
        _attackLevel = attackLevel;
        return this;
    }

    public VegieBuilder SetLuck(int luck)
    {
        _luck = luck;
        return this;
    }

    public VegieBuilder SetDifficulty(Difficulty difficulty)
    {
        _maxHealth = difficulty switch
        {
            Difficulty.Easy => 75,
            Difficulty.Normal => 100,
            Difficulty.Hard => 150,
            _ => 100
        };

        _attackLevel = difficulty switch
        {
            Difficulty.Easy => 5,
            Difficulty.Normal => 10,
            Difficulty.Hard => 15,
            _ => 10
        };

        return this;
    }

    public Vegie Build()
    {
        return new Vegie(_name, _maxHealth, _attackLevel, _luck);
    }

    public VegieBuilder SetDifficultyFromMenu()
    {
        Difficulty difficulty = MainMenu.GetCurrentDifficulty(); // Ambil difficulty global
        return SetDifficulty(difficulty);
    }

}
