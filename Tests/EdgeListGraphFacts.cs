using System;
using Xunit;
using ProjectEuler.Math;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
  public class EdgeListGraphFacts
  {
    public class AllNodesProperty
    {
      [Fact]
      public void RetrievesAllNodesInTheGraph()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        node1.AddEdgeTo(node1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        EdgeListNode<int> node3 = new EdgeListNode<int>(3);
        node2.AddEdgeTo(node3);
        node2.AddEdgeTo(node2);
        graph.AddNode(node1);
        graph.AddNode(node2);
        List<EdgeListNode<int>> nodes = graph.AllNodes.ToList();
        int count = nodes.Count;
        Assert.Equal(3, count);

        bool has1 = nodes.Contains(node1);
        Assert.True(has1);
        bool has2 = nodes.Contains(node2);
        Assert.True(has2);
        bool has3 = nodes.Contains(node3);
        Assert.True(has3);
      }
    }

    public class AllEdgesProperty
    {
      [Fact]
      public void RetrievesAllEdgesInTheGraph()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        node1.AddEdgeTo(node1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        EdgeListNode<int> node3 = new EdgeListNode<int>(3);
        node2.AddEdgeTo(node3);
        node2.AddEdgeTo(node2);
        graph.AddNode(node1);
        graph.AddNode(node2);
        List<EdgeListNode<int>> nodes = graph.AllNodes.ToList();
        int count = nodes.Count;
        Assert.Equal(3, count);

        bool has1 = nodes.Contains(node1);
        Assert.True(has1);
        bool has2 = nodes.Contains(node2);
        Assert.True(has2);
        bool has3 = nodes.Contains(node3);
        Assert.True(has3);
      }
    }

    public class NodesWithoutIncomingEdgesProperty
    {
      [Fact]
      public void RetrievesCorrectData()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        EdgeListNode<int> node3 = new EdgeListNode<int>(3);
        EdgeListNode<int> node4 = new EdgeListNode<int>(4);
        node1.AddEdgeTo(node1);
        node2.AddEdgeTo(node3);
        graph.AddNode(node1);
        graph.AddNode(node2);
        graph.AddNode(node4);
        List<EdgeListNode<int>> nodes = graph.NodesWithoutIncomingEdges.ToList();
        int count = nodes.Count;
        Assert.Equal(2, count);

        bool has2 = nodes.Contains(node2);
        Assert.True(has2);
        bool has4 = nodes.Contains(node4);
        Assert.True(has4);
      }
    }

    public class AddNodeMethod
    {
      [Fact]
      public void AddsNewNodes()
      {
        //by node
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        EdgeListNode<int> node3 = new EdgeListNode<int>(3);
        node2.AddEdgeTo(node3);
        graph.AddNode(node1);
        graph.AddNode(node2);
        List<EdgeListNode<int>> nodes = graph.Nodes.ToList();
        int count = nodes.Count;
        Assert.Equal(2, count);

        bool has1 = nodes.Contains(node1);
        Assert.True(has1);
        bool has2 = nodes.Contains(node2);
        Assert.True(has2);

        //by value
        graph = new EdgeListGraph<int>();
        node1 = graph.AddNode(1);
        node2 = graph.AddNode(2);
        node2.AddEdgeTo(node3);
        nodes = graph.Nodes.ToList();
        count = nodes.Count;
        Assert.Equal(2, count);

        has1 = nodes.Contains(node1);
        Assert.True(has1);
        has2 = nodes.Contains(node2);
        Assert.True(has2);
      }

      [Fact]
      public void DoesNotAddOldNodes()
      {
        //by node
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        EdgeListNode<int> node3 = new EdgeListNode<int>(3);
        node2.AddEdgeTo(node3);
        graph.AddNode(node1);
        graph.AddNode(node1);
        graph.AddNode(node2);
        graph.AddNode(node2);
        List<EdgeListNode<int>> nodes = graph.Nodes.ToList();
        int count = nodes.Count;
        Assert.Equal(2, count);

        bool has1 = nodes.Contains(node1);
        Assert.True(has1);
        bool has2 = nodes.Contains(node2);
        Assert.True(has2);

        //by value
        graph = new EdgeListGraph<int>();
        node1 = graph.AddNode(1);
        node2 = graph.AddNode(2);
        node2.AddEdgeTo(node3);
        graph.AddNode(1);
        graph.AddNode(1);
        nodes = graph.Nodes.ToList();
        count = nodes.Count;
        Assert.Equal(2, count);

        has1 = nodes.Contains(node1);
        Assert.True(has1);
        has2 = nodes.Contains(node2);
        Assert.True(has2);
      }
    }

    public class FindNodeMethod
    {
      [Fact]
      public void FindsExistingNodes()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        EdgeListNode<int> node3 = new EdgeListNode<int>(3);
        node2.AddEdgeTo(node3);
        graph.AddNode(node1);
        graph.AddNode(node2);
        EdgeListNode<int> result1, result2, result3;

        //by value
        result1 = graph.FindNode(1);
        result2 = graph.FindNode(2);
        result3 = graph.FindNode(3);
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotNull(result3);
        
        //by node
        result1 = graph.FindNode(node1);
        result2 = graph.FindNode(node2);
        result3 = graph.FindNode(node3);
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotNull(result3);

        //by predicate
        result1 = graph.FindNode(node => node.Value < 2);
        result2 = graph.FindNode(node => Math.Pow(node.Value, 2) == 4);
        result3 = graph.FindNode(node => node.Value > 2);
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotNull(result3);
      }

      [Fact]
      public void DoesNotFindNonExistingNodes()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        EdgeListNode<int> node3 = new EdgeListNode<int>(3);
        node2.AddEdgeTo(node3);
        graph.AddNode(node1);
        graph.AddNode(node2);
        EdgeListNode<int> result;

        //by value
        result = graph.FindNode(12);
        Assert.Null(result);
        
        //by node
        result = graph.FindNode(new EdgeListNode<int>(12));
        Assert.Null(result);

        //by predicate
        result = graph.FindNode(node => node.Value >= 12);
        Assert.Null(result);
      }
    }

    public class ContainsNodeMethod
    {
      [Fact]
      public void DetectsExistingNodes()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        EdgeListNode<int> node3 = new EdgeListNode<int>(3);
        node2.AddEdgeTo(node3);
        graph.AddNode(node1);
        graph.AddNode(node2);
        bool result1, result2, result3;

        //by value
        result1 = graph.ContainsNode(1);
        result2 = graph.ContainsNode(2);
        result3 = graph.ContainsNode(3);
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
        
        //by node
        result1 = graph.ContainsNode(node1);
        result2 = graph.ContainsNode(node2);
        result3 = graph.ContainsNode(node3);
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);

        //by predicate
        result1 = graph.ContainsNode(node => node.Value < 2);
        result2 = graph.ContainsNode(node => Math.Pow(node.Value, 2) == 4);
        result3 = graph.ContainsNode(node => node.Value > 2);
        Assert.True(result1);
        Assert.True(result2);
        Assert.True(result3);
      }

      [Fact]
      public void DetectsNonExistingNodes()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        EdgeListNode<int> node3 = new EdgeListNode<int>(3);
        node2.AddEdgeTo(node3);
        graph.AddNode(node1);
        graph.AddNode(node2);
        bool result;

        //by value
        result = graph.ContainsNode(12);
        Assert.False(result);
        
        //by node
        result = graph.ContainsNode(new EdgeListNode<int>(12));
        Assert.False(result);

        //by predicate
        result = graph.ContainsNode(node => node.Value >= 12);
        Assert.False(result);
      }
    }

    public class AddDirectedEdgeMethod
    {
      [Fact]
      public void AddsNewEdges()
      {
        //by value
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        graph.AddNode(1);
        graph.AddNode(2);
        graph.AddDirectedEdge(1, 2);
        int count = graph.AllEdges.Count();
        Assert.Equal(1, count);

        //by node
        graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        graph.AddNode(node1);
        graph.AddNode(node2);
        graph.AddDirectedEdge(node1, node2);
        count = graph.AllEdges.Count();
        Assert.Equal(1, count);
      }

      [Fact]
      public void ThrowsIfTheFromNodeIsNotInTheGraph()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        graph.AddNode(2);
        Assert.Throws<ArgumentException>(() => { graph.AddDirectedEdge(1, 2); });
      }

      [Fact]
      public void ThrowsIfTheToNodeIsNotInTheGraph()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        graph.AddNode(1);
        Assert.Throws<ArgumentException>(() => { graph.AddDirectedEdge(1, 2); });
      }
    }

    public class TopologicalSortMehtod
    {
      [Fact]
      public void SortsCorrectlyGraphNodes()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = graph.AddNode(1);
        EdgeListNode<int> node2 = graph.AddNode(2);
        EdgeListNode<int> node3 = node2.AddEdgeTo(3).To;
        EdgeListNode<int> node4 = node2.AddEdgeTo(4).To;
        EdgeListNode<int> node5 = node4.AddEdgeTo(5).To;
        List<EdgeListNode<int>> result = graph.TopologicalSort().ToList();
        int index1 = result.IndexOf(node1);
        int index2 = result.IndexOf(node2);
        int index3 = result.IndexOf(node3);
        int index4 = result.IndexOf(node4);
        int index5 = result.IndexOf(node5);

        Assert.True(index1 >= 0);
        Assert.True(index2 >= 0);
        Assert.True(index3 >= 0);
        Assert.True(index4 >= 0);
        Assert.True(index5 >= 0);
        Assert.InRange(index1, 0, 1);
        Assert.InRange(index2, 0, 1);
        Assert.True(index1 < index3);
        Assert.True(index1 < index4);
        Assert.True(index2 < index3);
        Assert.True(index2 < index4);
        Assert.True(index3 < index5);
        Assert.True(index4 < index5);
      }

      [Fact]
      public void ThrowsOnCyclicalGraphs()
      {
        EdgeListGraph<int> graph = new EdgeListGraph<int>();
        EdgeListNode<int> node1 = graph.AddNode(1);
        EdgeListNode<int> node2 = graph.AddNode(2);
        node1.AddEdgeTo(node1);
        node2.AddEdgeTo(3);

        Assert.Throws<Exception>(() => { graph.TopologicalSort(); });
      }
    }
  }
}