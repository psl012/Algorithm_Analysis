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
            (int, int)[] test = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Real_Case_01.txt");

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }

    class FileReader
    {
        public (int, int)[] Scan(string dir)
        {
            string[] lines = File.ReadAllLines(dir);
            (int, int)[] numTable = new (int, int)[lines.Length - 1];

            for(int i = 1; i < lines.Length; i++)
            {
                string[] literalString = lines[i].Split(" ");
                int x = Convert.ToInt32(literalString[0]);
                int y = Convert.ToInt32(literalString[1]);

                numTable[i-1] = (x,y);
            }

            return numTable;
        }
    }

    class SAT_2Solver
    {
        public void PapaDimitrious((int, int)[] numTable)
        {
            // Make a masterlist of Literals
            Dictionary<(int, int), bool> LiteralList = new Dictionary<(int, int), bool>();

            // Make a Dictionary of only positive literals 
            Dictionary<int, TruthValue> postiveDict = new Dictionary<int, TruthValue>();

            // Make a Dictionary of only negative literals
            Dictionary<int, TruthValue> negativeDict = new Dictionary<int, TruthValue>();

            // Check negaFunction
            void CheckNumberSignAndStore(int num)
            {
                if(num < 0 && !negativeDict.ContainsKey(num)) negativeDict.Add(num, num);  
                else if(num >= 0 && !postiveDict.ContainsKey(num)) postiveDict.Add(num, num);  
            }

            foreach((int, int) num in numTable)
            {
                CheckNumberSignAndStore(num.Item1);
                CheckNumberSignAndStore(num.Item2);
            }

            // Iterate through all the positive literals
            foreach(KeyValuePair<int, int> num)
                // randomly assign a bool value to the masterlist of Literals // for now make every literal initialized with true value
                // check if it has a negative literal
                    // - assign the opposite bool value from the positive literal in the masterlist of literals

                        
            
            Dictionary<int, bool> literalTable = new Dictionary<int, bool>();

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
