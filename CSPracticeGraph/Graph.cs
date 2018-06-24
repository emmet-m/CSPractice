using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPracticeGraph
{
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
         * Returns true if successfully added
         * Returns false if the vertex exists in the graph
         */
        public bool AddVertex(V vertex) {
            // Check if already exists
            if (this.connections.ContainsKey(vertex)) return false;
            // Insert node
            Node<V, E> n = new Node<V, E>(vertex);
            this.connections.Add(vertex,n);
            return true;
        }

        /*
         * Adds an edge to the graph, between verteces v1 and v2
         * 
         * Will throw ArgumentException if either of the verteces don't exist
         */
        public bool AddEdge(V v1, V v2, E weight) {
            // Check both verteces exist
            if (!(this.connections.ContainsKey(v1) && this.connections.ContainsKey(v2))) {
                throw new ArgumentException("Graph does not contain both verteces");
            }

            // Multiple connections okay! Insert away.
            Node<V, E> toAdd;
            this.connections.TryGetValue(v1, out toAdd);
            toAdd.AddEdge(v2, weight);
            this.connections.TryGetValue(v2, out toAdd);
            toAdd.AddEdge(v1, weight);
            return true;
        }

        /*
         * Returns true if v1 is connected to v2
         */ 
        public bool ConnectedTo(V v1, V v2) {
            Node<V, E> check;
            this.connections.TryGetValue(v1, out check);

            return check.ConnectedTo(v2);
        }
    }
}
