using System;
using System.IO;

namespace MemoryGame
{
    public class HighScore
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

        public void Save(string filename)
        {
            var path = $"{Environment.CurrentDirectory}/../../../Data/{filename}.txt";
            if (!File.Exists(path))
            {
                using var streamWriter = File.CreateText(path);
                streamWriter.WriteLine($"{name}|{dateTime}|{guessingTime}|{guessingTries}");
            }
            else
            {
                using var streamWriter = File.AppendText(path);
                streamWriter.WriteLine($"{name}|{dateTime}|{guessingTime}|{guessingTries}");
            }
        }
    }
}