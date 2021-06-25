using Xunit;
using Week_15_Programming_Asssignment;
using System;
using System.Collections.Generic;

public class TestClass
{
    [Fact]
    public void SelfTest()
    {
        Assert.Equal(0,0);
    }

    [Fact]
    public void PowerSet_Test()
    {
        // Arrange
        SetFunctions setFunctions = new SetFunctions();
        int[] set = {1,2,3,4};
        
        // Act
        QuickSort quickSort = new QuickSort();
        List<int?>[] powerSet = setFunctions.OrderedPowerSet(set);
        quickSort.Sort(powerSet, 0, powerSet.Length-1);

        // Assert
        Assert.Equal(15, powerSet.Length);        
    }

    [Fact]
    public void Test_Case_PowerSet1_City0()
    {
        // Arrange
        SetFunctions setFunctions = new SetFunctions();
        QuickSort quickSort = new QuickSort();
        (double, double)[] cityMap = {(0,0), (0,3), (3,3)};
        int[] cityIndex = {0,1,2};

        List<int?>[] powerSet = setFunctions.OrderedPowerSet(cityIndex);
        
        // Act
        CityVertex vertex = new CityVertex(0, cityMap);

        // Assert
        int i = 0;
        foreach(KeyValuePair<int, double> entry in vertex._neighbor)
        {
            Assert.Equal(i, entry.Key);
            i++;
        }

        Assert.Equal(0, vertex._neighbor[0]);
        Assert.Equal(3, vertex._neighbor[1]);
        Assert.Equal(4.2426, Math.Round(vertex._neighbor[2], 4));
    }

    [Fact]
    public void Test_Case_PowerSet2_City1()
    {
        // Arrange
        SetFunctions setFunctions = new SetFunctions();
        QuickSort quickSort = new QuickSort();
        (double, double)[] cityMap = {(0,0), (0,3), (3,3)};
        int[] cityIndex = {0,1,2};

        List<int?>[] powerSet = setFunctions.OrderedPowerSet(cityIndex);
        
        // Act
        CityVertex vertex = new CityVertex(1, cityMap);

        // Assert
        int i = 0;
        foreach(KeyValuePair<int, double> entry in vertex._neighbor)
        {
            Assert.Equal(i, entry.Key);
            i++;
        }

        Assert.Equal(3, vertex._neighbor[0]);
        Assert.Equal(0, vertex._neighbor[1]);
        Assert.Equal(3, vertex._neighbor[2]);        
    }

    [Fact]
    public void Test_Case_PowerSet3_City2()
    {
        // Arrange
        SetFunctions setFunctions = new SetFunctions();
        QuickSort quickSort = new QuickSort();
        (double, double)[] cityMap = {(0,0), (0,3), (3,3)};
        int[] cityIndex = {0,1,2};

        List<int?>[] powerSet = setFunctions.OrderedPowerSet(cityIndex);
        
        // Act
        CityVertex vertex = new CityVertex(2, cityMap);

        // Assert
        int i = 0;
        foreach(KeyValuePair<int, double> entry in vertex._neighbor)
        {
            Assert.Equal(i, entry.Key);
            i++;
        }
        Assert.Equal(4.2426, Math.Round(vertex._neighbor[0], 4));
        Assert.Equal(3, vertex._neighbor[1]);
        Assert.Equal(0, vertex._neighbor[2]);        
    }

    [Fact]
    public void Test_Case_Graph()
    {
        // Arrange
        (double, double)[] cityMap = {(0,0), (0,3), (3,3)};

        // Act
        Graph graph = new Graph(cityMap);

        // Assert
        Assert.Equal(3, graph._vertices.Length);
        Assert.Equal(0, graph._vertices[0]._myCityCode);
        Assert.Equal(1, graph._vertices[1]._myCityCode);
        Assert.Equal(2, graph._vertices[2]._myCityCode);

        Assert.Equal(0, graph._vertices[0]._neighbor[0]);
        Assert.Equal(3, graph._vertices[0]._neighbor[1]);
        Assert.Equal(4.2426, Math.Round(graph._vertices[0]._neighbor[2], 4));

        Assert.Equal(3, graph._vertices[1]._neighbor[0]);
        Assert.Equal(0, graph._vertices[1]._neighbor[1]);
        Assert.Equal(3, graph._vertices[1]._neighbor[2]);   

        Assert.Equal(4.2426, Math.Round(graph._vertices[2]._neighbor[0], 4));
        Assert.Equal(3, graph._vertices[2]._neighbor[1]);
        Assert.Equal(0, graph._vertices[2]._neighbor[2]);  
    }

    [Fact]
    public void Test_Case_01()
    {
        // Arrange
        FileReader filereader = new FileReader();
        (double, double)[] cityMap = filereader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15 Programming Asssignment\TestCases\Test_Case_01.txt");
        Graph graph = new Graph(cityMap);
        TSPFunction tspFunction = new TSPFunction();
    
        // Act
        double minLength = tspFunction.BellmanHeldKarp(graph);

        // Assert
        Assert.Equal(10.24, Math.Round(minLength, 2));

    }

    [Fact]
    public void Test_Case_02()
    {
        // Arrange
        FileReader filereader = new FileReader();
        (double, double)[] cityMap = filereader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15 Programming Asssignment\TestCases\Test_Case_02.txt");

        Graph graph = new Graph(cityMap);
        TSPFunction tspFunction = new TSPFunction();

        // Act
        double minLength = tspFunction.BellmanHeldKarp(graph);

        // Assert
        Assert.Equal(12.36, Math.Round(minLength, 2));
    }

    [Fact]
    public void Test_Case_03()
    {
        // Arrange 
        FileReader filereader = new FileReader();
        (double, double)[] cityMap = filereader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15 Programming Asssignment\TestCases\Test_Case_03.txt");

        Graph graph = new Graph(cityMap);
        TSPFunction tspFunction = new TSPFunction();

        // Act
        double minLength = tspFunction.BellmanHeldKarp(graph);

        // Assert
        Assert.Equal(14, minLength);
    }
}