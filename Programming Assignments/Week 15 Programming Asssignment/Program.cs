using System;
using System.Collections.Generic;
using System.Linq;

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
        public float BellmanHeldKarp(Graph graph)
        {
            SetFunctions setFunctions = new SetFunctions();
            int[] index = CreateIndexSet(graph._vertices.Length);

            // Get the power sets
            List<int?>[] powerSetInd = setFunctions.RemoveNonBase(setFunctions.OrderedPowerSet(index), 0);

            // Initialize A and n's
            int n = index.Length;
            int bar = 0;

            // Initialize the A to the size of the column (length of the powerset) and size of the row (length of the destination)
            float[,] A = new float[powerSetInd.Length, n - 1];

            // base Case |S| = 2
            for(int i = 0; i < powerSetInd.Length; i++)
            {
                // Initialization over we now reach Subsets with 3 or more elements
                if(powerSetInd[i].Count > 2)
                {
                    bar = i;
                    break;
                }

                else if(powerSetInd[i].Count == 2)
                {
                    // Set size only is 2: which is the source (0) and the destination (1) so our j is the destination is the second element in the powerset
                    int j = (int) powerSetInd[i][1];

                    // i is so that we are in the row with proper size 2 power set, j as i said is the destination
                    // cij means the c1j in the pseudocode: graph._vertices[0] is the default while 0 ia the source and j is the dest
                    A[i, j] = graph._vertices[0]._neighbor[j];
                } 
            }

            // Subproblems
            // bar is our checkpoint marker in the initial stage 
            // note in the pseudocode its the subproblem size but we made the powerset in a way that iterating through it will pass through all the subproblems from smallest to largest
            for(int i = bar; i < powerSetInd.Length; i++)
            {
                // the iteration of the powerset (the second for loop of the pesudocode is actually combined into one)
                List<int?> S = powerSetInd[i];

                // Iterate the destinations (j) and we must not include 0 thats why we start at 1        
                for(int e = 1; e < S.Count; e++)
                {
                    // j stores the iteration of the destination 
                    int j = (int) S[e];

                    //A[i, j] = 0; // The min Corollary 21.2 
                    // Set up the get minimum in the iteration
                    
                    // The index we want from S
                    int k_index = 0;
                    // The winning distance
                    float minDistance = 9999999;

                    // the winning BellmanRecurrence Index
                    int br_winner = 0;
                    
                    // iterate through the current power set but do not include 0 and j for k
                    for(int kk = 0; kk < S.Count; kk++)
                    {
                        // the current value of k in the iteration
                        int? k = S[kk];
                        if(k != 0 && k != j)
                        {
                            // Get the location of the S-{j} in the power set
                            int ind = BellmanRecurrence(powerSetInd, S, j);

                            // i is the proper location of row in the corresponds to the right power set, j is the destination
                            // ind  the proper location of row in the corresponds to the right S-{j}, k is the one in the psudocode 
                            // and graph_vert... k j is the length from k to j
                            //A[i,j] = A[ind, (int) k] + graph._vertices[(int) k]._neighbor[j];

                            // this is not finished we need to change this to get the minimum value for A[i,j] among the values of k in the iteration
                            float minCandidate = A[ind, (int) k] + graph._vertices[(int) k]._neighbor[j];
                            if(minCandidate < minDistance)
                            {
                                minDistance = minCandidate;
                                k_index = (int) k;
                                br_winner = ind;
                            }
                        }
                    }
                
                A[i,j]   = A[br_winner, k_index] + graph._vertices[k_index]._neighbor[j];
                }

            }   
             // From destination 1 to n-1 we need to see which has the minimum using the largest subset V
            int V_index = powerSetInd.Length - 1;
            float superMinDistance = 9999999;
            float superMinCandidate;
            for(int fj = 1; fj < n; fj++)
            {
                superMinCandidate = A[V_index, fj] + graph._vertices[fj]._neighbor[0];

                if(superMinCandidate < superMinDistance)
                {
                    superMinDistance = superMinCandidate;
                }
            } 
            return superMinDistance;
        }

        
        int BellmanRecurrence(List<int?>[] powerSetInd, List<int?> S, int? j)
        {
            // Get the list the does not include j
            List<int?> S_j = new List<int?>();
            foreach(int? ss in S)
            {
                if(ss != j)
                {
                    S_j.Add(ss);
                }
            }

            int index = 0;

            // iterate through the power set and get its index in the list that equals our S-{j}
            for(int i = 0; i < powerSetInd.Length; i++)
            {
                if(Enumerable.SequenceEqual(S_j, powerSetInd[i]))
                {
                    index = i;
                    break;
                }
            }

            return index;

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
