using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a node in a <see cref="T:System.Collections.Generic.LinkedList`1" />. This class cannot be inherited.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200031A RID: 794
	public sealed class LinkedListNode<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> class, containing the specified value.</summary>
		/// <param name="value">The value to contain in the <see cref="T:System.Collections.Generic.LinkedListNode`1" />.</param>
		// Token: 0x06001383 RID: 4995 RVA: 0x00055FC7 File Offset: 0x000541C7
		public LinkedListNode(T value)
		{
			this.item = value;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00055FD6 File Offset: 0x000541D6
		internal LinkedListNode(LinkedList<T> list, T value)
		{
			this.list = list;
			this.item = value;
		}

		/// <summary>Gets the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the last element (<see cref="P:System.Collections.Generic.LinkedList`1.Last" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x00055FEC File Offset: 0x000541EC
		public LinkedListNode<T> Next
		{
			get
			{
				if (this.next != null && this.next != this.list.head)
				{
					return this.next;
				}
				return null;
			}
		}

		/// <summary>Gets the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the first element (<see cref="P:System.Collections.Generic.LinkedList`1.First" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00056011 File Offset: 0x00054211
		public LinkedListNode<T> Previous
		{
			get
			{
				if (this.prev != null && this != this.list.head)
				{
					return this.prev;
				}
				return null;
			}
		}

		/// <summary>Gets the value contained in the node.</summary>
		/// <returns>The value contained in the node.</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x00056031 File Offset: 0x00054231
		// (set) Token: 0x06001388 RID: 5000 RVA: 0x00056039 File Offset: 0x00054239
		public T Value
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00056042 File Offset: 0x00054242
		internal void Invalidate()
		{
			this.list = null;
			this.next = null;
			this.prev = null;
		}

		// Token: 0x04000BAF RID: 2991
		internal LinkedList<T> list;

		// Token: 0x04000BB0 RID: 2992
		internal LinkedListNode<T> next;

		// Token: 0x04000BB1 RID: 2993
		internal LinkedListNode<T> prev;

		// Token: 0x04000BB2 RID: 2994
		internal T item;
	}
}
