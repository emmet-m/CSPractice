using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPracticeGraph
{
    /*
     * An undirected graph class.
     *
     * Any deletions/queries on edges/vertices require the vertex to exist
     * in the graph, otherwise an ArgumentException is thrown.
     *
     * Vertices and weights can be any Comparable type. 
     */
    class Graph<V, E> where E : IComparable
                      where V : IComparable
    {
        /*
         * Dictionary that maps a vertex to it's corresponding Node container 
         */
        private Dictionary<V,Node<V, E>> connections;

        public Graph() {
            this.connections = new Dictionary<V, Node<V, E>>();
        }

        /*
         * Adds a vertex to the graph
         * Nothing happens if vertex exists in graph.
         */
        public void AddVertex(V vertex) {
            // Check if already exists, ignore
            if (this.connections.ContainsKey(vertex)) return;

            // Insert node
            Node<V, E> n = new Node<V, E>(vertex);
            this.connections.Add(vertex,n);
        }

        /*
         * Deletes a vertex, and all it's associated connections.
         * Throws an ArgumentException if the vertex doesn't exist.
         */
        public void RemoveVertex(V vertex) {
            CheckVertexExists(vertex);

            Node<V,E> n = this.connections[vertex];
            foreach (V conn in n.Connections()) {
                // Remove edges between connection and vertex, both ways
                this.connections[conn].RemoveAllEdges(vertex);
                n.RemoveAllEdges(conn);
            }
        }

        /*
         * Adds an edge to the graph, between verteces v1 and v2
         * 
         * Will throw ArgumentException if either of the verteces don't exist
         */
        public void AddEdge(V v1, V v2, E weight) {
            CheckVertexExists(v1);
            CheckVertexExists(v2);

            // We know both exist at this point
            this.connections[v1].AddEdge(v2, weight);
            this.connections[v2].AddEdge(v1, weight);
        }

        /*
         * Removes the first edge of weight `weight` between v1 and v2.
         */
        public void RemoveEdge(V v1, V v2, E weight) {
            CheckVertexExists(v1);
            CheckVertexExists(v2);

            this.connections[v1].RemoveEdge(v2, weight);
            this.connections[v2].RemoveEdge(v1, weight);
        }

        /*
         * Returns true if v1 is connected to v2, false otherwise.
         * Throws an ArgumentException if either of the verteces doesn't exist.
         */
        public bool IsConnectedTo(V v1, V v2) {
            CheckVertexExists(v1);
            CheckVertexExists(v2);

            Node<V, E> check = this.connections[v1];
            return check.ConnectedTo(v2);
        }

        /*
         * Returns a list of weights between vertex v1 and v2. 
         * Throws an ArgumentException if the vertices are not connected.
         */
        public List<E> WeightsBetween(V v1, V v2) {
            CheckVertexExists(v1);
            CheckVertexExists(v2);

            return this.connections[v1].WeightsBetween(v2);
        }

        /*
         * Returns a list of all vertices connected to the given vertex. 
         * Throws ArgumentException if the given vertex doesn't exist in the graph.
         */
        public List<V> ConnectionsOf(V vertex) {
            CheckVertexExists(vertex);
            return this.connections[vertex].Connections();
        }

        /*
         * Returns true if the graph contains the given vertex,
         * false otherwise.
         */ 
        public bool ContainsVertex(V vertex) {
            try
            {
                CheckVertexExists(vertex);
                return true;
            }
            catch (ArgumentException) {
                return false;
            }
        }

        /*
         * Returns a list of all vertices in the graph. 
         */
        public List<V> AllVertices() {
            return new List<V>(this.connections.Keys);
        }


        /* ===== Bookkeeping functions, helpers ====== */

        /*
         * Throws an exception if vertex doesn't exist in our graph. 
         */
        private void CheckVertexExists(V vertex) {
            // Check both verteces exist
            if (!(this.connections.ContainsKey(vertex)))
                throw new ArgumentException("Graph does not contain vertex " + vertex.ToString());
        }
    }
}
