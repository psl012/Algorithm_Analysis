using System;
using System.IO;

namespace Week_2_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
            string [] lines = File.ReadAllLines("IntegerArray.txt");
            Console.WriteLine(lines[0]);           
            string [] test = {"34", "wd", "$%"};
            using (StreamWriter writer = new StreamWriter("tester.txt"))
            {
                writer.WriteLine(test[0]);
            }
*/          
            ArraySorter _arraySorter = new ArraySorter();

            int [] output;
            

            //   string [] numberArray = File.ReadAllLines("IntegerArray.txt");
            string [] numberStringArray = {"25", "10", "5", "0", "2", "45", "12", "22"};
            int [] numberArray = Array.ConvertAll(numberStringArray, Convert.ToInt32);

            output = _arraySorter.MergeSort(numberArray);
            Console.ReadKey();      
        }

        
    }

    public class ArraySorter
    {


        public int[] MergeSort(int[] array)
        {
            int midPoint = array.Length/2;  
            int[] sortedArray = new int[10];

            if (array.Length > 1)
            {
                int [] leftArray = array[0..midPoint];
                int [] rightArray = array[(midPoint + 1)..]; 

                if(leftArray.Length > 1 || rightArray.Length > 1)
                {
                    leftArray = MergeSort(leftArray);
                    rightArray = MergeSort(rightArray);
                }
                
                int i =0;
                int j = 0;
                int k = 0;
                while (i < array.Length)
                {
                    if(i >= leftArray.Length && j >= rightArray.Length)
                    {
                        return sortedArray;
                    }

                    else if (i == leftArray.Length)
                    {
                        sortedArray[k] = rightArray[j];
                        j++;
                        k++;
                    }

                    else if(j==rightArray.Length)
                    {
                        sortedArray[k] = leftArray[i];
                        i++;
                        k++;
                    }

                    else if(i < leftArray.Length || j < rightArray.Length)
                    {
                        if(leftArray[i] < rightArray[j])
                        {
                            sortedArray[k] = leftArray[i];
                            k++;
                            i++;
                        }
                        else
                        {
                            sortedArray[k] = rightArray[j];
                            k++;
                            j++;
                        }
                    }
    
                }
            }
            
            return sortedArray;
        }
    }
}
