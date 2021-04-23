using System;
using System.Collections.Generic;
using System.IO;

namespace Week_4_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MinCutContract _minCutContract = new MinCutContract();
            TestCases _testCases = new TestCases(_minCutContract);

            List<List<int>> _adjacencyList = new List<List<int>>();
            string[] lines = File.ReadAllLines("testCase1.txt");

            foreach(string entries in lines)
            {
                string[] lineArrayForm = entries.Split(null);
                int[] lineIntForm = Array.ConvertAll(lineArrayForm, s => int.TryParse(s, out var x) ? x : -999);

                List<int> listOfLineInts = new List<int>();
                
                listOfLineInts.AddRange(lineIntForm[0..(lineIntForm.Length-1)]);

                _adjacencyList.Add(listOfLineInts);
            }

            _minCutContract.DisplayGraphArray(_adjacencyList);
            

            Console.WriteLine("-----------------");
            _testCases.TestCase1();

            Console.ReadKey();
        }
    }

    public class MinCutContract
    {
        public List<List<int>> MinCut(List<List<int>> adjacencyList)
        {
            DisplayGraphArray(adjacencyList);
            
            int rr = 0;
            int rc = 2;

            List<List<int>> listOfListOfChosenVertices = new List<List<int>>();
            List<int> listOfChosenVertices = new List<int>();

            int tailOfChosenEdgeRR = adjacencyList[rr][0];
            int headOfChosenEdgeRC = adjacencyList[rr][rc];

            listOfChosenVertices.Add(tailOfChosenEdgeRR);
            listOfChosenVertices.Add(headOfChosenEdgeRC);

            listOfListOfChosenVertices.Add(listOfChosenVertices);

            return adjacencyList;
        }

        public void DisplayGraphArray(List<List<int>> graphList)
        {
            foreach(List<int> gp in graphList)
            {
                foreach(int v in gp)
                {
                    Console.Write(v + " ");
                }
                Console.WriteLine("");
            }
        }
    }

    public class TestCases
    {
        MinCutContract _minCutContract;
        public TestCases(MinCutContract minCutContract)
        {
            _minCutContract = minCutContract;
        }

        public void TestCase1()
        {
            List<List<int>> testCase1 = new List<List<int>>();    
            
            List<int> row0 = new List<int> {1,2,3};
            List<int> row1 = new List<int> {2,1,3,4};
            List<int> row2 = new List<int> {3,1,2,4};
            List<int> row3 = new List<int> {4,2,3};

            testCase1.Add(row0);
            testCase1.Add(row1);
            testCase1.Add(row2);
            testCase1.Add(row3);

            _minCutContract.DisplayGraphArray(testCase1);
        }
    }
}
