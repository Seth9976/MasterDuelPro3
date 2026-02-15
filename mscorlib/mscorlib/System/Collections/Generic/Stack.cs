using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000763 RID: 1891
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(StackDebugView<>))]
	[TypeForwardedFrom("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Stack<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x06003C33 RID: 15411 RVA: 0x000E87E6 File Offset: 0x000E69E6
		public Stack()
		{
			this._array = Array.Empty<T>();
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x000E87F9 File Offset: 0x000E69F9
		public Stack(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this._array = new T[capacity];
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000E8827 File Offset: 0x000E6A27
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x000E882F File Offset: 0x000E6A2F
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

		// Token: 0x06003C38 RID: 15416 RVA: 0x000E8851 File Offset: 0x000E6A51
		public void Clear()
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				Array.Clear(this._array, 0, this._size);
			}
			this._size = 0;
			this._version++;
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x000E8881 File Offset: 0x000E6A81
		public bool Contains(T item)
		{
			return this._size != 0 && Array.LastIndexOf<T>(this._array, item, this._size - 1) != -1;
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x000E88A8 File Offset: 0x000E6AA8
		void ICollection.CopyTo(Array array, int arrayIndex)
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
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			try
			{
				Array.Copy(this._array, 0, array, arrayIndex, this._size);
				Array.Reverse(array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x000E8978 File Offset: 0x000E6B78
		public Stack<T>.Enumerator GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x000E8980 File Offset: 0x000E6B80
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x000E8980 File Offset: 0x000E6B80
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x000E8990 File Offset: 0x000E6B90
		public T Peek()
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				this.ThrowForEmptyStack();
			}
			return array[num];
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x000E89C0 File Offset: 0x000E6BC0
		public T Pop()
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				this.ThrowForEmptyStack();
			}
			this._version++;
			this._size = num;
			T t = array[num];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				array[num] = default(T);
			}
			return t;
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x000E8A1C File Offset: 0x000E6C1C
		public bool TryPop(out T result)
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				result = default(T);
				return false;
			}
			this._version++;
			this._size = num;
			result = array[num];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				array[num] = default(T);
			}
			return true;
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x000E8A84 File Offset: 0x000E6C84
		public void Push(T item)
		{
			int size = this._size;
			T[] array = this._array;
			if (size < array.Length)
			{
				array[size] = item;
				this._version++;
				this._size = size + 1;
				return;
			}
			this.PushWithResize(item);
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x000E8ACC File Offset: 0x000E6CCC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void PushWithResize(T item)
		{
			Array.Resize<T>(ref this._array, (this._array.Length == 0) ? 4 : (2 * this._array.Length));
			this._array[this._size] = item;
			this._version++;
			this._size++;
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x000E8B28 File Offset: 0x000E6D28
		private void ThrowForEmptyStack()
		{
			throw new InvalidOperationException("Stack empty.");
		}

		// Token: 0x04001F41 RID: 8001
		private T[] _array;

		// Token: 0x04001F42 RID: 8002
		private int _size;

		// Token: 0x04001F43 RID: 8003
		private int _version;

		// Token: 0x04001F44 RID: 8004
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000764 RID: 1892
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06003C44 RID: 15428 RVA: 0x000E8B34 File Offset: 0x000E6D34
			internal Enumerator(Stack<T> stack)
			{
				this._stack = stack;
				this._version = stack._version;
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x06003C45 RID: 15429 RVA: 0x000E8B5D File Offset: 0x000E6D5D
			public void Dispose()
			{
				this._index = -1;
			}

			// Token: 0x06003C46 RID: 15430 RVA: 0x000E8B68 File Offset: 0x000E6D68
			public bool MoveNext()
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
				this._currentElement = default(T);
				return flag2;
			}

			// Token: 0x170009D2 RID: 2514
			// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000E8C2A File Offset: 0x000E6E2A
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

			// Token: 0x06003C48 RID: 15432 RVA: 0x000E8C41 File Offset: 0x000E6E41
			private void ThrowEnumerationNotStartedOrEnded()
			{
				throw new InvalidOperationException((this._index == -2) ? "Enumeration has not started. Call MoveNext." : "Enumeration already finished.");
			}

			// Token: 0x170009D3 RID: 2515
			// (get) Token: 0x06003C49 RID: 15433 RVA: 0x000E8C5E File Offset: 0x000E6E5E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003C4A RID: 15434 RVA: 0x000E8C6B File Offset: 0x000E6E6B
			void IEnumerator.Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x04001F45 RID: 8005
			private readonly Stack<T> _stack;

			// Token: 0x04001F46 RID: 8006
			private readonly int _version;

			// Token: 0x04001F47 RID: 8007
			private int _index;

			// Token: 0x04001F48 RID: 8008
			private T _currentElement;
		}
	}
}
