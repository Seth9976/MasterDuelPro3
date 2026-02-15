using System;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of key/value pairs that are accessible by the key or index.</summary>
	// Token: 0x02000302 RID: 770
	[Serializable]
	public class OrderedDictionary : IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class.</summary>
		// Token: 0x060012B1 RID: 4785 RVA: 0x00053EF8 File Offset: 0x000520F8
		public OrderedDictionary()
			: this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		// Token: 0x060012B2 RID: 4786 RVA: 0x00053F01 File Offset: 0x00052101
		public OrderedDictionary(int capacity)
			: this(capacity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity and comparer.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x060012B3 RID: 4787 RVA: 0x00053F0B File Offset: 0x0005210B
		public OrderedDictionary(int capacity, IEqualityComparer comparer)
		{
			this._initialCapacity = capacity;
			this._comparer = comparer;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		// Token: 0x060012B4 RID: 4788 RVA: 0x00053F21 File Offset: 0x00052121
		protected OrderedDictionary(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		/// <summary>Gets the number of key/values pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00053F30 File Offset: 0x00052130
		public int Count
		{
			get
			{
				return this.objectsArray.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size; otherwise, false. The default is false.</returns>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x00053F3D File Offset: 0x0005213D
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00053F3D File Offset: 0x0005213D
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object is synchronized (thread-safe).</summary>
		/// <returns>This method always returns false.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00053F45 File Offset: 0x00052145
		public ICollection Keys
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, true);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00053F53 File Offset: 0x00052153
		private ArrayList objectsArray
		{
			get
			{
				if (this._objectsArray == null)
				{
					this._objectsArray = new ArrayList(this._initialCapacity);
				}
				return this._objectsArray;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x00053F74 File Offset: 0x00052174
		private Hashtable objectsTable
		{
			get
			{
				if (this._objectsTable == null)
				{
					this._objectsTable = new Hashtable(this._initialCapacity, this._comparer);
				}
				return this._objectsTable;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00053F9B File Offset: 0x0005219B
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

		/// <summary>Gets or sets the value with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The property is being set and the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x170003F5 RID: 1013
		public object this[object key]
		{
			get
			{
				return this.objectsTable[key];
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
				}
				if (this.objectsTable.Contains(key))
				{
					this.objectsTable[key] = value;
					this.objectsArray[this.IndexOfKey(key)] = new DictionaryEntry(key, value);
					return;
				}
				this.Add(key, value);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0005402E File Offset: 0x0005222E
		public ICollection Values
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection with the lowest available index.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. This value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x060012C0 RID: 4800 RVA: 0x0005403C File Offset: 0x0005223C
		public void Add(object key, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
			}
			this.objectsTable.Add(key, value);
			this.objectsArray.Add(new DictionaryEntry(key, value));
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x060012C1 RID: 4801 RVA: 0x00054076 File Offset: 0x00052276
		public void Clear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
			}
			this.objectsTable.Clear();
			this.objectsArray.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		// Token: 0x060012C2 RID: 4802 RVA: 0x000540A1 File Offset: 0x000522A1
		public bool Contains(object key)
		{
			return this.objectsTable.Contains(key);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> elements to a one-dimensional <see cref="T:System.Array" /> object at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060012C3 RID: 4803 RVA: 0x000540AF File Offset: 0x000522AF
		public void CopyTo(Array array, int index)
		{
			this.objectsTable.CopyTo(array, index);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000540C0 File Offset: 0x000522C0
		private int IndexOfKey(object key)
		{
			for (int i = 0; i < this.objectsArray.Count; i++)
			{
				object key2 = ((DictionaryEntry)this.objectsArray[i]).Key;
				if (this._comparer != null)
				{
					if (this._comparer.Equals(key2, key))
					{
						return i;
					}
				}
				else if (key2.Equals(key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="key">The key of the entry to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060012C5 RID: 4805 RVA: 0x00054124 File Offset: 0x00052324
		public void Remove(object key)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException("The OrderedDictionary is readonly and cannot be modified.");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.IndexOfKey(key);
			if (num < 0)
			{
				return;
			}
			this.objectsTable.Remove(key);
			this.objectsArray.RemoveAt(num);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x060012C6 RID: 4806 RVA: 0x00054177 File Offset: 0x00052377
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x060012C7 RID: 4807 RVA: 0x00054177 File Offset: 0x00052377
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060012C8 RID: 4808 RVA: 0x00054188 File Offset: 0x00052388
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("KeyComparer", this._comparer, typeof(IEqualityComparer));
			info.AddValue("ReadOnly", this._readOnly);
			info.AddValue("InitialCapacity", this._initialCapacity);
			object[] array = new object[this.Count];
			this._objectsArray.CopyTo(array);
			info.AddValue("ArrayList", array);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060012C9 RID: 4809 RVA: 0x00054204 File Offset: 0x00052404
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is invalid.</exception>
		// Token: 0x060012CA RID: 4810 RVA: 0x00054210 File Offset: 0x00052410
		protected virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			this._comparer = (IEqualityComparer)this._siInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
			this._readOnly = this._siInfo.GetBoolean("ReadOnly");
			this._initialCapacity = this._siInfo.GetInt32("InitialCapacity");
			object[] array = (object[])this._siInfo.GetValue("ArrayList", typeof(object[]));
			if (array != null)
			{
				foreach (object obj in array)
				{
					DictionaryEntry dictionaryEntry;
					try
					{
						dictionaryEntry = (DictionaryEntry)obj;
					}
					catch
					{
						throw new SerializationException("There was an error deserializing the OrderedDictionary.  The ArrayList does not contain DictionaryEntries.");
					}
					this.objectsArray.Add(dictionaryEntry);
					this.objectsTable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		// Token: 0x04000B68 RID: 2920
		private ArrayList _objectsArray;

		// Token: 0x04000B69 RID: 2921
		private Hashtable _objectsTable;

		// Token: 0x04000B6A RID: 2922
		private int _initialCapacity;

		// Token: 0x04000B6B RID: 2923
		private IEqualityComparer _comparer;

		// Token: 0x04000B6C RID: 2924
		private bool _readOnly;

		// Token: 0x04000B6D RID: 2925
		private object _syncRoot;

		// Token: 0x04000B6E RID: 2926
		private SerializationInfo _siInfo;

		// Token: 0x02000303 RID: 771
		private class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060012CB RID: 4811 RVA: 0x0005430C File Offset: 0x0005250C
			internal OrderedDictionaryEnumerator(ArrayList array, int objectReturnType)
			{
				this._arrayEnumerator = array.GetEnumerator();
				this._objectReturnType = objectReturnType;
			}

			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x060012CC RID: 4812 RVA: 0x00054328 File Offset: 0x00052528
			public object Current
			{
				get
				{
					if (this._objectReturnType == 1)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
						return dictionaryEntry.Key;
					}
					if (this._objectReturnType == 2)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
						return dictionaryEntry.Value;
					}
					return this.Entry;
				}
			}

			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x060012CD RID: 4813 RVA: 0x00054384 File Offset: 0x00052584
			public DictionaryEntry Entry
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
					object key = dictionaryEntry.Key;
					dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
					return new DictionaryEntry(key, dictionaryEntry.Value);
				}
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x060012CE RID: 4814 RVA: 0x000543C8 File Offset: 0x000525C8
			public object Key
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
					return dictionaryEntry.Key;
				}
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x060012CF RID: 4815 RVA: 0x000543F0 File Offset: 0x000525F0
			public object Value
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this._arrayEnumerator.Current;
					return dictionaryEntry.Value;
				}
			}

			// Token: 0x060012D0 RID: 4816 RVA: 0x00054415 File Offset: 0x00052615
			public bool MoveNext()
			{
				return this._arrayEnumerator.MoveNext();
			}

			// Token: 0x060012D1 RID: 4817 RVA: 0x00054422 File Offset: 0x00052622
			public void Reset()
			{
				this._arrayEnumerator.Reset();
			}

			// Token: 0x04000B6F RID: 2927
			private int _objectReturnType;

			// Token: 0x04000B70 RID: 2928
			private IEnumerator _arrayEnumerator;
		}

		// Token: 0x02000304 RID: 772
		private class OrderedDictionaryKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x060012D2 RID: 4818 RVA: 0x0005442F File Offset: 0x0005262F
			public OrderedDictionaryKeyValueCollection(ArrayList array, bool isKeys)
			{
				this._objects = array;
				this._isKeys = isKeys;
			}

			// Token: 0x060012D3 RID: 4819 RVA: 0x00054448 File Offset: 0x00052648
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
				}
				foreach (object obj in this._objects)
				{
					array.SetValue(this._isKeys ? ((DictionaryEntry)obj).Key : ((DictionaryEntry)obj).Value, index);
					index++;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x060012D4 RID: 4820 RVA: 0x000544F0 File Offset: 0x000526F0
			int ICollection.Count
			{
				get
				{
					return this._objects.Count;
				}
			}

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x060012D5 RID: 4821 RVA: 0x000028AE File Offset: 0x00000AAE
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x060012D6 RID: 4822 RVA: 0x000544FD File Offset: 0x000526FD
			object ICollection.SyncRoot
			{
				get
				{
					return this._objects.SyncRoot;
				}
			}

			// Token: 0x060012D7 RID: 4823 RVA: 0x0005450A File Offset: 0x0005270A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new OrderedDictionary.OrderedDictionaryEnumerator(this._objects, this._isKeys ? 1 : 2);
			}

			// Token: 0x04000B71 RID: 2929
			private ArrayList _objects;

			// Token: 0x04000B72 RID: 2930
			private bool _isKeys;
		}
	}
}
