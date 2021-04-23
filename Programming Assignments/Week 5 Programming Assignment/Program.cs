﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Week_5_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCase testCase = new TestCase();
            testCase.TestCase1(); Console.WriteLine();
            testCase.TestCase2(); Console.WriteLine();
            testCase.TestCase3(); Console.WriteLine();
            testCase.TestCase4(); Console.WriteLine();
            testCase.TestCase5();
           // testCase.TestCase3Reverse();
            Console.ReadKey();
        }

    }

    class TestCase
    {
        SCC _scc = new SCC();
        List<int> _tailList = new List<int>();

        public void MiniTestCase()
        {
            Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 5 Programming Assignment\Mini Test Case.txt");

            _scc.Kosaraju(graph);
            graph.DisplayGraphTable();            
        }

        public void TestCase1()
        {
            Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 5 Programming Assignment\Test Case 1.txt");

            _scc.Kosaraju(graph);
        //    graph.DisplayGraphTable();
        }

        public void TestCase2()
        {
            Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 5 Programming Assignment\Test Case 2.txt");
            _scc.Kosaraju(graph);
        //    graph.DisplayGraphTable();
        }

        public void TestCase3()
        {
            Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 5 Programming Assignment\Test Case 3.txt");

            _scc.Kosaraju(graph);
         //   graph.DisplayGraphTable();    
        }

        public void TestCase4()
        {
            Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 5 Programming Assignment\Test Case 4.txt");

            _scc.Kosaraju(graph);           
        }

        public void TestCase5()
        {
            Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 5 Programming Assignment\Test Case 5.txt");

            _scc.Kosaraju(graph);                 
        }

        public void TestCase3Reverse()
        {
            Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 5 Programming Assignment\Test Case 3 Reverse.txt");
            _scc.Kosaraju(graph);

        //    graph.DisplayGraphTable();    
        }

    }

    class SCC
    {
        QuickSort _quickSort = new QuickSort();

        int _limitCounter = 1000;
        int _curLabel = -1;
        int _sccCounter;
        List<Vertex> _tailList = new List<Vertex>();
        List<int?> _arrangedKeys = new List<int?>();
      
        List<int> _sccCountList = new List<int>();
   
        public void Kosaraju(Graph graph)
        {
            int numScc;

            _arrangedKeys.Clear();
            _sccCountList.Clear();

            Graph reverseGraph = new Graph(graph._dir, true);
            TopoSort(reverseGraph);

            foreach(KeyValuePair<int?, Path> entry in reverseGraph.Value())
            {
                int? rank = entry.Value._origin._topoRank;
                int? keyInt = entry.Key;

                graph.Value()[keyInt]._origin._topoRank = rank;
            }
     
            numScc = 0;
            _curLabel = _arrangedKeys.Count;
            for(int i=_arrangedKeys.Count-1; i >= 0; i--)
            {
                if(!graph.Value()[_arrangedKeys[i]]._origin._isExplored)
                {
                    numScc += 1;
                    _sccCounter = 0;
                    RecursionDFS(graph, graph.Value()[_arrangedKeys[i]]._origin, false ,numScc);
                    _sccCountList.Add(_sccCounter);
                }
            }

            int[] answerArray = _sccCountList.ToArray();
            _quickSort.Sort(answerArray, 0, answerArray.Length - 1);
            
            Console.Write("Top 5 SCC Population: ");
            for(int s=answerArray.Length - 1; s > answerArray.Length - 6; s--)
            {
                if(s >= 0)
                {
                    Console.Write(answerArray[s] + ",");
                }
                else 
                {
                    Console.Write(0 + ",");
                }
            }
            Console.WriteLine();

        }


        public void TopoSort(Graph graph)
        {
            List<Vertex> listOfVertex = new List<Vertex>();
            foreach(Path pt in graph.Value().Values)
            {
                listOfVertex.Add(pt._origin);
            }
            _curLabel = listOfVertex.Count;
            foreach(Vertex v in listOfVertex)
            {
                if(!v._isExplored) 
                {
                    RecursionDFS(graph, v, true, -1);
                }
            }   
        }

        public void DFS(Graph graph, int relPos)
        {
            foreach(KeyValuePair<int?, Path> entry in graph.Value())
            {
                if(!entry.Value._origin._isExplored)
                {
                    _tailList.Add(entry.Value._origin);
                }
            }
            int idx = relPos % _tailList.Count;
            RecursionDFS(graph, _tailList[idx]);   
        }

        void RecursionDFS(Graph graph, Vertex s, bool isTopoSort = false, int numScc = -1)
        {
            _limitCounter -= 1;
      
            if(_limitCounter > 0)
            {
                Vertex origin = GetOriginVertex(graph, s);
                List<Vertex> paths = graph.Value()[s._value]._heads;
                
                origin._isExplored = true;
                origin._sccGroup = numScc;
                _sccCounter++;
                if(paths[0] != null)
                {
                    foreach(Vertex w in paths)
                    {
                        if(!GetOriginVertex(graph, w)._isExplored)
                        {
                            RecursionDFS(graph, w, isTopoSort, numScc);
                        }
                    }
                }
                if(isTopoSort)
                {
                    origin._topoRank = _curLabel;
                    _arrangedKeys.Add(origin._value);
                    _curLabel -= 1;
                }
            
            }
            
            else
            {
                return;
            }
        } 

        Vertex GetOriginVertex(Graph graph, Vertex vertex)
        {
            return graph.Value()[vertex._value]._origin;
        }

    }

    class Graph
    {   
        public Dictionary<int?, Path> _graph = new Dictionary<int?, Path>();   
        public string _dir;

        public Graph(string dir, bool isReverse = false)
        {
            _dir = dir;

            string[] lines = File.ReadAllLines(dir);
            foreach(string line in lines)
            {
                string[] verEdge = line.Split();

                int tail = Convert.ToInt32(verEdge[0]);
                int head = Convert.ToInt32(verEdge[1]);
                if(!isReverse)
                {
                    MakeGraph(tail, head);
                }
                else
                {
                    MakeGraph(head, tail);
                }
            }
            
        }

        void MakeGraph(int tail, int head)
        {
            Vertex vTail = new Vertex(tail, 9999);
            Vertex vHead = new Vertex(head, 9999);

            if(_graph.ContainsKey(vTail._value))
            {
                if(_graph[vTail._value]._heads[0] == null)
                {
                    _graph[vTail._value]._heads.RemoveAt(0);
                }
                    _graph[vTail._value].AddHead(vHead);
                
                if(!_graph.ContainsKey(vHead._value)) 
                {
                    _graph.Add(vHead._value, new Path(vHead, null));
                }
            }
            else
            {
                _graph.Add(vTail._value, new Path(vTail, vHead));

                if(!_graph.ContainsKey(vHead._value)) 
                {
                    _graph.Add(vHead._value, new Path(vHead, null));
                }
            }
        }

        public void DisplayGraphTable()
        {
            foreach(KeyValuePair<int?, Path> entry in _graph)
            {
                Vertex vertex = entry.Value._origin;
                Path paths = entry.Value;

                Console.Write(vertex._isExplored + " " + "Rank: |" + vertex._topoRank + "| " + vertex._value + " -> ");
                
                if(paths._heads[0] != null)
                {
                    foreach(Vertex dest in paths._heads)
                    {
                        {
                            Console.Write(dest._value + " ");
                        }
                    }
                }
                Console.Write("SCC: " + vertex._sccGroup);
                Console.WriteLine();
            }
        }

        public Dictionary<int?, Path> Value()
        {
            return _graph;
        }
    }

    class Path
    {
        public Vertex _origin;
        public List<Vertex> _heads;

        public Path(Vertex tail, Vertex head)
        {
            _origin = tail;
            _heads = new List<Vertex>();
            _heads.Add(head);
        }

        public void AddHead(Vertex head)
        {
            _heads.Add(head);
        }
    }

    class Vertex
    {
        public int? _value;
        public int? _topoRank;
        public int? _sccGroup;
        public bool _isExplored;

        public Vertex(int? point, int topoRank)
        {
            _isExplored = false;
            _value = point;
            _topoRank = topoRank;
        }
    }


    class QuickSort
    {
        static int Partition(int[] array, int low,
                                        int high)
        {
            //1. Select a pivot point.
            int pivot = array[high];

            int lowIndex = (low - 1);

            //2. Reorder the collection.
            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    lowIndex++;

                    int temp = array[lowIndex];
                    array[lowIndex] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;

            return lowIndex + 1;
        }

        public void Sort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);

                //3. Recursively continue sorting the array
                Sort(array, low, partitionIndex - 1);
                Sort(array, partitionIndex + 1, high);
            }
        }


    }

}
