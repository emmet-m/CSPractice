using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using CSPracticeGraph;

namespace UnitTesting
{
    [TestClass]
    public class GraphExtensionsTesting
    {
        [TestMethod]
        public void TestDijkstraSimple()
        {
            GraphExtensions<String> g = new GraphExtensions<string>();
            // Add  towns
            g.AddVertex("Sydney");
            g.AddVertex("Melbourne");
            g.AddVertex("Adelaide");

            // Draw a straight line between them
            g.AddEdge("Sydney", "Melbourne", 100);
            g.AddEdge("Melbourne", "Adelaide", 100);

            /*
             *  We now have:
             *  Sydney <-> Melbourne <-> Adelaide
             */
            List<String> l = g.ShortestPathBetween("Sydney", "Adelaide");
            Assert.AreEqual("Sydney", l[0]);
            Assert.AreEqual("Melbourne", l[1]);
            Assert.AreEqual("Adelaide", l[2]);
            Assert.AreEqual(3, l.Count);

            // Adjacent vertices
            l = g.ShortestPathBetween("Sydney", "Melbourne");
            Assert.AreEqual("Sydney", l[0]);
            Assert.AreEqual("Melbourne", l[1]);
            Assert.AreEqual(2, l.Count);

            // From self to self
            l = g.ShortestPathBetween("Sydney", "Sydney");
            Assert.AreEqual(0, l.Count);

            // Add a new unconnected vertex            
            g.AddVertex("Rome");
            // Not all roads lead to Rome!
            l = g.ShortestPathBetween("Sydney", "Rome");
            Assert.IsNull(l);

            // TODO remove
            g.AddVertex("Helsinki");
            g.AddEdge("Sydney", "Rome", 400);
            g.AddEdge("Sydney", "Helsinki", 500);

            // Make a heavier but direct connection
            g.AddEdge("Sydney", "Adelaide", 300);
            // The following from earlier should still hold
            Console.WriteLine("Here they come:");
            l = g.ShortestPathBetween("Sydney", "Adelaide");

            /*
            foreach (string i in l) {
                Console.WriteLine(i);
            }*/

            Assert.AreEqual("Sydney", l[0]);
            Assert.AreEqual("Melbourne", l[1]);
            Assert.AreEqual("Adelaide", l[2]);
            Assert.AreEqual(3, l.Count);
        }

        [TestMethod]
        public void TestCycleDetector()
        {
            GraphExtensions<String> g = new GraphExtensions<string>();

            // We want to construct a triangle           
            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");

            g.AddEdge("A", "B", 100);
            // At this point, we should definitely have no cycle!
            Assert.IsFalse(g.ExistsCycleFrom("A"));
            Assert.IsFalse(g.ExistsCycleFrom("B"));
            Assert.IsFalse(g.ExistsCycleFrom("C"));

            // Should still not have a cycle
            g.AddEdge("B", "C", 100);
            Assert.IsFalse(g.ExistsCycleFrom("A"));
            Assert.IsFalse(g.ExistsCycleFrom("B"));
            Assert.IsFalse(g.ExistsCycleFrom("C"));

            // Let's fully connect our trinagle
            g.AddEdge("C", "A", 100);
            Assert.IsTrue(g.ExistsCycleFrom("A"));
            Assert.IsTrue(g.ExistsCycleFrom("B"));
            Assert.IsTrue(g.ExistsCycleFrom("C"));

            // Let's add a branch from our triangle
            g.AddVertex("D");
            g.AddEdge("D", "A", 100);

            // These should still be true
            Assert.IsTrue(g.ExistsCycleFrom("A"));
            Assert.IsTrue(g.ExistsCycleFrom("B"));
            Assert.IsTrue(g.ExistsCycleFrom("C"));
            // this is a branch, so no cycle from D exists
            Assert.IsFalse(g.ExistsCycleFrom("D"));

        }
    }
}
