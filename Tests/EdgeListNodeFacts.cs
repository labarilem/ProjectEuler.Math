using System;
using Xunit;
using ProjectEuler.Math;
using System.Numerics;
using System.Linq;

namespace Tests
{
  public class EdgeListNodeFacts
  {
    public class FindEdgeToMethod
    {
      [Fact]
      public void RetreivesExistingEdges()
      {
        //by value
        EdgeListNode<int> node = new EdgeListNode<int>(1);
        node.AddEdgeTo(2);
        Edge<EdgeListNode<int>> valueResult = node.FindEdgeTo(2);
        Assert.NotNull(valueResult);

        //by node
        node = new EdgeListNode<int>(1);
        EdgeListNode<int> neighbour = new EdgeListNode<int>(2);
        node.AddEdgeTo(neighbour);
        Edge<EdgeListNode<int>> nodeResult = node.FindEdgeTo(2);
        Assert.NotNull(nodeResult);
      }

      [Fact]
      public void DoesNotRetrieveNonExistingEdges()
      {
        //by value
        EdgeListNode<int> node = new EdgeListNode<int>(1);
        node.AddEdgeTo(2);
        bool hasEdgeToValue = node.HasEdgeTo(5);
        Assert.False(hasEdgeToValue);

        //by node
        node = new EdgeListNode<int>(1);
        EdgeListNode<int> neighbour = new EdgeListNode<int>(5);
        bool hasEdgeToNode = node.HasEdgeTo(neighbour);
        Assert.False(hasEdgeToNode);
      }
    }

    public class HasEdgeToMethod
    {
      [Fact]
      public void DetectsPositiveCases()
      {
        //by value
        EdgeListNode<int> node = new EdgeListNode<int>(1);
        node.AddEdgeTo(2);
        bool hasEdgeToValue = node.HasEdgeTo(2);
        Assert.True(hasEdgeToValue);

        //by node
        node = new EdgeListNode<int>(1);
        EdgeListNode<int> neighbour = new EdgeListNode<int>(2);
        node.AddEdgeTo(neighbour);
        bool hasEdgeToNode = node.HasEdgeTo(neighbour);
        Assert.True(hasEdgeToNode);
      }

      [Fact]
      public void DetectsNegativesCases()
      {
        //by value
        EdgeListNode<int> node = new EdgeListNode<int>(1);
        node.AddEdgeTo(2);
        bool hasEdgeToValue = node.HasEdgeTo(5);
        Assert.False(hasEdgeToValue);

        //by node
        node = new EdgeListNode<int>(1);
        EdgeListNode<int> neighbour = new EdgeListNode<int>(5);
        bool hasEdgeToNode = node.HasEdgeTo(neighbour);
        Assert.False(hasEdgeToNode);
      }

    }

    public class AddEdgeToMethod
    {
      [Fact]
      public void AddsANewEdges()
      {
        //by value
        EdgeListNode<int> node = new EdgeListNode<int>(1);
        node.AddEdgeTo(1);
        bool added = node.HasEdgeTo(1);
        Assert.True(added);

        //by node
        node = new EdgeListNode<int>(1);
        node.AddEdgeTo(node);
        added = node.HasEdgeTo(1);
        Assert.True(added);
      }

      [Fact]
      public void DoesNotAddOldEdges()
      {
        //by value
        EdgeListNode<int> node = new EdgeListNode<int>(1);
        node.AddEdgeTo(1);
        node.AddEdgeTo(1);
        int count = node.Edges.Count();
        Assert.Equal(1, count);

        //by node
        node = new EdgeListNode<int>(1);
        node.AddEdgeTo(node);
        node.AddEdgeTo(node);
        count = node.Edges.Count();
        Assert.Equal(1, count);
      }

      [Fact]
      public void ProducesValidEdges()
      {
        //by value
        //correct reference passed
        EdgeListNode<int> node1 = new EdgeListNode<int>(1);
        Edge<EdgeListNode<int>> edge1 = node1.AddEdgeTo(1);
        Edge<EdgeListNode<int>> addResult1 = node1.AddEdgeTo(1);
        Assert.Equal(addResult1, edge1);
        //coherent 'from' and 'to' nodes
        EdgeListNode<int> node2 = new EdgeListNode<int>(2);
        Edge<EdgeListNode<int>> edge2 = node2.AddEdgeTo(1);
        EdgeListNode<int> from = edge2.From;
        EdgeListNode<int> to = edge2.To;
        Assert.Equal(from, node2);
        Assert.Equal(to.Value, 1);

        //by node
        //correct reference passed
        node1 = new EdgeListNode<int>(1);
        edge1 = node1.AddEdgeTo(node1);
        addResult1 = node1.AddEdgeTo(node1);
        Assert.Equal(addResult1, edge1);
        //coherent 'from' and 'to' nodes
        node2 = new EdgeListNode<int>(2);
        edge2 = node2.AddEdgeTo(node1);
        from = edge2.From;
        to = edge2.To;
        Assert.Equal(from, node2);
        Assert.Equal(to, node1);
      }
    }
  }

}