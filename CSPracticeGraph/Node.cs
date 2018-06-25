using System;
using System.Collections.Generic;


namespace CSPracticeGraph
{
    /*
     * Node class
     * Stores a map of connections between a vertex.
     *
     * Using nodes as opposed to bundling pairs in a graph class allows easily to make
     * both an undirected and directed graph using the exact same code, as most graph 
     * queries/operations differ by only a small section of repeated code for directed/undirected.
     *
     * Bundling this information in a node is a nice abstraction! :)
     * 
     * Edges <E> must be comparable, but Vertices need not be.
     *
     */
    public class Node <V,E> where E : IComparable 
    {
        private Dictionary<V, List<E>> connections;
        private V vertex;

        /* Constructor */
        public Node(V vertex)
        {
            this.vertex = vertex;
            this.connections = new Dictionary<V, List<E>>();
        }

        /*
         * Adds an edge between the current node and pair
         */
        public void AddEdge(V pair, E weight)
        {
            List<E> conns;
            try
            {
                // No connections exist, add new list
                conns = new List<E>();
                conns.Add(weight);
                this.connections.Add(pair, conns);
            }
            catch (ArgumentException)
            {
                // Edge already existed, just bopp it on the end of the list
                this.connections[pair].Add(weight);
            }
        }

        /* 
         * Deletes all connections between this node and pair
         * Returns true if connection was found and removed
         */
        public bool RemoveAllEdges(V pair)
        {
            return this.connections.Remove(pair);
        }

        /* 
         * Deletes the first edge with a given weight from this node
         * Returns true if connection was found and removed!
         */
        public bool RemoveEdge(V pair, E weight)
        {
            List<E> conns;

            // Check if exists
            if (!this.connections.TryGetValue(pair, out conns)) {
                return false;
            }
            
            // if doesn't exist
            bool b = conns.Remove(weight);

            // Delete if empty - Not connected
            if (conns.Count == 0) this.connections.Remove(pair);

            return b;
        }


        /*
         * Returns the weight between this vertex and another vertex.
         * Throws ArgumentException if the connection is not found!
         */
        public List<E> WeightsBetween(V pair)
        {
            // Throws Argument
            try
            {
                return this.connections[pair];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("No connection between vertex " + this.vertex.ToString() + "And vertex " + pair.ToString());
            }
        }

        /*
         * Returns a list of Verteces that are connected to this Node.
         */
        public List<V> Connections() {
            return new List<V>(this.connections.Keys);
        }

        /*
         * Returns true  if the node is connected to the given vertex.
         * false otherwise.
         */
        public bool ConnectedTo(V pair) {
            return this.connections.ContainsKey(pair);
        }
    }
}
