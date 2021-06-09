using Xunit;
using System;
using System.Collections.Generic;
using Week_13_Programming_Assignment;

public class TestClass
{
    [Fact]
    public void SelfTest()
    {
        Assert.Equal(0,0);
    }

    [Fact]
    public void FileReadTest_1()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
        Dictionary<int, List<(int?,int)>> adjGraph = fileReader.ReadFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 13 Programming Assignment\TestCases\TestCase1.txt");

        // Assert
        // Vertex: 1
        Assert.Equal(2, adjGraph[1][0].Item1);
        Assert.Equal(1, adjGraph[1][0].Item2);
        Assert.Equal(3, adjGraph[1][1].Item1);
    
        // Vertex: 2
        Assert.Equal(4, adjGraph[2][0].Item1);
        Assert.Equal(2, adjGraph[2][0].Item2);

        // Vertex 3
        Assert.Equal(4, adjGraph[3][0].Item1);
        Assert.Equal(3, adjGraph[3][0].Item2);

        // Vertex 4
        Assert.Equal(1, adjGraph[4][0].Item1);
        Assert.Equal(-4, adjGraph[4][0].Item2);
    }

    [Fact]
    public void FileReadTest_2()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
        Dictionary<int, List<(int?,int)>> adjGraph = fileReader.ReadFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 13 Programming Assignment\TestCases\TestCase2.txt");

        // Assert
        // Vertex: 1
        Assert.Equal(2, adjGraph[1][0].Item1);
        Assert.Equal(1, adjGraph[1][0].Item2);
        Assert.Equal(3, adjGraph[1][1].Item1);
    
        // Vertex: 2
        Assert.Equal(4, adjGraph[2][0].Item1);
        Assert.Equal(2, adjGraph[2][0].Item2);

        // Vertex 3
        Assert.Equal(4, adjGraph[3][0].Item1);
        Assert.Equal(3, adjGraph[3][0].Item2);

        // Vertex 4
        Assert.Equal(1, adjGraph[4][0].Item1);
        Assert.Equal(-2, adjGraph[4][0].Item2);
    }


    [Fact]
    public void TestCase_1()
    {
        // Expected answer: negative cycle
    }

    [Fact]
    public void TestCase_2()
    {
        // Expected answer: -2
    }
}