using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Unity;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a read-only, generic collection of key/value pairs.</summary>
	/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
	// Token: 0x02000737 RID: 1847
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(DictionaryDebugView<, >))]
	[Serializable]
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> class that is a wrapper around the specified dictionary.</summary>
		/// <param name="dictionary">The dictionary to wrap.</param>
		// Token: 0x06003ACF RID: 15055 RVA: 0x000E48DE File Offset: 0x000E2ADE
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.m_dictionary = dictionary;
		}

		/// <summary>Gets a key collection that contains the keys of the dictionary.</summary>
		/// <returns>A key collection that contains the keys of the dictionary.</returns>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x000E48FB File Offset: 0x000E2AFB
		public ReadOnlyDictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new ReadOnlyDictionary<TKey, TValue>.KeyCollection(this.m_dictionary.Keys);
				}
				return this._keys;
			}
		}

		/// <summary>Gets a collection that contains the values in the dictionary.</summary>
		/// <returns>A collection that contains the values in the object that implements <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.</returns>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000E4921 File Offset: 0x000E2B21
		public ReadOnlyDictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new ReadOnlyDictionary<TKey, TValue>.ValueCollection(this.m_dictionary.Values);
				}
				return this._values;
			}
		}

		/// <summary>Determines whether the dictionary contains an element that has the specified key.</summary>
		/// <returns>true if the dictionary contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the dictionary.</param>
		// Token: 0x06003AD2 RID: 15058 RVA: 0x000E4947 File Offset: 0x000E2B47
		public bool ContainsKey(TKey key)
		{
			return this.m_dictionary.ContainsKey(key);
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000E4955 File Offset: 0x000E2B55
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Retrieves the value that is associated with the specified key.</summary>
		/// <returns>true if the object that implements <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key whose value will be retrieved.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		// Token: 0x06003AD4 RID: 15060 RVA: 0x000E495D File Offset: 0x000E2B5D
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.m_dictionary.TryGetValue(key, out value);
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000E496C File Offset: 0x000E2B6C
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets the element that has the specified key.</summary>
		/// <returns>The element that has the specified key.</returns>
		/// <param name="key">The key of the element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found.</exception>
		// Token: 0x17000972 RID: 2418
		public TValue this[TKey key]
		{
			get
			{
				return this.m_dictionary[key];
			}
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000E009F File Offset: 0x000DE29F
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x000E009F File Offset: 0x000DE29F
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000973 RID: 2419
		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get
			{
				return this.m_dictionary[key];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		/// <summary>Gets the number of items in the dictionary.</summary>
		/// <returns>The number of items in the dictionary.</returns>
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x000E4982 File Offset: 0x000E2B82
		public int Count
		{
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000E498F File Offset: 0x000E2B8F
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.m_dictionary.Contains(item);
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000E499D File Offset: 0x000E2B9D
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.m_dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x0000C091 File Offset: 0x0000A291
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000E009F File Offset: 0x000DE29F
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000E009F File Offset: 0x000DE29F
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000E009F File Offset: 0x000DE29F
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06003AE2 RID: 15074 RVA: 0x000E49AC File Offset: 0x000E2BAC
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06003AE3 RID: 15075 RVA: 0x000E49B9 File Offset: 0x000E2BB9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000E49C6 File Offset: 0x000E2BC6
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="key">The key of the element to add. </param>
		/// <param name="value">The value of the element to add. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003AE5 RID: 15077 RVA: 0x000E009F File Offset: 0x000DE29F
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003AE6 RID: 15078 RVA: 0x000E009F File Offset: 0x000DE29F
		void IDictionary.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		/// <summary>Determines whether the dictionary contains an element that has the specified key.</summary>
		/// <returns>true if the dictionary contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the dictionary.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06003AE7 RID: 15079 RVA: 0x000E49DF File Offset: 0x000E2BDF
		bool IDictionary.Contains(object key)
		{
			return ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Returns an enumerator for the dictionary.</summary>
		/// <returns>An enumerator for the dictionary.</returns>
		// Token: 0x06003AE8 RID: 15080 RVA: 0x000E49F8 File Offset: 0x000E2BF8
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			IDictionary dictionary = this.m_dictionary as IDictionary;
			if (dictionary != null)
			{
				return dictionary.GetEnumerator();
			}
			return new ReadOnlyDictionary<TKey, TValue>.DictionaryEnumerator(this.m_dictionary);
		}

		/// <summary>Gets a value that indicates whether the dictionary has a fixed size.</summary>
		/// <returns>true if the dictionary has a fixed size; otherwise, false.</returns>
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x0000C091 File Offset: 0x0000A291
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary is read-only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x0000C091 File Offset: 0x0000A291
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a collection that contains the keys of the dictionary.</summary>
		/// <returns>A collection that contains the keys of the dictionary.</returns>
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000E4955 File Offset: 0x000E2B55
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003AEC RID: 15084 RVA: 0x000E009F File Offset: 0x000DE29F
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		/// <summary>Gets a collection that contains the values in the dictionary.</summary>
		/// <returns>A collection that contains the values in the dictionary.</returns>
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x000E496C File Offset: 0x000E2B6C
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets the element that has the specified key.</summary>
		/// <returns>The element that has the specified key.</returns>
		/// <param name="key">The key of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the dictionary has a fixed size. </exception>
		// Token: 0x1700097A RID: 2426
		object IDictionary.this[object key]
		{
			get
			{
				if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					return this[(TKey)((object)key)];
				}
				return null;
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		/// <summary>Copies the elements of the dictionary to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source dictionary is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or- The type of the source dictionary cannot be cast automatically to the type of the destination <paramref name="array" /><paramref name="." /></exception>
		// Token: 0x06003AF0 RID: 15088 RVA: 0x000E4A48 File Offset: 0x000E2C48
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.");
			}
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.m_dictionary.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = this.m_dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TKey, TValue> keyValuePair = enumerator.Current;
						array3[index++] = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
			}
			try
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair2 in this.m_dictionary)
				{
					array4[index++] = new KeyValuePair<TKey, TValue>(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
			}
		}

		/// <summary>Gets a value that indicates whether access to the dictionary is synchronized (thread safe).</summary>
		/// <returns>true if access to the dictionary is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the dictionary.</summary>
		/// <returns>An object that can be used to synchronize access to the dictionary.</returns>
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x000E4BD0 File Offset: 0x000E2DD0
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					ICollection collection = this.m_dictionary as ICollection;
					if (collection != null)
					{
						this._syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
				}
				return this._syncRoot;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x000E4955 File Offset: 0x000E2B55
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000E496C File Offset: 0x000E2B6C
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x04001EEE RID: 7918
		private readonly IDictionary<TKey, TValue> m_dictionary;

		// Token: 0x04001EEF RID: 7919
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001EF0 RID: 7920
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.KeyCollection _keys;

		// Token: 0x04001EF1 RID: 7921
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.ValueCollection _values;

		// Token: 0x02000738 RID: 1848
		[Serializable]
		private struct DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06003AF5 RID: 15093 RVA: 0x000E4C1A File Offset: 0x000E2E1A
			public DictionaryEnumerator(IDictionary<TKey, TValue> dictionary)
			{
				this._dictionary = dictionary;
				this._enumerator = this._dictionary.GetEnumerator();
			}

			// Token: 0x1700097F RID: 2431
			// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000E4C34 File Offset: 0x000E2E34
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this._enumerator.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x17000980 RID: 2432
			// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000E4C78 File Offset: 0x000E2E78
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x17000981 RID: 2433
			// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000E4CA0 File Offset: 0x000E2EA0
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17000982 RID: 2434
			// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000E4CC5 File Offset: 0x000E2EC5
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06003AFA RID: 15098 RVA: 0x000E4CD2 File Offset: 0x000E2ED2
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06003AFB RID: 15099 RVA: 0x000E4CDF File Offset: 0x000E2EDF
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x04001EF2 RID: 7922
			private readonly IDictionary<TKey, TValue> _dictionary;

			// Token: 0x04001EF3 RID: 7923
			private IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;
		}

		/// <summary>Represents a read-only collection of the keys of a <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		// Token: 0x02000739 RID: 1849
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x06003AFC RID: 15100 RVA: 0x000E4CEC File Offset: 0x000E2EEC
			internal KeyCollection(ICollection<TKey> collection)
			{
				if (collection == null)
				{
					throw new ArgumentNullException("collection");
				}
				this._collection = collection;
			}

			// Token: 0x06003AFD RID: 15101 RVA: 0x000E009F File Offset: 0x000DE29F
			void ICollection<TKey>.Add(TKey item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06003AFE RID: 15102 RVA: 0x000E009F File Offset: 0x000DE29F
			void ICollection<TKey>.Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06003AFF RID: 15103 RVA: 0x000E4D09 File Offset: 0x000E2F09
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this._collection.Contains(item);
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06003B00 RID: 15104 RVA: 0x000E4D17 File Offset: 0x000E2F17
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				this._collection.CopyTo(array, arrayIndex);
			}

			/// <summary>Gets the number of elements in the collection.</summary>
			/// <returns>The number of elements in the collection.</returns>
			// Token: 0x17000983 RID: 2435
			// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000E4D26 File Offset: 0x000E2F26
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x17000984 RID: 2436
			// (get) Token: 0x06003B02 RID: 15106 RVA: 0x0000C091 File Offset: 0x0000A291
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003B03 RID: 15107 RVA: 0x000E009F File Offset: 0x000DE29F
			bool ICollection<TKey>.Remove(TKey item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06003B04 RID: 15108 RVA: 0x000E4D33 File Offset: 0x000E2F33
			public IEnumerator<TKey> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06003B05 RID: 15109 RVA: 0x000E4D40 File Offset: 0x000E2F40
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06003B06 RID: 15110 RVA: 0x000E4D4D File Offset: 0x000E2F4D
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TKey>(this._collection, array, index);
			}

			/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
			// Token: 0x17000985 RID: 2437
			// (get) Token: 0x06003B07 RID: 15111 RVA: 0x00033991 File Offset: 0x00031B91
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x17000986 RID: 2438
			// (get) Token: 0x06003B08 RID: 15112 RVA: 0x000E4D5C File Offset: 0x000E2F5C
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						ICollection collection = this._collection as ICollection;
						if (collection != null)
						{
							this._syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
						}
					}
					return this._syncRoot;
				}
			}

			// Token: 0x06003B09 RID: 15113 RVA: 0x000176B9 File Offset: 0x000158B9
			internal KeyCollection()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x04001EF4 RID: 7924
			private readonly ICollection<TKey> _collection;

			// Token: 0x04001EF5 RID: 7925
			[NonSerialized]
			private object _syncRoot;
		}

		/// <summary>Represents a read-only collection of the values of a <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		// Token: 0x0200073A RID: 1850
		[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06003B0A RID: 15114 RVA: 0x000E4DA6 File Offset: 0x000E2FA6
			internal ValueCollection(ICollection<TValue> collection)
			{
				if (collection == null)
				{
					throw new ArgumentNullException("collection");
				}
				this._collection = collection;
			}

			// Token: 0x06003B0B RID: 15115 RVA: 0x000E009F File Offset: 0x000DE29F
			void ICollection<TValue>.Add(TValue item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06003B0C RID: 15116 RVA: 0x000E009F File Offset: 0x000DE29F
			void ICollection<TValue>.Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06003B0D RID: 15117 RVA: 0x000E4DC3 File Offset: 0x000E2FC3
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this._collection.Contains(item);
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06003B0E RID: 15118 RVA: 0x000E4DD1 File Offset: 0x000E2FD1
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				this._collection.CopyTo(array, arrayIndex);
			}

			/// <summary>Gets the number of elements in the collection.</summary>
			/// <returns>The number of elements in the collection.</returns>
			// Token: 0x17000987 RID: 2439
			// (get) Token: 0x06003B0F RID: 15119 RVA: 0x000E4DE0 File Offset: 0x000E2FE0
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x17000988 RID: 2440
			// (get) Token: 0x06003B10 RID: 15120 RVA: 0x0000C091 File Offset: 0x0000A291
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003B11 RID: 15121 RVA: 0x000E009F File Offset: 0x000DE29F
			bool ICollection<TValue>.Remove(TValue item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06003B12 RID: 15122 RVA: 0x000E4DED File Offset: 0x000E2FED
			public IEnumerator<TValue> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06003B13 RID: 15123 RVA: 0x000E4DFA File Offset: 0x000E2FFA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06003B14 RID: 15124 RVA: 0x000E4E07 File Offset: 0x000E3007
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TValue>(this._collection, array, index);
			}

			/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
			// Token: 0x17000989 RID: 2441
			// (get) Token: 0x06003B15 RID: 15125 RVA: 0x00033991 File Offset: 0x00031B91
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x1700098A RID: 2442
			// (get) Token: 0x06003B16 RID: 15126 RVA: 0x000E4E18 File Offset: 0x000E3018
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						ICollection collection = this._collection as ICollection;
						if (collection != null)
						{
							this._syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
						}
					}
					return this._syncRoot;
				}
			}

			// Token: 0x06003B17 RID: 15127 RVA: 0x000176B9 File Offset: 0x000158B9
			internal ValueCollection()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x04001EF6 RID: 7926
			private readonly ICollection<TValue> _collection;

			// Token: 0x04001EF7 RID: 7927
			[NonSerialized]
			private object _syncRoot;
		}
	}
}
