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
    public void Test_Case1_City0()
    {
        // Arrange
        SetFunctions setFunctions = new SetFunctions();
        QuickSort quickSort = new QuickSort();
        (float, float)[] cityMap = {(0,0), (0,3), (3,3)};
        int[] cityIndex = {0,1,2};

        List<int?>[] powerSet = setFunctions.OrderedPowerSet(cityIndex);
        
        // Act
        CityVertex vertex = new CityVertex(0, cityMap);

        // Assert
        int i = 0;
        foreach(KeyValuePair<int, float> entry in vertex._neighbor)
        {
            Assert.Equal(i, entry.Key);
            i++;
        }

        Assert.Equal(0, vertex._neighbor[0]);
        Assert.Equal(3, vertex._neighbor[1]);
        Assert.Equal(4.2426f, MathF.Round(vertex._neighbor[2], 4));
    }

    [Fact]
    public void Test_Case1_City1()
    {
        // Arrange
        SetFunctions setFunctions = new SetFunctions();
        QuickSort quickSort = new QuickSort();
        (float, float)[] cityMap = {(0,0), (0,3), (3,3)};
        int[] cityIndex = {0,1,2};

        List<int?>[] powerSet = setFunctions.OrderedPowerSet(cityIndex);
        
        // Act
        CityVertex vertex = new CityVertex(1, cityMap);

        // Assert
        int i = 0;
        foreach(KeyValuePair<int, float> entry in vertex._neighbor)
        {
            Assert.Equal(i, entry.Key);
            i++;
        }

        Assert.Equal(3, vertex._neighbor[0]);
        Assert.Equal(0, vertex._neighbor[1]);
        Assert.Equal(3, vertex._neighbor[2]);        
    }

    [Fact]
    public void Test_Case1_City2()
    {
        // Arrange
        SetFunctions setFunctions = new SetFunctions();
        QuickSort quickSort = new QuickSort();
        (float, float)[] cityMap = {(0,0), (0,3), (3,3)};
        int[] cityIndex = {0,1,2};

        List<int?>[] powerSet = setFunctions.OrderedPowerSet(cityIndex);
        
        // Act
        CityVertex vertex = new CityVertex(2, cityMap);

        // Assert
        int i = 0;
        foreach(KeyValuePair<int, float> entry in vertex._neighbor)
        {
            Assert.Equal(i, entry.Key);
            i++;
        }

        Assert.Equal(4.2426f, MathF.Round(vertex._neighbor[0], 4));
        Assert.Equal(3, vertex._neighbor[1]);
        Assert.Equal(0, vertex._neighbor[2]);        
    }

    [Fact]
    public void Test_Case_Graph()
    {
        // Arrange
        (float, float)[] cityMap = {(0,0), (0,3), (3,3)};

        // Act
        Graph graph = new Graph(cityMap);

        // Assert
        Assert.Equal(3, graph._vertices.Length);
        Assert.Equal(0, graph._vertices[0]._myCityCode);
        Assert.Equal(1, graph._vertices[1]._myCityCode);
        Assert.Equal(2, graph._vertices[2]._myCityCode);

        Assert.Equal(0, graph._vertices[0]._neighbor[0]);
        Assert.Equal(3, graph._vertices[0]._neighbor[1]);
        Assert.Equal(4.2426f, MathF.Round(graph._vertices[0]._neighbor[2], 4));

        Assert.Equal(3, graph._vertices[1]._neighbor[0]);
        Assert.Equal(0, graph._vertices[1]._neighbor[1]);
        Assert.Equal(3, graph._vertices[1]._neighbor[2]);   

        Assert.Equal(4.2426f, MathF.Round(graph._vertices[2]._neighbor[0], 4));
        Assert.Equal(3, graph._vertices[2]._neighbor[1]);
        Assert.Equal(0, graph._vertices[2]._neighbor[2]);  
    }
}