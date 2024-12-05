// This handle a single wave of combat between the play and a vegie

public static class Combat
{
    public static int TurnNumber { get; private set; } = 1;

    public static void ResetTurn()
    {
        TurnNumber = 1;
    }

    public static void AddTurn()
    {
        TurnNumber++;
    }

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
