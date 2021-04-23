using System;
using System.IO;

namespace Week_3_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            QuickSort _quickSort = new QuickSort();  

            string[] lines = File.ReadAllLines("QuickSort Data.txt");
            Console.WriteLine(lines[0]);

            int[] number = Array.ConvertAll(lines, Convert.ToInt32);
            
            _quickSort.Partition(number, 0, number.Length - 1, 2);
            
            using(StreamWriter writer = new StreamWriter("Output.txt"))
            {
                writer.WriteLine("Number of Comparisons {0}: ", _quickSort._numberOfComparison);
                writer.WriteLine();

                foreach(int num in number)
                {
                    writer.WriteLine(num);
                }
            }


            Console.ReadKey();
        }
    }

    class QuickSort
    {
        public int _numberOfComparison{get; private set;} = 0;

        public int[] Partition(int[] numberList, int l, int r, int pivotCode)
        {
            if (l >= r)
            {
                return numberList;
            }
            
            int pivotIndex = ChoosePivot(numberList, pivotCode, l, r);
            int pivot = numberList[pivotIndex];
            // pivot = 8

            Swap(ref numberList[l], ref numberList[pivotIndex]);
            // 8,3

            int i = l + 1;
            // i = 1

            for(int j = l + 1; j <= r; j++)
            {
                if(numberList[j] < pivot)
                {
                    // j = 1
                    Swap(ref numberList[i], ref numberList[j]);
                    // 8, 3
                    i = i + 1;
                    // i = 2
                }
            }

            Swap(ref numberList[l], ref numberList[i-1]);
            // l=0, i-1 = 1; 3,8

            _numberOfComparison += r - l - 1;

            Partition(numberList, l, i-2, pivotCode);
            // 0 , 1, 1

            Partition(numberList, i, r, pivotCode); 
            // 2, 1, 1
            return numberList;
        }

        int ChoosePivot(int[] numberList, int code, int l, int r)
        {
            if(code == 0)
            {
                return l;
            }
            else if(code ==1)
            {
                return r;
            }
            else
            {
                int leftNumber = numberList[l];
                int rightNumber = numberList[r];
                int midNumber = numberList[(r+l)/2];
                if (leftNumber > rightNumber && leftNumber < midNumber)
                {
                    return l;
                }
                else if(leftNumber > midNumber && leftNumber < rightNumber)
                {
                    return l;
                }

                else if (rightNumber > leftNumber && rightNumber < midNumber)
                {
                    return r;
                }
                else if (rightNumber > midNumber && rightNumber < leftNumber)
                {
                    return r;
                }
                else
                {
                    return(r+l)/2;
                }

            }
        }

        void Swap(ref int num1,ref int num2)
        {
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }
    }
}
