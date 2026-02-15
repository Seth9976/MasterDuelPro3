using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted by key based on the associated <see cref="T:System.Collections.Generic.IComparer`1" /> implementation. </summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TValue">The type of values in the collection.</typeparam>
	// Token: 0x02000329 RID: 809
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedList<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		// Token: 0x060013F4 RID: 5108 RVA: 0x00056E7F File Offset: 0x0005507F
		public SortedList()
		{
			this.keys = Array.Empty<TKey>();
			this.values = Array.Empty<TValue>();
			this._size = 0;
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x060013F5 RID: 5109 RVA: 0x00056EB0 File Offset: 0x000550B0
		public SortedList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this.keys = new TKey[capacity];
			this.values = new TValue[capacity];
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.-or-null to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x060013F6 RID: 5110 RVA: 0x00056F00 File Offset: 0x00055100
		public SortedList(IComparer<TKey> comparer)
			: this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x060013F7 RID: 5111 RVA: 0x00056F14 File Offset: 0x00055114
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key), "key");
			}
			this.Insert(~num, key, value);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00056F77 File Offset: 0x00055177
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00056F90 File Offset: 0x00055190
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00056FD4 File Offset: 0x000551D4
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value))
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.Generic.SortedList`2.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x1700044C RID: 1100
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x0005701C File Offset: 0x0005521C
		public int Capacity
		{
			set
			{
				if (value != this.keys.Length)
				{
					if (value < this._size)
					{
						throw new ArgumentOutOfRangeException("value", value, "capacity was less than the current size.");
					}
					if (value > 0)
					{
						TKey[] array = new TKey[value];
						TValue[] array2 = new TValue[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, array, 0, this._size);
							Array.Copy(this.values, 0, array2, 0, this._size);
						}
						this.keys = array;
						this.values = array2;
						return;
					}
					this.keys = Array.Empty<TKey>();
					this.values = Array.Empty<TValue>();
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> for the sorted list. </summary>
		/// <returns>The <see cref="T:System.IComparable`1" /> for the current <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x000570BE File Offset: 0x000552BE
		public IComparer<TKey> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.-or-<paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.-or-An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x060013FD RID: 5117 RVA: 0x000570C8 File Offset: 0x000552C8
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null && default(TValue) != null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(key is TKey))
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", key, typeof(TKey)), "key");
			}
			if (!(value is TValue) && value != null)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
			}
			this.Add((TKey)((object)key), (TValue)((object)value));
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00057166 File Offset: 0x00055366
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0005716E File Offset: 0x0005536E
		public IList<TKey> Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0005716E File Offset: 0x0005536E
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0005716E File Offset: 0x0005536E
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0005716E File Offset: 0x0005536E
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00057176 File Offset: 0x00055376
		public IList<TValue> Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00057176 File Offset: 0x00055376
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00057176 File Offset: 0x00055376
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00057176 File Offset: 0x00055376
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0005717E File Offset: 0x0005537E
		private SortedList<TKey, TValue>.KeyList GetKeyListHelper()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList<TKey, TValue>.KeyList(this);
			}
			return this.keyList;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0005719A File Offset: 0x0005539A
		private SortedList<TKey, TValue>.ValueList GetValueListHelper()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList<TKey, TValue>.ValueList(this);
			}
			return this.valueList;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000028AE File Offset: 0x00000AAE
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x000028AE File Offset: 0x00000AAE
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns false.</returns>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns the current instance.</returns>
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x000571B6 File Offset: 0x000553B6
		object ICollection.SyncRoot
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

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		// Token: 0x0600140E RID: 5134 RVA: 0x000571D8 File Offset: 0x000553D8
		public void Clear()
		{
			this.version++;
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
			{
				Array.Clear(this.keys, 0, this._size);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
			{
				Array.Clear(this.values, 0, this._size);
			}
			this._size = 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600140F RID: 5135 RVA: 0x0005722C File Offset: 0x0005542C
		bool IDictionary.Contains(object key)
		{
			return SortedList<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06001410 RID: 5136 RVA: 0x00057244 File Offset: 0x00055444
		public bool ContainsKey(TKey key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />. The value can be null for reference types.</param>
		// Token: 0x06001411 RID: 5137 RVA: 0x00057253 File Offset: 0x00055453
		public bool ContainsValue(TValue value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00057264 File Offset: 0x00055464
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				array[arrayIndex + i] = keyValuePair;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06001413 RID: 5139 RVA: 0x000572F4 File Offset: 0x000554F4
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
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					array2[i + index] = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				}
				return;
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				for (int j = 0; j < this.Count; j++)
				{
					array3[j + index] = new KeyValuePair<TKey, TValue>(this.keys[j], this.values[j]);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00057438 File Offset: 0x00055638
		private void EnsureCapacity(int min)
		{
			int num = ((this.keys.Length == 0) ? 4 : (this.keys.Length * 2));
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00057477 File Offset: 0x00055677
		private TValue GetByIndex(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.values[index];
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> of type <see cref="T:System.Collections.Generic.KeyValuePair`2" /> for the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x06001416 RID: 5142 RVA: 0x000574A8 File Offset: 0x000556A8
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x000574A8 File Offset: 0x000556A8
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06001418 RID: 5144 RVA: 0x000574B6 File Offset: 0x000556B6
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001419 RID: 5145 RVA: 0x000574A8 File Offset: 0x000556A8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x000574C4 File Offset: 0x000556C4
		private TKey GetKey(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.keys[index];
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" /> and a set operation creates a new element using the specified key.</returns>
		/// <param name="key">The key whose value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x1700045C RID: 1116
		public TValue this[TKey key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The element with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <param name="key">The key of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x1700045D RID: 1117
		object IDictionary.this[object key]
		{
			get
			{
				if (SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.IndexOfKey((TKey)((object)key));
					if (num >= 0)
					{
						return this.values[num];
					}
				}
				return null;
			}
			set
			{
				if (!SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					throw new ArgumentNullException("key");
				}
				if (value == null && default(TValue) != null)
				{
					throw new ArgumentNullException("value");
				}
				TKey tkey = (TKey)((object)key);
				try
				{
					this[tkey] = (TValue)((object)value);
				}
				catch (InvalidCastException)
				{
					throw new ArgumentException(SR.Format("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", value, typeof(TValue)), "value");
				}
			}
		}

		/// <summary>Searches for the specified key and returns the zero-based index within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The zero-based index of <paramref name="key" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x0600141F RID: 5151 RVA: 0x00057664 File Offset: 0x00055864
		public int IndexOfKey(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Searches for the specified value and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.  The value can be null for reference types.</param>
		// Token: 0x06001420 RID: 5152 RVA: 0x000576A5 File Offset: 0x000558A5
		public int IndexOfValue(TValue value)
		{
			return Array.IndexOf<TValue>(this.values, value, 0, this._size);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000576BC File Offset: 0x000558BC
		private void Insert(int index, TKey key, TValue value)
		{
			if (this._size == this.keys.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
				Array.Copy(this.values, index, this.values, index + 1, this._size - index);
			}
			this.keys[index] = key;
			this.values[index] = value;
			this._size++;
			this.version++;
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06001422 RID: 5154 RVA: 0x00057760 File Offset: 0x00055960
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				value = this.values[num];
				return true;
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		// Token: 0x06001423 RID: 5155 RVA: 0x00057798 File Offset: 0x00055998
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
			{
				this.keys[this._size] = default(TKey);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
			{
				this.values[this._size] = default(TValue);
			}
			this.version++;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06001424 RID: 5156 RVA: 0x0005786C File Offset: 0x00055A6C
		public bool Remove(TKey key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
			return num >= 0;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06001425 RID: 5157 RVA: 0x00057893 File Offset: 0x00055A93
		void IDictionary.Remove(object key)
		{
			if (SortedList<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x000565DC File Offset: 0x000547DC
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		// Token: 0x04000BC9 RID: 3017
		private TKey[] keys;

		// Token: 0x04000BCA RID: 3018
		private TValue[] values;

		// Token: 0x04000BCB RID: 3019
		private int _size;

		// Token: 0x04000BCC RID: 3020
		private int version;

		// Token: 0x04000BCD RID: 3021
		private IComparer<TKey> comparer;

		// Token: 0x04000BCE RID: 3022
		private SortedList<TKey, TValue>.KeyList keyList;

		// Token: 0x04000BCF RID: 3023
		private SortedList<TKey, TValue>.ValueList valueList;

		// Token: 0x04000BD0 RID: 3024
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x0200032A RID: 810
		[Serializable]
		private struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06001427 RID: 5159 RVA: 0x000578AA File Offset: 0x00055AAA
			internal Enumerator(SortedList<TKey, TValue> sortedList, int getEnumeratorRetType)
			{
				this._sortedList = sortedList;
				this._index = 0;
				this._version = this._sortedList.version;
				this._getEnumeratorRetType = getEnumeratorRetType;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x06001428 RID: 5160 RVA: 0x000578EA File Offset: 0x00055AEA
			public void Dispose()
			{
				this._index = 0;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x1700045E RID: 1118
			// (get) Token: 0x06001429 RID: 5161 RVA: 0x0005790B File Offset: 0x00055B0B
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._key;
				}
			}

			// Token: 0x0600142A RID: 5162 RVA: 0x00057940 File Offset: 0x00055B40
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._key = this._sortedList.keys[this._index];
					this._value = this._sortedList.values[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._key = default(TKey);
				this._value = default(TValue);
				return false;
			}

			// Token: 0x1700045F RID: 1119
			// (get) Token: 0x0600142B RID: 5163 RVA: 0x000579F4 File Offset: 0x00055BF4
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x17000460 RID: 1120
			// (get) Token: 0x0600142C RID: 5164 RVA: 0x00057A44 File Offset: 0x00055C44
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000461 RID: 1121
			// (get) Token: 0x0600142D RID: 5165 RVA: 0x00057A58 File Offset: 0x00055C58
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this._getEnumeratorRetType == 2)
					{
						return new DictionaryEntry(this._key, this._value);
					}
					return new KeyValuePair<TKey, TValue>(this._key, this._value);
				}
			}

			// Token: 0x17000462 RID: 1122
			// (get) Token: 0x0600142E RID: 5166 RVA: 0x00057ACD File Offset: 0x00055CCD
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._value;
				}
			}

			// Token: 0x0600142F RID: 5167 RVA: 0x00057B02 File Offset: 0x00055D02
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._key = default(TKey);
				this._value = default(TValue);
			}

			// Token: 0x04000BD1 RID: 3025
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04000BD2 RID: 3026
			private TKey _key;

			// Token: 0x04000BD3 RID: 3027
			private TValue _value;

			// Token: 0x04000BD4 RID: 3028
			private int _index;

			// Token: 0x04000BD5 RID: 3029
			private int _version;

			// Token: 0x04000BD6 RID: 3030
			private int _getEnumeratorRetType;
		}

		// Token: 0x0200032B RID: 811
		[Serializable]
		private sealed class SortedListKeyEnumerator : IEnumerator<TKey>, IDisposable, IEnumerator
		{
			// Token: 0x06001430 RID: 5168 RVA: 0x00057B41 File Offset: 0x00055D41
			internal SortedListKeyEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this._version = sortedList.version;
			}

			// Token: 0x06001431 RID: 5169 RVA: 0x00057B5C File Offset: 0x00055D5C
			public void Dispose()
			{
				this._index = 0;
				this._currentKey = default(TKey);
			}

			// Token: 0x06001432 RID: 5170 RVA: 0x00057B74 File Offset: 0x00055D74
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._currentKey = this._sortedList.keys[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._currentKey = default(TKey);
				return false;
			}

			// Token: 0x17000463 RID: 1123
			// (get) Token: 0x06001433 RID: 5171 RVA: 0x00057BFE File Offset: 0x00055DFE
			public TKey Current
			{
				get
				{
					return this._currentKey;
				}
			}

			// Token: 0x17000464 RID: 1124
			// (get) Token: 0x06001434 RID: 5172 RVA: 0x00057C06 File Offset: 0x00055E06
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentKey;
				}
			}

			// Token: 0x06001435 RID: 5173 RVA: 0x00057C3B File Offset: 0x00055E3B
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._currentKey = default(TKey);
			}

			// Token: 0x04000BD7 RID: 3031
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04000BD8 RID: 3032
			private int _index;

			// Token: 0x04000BD9 RID: 3033
			private int _version;

			// Token: 0x04000BDA RID: 3034
			private TKey _currentKey;
		}

		// Token: 0x0200032C RID: 812
		[Serializable]
		private sealed class SortedListValueEnumerator : IEnumerator<TValue>, IDisposable, IEnumerator
		{
			// Token: 0x06001436 RID: 5174 RVA: 0x00057C6E File Offset: 0x00055E6E
			internal SortedListValueEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this._version = sortedList.version;
			}

			// Token: 0x06001437 RID: 5175 RVA: 0x00057C89 File Offset: 0x00055E89
			public void Dispose()
			{
				this._index = 0;
				this._currentValue = default(TValue);
			}

			// Token: 0x06001438 RID: 5176 RVA: 0x00057CA0 File Offset: 0x00055EA0
			public bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._sortedList.Count)
				{
					this._currentValue = this._sortedList.values[this._index];
					this._index++;
					return true;
				}
				this._index = this._sortedList.Count + 1;
				this._currentValue = default(TValue);
				return false;
			}

			// Token: 0x17000465 RID: 1125
			// (get) Token: 0x06001439 RID: 5177 RVA: 0x00057D2A File Offset: 0x00055F2A
			public TValue Current
			{
				get
				{
					return this._currentValue;
				}
			}

			// Token: 0x17000466 RID: 1126
			// (get) Token: 0x0600143A RID: 5178 RVA: 0x00057D32 File Offset: 0x00055F32
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._sortedList.Count + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentValue;
				}
			}

			// Token: 0x0600143B RID: 5179 RVA: 0x00057D67 File Offset: 0x00055F67
			void IEnumerator.Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._currentValue = default(TValue);
			}

			// Token: 0x04000BDB RID: 3035
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04000BDC RID: 3036
			private int _index;

			// Token: 0x04000BDD RID: 3037
			private int _version;

			// Token: 0x04000BDE RID: 3038
			private TValue _currentValue;
		}

		// Token: 0x0200032D RID: 813
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		private sealed class KeyList : IList<TKey>, ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection
		{
			// Token: 0x0600143C RID: 5180 RVA: 0x00057D9A File Offset: 0x00055F9A
			internal KeyList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000467 RID: 1127
			// (get) Token: 0x0600143D RID: 5181 RVA: 0x00057DA9 File Offset: 0x00055FA9
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000468 RID: 1128
			// (get) Token: 0x0600143E RID: 5182 RVA: 0x00003BCC File Offset: 0x00001DCC
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000469 RID: 1129
			// (get) Token: 0x0600143F RID: 5183 RVA: 0x000028AE File Offset: 0x00000AAE
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700046A RID: 1130
			// (get) Token: 0x06001440 RID: 5184 RVA: 0x00057DB6 File Offset: 0x00055FB6
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x06001441 RID: 5185 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public void Add(TKey key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06001442 RID: 5186 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06001443 RID: 5187 RVA: 0x00057DCF File Offset: 0x00055FCF
			public bool Contains(TKey key)
			{
				return this._dict.ContainsKey(key);
			}

			// Token: 0x06001444 RID: 5188 RVA: 0x00057DDD File Offset: 0x00055FDD
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x06001445 RID: 5189 RVA: 0x00057E00 File Offset: 0x00056000
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				try
				{
					Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			// Token: 0x06001446 RID: 5190 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public void Insert(int index, TKey value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x1700046B RID: 1131
			public TKey this[int index]
			{
				get
				{
					return this._dict.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
				}
			}

			// Token: 0x06001449 RID: 5193 RVA: 0x00057E7A File Offset: 0x0005607A
			public IEnumerator<TKey> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x0600144A RID: 5194 RVA: 0x00057E7A File Offset: 0x0005607A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x0600144B RID: 5195 RVA: 0x00057E88 File Offset: 0x00056088
			public int IndexOf(TKey key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = Array.BinarySearch<TKey>(this._dict.keys, 0, this._dict.Count, key, this._dict.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x0600144C RID: 5196 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public bool Remove(TKey key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x0600144D RID: 5197 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x04000BDF RID: 3039
			private SortedList<TKey, TValue> _dict;
		}

		// Token: 0x0200032E RID: 814
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		private sealed class ValueList : IList<TValue>, ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection
		{
			// Token: 0x0600144E RID: 5198 RVA: 0x00057ED8 File Offset: 0x000560D8
			internal ValueList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x1700046C RID: 1132
			// (get) Token: 0x0600144F RID: 5199 RVA: 0x00057EE7 File Offset: 0x000560E7
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x1700046D RID: 1133
			// (get) Token: 0x06001450 RID: 5200 RVA: 0x00003BCC File Offset: 0x00001DCC
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700046E RID: 1134
			// (get) Token: 0x06001451 RID: 5201 RVA: 0x000028AE File Offset: 0x00000AAE
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700046F RID: 1135
			// (get) Token: 0x06001452 RID: 5202 RVA: 0x00057EF4 File Offset: 0x000560F4
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x06001453 RID: 5203 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public void Add(TValue key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06001454 RID: 5204 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x06001455 RID: 5205 RVA: 0x00057F01 File Offset: 0x00056101
			public bool Contains(TValue value)
			{
				return this._dict.ContainsValue(value);
			}

			// Token: 0x06001456 RID: 5206 RVA: 0x00057F0F File Offset: 0x0005610F
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x06001457 RID: 5207 RVA: 0x00057F30 File Offset: 0x00056130
			void ICollection.CopyTo(Array array, int index)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				try
				{
					Array.Copy(this._dict.values, 0, array, index, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
				}
			}

			// Token: 0x06001458 RID: 5208 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public void Insert(int index, TValue value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x17000470 RID: 1136
			public TValue this[int index]
			{
				get
				{
					return this._dict.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
				}
			}

			// Token: 0x0600145B RID: 5211 RVA: 0x00057FAA File Offset: 0x000561AA
			public IEnumerator<TValue> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x0600145C RID: 5212 RVA: 0x00057FAA File Offset: 0x000561AA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x0600145D RID: 5213 RVA: 0x00057FB7 File Offset: 0x000561B7
			public int IndexOf(TValue value)
			{
				return Array.IndexOf<TValue>(this._dict.values, value, 0, this._dict.Count);
			}

			// Token: 0x0600145E RID: 5214 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public bool Remove(TValue value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x0600145F RID: 5215 RVA: 0x00057DC3 File Offset: 0x00055FC3
			public void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x04000BE0 RID: 3040
			private SortedList<TKey, TValue> _dict;
		}
	}
}
