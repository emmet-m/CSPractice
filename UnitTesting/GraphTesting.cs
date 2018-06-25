using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSPracticeGraph;

namespace UnitTesting
{
    [TestClass]
    public class GraphTesting
    {
        [TestMethod]
        public void TestVertices()
        {
            // New graph
            Graph<int, int> g = new Graph<int, int>();

            // The graph is empty
            Assert.IsTrue(g.Vertices().Count == 0);

            // Test simple vertex
            g.AddVertex(0);
            Assert.IsTrue(g.ContainsVertex(0));

            // We have 1 vertex
            Assert.IsTrue(g.Vertices().Count == 1);

            // Let's add some more
            g.AddVertex(1);
            g.AddVertex(2);
            g.AddVertex(3);

            // Our graph should contain 4 verices
            Assert.IsTrue(g.Vertices().Count == 4);

            // This should do nothing
            g.AddVertex(0);
            Assert.IsTrue(g.Vertices().Count == 4);

            Assert.IsTrue(g.ContainsVertex(0));
            Assert.IsTrue(g.ContainsVertex(1));
            Assert.IsTrue(g.ContainsVertex(2));
            Assert.IsTrue(g.ContainsVertex(3));

            // Let's remove some vertexes
            g.RemoveVertex(0);

            // 0 shouldn't be in the graph anymore
            Assert.IsFalse(g.ContainsVertex(0));

            // These should still be here!
            Assert.IsTrue(g.ContainsVertex(1));
            Assert.IsTrue(g.ContainsVertex(2));
            Assert.IsTrue(g.ContainsVertex(3));

            // Size has changed
            Assert.IsTrue(g.Vertices().Count == 3);

            // Remove some more
            g.RemoveVertex(1);
            Assert.IsTrue(g.Vertices().Count == 2);
            g.RemoveVertex(2);
            Assert.IsTrue(g.Vertices().Count == 1);

            // Should not be in graph
            Assert.IsFalse(g.ContainsVertex(0));
            Assert.IsFalse(g.ContainsVertex(1));
            Assert.IsFalse(g.ContainsVertex(2));

            // Should still be there
            Assert.IsTrue(g.ContainsVertex(3));

            // Removing this should cause an exception
            bool b = false;
            try
            {
                g.RemoveVertex(0);
            }
            catch (ArgumentException) {
                b = true;
            }

            Assert.IsTrue(b);

            b = false;
            try
            {
                g.RemoveVertex(1);
            }
            catch (ArgumentException) {
                b = true;
            }
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void TestEdges() {
            // New graph
            Graph<int, int> g = new Graph<int, int>();

            // Insert an edge between 2 non-existant vertices
            bool b = false;
            try
            {
                g.AddEdge(0, 1, 100); // Will fail
            }
            catch (ArgumentException) {
                b = true;
            }
            Assert.IsTrue(b);

            // Make some vertices
            g.AddVertex(0);
            g.AddVertex(1);
            g.AddVertex(2);
            g.AddVertex(3);

            // Some more failure checking
            b = false;
            try
            {
                g.AddEdge(0, 4, 100); // Will fail
            }
            catch (ArgumentException) {
                b = true;
            }
            Assert.IsTrue(b);

            b = false;
            try
            {
                g.AddEdge(4, 0, 100); // Will fail
            }
            catch (ArgumentException) {
                b = true;
            }
            Assert.IsTrue(b);

            // Alrigt, now let's add some edges
            g.AddEdge(0, 1, 100);
            Assert.IsTrue(g.IsNeighbourOf(0, 1));
            Assert.IsTrue(g.IsNeighbourOf(1, 0));
            Assert.AreEqual(g.WeightsBetween(0, 1).Count, 1);
            Assert.AreEqual(g.WeightsBetween(0, 1)[0], 100);

            g.AddEdge(1, 2, 100);
            Assert.IsTrue(g.IsNeighbourOf(1, 2));
            Assert.IsTrue(g.IsNeighbourOf(2, 1));
            Assert.AreEqual(g.WeightsBetween(1, 2).Count, 1);
            Assert.AreEqual(g.WeightsBetween(1, 2)[0], 100);

            // Add 'backwards' edge
            g.AddEdge(2, 1, 200);
            Assert.IsTrue(g.IsNeighbourOf(1, 2));
            Assert.IsTrue(g.IsNeighbourOf(2, 1));
            Assert.AreEqual(g.WeightsBetween(1, 2).Count, 2);
            Assert.AreEqual(g.WeightsBetween(2, 1).Count, 2);
            Assert.AreEqual(g.WeightsBetween(1, 2)[1], 200);
            Assert.AreEqual(g.WeightsBetween(2, 1)[0], 100);

            // Nothing should be connected to 3
            Assert.IsFalse(g.IsNeighbourOf(0, 3));
            Assert.IsFalse(g.IsNeighbourOf(1, 3));
            Assert.IsFalse(g.IsNeighbourOf(2, 3));

            // Check number of neighbours is correct
            Assert.AreEqual(g.NeighboursOf(3).Count, 0);
            Assert.AreEqual(g.NeighboursOf(0).Count, 1); // 1
            Assert.AreEqual(g.NeighboursOf(1).Count, 2); // 1, 2
            Assert.AreEqual(g.NeighboursOf(2).Count, 1); // 1

            Assert.AreEqual(1, g.WeightsBetween(0, 1).Count);
            Assert.AreEqual(2, g.WeightsBetween(1, 2).Count);
            Assert.AreEqual(2, g.WeightsBetween(2, 1).Count);

            // Nobody should be connected to 3
            foreach (int i in g.Vertices()) {
                Assert.AreEqual(0, g.WeightsBetween(i, 3).Count);
            }

            g.RemoveEdge(0, 1, 100);
            // No longer should be neighbours after removing their only edge
            Assert.IsFalse(g.IsNeighbourOf(0, 1));

            // Remove a vertex
            g.RemoveVertex(1);
            foreach (int i in g.Vertices()) {
                // There should be no connections now
                Assert.AreEqual(0, g.NeighboursOf(i).Count);
            }
        }
    }
}