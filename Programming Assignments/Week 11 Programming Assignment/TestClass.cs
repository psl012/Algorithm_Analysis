using Xunit;
using System;
using System.Collections.Generic;
using Week_11_Programming_Assignment;

public class TestClass
{
    [Fact]
    public void RigidTest()
    {
        Assert.Equal(0,0);
    }

    [Fact]
    public void Get_Min_Two_Test()
    {
        // Arrange8
        int[] testSet = {8,7,12};

        Dictionary<string, Tree> exampleTree = new Dictionary<string, Tree>();
        exampleTree.Add("0", new Tree(8));
        exampleTree.Add("1", new Tree(7));
        exampleTree.Add("2", new Tree(12));
               

        Huffman huffman = new Huffman();

        Tree firstMinTreeAnswer = exampleTree["1"];
        Tree secondMinTreeAnswer = exampleTree["0"];

        // Act
        ((Tree, string), (Tree, string)) minTrees = huffman.Get2MinNumber(exampleTree);

        // Assert
        Assert.Equal("1", minTrees.Item1.Item2);
        Assert.Equal("0", minTrees.Item2.Item2);
    
        Assert.Equal(firstMinTreeAnswer, minTrees.Item1.Item1);
        Assert.Equal(secondMinTreeAnswer, minTrees.Item2.Item1);   
    }

    [Fact]
    public void Get_Min_Two_Test_Descending()
    {
        
    }

/**
    [Fact]
    public void TestCase_0()
    {
        // Arrange
        float[] testSet = {3,2,6,8,2,6};
        List<Tree> exampleTree = new List<Tree>() {new Tree(3),
        new Tree(2), new Tree(6), new Tree(8), new Tree(2), new Tree(6)};

        Huffman huffman = new Huffman();

        // Act
        huffman.Apply(testSet);
    
    } 
    */  
}