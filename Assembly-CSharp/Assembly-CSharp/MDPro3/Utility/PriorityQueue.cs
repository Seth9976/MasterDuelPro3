using System;
using System.Collections.Generic;

namespace MDPro3.Utility
{
	// Token: 0x020012C1 RID: 4801
	public class PriorityQueue<T>
	{
		// Token: 0x06008C82 RID: 35970 RVA: 0x001243E0 File Offset: 0x001225E0
		public PriorityQueue()
		{
			this.data = new List<T>();
			this.comparer = Comparer<T>.Default;
		}

		// Token: 0x06008C83 RID: 35971 RVA: 0x001243FE File Offset: 0x001225FE
		public PriorityQueue(IComparer<T> comparer)
		{
			this.data = new List<T>();
			this.comparer = comparer ?? Comparer<T>.Default;
		}

		// Token: 0x06008C84 RID: 35972 RVA: 0x00124421 File Offset: 0x00122621
		public PriorityQueue(Comparison<T> comparison)
		{
			this.data = new List<T>();
			this.comparer = Comparer<T>.Create(comparison);
		}

		// Token: 0x06008C85 RID: 35973 RVA: 0x00124440 File Offset: 0x00122640
		public void Enqueue(T item)
		{
			this.data.Add(item);
			int parentIndex;
			for (int childIndex = this.data.Count - 1; childIndex > 0; childIndex = parentIndex)
			{
				parentIndex = (childIndex - 1) / 2;
				if (this.comparer.Compare(this.data[childIndex], this.data[parentIndex]) >= 0)
				{
					break;
				}
				T tmp = this.data[childIndex];
				this.data[childIndex] = this.data[parentIndex];
				this.data[parentIndex] = tmp;
			}
		}

		// Token: 0x06008C86 RID: 35974 RVA: 0x001244D0 File Offset: 0x001226D0
		public T Dequeue()
		{
			if (this.data.Count == 0)
			{
				throw new InvalidOperationException("Queue is empty");
			}
			int lastIndex = this.data.Count - 1;
			T frontItem = this.data[0];
			this.data[0] = this.data[lastIndex];
			this.data.RemoveAt(lastIndex);
			lastIndex--;
			int parentIndex = 0;
			for (;;)
			{
				int leftChildIndex = parentIndex * 2 + 1;
				if (leftChildIndex > lastIndex)
				{
					break;
				}
				int rightChildIndex = leftChildIndex + 1;
				if (rightChildIndex <= lastIndex && this.comparer.Compare(this.data[rightChildIndex], this.data[leftChildIndex]) < 0)
				{
					leftChildIndex = rightChildIndex;
				}
				if (this.comparer.Compare(this.data[parentIndex], this.data[leftChildIndex]) <= 0)
				{
					break;
				}
				T tmp = this.data[parentIndex];
				this.data[parentIndex] = this.data[leftChildIndex];
				this.data[leftChildIndex] = tmp;
				parentIndex = leftChildIndex;
			}
			return frontItem;
		}

		// Token: 0x06008C87 RID: 35975 RVA: 0x001245DD File Offset: 0x001227DD
		public T Peek()
		{
			if (this.data.Count == 0)
			{
				throw new InvalidOperationException("Queue is empty");
			}
			return this.data[0];
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06008C88 RID: 35976 RVA: 0x00124603 File Offset: 0x00122803
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x06008C89 RID: 35977 RVA: 0x00124610 File Offset: 0x00122810
		public bool TryDequeue(out T result)
		{
			if (this.data.Count == 0)
			{
				result = default(T);
				return false;
			}
			result = this.Dequeue();
			return true;
		}

		// Token: 0x06008C8A RID: 35978 RVA: 0x00124635 File Offset: 0x00122835
		public bool TryPeek(out T result)
		{
			if (this.data.Count == 0)
			{
				result = default(T);
				return false;
			}
			result = this.Peek();
			return true;
		}

		// Token: 0x06008C8B RID: 35979 RVA: 0x0012465A File Offset: 0x0012285A
		public void Clear()
		{
			this.data.Clear();
		}

		// Token: 0x06008C8C RID: 35980 RVA: 0x00124667 File Offset: 0x00122867
		public bool Contains(T item)
		{
			return this.data.Contains(item);
		}

		// Token: 0x0400CA6F RID: 51823
		private readonly List<T> data;

		// Token: 0x0400CA70 RID: 51824
		private readonly IComparer<T> comparer;
	}
}
