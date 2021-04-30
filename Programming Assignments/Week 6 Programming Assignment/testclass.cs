using Xunit;
using System.Collections.Generic;
using Week_6_Programming_Assignment;

public class testclass
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
    public void MakingGraphTest()
    {
        // Arrange
        Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 6 Programming Assignment\Unit Test Data.txt");

        // Vertices
        // Act
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
        Edge g_edge_1_2 = graph._edges["12"];
        Edge g_edge_1_3 = graph._edges["13"];
        Edge g_edge_2_3 = graph._edges["23"];
        Edge g_edge_2_4 = graph._edges["24"];
        Edge g_edge_3_4 = graph._edges["34"];
        
        // Assert
        TestEdgeValues(edge_1_2, g_edge_1_2);
        TestEdgeValues(edge_1_3, g_edge_1_3);
        TestEdgeValues(edge_2_3, g_edge_2_3);
        TestEdgeValues(edge_2_4, g_edge_2_4);
        TestEdgeValues(edge_3_4, g_edge_3_4);
       
        // Memory Manipulation
        // Arrange
        int OrigdijkstraLength_1 = graph._vertices[1]._dijkstraLength;
        int newdijkstraLength = 777;

        int origDijkstraLength_2 = graph._vertices[2]._dijkstraLength;
        int newdijkstraLength2 = 222;

        // Act
        // Manipulating from the tail
        graph._edges["12"]._tail._dijkstraLength = newdijkstraLength;
    
        // Manipulating from the head
        graph._edges["12"]._head._dijkstraLength = newdijkstraLength2;

        // Assert
        // Manipulating from tail
        Assert.Equal(newdijkstraLength, graph._vertices[1]._dijkstraLength);

        // Manipulating from head
        Assert.Equal(newdijkstraLength2, graph._vertices[2]._dijkstraLength);

        // Check if a manipulation from head will change the tail perspective...
        // the head of one edge is equal to the tail of another
        Assert.Equal(newdijkstraLength2, graph._edges["23"]._tail._dijkstraLength);

        void TestEdgeValues((int?, int?, int) edge, Edge g_edge)
        {
           Assert.Equal(edge, (g_edge._tail._number, g_edge._head._number, g_edge._length));
        } 
    }

    [Fact]
    public void DijkstraTest()
    {
        // Check the links of vertices
        // Arrange----------------------------------------------------------------------------
        Dijkstra dijkstra = new Dijkstra();
        Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 6 Programming Assignment\Unit Test Data.txt");
        SortedDictionary<int?, List<Vertex>> myPaths = dijkstra.Apply(graph, graph._vertices[1]);
        int?[] vNum = new int?[4];
        int?[] testNum = new int?[4];
   
        // Assert
        Assert.Equal(null, graph._vertices[1]._link);
        Assert.Equal(1, graph._vertices[2]._link._number);
        Assert.Equal(2, graph._vertices[3]._link._number);
        Assert.Equal(3, graph._vertices[4]._link._number);

        //-Making Paths Dictionary---------------------------------------------------
        // Arrange
        int?[] truePathsLength = new int?[4];
        int?[] truePath_1 = {null};
        int?[] truePath_2 = {1,2};
        int?[] truePath_3 = {1,2,3};
        int?[] truePath_4 = {1,2,3,4};

        
        // Assert
        Assert.Equal(truePathsLength.Length, dijkstra._myPath.Count);

        Assert.Equal(0, graph._vertices[1]._dijkstraLength);
        Assert.Equal(null, dijkstra._myPath[1][0]._link);

        Assert.Equal(1, graph._vertices[2]._dijkstraLength);
        for(int i=0; i<2; i++)
        {
            Assert.Equal(truePath_2[i], myPaths[2][i]._number);
        }

        Assert.Equal(3, graph._vertices[3]._dijkstraLength);
        for(int i=0; i<3; i++)
        {
            Assert.Equal(truePath_3[i], myPaths[3][i]._number);
        }

        Assert.Equal(6, graph._vertices[4]._dijkstraLength);
        for(int i=0; i<4; i++)
        {
            Assert.Equal(truePath_4[i], myPaths[4][i]._number);
        }
    }

    [Fact]
    public void TestCase1()
    {
        // Arrange
        Dijkstra dijkstra = new Dijkstra();
        Graph graph = new Graph(@"C:\Users\Paul\Documents\Open Source Society for Computer Science (OSSU)\Algorithms Coursera\Programming Assignments\Week 6 Programming Assignment\Test Case.txt");
        SortedDictionary<int?, List<Vertex>> myPaths = dijkstra.Apply(graph, graph._vertices[1]);

        int?[] truePathsLength = new int?[8];
        int?[] truePath_1 = {null};
        int?[] truePath_2 = {1,2};
        int?[] truePath_3 = {1,2,3};
        int?[] truePath_4 = {1,2,3,4};
        int?[] truePath_5 = {1,2,3,4,5};
        int?[] truePath_6 = {1,8,7,6};
        int?[] truePath_7 = {1,8,7};
        int?[] truePath_8 = {1,8};

        Assert.Equal(truePathsLength.Length, dijkstra._myPath.Count);

        Assert.Equal(0, graph._vertices[1]._dijkstraLength);
        Assert.Equal(null, dijkstra._myPath[1][0]._link);

        Assert.Equal(1, graph._vertices[2]._dijkstraLength);
        for(int i=0; i<2; i++)
        {
            Assert.Equal(truePath_2[i], myPaths[2][i]._number);
        }

        Assert.Equal(2, graph._vertices[3]._dijkstraLength);
        for(int i=0; i<3; i++)
        {
            Assert.Equal(truePath_3[i], myPaths[3][i]._number);
        }

        Assert.Equal(3, graph._vertices[4]._dijkstraLength);
        for(int i=0; i<4; i++)
        {
            Assert.Equal(truePath_4[i], myPaths[4][i]._number);
        }

        Assert.Equal(4, graph._vertices[5]._dijkstraLength);
        for(int i=0; i<5; i++)
        {
            Assert.Equal(truePath_5[i], myPaths[5][i]._number);
        }

        Assert.Equal(4, graph._vertices[6]._dijkstraLength);
        for(int i=0; i<4; i++)
        {
            Assert.Equal(truePath_6[i], myPaths[6][i]._number);
        }

        Assert.Equal(3, graph._vertices[7]._dijkstraLength);
        for(int i=0; i<3; i++)
        {
            Assert.Equal(truePath_7[i], myPaths[7][i]._number);
        }

        Assert.Equal(2, graph._vertices[8]._dijkstraLength);
        for(int i=0; i<2; i++)
        {
            Assert.Equal(truePath_8[i], myPaths[8][i]._number);
        }
    }
}