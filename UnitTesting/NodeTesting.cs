using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CSPracticeGraph;

namespace UnitTesting
{
    [TestClass]
    public class NodeTesting
    {
        [TestMethod]
        public void TestEdgeConnected()
        {
            // Make a new node, vertex 0
            Node<int, int> n1 = new Node<int, int>(0);
            // Vertex 0 is connected to vertex 1, with a weight of 100
            n1.AddEdge(1, 100);

            Assert.IsTrue(n1.ConnectedTo(1));

            // Vertex 0 is connected to vertex 2, ...
            n1.AddEdge(2, 100);
            n1.AddEdge(3, 100);

            Assert.IsTrue(n1.ConnectedTo(1)); // Still connected
            Assert.IsTrue(n1.ConnectedTo(2)); // Newly connected
            Assert.IsTrue(n1.ConnectedTo(3)); // "             "

            // Should not be connected
            Assert.IsFalse(n1.ConnectedTo(4));

            // Should be able to remove
            Assert.IsTrue(n1.RemoveEdge(1, 100));

            Assert.IsFalse(n1.ConnectedTo(1));
        }


        [TestMethod]
        public void TestWeightsBetween()
        {
            // New vertex 0
            Node<int, int> n1 = new Node<int, int>(0);

            n1.AddEdge(0, 100); // Connected to self
            // Connections to vertex 1
            n1.AddEdge(1, 100);
            n1.AddEdge(1, 100);
            n1.AddEdge(1, 100);

            // Check removing
            Assert.IsTrue(n1.WeightsBetween(1).Count == 3);
            Assert.IsTrue(n1.RemoveEdge(1, 100));
            Assert.IsTrue(n1.WeightsBetween(1).Count == 2);
            Assert.IsTrue(n1.RemoveEdge(1, 100));
            Assert.IsTrue(n1.WeightsBetween(1).Count == 1);
            Assert.IsTrue(n1.RemoveEdge(1, 100));

            // vertex 0 should have no connections to vertex 1
            bool b = false;
            try
            {
                n1.WeightsBetween(1);
            }
            catch (ArgumentException)
            {
                // This should be executed
                b = true;
            }

            // Check that weight beteen 1 didn't exist, and cannot remove
            Assert.IsTrue(b);
            Assert.IsFalse(n1.RemoveEdge(1, 100));

        }
    }
}
