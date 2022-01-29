using System;
using System.Collections.Generic;

namespace MemoryGame
{
    public class Game
    {
        private readonly List<string> data;
        private readonly int level;

        public Game(int level, List<string> data)
        {
            this.level = level;     // 0 - easy, 1 - hard
            this.data = data;
        }

        private int Chances()
        {
            return level == 0 ? 10 : 15;
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
        
        
    }
}