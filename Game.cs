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
        private int time { get; set; }

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
            var gameArrayA = _level == 0 ? new[] {"X", "X", "X", "X"} : new[] {"X", "X", "X", "X", "X", "X", "X", "X"};
            var gameArrayATemp = new string[gameArrayA.Length];
            gameArrayA.CopyTo(gameArrayATemp, 0);

            var gameArrayB = _level == 0 ? new[] {"X", "X", "X", "X"} : new[] {"X", "X", "X", "X", "X", "X", "X", "X"};
            var gameArrayBTemp = new string[gameArrayB.Length];
            gameArrayB.CopyTo(gameArrayBTemp, 0);

            var levelCounter = _level == 0 ? 4 : 8;

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

            time = (int) Math.Round((decimal) (watch.ElapsedMilliseconds / 1000));

            FinalMessage(guessChances);

            if (guessChances > 0)
            {
                SaveHighScore(time, Chances() - guessChances);
            }
        }

        private int Chances()
        {
            return _level == 0 ? 10 : 15;
        }

        private void DrawGameScreen(string[] gameArrayA, string[] gameArrayB, int chances)
        {
            Console.WriteLine(AsciiArt.title);
            Console.WriteLine(DrawLineToTable(LengthEveryStringFromArray(gameArrayA), LengthEveryStringFromArray(gameArrayB), "-"));
            Console.WriteLine();
            Console.Write("   ");
            Console.WriteLine(_level == 0 ? "Level: easy" : "Level: hard");
            Console.Write("   ");
            Console.WriteLine($"Guess chances: {chances}\n");
            Console.WriteLine(DrawLineToTable(LengthEveryStringFromArray(gameArrayA), LengthEveryStringFromArray(gameArrayB), "+"));
            Console.WriteLine(DrawNumbersToLIne(LengthEveryStringFromArray(gameArrayA), LengthEveryStringFromArray(gameArrayB)));
            Console.WriteLine(DrawLineToTable(LengthEveryStringFromArray(gameArrayA), LengthEveryStringFromArray(gameArrayB), "+"));
            Console.Write("| A |");

            for (int i = 0; i < gameArrayA.Length; i++)
            {
                string add = AddSpaces(gameArrayA, gameArrayB, i);
                Console.Write(" " + gameArrayA[i] + add + " |");
            }

            // foreach (var s1 in gameArrayA)
            // {
            //     Console.Write(" "+ s1 + " |");
            // }

            Console.WriteLine();
            Console.WriteLine(DrawLineToTable(LengthEveryStringFromArray(gameArrayA), LengthEveryStringFromArray(gameArrayB), "+"));
            Console.Write("| B |");
            
            for (int i = 0; i < gameArrayB.Length; i++)
            {
                string add = AddSpaces(gameArrayB, gameArrayA, i);
                Console.Write(" " + gameArrayB[i] + add + " |");
            }
            
            Console.WriteLine();
            Console.WriteLine(DrawLineToTable(LengthEveryStringFromArray(gameArrayA), LengthEveryStringFromArray(gameArrayB), "+"));
            Console.WriteLine();
            Console.WriteLine(DrawLineToTable(LengthEveryStringFromArray(gameArrayA), LengthEveryStringFromArray(gameArrayB), "-"));
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

        private void SaveHighScore(int guessingTime, int guessingTries)
        {
            Console.WriteLine("Enter the name");
            var name = Console.ReadLine();

            var highScore = new HighScore(name, DateTime.Now, guessingTime, guessingTries);
            highScore.Save("HighScore");
            highScore.DisplayBest(10);
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
        

        private int[] LengthEveryStringFromArray(string[] array)
        {
            var a = new int[array.Length];

            for (var i = 0; i < array.Length; i++)
            {
                a[i] = array[i].Length;
            }

            return a;
        }

        private string DrawLineToTable(int[] array1, int[] array2, string separator)
        {
            string line = $"{separator}---{separator}";

            for (int i = 0; i < array1.Length; i++)
            {
                line += "-";
                if (array1[i] >= array2[i])
                {
                    for (int j = 0; j < array1[i]; j++)
                    {
                        line += "-";
                    }
                }
                else
                {
                    for (int j = 0; j < array2[i]; j++)
                    {
                        line += "-";
                    }
                }

                line += $"-{separator}";
            }

            return line;
        }
        
        private string DrawNumbersToLIne(int[] array1, int[] array2)
        {
            string line = "|   |";

            for (int i = 0; i < array1.Length; i++)
            {
                line += " " + (i + 1);
                if (array1[i] >= array2[i])
                {
                    for (int j = 0; j < array1[i]; j++)
                    {
                        line += " ";
                    }
                }
                else
                {
                    for (int j = 0; j < array2[i]; j++)
                    {
                        line += " ";
                    }
                }

                line += "|";
            }

            return line;
        }

        private string AddSpaces(string[] array1, string[] array2, int i)
        {
            string add = "";
            if (array1[i].Length < array2[i].Length)
            {
                for (int j = 0; j < array2[i].Length - array1[i].Length; j++)
                {
                    add += " ";
                }
            }

            return add;
        }
    }
}