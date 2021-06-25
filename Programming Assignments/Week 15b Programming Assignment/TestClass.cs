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
    public void File_Read_Test()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
        (double, double)[] cities = fileReader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15b Programming Assignment\TestCases\Test_Case_01.txt");
    
        // Assert
        Assert.Equal((0,0), cities[0]);
        Assert.Equal((0,3), cities[1]);
        Assert.Equal((3,3), cities[2]);
    }

    [Fact]
    public void File_Read_Test_2()
    {
        // Arrange
        FileReader fileReader = new FileReader();
        
        // Act
        (double, double)[] cities = fileReader.ReadFile(@"C:\Users\lacap\Desktop\Paul\Cloud Folders Git\Algorithm Analysis\Programming Assignments\Week 15b Programming Assignment\TestCases\Test_Case_02.txt");
        
        // Assert
        Assert.Equal((0,2.05), cities[0]);
        Assert.Equal((3.414213562373095, 3.4642135623730947), cities[1]);
        Assert.Equal((0.5857864376269049, 0.6357864376269047), cities[2]);
        Assert.Equal((0.5857864376269049, 3.4642135623730947), cities[3]);
        Assert.Equal((2, 0), cities[4]);
        Assert.Equal((4.05, 2.05), cities[5]);
        Assert.Equal((2, 4.10), cities[6]);
        Assert.Equal((3.414213562373095, 0.6357864376269047), cities[7]);
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

}