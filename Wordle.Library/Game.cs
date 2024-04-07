using System.Reflection;

namespace Wordle.Library;

public static class Game
{
    public static void Play()
    {
        var wordsList = new List<string>();

        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("wordList.txt"));

        using (var stream = assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new(stream))
        {
            while (reader.ReadLine() != null)
            {
                wordsList.Add(reader.ReadLine());
            }
        }
        var words = wordsList.ToArray();

        string Guess;
        string returnGuess;
        var repeat = true;
        //Split off for variables if wanted to having multiple games without having to restart program
        while (repeat == true)
        {
            var wordLength = false; //Loop around to make sure user input is valid
            var notNum = false;
            var inWord = false; //Stupid solution to a stupid problem
            var correct = false;

            //pick random word from the list
            var randPick = new Random().Next(words.Length);
            var chosenWord = words[randPick];
            //Console.WriteLine(chosenWord); //Testing Purposes
            var instructions = new List<string> { "Green means the right letter is in the right place.", "Blue means the letter is in the word, just in the wrong place.", "Finally, red means that the letter isn't in the word at all." };
            var colourList = new ConsoleColor[] { ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red };

            //Instructions that are very poorly coded for shitty wordle
            Console.WriteLine("Welcome to shitty wordle");

            for (var wMessage = 0; wMessage < instructions.Count; wMessage++)
            {
                Console.ForegroundColor = colourList[wMessage];
                Console.WriteLine(instructions[wMessage]);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Try to guess the random ");
            Console.Write(chosenWord.Length);
            Console.WriteLine(" letter word");

            //For loop here to allow multiple tries
            for (var tries = 0; tries < 6; tries++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                do
                {
                    wordLength = false;
                    notNum = false;
                    Guess = Console.ReadLine();
                    returnGuess = Guess;
                    Guess = Guess.ToLower(); //Case matching
                    Console.ForegroundColor = colourList[2];
                    if (Guess.Length == chosenWord.Length)
                    {
                        wordLength = true;
                        notNum = Guess.All(c => c >= 'a' && c <= 'z'); //Copy and pasted but know what it does, was just lazy to fugure it out myself https://www.techiedelight.com/check-if-string-contains-only-letters-in-csharp/
                    }
                    else
                    {
                        Console.WriteLine("Please pick a 5 letter word");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                } while (wordLength == false || notNum == false);

                //Time to compare the guess vs word
                if (chosenWord == Guess)
                {
                    Console.ForegroundColor = colourList[0];
                    Console.Write(returnGuess);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                else //Fuck this next bit
                {
                    //Check to see if a letter is in the right place
                    for (var pos = 0; pos < Guess.Length; pos++)
                    {
                        if (Guess[pos] == chosenWord[pos])
                        {

                            Console.ForegroundColor = colourList[0];
                            Console.Write(returnGuess[pos]);
                            Console.ForegroundColor = colourList[2];
                        }
                        else if (Guess[pos] != chosenWord[pos]) //Fuck this thing
                        {
                            for (var Wpos2 = 0; Wpos2 < Guess.Length; Wpos2++)
                            {
                                inWord = false;
                                if (Guess[pos] == chosenWord[Wpos2])
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write(returnGuess[pos]);
                                    Console.ForegroundColor = colourList[2];
                                    inWord = true;
                                    break;
                                }
                            }
                            if (inWord == false) //Stupid solution :)
                            {
                                Console.Write(returnGuess[pos]);
                            }
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            if (correct == false)
            {
                Console.Write("Out of tries! The correct answer is ");
                Console.WriteLine(chosenWord);
                Console.WriteLine();

            }
            var CanPress1orQ = false;
            do
            {
                Console.WriteLine("Do you want to go agian?");
                Console.WriteLine("Press 1 for yes or Q to quit");
                var playAgain = Console.ReadLine().ToLower();

                if (playAgain == "1")
                {
                    CanPress1orQ = true;
                }
                else if (playAgain == "q")
                {
                    repeat = false;
                    CanPress1orQ = true;
                }
                else
                {
                    Console.WriteLine("Not a valid input");
                }
            } while (CanPress1orQ == false);
        }
    }
}
