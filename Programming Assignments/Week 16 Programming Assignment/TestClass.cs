using Xunit;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Week_16_Programming_Assignment;

public class TestClass
{   
    [Fact]
    public void Self_Test()
    {
        Assert.Equal(0,0);
    }

    [Fact]
    public void Scan_Test()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
        (int, int)[] myAnswer = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Test_Case_01.txt");

        // Assert
        Assert.Equal((2,5), (myAnswer[0]));
        Assert.Equal((1,87), (myAnswer[1]));
        Assert.Equal((-5,2), (myAnswer[2]));

    }

    [Fact]
    public void Partial_Test_Real_Case_01()
    {
         // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
        (int, int)[] myAnswer = fileReader.Scan(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 16 Programming Assignment\Test Cases\Real_Case_01.txt");

        // Assert
        Assert.Equal((-16808, 75250), (myAnswer[0]));
        Assert.Equal((22891, 24500), (myAnswer[51778]));
        Assert.Equal((52249, 60667), (myAnswer[99999]));  
        Assert.Equal(100000, myAnswer.Length);
    }
}