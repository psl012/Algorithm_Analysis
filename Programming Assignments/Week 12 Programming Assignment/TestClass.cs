using Xunit;
using Week_12_Programming_Assignment;
using System.Collections.Generic;

public class TestClass
{
    [Fact]
    public void TestClassUnit()
    {
        Assert.Equal(0,0);
    }

    [Fact]
    public void FileReadTest()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        int knapSackSizeAnswer = 8;
        // listOfValuesAnswer = {1,2,5,5,4,1};
        // listOfWeightAnswer = {1,3,4,2,2,5}; 

        // Act
        (int, Item[]) extractedValues = fileReader.ExtractItems(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 12 Programming Assignment\TestCase\testcase1.txt");

        // Assert
        Assert.Equal(knapSackSizeAnswer, extractedValues.Item1);
        Assert.Equal(1, extractedValues.Item2[0]._value);
        Assert.Equal(2, extractedValues.Item2[1]._value);
        Assert.Equal(5, extractedValues.Item2[2]._value);
        Assert.Equal(5, extractedValues.Item2[3]._value);
        Assert.Equal(4, extractedValues.Item2[4]._value);
        Assert.Equal(1, extractedValues.Item2[5]._value);

        Assert.Equal(1, extractedValues.Item2[0]._weight);
        Assert.Equal(3, extractedValues.Item2[1]._weight);
        Assert.Equal(4, extractedValues.Item2[2]._weight);
        Assert.Equal(2, extractedValues.Item2[3]._weight);
        Assert.Equal(2, extractedValues.Item2[4]._weight);
        Assert.Equal(5, extractedValues.Item2[5]._weight);
    }

    [Fact]
    public void TestCase1()
    {
        // Expected Result 14
        // Arrange
        FileReader fileReader = new FileReader();
        (int, Item[]) extractedValues = fileReader.ExtractItems(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 12 Programming Assignment\TestCase\testcase1.txt");
        
        // Act
        KnapSack knapSack = new KnapSack(extractedValues.Item2, extractedValues.Item1);

        // Assert
        Assert.Equal(14, knapSack.GetOptimalSackValue());
    }

    [Fact]
    public void TestCase2()
    {
        // Total Value = 150
        // Total Wight = 190
            // Item with vi 50 and weight 75
            //              50            59
            //              50            56

        // Arrange
        FileReader fileReader = new FileReader();
        (int, Item[]) extractedValues = fileReader.ExtractItems(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 12 Programming Assignment\TestCase\testcase2.txt");
        List<int> indices;

        // Act
        KnapSack knapSack = new KnapSack(extractedValues.Item2, extractedValues.Item1);
        indices = knapSack.Reconstruction();

       // Assert
       Assert.Equal(150, knapSack.GetOptimalSackValue());
       Assert.Equal(4, indices[0]);
       Assert.Equal(1, indices[1]);
       Assert.Equal(0, indices[2]);

        int totalWeight = 0;
       foreach(int ind in indices)
       {
           totalWeight += extractedValues.Item2[ind]._weight;
       }

       Assert.Equal(190, totalWeight);
    }
}