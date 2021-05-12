using Xunit;
using System;
using HeapLibrary;

public class TestClassHeap
{
    [Fact]
    public void MinHashTest()
    {
        // Test for MinHash Creation, add, and pull
        // Arrange
        MinIntHeap minIntHeap = new MinIntHeap();
        int[] arrayAnswer = {10, 15, 20, 17};
        int[] myArray = new int[4];

        // Act
        minIntHeap.add(10);
        minIntHeap.add(15);
        minIntHeap.add(20);
        minIntHeap.add(17);

        // Assert
        Assert.Equal(10, minIntHeap.pull());
        Assert.Equal(15, minIntHeap.pull());
        Assert.Equal(17, minIntHeap.pull());
        Assert.Equal(20, minIntHeap.pull());
    }

    

     [Fact]
    public void MinHashNodePullFuncTests()
    {
        // Arrange
        MinIntHeap minIntHeap = new MinIntHeap();
        // 10 15 20 17
        int[] arrayAnswer = {15, 17, 20};
        int[] myArray = new int[4];
        minIntHeap.add(10);
        minIntHeap.add(15);
        minIntHeap.add(20);
        minIntHeap.add(17);

        // Act
        minIntHeap.pull();
        
        // Assert
        Assert.Equal(15, minIntHeap.pull());
    }

    [Fact]
    public void MinHashNodePushFuncTests()
    {
        // Arrange
        MinIntHeap minIntHeap = new MinIntHeap();
        // 15, 17, 20
        int[] arrayAnswer = {5, 15, 17, 20};
        int[] myArray = new int[4];

        // Act
        minIntHeap.add(10);
        minIntHeap.add(15);
        minIntHeap.add(20);
        minIntHeap.add(17);
        minIntHeap.pull();
        minIntHeap.add(5);

        // Assert
        Assert.Equal(5, minIntHeap.pull());
        Assert.Equal(15, minIntHeap.pull());
        Assert.Equal(17, minIntHeap.pull());
        Assert.Equal(20, minIntHeap.pull());
    }

    [Fact]
    public void MinIntHashOverflowTest()
    {
        // Arrange
        MinIntHeap minIntHeap = new MinIntHeap(4);
        int[] arrayAnswer = {5, 10, 15, 20, 17};
        int[] myArray = new int[5];

        // Act
        minIntHeap.add(10);
        minIntHeap.add(15);
        minIntHeap.add(20);
        minIntHeap.add(17);
        minIntHeap.add(5);
        
        // Assert
        Assert.Equal(8,minIntHeap.GetHeapSize()); 
    }

    [Fact]
    public void MaxIntHeap_Test()
    {
                // Test for MinHash Creation, add, and pull
        // Arrange
        MaxIntHeap maxIntHeap = new MaxIntHeap();
        int[] arrayAnswer = {10, 15, 20, 17};
        int[] myArray = new int[4];

        // Act
        maxIntHeap.add(10);
        maxIntHeap.add(15);
        maxIntHeap.add(20);
        maxIntHeap.add(17);

        // Assert
        Assert.Equal(20, maxIntHeap.pull());
        Assert.Equal(17, maxIntHeap.pull());
        Assert.Equal(15, maxIntHeap.pull());
        Assert.Equal(10, maxIntHeap.pull());
    }

    [Fact]
    public void MaxHashNodePullFuncTests()
    {
        // Arrange
        MaxIntHeap maxIntHeap = new MaxIntHeap();
        // 10 15 20 17
        int[] arrayAnswer = {15, 17, 20};
        int[] myArray = new int[4];
        maxIntHeap.add(10);
        maxIntHeap.add(15);
        maxIntHeap.add(20);
        maxIntHeap.add(17);

        // Act
        maxIntHeap.pull();
        
        // Assert
        Assert.Equal(17, maxIntHeap.pull());
    }

    [Fact]
    public void MaxHashNodePushFuncTests()
    {
        // Arrange
        MaxIntHeap maxIntHeap = new MaxIntHeap();
        // 15, 17, 20
        int[] arrayAnswer = {5, 15, 17, 20};
        int[] myArray = new int[4];

        // Act
        maxIntHeap.add(10);
        maxIntHeap.add(15);
        maxIntHeap.add(20);
        maxIntHeap.add(17);
        maxIntHeap.pull();
        maxIntHeap.add(5);

        // Assert
        Assert.Equal(17, maxIntHeap.pull());
        Assert.Equal(15, maxIntHeap.pull());
        Assert.Equal(10, maxIntHeap.pull());
        Assert.Equal(5, maxIntHeap.pull());
    }

    [Fact]
    public void MaxIntHashOverflowTest()
    {
        // Arrange
        MaxIntHeap maxIntHeap = new MaxIntHeap(4);
        int[] arrayAnswer = {5, 10, 15, 20, 17};
        int[] myArray = new int[5];

        // Act
        maxIntHeap.add(10);
        maxIntHeap.add(15);
        maxIntHeap.add(20);
        maxIntHeap.add(17);
        maxIntHeap.add(5);
        
        // Assert
        Assert.Equal(8,maxIntHeap.GetHeapSize()); 
    }

    [Fact]
    public void Pull_ZeroSizeTree_ThrowsArgumentOutofRangeException()
    {
        // Arrange
        MinIntHeap minIntHeap = new MinIntHeap(0);
        MaxIntHeap maxIntHeap = new MaxIntHeap(0);

        // Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => minIntHeap.pull());
        Assert.Contains("Tree size is zero", ex.Message);

        var ex2 = Assert.Throws<ArgumentOutOfRangeException>(() => maxIntHeap.pull());
        Assert.Contains("Tree size is zero", ex2.Message);
    }

    [Fact]
    public void Peek_ZeroSizeTree_ThrowsArgumentOutOfRangeException()
    {
        MinIntHeap minIntHeap = new MinIntHeap(0);
        MaxIntHeap maxIntHeap = new MaxIntHeap(0);

        // Assert
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => minIntHeap.peek());
        Assert.Contains("Tree size is zero", ex.Message);
    
        var ex2 = Assert.Throws<ArgumentOutOfRangeException>(() => maxIntHeap.peek());
        Assert.Contains("Tree size is zero", ex2.Message);
    }
}