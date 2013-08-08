using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStrucs
{
	public interface ISearchTree: ICollection, ITree
{
		IComparable Min { get; }
		IComparable Max { get; }

		//need Find()
}

	public class BinarySearchTree:BinaryTree, ISearchTree
	{
		new public virtual IComparable Key { get { return (IComparable) base.Key; } }
		new public virtual BinarySearchTree Left { get { return (BinarySearchTree) base.Left; } }
		new public virtual BinarySearchTree Right { get { return (BinarySearchTree) base.Right; } }

		public virtual IComparable Find(IComparable obj)
		{
			if (this.IsEmpty)
			{
				return null;
			}

			int diff = obj.CompareTo(Key);
			if (diff == 0)
			{
				return Key;
			}
			else if (diff < 0)
			{
				return Left.Find(obj);
			}
			else
			{
				return Right.Find(obj);
			}
		}
	
		public virtual IComparable Min
		{
			get
			{
				if (IsEmpty)
					return null;
				else if (Left.IsEmpty)
					return Key;
				else
					return Left.Min;
			}
		}
		public virtual IComparable Max
		{
			get
			{
				if (IsEmpty)
					return null;
				else if (Right.IsEmpty)
					return Key;
				else
					return Right.Max;
			}
		}

		public virtual void Remove(IComparable obj)
		{
			if (IsEmpty)
			{
				throw new ArgumentException("object not found");
			}
			int diff = obj.CompareTo(Key);
			if (diff==0)
			{
				if (!Left.IsEmpty)
				{
					IComparable max = Left.Max;
					key = max;
					Left.Remove(max);
				}
				else if (!Right.IsEmpty)
				{
					IComparable min = Right.Min;
					key = min;
					Right.Remove(min);
				}
				else
				{
					//DetachKey();
				}
			}
			else if (diff < 0)
			{
				Left.Remove(obj);
			}
			else
			{
				Right.Remove(obj);
			}
			Balance();
		}
		public virtual void Insert(IComparable obj)
		{
			if (IsEmpty)
			{
				AttachKey(obj);
			}
			else
			{
				int diff = obj.CompareTo(Key);
				if (diff == 0)
				{
					throw new ArgumentException("duplicate key");
				}
				if (diff<0)
				{
					Left.Insert(obj);
				}
				else
				{
					Right.Insert(obj);
				}
			}
			Balance();
		}

		protected void Balance()
		{
			
		}
		private void AttachKey(object obj)
		{
			if (!IsEmpty)
			{
				throw new InvalidOperationException();
			}
			key = obj;
			left=new BinarySearchTree();
			right=new BinarySearchTree();
		}

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
	}

interface ISorter
{
	void Sort(IComparable[] array);
}

	public abstract class AbstractQuickSorter:ISorter
	{
		protected const int CUTOFF = 2;
		protected abstract int SelectPivot(int left, int right);

		private IComparable[] array;
		private int n;

		protected void Sort()
		{
			Sort(0, n - 1);
			
		}
		public void Sort(IComparable[] array)
		{
			n = array.Length;
			this.array = array;
			if (n > 0)
			{
				Sort();
			}
			this.array = null;
		}

		protected void Sort(int left, int right )
		{
			if (right-left+1>CUTOFF)
			{
				int p = SelectPivot(left, right);
				Swap(p, right);
				var pivot = array[right];
				int i = left;
				int j = right - 1;
				for (;;)
				{
					while (i < j && array[i].CompareTo(pivot)<0) ++i;
					while (i < j && array[j].CompareTo(pivot)>0) --j;
					if (i >= j) break;
					Swap(i++,j--);
				}
				if (array[i].CompareTo(pivot) > 0)
				{
					Swap(i,right);
				}
				if (left < i)
				{
					Sort(left,i-1);
				}
				if (right>i)
				{
					Sort(i+1,right);
				}

			}
		}

		private void Swap(int  a, int b)
		{
			var tmp = array[a];
			array[a] = array[b];
			array[b] = tmp;
		}
	}
}

