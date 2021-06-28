using Xunit;
using Week_15b_Programming_Assignment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TestClass
{
    [Fact]
    public void Self_Test()
    {
        Assert.Equal(0,0);        
    }

    [Fact]
    public void File_Graph_Test_3()
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
    public void Test_Case_Array_To_Dictionary()
    {
        // Arrange
        TSPFunctions tspFunction = new TSPFunctions();
        (double, double)[] cityMap = {(0,0), (0,3), (3,3)};
        Dictionary<int, (double, double)> dictAnswer = new Dictionary<int, (double, double)>();

        // Act
        dictAnswer = tspFunction.GetDictFromCityMap(cityMap);

        // Assert
        Assert.Equal((0,0), dictAnswer[0]);
        Assert.Equal((0,3), dictAnswer[1]);
        Assert.Equal((3,3), dictAnswer[2]);
        Assert.Equal(3, dictAnswer.Count);
    }
    
    [Fact]
    public void Test_Case_m01()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        TSPFunctions tspFunction = new TSPFunctions();

        (double, double)[] cities = fileReader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15b Programming Assignment\TestCases\Test_Case_m01.txt");

        // Act
        (double, double)[] answer = tspFunction.TrickHeuristicTSP(cities);
        double trueAnswer = tspFunction.EucledianCompute(answer);

        // Assert
        Assert.Equal(0, Math.Round(trueAnswer));
    }

    [Fact]
    public void Test_Case_m02()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        TSPFunctions tspFunction = new TSPFunctions();

        (double, double)[] cities = fileReader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15b Programming Assignment\TestCases\Test_Case_m02.txt");

        // Act
        (double, double)[] answer = tspFunction.TrickHeuristicTSP(cities);
        double trueAnswer = tspFunction.EucledianCompute(answer);

        // Assert
        Assert.Equal(2, Math.Round(trueAnswer));
    }

    [Fact]
    public void Test_Case_m03b()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        TSPFunctions tspFunction = new TSPFunctions();

        (double, double)[] cities = fileReader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15b Programming Assignment\TestCases\Test_Case_m03b.txt");

        // Act
        (double, double)[] answer = tspFunction.TrickHeuristicTSP(cities);
        double trueAnswer = tspFunction.EucledianCompute(answer);

        // Assert
        Assert.Equal(5, Math.Round(trueAnswer));
    }

    [Fact]
    public void Test_Case_m04()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        TSPFunctions tspFunction = new TSPFunctions();

        (double, double)[] cities = fileReader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15b Programming Assignment\TestCases\Test_Case_m04.txt");

        // Act
        (double, double)[] answer = tspFunction.TrickHeuristicTSP(cities);
        double trueAnswer = tspFunction.EucledianCompute(answer);

        // Assert
        Assert.Equal(11, Math.Round(trueAnswer));
    }

        [Fact]
    public void Test_Case_01()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        TSPFunctions tspFunction = new TSPFunctions();

        (double, double)[] cities = fileReader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15b Programming Assignment\TestCases\Test_Case_01b.txt");

        // Act
        (double, double)[] answer = tspFunction.TrickHeuristicTSP(cities);
        double trueAnswer = tspFunction.EucledianCompute(answer);

        // Assert
        Assert.Equal(23, Math.Round(trueAnswer));
    }

}