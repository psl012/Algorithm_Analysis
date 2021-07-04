using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Week_16_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            FileReader fileReader = new FileReader();
            SAT_2Solver sat = new SAT_2Solver();
            
            (TruthValue, TruthValue)[] truthTable = fileReader.Scan("Test Cases/input_beaunus_39_100000_True.txt");
            (TruthValue, TruthValue)[] coreTruthTable = sat.GetCoreTruthTable(truthTable);

            bool finalAnswer = sat.PapaDimitrious(coreTruthTable);
            
            Console.WriteLine("END-OF-SEE-END-OF-SEE-------------------");
            Console.WriteLine(finalAnswer);
            Console.ReadKey();

            // Internal Functions
            void ShowTruthValues()
            {
                foreach((TruthValue, TruthValue) tv in truthTable)
                {
                    Console.WriteLine(tv.Item1._ID + ":" +  tv.Item1._value + ", " + tv.Item2._ID + ":" + tv.Item2._value);
                }
            }
        }
    }

    class FileReader
    {
        public (TruthValue, TruthValue)[] Scan(string dir)
        {
            string[] lines = File.ReadAllLines(dir);

            Dictionary<int, TruthValue> truthDictionary = new Dictionary<int, TruthValue>();
            (TruthValue, TruthValue)[]  truthTable = new (TruthValue, TruthValue)[lines.Length - 1];

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

        public (TruthValue, TruthValue)[] GetCoreTruthTable((TruthValue, TruthValue)[] truthTable)
        {
            Dictionary<int, (TruthValue, TruthValue)> coreTruthDictionary = new Dictionary<int, (TruthValue, TruthValue)>();
            for(int i = 0; i < truthTable.Length; i++)
            {
                coreTruthDictionary.Add(i, truthTable[i]);
            }

            // Make a Dictionary of only positive literals 
            _positiveID_Dict = new Dictionary<int, TruthValue>();

            // Make a Dictionary of only negative literals
            _negativeID_Dict = new Dictionary<int, TruthValue>();

            // Make a pure dictionary (a verb that is only either positive all through out the truthTable or negative all throughout the truthtable)
            Dictionary<int, TruthValue> pureID_Dict = new Dictionary<int, TruthValue>();


            for(int i = 0; i < 1000; i++)
            {
                UpdatePostive_Negative_Dicts();
                MakePureTruthDictionary();
                foreach(KeyValuePair<int, (TruthValue, TruthValue)> tEntry in coreTruthDictionary)
                {
                    (TruthValue, TruthValue) literal = tEntry.Value;
                    if(pureID_Dict.ContainsKey(literal.Item1._ID) || pureID_Dict.ContainsKey(literal.Item2._ID))
                    {
                        coreTruthDictionary.Remove(tEntry.Key);
                    }
                }

                if(pureID_Dict.Count == 0)
                {
                    Console.WriteLine("managed to get the core");
                    break;
                }
            }

            (TruthValue, TruthValue)[] coreTruthArray = new (TruthValue, TruthValue)[coreTruthDictionary.Count];

            int ii = 0;
            foreach(KeyValuePair<int, (TruthValue, TruthValue)> entry in coreTruthDictionary)
            {
                coreTruthArray[ii] = entry.Value;
                ii++;
            }

            return coreTruthArray;

       //     ShowCoreTable();
        // Internal Functions--------------------------------------------------------------------

            void UpdatePostive_Negative_Dicts()
            {
                _positiveID_Dict.Clear();
                _negativeID_Dict.Clear();

                foreach(KeyValuePair<int, (TruthValue, TruthValue)> truthValue in coreTruthDictionary)
                {
                    CheckNumberSignAndStore(truthValue.Value.Item1);
                    CheckNumberSignAndStore(truthValue.Value.Item2);
                }
            }

            void CheckNumberSignAndStore(TruthValue truthValue)
            {
                int num = truthValue._ID;
                if(num < 0 && !_negativeID_Dict.ContainsKey(num)) _negativeID_Dict.Add(num, truthValue);  
                else if(num >= 0 && !_positiveID_Dict.ContainsKey(num)) _positiveID_Dict.Add(num, truthValue);  
            }


            void MakePureTruthDictionary()
            {
                pureID_Dict.Clear();
                // Finds pure positive verbs
                foreach(KeyValuePair<int, TruthValue> entry in _positiveID_Dict)
                {
                    if(!_negativeID_Dict.ContainsKey(-entry.Key))
                    {
                        pureID_Dict.Add(entry.Value._ID, entry.Value);
                    }
                }

                // Finds pure negative verbs
                foreach(KeyValuePair<int, TruthValue> entry in _negativeID_Dict)
                {
                    if(!_positiveID_Dict.ContainsKey(-entry.Key))
                    {
                        pureID_Dict.Add(entry.Value._ID, entry.Value);
                    }
                }
            }

            void ShowCoreTable()
            {
                foreach(KeyValuePair<int, (TruthValue, TruthValue)> entry in coreTruthDictionary)
                {
                    Console.WriteLine(entry.Value.Item1._ID + "," + entry.Value.Item2._ID);
                }
            }

        }

        public bool PapaDimitrious((TruthValue, TruthValue)[] truthTable)
        {

        // Initialization-----------------------------------------------------------------------------
            // Make a dictionary List containg all the verbs (unique values)        
            Dictionary<int, TruthValue> _truthDictList = new Dictionary<int, TruthValue>();

            // Make a Dictionary of only positive literals 
            _positiveID_Dict = new Dictionary<int, TruthValue>();

            // Make a Dictionary of only negative literals
            _negativeID_Dict = new Dictionary<int, TruthValue>();

            // Random number makeer of TruthValue Initializer
            Random randomNumber = new Random(2);

            foreach((TruthValue, TruthValue) entry in truthTable)
            {
                (TruthValue, TruthValue) literal = entry;
                AddToTheDictList(literal.Item1);
                AddToTheDictList(literal.Item2);
            }

            int tableLength = truthTable.Length;
            int n = _truthDictList.Count;
            int outerLoopLimit = (int) Math.Log2(n);
            int innerLoopLimit = (int) 2*n*n;

            foreach((TruthValue, TruthValue) truthValue in truthTable)
            {
                CheckNumberSignAndStore(truthValue.Item1);
                CheckNumberSignAndStore(truthValue.Item2);
            }
            Initialize_TruthValues();
        // End of Initialization-----------------------------------------------------------------
        

        // Main Process------------------------------------------------
            for(int i = 0; i < outerLoopLimit; i++)
            {
                for(int j = 0; j < innerLoopLimit; j++)
                {
                    if(ReplaceMeTruthTableChecker(truthTable))
                    {
                        return true;
                    }
                    else
                    {
                        int randomIndex = randomNumber.Next(0, tableLength);

                        if(!(truthTable[randomIndex].Item1._value || truthTable[randomIndex].Item2._value))
                        {
                            if(randomNumber.Next(0, 2) == 0)
                            {
                                FlipTruthValue(truthTable[randomIndex].Item1);
                            }
                            else
                            {
                                FlipTruthValue(truthTable[randomIndex].Item2);                                
                            }
                        }
                    }
                }
            }
        // End of Main Process----------------------------------------------------------

        // Check 
            return ReplaceMeTruthTableChecker(truthTable);

            // internal functions---------------------------------------------------------------------------------------
            // Initializaztion
            void Initialize_TruthValues()
            {
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

                // Iterate through all the negative literals
                foreach(KeyValuePair<int, TruthValue> tValue in _negativeID_Dict)
                {
                    // Reference for negative check
                    int truthValueID = tValue.Key;

                    if(!_positiveID_Dict.ContainsKey(-truthValueID))
                    {
                        // Truth Value of RandomTruth
                        bool myRandomTruth = GiveRandomTruth();
                        tValue.Value._value = myRandomTruth;
                    }
                }   
            }

            void AddToTheDictList(TruthValue truthValue)
            {
                if(!_truthDictList.ContainsKey(truthValue._ID))
                {
                    _truthDictList.Add(truthValue._ID, truthValue);
                }
            }


            // Check negaFunction
            void CheckNumberSignAndStore(TruthValue truthValue)
            {
                int num = truthValue._ID;
                if(num < 0 && !_negativeID_Dict.ContainsKey(num)) _negativeID_Dict.Add(num, truthValue);  
                else if(num >= 0 && !_positiveID_Dict.ContainsKey(num)) _positiveID_Dict.Add(num, truthValue);  
            }

            bool GiveRandomTruth()
            {
                int rNumber = randomNumber.Next(0,2);
               // Console.WriteLine("Truth Value Number: " + rNumber);
                if(rNumber == 0) {return false;}
                else{return true;}
            }

            // Temporary Check Function
            bool ReplaceMeTruthTableChecker((TruthValue, TruthValue)[] truthTable)
            {
                bool tableValue = true;
                foreach((TruthValue, TruthValue) entry in truthTable)
                {
                    tableValue = tableValue && (entry.Item1._value || entry.Item2._value);

                    if(tableValue == false)
                    {
                        return false;
                    }
                }

                return true;
            }

            // Core Action: Flip Truth Value and it's negative counterpart
            void FlipTruthValue(TruthValue currentTruth)
            {
                currentTruth._value = !currentTruth._value;
                int negativeID = -currentTruth._ID;

                if(_truthDictList.ContainsKey(negativeID))
                {
                    _truthDictList[negativeID]._value = !currentTruth._value;
                }
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
