//TODO:

using Wordle.Ai;
using Wordle.Library;

class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Do you want to play manually (1) or AI (2)?");
            Console.WriteLine("Enter Number");
            if (!int.TryParse(Console.ReadLine(), out var num))
            {
                Console.WriteLine("Enter Num");
                Console.WriteLine();
                continue;
            }

            if (num == 1)
            {
                Game.Play();
            }
            else
            {
                AiMain.Main();
            }
        }
    }
}