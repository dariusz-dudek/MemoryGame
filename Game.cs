using System;

namespace MemoryGame
{
    public class Game
    {
        private string[] wordsToGuessA;
        private string[] wordsToGuessB;
        private readonly int _level;

        public Game(string[] wordsToGuessA, string[] wordsToGuessB, int level)
        {
            this.wordsToGuessA = wordsToGuessA;
            this.wordsToGuessB = wordsToGuessB;
            this._level = level;
        }

        private int Chances()
        {
            return _level == 0 ? 10 : 15;
        }

        private void DrawGameScreen(string[] gameArrayA, string[] gameArrayB, int chances)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Guess chances: {chances}");
            Console.WriteLine("  1 2 3 4");
            Console.Write("A ");
            foreach (var s1 in gameArrayA)
            {
                Console.Write(s1 + " ");
            }

            Console.WriteLine();
            Console.Write("B ");
            foreach (var s1 in gameArrayB)
            {
                Console.Write(s1 + " ");
            }

            Console.WriteLine("\n-----------------------------------");
        }

        private char[] EnterCoordinates(string[] gameArrayA, string[] gameArrayB, string[] toGuessA,
            string[] toGuessB)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter coordinates");

                    var coordinates = Console.ReadLine().ToCharArray();


                    switch (coordinates[0])
                    {
                        case 'A':
                        case 'a':
                            gameArrayA[coordinates[1] - '0'] = toGuessA[coordinates[1] - '0'];
                            break;
                        case 'B':
                        case 'b':
                            gameArrayB[coordinates[1] - '0'] = toGuessB[coordinates[1] - '0'];
                            break;
                    }

                    return new[] {coordinates[0], coordinates[1]};
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Wrong data. Try one more time. ");
                }
                catch (Exception)
                {
                    Console.WriteLine("Some error. Try one more time. ");
                }
            }
        }
    }
}