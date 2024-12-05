[Serializable] // Menandakan bahwa kelas ini dapat diserialisasi
public class GameState
{
    public int CurrentDay { get; set; } // Menyimpan hari saat ini dalam permainan
    public int CurrentWave { get; set; } // Menyimpan gelombang saat ini dalam permainan
    public Player Player { get; set; } // Menyimpan informasi tentang pemain
    public Maps Map { get; set; } // Menyimpan nama map
}
