using Xunit;
using GraphLibrary;
using System.Collections.Generic;
using Assignment9;

public class TestClassGraph
{
    [Fact]
    public void MakingVertexTest()
    {
        // Arrange
        Vertex vertex = new Vertex(4);

        // Act
        int? number = vertex._number;

        // Assert
        Assert.Equal(4, number); 
    }

    [Fact]
    public void MakingEdgeTest()
    {
        // Arrange
        Vertex tail = new Vertex(2);
        Vertex head = new Vertex(10);
        Edge edge = new Edge(tail, head, 5);

        // Act
        Assert.Equal(tail, edge._tail);
        Assert.Equal(head, edge._head);
        Assert.Equal(5, edge._length);
    }

    [Fact]
    public void GraphFileReaderTest()
    {
        // Arrange
        Dictionary<int, List<(int?, int)>> answerForAdjGraph = new Dictionary<int, List<(int?, int)>>();
        List<(int?, int)> lst1 = new List<(int?, int)>();
        List<(int?, int)> lst2 = new List<(int?, int)>();
        List<(int?, int)> lst3 = new List<(int?, int)>();
        List<(int?, int)> lst4 = new List<(int?, int)>();

        lst1.Add((2,1)); lst1.Add((3,4));
        lst2.Add((3,2)); lst2.Add((4,6));
        lst3.Add((4,3));
        
        lst4.Add((null, -9999));

        answerForAdjGraph.Add(1, lst1);
        answerForAdjGraph.Add(2, lst2);
        answerForAdjGraph.Add(3, lst3);
        answerForAdjGraph.Add(4, lst4);

        Week9FileReader fileReader = new Week9FileReader();

        // Act
        Dictionary<int, List<(int?, int)>> myGraph = fileReader.GraphFileReader(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 9 Programming Assignment with DLL\Assignment9\Unit Test Data.txt");
   
        // Assert
        Assert.Equal((2,1), myGraph[1][0]);
        Assert.Equal((3,4), myGraph[1][1]);
        Assert.Equal((3,2), myGraph[2][0]);
        Assert.Equal((4,6), myGraph[2][1]);
        Assert.Equal((4,3), myGraph[3][0]);

        (int?, int) lastTest = (null, -9999);
        Assert.Equal((lastTest), myGraph[4][0]);
    }

    [Fact]
    public void MakingGraphTest()
    {
        // Arrange
        Week9FileReader fileReader = new Week9FileReader();
        Dictionary<int, List<(int?, int)>> myGraph = fileReader.GraphFileReader(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 9 Programming Assignment with DLL\Assignment9\Unit Test Data.txt");
        Graph graph = new Graph(myGraph);

        // Act
        // Vertices
        int? vertex_1 = graph._vertices[1]._number;
        int? vertex_2 = graph._vertices[2]._number;
        int? vertex_3 = graph._vertices[3]._number;
        int? vertex_4 = graph._vertices[4]._number;
        
        // Assert
        Assert.Equal(1, vertex_1);
        Assert.Equal(2, vertex_2);
        Assert.Equal(3, vertex_3);
        Assert.Equal(4, vertex_4);
        Assert.Equal(4, graph._vertices.Count);

        // Edges Test
        // Arrange
        (int?, int?, int) edge_1_2 = (1,2,1);
        (int?, int?, int) edge_1_3 = (1,3,4);
        (int?, int?, int) edge_2_3 = (2,3,2);
        (int?, int?, int) edge_2_4 = (2,4,6);
        (int?, int?, int) edge_3_4 = (3,4,3);
        
        // Act
        Edge g_edge_1_2 = graph._edges["1T2H"];
        Edge g_edge_1_3 = graph._edges["1T3H"];
        Edge g_edge_2_3 = graph._edges["2T3H"];
        Edge g_edge_2_4 = graph._edges["2T4H"];
        Edge g_edge_3_4 = graph._edges["3T4H"];
        
        // Assert
        
        TestEdgeValues(edge_1_2, g_edge_1_2);
        TestEdgeValues(edge_1_3, g_edge_1_3);
        TestEdgeValues(edge_2_3, g_edge_2_3);
        TestEdgeValues(edge_2_4, g_edge_2_4);
        TestEdgeValues(edge_3_4, g_edge_3_4);

        void TestEdgeValues((int?, int?, int) edge, Edge g_edge)
        {
           Assert.Equal(edge, (g_edge._tail._number, g_edge._head._number, g_edge._length));
        }
       
        // Memory Manipulation
        // Arrange
        int OrigIntHolderLength_1 = graph._vertices[1]._intHolder;
        int newIntHolder = 777;

        int origIntHolder_2 = graph._vertices[2]._intHolder;
        int newIntHolder2 = 222;

        // Act
        // Manipulating from the tail
        graph._edges["1T2H"]._tail._intHolder = newIntHolder;
    
        // Manipulating from the head
        graph._edges["1T2H"]._head._intHolder = newIntHolder2;

        // Assert
        // Manipulating from tail
        Assert.Equal(newIntHolder, graph._vertices[1]._intHolder);

        // Manipulating from head
        Assert.Equal(newIntHolder2, graph._vertices[2]._intHolder);

        // Check if a manipulation from head will change the tail perspective...
        // the head of one edge is equal to the tail of another
        Assert.Equal(newIntHolder2, graph._edges["2T3H"]._tail._intHolder);
    }
}