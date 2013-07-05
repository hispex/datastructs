using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucs
{
	 class Program
	{
		 static void Main(string[] args)
		{
			var tree = new Tree(1);
			tree.AddSubtree(new Tree(2));
			tree.AddSubtree(new Tree(3));
			tree.AddSubtree(new Tree(4));
			
			Algorithms.BreadthFirstTraversal(tree);
			//blabla

		}
	}

	public static class Algorithms
	{
		public static void BreadthFirstTraversal(Tree tree)
		{
			Queue<ITree> queue = new Queue<ITree>();
			queue.Enqueue(tree);
			while (queue.Count>0)
			{
				var t = queue.Dequeue();
				Console.WriteLine(t.Key);
				for (int i = 0; i < t.Degree; i++)
				{
					var subtree = t.GetSubtree(i);
					queue.Enqueue(subtree);
				}
			}
		}
	}

	public interface ITree
	{
		object Key { get;}
		int Degree { get; }
		ITree GetSubtree(int i);
	}

	public class Tree:ITree
	{
		private List<Tree> subtrees=new List<Tree>();
		
		private int key;

		public Tree(int key)
		{
			this.key = key;
		}

		public object Key { get { return key; }
		}
		public int Degree { get { return subtrees.Count; } }

		public ITree GetSubtree(int i)
		{
			if (i > subtrees.Count-1)
			{
				throw new ArgumentOutOfRangeException();
			}
			return subtrees[i];
		}
		public void AddSubtree(Tree subtree)
		{
			subtrees.Add(subtree);
		}
	}
}
