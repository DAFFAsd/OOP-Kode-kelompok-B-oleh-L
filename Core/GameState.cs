[Serializable]
public class GameState
{
    public int CurrentDay { get; set; }
    public int CurrentWave { get; set; }
    public Player Player { get; set; }
    public Maps Map { get; set; } // Menyimpan nama map
}
