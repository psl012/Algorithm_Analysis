using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphLibrary;
using System.Collections.Generic;

namespace GraphLibraryTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTest()
        {
            Assert.AreEqual(0,0);
        }

        [TestMethod]
        public void MakingVertexTest()
        {
            // Arrange
            Vertex vertex = new Vertex(4);

            // Act
            int? number = vertex._number;

            // Assert
            Assert.AreEqual(4, number); 
        }

        [TestMethod]
        public void MakingEdgeTest()
        {
            // Arrange
            Vertex tail = new Vertex(2);
            Vertex head = new Vertex(10);
            Edge edge = new Edge(tail, head, 5);

            // Assert
            Assert.AreEqual(tail, edge._tail);
            Assert.AreEqual(head, edge._head);
            Assert.AreEqual(5, edge._length);
        }

        [TestMethod]
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

            // Act
            Graph myGraph = new Graph(answerForAdjGraph);
            Edge g_edge_1_2 = myGraph._edges["1T2H"];
            Edge g_edge_1_3 = myGraph._edges["1T3H"];
            Edge g_edge_2_3 = myGraph._edges["2T3H"];
            Edge g_edge_2_4 = myGraph._edges["2T4H"];
            Edge g_edge_3_4 = myGraph._edges["3T4H"];

            // Assert
            Assert.AreEqual(1, myGraph._vertices[1]._number);
            Assert.AreEqual(2, myGraph._vertices[2]._number);
            Assert.AreEqual(3, myGraph._vertices[3]._number);
            Assert.AreEqual(4, myGraph._vertices[4]._number);
        
            Assert.AreEqual(1, g_edge_1_2._tail._number);
            Assert.AreEqual(2, g_edge_1_2._head._number);
            
            Assert.AreEqual(1, g_edge_1_3._tail._number);
            Assert.AreEqual(3, g_edge_1_3._head._number);

            Assert.AreEqual(2, g_edge_2_3._tail._number);
            Assert.AreEqual(3, g_edge_2_3._head._number);

            Assert.AreEqual(3, g_edge_3_4._tail._number);
            Assert.AreEqual(4, g_edge_3_4._head._number);
        }

        [TestMethod]
        public void MemoryManipulation_VertexEdge_test()
        {
            // Arrange
            Vertex tail = new Vertex(2);
            Vertex head = new Vertex(10);
            Edge edge = new Edge(tail, head, 5);
            

            // Act
            edge._tail._number = 200;
            edge._head._number = 1000;
            
            // Assert
            Assert.AreEqual(200, tail._number);
            Assert.AreEqual(1000, head._number);

        }

        [TestMethod]
        public void MemoryManipulation_TwoEdge_OneVertex()
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

            // Act
            Graph myGraph = new Graph(answerForAdjGraph);
            Edge g_edge_1_2 = myGraph._edges["1T2H"];
            Edge g_edge_1_3 = myGraph._edges["1T3H"];
            Edge g_edge_2_3 = myGraph._edges["2T3H"];
            Edge g_edge_2_4 = myGraph._edges["2T4H"];
            Edge g_edge_3_4 = myGraph._edges["3T4H"];

            myGraph._edges["1T2H"]._head._number = 200;

            // Assert
            //myGraph._edges["2T3H"]
            Assert.AreEqual(200, myGraph._vertices[2]._number);
            Assert.AreEqual(200, myGraph._edges["2T4H"]._tail._number);
        
        }
    }
}
