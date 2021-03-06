﻿using System;
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

            // Should have 2 connected vertices (as we just removed 1)
            Assert.AreEqual(n1.Connections().Count, 2);

            // The connections should be 2 and 3
            Assert.IsFalse(n1.Connections().Contains(1));
            Assert.IsTrue(n1.Connections().Contains(2));
            Assert.IsTrue(n1.Connections().Contains(3));
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

            Assert.AreEqual(0, n1.WeightsBetween(1).Count);
            // Check that we cannot remove the edge between 0 and 1
            Assert.IsFalse(n1.RemoveEdge(1, 100));

        }
    }
}
