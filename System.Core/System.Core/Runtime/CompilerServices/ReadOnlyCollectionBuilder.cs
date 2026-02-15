using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace System.Runtime.CompilerServices
{
	/// <summary>The builder for read only collection.</summary>
	/// <typeparam name="T">The type of the collection element.</typeparam>
	// Token: 0x02000118 RID: 280
	[Serializable]
	public sealed class ReadOnlyCollectionBuilder<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		/// <summary>Constructs a ReadOnlyCollectionBuilder.</summary>
		// Token: 0x06000978 RID: 2424 RVA: 0x0002573F File Offset: 0x0002393F
		public ReadOnlyCollectionBuilder()
		{
			this._items = Array.Empty<T>();
		}

		/// <summary>Constructs a ReadOnlyCollectionBuilder with a given initial capacity. The contents are empty but builder will have reserved room for the given number of elements before any reallocations are required.</summary>
		/// <param name="capacity">Initial capacity.</param>
		// Token: 0x06000979 RID: 2425 RVA: 0x00025752 File Offset: 0x00023952
		public ReadOnlyCollectionBuilder(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this._items = new T[capacity];
		}

		/// <summary>Gets and sets the capacity of this ReadOnlyCollectionBuilder.</summary>
		/// <returns>The capacity of this ReadOnlyCollectionBuilder.</returns>
		// Token: 0x17000193 RID: 403
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x00025778 File Offset: 0x00023978
		public int Capacity
		{
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = Array.Empty<T>();
				}
			}
		}

		/// <summary>Returns number of elements in the ReadOnlyCollectionBuilder.</summary>
		/// <returns>The number of elements in the ReadOnlyCollectionBuilder.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x000257DF File Offset: 0x000239DF
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Returns the index of the first occurrence of a given value in the builder.</summary>
		/// <returns>The index of the first occurrence of an item.</returns>
		/// <param name="item">An item to search for.</param>
		// Token: 0x0600097C RID: 2428 RVA: 0x000257E7 File Offset: 0x000239E7
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		/// <summary>Inserts an item to the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which item should be inserted.</param>
		/// <param name="item">The object to insert into the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x0600097D RID: 2429 RVA: 0x000257FC File Offset: 0x000239FC
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		/// <summary>Removes the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x0600097E RID: 2430 RVA: 0x0002588C File Offset: 0x00023A8C
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000195 RID: 405
		public T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._items[index];
			}
			set
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._items[index] = value;
				this._version++;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x06000981 RID: 2433 RVA: 0x00025960 File Offset: 0x00023B60
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		/// <summary>Removes all items from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		// Token: 0x06000982 RID: 2434 RVA: 0x000259B6 File Offset: 0x00023BB6
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		/// <summary>Determines whether the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> contains a specific value</summary>
		/// <returns>true if item is found in the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />; otherwise, false.</returns>
		/// <param name="item">the object to locate in the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x06000983 RID: 2435 RVA: 0x000259E8 File Offset: 0x00023BE8
		public bool Contains(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int j = 0; j < this._size; j++)
			{
				if (@default.Equals(this._items[j], item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> to an <see cref="T:System.Array" />, starting at particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
		// Token: 0x06000984 RID: 2436 RVA: 0x00025A54 File Offset: 0x00023C54
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0000B252 File Offset: 0x00009452
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</summary>
		/// <returns>true if item was successfully removed from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</returns>
		/// <param name="item">The object to remove from the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</param>
		// Token: 0x06000986 RID: 2438 RVA: 0x00025A6C File Offset: 0x00023C6C
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000987 RID: 2439 RVA: 0x00025A8F File Offset: 0x00023C8F
		public IEnumerator<T> GetEnumerator()
		{
			return new ReadOnlyCollectionBuilder<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000988 RID: 2440 RVA: 0x00025A97 File Offset: 0x00023C97
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0000B252 File Offset: 0x00009452
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600098A RID: 2442 RVA: 0x00025AA0 File Offset: 0x00023CA0
		int IList.Add(object value)
		{
			ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
			try
			{
				this.Add((T)((object)value));
			}
			catch (InvalidCastException)
			{
				throw Error.InvalidTypeException(value, typeof(T), "value");
			}
			return this.Count - 1;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600098B RID: 2443 RVA: 0x00025AF8 File Offset: 0x00023CF8
		bool IList.Contains(object value)
		{
			return ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="item" /> if found in the list; otherwise, –1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600098C RID: 2444 RVA: 0x00025B10 File Offset: 0x00023D10
		int IList.IndexOf(object value)
		{
			if (ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="value">The object to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600098D RID: 2445 RVA: 0x00025B28 File Offset: 0x00023D28
		void IList.Insert(int index, object value)
		{
			ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
			try
			{
				this.Insert(index, (T)((object)value));
			}
			catch (InvalidCastException)
			{
				throw Error.InvalidTypeException(value, typeof(T), "value");
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0000B252 File Offset: 0x00009452
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x0600098F RID: 2447 RVA: 0x00025B78 File Offset: 0x00023D78
		void IList.Remove(object value)
		{
			if (ReadOnlyCollectionBuilder<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000199 RID: 409
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				ReadOnlyCollectionBuilder<T>.ValidateNullValue(value, "value");
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					throw Error.InvalidTypeException(value, typeof(T), "value");
				}
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000992 RID: 2450 RVA: 0x00025BF0 File Offset: 0x00023DF0
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("array");
			}
			Array.Copy(this._items, 0, array, index, this._size);
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0000B252 File Offset: 0x00009452
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00002050 File Offset: 0x00000250
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> to a new array.</summary>
		/// <returns>An array containing copies of the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />.</returns>
		// Token: 0x06000995 RID: 2453 RVA: 0x00025C28 File Offset: 0x00023E28
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Creates a <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> containing all of the elements of the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" />, avoiding copying the elements to the new array if possible. Resets the <see cref="T:System.Runtime.CompilerServices.ReadOnlyCollectionBuilder`1" /> after the <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> has been created.</summary>
		/// <returns>A new instance of <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" />.</returns>
		// Token: 0x06000996 RID: 2454 RVA: 0x00025C58 File Offset: 0x00023E58
		public ReadOnlyCollection<T> ToReadOnlyCollection()
		{
			T[] array;
			if (this._size == this._items.Length)
			{
				array = this._items;
			}
			else
			{
				array = this.ToArray();
			}
			this._items = Array.Empty<T>();
			this._size = 0;
			this._version++;
			return new TrueReadOnlyCollection<T>(array);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00025CAC File Offset: 0x00023EAC
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = 4;
				if (this._items.Length != 0)
				{
					num = this._items.Length * 2;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00025CE8 File Offset: 0x00023EE8
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00025D18 File Offset: 0x00023F18
		private static void ValidateNullValue(object value, string argument)
		{
			if (value == null && default(T) != null)
			{
				throw Error.InvalidNullValue(typeof(T), argument);
			}
		}

		// Token: 0x040002F1 RID: 753
		private T[] _items;

		// Token: 0x040002F2 RID: 754
		private int _size;

		// Token: 0x040002F3 RID: 755
		private int _version;

		// Token: 0x02000119 RID: 281
		[Serializable]
		private class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600099A RID: 2458 RVA: 0x00025D49 File Offset: 0x00023F49
			internal Enumerator(ReadOnlyCollectionBuilder<T> builder)
			{
				this._builder = builder;
				this._version = builder._version;
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x0600099B RID: 2459 RVA: 0x00025D77 File Offset: 0x00023F77
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x0600099C RID: 2460 RVA: 0x0000A01D File Offset: 0x0000821D
			public void Dispose()
			{
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x0600099D RID: 2461 RVA: 0x00025D7F File Offset: 0x00023F7F
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index > this._builder._size)
					{
						throw Error.EnumerationIsDone();
					}
					return this._current;
				}
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x00025DB0 File Offset: 0x00023FB0
			public bool MoveNext()
			{
				if (this._version != this._builder._version)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				if (this._index < this._builder._size)
				{
					T[] items = this._builder._items;
					int index = this._index;
					this._index = index + 1;
					this._current = items[index];
					return true;
				}
				this._index = this._builder._size + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x0600099F RID: 2463 RVA: 0x00025E32 File Offset: 0x00024032
			void IEnumerator.Reset()
			{
				if (this._version != this._builder._version)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x040002F4 RID: 756
			private readonly ReadOnlyCollectionBuilder<T> _builder;

			// Token: 0x040002F5 RID: 757
			private readonly int _version;

			// Token: 0x040002F6 RID: 758
			private int _index;

			// Token: 0x040002F7 RID: 759
			private T _current;
		}
	}
}
