using System;
using System.Collections.Generic;

namespace MemoryGame
{
    public class GameGenerator
    {
        private readonly List<string> data;
        private readonly int level;

        public GameGenerator(int level, List<string> data)
        {
            this.level = level; // 0 - easy, 1 - hard
            this.data = data;
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
            return level == 0 ? 4 : 8;
        }

        private int[] RandomIndexWords()
        {
            var random = new Random();
            var randomIndex = new HashSet<int>();
            while (randomIndex.Count < NumbersOfWords())
            {
                randomIndex.Add(random.Next(0, data.Count));
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
                wordsToGuessA[i] = data[randomIndexWords[i]];
            }

            return wordsToGuessA;
        }

        private string[] WordsToGuessB(string[] wordsToGuessA)
        {
            var random = new Random();
            var randomIndexToGuess = new HashSet<int>();
            while (randomIndexToGuess.Count < NumbersOfWords())
            {
                randomIndexToGuess.Add(random.Next(0, 4));
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