using System;
using System.Collections.Generic;

namespace GraphLibrary
{
    public class Graph
    {
        public Dictionary<string, Edge> _edges = new Dictionary<string, Edge>();
        public Dictionary<int?, Vertex> _vertices = new Dictionary<int?, Vertex>();
    
        public Graph(Dictionary<int, List<(int?,int)>> adjGraph)
        {
            // Populate the list of _vertices (assumed that a vertex with no outgoing edge is here)        
            foreach(KeyValuePair<int, List<(int?,int)>> tree in adjGraph)
            {
                if(!_vertices.ContainsKey(tree.Key)) 
                    _vertices.Add(tree.Key, new Vertex(tree.Key));
            }

            // Populate the list of Edges/
            foreach(KeyValuePair<int, List<(int?,int)>> tree in adjGraph)
            {
                if(tree.Value[0].Item1 != null)
                {
                    Vertex tail = _vertices[tree.Key];
                    foreach((int?, int) edge in tree.Value)
                    {
                        Vertex head = _vertices[edge.Item1];
                        int cost = edge.Item2;
                        string key = tail._number.ToString() + "T" + head._number.ToString() + "H";

                        if(_edges.ContainsKey(key))
                        {
                            // !!! Handle Error
                        }
                        else
                        {
                            _edges.Add(key, new Edge(tail, head, cost));
                        }
                    }
                }
            }
        }
    }

    public class Vertex
    {
        public int? _number;
        public int _intHolder = 1000000;
        public Vertex _link = null;
        
        public Vertex(int? number)
        {
            _number = number;
        }
    }

    public class Edge
    {
        public Vertex _tail, _head;
        public int _length;
        public Edge(Vertex tail, Vertex head, int length)
        {
            _tail = tail;
            _head = head;
            _length = length;
        }
    }
}
