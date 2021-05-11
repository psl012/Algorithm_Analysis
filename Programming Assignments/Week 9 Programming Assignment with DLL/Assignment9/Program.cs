using System;
using System.Collections.Generic;
using JobLibrary;
using GraphLibrary;
using System.IO;

namespace Assignment9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Job hello = new Job(23,23);
            JobFileReader fileReader = new JobFileReader();
            (int, Job[]) myJobData = fileReader.ReadJobFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 9 Programming Assignment\TestCases\Test Case 1.txt");
            JobScheduler jobScheduler = new JobScheduler();
            Job[] gg = jobScheduler.DifferenceCriterion(myJobData.Item2);
            foreach(Job j in gg)
            {
                Console.WriteLine(j._jobValue);
            }
            Console.WriteLine("---------------------------");
            Week9FileReader fileReader2 = new Week9FileReader();

            Dictionary<int, List<(int?, int)>> adjacentGraph = fileReader2.GraphFileReader(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 9 Programming Assignment with DLL\Assignment9\Unit Test Data.txt");
            foreach(KeyValuePair<int, List<(int?, int)>> test in adjacentGraph)
            {
                Console.Write(test.Key + " " );
                foreach((int?, int) ww in test.Value)
                {
                    Console.Write(ww.Item1 + "," + ww.Item2 + " ");
                }
                Console.WriteLine();
            }


            Console.ReadKey();
        }

        
    }

    class Week9FileReader
    {
        public Dictionary<int, List<(int?, int)>> GraphFileReader(string dir)
        {
            
            Dictionary<int, List<(int?, int)>> adjacentGraph = new Dictionary<int, List<(int?, int)>>();
            string[] lineData = File.ReadAllLines(dir);

            for(int i=1; i < lineData.Length; i++)
            {
                string[] line = lineData[i].Split(" ");          
                int node1 = Convert.ToInt32(line[0]);
                int node2 = Convert.ToInt32(line[1]);
                InitializeAdjGraph(adjacentGraph, node1);
                InitializeAdjGraph(adjacentGraph, node2);
            }

            for(int i=1; i < lineData.Length; i++)
            {
                string[] line = lineData[i].Split(" ");
                int tail = Convert.ToInt32(line[0]);
                int head = Convert.ToInt32(line[1]);
                int cost = Convert.ToInt32(line[2]);

                if(adjacentGraph[tail][0].Item1 == null)
                {
                    adjacentGraph[tail].RemoveAt(0);
                }

                adjacentGraph[tail].Add((head, cost));
            }

            return adjacentGraph;    
        }

        void InitializeAdjGraph(Dictionary<int, List<(int?, int)>> adjGraph, int node)
        {
            if(!adjGraph.ContainsKey(node))
            {
                List<(int?, int)> paths = new List<(int?, int)>();
                paths.Add((null, -9999));
                adjGraph.Add(node, paths);
            }
        }
    }
}
