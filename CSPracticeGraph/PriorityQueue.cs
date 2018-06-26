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

        /*
         * Checks if an element exists in the queue
         */ 
        public bool Contains(T key) {
            foreach (Tuple<T,E> t in this.elements) {
                if (t.Item1.Equals(key))
                    return true;
            }
            return false;
        }

        /*
         * Updates the value of a given element in the queue.
         * 
         * Throws an ArgumentException if the provided key doesn't exist.
         */
        public void Update(T key, E value) {
            // Find key
            int where = 0;
            foreach (Tuple<T, E> t in this.elements) {
                if (t.Item1.Equals(key))
                    break;
                where++;
            }
            // We never broke from the loop
            if (where == this.elements.Count)
                throw new ArgumentException("Key " + key.ToString() + " does not exist in PriorityQueue");

            // Put it back in take it out and put it back in!
            this.elements.RemoveAt(where);
            this.Enqueue(key, value);
        }

        /*
         * Get's the associated weight of a key 
         */
        public E WeightOf(T key) {
            foreach (Tuple<T, E> t in this.elements) {
                if (t.Item1.Equals(key))
                    return t.Item2;
            }

            throw new ArgumentException("Key " + key.ToString() + " does not exist in PriorityQueue");
        }

        /*
         * Returns without removing the next element in the queue
         */ 
        public T Peek() {
            return this.elements.ElementAt(0).Item1;
        }

        public void Show() {
            foreach (Tuple<T,E> t in this.elements){
                Console.WriteLine(t.Item1.ToString() + " : " + t.Item2.ToString());
            }
        }
    }
}
