namespace Wordle.Library;
public class GuessResult
{
    public LetterState First { get; set; }
    public LetterState Second { get; set; }
    public LetterState Third { get; set; }
    public LetterState Fourth { get; set; }
    public LetterState Fifth { get; set; }
}
public enum LetterState
{
    Unused,
    UsedWrongPlace,
    Correct
}
