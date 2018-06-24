using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPractice
{
    class Graph <E,V> where E : IComparable
                      where V : IComparable
    {
        /*
         * Node class
         * Stores a map of connections between vertices
         */
        private class Node {
            private Dictionary<V, E> connections;
            private V vertex;

            /*
             * Adds an edge to the current node.
             * Returns true if the edge was corrected, false if it already existed.
             */
            public bool AddEdge(V pair, E weight) {
                try {
                    this.connections.Add(pair, weight);
                } catch (ArgumentException) {
                    // Edge already existed
                    return false;
                }
                return true;
            }

            /* 
             * Deletes an connection from this ndoe
             * Returns true if connection was found and removed!
             */
            public bool RemoveEdge(V pair) {
                return this.connections.Remove(pair);
            }

            /* 
             * Deletes an connection from this ndoe
             * Returns true if connection was found and removed!
             */
            public bool UpdateEdge(V pair, E weight)
            {
                // Will remove Edge if it exists, and returns false if it doesn't exist already
                if (!this.connections.Remove(pair)) return false;
                // Will return true at this point
                return this.AddEdge(pair, weight);
            }

            /*
             * Returns the weight between this vertex and another vertex.
             * Throws an exception if the connection is not found!
             */
            public E WeightBetween(V pair) {
                E toRet;
                if (this.connections.TryGetValue(pair, out toRet)) {
                    return toRet;
                }
                // Not found - Exception
                throw new ArgumentException("Connection not found");
            }
            
            /* Constructor */
            public Node(V vertex) {
                this.vertex = vertex;
                this.connections = new Dictionary<V, E>();
            }
        }
    }
}
