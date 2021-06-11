using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Week_13_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FileReader fileReader = new FileReader();
            Dictionary<int, List<(int?,int)>> adjGraph = fileReader.ReadFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 13 Programming Assignment\TestCases\TestCase3.txt");

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

            ShortestPath shortestPath = new ShortestPath();
            List<int> shortestCost = new List<int>();

            foreach(KeyValuePair<int, List<(int?, int)>> entry in adjGraph)
            {
                int s = entry.Key - 1;
                Console.WriteLine("-source vertex: " + entry.Key + "----------");
                shortestCost.Add(shortestPath.Bellman_Ford(adjGraph, s));
            }            
            Console.WriteLine("----");
            Console.WriteLine(shortestCost.Min());
            Console.ReadKey();
        }
    }

    public class CustomArray<T>
    {
        public T[] GetColumn(T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        public T[] GetRow(T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }

    class ShortestPath
    {
        CustomArray<int> customArray = new CustomArray<int>();
        
        public int Bellman_Ford(Dictionary<int, List<(int?,int)>> adjGraph, int s)
        {
            // Subproblems: A[i,j]
            int n = adjGraph.Count;
            int[,] A = new int[n + 1, n];

            // base cases (i  = 0)
            A[0, s] = 0;
            foreach(KeyValuePair<int, List<(int?, int)>> entry in adjGraph)
            {
                int v = entry.Key - 1;
                if(v!=s)
                {
                    A[0, v] = 99999;
                }
            }

            // Systematically solve all subproblems
            bool stable = true;
            
            // the number of allowable jumps
            for (int i = 1; i < n + 1; i++)
            {
                stable = true;
                
                // Get the case 2 min  
                int w = 0;
                foreach(KeyValuePair<int, List<(int?, int)>> entry in adjGraph)
                {
                    // destination
                    int v = entry.Key-1;

                    int wvCost = 99999; 
                    foreach(KeyValuePair<int, List<(int?, int)>> E in adjGraph)
                    {

                        foreach((int?, int) head in E.Value)
                        {
                            if(head.Item1-1 == v && A[i-1, E.Key-1] < 99999)
                            {                            
                                int case_2Cost = A[i-1, E.Key-1] + head.Item2;
                                if(case_2Cost < wvCost)
                                {
                                    w = E.Key-1;
                                    wvCost = case_2Cost;
                                    Console.WriteLine(wvCost + " iteration: " + i + " destination " + v);
                                } 
                            }
                        }
                    }

                    // use Recurrence from Corollary 18.2
                    A[i,v] = Math.Min(A[i-1,v], wvCost);

                    if(A[i,v] != A[i-1,v])
                    {
                        stable = false;
                    }
                }

                if(stable)
                {
                    int[] rowAnswer = customArray.GetRow(A,i);
                    return rowAnswer.Min();
                }
            }
            return -545;
        }
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