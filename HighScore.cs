using System;
using System.Collections.Generic;
using System.IO;

namespace MemoryGame
{
    public class HighScore : IComparable
    {
        private readonly string name;
        private readonly DateTime dateTime;
        private readonly int guessingTime;
        private readonly int guessingTries;

        public HighScore(string name, DateTime dateTime, int guessingTime, int guessingTries)
        {
            this.name = name;
            this.dateTime = dateTime;
            this.guessingTime = guessingTime;
            this.guessingTries = guessingTries;
        }

        public HighScore()
        {
        }

        public void Save(string filename)
        {
            var path = $"{Environment.CurrentDirectory}/../../../Data/{filename}.txt";
            if (!File.Exists(path))
            {
                using var streamWriter = File.CreateText(path);
                streamWriter.WriteLine($"{name}|{dateTime}|{guessingTime}|{guessingTries}|");
            }
            else
            {
                using var streamWriter = File.AppendText(path);
                streamWriter.WriteLine($"{name}|{dateTime}|{guessingTime}|{guessingTries}|");
            }
        }

        public static List<HighScore> Load(string filename)
        {
            var list = new List<HighScore>();
            var path = $"{Environment.CurrentDirectory}/../../../Data/{filename}.txt";
            using var streamReader = File.OpenText(path);
            var s = "";
            while ((s = streamReader.ReadLine()) != null)
            {
                var data = s.Split("|");
                list.Add(new HighScore(data[0], Convert.ToDateTime(data[1]), Convert.ToInt32(data[2]),
                    Convert.ToInt32(data[3])));
            }

            return list;
        }

        public int CompareTo(object obj)
        {
            var arg = (HighScore) obj;
            if (guessingTries < arg.guessingTries)
            {
                return 1;
            }
            else if (guessingTries == arg.guessingTries)
            {
                if (guessingTime < arg.guessingTime)
                {
                    return -1;
                }
                else if (guessingTime == arg.guessingTime)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }

        public void DisplayBest(int number)
        {
            var data = Load("HighScore");
            data.Sort();
            if (data.Count < number)
            {
                foreach (var highScore in data)
                {
                    Console.WriteLine(
                        $"{highScore.name} {highScore.dateTime} {highScore.guessingTime} {highScore.guessingTries}");
                }
            }

            {
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine(
                        $"{data[i].name} {data[i].dateTime} {data[i].guessingTime} {data[i].guessingTries}");
                }
            }
        }
    }
}