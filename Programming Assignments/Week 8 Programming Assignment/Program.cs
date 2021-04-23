using System;
using System.Collections;
using System.IO;


namespace Week_8_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCase _testCase = new TestCase();
            Hashtable gg = new Hashtable();

            Console.WriteLine("Hello World!");
            _testCase.TestCase1();

            Console.ReadKey();
        }

    }

    class Sum2Array
    {
        Hashtable _ht = new Hashtable();
        int[] _intArray;
      
        public Sum2Array(int[] intArray)
        {
            _intArray = intArray;
        }

        public void HashMethod()
        {
            for(int i=0; i < _intArray.Length; i++)
            {
                if(!_ht.Contains(_intArray[i]))
                {
                    _ht.Add(_intArray[i], _intArray[i]);
                }
                else
                {
                    Console.WriteLine("This repeats");
                }
               
            }

            
        }

        public void DisplayTable()
        {
            Console.WriteLine("Hash");
            foreach(DictionaryEntry num in _ht)
            {
                Console.WriteLine(num.Value);
            }

            Console.WriteLine("");

            for(int i=0; i < _intArray.Length; i++)
            {
                Console.WriteLine(_intArray[i]);
            }
        }
    }

    class TestCase
    {
        Sum2Array _sum2ArrayTestCase1;

        public void TestCase1()
        {
            string[] text = File.ReadAllLines(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 8 Programming Assignment\TestCase.txt");          
            int[] lineIntForm = Array.ConvertAll(text, s => Convert.ToInt32(s));
            
            _sum2ArrayTestCase1 = new Sum2Array(lineIntForm);
            _sum2ArrayTestCase1.HashMethod();
            _sum2ArrayTestCase1.DisplayTable();

            //Console.WriteLine(lineIntForm[0] + lineIntForm[1]);
        }
    }
}
