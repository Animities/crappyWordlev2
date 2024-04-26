namespace Wordle.Library;
public class NewGame
{
    public bool IsGameDone { get; set; }

    public void StartGame()
    {
        //Sets global things
        //Starts the game, tells user what to do, etc
    }
    public GuessResult Guess(string? guess)
    {
        //Process the guess. Return GuessResult.  
        var result = new GuessResult();

        //do stuff
        // ...
        // ...
        // ...
        // Set Result (result.First = LetterState.UsedWrongPlace; )
        // ...
        // ...

        WriteResultToConsole(result, guess);
        return result;
    }

    private void WriteResultToConsole(GuessResult result, string guess)
    {
        for (var pos = 0; pos < guess.Length; pos++)
        {
            if (pos == 0)
            {
                SetForeground(result.First);
            }
            else if (pos == 1)
            {
                SetForeground(result.Second);
            }
            else if (pos == 2)
            {
                SetForeground(result.Third);
            }
            else if (pos == 3)
            {
                SetForeground(result.Fourth);
            }
            else if (pos == 4)
            {
                SetForeground(result.Fifth);
            }
            Console.Write(guess[pos]);
        }
    }
    private static void SetForeground(LetterState result)
    {
        switch (result)
        {
            case LetterState.Correct:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case LetterState.UsedWrongPlace:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
        }
    }
}
