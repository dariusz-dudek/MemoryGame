using System;

namespace MemoryGame
{
    public class Menu
    {
        public void MainManu()
        {
            Console.WriteLine("Memory Game");
            Console.WriteLine("Input yor choice");
            Console.WriteLine("1. New game");
            Console.WriteLine("2. Exit");
            
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
                        Console.WriteLine("Exit");
                        notDone = false;
                        break;
                    default:
                        Console.WriteLine("I don't understand");
                        break;
                }
            }
        }

        public void GameMenu()
        {
            Console.Clear();
            Console.WriteLine("Choice your difficulty level:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Hard");
            Console.WriteLine("3. Return");
            
            var notDone = true;

            while (notDone)
            {
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        Console.WriteLine("Easy level");
                        notDone = false;
                        break;
                    case "2":
                        Console.WriteLine("Hard level");
                        notDone = false;
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
    }
}