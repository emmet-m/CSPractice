using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPracticeGraph
{
    /*
     * Contains extension methods for the Graph class.
     * 
     * I used int as the default second parameter here as I needed
     * to be able to do arithmetic on weights for the Dijkstra to work.
     * 
     * C# doesn't have a 'Num' kind (like Haskell does), nor a kind of 
     * arithmetic supporting types, so I just had to pick one!
     * 
     * TODO(?): Implement num class (like a C union)? 
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
                // Empty list, we're here already
                return new List<V>();
            }

            // Comparing based on edge distance
            PriorityQueue<V,int> pq = new PriorityQueue<V,int>();
            HashSet<V> seen = new HashSet<V>(); // seen array
            // Keep track of how we reached any given node
            Dictionary<V, V> prev = new Dictionary<V, V>();

            pq.Enqueue(src,0);

            // We do Dijkstra.
            // While still items in the queue
            while (pq.Count != 0)
            {
                // Get first element in queue and it's best weight from src
                V curr = pq.Peek();
                int currWeight = pq.WeightOf(curr);
                pq.Dequeue();

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

                    // Can have an edge to yourself, so skip if we see ourself
                    if (neighbour.Equals(curr)) continue;
                    // smallest dist from curr to neighbour + 
                    // distance from src to curr
                    int currToNeighbourWeight = this.connections[curr].WeightsBetween(neighbour).Min();

                    if ((!seen.Contains(neighbour)) && 
                        ((!pq.Contains(neighbour)) || pq.WeightOf(neighbour) > (currToNeighbourWeight + currWeight)))
                    {

                        // We now have a new best, lets put it in the queue
                        if (pq.Contains(neighbour))
                        {
                            pq.Update(neighbour, this.connections[curr].WeightsBetween(neighbour).Min() + currWeight);
                        }
                        else {
                            pq.Enqueue(neighbour, this.connections[curr].WeightsBetween(neighbour).Min() + currWeight);
                        }
                        // Show that it came from the current node
                        prev[neighbour] = curr;
                    }
                }
                seen.Add(curr);
            }

            return null;
        }

        /*
         * Detects if there is a cycle from src.
         * 
         * Throws ArgumentException if src is not in graph.
         */ 
        public bool ExistsCycleFrom(V src)
        {
            CheckVertexExists(src);

            // Depth first seach
            Stack<V> stack = new Stack<V>();
            // To track where we came from
            Dictionary<V,V> prevOf = new Dictionary<V,V>();
            // So we don't repeat ourselves/infinite loop
            HashSet<V> seen = new HashSet<V>();

            // Initial pushing
            foreach (V i in this.connections[src].Connections()) {
                stack.Push(i);
                prevOf[i] = src;
            }

            seen.Add(src);

            while (stack.Count != 0) {
                V curr = stack.Pop();
                
                foreach (V neighbour in this.connections[curr].Connections()) {
                    // If we arrive at src from a node that we didn't just arrive at from src
                    if (neighbour.Equals(src) && (!prevOf[curr].Equals(src)))
                    {
                        return true; // It means we found a cycle!
                    }

                    // No cycle found, keep going

                    // ignore neighbours we've already visited
                    if (!seen.Contains(neighbour))
                    {
                        stack.Push(neighbour);
                        prevOf[neighbour] = curr;
                    }
                }
                // Mark as seen and continue
                seen.Add(curr);
            }
            
            // Our loop terminated, we didn't find a cycle :(
            return false;
        }

        public Graph<V, int> MinimumSpanningTree()
        {
            throw new NotImplementedException();

            return new Graph<V, int>();
        }
    }
}
