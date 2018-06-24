using System;
using System.Collections.Generic;


namespace CSPracticeGraph
{
    /*
     * Node class
     * Stores a map of connections between vertices
     */
    public class Node <V,E> where V : IComparable
                            where E : IComparable 
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
                this.connections.TryGetValue(pair, out conns);
                conns.Add(weight);
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
         * Throws an exception if the connection is not found!
         */
        public List<E> WeightsBetween(V pair)
        {
            List<E> toRet;
            if (this.connections.TryGetValue(pair, out toRet))
            {
                return toRet;
            }

            // Not found - Exception
            throw new ArgumentException("Connection not found");
        }

        public bool ConnectedTo(V pair) {
            return this.connections.ContainsKey(pair);
        }
    }
}
