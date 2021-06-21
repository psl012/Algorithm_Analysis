using System;
using System.Collections.Generic;

namespace Week_15_Programming_Asssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SetFunctions setFunctions = new SetFunctions();
            QuickSort quickSort = new QuickSort();
            (float, float)[] cityMap = {(0,0), (0,3), (3,3)};
            int[] cityIndex = {0,1,2,3};

            List<int?>[] powerSet = setFunctions.OrderedPowerSet(cityIndex);            
            
            List<int?>[] gg = setFunctions.RemoveNonBase(powerSet, 0);
            setFunctions.PrintPowerSet(gg);

            CityVertex vertex = new CityVertex(0, cityMap);

            Console.WriteLine("==============");
            Console.WriteLine(vertex._myCityCode);

            foreach(KeyValuePair<int, float> entry in vertex._neighbor)
            {
                Console.WriteLine(entry.Value);
            }

            Console.ReadKey();
        }
    }

    class TSPFunction
    {
        public void BellmanHeldKarp(Graph graph)
        {
            SetFunctions setFunctions = new SetFunctions();
            int[] index = CreateIndexSet(graph._vertices.Length);
            List<int?>[] powerSetInd = setFunctions.RemoveNonBase(setFunctions.OrderedPowerSet(index), 0);

            // Initialize A and n's
            int n = index.Length;
            int bar = 0;
            float[,] A = new float[powerSetInd.Length, n - 1];

            // base Case |S| = 2
            for(int i = 0; i < powerSetInd.Length; i++)
            {
                if(powerSetInd[i].Count > 2)
                {
                    bar = i;
                    break;
                }

                else if(powerSetInd[i].Count == 2)
                {
                    int j = (int) powerSetInd[i][1];
                    A[i, j] = graph._vertices[0]._neighbor[j];
                } 
            }

            for(int i = bar; i < powerSetInd.Length; i++)
            {
                List<int?> S = powerSetInd[i];
                for(int e = 1; e < S.Count; e++)
                {
                    int j = (int) S[e];
                    A[i, j] = 0; // The min Corollary 21.2
                }
            }

            
        }

        int[] CreateIndexSet(int length)
        {
            int[] indices = new int[length];
            for(int i = 0; i < length; i++)
            {
                indices[i] = i;
            }

            return indices;
        }
    }

    class Graph
    {
        public CityVertex[] _vertices;
        (float, float)[] _cityMap;
        
        public Graph((float, float)[] cityMap)
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
        public Dictionary<int, float>  _neighbor {get; private set;} = new Dictionary<int, float>();
        public int _myCityCode;
        
        (float, float)[] _cityMap;

        public CityVertex(int cityCode, (float, float)[] cityMap)
        {
            _myCityCode = cityCode;
            _cityMap = cityMap;

            int n = cityMap.Length;
            (float, float) myCity = _cityMap[_myCityCode];

            for(int i = 0; i < n; i++)
            {
            
                (float, float) neightborCity = _cityMap[i];
                float xSqrDistance = MathF.Pow((myCity.Item1 - neightborCity.Item1), 2);
                float ySqrDistance = MathF.Pow((myCity.Item2 - neightborCity.Item2), 2);
                
                float neighborDistance = MathF.Sqrt(xSqrDistance + ySqrDistance);

                _neighbor.Add(i, neighborDistance);            
            }
        }
    }

    class SetFunctions
    {
        public List<int?>[] OrderedPowerSet(int[] set)
        {
            QuickSort quickSort = new QuickSort();

            int set_size = set.Length;
            uint pow_set_size = (uint)Math.Pow(2, set_size);
            
            List<int?>[] powerSet = new List<int?>[pow_set_size];
            int pwrCounter = 0;
            
            List<int?> subSet = new List<int?>();

            for(int counter = 0; counter < pow_set_size; counter++)
            {
                for(int j = 0; j < set_size; j++)
                {
                    if((counter & (1 << j)) > 0)
                    {
                        subSet.Add(j);
                    }
                }
                powerSet[pwrCounter] = HardCopy(subSet);
                pwrCounter++;
                subSet.Clear();
            }

            List<int?>[] orderedPowerSet = powerSet[1..];
            quickSort.Sort(orderedPowerSet, 0 , orderedPowerSet.Length - 1);

            return orderedPowerSet;
        }

        List<int?> HardCopy(List<int?> set)
        {
            List<int?> setCopy = new List<int?>();
            foreach(int? num in set)
            {
                setCopy.Add(num);
            }

            return setCopy;
        }

        List<int?>[] ArrangePowerSet(List<int?>[] powerSet)
        {
            return null;
        }

        public void PrintPowerSet(List<int?>[] powerSet)
        {
            foreach(List<int?> set in powerSet)
            {
                foreach(int? element in set)
                {
                    Console.Write(element + ",");
                }
                Console.WriteLine();
            }
        }

        public List<int?>[] RemoveNonBase(List<int?>[] powerSet, int fbase)
        {
            List<List<int?>> holder = new List<List<int?>>();

            for(int i = 0; i < powerSet.Length; i++)
            {
                if(powerSet[i][0] == 0)
                {
                    holder.Add(powerSet[i]);
                }
            }

            List<int?>[] bellmanFordPowerSet = new List<int?>[holder.Count];
            
            for(int i=0; i < holder.Count; i++)
            {
                bellmanFordPowerSet[i] = holder[i];
            }

            return bellmanFordPowerSet;
        } 
    }

    class QuickSort
    {
        static int Partition(List<int?>[] array, int low,
                                        int high)
        {
            //1. Select a pivot point.
            int pivot = array[high].Count;

            int lowIndex = (low - 1);

            //2. Reorder the collection.
            for (int j = low; j < high; j++)
            {
                if (array[j].Count <= pivot)
                {
                    lowIndex++;

                    List<int?> temp = array[lowIndex];
                    array[lowIndex] = array[j];
                    array[j] = temp;
                }
            }

            List<int?> temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;

            return lowIndex + 1;
        }

        public void Sort(List<int?>[] array, int low, int high)
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
