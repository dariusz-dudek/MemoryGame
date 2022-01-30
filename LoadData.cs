using System;
using System.Collections.Generic;
using System.IO;

namespace MemoryGame
{
    public class LoadData
    {
        private readonly string _path;

        public LoadData(string path)
        {
            _path = path;
        }

        public List<string> Data()
        {
            string s;
            var streamReader = File.OpenText(_path);
            var list = new List<string>();
            while ((s = streamReader.ReadLine()) != null)
            {
                list.Add(s);
            }
            
            streamReader.Close();

            return list;
        }
    }
}