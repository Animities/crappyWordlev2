﻿//TODO:

using Wordle.Ai;
using Wordle.Library;

class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Do you want to play old (0) or clean manually (1) or AI (2)?");
            Console.WriteLine("Enter Number");
            if (!int.TryParse(Console.ReadLine(), out var num))
            {
                Console.WriteLine("Enter Num");
                Console.WriteLine();
                continue;
            }

            if (num == 0)
            {
                Game.Play();
            }
            if (num == 1)
            {
                var game = new Game();
                game.StartGame();

                while (!game.IsGameDone)
                {
                    var userGuess = Console.ReadLine();
                    _ = game.Guess(userGuess);
                }
            }
            else
            {
                var game = new Game();
                game.StartGame();

                var ai = new AiMain();
                GuessResult? previousResult = null;
                while (!game.IsGameDone)
                {
                    var aiGuess = ai.Guess(previousResult);
                    previousResult = game.Guess(aiGuess);
                }
            }
        }
    }
}