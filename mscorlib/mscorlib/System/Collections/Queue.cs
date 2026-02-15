using System;
using System.Diagnostics;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a first-in, first-out collection of objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200070A RID: 1802
	[DebuggerTypeProxy(typeof(Queue.QueueDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Queue : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the default initial capacity, and uses the default growth factor.</summary>
		// Token: 0x0600387C RID: 14460 RVA: 0x000DC7AD File Offset: 0x000DA9AD
		public Queue()
			: this(32, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the default growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x0600387D RID: 14461 RVA: 0x000DC7BC File Offset: 0x000DA9BC
		public Queue(int capacity)
			: this(capacity, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the specified growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
		/// <param name="growFactor">The factor by which the capacity of the <see cref="T:System.Collections.Queue" /> is expanded. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="growFactor" /> is less than 1.0 or greater than 10.0. </exception>
		// Token: 0x0600387E RID: 14462 RVA: 0x000DC7CC File Offset: 0x000DA9CC
		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Non-negative number required.");
			}
			if ((double)growFactor < 1.0 || (double)growFactor > 10.0)
			{
				throw new ArgumentOutOfRangeException("growFactor", SR.Format("Queue grow factor must be between {0} and {1}.", 1, 10));
			}
			this._array = new object[capacity];
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._growFactor = (int)(growFactor * 100f);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that contains elements copied from the specified collection, has the same initial capacity as the number of elements copied, and uses the default growth factor.</summary>
		/// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is null. </exception>
		// Token: 0x0600387F RID: 14463 RVA: 0x000DC860 File Offset: 0x000DAA60
		public Queue(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Enqueue(obj);
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x000DC8AB File Offset: 0x000DAAAB
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003881 RID: 14465 RVA: 0x000DC8B4 File Offset: 0x000DAAB4
		public virtual object Clone()
		{
			Queue queue = new Queue(this._size);
			queue._size = this._size;
			int num = this._size;
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, queue._array, 0, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, queue._array, this._array.Length - this._head, num);
			}
			queue._version = this._version;
			return queue;
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x000DC955 File Offset: 0x000DAB55
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Queue" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Queue" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Collections.Queue" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003884 RID: 14468 RVA: 0x000DC978 File Offset: 0x000DAB78
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
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, array, index, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <param name="obj">The object to add to the <see cref="T:System.Collections.Queue" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003885 RID: 14469 RVA: 0x000DCA48 File Offset: 0x000DAC48
		public virtual void Enqueue(object obj)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * (long)this._growFactor / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = obj;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
			this._version++;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003886 RID: 14470 RVA: 0x000DCADC File Offset: 0x000DACDC
		public virtual IEnumerator GetEnumerator()
		{
			return new Queue.QueueEnumerator(this);
		}

		/// <summary>Removes and returns the object at the beginning of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The object that is removed from the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003887 RID: 14471 RVA: 0x000DCAE4 File Offset: 0x000DACE4
		public virtual object Dequeue()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("Queue empty.");
			}
			object obj = this._array[this._head];
			this._array[this._head] = null;
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			this._version++;
			return obj;
		}

		/// <summary>Returns the object at the beginning of the <see cref="T:System.Collections.Queue" /> without removing it.</summary>
		/// <returns>The object at the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003888 RID: 14472 RVA: 0x000DCB52 File Offset: 0x000DAD52
		public virtual object Peek()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("Queue empty.");
			}
			return this._array[this._head];
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000DCB74 File Offset: 0x000DAD74
		internal object GetElement(int i)
		{
			return this._array[(this._head + i) % this._array.Length];
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000DCB90 File Offset: 0x000DAD90
		private void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		// Token: 0x04001E60 RID: 7776
		private object[] _array;

		// Token: 0x04001E61 RID: 7777
		private int _head;

		// Token: 0x04001E62 RID: 7778
		private int _tail;

		// Token: 0x04001E63 RID: 7779
		private int _size;

		// Token: 0x04001E64 RID: 7780
		private int _growFactor;

		// Token: 0x04001E65 RID: 7781
		private int _version;

		// Token: 0x04001E66 RID: 7782
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x0200070B RID: 1803
		[Serializable]
		private class QueueEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x0600388B RID: 14475 RVA: 0x000DCC50 File Offset: 0x000DAE50
			internal QueueEnumerator(Queue q)
			{
				this._q = q;
				this._version = this._q._version;
				this._index = 0;
				this._currentElement = this._q._array;
				if (this._q._size == 0)
				{
					this._index = -1;
				}
			}

			// Token: 0x0600388C RID: 14476 RVA: 0x00019FBE File Offset: 0x000181BE
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x0600388D RID: 14477 RVA: 0x000DCCA8 File Offset: 0x000DAEA8
			public virtual bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < 0)
				{
					this._currentElement = this._q._array;
					return false;
				}
				this._currentElement = this._q.GetElement(this._index);
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -1;
				}
				return true;
			}

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x0600388E RID: 14478 RVA: 0x000DCD2F File Offset: 0x000DAF2F
			public virtual object Current
			{
				get
				{
					if (this._currentElement != this._q._array)
					{
						return this._currentElement;
					}
					if (this._index == 0)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					throw new InvalidOperationException("Enumeration already finished.");
				}
			}

			// Token: 0x0600388F RID: 14479 RVA: 0x000DCD68 File Offset: 0x000DAF68
			public virtual void Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._q._size == 0)
				{
					this._index = -1;
				}
				else
				{
					this._index = 0;
				}
				this._currentElement = this._q._array;
			}

			// Token: 0x04001E67 RID: 7783
			private Queue _q;

			// Token: 0x04001E68 RID: 7784
			private int _index;

			// Token: 0x04001E69 RID: 7785
			private int _version;

			// Token: 0x04001E6A RID: 7786
			private object _currentElement;
		}

		// Token: 0x0200070C RID: 1804
		internal class QueueDebugView
		{
		}
	}
}
