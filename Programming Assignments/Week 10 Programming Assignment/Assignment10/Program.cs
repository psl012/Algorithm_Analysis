using System;
using System.Collections.Generic;
using GraphLibrary;
using System.IO;

namespace Assignment10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            FileReader fileReader = new FileReader();
            Dictionary<int, List<(int?,int)>> adjGraph = fileReader.ReadFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 10 Programming Assignment\TestCases\TestCase1 - K_2.txt");
            Graph graph = new Graph(adjGraph);
        
            // Act
            Clustering clustering = new Clustering();
            (Dictionary<int,Cluster>, int) clusterAnswer = clustering.GroupCluster(graph, 2);

            foreach(KeyValuePair<int, Cluster> cl in clusterAnswer.Item1)
            {
                Console.WriteLine("set--");
                foreach(KeyValuePair<int?, VertexClus> en in cl.Value.elements)
                {
                    Console.WriteLine(en.Value._number);
                }

            }

            Console.ReadKey();
        }
    }

    class VertexClus : Vertex
    {
        public int _clusterNumber;
        public VertexClus(int? number, int clusterNumber) : base(number)
        {
            _clusterNumber = clusterNumber;
        }
    }

    class Clustering
    {
        public (Dictionary<int, Cluster>, int) GroupCluster(Graph graph, int k)
        {
            // Initialization
            Dictionary<int?, VertexClus> dictOfvClus = new Dictionary<int?, VertexClus>();
            Dictionary<int, Cluster> dict_int_cluster = new Dictionary<int, Cluster>();
            InitializeCluster(dictOfvClus, dict_int_cluster, graph);

            int minCost;

            while(dict_int_cluster.Count > k)
            {
                (int, int) clusterNumber = MinCandidate(dictOfvClus, graph);

                Cluster tailCluster = dict_int_cluster[clusterNumber.Item1]; 
                Cluster headCluster = dict_int_cluster[clusterNumber.Item2];
                if(tailCluster.Size() > headCluster.Size())
                {
                    ModifyCluster(dict_int_cluster, tailCluster, headCluster);
                }
                else
                {
                    ModifyCluster(dict_int_cluster, headCluster, tailCluster);
                }
            }

            minCost = 9999999;
            foreach(KeyValuePair<String, Edge> entry in graph._edges)
            {
                if(dictOfvClus[entry.Value._tail._number]._clusterNumber != dictOfvClus[entry.Value._head._number]._clusterNumber)
                {
                    if(entry.Value._length < minCost)
                    {
                        minCost = entry.Value._length;
                    }
                }
            }          
            return (dict_int_cluster, minCost);
        }

        (int, int) MinCandidate(Dictionary<int?, VertexClus> dictOfvClus, Graph graph)
        {
            int minCost = 9999999;
            int tailClusterNumber = 1;
            int headClusterNumber = 1;
            foreach(KeyValuePair<String, Edge> entry in graph._edges)
            {
                VertexClus vertexTailClus = dictOfvClus[entry.Value._tail._number];
                VertexClus vertexHeadClus = dictOfvClus[entry.Value._head._number];

                if(vertexTailClus._clusterNumber != vertexHeadClus._clusterNumber)
                {
                    if(entry.Value._length < minCost)
                    {
                        minCost = entry.Value._length;
                        tailClusterNumber = vertexTailClus._clusterNumber;
                        headClusterNumber = vertexHeadClus._clusterNumber;
                    }
                }
            }

            return (tailClusterNumber, headClusterNumber);
        }

        void InitializeCluster(Dictionary<int?, VertexClus> dictOfvClus, Dictionary<int, Cluster> dict_int_cluster, Graph graph)
        {
            foreach(KeyValuePair<int?, Vertex> entry in graph._vertices)
            {
                VertexClus vertexClus = new VertexClus(entry.Value._number, 0);
                dictOfvClus.Add(vertexClus._number, vertexClus);
            }

            int counter = 0;
            foreach(KeyValuePair<int?, VertexClus> entry in dictOfvClus)
            {
                List<VertexClus> lovc = new List<VertexClus>() {entry.Value} ;
                dict_int_cluster.Add(counter, new Cluster(lovc, counter));
                counter++;
            }
        }

        void ModifyCluster(Dictionary<int, Cluster> dict, Cluster tail, Cluster head)
        {
            foreach(KeyValuePair<int?, VertexClus> vc in head.elements)
            {
                vc.Value._clusterNumber = tail._number;
            }

            tail.addCluster(head);
            dict.Remove(head._number);
        }
    }

    class Cluster
    {
        public Dictionary<int?, VertexClus> elements = new Dictionary<int?, VertexClus>();
        public int _number;
 
        public Cluster(List<VertexClus> node, int number)
        {
            _number = number;
            foreach(VertexClus v in node)
            {
                v._clusterNumber = _number;
                elements.Add(v._number, v);
            }
        }

        public int Size()
        {
            return elements.Count;
        }  

        public void addCluster(Cluster cluster)
        {
            foreach(KeyValuePair<int?, VertexClus> entry in cluster.elements)
            {
                elements.Add(entry.Key, entry.Value);
            }
        }
    }

    class FileReader
    {
        public Dictionary<int, List<(int?,int)>> ReadFile(string dir)
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
                PopulateGraph(adjGraph, headCand, tailCand, cost);
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