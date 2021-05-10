using System;
using JobLibrary;
using System.IO;

namespace Assignment9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Job hello = new Job(23,23);
            JobFileReader fileReader = new JobFileReader();
            (int, Job[]) myJobData = fileReader.ReadJobFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 9 Programming Assignment\TestCases\Test Case 1.txt");
            JobScheduler jobScheduler = new JobScheduler();
            Job[] gg = jobScheduler.DifferenceCriterion(myJobData.Item2);

            foreach(Job j in gg)
            {
                Console.WriteLine(j._jobValue);
            }
            Console.ReadKey();
        }
    }
}
