using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPracticeGraph
{
    /*
     * Apparently, C# doesn't have priority queues in the stdlib...
     * So here's my own!
     */
    public class PriorityQueue<T, E>
        where E : IComparable
    {
        private List<Tuple<T, E>> elements;

        public int Count {
            get { return this.elements.Count; }
        }

        public PriorityQueue() {
            this.elements = new List<Tuple<T,E>>(); // Start here, dynamically resize
        }

        public void Enqueue(T elem, E weight) {
            int lo = 0, hi = this.elements.Count;

            while (lo < hi) {
                int mid = (lo + hi)/2;
                if (this.elements.ElementAt(mid).Item2.CompareTo(weight) < 0)
                {
                    lo = mid + 1;
                }
                else if (this.elements.ElementAt(mid).Item2.CompareTo(weight) > 0)
                {
                    hi = mid;
                }
                else {
                    lo = mid;
                    hi = mid;
                }
            }

            // This isn't really a good PriorityQueue because 
            // insertion here is O(n), but it'll have to do for now :)
            this.elements.Insert(lo, Tuple.Create(elem,weight));
        }

        public T Dequeue() {
            if (this.elements.Count == 0)
                throw new InvalidOperationException();


            Tuple<T,E> toRet = this.elements.ElementAt(0);
            this.elements.RemoveAt(0);
            return toRet.Item1;
        }
    }
}
