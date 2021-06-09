using Xunit;
using Assignment10;
using System;
using System.Collections.Generic;
using GraphLibrary;

public class TestClass
{
    [Fact]
    public void UnitTest()
    {
        Assert.Equal(0,0);
    }

    [Fact]
    public void FileReader_Test_1()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
        Dictionary<int, List<(int?,int)>> adjgraph = fileReader.ReadFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 10 Programming Assignment\TestCases\TestCase0.txt");

        // Assert
        Assert.Equal((2,1), adjgraph[1][0]);
    }

    [Fact]
    public void FileReader_Test_2()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
        Dictionary<int, List<(int?,int)>> adjgraph = fileReader.ReadFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 10 Programming Assignment\TestCases\TestCase1 - K_2.txt");
        
        // Assert
        // Tail One
        Assert.Equal((2,1), adjgraph[1][0]);
        Assert.Equal((3,100), adjgraph[1][1]);
        Assert.Equal((4,100), adjgraph[1][2]);
        Assert.Equal((5,100), adjgraph[1][3]);

        // Tail Two
        Assert.Equal((1,1), adjgraph[2][0]);
        Assert.Equal((3,100), adjgraph[2][1]);
        Assert.Equal((4,100), adjgraph[2][2]);
        Assert.Equal((5,100), adjgraph[2][3]);

        // Tail Three
        Assert.Equal((1,100), adjgraph[3][0]);
        Assert.Equal((2,100), adjgraph[3][1]);
        Assert.Equal((4,10), adjgraph[3][2]);
        Assert.Equal((5,10), adjgraph[3][3]);

        // Tail 4
        Assert.Equal((1, 100), adjgraph[4][0]);
        Assert.Equal((2, 100), adjgraph[4][1]);
        Assert.Equal((3, 10), adjgraph[4][2]);
        Assert.Equal((5, 10), adjgraph[4][3]);    
    }

    [Fact]
    public void Clustering_Super_Uniit_Test ()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        Dictionary<int, List<(int?,int)>> adjGraph = fileReader.ReadFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 10 Programming Assignment\TestCases\TestCase0.txt");
        Graph graph = new Graph(adjGraph);
        
        // Act
        Clustering clustering = new Clustering();
        (Dictionary<int, Cluster>, int) clusterAnswer = clustering.GroupCluster(graph, 2);

        // Assert
        Assert.Equal(2,clusterAnswer.Item1.Count);
        Assert.Equal(1, clusterAnswer.Item2); 
        
    }

    [Fact]
    public void Unit_Test_1()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        Dictionary<int, List<(int?,int)>> adjGraph = fileReader.ReadFile(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 10 Programming Assignment\TestCases\TestCase1 - K_2.txt");
        Graph graph = new Graph(adjGraph);
        
        // Act
        Clustering clustering = new Clustering();
        (Dictionary<int, Cluster>, int) clusterAnswer = clustering.GroupCluster(graph, 2);

        // Assert
        Assert.Equal(2,clusterAnswer.Item1.Count);
        Assert.Equal(100, clusterAnswer.Item2); 
    }

}