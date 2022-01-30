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

        public void RunGame()
        {
            var gameCounter = 0;
            var guessChances = Chances();
            string[] gameArrayA = {"X", "X", "X", "X"};
            var gameArrayATemp = new string[gameArrayA.Length];
            gameArrayA.CopyTo(gameArrayATemp, 0);

            string[] gameArrayB = {"X", "X", "X", "X"};
            var gameArrayBTemp = new string[gameArrayB.Length];
            gameArrayB.CopyTo(gameArrayBTemp, 0);
            
            while ((gameCounter < 4) && (guessChances > 0))
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
                    coordinates[1] =  Convert.ToChar((coordinates[1] - '0' - 1).ToString());


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

    }
}