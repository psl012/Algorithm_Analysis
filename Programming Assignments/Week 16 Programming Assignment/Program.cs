using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Week_16_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReader fileReader = new FileReader();
            (TruthValue, TruthValue)[] test = fileReader.Scan(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 16 Programming Assignment\Test Cases\Real_Case_01.txt");

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }

    class FileReader
    {
        public (TruthValue, TruthValue)[] Scan(string dir)
        {
            string[] lines = File.ReadAllLines(dir);
            (TruthValue, TruthValue)[] numTable = new (TruthValue, TruthValue)[lines.Length - 1];

            for(int i = 1; i < lines.Length; i++)
            {
                string[] literalString = lines[i].Split(" ");
                int x = Convert.ToInt32(literalString[0]);
                int y = Convert.ToInt32(literalString[1]);

                numTable[i-1] = (new TruthValue(x, false), new TruthValue(y, false));
            }

            return numTable;
        }
    }

    class SAT_2Solver
    {
        public Dictionary<(int, int), bool> _literalList {get; private set;}
        public Dictionary<int, int> _positiveID_Dict {get; private set;}
        public Dictionary<int, int> _negativeID_Dict  {get; private set;}

        public void PapaDimitrious((TruthValue, TruthValue)[] truthTable)
        {
            // Make a masterlist of Literals
            _literalList = new Dictionary<(int, int), bool>();

            // Make a Dictionary of only positive literals 
            _positiveID_Dict = new Dictionary<int, int>();

            // Make a Dictionary of only negative literals
            _negativeID_Dict = new Dictionary<int, int>();

            // Check negaFunction
            void CheckNumberSignAndStore(int num)
            {
                if(num < 0 && !_negativeID_Dict.ContainsKey(num)) _negativeID_Dict.Add(num, num);  
                else if(num >= 0 && !_positiveID_Dict.ContainsKey(num)) _positiveID_Dict.Add(num, num);  
            }

            foreach((TruthValue, TruthValue) truthValue in truthTable)
            {
                CheckNumberSignAndStore(truthValue.Item1._ID);
                CheckNumberSignAndStore(truthValue.Item2._ID);
                
                _literalList.Add((truthValue.Item1._ID, truthValue.Item2._ID), false);
            }

            // Iterate through all the positive literals
            
                // randomly assign a bool value to the masterlist of Literals // for now make every literal initialized with true value
                // check if it has a negative literal
                    // - assign the opposite bool value from the positive literal in the masterlist of literals
        }
    }

    // Make a literal Object
    class TruthValue
    {
        public int _ID;
        public bool _value;

        public TruthValue(int ID, bool value)
        {
            _ID = ID;
            _value = value;
        }        

    }
    
}
