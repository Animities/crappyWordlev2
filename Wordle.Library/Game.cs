//Questions:
//
//-How to get the random word to be used in the other methods?
//
//
//


using System.Reflection;

namespace Wordle.Library;

public class Game
{
    public int Attempts { get; set; }
    public bool IsGameDone { get; set; }

    public string ChosenWord { get; set; }

    //TODO implement this




    public void SetChosenWord()//This is shit for repeatable fast speed reruns for the AI (Due to recreation of the list over and over again)
    {
        var wordsList = new List<string>();

        if (wordsList.Count == 0)
        {
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
        }
        //////////////////////////////////////////////////
        ChosenWord = wordsList[new Random().Next(wordsList.Count)];

    }

    public void StartGame()
    {
        SetChosenWord();

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
        Console.Write(5);
        Console.WriteLine(" letter word");


        //starting stuff, goal word picker, directions, etc
    }
    //TODO implement this
    public GuessResult Guess(string guess)
    {
        //Compute guess, show colors, check if last attempt was made, check if user won, etc
        //Set isGameDone based on win/loss
        var cWord = ChosenWord;
        bool notNum = false;
        bool inWord = false;
        bool correct = false;
        var result = new GuessResult();

        if (Attempts < 6)
        {

            Console.ForegroundColor = ConsoleColor.White;
            notNum = false;
            guess = guess.ToLower(); //Case matching
            if (guess.Length == cWord.Length)
            {
                notNum = guess.All(c => c >= 'a' && c <= 'z'); //Copy and pasted but know what it does, was just lazy to fugure it out myself https://www.techiedelight.com/check-if-string-contains-only-letters-in-csharp/
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please pick a 5 letter word");
                Console.ForegroundColor = ConsoleColor.White;
                return result;
            }
            Attempts += 1;
            //Time to compare the guess vs word
            if (cWord == guess)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(guess);
                Console.ForegroundColor = ConsoleColor.White;
                correct = true;
            }
            else //Fuck this next bit
            {
                //Check to see if a letter is in the right place
                for (var pos = 0; pos < guess.Length; pos++)
                {
                    if (guess[pos] == cWord[pos])
                    {
                        SetCorrectResult(result, pos);
                        //Console.ForegroundColor = ConsoleColor.Green;
                        //Console.Write(guess[pos]);
                    }
                    else if (guess[pos] != cWord[pos]) //Fuck this thing
                    {
                        for (var Wpos2 = 0; Wpos2 < guess.Length; Wpos2++)
                        {
                            inWord = false;
                            if (guess[pos] == cWord[Wpos2])
                            {
                                //TODO: Create a method similar to SetCorrectResult for setting this 
                                //Console.ForegroundColor = ConsoleColor.Blue;
                                //Console.Write(guess[pos]);
                                inWord = true;
                                break;
                            }
                        }
                        if (inWord == false) //Stupid solution :)
                        {
                            //TODO: Create a method similar to SetCorrectResult for setting this 
                            //Console.ForegroundColor = ConsoleColor.Red;
                            //Console.Write(guess[pos]);
                        }
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        if (correct == false && Attempts == 6)
        {
            Console.Write("Out of tries! The correct answer is ");
            Console.WriteLine(cWord);
            Console.WriteLine();
            IsGameDone = true;
        }

        else if (correct == true)
        {
            IsGameDone = true;
        }

        //if (false)//user won the game
        //{
        //    IsGameDone = true;
        //}

        WriteResultToConsole(result, guess);
        Console.ForegroundColor = ConsoleColor.White;
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

    private static void SetCorrectResult(GuessResult result, int pos)
    {
        switch (pos)
        {
            case 0:
                result.First = LetterState.Correct;
                break;
            case 1:
                result.Second = LetterState.Correct;
                break;
            case 2:
                result.Third = LetterState.Correct;
                break;
            case 3:
                result.Fourth = LetterState.Correct;
                break;
            case 4:
                result.Fifth = LetterState.Correct;
                break;
        }
    }

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