using Xunit;
using Week_9_Programming_Assignment;

public class TestClass
{
    [Fact]
    public void JobFileReadTest()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        int answerNumberOfJobs = 12;
        Job[] answerArrayOfJobs = new Job[answerNumberOfJobs];
        answerArrayOfJobs[0] = new Job(8, 50);
        answerArrayOfJobs[1] = new Job(74, 59);
        answerArrayOfJobs[2] = new Job(31, 73);
        answerArrayOfJobs[3] = new Job(45, 79);
        answerArrayOfJobs[4] = new Job(24, 10);
        answerArrayOfJobs[5] = new Job(41, 66);
        answerArrayOfJobs[6] = new Job(93, 43);
        answerArrayOfJobs[7] = new Job(88, 4);
        answerArrayOfJobs[8] = new Job(28, 30);
        answerArrayOfJobs[9] = new Job(41, 13);
        answerArrayOfJobs[10] = new Job(4, 70);
        answerArrayOfJobs[11] = new Job(10, 58);

        (int, Job[]) myJobData;

        // Act
        myJobData = fileReader.ReadJobFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 9 Programming Assignment\TestCases\Test Case 1.txt");

        // Assert
        Assert.Equal(answerNumberOfJobs, myJobData.Item1);

        for(int i=0; i<answerArrayOfJobs.Length; i++)
        {
            Assert.Equal(answerArrayOfJobs[i]._weight, myJobData.Item2[i]._weight);
            Assert.Equal(answerArrayOfJobs[i]._length, myJobData.Item2[i]._length);
        }
    }

    [Fact]
    public void DifferenceCriterion_Test()
    {
        // Arrange
        Job[] valueAnswer = new Job[12];
        valueAnswer[0] = new Job(88, 4, 84);
        valueAnswer[1] = new Job(93, 43, 50);
        valueAnswer[2] = new Job(41, 13, 28);   
        valueAnswer[3] = new Job(74, 59, 15);
        valueAnswer[4] = new Job(24, 10, 14);
        valueAnswer[5] = new Job(28, 30, -2);
        valueAnswer[6] = new Job(41, 66, -25);
        valueAnswer[7] = new Job(45, 79, -34);
        valueAnswer[8] = new Job(31, 73, -42);                
        valueAnswer[9] = new Job(8, 50, -42);
        valueAnswer[10] = new Job(10, 58, -48);
        valueAnswer[11] = new Job(4, 70, -66);
        
        FileReader fileReader = new FileReader();
        (int, Job[]) myJobData = fileReader.ReadJobFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 9 Programming Assignment\TestCases\Test Case 1.txt");
        JobScheduler jobScheduler = new JobScheduler();
        
        // Act
        Job[] myJobAnswer = jobScheduler.DifferenceCriterion(myJobData.Item2);

        // Assert
        for(int i=0; i < 12; i++)
        {
            Assert.Equal(valueAnswer[i]._jobValue, myJobAnswer[i]._jobValue);
        }

        // Difference Test
        Assert.Equal(68615, jobScheduler.GetWeightedSum(myJobAnswer));
    }

    [Fact]
    public void RatioCriterion_Test()
    {
        FileReader fileReader = new FileReader();
        (int, Job[]) myJobData = fileReader.ReadJobFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 9 Programming Assignment\TestCases\Test Case 1.txt");
        JobScheduler jobScheduler = new JobScheduler();
        
        // Act
        Job[] myJobAnswer = jobScheduler.RatioCriterion(myJobData.Item2);

        // Assert
        Assert.Equal(67247, jobScheduler.GetWeightedSum(myJobAnswer));

    }
}