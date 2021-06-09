using System;
using System.Collections.Generic;
using System.IO;

namespace Week_13_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FileReader fileReader = new FileReader();
            Dictionary<int, List<(int?,int)>> adjGraph = fileReader.ReadFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 13 Programming Assignment\TestCases\TestCase1.txt");

            foreach(KeyValuePair<int, List<(int?, int)>> entry in adjGraph)
            {
                Console.Write(entry.Key + ": " );
                foreach((int?, int) ent in entry.Value)
                {
                    Console.Write(ent.Item1 + ",");
                    Console.Write(ent.Item2);
                    Console.Write("; ");
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }

    class ShortestPath
    {
        // Insert stuffs here tomorrow paul
    }

    class FileReader
    {
        public Dictionary<int, List<(int?, int)>> ReadFile(string dir)
        {
            Dictionary<int, List<(int?,int)>> adjGraph = new Dictionary<int, List<(int?, int)>>();
            string[] lines = File.ReadAllLines(dir);
            for(int i = 1; i < lines.Length; i++)
            {
                string[] lin = lines[i].Split(" ");
                int tailCand = Convert.ToInt32(lin[0]);
                int headCand = Convert.ToInt32(lin[1]);
                int cost = Convert.ToInt32(lin[2]);

                PopulateGraph(adjGraph, tailCand, headCand, cost);
            }

            return adjGraph;
        }
        void PopulateGraph(Dictionary<int, List<(int?,int)>> adjGraph, int tail, int head, int cost)
        {
            if(!adjGraph.ContainsKey(tail))
            {
                List<(int?, int)> paths = new List<(int?, int)>();
                paths.Add((head, cost));
                adjGraph.Add(tail, paths);
            }
            else
            {
                adjGraph[tail].Add((head, cost));
            }
        }
    }
}