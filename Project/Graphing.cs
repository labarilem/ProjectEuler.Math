using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Math {

  /// <summary>
  /// Represents a generic graph node.
  /// </summary>
  public class Node<T>
  {
    /// <summary>
    /// The value of the node.
    /// </summary>
    public T Value;

    /// <summary>
    /// Initializes a new instance of the Node class.
    /// </summary>
    /// <param name="value">Value of the node.</param>
    public Node (T value)
    {
      this.Value = value;
    }
  }

  /// <summary>
  /// Represents a generic edge between two objects/values of the same type.
  /// </summary>
  public class Edge<T>
  {
    private const byte DEFAULT_WEIGHT = 1;
    /// <summary>
    /// The 'from' object/value.
    /// </summary>
    public T From;
    /// <summary>
    /// The 'to' object/value
    /// </summary>
    public T To;
    /// <summary>
    /// The weight of this edge.
    /// </summary>
    public double Weight;

    /// <summary>
    /// Initializes a new instance of the Edge class.
    /// </summary>
    /// <param name="from">The 'from' object/value.</param>
    /// <param name="to">The 'to' object/value.</param>
    /// <param name="weight">The weight of this edge.</param>
    public Edge(T from, T to, double weight = DEFAULT_WEIGHT)
    {
      From = from;
      To = to;
      Weight = weight;
    }
  }

  /// <summary>
  /// Represents an adjacency list directed graph node which stores edges. Nodes are identified by their value.
  /// </summary>
  public class EdgeListNode<T> : Node<T>
  {   
    private List<Edge<EdgeListNode<T>>> _edges;
    /// <summary>
    /// Gets the edges spawning from this node.
    /// </summary>
    public IEnumerable<Edge<EdgeListNode<T>>> Edges
    {
      get { return _edges.AsReadOnly(); }
    }

    /// <summary>
    /// Gets the nodes on the 'to' end of each edge spawning from this node.
    /// </summary>
    public IEnumerable<EdgeListNode<T>> Neighbours
    {
      get { return _edges.Select(edge => edge.To); }
    }

    /// <summary>
    /// Initializes a new instance of the EdgeListNode class.
    /// </summary>
    /// <param name="value">Value ot the node.</param>
    public EdgeListNode(T value) : base(value)
    {
      this._edges = new List<Edge<EdgeListNode<T>>>();
    }

    /// <summary>
    /// Searches for an edge spawning from this node and ending in a node having the specified value as value.
    /// </summary>
    /// <param name="value">The value of the to node to search for.</param>
    /// <returns>The edge if it is found, null otherwise.</returns>
    public Edge<EdgeListNode<T>> FindEdgeTo(T value)
    {
      return _edges.Find(edge => edge.To.Value.Equals(value));
    }

    /// <summary>
    /// Searches for an edge spawning from this node and ending in a node equivalent to the specified node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public Edge<EdgeListNode<T>> FindEdgeTo(EdgeListNode<T> node)
    {
      return this.FindEdgeTo(node.Value);
    }

    /// <summary>
    /// Gets whether this node has an edge going to a node equivalent to the specified node.
    /// </summary>
    /// <param name="node">The node on the 'to' end.</param>
    /// <returns>True if the condition is met, false otherwise.</returns>
    public bool HasEdgeTo(EdgeListNode<T> node)
    {
      return this.FindEdgeTo(node) != null;
    }

    /// <summary>
    /// Gets whether this node has an edge going to a node having the specified value as value.
    /// </summary>
    /// <param name="value">The value of the 'to' end node.</param>
    /// <returns>True if the condition is met, false otherwise.</returns>
    public bool HasEdgeTo(T value)
    {
      return this.FindEdgeTo(value) != null;
    }

    /// <summary>
    /// Creates an edge to the specified node and stores it in this node edges, if it does not already exist.
    /// </summary>
    /// <param name="node">The node on the 'to' end of the edge.</param>
    /// <returns>The edge between this node and the specified node.</returns>
    public Edge<EdgeListNode<T>> AddEdgeTo(EdgeListNode<T> node)
    {
      Edge<EdgeListNode<T>> newEdge = new Edge<EdgeListNode<T>>(this, node);
      Edge<EdgeListNode<T>> oldEdge = this.FindEdgeTo(node);
      Edge<EdgeListNode<T>> actualEdge = oldEdge;
      if(oldEdge == null)
      {
        _edges.Add(newEdge);
        actualEdge = newEdge;
      }
      return actualEdge;
        
    }

    /// <summary>
    /// Creates an edge to a node having the specified value and stores it in this node edges, if it does not already exist.
    /// </summary>
    /// <param name="value">The value of the node on the 'to' end of the edge.</param>
    /// /// <returns>The edge between this node and the one having value as the specified value.</returns>
    public Edge<EdgeListNode<T>> AddEdgeTo(T value)
    {
      EdgeListNode<T> newNode = new EdgeListNode<T>(value);
      return this.AddEdgeTo(newNode);
    }

    /// <summary>
    /// Returns the string representing the value of this node.
    /// </summary>
    public override string ToString()
    {
      return this.Value.ToString();
    }
  }

  /// <summary>
  /// Represents an adjacency list directed graph with nodes storing edges. Nodes are identified by their values.
  /// </summary>
  public class EdgeListGraph<T>
  {   
    private List<EdgeListNode<T>> _nodes;
    /// <summary>
    /// Gets the nodes directly added to this graph.
    /// </summary>
    public IEnumerable<EdgeListNode<T>> Nodes
    {
      get { return _nodes.AsReadOnly(); }
    }

    /// <summary>
    /// Gets all the nodes in this graph by lazy enumeration.
    /// </summary>
    public IEnumerable<EdgeListNode<T>> AllNodes
    {
      get
      {
        List<EdgeListNode<T>> explored = new List<EdgeListNode<T>>();
        List<EdgeListNode<T>> toExplore = new List<EdgeListNode<T>>();
        toExplore.AddRange(_nodes);
        while(toExplore.Count > 0)
        {
          EdgeListNode<T> node = toExplore[0];
          yield return node;
          toExplore.RemoveAt(0);
          explored.Add(node);
          foreach(EdgeListNode<T> neighbour in node.Neighbours)
            if(explored.FindIndex(n => n.Value.Equals(neighbour.Value)) == -1)
              toExplore.Add(neighbour);
        }
      }
    }

    /// <summary>
    /// Gets all the edges in this graph by lazy enumeration.
    /// </summary>
    public IEnumerable<Edge<EdgeListNode<T>>> AllEdges
    {
      get
      {   
        List<Edge<EdgeListNode<T>>> allEdges = new List<Edge<EdgeListNode<T>>>();
        foreach(EdgeListNode<T> node in AllNodes)
        {
          foreach(Edge<EdgeListNode<T>> edge in node.Edges)
          {
            if(!allEdges.Contains(edge))
            {
              allEdges.Add(edge);
              yield return edge;
            }
          }
        }
      }
    }

    /// <summary>
    /// Gets all the nodes in this graph having no incoming edges.
    /// </summary>
    public IEnumerable<EdgeListNode<T>> NodesWithoutIncomingEdges
    {
      get
      {
        Dictionary<EdgeListNode<T>, bool> hasIncoming = new Dictionary<EdgeListNode<T>, bool>();
        foreach(EdgeListNode<T> node in Nodes)
        {
          hasIncoming.Add(node, false);
        }
          
        foreach(EdgeListNode<T> node in AllNodes)
        {
          foreach(EdgeListNode<T> neighbour in node.Neighbours)
          {
            if(hasIncoming.ContainsKey(neighbour))
              hasIncoming[neighbour] = true;
          }
        }

        return hasIncoming.Where((KeyValuePair<EdgeListNode<T>, bool> pair) => !pair.Value)
          .Select((KeyValuePair<EdgeListNode<T>, bool> pair) => pair.Key);
      }
    }

    /// <summary>
    /// Initializes a new instance of the EdgeListGraph class.
    /// </summary>
    public EdgeListGraph()
    {
      _nodes = new List<EdgeListNode<T>>();
    }

    /// <summary>
    /// Initializes a new instance of the EdgeListGraph class.
    /// </summary>
    /// <param name="nodes">Initial list of nodes.</param>
    public EdgeListGraph(List<EdgeListNode<T>> nodes)
    {
      if(nodes != null)
        _nodes = nodes;
      else
        _nodes = new List<EdgeListNode<T>>();
    }

    /// <summary>
    /// Adds a new node to this graph, if it does not already exist.
    /// </summary>
    /// <param name="node">The node to add.</param>
    public EdgeListNode<T> AddNode(EdgeListNode<T> node)
    {
      EdgeListNode<T> target = FindNode(node.Value);
      if(target == null)
      {
        _nodes.Add(node);
        return node;
      }
      else
      {
        return target;
      } 
    }

    /// <summary>
    /// Adds a new node having the specified value as value, if it does not already exist.
    /// </summary>
    /// <param name="value"></param>
    public EdgeListNode<T> AddNode(T value)
    {
      EdgeListNode<T> target = new EdgeListNode<T>(value);
      return AddNode(target);
    }

    /// <summary>
    /// Finds a node by value.
    /// </summary>
    /// <param name="value">The value of the node.</param>
    /// <returns>The node if it is found, null otherwise.</returns>
    public EdgeListNode<T> FindNode(T value)
    {
      return FindNode((EdgeListNode<T> node) => node.Value.Equals(value));
    }

    /// <summary>
    /// Finds a node equivalent to the specified node.
    /// </summary>
    /// <param name="node">The node to search for.</param>
    /// <returns>The node if it is found, null otherwise.</returns>
    public EdgeListNode<T> FindNode(EdgeListNode<T> node)
    {
      return this.FindNode(node.Value);
    }

    /// <summary>
    /// Returns the first node satisfying the specified predicate.
    /// </summary>
    /// <param name="predicate">Predicate to test on the nodes of this graph.</param>
    /// <returns>The first node satisfying the predicate or null if no node satifies the predicate.</returns>
    public EdgeListNode<T> FindNode(Func<EdgeListNode<T>, bool> predicate)
    {
      foreach(EdgeListNode<T> node in AllNodes)
        if(predicate(node))
          return node;
      return null;
    }

    /// <summary>
    /// Gets whether this graph contains a node having the specfied value as value.
    /// </summary>
    /// <param name="value">Value to search for.</param>
    /// <returns>True if a node is found, false otherwise.</returns>
    public bool ContainsNode(T value)
    {
      return this.FindNode(value) != null;
    }

    /// <summary>
    /// Gets whether this graph contains a node equivalent to the specified node.
    /// </summary>
    /// <param name="node">The equivalent node to search for.</param>
    /// <returns>True if a node is found, false otherwise.</returns>
    public bool ContainsNode(EdgeListNode<T> node)
    {
      return this.ContainsNode(node.Value);
    }

    /// <summary>
    /// Gets whether this graph contains a node satisfying the specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public bool ContainsNode(Func<EdgeListNode<T>, bool> predicate)
    {
      return this.FindNode(predicate) != null;
    }

    /// <summary>
    /// Adds a directed edge between the specified nodes.
    /// </summary>
    /// <param name="from">The node spawning the edge.</param>
    /// <param name="to">The destination of the edge.</param>
    /// <returns>The edge between the nodes equivalent to the specified nodes.</returns>
    public Edge<EdgeListNode<T>> AddDirectedEdge(EdgeListNode<T> from, EdgeListNode<T> to)
    {
      return this.AddDirectedEdge(from.Value, to.Value);
    }

    /// <summary>
    /// Adds a directed edge between the nodes identified by the specified values.
    /// </summary>
    /// <param name="from">Value identifying the node that will spawn the edge.</param>
    /// <param name="to">Value identifying the node on the 'to' end of the edge.</param>
    /// <returns>The edge between the targeted nodes.</returns>
    public Edge<EdgeListNode<T>> AddDirectedEdge(T from, T to)
    {
      EdgeListNode<T> fromTarget = FindNode(from);
      EdgeListNode<T> toTarget = FindNode(to);
      if(fromTarget == null)
         throw new ArgumentException("The 'from' node is not part of this graph.");
      if(toTarget == null)
         throw new ArgumentException("The 'to' node is not part of this graph.");
      return fromTarget.AddEdgeTo(to);
    }

    /// <summary>
    /// Topologically sorts the nodes of this graph. Throws an exception if this graph is acyclic.
    /// </summary>
    /// <returns>The sorted nodes collection.</returns>
    public IEnumerable<EdgeListNode<T>> TopologicalSort()
    {
      List<EdgeListNode<T>> ordered = new List<EdgeListNode<T>>();
      List<EdgeListNode<T>> noIncomingEdges = NodesWithoutIncomingEdges.ToList();
      List<Edge<EdgeListNode<T>>> allEdges = AllEdges.ToList();

      while(noIncomingEdges.Count() > 0)
      {
        EdgeListNode<T> node = noIncomingEdges[0];
        noIncomingEdges.RemoveAt(0);
        ordered.Add(node);
        IEnumerable<Edge<EdgeListNode<T>>> outgoingEdges = allEdges.Where((Edge<EdgeListNode<T>> e) => e.From.Equals(node)).ToList();
        foreach(Edge<EdgeListNode<T>> edge in outgoingEdges)
        {
          allEdges.Remove(edge);
          int index = allEdges.FindIndex((Edge<EdgeListNode<T>> e) => e.To.Equals(edge.To));
          if(index == -1)
            noIncomingEdges.Add(edge.To);
        }
      }
      
      if(allEdges.Count > 0)
        throw new Exception("This graph has at least one cycle, so it can't be topologically sorted.");
      else
        return ordered;
    }
  }
}