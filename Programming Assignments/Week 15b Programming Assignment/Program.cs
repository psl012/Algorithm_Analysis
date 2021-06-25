using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace Week_15b_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            FileReader fileReader = new FileReader();
        
            // Act
            (double, double)[] cities = fileReader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15b Programming Assignment\TestCases\Test_Case_02.txt");
            Graph graph = new Graph(cities);
            Dictionary<string, double> test = new Dictionary<string, double>();
            test.Add("zero", 100);
            test.Add("uno", 99);
            test.Add("dos", 98);

            var mySortedDictionary = test.OrderBy(d => d.Value);

            

            Console.ReadKey();
        }
    }

    class TSPFunctions
    {
        public double HeuristicTSP(CityVertex sourceCity)
        {
            // Initialization
            // list of visited cities (Dictionary for O(1) time using contains) dictionary is faster as internet stated

            // Start at the first city

            // Scan the neighbor get the one with the min value  and go that city
                // - needs an sorted dictionary with the distance as parameter we can bypass this using sorted List i already got this in my head so just rethink it
                    // keep iterating if the object in the list is already in the travelled dictionary
                    // if the iteration reached the end then all vertices is travelled go back to the source



            return 0;
        }
    }

    class FileReader
    {
        public (double, double)[] ReadFile(string dir)
        {
            string[] lines = File.ReadAllLines(dir);
            (double, double)[] cityMap = new (double, double)[lines.Length - 1];

            for(int i = 1; i < lines.Length; i++)
            {
                string[] line_part = lines[i].Split(" ");
                cityMap[i - 1] = (Convert.ToDouble(line_part[0]), Convert.ToDouble(line_part[1]));
            }

            return cityMap;
        }
    }

    class Graph
    {
        public CityVertex[] _vertices;
        (double, double)[] _cityMap;
        
        public Graph((double, double)[] cityMap)
        {
            _vertices = new CityVertex[cityMap.Length];
            _cityMap = cityMap;

            for(int i = 0; i < cityMap.Length; i++)
            {
                _vertices[i] = new CityVertex(i, _cityMap);
            }
        }
    }

    class CityVertex
    {
        public Dictionary<int, double>  _neighbor {get; private set;} = new Dictionary<int, double>();
        public int _myCityCode;
        
        (double, double)[] _cityMap;

        public CityVertex(int cityCode, (double, double)[] cityMap)
        {
            _myCityCode = cityCode;
            _cityMap = cityMap;

            int n = cityMap.Length;
            (double, double) myCity = _cityMap[_myCityCode];

            for(int i = 0; i < n; i++)
            {
            
                (double, double) neightborCity = _cityMap[i];
                double xSqrDistance = Math.Pow((myCity.Item1 - neightborCity.Item1), 2);
                double ySqrDistance = Math.Pow((myCity.Item2 - neightborCity.Item2), 2);
                
                double neighborDistance = Math.Sqrt(xSqrDistance + ySqrDistance);

                _neighbor.Add(i, neighborDistance);            
            }
        }
    }

}
