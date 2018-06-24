using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPracticeGraph
{
    /*
     * An undirected graph class
     * 
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
            // Check if already exists
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
            // Check if exists
            if (!this.connections.ContainsKey(vertex))
                throw new ArgumentException("Vertex to remove does not exist in graph");

            Node<V,E> n = this.connections[vertex];
            foreach (V conn in n.Connections()) {
                // Remove edges between connection and vertex
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
            // Check both verteces exist
            if (!(this.connections.ContainsKey(v1) && this.connections.ContainsKey(v2))) {
                throw new ArgumentException("Graph does not contain both verteces");
            }

            // We know both exist at this point
            this.connections[v1].AddEdge(v2, weight);
            this.connections[v2].AddEdge(v1, weight);
        }

        /*
         * Removes the first edge of weight `weight` between v1 and v2.
         */
        public void RemoveEdge(V v1, V v2, E weight) {
            try
            {
                this.connections[v1].RemoveEdge(v2, weight);
                this.connections[v2].RemoveEdge(v1, weight);
            }
            catch (KeyNotFoundException)
            {
                // Mistakes were made
                throw new ArgumentException("Graph does not contain both verteces");
            }
        }

        /*
         * Returns true if v1 is connected to v2, false otherwise.
         * Throws an ArgumentException if either of the verteces doesn't exist.
         */ 
        public bool ConnectedTo(V v1, V v2) {
            // Check both verteces exist
            if (!(this.connections.ContainsKey(v1) && this.connections.ContainsKey(v2))) {
                throw new ArgumentException("Graph does not contain both verteces");
            }

            Node<V, E> check = this.connections[v1];
            return check.ConnectedTo(v2);
        }
    }
}
