public class VegieBuilder
{
    // Deklarasi variabel untuk menyimpan properti Vegie
    private string _name = "Unknown Vegie";
    private int _maxHealth = 100;
    private int _attackLevel = 10;
    private int _luck = 5;

    // Metode untuk mengatur nama Vegie
    public VegieBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    // Metode untuk mengatur kesehatan maksimal Vegie
    public VegieBuilder SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        return this;
    }

    // Metode untuk mengatur level serangan Vegie
    public VegieBuilder SetAttackLevel(int attackLevel)
    {
        _attackLevel = attackLevel;
        return this;
    }

    // Metode untuk mengatur keberuntungan Vegie
    public VegieBuilder SetLuck(int luck)
    {
        _luck = luck;
        return this;
    }

    // Metode untuk mengatur kesulitan dan menyesuaikan properti Vegie berdasarkan kesulitan
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

    // Metode untuk membangun objek Vegie dengan properti yang telah diatur
    public Vegie Build()
    {
        return new Vegie(_name, _maxHealth, _attackLevel, _luck);
    }

    // Metode untuk mengatur kesulitan dari menu utama
    public VegieBuilder SetDifficultyFromMenu()
    {
        Difficulty difficulty = MainMenu.GetCurrentDifficulty(); // Ambil difficulty global
        return SetDifficulty(difficulty);
    }
}
