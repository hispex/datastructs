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
}
