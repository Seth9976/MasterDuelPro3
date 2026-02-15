using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a collection of key/value pairs that are sorted by the keys and are accessible by key and by index.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200070E RID: 1806
	[DebuggerTypeProxy(typeof(SortedList.SortedListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedList : IDictionary, ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		// Token: 0x06003897 RID: 14487 RVA: 0x000DCE1F File Offset: 0x000DB01F
		public SortedList()
		{
			this.Init();
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000DCE2D File Offset: 0x000DB02D
		private void Init()
		{
			this.keys = Array.Empty<object>();
			this.values = Array.Empty<object>();
			this._size = 0;
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="initialCapacity" />.</exception>
		// Token: 0x06003899 RID: 14489 RVA: 0x000DCE5C File Offset: 0x000DB05C
		public SortedList(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "Non-negative number required.");
			}
			this.keys = new object[initialCapacity];
			this.values = new object[initialCapacity];
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.-or- null to use the <see cref="T:System.IComparable" /> implementation of each key. </param>
		// Token: 0x0600389A RID: 14490 RVA: 0x000DCEAB File Offset: 0x000DB0AB
		public SortedList(IComparer comparer)
			: this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.-or- null to use the <see cref="T:System.IComparable" /> implementation of each key. </param>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="capacity" />.</exception>
		// Token: 0x0600389B RID: 14491 RVA: 0x000DCEBD File Offset: 0x000DB0BD
		public SortedList(IComparer comparer, int capacity)
			: this(comparer)
		{
			this.Capacity = capacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that contains elements copied from the specified dictionary, has the same initial capacity as the number of elements copied, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> implementation to copy to a new <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.-or- null to use the <see cref="T:System.IComparable" /> implementation of each key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="comparer" /> is null, and one or more elements in <paramref name="d" /> do not implement the <see cref="T:System.IComparable" /> interface. </exception>
		// Token: 0x0600389C RID: 14492 RVA: 0x000DCED0 File Offset: 0x000DB0D0
		public SortedList(IDictionary d, IComparer comparer)
			: this(comparer, (d != null) ? d.Count : 0)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Dictionary cannot be null.");
			}
			d.Keys.CopyTo(this.keys, 0);
			d.Values.CopyTo(this.values, 0);
			Array.Sort(this.keys, comparer);
			for (int i = 0; i < this.keys.Length; i++)
			{
				this.values[i] = d[this.keys[i]];
			}
			this._size = d.Count;
		}

		/// <summary>Adds an element with the specified key and value to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to add. </param>
		/// <param name="value">The value of the element to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the specified <paramref name="key" /> already exists in the <see cref="T:System.Collections.SortedList" /> object.-or- The <see cref="T:System.Collections.SortedList" /> is set to use the <see cref="T:System.IComparable" /> interface, and <paramref name="key" /> does not implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600389D RID: 14493 RVA: 0x000DCF68 File Offset: 0x000DB168
		public virtual void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(SR.Format("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", this.GetKey(num), key));
			}
			this.Insert(~num, key, value);
		}

		/// <summary>Gets or sets the capacity of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than the current number of elements in the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008DB RID: 2267
		// (set) Token: 0x0600389E RID: 14494 RVA: 0x000DCFC8 File Offset: 0x000DB1C8
		public virtual int Capacity
		{
			set
			{
				if (value < this.Count)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (value != this.keys.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						object[] array2 = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, array, 0, this._size);
							Array.Copy(this.values, 0, array2, 0, this._size);
						}
						this.keys = array;
						this.values = array2;
						return;
					}
					this.keys = Array.Empty<object>();
					this.values = Array.Empty<object>();
				}
			}
		}

		/// <summary>Gets the number of elements contained in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600389F RID: 14495 RVA: 0x000DD061 File Offset: 0x000DB261
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x000DD069 File Offset: 0x000DB269
		public virtual ICollection Keys
		{
			get
			{
				return this.GetKeyList();
			}
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x000DD071 File Offset: 0x000DB271
		public virtual ICollection Values
		{
			get
			{
				return this.GetValueList();
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object has a fixed size; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x000DD079 File Offset: 0x000DB279
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

		/// <summary>Removes all elements from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060038A6 RID: 14502 RVA: 0x000DD09B File Offset: 0x000DB29B
		public virtual void Clear()
		{
			this.version++;
			Array.Clear(this.keys, 0, this._size);
			Array.Clear(this.values, 0, this._size);
			this._size = 0;
		}

		/// <summary>Creates a shallow copy of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060038A7 RID: 14503 RVA: 0x000DD0D8 File Offset: 0x000DB2D8
		public virtual object Clone()
		{
			SortedList sortedList = new SortedList(this._size);
			Array.Copy(this.keys, 0, sortedList.keys, 0, this._size);
			Array.Copy(this.values, 0, sortedList.values, 0, this._size);
			sortedList._size = this._size;
			sortedList.version = this.version;
			sortedList.comparer = this.comparer;
			return sortedList;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060038A8 RID: 14504 RVA: 0x000DD148 File Offset: 0x000DB348
		public virtual bool Contains(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060038A9 RID: 14505 RVA: 0x000DD148 File Offset: 0x000DB348
		public virtual bool ContainsKey(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="value" />; otherwise, false.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060038AA RID: 14506 RVA: 0x000DD157 File Offset: 0x000DB357
		public virtual bool ContainsValue(object value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		/// <summary>Copies <see cref="T:System.Collections.SortedList" /> elements to a one-dimensional <see cref="T:System.Array" /> object, starting at the specified index in the array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.SortedList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.SortedList" /> object is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.SortedList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060038AB RID: 14507 RVA: 0x000DD168 File Offset: 0x000DB368
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Array cannot be null.");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[i], this.values[i]);
				array.SetValue(dictionaryEntry, i + arrayIndex);
			}
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000DD208 File Offset: 0x000DB408
		private void EnsureCapacity(int min)
		{
			int num = ((this.keys.Length == 0) ? 16 : (this.keys.Length * 2));
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

		/// <summary>Gets the value at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The value at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <param name="index">The zero-based index of the value to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060038AD RID: 14509 RVA: 0x000DD248 File Offset: 0x000DB448
		public virtual object GetByIndex(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.values[index];
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.SortedList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.SortedList" />.</returns>
		// Token: 0x060038AE RID: 14510 RVA: 0x000DD26F File Offset: 0x000DB46F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060038AF RID: 14511 RVA: 0x000DD26F File Offset: 0x000DB46F
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		/// <summary>Gets the key at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The key at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <param name="index">The zero-based index of the key to get. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060038B0 RID: 14512 RVA: 0x000DD27F File Offset: 0x000DB47F
		public virtual object GetKey(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.keys[index];
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060038B1 RID: 14513 RVA: 0x000DD2A6 File Offset: 0x000DB4A6
		public virtual IList GetKeyList()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList.KeyList(this);
			}
			return this.keyList;
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060038B2 RID: 14514 RVA: 0x000DD2C2 File Offset: 0x000DB4C2
		public virtual IList GetValueList()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList.ValueList(this);
			}
			return this.valueList;
		}

		/// <summary>Gets and sets the value associated with a specific key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The value associated with the <paramref name="key" /> parameter in the <see cref="T:System.Collections.SortedList" /> object, if <paramref name="key" /> is found; otherwise, null.</returns>
		/// <param name="key">The key associated with the value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008E3 RID: 2275
		public virtual object this[object key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		/// <summary>Returns the zero-based index of the specified key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The zero-based index of the <paramref name="key" /> parameter, if <paramref name="key" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060038B5 RID: 14517 RVA: 0x000DD364 File Offset: 0x000DB564
		public virtual int IndexOfKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified value in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter, if <paramref name="value" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060038B6 RID: 14518 RVA: 0x000DD3A5 File Offset: 0x000DB5A5
		public virtual int IndexOfValue(object value)
		{
			return Array.IndexOf<object>(this.values, value, 0, this._size);
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000DD3BC File Offset: 0x000DB5BC
		private void Insert(int index, object key, object value)
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

		/// <summary>Removes the element at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index of the element to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060038B8 RID: 14520 RVA: 0x000DD458 File Offset: 0x000DB658
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			this.keys[this._size] = null;
			this.values[this._size] = null;
			this.version++;
		}

		/// <summary>Removes the element with the specified key from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.-or- The <see cref="T:System.Collections.SortedList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060038B9 RID: 14521 RVA: 0x000DD500 File Offset: 0x000DB700
		public virtual void Remove(object key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Returns a synchronized (thread-safe) wrapper for a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>A synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <param name="list">The <see cref="T:System.Collections.SortedList" /> object to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060038BA RID: 14522 RVA: 0x000DD520 File Offset: 0x000DB720
		public static SortedList Synchronized(SortedList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new SortedList.SyncSortedList(list);
		}

		// Token: 0x04001E6C RID: 7788
		private object[] keys;

		// Token: 0x04001E6D RID: 7789
		private object[] values;

		// Token: 0x04001E6E RID: 7790
		private int _size;

		// Token: 0x04001E6F RID: 7791
		private int version;

		// Token: 0x04001E70 RID: 7792
		private IComparer comparer;

		// Token: 0x04001E71 RID: 7793
		private SortedList.KeyList keyList;

		// Token: 0x04001E72 RID: 7794
		private SortedList.ValueList valueList;

		// Token: 0x04001E73 RID: 7795
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x0200070F RID: 1807
		[Serializable]
		private class SyncSortedList : SortedList
		{
			// Token: 0x060038BB RID: 14523 RVA: 0x000DD536 File Offset: 0x000DB736
			internal SyncSortedList(SortedList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x170008E4 RID: 2276
			// (get) Token: 0x060038BC RID: 14524 RVA: 0x000DD554 File Offset: 0x000DB754
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x170008E5 RID: 2277
			// (get) Token: 0x060038BD RID: 14525 RVA: 0x000DD59C File Offset: 0x000DB79C
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170008E6 RID: 2278
			// (get) Token: 0x060038BE RID: 14526 RVA: 0x000DD5A4 File Offset: 0x000DB7A4
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x170008E7 RID: 2279
			// (get) Token: 0x060038BF RID: 14527 RVA: 0x000DD5B1 File Offset: 0x000DB7B1
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x170008E8 RID: 2280
			// (get) Token: 0x060038C0 RID: 14528 RVA: 0x0000C091 File Offset: 0x0000A291
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008E9 RID: 2281
			public override object this[object key]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[key];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[key] = value;
					}
				}
			}

			// Token: 0x060038C3 RID: 14531 RVA: 0x000DD650 File Offset: 0x000DB850
			public override void Add(object key, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(key, value);
				}
			}

			// Token: 0x060038C4 RID: 14532 RVA: 0x000DD698 File Offset: 0x000DB898
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x060038C5 RID: 14533 RVA: 0x000DD6E0 File Offset: 0x000DB8E0
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._list.Clone();
				}
				return obj;
			}

			// Token: 0x060038C6 RID: 14534 RVA: 0x000DD728 File Offset: 0x000DB928
			public override bool Contains(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(key);
				}
				return flag2;
			}

			// Token: 0x060038C7 RID: 14535 RVA: 0x000DD770 File Offset: 0x000DB970
			public override bool ContainsKey(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsKey(key);
				}
				return flag2;
			}

			// Token: 0x060038C8 RID: 14536 RVA: 0x000DD7B8 File Offset: 0x000DB9B8
			public override bool ContainsValue(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsValue(key);
				}
				return flag2;
			}

			// Token: 0x060038C9 RID: 14537 RVA: 0x000DD800 File Offset: 0x000DBA00
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x060038CA RID: 14538 RVA: 0x000DD848 File Offset: 0x000DBA48
			public override object GetByIndex(int index)
			{
				object root = this._root;
				object byIndex;
				lock (root)
				{
					byIndex = this._list.GetByIndex(index);
				}
				return byIndex;
			}

			// Token: 0x060038CB RID: 14539 RVA: 0x000DD890 File Offset: 0x000DBA90
			public override IDictionaryEnumerator GetEnumerator()
			{
				object root = this._root;
				IDictionaryEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x060038CC RID: 14540 RVA: 0x000DD8D8 File Offset: 0x000DBAD8
			public override object GetKey(int index)
			{
				object root = this._root;
				object key;
				lock (root)
				{
					key = this._list.GetKey(index);
				}
				return key;
			}

			// Token: 0x060038CD RID: 14541 RVA: 0x000DD920 File Offset: 0x000DBB20
			public override IList GetKeyList()
			{
				object root = this._root;
				IList keyList;
				lock (root)
				{
					keyList = this._list.GetKeyList();
				}
				return keyList;
			}

			// Token: 0x060038CE RID: 14542 RVA: 0x000DD968 File Offset: 0x000DBB68
			public override IList GetValueList()
			{
				object root = this._root;
				IList valueList;
				lock (root)
				{
					valueList = this._list.GetValueList();
				}
				return valueList;
			}

			// Token: 0x060038CF RID: 14543 RVA: 0x000DD9B0 File Offset: 0x000DBBB0
			public override int IndexOfKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfKey(key);
				}
				return num;
			}

			// Token: 0x060038D0 RID: 14544 RVA: 0x000DDA0C File Offset: 0x000DBC0C
			public override int IndexOfValue(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfValue(value);
				}
				return num;
			}

			// Token: 0x060038D1 RID: 14545 RVA: 0x000DDA54 File Offset: 0x000DBC54
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x060038D2 RID: 14546 RVA: 0x000DDA9C File Offset: 0x000DBC9C
			public override void Remove(object key)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(key);
				}
			}

			// Token: 0x04001E74 RID: 7796
			private SortedList _list;

			// Token: 0x04001E75 RID: 7797
			private object _root;
		}

		// Token: 0x02000710 RID: 1808
		[Serializable]
		private class SortedListEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x060038D3 RID: 14547 RVA: 0x000DDAE4 File Offset: 0x000DBCE4
			internal SortedListEnumerator(SortedList sortedList, int index, int count, int getObjRetType)
			{
				this._sortedList = sortedList;
				this._index = index;
				this._startIndex = index;
				this._endIndex = index + count;
				this._version = sortedList.version;
				this._getObjectRetType = getObjRetType;
				this._current = false;
			}

			// Token: 0x060038D4 RID: 14548 RVA: 0x00019FBE File Offset: 0x000181BE
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x170008EA RID: 2282
			// (get) Token: 0x060038D5 RID: 14549 RVA: 0x000DDB30 File Offset: 0x000DBD30
			public virtual object Key
			{
				get
				{
					if (this._version != this._sortedList.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._key;
				}
			}

			// Token: 0x060038D6 RID: 14550 RVA: 0x000DDB6C File Offset: 0x000DBD6C
			public virtual bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._endIndex)
				{
					this._key = this._sortedList.keys[this._index];
					this._value = this._sortedList.values[this._index];
					this._index++;
					this._current = true;
					return true;
				}
				this._key = null;
				this._value = null;
				this._current = false;
				return false;
			}

			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000DDC04 File Offset: 0x000DBE04
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (this._version != this._sortedList.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000DDC54 File Offset: 0x000DBE54
			public virtual object Current
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this._getObjectRetType == 1)
					{
						return this._key;
					}
					if (this._getObjectRetType == 2)
					{
						return this._value;
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x060038D9 RID: 14553 RVA: 0x000DDCAA File Offset: 0x000DBEAA
			public virtual object Value
			{
				get
				{
					if (this._version != this._sortedList.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._value;
				}
			}

			// Token: 0x060038DA RID: 14554 RVA: 0x000DDCE4 File Offset: 0x000DBEE4
			public virtual void Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = this._startIndex;
				this._current = false;
				this._key = null;
				this._value = null;
			}

			// Token: 0x04001E76 RID: 7798
			private SortedList _sortedList;

			// Token: 0x04001E77 RID: 7799
			private object _key;

			// Token: 0x04001E78 RID: 7800
			private object _value;

			// Token: 0x04001E79 RID: 7801
			private int _index;

			// Token: 0x04001E7A RID: 7802
			private int _startIndex;

			// Token: 0x04001E7B RID: 7803
			private int _endIndex;

			// Token: 0x04001E7C RID: 7804
			private int _version;

			// Token: 0x04001E7D RID: 7805
			private bool _current;

			// Token: 0x04001E7E RID: 7806
			private int _getObjectRetType;
		}

		// Token: 0x02000711 RID: 1809
		[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[Serializable]
		private class KeyList : IList, ICollection, IEnumerable
		{
			// Token: 0x060038DB RID: 14555 RVA: 0x000DDD30 File Offset: 0x000DBF30
			internal KeyList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x060038DC RID: 14556 RVA: 0x000DDD3F File Offset: 0x000DBF3F
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x060038DD RID: 14557 RVA: 0x0000C091 File Offset: 0x0000A291
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x060038DE RID: 14558 RVA: 0x0000C091 File Offset: 0x0000A291
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x060038DF RID: 14559 RVA: 0x000DDD4C File Offset: 0x000DBF4C
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000DDD59 File Offset: 0x000DBF59
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x060038E1 RID: 14561 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual int Add(object key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060038E2 RID: 14562 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060038E3 RID: 14563 RVA: 0x000DDD72 File Offset: 0x000DBF72
			public virtual bool Contains(object key)
			{
				return this.sortedList.Contains(key);
			}

			// Token: 0x060038E4 RID: 14564 RVA: 0x000DDD80 File Offset: 0x000DBF80
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				Array.Copy(this.sortedList.keys, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x060038E5 RID: 14565 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x170008F3 RID: 2291
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
				}
			}

			// Token: 0x060038E8 RID: 14568 RVA: 0x000DDDD6 File Offset: 0x000DBFD6
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 1);
			}

			// Token: 0x060038E9 RID: 14569 RVA: 0x000DDDF0 File Offset: 0x000DBFF0
			public virtual int IndexOf(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				int num = Array.BinarySearch(this.sortedList.keys, 0, this.sortedList.Count, key, this.sortedList.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x060038EA RID: 14570 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual void Remove(object key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060038EB RID: 14571 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x04001E7F RID: 7807
			private SortedList sortedList;
		}

		// Token: 0x02000712 RID: 1810
		[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[Serializable]
		private class ValueList : IList, ICollection, IEnumerable
		{
			// Token: 0x060038EC RID: 14572 RVA: 0x000DDE40 File Offset: 0x000DC040
			internal ValueList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170008F4 RID: 2292
			// (get) Token: 0x060038ED RID: 14573 RVA: 0x000DDE4F File Offset: 0x000DC04F
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170008F5 RID: 2293
			// (get) Token: 0x060038EE RID: 14574 RVA: 0x0000C091 File Offset: 0x0000A291
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008F6 RID: 2294
			// (get) Token: 0x060038EF RID: 14575 RVA: 0x0000C091 File Offset: 0x0000A291
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000DDE5C File Offset: 0x000DC05C
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x060038F1 RID: 14577 RVA: 0x000DDE69 File Offset: 0x000DC069
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x060038F2 RID: 14578 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual int Add(object key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060038F3 RID: 14579 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060038F4 RID: 14580 RVA: 0x000DDE76 File Offset: 0x000DC076
			public virtual bool Contains(object value)
			{
				return this.sortedList.ContainsValue(value);
			}

			// Token: 0x060038F5 RID: 14581 RVA: 0x000DDE84 File Offset: 0x000DC084
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				Array.Copy(this.sortedList.values, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x060038F6 RID: 14582 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x170008F9 RID: 2297
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
				}
			}

			// Token: 0x060038F9 RID: 14585 RVA: 0x000DDECE File Offset: 0x000DC0CE
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 2);
			}

			// Token: 0x060038FA RID: 14586 RVA: 0x000DDEE8 File Offset: 0x000DC0E8
			public virtual int IndexOf(object value)
			{
				return Array.IndexOf<object>(this.sortedList.values, value, 0, this.sortedList.Count);
			}

			// Token: 0x060038FB RID: 14587 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060038FC RID: 14588 RVA: 0x000DDD66 File Offset: 0x000DBF66
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x04001E80 RID: 7808
			private SortedList sortedList;
		}

		// Token: 0x02000713 RID: 1811
		internal class SortedListDebugView
		{
		}
	}
}
