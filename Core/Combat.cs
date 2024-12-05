// This handle a single wave of combat between the play and a vegie

// Class Combat
public static class Combat
{
    // Method untuk memulai pertarungan
    public static int TurnNumber { get; private set; } = 1;

    // Method untuk mereset turn
    public static void ResetTurn()
    {
        TurnNumber = 1;
    }

    // Method untuk memnambah turn
    public static void AddTurn()
    {
        TurnNumber++;
    }

    // Method untuk mengakhiri day
    public static void EndDay()
    {
        Console.Clear();
        Console.WriteLine("You were defeated by the Vegie... Your progress will reset from the 1st Wave");

        Console.WriteLine("Press any key to continue...");

        Console.ReadKey();
        Game.Instance.resetWave();
        Game.Instance.StartDay(); 
    }
}
