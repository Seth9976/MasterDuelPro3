using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of keys and values.</summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200073D RID: 1853
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.</summary>
		// Token: 0x06003B19 RID: 15129 RVA: 0x000E4F70 File Offset: 0x000E3170
		public Dictionary()
			: this(0, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x06003B1A RID: 15130 RVA: 0x000E4F7A File Offset: 0x000E317A
		public Dictionary(int capacity)
			: this(capacity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		// Token: 0x06003B1B RID: 15131 RVA: 0x000E4F84 File Offset: 0x000E3184
		public Dictionary(IEqualityComparer<TKey> comparer)
			: this(0, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x06003B1C RID: 15132 RVA: 0x000E4F8E File Offset: 0x000E318E
		public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
			if (comparer != EqualityComparer<TKey>.Default)
			{
				this._comparer = comparer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the default equality comparer for the key type.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06003B1D RID: 15133 RVA: 0x000E4FBC File Offset: 0x000E31BC
		public Dictionary(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06003B1E RID: 15134 RVA: 0x000E4FC8 File Offset: 0x000E31C8
		public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
			: this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			if (dictionary.GetType() == typeof(Dictionary<TKey, TValue>))
			{
				Dictionary<TKey, TValue> dictionary2 = (Dictionary<TKey, TValue>)dictionary;
				int count = dictionary2._count;
				Dictionary<TKey, TValue>.Entry[] entries = dictionary2._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						this.Add(entries[i].key, entries[i].value);
					}
				}
				return;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class with serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		// Token: 0x06003B1F RID: 15135 RVA: 0x000E50A0 File Offset: 0x000E32A0
		protected Dictionary(SerializationInfo info, StreamingContext context)
		{
			HashHelpers.SerializationInfoTable.Add(this, info);
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x000E50B4 File Offset: 0x000E32B4
		public int Count
		{
			get
			{
				return this._count - this._freeCount;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000E50C3 File Offset: 0x000E32C3
		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000E50C3 File Offset: 0x000E32C3
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000E50C3 File Offset: 0x000E32C3
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this._keys;
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x000E50DF File Offset: 0x000E32DF
		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x000E50DF File Offset: 0x000E32DF
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000E50DF File Offset: 0x000E32DF
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this._values;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000992 RID: 2450
		public TValue this[TKey key]
		{
			get
			{
				int num = this.FindEntry(key);
				if (num >= 0)
				{
					return this._entries[num].value;
				}
				ThrowHelper.ThrowKeyNotFoundException(key);
				return default(TValue);
			}
			set
			{
				this.TryInsert(key, value, InsertionBehavior.OverwriteExisting);
			}
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x06003B29 RID: 15145 RVA: 0x000E5147 File Offset: 0x000E3347
		public void Add(TKey key, TValue value)
		{
			this.TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000E5153 File Offset: 0x000E3353
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000E516C File Offset: 0x000E336C
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this._entries[num].value, keyValuePair.Value);
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000E51B4 File Offset: 0x000E33B4
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this._entries[num].value, keyValuePair.Value))
			{
				this.Remove(keyValuePair.Key);
				return true;
			}
			return false;
		}

		/// <summary>Removes all keys and values from the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		// Token: 0x06003B2D RID: 15149 RVA: 0x000E5208 File Offset: 0x000E3408
		public void Clear()
		{
			int count = this._count;
			if (count > 0)
			{
				Array.Clear(this._buckets, 0, this._buckets.Length);
				this._count = 0;
				this._freeList = -1;
				this._freeCount = 0;
				Array.Clear(this._entries, 0, count);
			}
			this._version++;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003B2E RID: 15150 RVA: 0x000E5264 File Offset: 0x000E3464
		public bool ContainsKey(TKey key)
		{
			return this.FindEntry(key) >= 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified value; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.Dictionary`2" />. The value can be null for reference types.</param>
		// Token: 0x06003B2F RID: 15151 RVA: 0x000E5274 File Offset: 0x000E3474
		public bool ContainsValue(TValue value)
		{
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			if (value == null)
			{
				for (int i = 0; i < this._count; i++)
				{
					if (entries[i].hashCode >= 0 && entries[i].value == null)
					{
						return true;
					}
				}
			}
			else if (default(TValue) != null)
			{
				for (int j = 0; j < this._count; j++)
				{
					if (entries[j].hashCode >= 0 && EqualityComparer<TValue>.Default.Equals(entries[j].value, value))
					{
						return true;
					}
				}
			}
			else
			{
				EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
				for (int k = 0; k < this._count; k++)
				{
					if (entries[k].hashCode >= 0 && @default.Equals(entries[k].value, value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000E5360 File Offset: 0x000E3560
		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index > array.Length)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			int count = this._count;
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			for (int i = 0; i < count; i++)
			{
				if (entries[i].hashCode >= 0)
				{
					array[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
				}
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x06003B31 RID: 15153 RVA: 0x000E53E6 File Offset: 0x000E35E6
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000E53EF File Offset: 0x000E35EF
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06003B33 RID: 15155 RVA: 0x000E5400 File Offset: 0x000E3600
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
			}
			info.AddValue("Version", this._version);
			info.AddValue("Comparer", this._comparer ?? EqualityComparer<TKey>.Default, typeof(IEqualityComparer<TKey>));
			info.AddValue("HashSize", (this._buckets == null) ? 0 : this._buckets.Length);
			if (this._buckets != null)
			{
				KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("KeyValuePairs", array, typeof(KeyValuePair<TKey, TValue>[]));
			}
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000E549C File Offset: 0x000E369C
		private int FindEntry(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			int num = -1;
			int[] buckets = this._buckets;
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			int num2 = 0;
			if (buckets != null)
			{
				IEqualityComparer<TKey> comparer = this._comparer;
				if (comparer == null)
				{
					int num3 = key.GetHashCode() & int.MaxValue;
					num = buckets[num3 % buckets.Length] - 1;
					if (default(TKey) != null)
					{
						while (num < entries.Length && (entries[num].hashCode != num3 || !EqualityComparer<TKey>.Default.Equals(entries[num].key, key)))
						{
							num = entries[num].next;
							if (num2 >= entries.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
							num2++;
						}
					}
					else
					{
						EqualityComparer<TKey> @default = EqualityComparer<TKey>.Default;
						while (num < entries.Length && (entries[num].hashCode != num3 || !@default.Equals(entries[num].key, key)))
						{
							num = entries[num].next;
							if (num2 >= entries.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
							num2++;
						}
					}
				}
				else
				{
					int num4 = comparer.GetHashCode(key) & int.MaxValue;
					num = buckets[num4 % buckets.Length] - 1;
					while (num < entries.Length && (entries[num].hashCode != num4 || !comparer.Equals(entries[num].key, key)))
					{
						num = entries[num].next;
						if (num2 >= entries.Length)
						{
							ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
						}
						num2++;
					}
				}
			}
			return num;
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000E5620 File Offset: 0x000E3820
		private int Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this._freeList = -1;
			this._buckets = new int[prime];
			this._entries = new Dictionary<TKey, TValue>.Entry[prime];
			return prime;
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x000E5654 File Offset: 0x000E3854
		private bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			this._version++;
			if (this._buckets == null)
			{
				this.Initialize(0);
			}
			Dictionary<TKey, TValue>.Entry[] array = this._entries;
			IEqualityComparer<TKey> comparer = this._comparer;
			int num = ((comparer == null) ? key.GetHashCode() : comparer.GetHashCode(key)) & int.MaxValue;
			int num2 = 0;
			ref int ptr = ref this._buckets[num % this._buckets.Length];
			int i = ptr - 1;
			if (comparer == null)
			{
				if (default(TKey) != null)
				{
					while (i < array.Length)
					{
						if (array[i].hashCode == num && EqualityComparer<TKey>.Default.Equals(array[i].key, key))
						{
							if (behavior == InsertionBehavior.OverwriteExisting)
							{
								array[i].value = value;
								return true;
							}
							if (behavior == InsertionBehavior.ThrowOnExisting)
							{
								ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
							}
							return false;
						}
						else
						{
							i = array[i].next;
							if (num2 >= array.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
							num2++;
						}
					}
				}
				else
				{
					EqualityComparer<TKey> @default = EqualityComparer<TKey>.Default;
					while (i < array.Length)
					{
						if (array[i].hashCode == num && @default.Equals(array[i].key, key))
						{
							if (behavior == InsertionBehavior.OverwriteExisting)
							{
								array[i].value = value;
								return true;
							}
							if (behavior == InsertionBehavior.ThrowOnExisting)
							{
								ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
							}
							return false;
						}
						else
						{
							i = array[i].next;
							if (num2 >= array.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
							num2++;
						}
					}
				}
			}
			else
			{
				while (i < array.Length)
				{
					if (array[i].hashCode == num && comparer.Equals(array[i].key, key))
					{
						if (behavior == InsertionBehavior.OverwriteExisting)
						{
							array[i].value = value;
							return true;
						}
						if (behavior == InsertionBehavior.ThrowOnExisting)
						{
							ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
						}
						return false;
					}
					else
					{
						i = array[i].next;
						if (num2 >= array.Length)
						{
							ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
						}
						num2++;
					}
				}
			}
			bool flag = false;
			bool flag2 = false;
			int num3;
			if (this._freeCount > 0)
			{
				num3 = this._freeList;
				flag2 = true;
				this._freeCount--;
			}
			else
			{
				int count = this._count;
				if (count == array.Length)
				{
					this.Resize();
					flag = true;
				}
				num3 = count;
				this._count = count + 1;
				array = this._entries;
			}
			ref int ptr2 = (ref flag ? ref this._buckets[num % this._buckets.Length] : ref ptr);
			ref Dictionary<TKey, TValue>.Entry ptr3 = ref array[num3];
			if (flag2)
			{
				this._freeList = ptr3.next;
			}
			ptr3.hashCode = num;
			ptr3.next = ptr2 - 1;
			ptr3.key = key;
			ptr3.value = value;
			ptr2 = num3 + 1;
			return true;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.Dictionary`2" /> instance is invalid.</exception>
		// Token: 0x06003B37 RID: 15159 RVA: 0x000E5918 File Offset: 0x000E3B18
		public virtual void OnDeserialization(object sender)
		{
			SerializationInfo serializationInfo;
			HashHelpers.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				return;
			}
			int @int = serializationInfo.GetInt32("Version");
			int int2 = serializationInfo.GetInt32("HashSize");
			this._comparer = (IEqualityComparer<TKey>)serializationInfo.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			if (int2 != 0)
			{
				this.Initialize(int2);
				KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])serializationInfo.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
				if (array == null)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Key == null)
					{
						ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
					}
					this.Add(array[i].Key, array[i].Value);
				}
			}
			else
			{
				this._buckets = null;
			}
			this._version = @int;
			HashHelpers.SerializationInfoTable.Remove(this);
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000E5A08 File Offset: 0x000E3C08
		private void Resize()
		{
			this.Resize(HashHelpers.ExpandPrime(this._count), false);
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000E5A1C File Offset: 0x000E3C1C
		private void Resize(int newSize, bool forceNewHashCodes)
		{
			int[] array = new int[newSize];
			Dictionary<TKey, TValue>.Entry[] array2 = new Dictionary<TKey, TValue>.Entry[newSize];
			int count = this._count;
			Array.Copy(this._entries, 0, array2, 0, count);
			if (default(TKey) == null && forceNewHashCodes)
			{
				for (int i = 0; i < count; i++)
				{
					if (array2[i].hashCode >= 0)
					{
						array2[i].hashCode = array2[i].key.GetHashCode() & int.MaxValue;
					}
				}
			}
			for (int j = 0; j < count; j++)
			{
				if (array2[j].hashCode >= 0)
				{
					int num = array2[j].hashCode % newSize;
					array2[j].next = array[num] - 1;
					array[num] = j + 1;
				}
			}
			this._buckets = array;
			this._entries = array2;
		}

		/// <summary>Removes the value with the specified key from the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>true if the element is successfully found and removed; otherwise, false.  This method returns false if <paramref name="key" /> is not found in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003B3A RID: 15162 RVA: 0x000E5B08 File Offset: 0x000E3D08
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets != null)
			{
				IEqualityComparer<TKey> comparer = this._comparer;
				int num = ((comparer != null) ? comparer.GetHashCode(key) : key.GetHashCode()) & int.MaxValue;
				int num2 = num % this._buckets.Length;
				int num3 = -1;
				ref Dictionary<TKey, TValue>.Entry ptr;
				for (int i = this._buckets[num2] - 1; i >= 0; i = ptr.next)
				{
					ptr = ref this._entries[i];
					if (ptr.hashCode == num)
					{
						IEqualityComparer<TKey> comparer2 = this._comparer;
						if ((comparer2 != null) ? comparer2.Equals(ptr.key, key) : EqualityComparer<TKey>.Default.Equals(ptr.key, key))
						{
							if (num3 < 0)
							{
								this._buckets[num2] = ptr.next + 1;
							}
							else
							{
								this._entries[num3].next = ptr.next;
							}
							ptr.hashCode = -1;
							ptr.next = this._freeList;
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
							{
								ptr.key = default(TKey);
							}
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
							{
								ptr.value = default(TValue);
							}
							this._freeList = i;
							this._freeCount++;
							this._version++;
							return true;
						}
					}
					num3 = i;
				}
			}
			return false;
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000E5C60 File Offset: 0x000E3E60
		public bool Remove(TKey key, out TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets != null)
			{
				IEqualityComparer<TKey> comparer = this._comparer;
				int num = ((comparer != null) ? comparer.GetHashCode(key) : key.GetHashCode()) & int.MaxValue;
				int num2 = num % this._buckets.Length;
				int num3 = -1;
				ref Dictionary<TKey, TValue>.Entry ptr;
				for (int i = this._buckets[num2] - 1; i >= 0; i = ptr.next)
				{
					ptr = ref this._entries[i];
					if (ptr.hashCode == num)
					{
						IEqualityComparer<TKey> comparer2 = this._comparer;
						if ((comparer2 != null) ? comparer2.Equals(ptr.key, key) : EqualityComparer<TKey>.Default.Equals(ptr.key, key))
						{
							if (num3 < 0)
							{
								this._buckets[num2] = ptr.next + 1;
							}
							else
							{
								this._entries[num3].next = ptr.next;
							}
							value = ptr.value;
							ptr.hashCode = -1;
							ptr.next = this._freeList;
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
							{
								ptr.key = default(TKey);
							}
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
							{
								ptr.value = default(TValue);
							}
							this._freeList = i;
							this._freeCount++;
							this._version++;
							return true;
						}
					}
					num3 = i;
				}
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003B3C RID: 15164 RVA: 0x000E5DCC File Offset: 0x000E3FCC
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.FindEntry(key);
			if (num >= 0)
			{
				value = this._entries[num].value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x000E5E06 File Offset: 0x000E4006
		public bool TryAdd(TKey key, TValue value)
		{
			return this.TryInsert(key, value, InsertionBehavior.None);
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x000E5E11 File Offset: 0x000E4011
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.Generic.ICollection`1" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003B40 RID: 15168 RVA: 0x000E5E1C File Offset: 0x000E401C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index > array.Length)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				Dictionary<TKey, TValue>.Entry[] entries = this._entries;
				for (int i = 0; i < this._count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array3[index++] = new DictionaryEntry(entries[i].key, entries[i].value);
					}
				}
				return;
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			try
			{
				int count = this._count;
				Dictionary<TKey, TValue>.Entry[] entries2 = this._entries;
				for (int j = 0; j < count; j++)
				{
					if (entries2[j].hashCode >= 0)
					{
						array4[index++] = new KeyValuePair<TKey, TValue>(entries2[j].key, entries2[j].value);
					}
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003B41 RID: 15169 RVA: 0x000E53EF File Offset: 0x000E35EF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x000E5F7C File Offset: 0x000E417C
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			int num = ((this._entries == null) ? 0 : this._entries.Length);
			if (num >= capacity)
			{
				return num;
			}
			if (this._buckets == null)
			{
				return this.Initialize(capacity);
			}
			int prime = HashHelpers.GetPrime(capacity);
			this.Resize(prime, false);
			return prime;
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. </returns>
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000E5FCE File Offset: 0x000E41CE
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

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x00033991 File Offset: 0x00031B91
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns false.</returns>
		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x00033991 File Offset: 0x00031B91
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000E5FF0 File Offset: 0x000E41F0
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000E5FF8 File Offset: 0x000E41F8
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets or sets the value with the specified key.</summary>
		/// <returns>The value associated with the specified key, or null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.-or-A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x1700099A RID: 2458
		object IDictionary.this[object key]
		{
			get
			{
				if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.FindEntry((TKey)((object)key));
					if (num >= 0)
					{
						return this._entries[num].value;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
				try
				{
					TKey tkey = (TKey)((object)key);
					try
					{
						this[tkey] = (TValue)((object)value);
					}
					catch (InvalidCastException)
					{
						ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
					}
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
				}
			}
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000E60B8 File Offset: 0x000E42B8
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The object to use as the key.</param>
		/// <param name="value">The object to use as the value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.-or-<paramref name="value" /> is of a type that is not assignable to <paramref name="TValue" />, the type of values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.-or-A value with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x06003B4C RID: 15180 RVA: 0x000E60CC File Offset: 0x000E42CC
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
			try
			{
				TKey tkey = (TKey)((object)key);
				try
				{
					this.Add(tkey, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
				}
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003B4D RID: 15181 RVA: 0x000E6144 File Offset: 0x000E4344
		bool IDictionary.Contains(object key)
		{
			return Dictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06003B4E RID: 15182 RVA: 0x000E615C File Offset: 0x000E435C
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003B4F RID: 15183 RVA: 0x000E616A File Offset: 0x000E436A
		void IDictionary.Remove(object key)
		{
			if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x04001EFC RID: 7932
		private int[] _buckets;

		// Token: 0x04001EFD RID: 7933
		private Dictionary<TKey, TValue>.Entry[] _entries;

		// Token: 0x04001EFE RID: 7934
		private int _count;

		// Token: 0x04001EFF RID: 7935
		private int _freeList;

		// Token: 0x04001F00 RID: 7936
		private int _freeCount;

		// Token: 0x04001F01 RID: 7937
		private int _version;

		// Token: 0x04001F02 RID: 7938
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x04001F03 RID: 7939
		private Dictionary<TKey, TValue>.KeyCollection _keys;

		// Token: 0x04001F04 RID: 7940
		private Dictionary<TKey, TValue>.ValueCollection _values;

		// Token: 0x04001F05 RID: 7941
		private object _syncRoot;

		// Token: 0x0200073E RID: 1854
		private struct Entry
		{
			// Token: 0x04001F06 RID: 7942
			public int hashCode;

			// Token: 0x04001F07 RID: 7943
			public int next;

			// Token: 0x04001F08 RID: 7944
			public TKey key;

			// Token: 0x04001F09 RID: 7945
			public TValue value;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		// Token: 0x0200073F RID: 1855
		[Serializable]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06003B50 RID: 15184 RVA: 0x000E6181 File Offset: 0x000E4381
			internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this._dictionary = dictionary;
				this._version = dictionary._version;
				this._index = 0;
				this._getEnumeratorRetType = getEnumeratorRetType;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06003B51 RID: 15185 RVA: 0x000E61B0 File Offset: 0x000E43B0
			public bool MoveNext()
			{
				if (this._version != this._dictionary._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				while (this._index < this._dictionary._count)
				{
					Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
					int index = this._index;
					this._index = index + 1;
					ref Dictionary<TKey, TValue>.Entry ptr = ref entries[index];
					if (ptr.hashCode >= 0)
					{
						this._current = new KeyValuePair<TKey, TValue>(ptr.key, ptr.value);
						return true;
					}
				}
				this._index = this._dictionary._count + 1;
				this._current = default(KeyValuePair<TKey, TValue>);
				return false;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2" /> at the current position of the enumerator.</returns>
			// Token: 0x1700099B RID: 2459
			// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000E624E File Offset: 0x000E444E
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" />.</summary>
			// Token: 0x06003B53 RID: 15187 RVA: 0x00002C89 File Offset: 0x00000E89
			public void Dispose()
			{
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator, as an <see cref="T:System.Object" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700099C RID: 2460
			// (get) Token: 0x06003B54 RID: 15188 RVA: 0x000E6258 File Offset: 0x000E4458
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					if (this._getEnumeratorRetType == 1)
					{
						return new DictionaryEntry(this._current.Key, this._current.Value);
					}
					return new KeyValuePair<TKey, TValue>(this._current.Key, this._current.Value);
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06003B55 RID: 15189 RVA: 0x000E62DB File Offset: 0x000E44DB
			void IEnumerator.Reset()
			{
				if (this._version != this._dictionary._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = 0;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the dictionary at the current position of the enumerator, as a <see cref="T:System.Collections.DictionaryEntry" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700099D RID: 2461
			// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000E6308 File Offset: 0x000E4508
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return new DictionaryEntry(this._current.Key, this._current.Value);
				}
			}

			/// <summary>Gets the key of the element at the current position of the enumerator.</summary>
			/// <returns>The key of the element in the dictionary at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700099E RID: 2462
			// (get) Token: 0x06003B57 RID: 15191 RVA: 0x000E635C File Offset: 0x000E455C
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current.Key;
				}
			}

			/// <summary>Gets the value of the element at the current position of the enumerator.</summary>
			/// <returns>The value of the element in the dictionary at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x1700099F RID: 2463
			// (get) Token: 0x06003B58 RID: 15192 RVA: 0x000E6390 File Offset: 0x000E4590
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current.Value;
				}
			}

			// Token: 0x04001F0A RID: 7946
			private Dictionary<TKey, TValue> _dictionary;

			// Token: 0x04001F0B RID: 7947
			private int _version;

			// Token: 0x04001F0C RID: 7948
			private int _index;

			// Token: 0x04001F0D RID: 7949
			private KeyValuePair<TKey, TValue> _current;

			// Token: 0x04001F0E RID: 7950
			private int _getEnumeratorRetType;
		}

		/// <summary>Represents the collection of keys in a <see cref="T:System.Collections.Generic.Dictionary`2" />. This class cannot be inherited.</summary>
		// Token: 0x02000740 RID: 1856
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2" /> whose keys are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x06003B59 RID: 15193 RVA: 0x000E63C4 File Offset: 0x000E45C4
			public KeyCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this._dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" /> for the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</returns>
			// Token: 0x06003B5A RID: 15194 RVA: 0x000E63DC File Offset: 0x000E45DC
			public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06003B5B RID: 15195 RVA: 0x000E63EC File Offset: 0x000E45EC
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].key;
					}
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.Retrieving the value of this property is an O(1) operation.</returns>
			// Token: 0x170009A0 RID: 2464
			// (get) Token: 0x06003B5C RID: 15196 RVA: 0x000E6474 File Offset: 0x000E4674
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x170009A1 RID: 2465
			// (get) Token: 0x06003B5D RID: 15197 RVA: 0x0000C091 File Offset: 0x0000A291
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003B5E RID: 15198 RVA: 0x000E6481 File Offset: 0x000E4681
			void ICollection<TKey>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06003B5F RID: 15199 RVA: 0x000E6481 File Offset: 0x000E4681
			void ICollection<TKey>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06003B60 RID: 15200 RVA: 0x000E648A File Offset: 0x000E468A
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this._dictionary.ContainsKey(item);
			}

			// Token: 0x06003B61 RID: 15201 RVA: 0x000E6498 File Offset: 0x000E4698
			bool ICollection<TKey>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
				return false;
			}

			// Token: 0x06003B62 RID: 15202 RVA: 0x000E64A2 File Offset: 0x000E46A2
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06003B63 RID: 15203 RVA: 0x000E64A2 File Offset: 0x000E46A2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06003B64 RID: 15204 RVA: 0x000E64B4 File Offset: 0x000E46B4
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].key;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns false.</returns>
			// Token: 0x170009A2 RID: 2466
			// (get) Token: 0x06003B65 RID: 15205 RVA: 0x00033991 File Offset: 0x00031B91
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
			// Token: 0x170009A3 RID: 2467
			// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000E65A0 File Offset: 0x000E47A0
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x04001F0F RID: 7951
			private Dictionary<TKey, TValue> _dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			// Token: 0x02000741 RID: 1857
			[Serializable]
			public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
			{
				// Token: 0x06003B67 RID: 15207 RVA: 0x000E65AD File Offset: 0x000E47AD
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this._dictionary = dictionary;
					this._version = dictionary._version;
					this._index = 0;
					this._currentKey = default(TKey);
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" />.</summary>
				// Token: 0x06003B68 RID: 15208 RVA: 0x00002C89 File Offset: 0x00000E89
				public void Dispose()
				{
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06003B69 RID: 15209 RVA: 0x000E65D8 File Offset: 0x000E47D8
				public bool MoveNext()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					while (this._index < this._dictionary._count)
					{
						Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
						int index = this._index;
						this._index = index + 1;
						ref Dictionary<TKey, TValue>.Entry ptr = ref entries[index];
						if (ptr.hashCode >= 0)
						{
							this._currentKey = ptr.key;
							return true;
						}
					}
					this._index = this._dictionary._count + 1;
					this._currentKey = default(TKey);
					return false;
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x170009A4 RID: 2468
				// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000E666B File Offset: 0x000E486B
				public TKey Current
				{
					get
					{
						return this._currentKey;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x170009A5 RID: 2469
				// (get) Token: 0x06003B6B RID: 15211 RVA: 0x000E6673 File Offset: 0x000E4873
				object IEnumerator.Current
				{
					get
					{
						if (this._index == 0 || this._index == this._dictionary._count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
						}
						return this._currentKey;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06003B6C RID: 15212 RVA: 0x000E66A2 File Offset: 0x000E48A2
				void IEnumerator.Reset()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					this._index = 0;
					this._currentKey = default(TKey);
				}

				// Token: 0x04001F10 RID: 7952
				private Dictionary<TKey, TValue> _dictionary;

				// Token: 0x04001F11 RID: 7953
				private int _index;

				// Token: 0x04001F12 RID: 7954
				private int _version;

				// Token: 0x04001F13 RID: 7955
				private TKey _currentKey;
			}
		}

		/// <summary>Represents the collection of values in a <see cref="T:System.Collections.Generic.Dictionary`2" />. This class cannot be inherited. </summary>
		// Token: 0x02000742 RID: 1858
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> class that reflects the values in the specified <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2" /> whose values are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is null.</exception>
			// Token: 0x06003B6D RID: 15213 RVA: 0x000E66CF File Offset: 0x000E48CF
			public ValueCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this._dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" /> for the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</returns>
			// Token: 0x06003B6E RID: 15214 RVA: 0x000E66E7 File Offset: 0x000E48E7
			public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06003B6F RID: 15215 RVA: 0x000E66F4 File Offset: 0x000E48F4
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].value;
					}
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</returns>
			// Token: 0x170009A6 RID: 2470
			// (get) Token: 0x06003B70 RID: 15216 RVA: 0x000E677C File Offset: 0x000E497C
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x170009A7 RID: 2471
			// (get) Token: 0x06003B71 RID: 15217 RVA: 0x0000C091 File Offset: 0x0000A291
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003B72 RID: 15218 RVA: 0x000E6789 File Offset: 0x000E4989
			void ICollection<TValue>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06003B73 RID: 15219 RVA: 0x000E6792 File Offset: 0x000E4992
			bool ICollection<TValue>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
				return false;
			}

			// Token: 0x06003B74 RID: 15220 RVA: 0x000E6789 File Offset: 0x000E4989
			void ICollection<TValue>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06003B75 RID: 15221 RVA: 0x000E679C File Offset: 0x000E499C
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this._dictionary.ContainsValue(item);
			}

			// Token: 0x06003B76 RID: 15222 RVA: 0x000E67AA File Offset: 0x000E49AA
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06003B77 RID: 15223 RVA: 0x000E67AA File Offset: 0x000E49AA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06003B78 RID: 15224 RVA: 0x000E67BC File Offset: 0x000E49BC
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].value;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, this property always returns false.</returns>
			// Token: 0x170009A8 RID: 2472
			// (get) Token: 0x06003B79 RID: 15225 RVA: 0x00033991 File Offset: 0x00031B91
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, this property always returns the current instance.</returns>
			// Token: 0x170009A9 RID: 2473
			// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000E68A8 File Offset: 0x000E4AA8
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x04001F14 RID: 7956
			private Dictionary<TKey, TValue> _dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			// Token: 0x02000743 RID: 1859
			[Serializable]
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x06003B7B RID: 15227 RVA: 0x000E68B5 File Offset: 0x000E4AB5
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this._dictionary = dictionary;
					this._version = dictionary._version;
					this._index = 0;
					this._currentValue = default(TValue);
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" />.</summary>
				// Token: 0x06003B7C RID: 15228 RVA: 0x00002C89 File Offset: 0x00000E89
				public void Dispose()
				{
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
				/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06003B7D RID: 15229 RVA: 0x000E68E0 File Offset: 0x000E4AE0
				public bool MoveNext()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					while (this._index < this._dictionary._count)
					{
						Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
						int index = this._index;
						this._index = index + 1;
						ref Dictionary<TKey, TValue>.Entry ptr = ref entries[index];
						if (ptr.hashCode >= 0)
						{
							this._currentValue = ptr.value;
							return true;
						}
					}
					this._index = this._dictionary._count + 1;
					this._currentValue = default(TValue);
					return false;
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x170009AA RID: 2474
				// (get) Token: 0x06003B7E RID: 15230 RVA: 0x000E6973 File Offset: 0x000E4B73
				public TValue Current
				{
					get
					{
						return this._currentValue;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
				// Token: 0x170009AB RID: 2475
				// (get) Token: 0x06003B7F RID: 15231 RVA: 0x000E697B File Offset: 0x000E4B7B
				object IEnumerator.Current
				{
					get
					{
						if (this._index == 0 || this._index == this._dictionary._count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
						}
						return this._currentValue;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
				// Token: 0x06003B80 RID: 15232 RVA: 0x000E69AA File Offset: 0x000E4BAA
				void IEnumerator.Reset()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					this._index = 0;
					this._currentValue = default(TValue);
				}

				// Token: 0x04001F15 RID: 7957
				private Dictionary<TKey, TValue> _dictionary;

				// Token: 0x04001F16 RID: 7958
				private int _index;

				// Token: 0x04001F17 RID: 7959
				private int _version;

				// Token: 0x04001F18 RID: 7960
				private TValue _currentValue;
			}
		}
	}
}
