using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPracticeGraph
{
    /*
     * Contains extension methods for the Graph class
     */
    public class GraphExtensions<V> : Graph<V, int>
        where V : IComparable, IEquatable<V>
    {
        /*
         * Returns a list of vertices between src and dst (including src and dst) that
         * is the shortest path in the graph from src and dst.
         * 
         * Returns an empty list in the case of src == dst.
         * 
         * Returns null if dst is unreachable from src.
         */
        public List<V> ShortestPathBetween(V src, V dst)
        {
            Console.WriteLine();
            Console.WriteLine();

            CheckVertexExists(src);
            CheckVertexExists(dst);

            if (src.Equals(dst))
            {
                // Empty list
                return new List<V>();
            }

            // Comparing based on edge distance
            SortedList<V,int> pq = new SortedList<V,int>();
            HashSet<V> seen = new HashSet<V>(); // seen array
            // Keep track of how we reached any given node
            Dictionary<V, V> prev = new Dictionary<V, V>();

            pq.Add(src,0);

            // We do Dijkstra.
            // While still items in the queue
            while (pq.Count != 0)
            {
                Console.WriteLine("----");
                foreach (KeyValuePair<V,int> s in pq)
                {
                    Console.Write(s.Key);
                    Console.Write(" ");
                    Console.WriteLine(s.Value);
                }
                Console.WriteLine("----");
                V curr = pq.ElementAt(0).Key;

                if (curr.Equals(dst))
                {
                    // We're done, retrieve all previous nodes
                    V p = prev[curr]; // Guaranteed not to be src
                    List<V> toRet = new List<V>();
                    toRet.Add(dst);
                    toRet.Add(p);

                    while (!p.Equals(src))
                    {
                        p = prev[p];
                        toRet.Add(p);
                    }

                    toRet.Reverse();
                    // toRet should now contain [src, ..., dst]
                    return toRet;
                }

                foreach (V neighbour in this.NeighboursOf(curr))
                {
                    // if not seen yet, or have better distance

                    if ((!seen.Contains(neighbour)) &&
                        (!pq.ContainsKey(neighbour) ||
                            // Current dist
                            pq[neighbour] <
                              // smallest dist from curr to neighbour 
                              (this.connections[curr].WeightsBetween(neighbour).Min()
                              // distance from src to curr
                              + pq[curr]))
                            )
                    {

                        // We now have a new best, lets put it in the queue
                        pq[neighbour] = this.connections[curr].WeightsBetween(neighbour).Min() + pq[curr];

                        // Show that it came from the current node
                        prev[neighbour] = curr;
                    }
                }
                pq.Remove(curr);
                seen.Add(curr);
            }

            return null;
        }

        public bool ExistsCycleFrom(V v1)
        {
            throw new NotImplementedException();

            return false;
        }

        public Graph<V, int> MinimumSpanningTree()
        {
            throw new NotImplementedException();

            return new Graph<V, int>();
        }
    }
}
