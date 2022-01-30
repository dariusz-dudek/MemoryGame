using System;
using System.Collections.Generic;

namespace MemoryGame
{
    public class GameGenerator
    {
        private readonly List<string> _data;
        private readonly int _level;

        public GameGenerator(int level, List<string> data)
        {
            _level = level; // 0 - easy, 1 - hard
            _data = data;
        }


        public string[][] GenerateGameWords()
        {
            var randomIndexWords = RandomIndexWords();
            var wordsToGuessA = WordsToGuessA(randomIndexWords);
            var wordsToGuessB = WordsToGuessB(wordsToGuessA);
            return (new[] {wordsToGuessA, wordsToGuessB});
        }

        private int NumbersOfWords()
        {
            return _level == 0 ? 4 : 8;
        }

        private int[] RandomIndexWords()
        {
            var random = new Random();
            var randomIndex = new HashSet<int>();
            while (randomIndex.Count < NumbersOfWords())
            {
                randomIndex.Add(random.Next(0, _data.Count));
            }

            var randomIndexArrayA = new int[randomIndex.Count];
            randomIndex.CopyTo(randomIndexArrayA);
            return randomIndexArrayA;
        }

        private string[] WordsToGuessA(int[] randomIndexWords)
        {
            var wordsToGuessA = new string[NumbersOfWords()];
            for (var i = 0; i < wordsToGuessA.Length; i++)
            {
                wordsToGuessA[i] = _data[randomIndexWords[i]];
            }

            return wordsToGuessA;
        }

        private string[] WordsToGuessB(string[] wordsToGuessA)
        {
            var random = new Random();
            Console.WriteLine(random);
            var randomIndexToGuess = new HashSet<int>();
            Console.WriteLine(randomIndexToGuess);
            while (randomIndexToGuess.Count < NumbersOfWords())
            {
                randomIndexToGuess.Add(random.Next(0, NumbersOfWords()));
            }

            var randomIndexArrayB = new int[NumbersOfWords()];
            randomIndexToGuess.CopyTo(randomIndexArrayB);

            var wordsToGuessB = new string[NumbersOfWords()];
            for (var i = 0; i < wordsToGuessB.Length; i++)
            {
                wordsToGuessB[i] = wordsToGuessA[randomIndexArrayB[i]];
            }

            return wordsToGuessB;
        }
    }
}