using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000760 RID: 1888
	[TypeForwardedFrom("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(QueueDebugView<>))]
	[Serializable]
	public class Queue<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x06003C19 RID: 15385 RVA: 0x000E8109 File Offset: 0x000E6309
		public Queue()
		{
			this._array = Array.Empty<T>();
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x000E811C File Offset: 0x000E631C
		public Queue(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this._array = new T[capacity];
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000E814A File Offset: 0x000E634A
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x000E8152 File Offset: 0x000E6352
		object ICollection.SyncRoot
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

		// Token: 0x06003C1E RID: 15390 RVA: 0x000E8174 File Offset: 0x000E6374
		public void Clear()
		{
			if (this._size != 0)
			{
				if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
				{
					if (this._head < this._tail)
					{
						Array.Clear(this._array, this._head, this._size);
					}
					else
					{
						Array.Clear(this._array, this._head, this._array.Length - this._head);
						Array.Clear(this._array, 0, this._tail);
					}
				}
				this._size = 0;
			}
			this._head = 0;
			this._tail = 0;
			this._version++;
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x000E820C File Offset: 0x000E640C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			int length = array.Length;
			if (index < 0 || index > length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (length - index < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			try
			{
				int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
				Array.Copy(this._array, this._head, array, index, num2);
				num -= num2;
				if (num > 0)
				{
					Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x000E8324 File Offset: 0x000E6524
		public void Enqueue(T item)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * 200L / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = item;
			this.MoveNext(ref this._tail);
			this._size++;
			this._version++;
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x000E83B0 File Offset: 0x000E65B0
		public Queue<T>.Enumerator GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x000E83B8 File Offset: 0x000E65B8
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x000E83B8 File Offset: 0x000E65B8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x000E83C8 File Offset: 0x000E65C8
		public T Dequeue()
		{
			int head = this._head;
			T[] array = this._array;
			if (this._size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			T t = array[head];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				array[head] = default(T);
			}
			this.MoveNext(ref this._head);
			this._size--;
			this._version++;
			return t;
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x000E8437 File Offset: 0x000E6637
		public T Peek()
		{
			if (this._size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			return this._array[this._head];
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x000E8458 File Offset: 0x000E6658
		public bool TryPeek(out T result)
		{
			if (this._size == 0)
			{
				result = default(T);
				return false;
			}
			result = this._array[this._head];
			return true;
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x000E8484 File Offset: 0x000E6684
		public bool Contains(T item)
		{
			if (this._size == 0)
			{
				return false;
			}
			if (this._head < this._tail)
			{
				return Array.IndexOf<T>(this._array, item, this._head, this._size) >= 0;
			}
			return Array.IndexOf<T>(this._array, item, this._head, this._array.Length - this._head) >= 0 || Array.IndexOf<T>(this._array, item, 0, this._tail) >= 0;
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x000E8508 File Offset: 0x000E6708
		public T[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._size];
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x000E85A0 File Offset: 0x000E67A0
		private void SetCapacity(int capacity)
		{
			T[] array = new T[capacity];
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

		// Token: 0x06003C2A RID: 15402 RVA: 0x000E8660 File Offset: 0x000E6860
		private void MoveNext(ref int index)
		{
			int num = index + 1;
			if (num == this._array.Length)
			{
				num = 0;
			}
			index = num;
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x000E8682 File Offset: 0x000E6882
		private void ThrowForEmptyQueue()
		{
			throw new InvalidOperationException("Queue empty.");
		}

		// Token: 0x04001F37 RID: 7991
		private T[] _array;

		// Token: 0x04001F38 RID: 7992
		private int _head;

		// Token: 0x04001F39 RID: 7993
		private int _tail;

		// Token: 0x04001F3A RID: 7994
		private int _size;

		// Token: 0x04001F3B RID: 7995
		private int _version;

		// Token: 0x04001F3C RID: 7996
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000761 RID: 1889
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06003C2C RID: 15404 RVA: 0x000E868E File Offset: 0x000E688E
			internal Enumerator(Queue<T> q)
			{
				this._q = q;
				this._version = q._version;
				this._index = -1;
				this._currentElement = default(T);
			}

			// Token: 0x06003C2D RID: 15405 RVA: 0x000E86B6 File Offset: 0x000E68B6
			public void Dispose()
			{
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x06003C2E RID: 15406 RVA: 0x000E86CC File Offset: 0x000E68CC
			public bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index == -2)
				{
					return false;
				}
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -2;
					this._currentElement = default(T);
					return false;
				}
				T[] array = this._q._array;
				int num = array.Length;
				int num2 = this._q._head + this._index;
				if (num2 >= num)
				{
					num2 -= num;
				}
				this._currentElement = array[num2];
				return true;
			}

			// Token: 0x170009CD RID: 2509
			// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000E8773 File Offset: 0x000E6973
			public T Current
			{
				get
				{
					if (this._index < 0)
					{
						this.ThrowEnumerationNotStartedOrEnded();
					}
					return this._currentElement;
				}
			}

			// Token: 0x06003C30 RID: 15408 RVA: 0x000E878A File Offset: 0x000E698A
			private void ThrowEnumerationNotStartedOrEnded()
			{
				throw new InvalidOperationException((this._index == -1) ? "Enumeration has not started. Call MoveNext." : "Enumeration already finished.");
			}

			// Token: 0x170009CE RID: 2510
			// (get) Token: 0x06003C31 RID: 15409 RVA: 0x000E87A6 File Offset: 0x000E69A6
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003C32 RID: 15410 RVA: 0x000E87B3 File Offset: 0x000E69B3
			void IEnumerator.Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -1;
				this._currentElement = default(T);
			}

			// Token: 0x04001F3D RID: 7997
			private readonly Queue<T> _q;

			// Token: 0x04001F3E RID: 7998
			private readonly int _version;

			// Token: 0x04001F3F RID: 7999
			private int _index;

			// Token: 0x04001F40 RID: 8000
			private T _currentElement;
		}
	}
}
