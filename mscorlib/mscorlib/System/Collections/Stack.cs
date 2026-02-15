using System;
using System.Diagnostics;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a simple last-in-first-out (LIFO) non-generic collection of objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000714 RID: 1812
	[DebuggerTypeProxy(typeof(Stack.StackDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Stack : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x060038FD RID: 14589 RVA: 0x000DDF07 File Offset: 0x000DC107
		public Stack()
		{
			this._array = new object[10];
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.Stack" /> can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero. </exception>
		// Token: 0x060038FE RID: 14590 RVA: 0x000DDF2A File Offset: 0x000DC12A
		public Stack(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "Non-negative number required.");
			}
			if (initialCapacity < 10)
			{
				initialCapacity = 10;
			}
			this._array = new object[initialCapacity];
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000DDF69 File Offset: 0x000DC169
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe).</summary>
		/// <returns>true, if access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x000DDF71 File Offset: 0x000DC171
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Stack" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003902 RID: 14594 RVA: 0x000DDF93 File Offset: 0x000DC193
		public virtual void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
			this._version++;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003903 RID: 14595 RVA: 0x000DDFBC File Offset: 0x000DC1BC
		public virtual object Clone()
		{
			Stack stack = new Stack(this._size);
			stack._size = this._size;
			Array.Copy(this._array, 0, stack._array, 0, this._size);
			stack._version = this._version;
			return stack;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>true, if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Stack" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Stack" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003904 RID: 14596 RVA: 0x000DE008 File Offset: 0x000DC208
		public virtual bool Contains(object obj)
		{
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[size] == null)
					{
						return true;
					}
				}
				else if (this._array[size] != null && this._array[size].Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Stack" /> to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Stack" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Stack" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Stack" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003905 RID: 14597 RVA: 0x000DE054 File Offset: 0x000DC254
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int i = 0;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i < this._size)
				{
					array2[i + index] = this._array[this._size - i - 1];
					i++;
				}
				return;
			}
			while (i < this._size)
			{
				array.SetValue(this._array[this._size - i - 1], i + index);
				i++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003906 RID: 14598 RVA: 0x000DE110 File Offset: 0x000DC310
		public virtual IEnumerator GetEnumerator()
		{
			return new Stack.StackEnumerator(this);
		}

		/// <summary>Returns the object at the top of the <see cref="T:System.Collections.Stack" /> without removing it.</summary>
		/// <returns>The <see cref="T:System.Object" /> at the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003907 RID: 14599 RVA: 0x000DE118 File Offset: 0x000DC318
		public virtual object Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("Stack empty.");
			}
			return this._array[this._size - 1];
		}

		/// <summary>Removes and returns the object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> removed from the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003908 RID: 14600 RVA: 0x000DE13C File Offset: 0x000DC33C
		public virtual object Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("Stack empty.");
			}
			this._version++;
			object[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			object obj = array[num];
			this._array[this._size] = null;
			return obj;
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to push onto the <see cref="T:System.Collections.Stack" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003909 RID: 14601 RVA: 0x000DE190 File Offset: 0x000DC390
		public virtual void Push(object obj)
		{
			if (this._size == this._array.Length)
			{
				object[] array = new object[2 * this._array.Length];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
			}
			object[] array2 = this._array;
			int size = this._size;
			this._size = size + 1;
			array2[size] = obj;
			this._version++;
		}

		// Token: 0x04001E81 RID: 7809
		private object[] _array;

		// Token: 0x04001E82 RID: 7810
		private int _size;

		// Token: 0x04001E83 RID: 7811
		private int _version;

		// Token: 0x04001E84 RID: 7812
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000715 RID: 1813
		[Serializable]
		private class StackEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x0600390A RID: 14602 RVA: 0x000DE1FF File Offset: 0x000DC3FF
			internal StackEnumerator(Stack stack)
			{
				this._stack = stack;
				this._version = this._stack._version;
				this._index = -2;
				this._currentElement = null;
			}

			// Token: 0x0600390B RID: 14603 RVA: 0x00019FBE File Offset: 0x000181BE
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x0600390C RID: 14604 RVA: 0x000DE230 File Offset: 0x000DC430
			public virtual bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					bool flag = this._index >= 0;
					if (flag)
					{
						this._currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				bool flag2 = num >= 0;
				if (flag2)
				{
					this._currentElement = this._stack._array[this._index];
					return flag2;
				}
				this._currentElement = null;
				return flag2;
			}

			// Token: 0x170008FD RID: 2301
			// (get) Token: 0x0600390D RID: 14605 RVA: 0x000DE2E5 File Offset: 0x000DC4E5
			public virtual object Current
			{
				get
				{
					if (this._index == -2)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					return this._currentElement;
				}
			}

			// Token: 0x0600390E RID: 14606 RVA: 0x000DE316 File Offset: 0x000DC516
			public virtual void Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -2;
				this._currentElement = null;
			}

			// Token: 0x04001E85 RID: 7813
			private Stack _stack;

			// Token: 0x04001E86 RID: 7814
			private int _index;

			// Token: 0x04001E87 RID: 7815
			private int _version;

			// Token: 0x04001E88 RID: 7816
			private object _currentElement;
		}

		// Token: 0x02000716 RID: 1814
		internal class StackDebugView
		{
		}
	}
}
