using System;

namespace MemoryGame
{
    class Program
    {
        static void Main(string[] args)
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
    }
}