using System.Reflection;
using Wordle.Library;

namespace Wordle.Ai;
public class AiMain
{
    public string Guess(GuessResult? previousResult)
    {
        //Take in previous result, which might be null, return a guess

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

        return wordsList[new Random().Next(wordsList.Count)];
    }
}
