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
            TSPFunctions tSPFunctions = new TSPFunctions();
            // Act
            (double, double)[] cities = fileReader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15b Programming Assignment\TestCases\Test_Case_nn.txt");
            (double, double)[] answer = tSPFunctions.TrickHeuristicTSP(cities);   
/**
            foreach(int ii in tSPFunctions.indexRef)
            {
                Console.WriteLine(ii);   
            }
            Console.WriteLine("====");
            Console.WriteLine(tSPFunctions.indexRef.Length);
*/
            double trueAnswer = tSPFunctions.EucledianCompute(answer);    
            Console.WriteLine("GG:" + trueAnswer);
            Console.ReadKey();
        }
    }

    class TSPFunctions
    {
        public int[] indexRef;

        public (double, double)[] TrickHeuristicTSP((double, double)[] cityMap)
        {
            // Initialize 
            Dictionary<int, (double, double)> unvisitedCities = GetDictFromCityMap(cityMap);
            (double, double)[] visitedCities = new (double, double)[cityMap.Length];

            (double, double) start = cityMap[0];
            (double, double) head = start;
            (double, double) minCandidate = start;
            visitedCities[0] = head;
            unvisitedCities.Remove(0);

            double delta_x;   
            int minCandidateKey = 0;
            int visitIndex = 1;
            
            int n = cityMap.Length;
            indexRef = new int[n];
            indexRef[0] = 0;

            for(int i = 1; i < n; i++)
            {
                double currentMinDistance = 9999999999999999999;
                      //              Console.WriteLine("head: " + head);
                foreach(KeyValuePair<int, (double, double)> entry in unvisitedCities)
                {
                    // delta_x is the x distance between the unvisited x-coord and the current head x-coord
                    delta_x = Math.Abs(head.Item1 - entry.Value.Item1);
                    if(delta_x <= currentMinDistance)
                    {
                        double minDistanceCandiate = SquaredEucledianSimple(head, entry.Value);
                        if(minDistanceCandiate < currentMinDistance)
                        {
                            minCandidate = entry.Value;
                            minCandidateKey = entry.Key;
                            currentMinDistance = minDistanceCandiate;
                        }                        
                 //       Console.WriteLine("entry: " + entry + " distance: " + currentMinDistance + " Delta x: " + delta_x);
                    }

                    else
                    {
                    //    Console.WriteLine("entry: " + entry + " distance: " + currentMinDistance + " Delta x: " + delta_x);
                        break;
                    }
                }
            
                // Winner gets stored in the winner array
                visitedCities[visitIndex] = minCandidate;
                indexRef[visitIndex] = minCandidateKey;
                head = minCandidate;
                visitIndex += 1;

                // Update tables   
                unvisitedCities.Remove(minCandidateKey);
            }
            return visitedCities;
        }

        public Dictionary<int, (double, double)> GetDictFromCityMap((double, double)[] cityMap)
        {
            Dictionary<int, (double, double)> dictCityMap = new Dictionary<int, (double, double)>();
            for(int i = 0; i < cityMap.Length; i++)
            {
                dictCityMap.Add(i, cityMap[i]);
            }

            return dictCityMap;
        }

        double SquaredEucledianSimple((double, double) a, (double, double) b)
        {
                double xSqrDistance = Math.Pow((b.Item1 - a.Item1), 2);
                double ySqrDistance = Math.Pow((b.Item2 - a.Item2), 2);

                return Math.Sqrt(xSqrDistance + ySqrDistance);
        }

        public double EucledianCompute((double, double)[] tspPath)
        {        
            double TSPDistance = 0;
            double neighborDistance;
            double xSqrDistance;
            double ySqrDistance;
            for(int i = 1; i < tspPath.Length; i++)
            {
                xSqrDistance = Math.Pow((tspPath[i].Item1 - tspPath[i-1].Item1), 2);
                ySqrDistance = Math.Pow((tspPath[i].Item2 - tspPath[i-1].Item2), 2);
                
                neighborDistance = Math.Sqrt(xSqrDistance + ySqrDistance);    
                TSPDistance += neighborDistance; 
     
            }

            xSqrDistance = Math.Pow((tspPath[tspPath.Length - 1 ].Item1 - tspPath[0].Item1), 2);
            ySqrDistance = Math.Pow((tspPath[tspPath.Length - 1].Item2 - tspPath[0].Item2), 2);
                
            neighborDistance = Math.Sqrt(xSqrDistance + ySqrDistance);    
            TSPDistance += neighborDistance; 
      
            
            return TSPDistance;
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
                cityMap[i - 1] = (Convert.ToDouble(line_part[1]), Convert.ToDouble(line_part[2]));
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

    
    class SimpleCityVertex
    {
        public (double, double) _address;
        

        public SimpleCityVertex((double, double) address)
        {
            _address = address;
        }
    }

    class CityVertex
    {
        public Dictionary<int, double>  _neighbor {get; private set;} = new Dictionary<int, double>();
        public KeyValuePair<int, double>[] _sortedNeighbors;

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

            _sortedNeighbors = _neighbor.OrderBy(d => d.Value).ToArray();
        }
    }

}
