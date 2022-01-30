using System;
using System.Diagnostics;

namespace MemoryGame
{
    public class Game
    {
        private string[] wordsToGuessA;
        private string[] wordsToGuessB;
        private readonly int _level;
        private Stopwatch watch;
        public decimal time { get; private set; }

        public Game(string[] wordsToGuessA, string[] wordsToGuessB, int level)
        {
            this.wordsToGuessA = wordsToGuessA;
            this.wordsToGuessB = wordsToGuessB;
            _level = level;
        }

        public void RunGame()
        {
            var gameCounter = 0;
            var guessChances = Chances();
            var gameArrayA = _level == 0 ? new []{"X", "X", "X", "X"} : new []{"X", "X", "X", "X", "X", "X", "X", "X"};
            var gameArrayATemp = new string[gameArrayA.Length];
            gameArrayA.CopyTo(gameArrayATemp, 0);

            var gameArrayB = _level == 0 ? new []{"X", "X", "X", "X"} : new []{"X", "X", "X", "X", "X", "X", "X", "X"};
            var gameArrayBTemp = new string[gameArrayB.Length];
            gameArrayB.CopyTo(gameArrayBTemp, 0);

            int levelCounter = _level == 0 ? 4 : 8;

            watch = Stopwatch.StartNew();

            while ((gameCounter < levelCounter) && (guessChances > 0))
            {
                DrawGameScreen(gameArrayA, gameArrayB, guessChances);

                var enterCoordinates1 = EnterCoordinates(gameArrayA, gameArrayB, wordsToGuessA, wordsToGuessB);

                Console.Clear();

                DrawGameScreen(gameArrayA, gameArrayB, guessChances);

                var enterCoordinates2 = EnterCoordinates(gameArrayA, gameArrayB, wordsToGuessA, wordsToGuessB);

                Console.Clear();

                DrawGameScreen(gameArrayA, gameArrayB, guessChances);

                if (CheckingAnswers(enterCoordinates1, enterCoordinates2, wordsToGuessA, wordsToGuessB))
                {
                    gameArrayA.CopyTo(gameArrayATemp, 0);
                    gameArrayB.CopyTo(gameArrayBTemp, 0);
                    gameCounter++;
                    guessChances--;
                }
                else
                {
                    gameArrayATemp.CopyTo(gameArrayA, 0);
                    gameArrayBTemp.CopyTo(gameArrayB, 0);
                    guessChances--;
                }

                Console.WriteLine("Press any key");
                Console.ReadKey();
                Console.Clear();
            }
            watch.Stop();

            time = Math.Round((decimal) (watch.ElapsedMilliseconds / 1000));

            FinalMessage(guessChances);
        }

        private int Chances()
        {
            return _level == 0 ? 10 : 15;
        }

        private void DrawGameScreen(string[] gameArrayA, string[] gameArrayB, int chances)
        {
            Console.WriteLine("-----------------------------------");
            Console.Write("     ");
            Console.WriteLine(_level == 0 ? "Level: easy" : "Level: hard");
            Console.Write("     ");
            Console.WriteLine($"Guess chances: {chances}\n");
            Console.Write("     ");
            Console.WriteLine(_level == 0 ? "  1 2 3 4" : "  1 2 3 4 5 6 7 8");
            Console.Write("     ");
            Console.Write("A ");
            
            foreach (var s1 in gameArrayA)
            {
                Console.Write(s1 + " ");
            }

            Console.WriteLine();
            Console.Write("     ");
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
                    coordinates[1] = Convert.ToChar((coordinates[1] - '0' - 1).ToString());


                    switch (coordinates[0])
                    {
                        case 'A':
                        case 'a':
                            gameArrayA[(coordinates[1] - '0')] = toGuessA[(coordinates[1] - '0')];
                            break;
                        case 'B':
                        case 'b':
                            gameArrayB[(coordinates[1] - '0')] = toGuessB[(coordinates[1] - '0')];
                            break;
                    }

                    return new[] {coordinates[0], coordinates[1]};
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Wrong data. Try one more time. " + e);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Some error. Try one more time. " + e);
                }
            }
        }

        private bool CheckingAnswers(char[] answer1, char[] answer2, string[] toGuessA,
            string[] toGuessB)
        {
            return toGuessA[answer1[1] - '0'] == toGuessB[answer2[1] - '0'];
        }

        private void FinalMessage(int guessChances)
        {
            Console.WriteLine(guessChances > 0
                ? $"You solved the memory game after {Chances() - guessChances} chances. It took you {time} seconds"
                : $"You lose. It took you {time} seconds");
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}