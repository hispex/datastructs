using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucs
{
	class Graph
	{
	}

	public interface IVertex: IComparable
	{
		int Number { get; }
		object Weight { get; }
		IEnumerable IncidentEdge { get; }
		IEnumerable EmanatingEdges { get; }
		IEnumerable Predecessors { get;  }
		IEnumerable Successors { get; }
	}
	public interface IEdge:IComparable
	{
		IVertex VO { get; }
		IVertex V1 { get; }
		object Weight { get; }
		bool IsDirected { get; }
		IVertex MateOf(IVertex vertex);
	}
	public interface IGraph:ICollection
	{
		int NumberOfEdges { get; }
		int NumberOfVertices { get; }
		bool IsDirected { get; }
		void AddVertex(int v);
		void AddVertex(int v, object weight);
		IVertex GetVertex(int v);
		void AddEdge(int v, int w);
		void AddEdge(int v, int w, object weight);
		IEdge GetEdge(int v, int w);
		bool IsEdge(int v, int w);
		bool IsConnected{get;}
		bool IsCyclic { get; }
		IEnumerable Vertices { get; }
		IEnumerable Edges { get; }
		void DepthFirstTraversal(IPrePostVisitor visitor, int start);
		void BreadthFirstTraversal(Visitor visitor, int start);
	}
	public interface IDigraph:IGraph
	{
		bool IsStronglyConnected { get; }
		void TopologicalOrderTraversal(Visitor visitor);
	}
	public abstract class AbstractGraph:IGraph
	{

		protected int numberOfVertices;
		protected int numberOfEdges;
		protected IVertex[] vertex;

		public AbstractGraph(int size)
		{
			vertex =new IVertex[size];
		}

		protected class GraphVertex:ComparableObject, IVertex
		{
			protected AbstractGraph graph;
			protected int number;
			protected object weight;

			public override int CompareTo(object obj)
			{
				throw new NotImplementedException();
			}

			public int Number { get; private set; }
			public object Weight { get; private set; }
			public IEnumerable IncidentEdge { get; private set; }
			public IEnumerable EmanatingEdges { get; private set; }
			public IEnumerable Predecessors { get; private set; }
			public IEnumerable Successors { get; private set; }
		}

		protected class GraphEdge:ComparableObject,IEdge
		{

			protected AbstractGraph graph;
			protected int v0;
			protected int v1;
			protected object weight;

			public override int CompareTo(object obj)
			{
				throw new NotImplementedException();
			}

			public IVertex VO { get; private set; }
			public IVertex V1 { get; private set; }
			public object Weight { get; private set; }
			public bool IsDirected { get; private set; }
			public IVertex MateOf(IVertex vertex)
			{
				throw new NotImplementedException();
			}
		}

		protected abstract IEnumerable GetIncidentEdges(int v);
		protected abstract IEnumerable GetEmanatingEdges(int v);
		
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		public int Count { get; private set; }
		public object SyncRoot { get; private set; }
		public bool IsSynchronized { get; private set; }
		public int NumberOfEdges { get; private set; }
		public int NumberOfVertices { get; private set; }
		public bool IsDirected { get; private set; }
		public void AddVertex(int v)
		{
			throw new NotImplementedException();
		}

		public void AddVertex(int v, object weight)
		{
			throw new NotImplementedException();
		}

		public IVertex GetVertex(int v)
		{
			throw new NotImplementedException();
		}

		public void AddEdge(int v, int w)
		{
			throw new NotImplementedException();
		}

		public void AddEdge(int v, int w, object weight)
		{
			throw new NotImplementedException();
		}

		public IEdge GetEdge(int v, int w)
		{
			throw new NotImplementedException();
		}

		public bool IsEdge(int v, int w)
		{
			throw new NotImplementedException();
		}

		public bool IsConnected { get; private set; }
		public bool IsCyclic { get; private set; }
		public IEnumerable Vertices { get; private set; }
		public IEnumerable Edges { get; private set; }
		public void DepthFirstTraversal(IPrePostVisitor visitor, int start)
		{
			throw new NotImplementedException();
		}

		public void BreadthFirstTraversal(Visitor visitor, int start)
		{
			throw new NotImplementedException();
		}
	}

	public class GraphAsMatrix:AbstractGraph
	{
		protected IEdge[,] matrix;
		public GraphAsMatrix(int size) : base(size)
		{
			matrix=new IEdge[size,size];
		}

		protected override IEnumerable GetIncidentEdges(int v)
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable GetEmanatingEdges(int v)
		{
			throw new NotImplementedException();
		}
	}
	public class GraphAsLists:AbstractGraph
	{
		protected ArrayList[] adjacencyList;
		public GraphAsLists(int size) : base(size)
		{
			adjacencyList = new ArrayList[size];
			for (int i = 0; i < size; i++)
			{
				adjacencyList[i]=new ArrayList();
			}
		}

		protected override IEnumerable GetIncidentEdges(int v)
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable GetEmanatingEdges(int v)
		{
			throw new NotImplementedException();
		}
	}
}
