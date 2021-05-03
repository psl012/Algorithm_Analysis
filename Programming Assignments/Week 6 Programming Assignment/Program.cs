using System;
using System.Collections.Generic;
using System.IO;

namespace Week_6_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Dijkstra dijkstra = new Dijkstra();
            Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 6 Programming Assignment\Week 6 Data Partial.txt");
            dijkstra.Apply(graph, graph._vertices[1]);
            dijkstra.PrintAnswer();
            Console.ReadKey();
        }
    }

    class Dijkstra
    {
        public SortedDictionary<int?, List<Vertex>> _myPath = new SortedDictionary<int?, List<Vertex>>();
        Graph _graph;

        public SortedDictionary<int?, List<Vertex>> Apply(Graph graph, Vertex s)
        {
            _graph = graph;
            List<Vertex> X = new List<Vertex>();
            s._dijkstraLength = 0;
            X.Add(s);

            bool isCrossed = true;
            while(isCrossed)
            {
                Edge shortestPath = null;
                int tempDijkstraLength = 1000000;
                isCrossed = false;
                foreach(KeyValuePair<string, Edge> entry in graph._edges)
                {
                    if(X.Contains(entry.Value._tail) && !X.Contains(entry.Value._head))
                    {
                        int totalLength = entry.Value._tail._dijkstraLength + entry.Value._length;
                        if(totalLength < tempDijkstraLength)
                        {
                            shortestPath = entry.Value;
                            tempDijkstraLength = totalLength;
                            isCrossed = true;
                        }
                    }
                }

                if(shortestPath != null)
                {
                    shortestPath._head._dijkstraLength = tempDijkstraLength;
                    shortestPath._head._link = shortestPath._tail;
                    X.Add(shortestPath._head);
                }
            }

            MakeThePaths();
            
            return _myPath;
        }

        void MakeThePaths()
        {
            foreach(KeyValuePair<int?, Vertex> entry in _graph._vertices)
            {
                List<Vertex> truePath = new List<Vertex>();
                truePath.Clear();

                Vertex v = entry.Value;
                truePath.Add(v);
                while(v._link != null)
                {
                    v = v._link;
                    truePath.Add(v);
                }
                truePath.Reverse();
                _myPath.Add(entry.Key, truePath);
            }
        }

        public void PrintAnswer()
        {
            foreach(KeyValuePair<int?, List<Vertex>> entry in _myPath)
            {
                Console.Write(entry.Key + " " + _graph._vertices[entry.Key]._dijkstraLength + " " );
                if(entry.Value.Count <= 1)
                {
                    Console.Write("null");
                }
                else
                {
                    for(int i=1; i<entry.Value.Count; i++)
                    {
                        Console.Write(entry.Value[i]._number + ",");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("The final Answer is:");
            Console.WriteLine(_graph._vertices[7]._number + " " + _graph._vertices[7]._dijkstraLength);
            Console.WriteLine(_graph._vertices[37]._number + " " + _graph._vertices[37]._dijkstraLength);
            Console.WriteLine(_graph._vertices[59]._number + " " + _graph._vertices[59]._dijkstraLength);
            Console.WriteLine(_graph._vertices[82]._number + " " + _graph._vertices[82]._dijkstraLength);
            Console.WriteLine(_graph._vertices[99]._number + " " + _graph._vertices[99]._dijkstraLength);
            Console.WriteLine(_graph._vertices[115]._number + " " + _graph._vertices[115]._dijkstraLength);
            Console.WriteLine(_graph._vertices[133]._number + " " + _graph._vertices[133]._dijkstraLength);
            Console.WriteLine(_graph._vertices[165]._number + " " + _graph._vertices[165]._dijkstraLength);
            Console.WriteLine(_graph._vertices[188]._number + " " + _graph._vertices[188]._dijkstraLength);
            Console.WriteLine(_graph._vertices[197]._number + " " + _graph._vertices[197]._dijkstraLength);
        }

    }

    class Graph
    {
        public Dictionary<string, Edge> _edges = new Dictionary<string, Edge>();
        public Dictionary<int?, Vertex> _vertices = new Dictionary<int?, Vertex>();
    
        public Graph(string dir)
        {
            string[] lines = File.ReadAllLines(dir);
            List<(int?, int?, int)> edgeNumbers = new List<(int?, int?, int)>();

            foreach(string li in lines)
            {
                Vertex tailVertex; 
                string[] entry = li.Split();
                int? tailVertexNumber = Convert.ToInt32(entry[0]);
                
                tailVertex = new Vertex(tailVertexNumber);
                if(!_vertices.ContainsKey(tailVertex._number))
                {
                    _vertices.Add(tailVertex._number, tailVertex);
                }

                for(int i=1; i < entry.Length; i++)
                {
                    string[] vertexElements = entry[i].Split(",");
                    if(!string.IsNullOrEmpty(vertexElements[0]))
                    {  
                        Vertex headVertex = new Vertex(Convert.ToInt32(vertexElements[0]));
                        int edgeLength = Convert.ToInt32(vertexElements[1]);
                        if(!_vertices.ContainsKey(headVertex._number))  
                        {
                            _vertices.Add(headVertex._number, headVertex);
                        }
                        edgeNumbers.Add((tailVertexNumber, headVertex._number, edgeLength));   
                    }
                }
            }

            foreach((int?, int?, int) edNum in edgeNumbers)
            {
                Vertex tail = _vertices[edNum.Item1];
                Vertex head = _vertices[edNum.Item2];
                int edgeLength = edNum.Item3;

                Edge edge = new Edge(tail, head, edgeLength);
                
                string key = tail._number.ToString() + "T" + head._number.ToString() + "H";
                
                if(!_edges.ContainsKey(key))
                {
                    _edges.Add(key, edge);
                }
                else
                {
                    Console.WriteLine(key + "why did this repeat");
                }

            }
        }
    }

    class Vertex
    {
        public int? _number;
        public int _dijkstraLength = 1000000;
        public Vertex _link = null;
        
        public Vertex(int? number)
        {
            _number = number;
        }
    }

    class Edge
    {
        public Vertex _tail, _head;
        public int _length;
        public Edge(Vertex tail, Vertex head, int length)
        {
            _tail = tail;
            _head = head;
            _length = length;
        }
    }
}
