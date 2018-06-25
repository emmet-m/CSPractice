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
            l = g.ShortestPathBetween("Sydney", "Adelaide");
            Assert.AreEqual("Sydney", l[0]);
            Assert.AreEqual("Melbourne", l[1]);
            Assert.AreEqual("Adelaide", l[2]);
            Assert.AreEqual(3, l.Count);
        }
    }
}
