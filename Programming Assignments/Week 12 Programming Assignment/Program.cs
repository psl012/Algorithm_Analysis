using System;
using System.Collections.Generic;
using System.IO;

namespace Week_12_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            FileReader fileReader = new FileReader();
            (int, Item[]) extractedValues = fileReader.ExtractItems(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 12 Programming Assignment\TestCase\knapsack1.txt");
            List<int> indices;

            // Act
            KnapSack knapSack = new KnapSack(extractedValues.Item2, extractedValues.Item1);
            indices = knapSack.Reconstruction();
            
            int ans = knapSack.GetOptimalSackValue();

            Console.WriteLine("Sack Value: " + ans);

            Console.ReadKey();
        }
    }

    class KnapSack
    {
        public int[,] _A {get; private set;}
        int _n, _C;
        Item[] _listOfItems;

        public KnapSack(Item[] listOfItems, int C)
        {
            _listOfItems = listOfItems;
            _n = listOfItems.Length;
            _C = C;
            
            _A = new int[_n + 1, _C + 1];
            // Base Case i = 0;
            for(int c = 0; c <= _C; c++)
            {
                _A[0, c] = 0;
            }

            // Solve all subproblems
            for(int i = 1; i <= _n; i++)
            {
                for(int c = 0; c <= _C; c++)
                {
                    if(listOfItems[i-1]._weight > c)
                    {
                        _A[i,c] = _A[i-1, c];
                    }
                    else
                    {
                        _A[i,c] = Math.Max(_A[i-1,c], _A[i-1,c-listOfItems[i-1]._weight] + listOfItems[i-1]._value);
                    }

                }
            }
        }

        public int GetOptimalSackValue()
        {
            return _A[_n, _C];
        }

        public List<int> Reconstruction()
        {
            List<int> S = new List<int>();
            int c = _C;

            for(int i = _n; i > 0; i--)
            {
                int si = _listOfItems[i-1]._weight;
                int vi = _listOfItems[i-1]._value;
    
                if (si <= c && _A[i-1, c-si] + vi >= _A[i-1, c])
                {
                    S.Add(i-1);
                    c = c - si;
                }
            }

            return S;
        }
    }

    class FileReader
    {
        public (int, Item[]) ExtractItems(string dir)
        {
            string[] lines = File.ReadAllLines(dir);
            Item[] arrayOfItems = new Item[lines.Length - 1];
            
            string[] temp = lines[0].Split();
            int knapSackSize = Convert.ToInt32(temp[0]);

            for(int i = 1; i < lines.Length; i++)
            {
                temp = lines[i].Split(" ");
                int value = Convert.ToInt32(temp[0]);
                int weight = Convert.ToInt32(temp[1]);

                arrayOfItems[i-1] = new Item(value, weight);
            }

            return (knapSackSize, arrayOfItems);
        }
    }

    class Item
    {
        public int _value {get; private set;}
        public int _weight {get; private set;}

        public Item(int value, int weight)
        {
            _value = value;
            _weight = weight;
        }
    }

}
