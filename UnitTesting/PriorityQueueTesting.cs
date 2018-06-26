using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSPracticeGraph;

namespace UnitTesting
{
    [TestClass]
    public class PriorityQueueTesting
    {
        [TestMethod]
        public void PriorityQueueTest()
        {
            PriorityQueue<String, Int32> pq = new PriorityQueue<String, Int32>();

            Assert.AreEqual(0, pq.Count);
            pq.Enqueue("Zero", 0);
            Assert.AreEqual(1, pq.Count);
            pq.Enqueue("One", 1);
            Assert.AreEqual(2, pq.Count);
            pq.Enqueue("NOne", -1);
            Assert.AreEqual(3, pq.Count);

            Assert.AreEqual("NOne", pq.Dequeue());
            Assert.AreEqual("Zero", pq.Dequeue());
            Assert.AreEqual("One", pq.Dequeue());
        }

        [TestMethod]
        public void PriorityQueueStressTest()
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

            Random r = new Random();

            for (int i = 0; i < 1000; i++) {
                int rand = r.Next();
                // Use weight as key
                pq.Enqueue(rand, rand);
            }

            int prev = 0; // Dummy value to keep compiler happy
            bool first = true;
            while (pq.Count != 0) {
                if (first) {
                    prev = pq.Dequeue();
                    first = false;
                    continue;
                }
                int next = pq.Dequeue();
                // The last number we saw should be less than the current one - as they come out in order
                Assert.IsTrue(prev < next);
                prev = next;
            }
        }

        [TestMethod]
        public void PriorityQueueUpdateAndContains()
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

            // Insert 4 numbers
            pq.Enqueue(0, 4);
            pq.Enqueue(1, 1);
            pq.Enqueue(2, 2);
            pq.Enqueue(3, 3);

            // They should all be in the queue
            Assert.IsTrue(pq.Contains(0));
            Assert.IsTrue(pq.Contains(1));
            Assert.IsTrue(pq.Contains(2));
            Assert.IsTrue(pq.Contains(3));

            // Let's update one of them...
            pq.Update(0, 0);

            // 0 should now be first
            Assert.AreEqual(0, pq.Dequeue());
            // Rest should be in relative order
            Assert.AreEqual(1, pq.Dequeue());
            Assert.AreEqual(2, pq.Dequeue());
            Assert.AreEqual(3, pq.Dequeue());

        }
    }
}
