using System;

namespace MemoryGame
{
    public class Menu
    {
        public void MainManu()
        {
            Console.Clear();
            Console.WriteLine(AsciiArt.title);
            Console.WriteLine("Input yor choice");
            Console.WriteLine("1. New game");
            Console.WriteLine("2. High score");
            Console.WriteLine("3. Exit");

            var notDone = true;
            while (notDone)
            {
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        Console.WriteLine("Game");
                        notDone = false;
                        GameMenu();
                        break;
                    case "2":
                        Console.WriteLine("High score");
                        notDone = false;
                        var highScore = new HighScore();
                        highScore.DisplayBest(int.MaxValue);
                        Console.WriteLine("Press any key");
                        Console.ReadKey();
                        MainManu();
                        break;
                    case "3":
                        Console.WriteLine("Exit");
                        notDone = false;
                        break;
                    default:
                        Console.WriteLine("I don't understand");
                        break;
                }
            }
        }

        private void GameMenu()
        {
            Console.Clear();
            Console.WriteLine(AsciiArt.title);
            Console.WriteLine("Choice your difficulty level:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Hard");
            Console.WriteLine("3. Return");

            var loadData = new LoadData($"{Environment.CurrentDirectory}/../../../Data/Words.txt");
            var data = loadData.Data();

            var notDone = true;

            while (notDone)
            {
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        Console.WriteLine("Easy level");
                        var gameGeneratorEasy = new GameGenerator(0, data);
                        var gameWordsEasy = gameGeneratorEasy.GenerateGameWords();
                        var gameEasy = new Game(gameWordsEasy[0], gameWordsEasy[1], 0);
                        Console.Clear();
                        gameEasy.RunGame();
                        notDone = false;
                        EndGameMenu();
                        break;
                    case "2":
                        Console.WriteLine("Hard level");
                        var gameGeneratorHard = new GameGenerator(1, data);
                        Console.WriteLine("gameGeneratorHard");
                        var gameWordsHard = gameGeneratorHard.GenerateGameWords();
                        Console.WriteLine("gameWordsHard");
                        var gameHard = new Game(gameWordsHard[0], gameWordsHard[1], 1);
                        Console.Clear();
                        gameHard.RunGame();
                        notDone = false;
                        EndGameMenu();
                        break;
                    case "3":
                        Console.WriteLine("Return");
                        MainManu();
                        notDone = false;
                        break;
                    default:
                        Console.WriteLine("I don't understand");
                        break;
                }
            }
        }

        private void EndGameMenu()
        {
            Console.Clear();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Play one more time");
            Console.WriteLine("2. Back to main menu");
            
            var notDone = true;

            while (notDone)
            {
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        Console.WriteLine("Play one more time");
                        GameMenu();
                        notDone = false;
                        break;
                    case "2":
                        Console.WriteLine("Back to main menu");
                        MainManu();
                        notDone = false;
                        break;
                    default:
                        Console.WriteLine("I don't understand");
                        break;
                }
            }
        }
    }
}