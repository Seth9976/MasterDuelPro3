using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using Unity;

namespace System.Collections.Specialized
{
	/// <summary>Provides the abstract base class for a collection of associated <see cref="T:System.String" /> keys and <see cref="T:System.Object" /> values that can be accessed either with the key or with the index.</summary>
	// Token: 0x0200030E RID: 782
	[Serializable]
	public abstract class NameObjectCollectionBase : ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty.</summary>
		// Token: 0x06001321 RID: 4897 RVA: 0x00054ABA File Offset: 0x00052CBA
		protected NameObjectCollectionBase()
			: this(NameObjectCollectionBase.defaultComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		// Token: 0x06001322 RID: 4898 RVA: 0x00054AC8 File Offset: 0x00052CC8
		protected NameObjectCollectionBase(IEqualityComparer equalityComparer)
		{
			IEqualityComparer equalityComparer2;
			if (equalityComparer != null)
			{
				equalityComparer2 = equalityComparer;
			}
			else
			{
				IEqualityComparer equalityComparer3 = NameObjectCollectionBase.defaultComparer;
				equalityComparer2 = equalityComparer3;
			}
			this._keyComparer = equalityComparer2;
			this.Reset();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object can initially contain.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06001323 RID: 4899 RVA: 0x00054AF4 File Offset: 0x00052CF4
		protected NameObjectCollectionBase(int capacity, IEqualityComparer equalityComparer)
			: this(equalityComparer)
		{
			this.Reset(capacity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is empty, has the specified initial capacity, and uses the default hash code provider and the default comparer.</summary>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance can initially contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06001324 RID: 4900 RVA: 0x00054B04 File Offset: 0x00052D04
		protected NameObjectCollectionBase(int capacity)
		{
			this._keyComparer = StringComparer.InvariantCultureIgnoreCase;
			this.Reset(capacity);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x000026E5 File Offset: 0x000008E5
		internal NameObjectCollectionBase(DBNull dummy)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> class that is serializable and uses the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		// Token: 0x06001326 RID: 4902 RVA: 0x00054B1E File Offset: 0x00052D1E
		protected NameObjectCollectionBase(SerializationInfo info, StreamingContext context)
		{
			this._serializationInfo = info;
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06001327 RID: 4903 RVA: 0x00054B30 File Offset: 0x00052D30
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ReadOnly", this._readOnly);
			if (this._keyComparer == NameObjectCollectionBase.defaultComparer)
			{
				info.AddValue("HashProvider", CompatibleComparer.DefaultHashCodeProvider, typeof(IHashCodeProvider));
				info.AddValue("Comparer", CompatibleComparer.DefaultComparer, typeof(IComparer));
			}
			else if (this._keyComparer == null)
			{
				info.AddValue("HashProvider", null, typeof(IHashCodeProvider));
				info.AddValue("Comparer", null, typeof(IComparer));
			}
			else if (this._keyComparer is CompatibleComparer)
			{
				CompatibleComparer compatibleComparer = (CompatibleComparer)this._keyComparer;
				info.AddValue("HashProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
			}
			else
			{
				info.AddValue("KeyComparer", this._keyComparer, typeof(IEqualityComparer));
			}
			int count = this._entriesArray.Count;
			info.AddValue("Count", count);
			string[] array = new string[count];
			object[] array2 = new object[count];
			for (int i = 0; i < count; i++)
			{
				NameObjectCollectionBase.NameObjectEntry nameObjectEntry = (NameObjectCollectionBase.NameObjectEntry)this._entriesArray[i];
				array[i] = nameObjectEntry.Key;
				array2[i] = nameObjectEntry.Value;
			}
			info.AddValue("Keys", array, typeof(string[]));
			info.AddValue("Values", array2, typeof(object[]));
			info.AddValue("Version", this._version);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is invalid.</exception>
		// Token: 0x06001328 RID: 4904 RVA: 0x00054CE4 File Offset: 0x00052EE4
		public virtual void OnDeserialization(object sender)
		{
			if (this._keyComparer != null)
			{
				return;
			}
			if (this._serializationInfo == null)
			{
				throw new SerializationException();
			}
			SerializationInfo serializationInfo = this._serializationInfo;
			this._serializationInfo = null;
			bool flag = false;
			int num = 0;
			string[] array = null;
			object[] array2 = null;
			IHashCodeProvider hashCodeProvider = null;
			IComparer comparer = null;
			bool flag2 = false;
			int num2 = 0;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num3 = global::<PrivateImplementationDetails>.ComputeStringHash(name);
				if (num3 <= 1573770551U)
				{
					if (num3 <= 1202781175U)
					{
						if (num3 != 891156946U)
						{
							if (num3 == 1202781175U)
							{
								if (name == "ReadOnly")
								{
									flag = serializationInfo.GetBoolean("ReadOnly");
								}
							}
						}
						else if (name == "Comparer")
						{
							comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
						}
					}
					else if (num3 != 1228509323U)
					{
						if (num3 == 1573770551U)
						{
							if (name == "Version")
							{
								flag2 = true;
								num2 = serializationInfo.GetInt32("Version");
							}
						}
					}
					else if (name == "KeyComparer")
					{
						this._keyComparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
					}
				}
				else if (num3 <= 1944240600U)
				{
					if (num3 != 1613443821U)
					{
						if (num3 == 1944240600U)
						{
							if (name == "HashProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Keys")
					{
						array = (string[])serializationInfo.GetValue("Keys", typeof(string[]));
					}
				}
				else if (num3 != 2370642523U)
				{
					if (num3 == 3790059668U)
					{
						if (name == "Count")
						{
							num = serializationInfo.GetInt32("Count");
						}
					}
				}
				else if (name == "Values")
				{
					array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
				}
			}
			if (this._keyComparer == null)
			{
				if (comparer == null || hashCodeProvider == null)
				{
					throw new SerializationException();
				}
				this._keyComparer = new CompatibleComparer(comparer, hashCodeProvider);
			}
			if (array == null || array2 == null)
			{
				throw new SerializationException();
			}
			this.Reset(num);
			for (int i = 0; i < num; i++)
			{
				this.BaseAdd(array[i], array2[i]);
			}
			this._readOnly = flag;
			if (flag2)
			{
				this._version = num2;
			}
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00054FC2 File Offset: 0x000531C2
		private void Reset()
		{
			this._entriesArray = new ArrayList();
			this._entriesTable = new Hashtable(this._keyComparer);
			this._nullKeyEntry = null;
			this._version++;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00054FF9 File Offset: 0x000531F9
		private void Reset(int capacity)
		{
			this._entriesArray = new ArrayList(capacity);
			this._entriesTable = new Hashtable(capacity, this._keyComparer);
			this._nullKeyEntry = null;
			this._version++;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00055032 File Offset: 0x00053232
		private NameObjectCollectionBase.NameObjectEntry FindEntry(string key)
		{
			if (key != null)
			{
				return (NameObjectCollectionBase.NameObjectEntry)this._entriesTable[key];
			}
			return this._nullKeyEntry;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00055053 File Offset: 0x00053253
		// (set) Token: 0x0600132D RID: 4909 RVA: 0x0005505B File Offset: 0x0005325B
		internal IEqualityComparer Comparer
		{
			get
			{
				return this._keyComparer;
			}
			set
			{
				this._keyComparer = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance is read-only; otherwise, false.</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00055064 File Offset: 0x00053264
		protected bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to add. The key can be null.</param>
		/// <param name="value">The <see cref="T:System.Object" /> value of the entry to add. The value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only. </exception>
		// Token: 0x0600132F RID: 4911 RVA: 0x0005506C File Offset: 0x0005326C
		protected void BaseAdd(string name, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = new NameObjectCollectionBase.NameObjectEntry(name, value);
			if (name != null)
			{
				if (this._entriesTable[name] == null)
				{
					this._entriesTable.Add(name, nameObjectEntry);
				}
			}
			else if (this._nullKeyEntry == null)
			{
				this._nullKeyEntry = nameObjectEntry;
			}
			this._entriesArray.Add(nameObjectEntry);
			this._version++;
		}

		/// <summary>Removes the entries with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entries to remove. The key can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only. </exception>
		// Token: 0x06001330 RID: 4912 RVA: 0x000550EC File Offset: 0x000532EC
		protected void BaseRemove(string name)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			if (name != null)
			{
				this._entriesTable.Remove(name);
				for (int i = this._entriesArray.Count - 1; i >= 0; i--)
				{
					if (this._keyComparer.Equals(name, this.BaseGetKey(i)))
					{
						this._entriesArray.RemoveAt(i);
					}
				}
			}
			else
			{
				this._nullKeyEntry = null;
				for (int j = this._entriesArray.Count - 1; j >= 0; j--)
				{
					if (this.BaseGetKey(j) == null)
					{
						this._entriesArray.RemoveAt(j);
					}
				}
			}
			this._version++;
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06001331 RID: 4913 RVA: 0x000551A1 File Offset: 0x000533A1
		protected void BaseClear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			this.Reset();
		}

		/// <summary>Gets the value of the first entry with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the value of the first entry with the specified key, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to get. The key can be null.</param>
		// Token: 0x06001332 RID: 4914 RVA: 0x000551C4 File Offset: 0x000533C4
		protected object BaseGet(string name)
		{
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = this.FindEntry(name);
			if (nameObjectEntry == null)
			{
				return null;
			}
			return nameObjectEntry.Value;
		}

		/// <summary>Sets the value of the first entry with the specified key in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance, if found; otherwise, adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to set. The key can be null.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the new value of the entry to set. The value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only. </exception>
		// Token: 0x06001333 RID: 4915 RVA: 0x000551E4 File Offset: 0x000533E4
		protected void BaseSet(string name, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("Collection is read-only."));
			}
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = this.FindEntry(name);
			if (nameObjectEntry != null)
			{
				nameObjectEntry.Value = value;
				this._version++;
				return;
			}
			this.BaseAdd(name, value);
		}

		/// <summary>Gets the value of the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the value of the entry at the specified index.</returns>
		/// <param name="index">The zero-based index of the value to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x06001334 RID: 4916 RVA: 0x00055232 File Offset: 0x00053432
		protected object BaseGet(int index)
		{
			return ((NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index]).Value;
		}

		/// <summary>Gets the key of the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the key of the entry at the specified index.</returns>
		/// <param name="index">The zero-based index of the key to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x06001335 RID: 4917 RVA: 0x0005524A File Offset: 0x0005344A
		protected string BaseGetKey(int index)
		{
			return ((NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index]).Key;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x06001336 RID: 4918 RVA: 0x00055262 File Offset: 0x00053462
		public virtual IEnumerator GetEnumerator()
		{
			return new NameObjectCollectionBase.NameObjectKeysEnumerator(this);
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0005526A File Offset: 0x0005346A
		public virtual int Count
		{
			get
			{
				return this._entriesArray.Count;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06001338 RID: 4920 RVA: 0x00055278 File Offset: 0x00053478
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Multi dimension array is not supported on this operation."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("Index {0} is out of range.", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
			}
			if (array.Length - index < this._entriesArray.Count)
			{
				throw new ArgumentException(SR.GetString("Insufficient space in the target location to copy the information."));
			}
			foreach (object obj in this)
			{
				array.SetValue(obj, index++);
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object.</returns>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00055322 File Offset: 0x00053522
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

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> array that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x0600133B RID: 4923 RVA: 0x00055344 File Offset: 0x00053544
		protected string[] BaseGetAllKeys()
		{
			int count = this._entriesArray.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGetKey(i);
			}
			return array;
		}

		/// <summary>Gets a <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> instance that contains all the keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</returns>
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0005537B File Offset: 0x0005357B
		public virtual NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new NameObjectCollectionBase.KeysCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x04000B83 RID: 2947
		private const string ReadOnlyName = "ReadOnly";

		// Token: 0x04000B84 RID: 2948
		private const string CountName = "Count";

		// Token: 0x04000B85 RID: 2949
		private const string ComparerName = "Comparer";

		// Token: 0x04000B86 RID: 2950
		private const string HashCodeProviderName = "HashProvider";

		// Token: 0x04000B87 RID: 2951
		private const string KeysName = "Keys";

		// Token: 0x04000B88 RID: 2952
		private const string ValuesName = "Values";

		// Token: 0x04000B89 RID: 2953
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04000B8A RID: 2954
		private const string VersionName = "Version";

		// Token: 0x04000B8B RID: 2955
		private bool _readOnly;

		// Token: 0x04000B8C RID: 2956
		private ArrayList _entriesArray;

		// Token: 0x04000B8D RID: 2957
		private IEqualityComparer _keyComparer;

		// Token: 0x04000B8E RID: 2958
		private volatile Hashtable _entriesTable;

		// Token: 0x04000B8F RID: 2959
		private volatile NameObjectCollectionBase.NameObjectEntry _nullKeyEntry;

		// Token: 0x04000B90 RID: 2960
		private NameObjectCollectionBase.KeysCollection _keys;

		// Token: 0x04000B91 RID: 2961
		private SerializationInfo _serializationInfo;

		// Token: 0x04000B92 RID: 2962
		private int _version;

		// Token: 0x04000B93 RID: 2963
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04000B94 RID: 2964
		private static StringComparer defaultComparer = StringComparer.InvariantCultureIgnoreCase;

		// Token: 0x0200030F RID: 783
		internal class NameObjectEntry
		{
			// Token: 0x0600133E RID: 4926 RVA: 0x000553A3 File Offset: 0x000535A3
			internal NameObjectEntry(string name, object value)
			{
				this.Key = name;
				this.Value = value;
			}

			// Token: 0x04000B95 RID: 2965
			internal string Key;

			// Token: 0x04000B96 RID: 2966
			internal object Value;
		}

		// Token: 0x02000310 RID: 784
		[Serializable]
		internal class NameObjectKeysEnumerator : IEnumerator
		{
			// Token: 0x0600133F RID: 4927 RVA: 0x000553B9 File Offset: 0x000535B9
			internal NameObjectKeysEnumerator(NameObjectCollectionBase coll)
			{
				this._coll = coll;
				this._version = this._coll._version;
				this._pos = -1;
			}

			// Token: 0x06001340 RID: 4928 RVA: 0x000553E0 File Offset: 0x000535E0
			public bool MoveNext()
			{
				if (this._version != this._coll._version)
				{
					throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				if (this._pos < this._coll.Count - 1)
				{
					this._pos++;
					return true;
				}
				this._pos = this._coll.Count;
				return false;
			}

			// Token: 0x06001341 RID: 4929 RVA: 0x00055447 File Offset: 0x00053647
			public void Reset()
			{
				if (this._version != this._coll._version)
				{
					throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				this._pos = -1;
			}

			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x06001342 RID: 4930 RVA: 0x00055473 File Offset: 0x00053673
			public object Current
			{
				get
				{
					if (this._pos >= 0 && this._pos < this._coll.Count)
					{
						return this._coll.BaseGetKey(this._pos);
					}
					throw new InvalidOperationException(SR.GetString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x04000B97 RID: 2967
			private int _pos;

			// Token: 0x04000B98 RID: 2968
			private NameObjectCollectionBase _coll;

			// Token: 0x04000B99 RID: 2969
			private int _version;
		}

		/// <summary>Represents a collection of the <see cref="T:System.String" /> keys of a collection.</summary>
		// Token: 0x02000311 RID: 785
		[DefaultMember("Item")]
		[Serializable]
		public class KeysCollection : ICollection, IEnumerable
		{
			// Token: 0x06001343 RID: 4931 RVA: 0x000554B2 File Offset: 0x000536B2
			internal KeysCollection(NameObjectCollectionBase coll)
			{
				this._coll = coll;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</returns>
			// Token: 0x06001344 RID: 4932 RVA: 0x000554C1 File Offset: 0x000536C1
			public IEnumerator GetEnumerator()
			{
				return new NameObjectCollectionBase.NameObjectKeysEnumerator(this._coll);
			}

			/// <summary>Gets the number of keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</summary>
			/// <returns>The number of keys in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</returns>
			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x06001345 RID: 4933 RVA: 0x000554CE File Offset: 0x000536CE
			public int Count
			{
				get
				{
					return this._coll.Count;
				}
			}

			/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero. </exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
			/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
			// Token: 0x06001346 RID: 4934 RVA: 0x000554DC File Offset: 0x000536DC
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.GetString("Multi dimension array is not supported on this operation."));
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("Index {0} is out of range.", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
				}
				if (array.Length - index < this._coll.Count)
				{
					throw new ArgumentException(SR.GetString("Insufficient space in the target location to copy the information."));
				}
				foreach (object obj in this)
				{
					array.SetValue(obj, index++);
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" />.</returns>
			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x06001347 RID: 4935 RVA: 0x00055586 File Offset: 0x00053786
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._coll).SyncRoot;
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> is synchronized (thread safe).</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
			// Token: 0x17000418 RID: 1048
			// (get) Token: 0x06001348 RID: 4936 RVA: 0x000028AE File Offset: 0x00000AAE
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001349 RID: 4937 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
			internal KeysCollection()
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x04000B9A RID: 2970
			private NameObjectCollectionBase _coll;
		}
	}
}
