using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucs
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			

		}
	}

	public abstract class ComparableObject:IComparable
	{
		public abstract int CompareTo(object obj);
		private int Compare(object obj)
		{
			if (GetType()==obj.GetType())
			{
				return CompareTo(obj);
			}
			else
			{
				return GetType().FullName.CompareTo(obj.GetType().FullName);
			}
		}
		public override bool Equals(object obj)
		{
			return Compare(obj) == 0;
		}

	}

	public static class Algorithms
	{
		public static void BreadthFirstTraversal(Tree tree)
		{
			Queue<ITree> queue = new Queue<ITree>();
			queue.Enqueue(tree);
			while (queue.Count > 0)
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
		object Key { get; }
		ITree GetSubtree(int i);
		bool IsLeaf { get; }
		int Degree { get; }
		int Height { get; }
		void DepthFirstTraversal(IPrePostVisitor visitor);
		void BreadthFirstTraversal(Visitor visitor);
	}

	public abstract class AbstractTree : ITree
	{
		public int Degree { get; private set; }

		public virtual void BreadthFirstTraversal(Visitor visitor)
		{
			Queue<object> queue = new Queue<object>();
			if (!IsEmpty)
			{
				queue.Enqueue(this);
			}
			while (queue.Count > 0 && !visitor.IsDone)
			{
				Tree head = (Tree) queue.Dequeue();
				visitor.Visit(head.Key);
				for (int i = 0; i < head.Degree; i++)
				{
					ITree child = head.GetSubtree(i);
					if (!child.IsLeaf)
					{
						queue.Enqueue(child);
					}
				}
			}
		}

		protected bool IsEmpty
		{
			get { return Degree == 0; }
		}

		public virtual void DepthFirstTraversal(IPrePostVisitor visitor)
		{
			if (visitor.IsDone)
			{
				return;
			}
			if (!IsEmpty)
			{
				visitor.PreVisit(Key);
				for (int i = 0; i < Degree; i++)
				{
					GetSubtree(i).DepthFirstTraversal(visitor);
				}
				visitor.PostVisit(Key);
			}
		}

		public ITree GetSubtree(int i)
		{
			throw new NotImplementedException();
		}

		public int Height { get; private set; }
		public bool IsLeaf { get; private set; }
		public object Key { get; private set; }

		public void Accept(Visitor visitor)
		{
			DepthFirstTraversal(new PreOrder(visitor));
		}
	}

	public class BinaryTree : AbstractTree
	{
		protected object key;
		protected BinaryTree left;
		protected BinaryTree right;

		public BinaryTree(object key, BinaryTree left, BinaryTree right)
		{
			this.key = key;
			this.left = left;
			this.right = right;
		}

		public BinaryTree(object key) : this(key, new BinaryTree(), new BinaryTree())
		{
		}

		public BinaryTree() : this(null, null, null)
		{
		}

		public BinaryTree Left
		{
			get
			{
				if (IsEmpty)
				{
					throw new InvalidOperationException();
				}
				return left;
			}
			set { left = value; }
		}

		public BinaryTree Right
		{
			get
			{
				if (IsEmpty)
				{
					throw new InvalidOperationException();
				}
				return right;
			}
			set
			{
				right = value;
			}
		}

		public override void DepthFirstTraversal(IPrePostVisitor visitor)
		{
			if (!IsEmpty)
			{
				visitor.PreVisit(key);
				Left.DepthFirstTraversal(visitor);
				visitor.InVisit(key);
				Right.DepthFirstTraversal(visitor);
				visitor.PostVisit(key);
			}
		}

		public void Purge()
		{
			key = null;
			left = null;
			right = null;
		}
	}

	public class ExpressionTree : BinaryTree
	{
		public ExpressionTree(char key) : base(key)
		{
		}

		public static ExpressionTree ParsePostfix(TextReader reader)
		{
			Stack<ExpressionTree> stack = new Stack<ExpressionTree>();
			int i;
			while ((i = reader.Read()) >= 0)
			{
				char c = (char) i;
				if (Char.IsLetterOrDigit(c))
				{
					stack.Push(new ExpressionTree(c));
				}
				else if (c == '+' || c == '-' || c == '*' || c == '/')
				{
					ExpressionTree result = new ExpressionTree(c);
					result.Right=stack.Pop();
					result.Left=stack.Pop();
					stack.Push(result);
				}
			}
			return stack.Pop();
		}
	}

	#region Visitor

	public interface IPrePostVisitor
	{
		void PreVisit(object obj);
		void InVisit(object obj);
		void PostVisit(object obj);
		bool IsDone { get; }
	}

	public abstract class AbstractPreVisitor : IPrePostVisitor
	{
		public virtual void PreVisit(object obj)
		{
			throw new NotImplementedException();
		}

		public virtual void InVisit(object obj)
		{
			throw new NotImplementedException();
		}

		public virtual void PostVisit(object obj)
		{
			throw new NotImplementedException();
		}

		public virtual bool IsDone
		{
			get { return false; }
		}
	}

	public class PreOrder : AbstractPreVisitor
	{
		protected Visitor visitor;

		public PreOrder(Visitor visitor)
		{
			this.visitor = visitor;
		}

		public override void PreVisit(object obj)
		{
			visitor.Visit(obj);
		}

		public override bool IsDone
		{
			get { return visitor.IsDone; }
		}
	}

	public class InOrder : AbstractPreVisitor
	{
		protected Visitor visitor;

		public InOrder(Visitor visitor)
		{
			this.visitor = visitor;
		}

		public override void InVisit(object obj)
		{
			visitor.Visit(obj);
		}

		public override bool IsDone
		{
			get { return visitor.IsDone; }
		}
	}

	public class PostOrder : AbstractPreVisitor
	{
		protected Visitor visitor;

		public PostOrder(Visitor visitor)
		{
			this.visitor = visitor;
		}

		public override void PostVisit(object obj)
		{
			visitor.Visit(obj);
		}

		public override bool IsDone
		{
			get { return visitor.IsDone; }
		}
	}

	public interface Visitor
	{
		void Visit(object o);
		bool IsDone { get; set; }
	}

	#endregion

	public class Tree : ITree
	{
		private List<Tree> subtrees = new List<Tree>();

		private int key;

		public Tree(int key)
		{
			this.key = key;
		}

		public object Key
		{
			get { return key; }
		}

		public int Degree
		{
			get { return subtrees.Count; }
		}

		public int Height { get; private set; }

		public void DepthFirstTraversal(IPrePostVisitor visitor)
		{
			throw new NotImplementedException();
		}

		public void BreadthFirstTraversal(Visitor visitor)
		{
			throw new NotImplementedException();
		}

		public ITree GetSubtree(int i)
		{
			if (i > subtrees.Count - 1)
			{
				throw new ArgumentOutOfRangeException();
			}
			return subtrees[i];
		}

		public bool IsLeaf { get; private set; }

		public void AddSubtree(Tree subtree)
		{
			subtrees.Add(subtree);
		}
	}
}