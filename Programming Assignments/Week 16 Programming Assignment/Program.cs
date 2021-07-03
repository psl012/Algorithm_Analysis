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
            SAT_2Solver sat = new SAT_2Solver();
            

            // truthTable  = fileReader.Scan(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 16 Programming Assignment\Test Cases\Real_Case_01.txt");
            (TruthValue, TruthValue)[] truthTable = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_01.txt");

            sat.PapaDimitrious(truthTable);

            foreach((TruthValue, TruthValue) tv in truthTable)
            {
                Console.WriteLine(tv.Item1._ID + ":" +  tv.Item1._value + ", " + tv.Item2._ID + ":" + tv.Item2._value);
            }

            Console.WriteLine("END-OF-SEE-END-OF-SEE-------------------");
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }

    class FileReader
    {
        public (TruthValue, TruthValue)[] Scan(string dir)
        {
            string[] lines = File.ReadAllLines(dir);
            (TruthValue, TruthValue)[]  truthTable = new (TruthValue, TruthValue)[lines.Length - 1];
            Dictionary<int, TruthValue> truthDictionary = new Dictionary<int, TruthValue>();

            for(int i = 1; i < lines.Length; i++)
            {
                string[] literalString = lines[i].Split(" ");
                int x = Convert.ToInt32(literalString[0]);
                int y = Convert.ToInt32(literalString[1]);

                TruthValue item1 = AssignItem(x);
                TruthValue item2 = AssignItem(y);

                truthTable[i-1] = (item1, item2);
            }

            // Internal functions
            TruthValue AssignItem(int numID)
            {
                if(truthDictionary.ContainsKey(numID))
                {
                    return truthDictionary[numID];
                }
                else
                {
                    TruthValue item = new TruthValue(numID, false);
                    truthDictionary.Add(numID, item);
                    return item;
                }
            }

            return truthTable;
        }
    }

    class SAT_2Solver
    {
        public Dictionary<int, TruthValue> _positiveID_Dict {get; private set;}
        public Dictionary<int, TruthValue> _negativeID_Dict  {get; private set;}

        public void PapaDimitrious((TruthValue, TruthValue)[] truthTable)
        {
            // Make a Dictionary of only positive literals 
            _positiveID_Dict = new Dictionary<int, TruthValue>();

            // Make a Dictionary of only negative literals
            _negativeID_Dict = new Dictionary<int, TruthValue>();

            // Check negaFunction
            void CheckNumberSignAndStore(TruthValue truthValue)
            {
                int num = truthValue._ID;
                if(num < 0 && !_negativeID_Dict.ContainsKey(num)) _negativeID_Dict.Add(num, truthValue);  
                else if(num >= 0 && !_positiveID_Dict.ContainsKey(num)) _positiveID_Dict.Add(num, truthValue);  
            }

            // Random number makeer of TruthValue Initializer (Put this inside the function at final)
            Random randomNumber = new Random(2);


            foreach((TruthValue, TruthValue) truthValue in truthTable)
            {
                CheckNumberSignAndStore(truthValue.Item1);
                CheckNumberSignAndStore(truthValue.Item2);
            }

            // Iterate through all the positive literals
            foreach(KeyValuePair<int, TruthValue> tValue in _positiveID_Dict)
            {
                // Reference for negative check
                int truthValueID = tValue.Key;

                // Truth Value of RandomTruth
                bool myRandomTruth = GiveRandomTruth();

                // randomly assign a bool value to the masterlist of Literals // for now make every literal initialized with true value
                tValue.Value._value = myRandomTruth;
                
                // check if it has a negative literal
                if(_negativeID_Dict.ContainsKey(-truthValueID))
                {
                    // - assign the opposite bool value from the positive literal in the masterlist of literals 
                    _negativeID_Dict[-truthValueID]._value = !myRandomTruth;
                }

            }

            // internal functions
            bool GiveRandomTruth()
            {
                int rNumber = randomNumber.Next(0,2);
               // Console.WriteLine("Truth Value Number: " + rNumber);
                if(rNumber == 0) {return false;}
                else{return true;}
            }
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
