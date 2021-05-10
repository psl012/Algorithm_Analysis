using System;
using System.Collections.Generic;
using System.IO;

namespace Week_9_Programming_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
         /**   FileReader fileReader = new FileReader();
            (int, Job[]) myJobData = fileReader.ReadJobFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 9 Programming Assignment\TestCases\Test Case 1.txt");
            JobScheduler jobScheduler = new JobScheduler();
            Job[] gg = jobScheduler.DifferenceCriterion(myJobData.Item2);

            foreach(Job j in gg)
            {
                Console.WriteLine(j._jobValue);
            }*/

            Console.ReadKey();
        }
    }

    class JobScheduler
    {
        QuickSort _quickSort = new QuickSort();
        public Job[] DifferenceCriterion(Job[] arrayOfJobs)
        {
            ClearJobValues(arrayOfJobs);
            for(int i=0; i<arrayOfJobs.Length; i++)
            {
                arrayOfJobs[i]._jobValue = arrayOfJobs[i]._weight - arrayOfJobs[i]._length;
            }
            _quickSort.Sort(arrayOfJobs, 0, arrayOfJobs.Length - 1);
            
            return arrayOfJobs;
        }

        public Job[] RatioCriterion(Job[] arrayOfJobs)
        {
            ClearJobValues(arrayOfJobs);
            for(int i=0; i<arrayOfJobs.Length; i++)
            {
                arrayOfJobs[i]._jobValue = arrayOfJobs[i]._weight / arrayOfJobs[i]._length;
            }
            _quickSort.Sort(arrayOfJobs, 0, arrayOfJobs.Length - 1);
            
            return arrayOfJobs;
        }

        public float GetWeightedSum(Job[] arrayOfJobs)
        {
            float cTime = 0;
            float weightedSum = 0;
            foreach(Job job in arrayOfJobs)
            {
                cTime += job._length;
                weightedSum += cTime*job._weight;
            }
            return weightedSum;
        }

        void ClearJobValues(Job[] arrayOfJobs)
        {
            foreach(Job j in arrayOfJobs)
            {
                j._jobValue = -9999;
            }
        }
    }

    class FileReader
    {
        public (int,Job[]) ReadJobFile(string dir)
        {
            string[] line = File.ReadAllLines(dir);
            int numberOfJobs = Convert.ToInt32(line[0]);
            Job[] arrayOfJobs = new Job[numberOfJobs];
            
            for(int i=1; i< line.Length; i++)
            {
                string[] temp = line[i].Split();
                int weight = Convert.ToInt32(temp[0]);
                int length = Convert.ToInt32(temp[1]);
                arrayOfJobs[i - 1] = new Job(weight, length);
            }

            return (numberOfJobs, arrayOfJobs);
        }
    }

    class Job
    {
        public float _weight {get; private set;}
        public float _length {get; private set;}
        public float _jobValue;

        public Job(int weight, int length, int jobValue = -9999)
        {
            _weight = weight;
            _length = length;
            _jobValue = jobValue;
        }
    }

    class QuickSort
    {
        int Partition(Job[] array, int low,
                                        int high)
        {
            //1. Select a pivot point.
            float pivot = array[high]._jobValue;

            int lowIndex = (low - 1);

            //2. Reorder the collection.
            for (int j = low; j < high; j++)
            {
                if (array[j]._jobValue > pivot)
                {
                    lowIndex++;

                    Job temp = array[lowIndex];
                    array[lowIndex] = array[j];
                    array[j] = temp;
                }
                else if( array[j]._jobValue == pivot)
                {
                    if(array[j]._weight > array[high]._weight)
                    {
                        lowIndex++;

                        Job temp = array[lowIndex];
                        array[lowIndex] = array[j];
                        array[j] = temp;
                    }
                }
            }

            Job temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;

            return lowIndex + 1;
        }

        public void Sort(Job[] array, int low, int high)
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
