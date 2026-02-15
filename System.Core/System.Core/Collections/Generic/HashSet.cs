using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	/// <summary>Represents a set of values.</summary>
	/// <typeparam name="T">The type of elements in the hash set.</typeparam>
	// Token: 0x0200015A RID: 346
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class HashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that is empty and uses the default equality comparer for the set type.</summary>
		// Token: 0x06000B60 RID: 2912 RVA: 0x0002C8AE File Offset: 0x0002AAAE
		public HashSet()
			: this(EqualityComparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that is empty and uses the specified equality comparer for the set type.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing values in the set, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> implementation for the set type.</param>
		// Token: 0x06000B61 RID: 2913 RVA: 0x0002C8BB File Offset: 0x0002AABB
		public HashSet(IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			this._comparer = comparer;
			this._lastIndex = 0;
			this._count = 0;
			this._freeList = -1;
			this._version = 0;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002C8F0 File Offset: 0x0002AAF0
		public HashSet(int capacity)
			: this(capacity, EqualityComparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that uses the default equality comparer for the set type, contains elements copied from the specified collection, and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x06000B63 RID: 2915 RVA: 0x0002C8FE File Offset: 0x0002AAFE
		public HashSet(IEnumerable<T> collection)
			: this(collection, EqualityComparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that uses the specified equality comparer for the set type, contains elements copied from the specified collection, and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new set.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing values in the set, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> implementation for the set type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is null.</exception>
		// Token: 0x06000B64 RID: 2916 RVA: 0x0002C90C File Offset: 0x0002AB0C
		public HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			HashSet<T> hashSet = collection as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				this.CopyFrom(hashSet);
				return;
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			int num = ((collection2 == null) ? 0 : collection2.Count);
			this.Initialize(num);
			this.UnionWith(collection);
			if (this._count > 0 && this._slots.Length / this._count > 3)
			{
				this.TrimExcess();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class with serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		// Token: 0x06000B65 RID: 2917 RVA: 0x0002C98D File Offset: 0x0002AB8D
		protected HashSet(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002C99C File Offset: 0x0002AB9C
		private void CopyFrom(HashSet<T> source)
		{
			int count = source._count;
			if (count == 0)
			{
				return;
			}
			int num = source._buckets.Length;
			if (HashHelpers.ExpandPrime(count + 1) >= num)
			{
				this._buckets = (int[])source._buckets.Clone();
				this._slots = (HashSet<T>.Slot[])source._slots.Clone();
				this._lastIndex = source._lastIndex;
				this._freeList = source._freeList;
			}
			else
			{
				int lastIndex = source._lastIndex;
				HashSet<T>.Slot[] slots = source._slots;
				this.Initialize(count);
				int num2 = 0;
				for (int i = 0; i < lastIndex; i++)
				{
					int hashCode = slots[i].hashCode;
					if (hashCode >= 0)
					{
						this.AddValue(num2, hashCode, slots[i].value);
						num2++;
					}
				}
				this._lastIndex = num2;
			}
			this._count = count;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002CA77 File Offset: 0x0002AC77
		public HashSet(int capacity, IEqualityComparer<T> comparer)
			: this(comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
		}

		/// <summary>Adds an item to an <see cref="T:System.Collections.Generic.ICollection`1" /> object.</summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
		// Token: 0x06000B68 RID: 2920 RVA: 0x0002CA9B File Offset: 0x0002AC9B
		void ICollection<T>.Add(T item)
		{
			this.AddIfNotPresent(item);
		}

		/// <summary>Removes all elements from a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		// Token: 0x06000B69 RID: 2921 RVA: 0x0002CAA8 File Offset: 0x0002ACA8
		public void Clear()
		{
			if (this._lastIndex > 0)
			{
				Array.Clear(this._slots, 0, this._lastIndex);
				Array.Clear(this._buckets, 0, this._buckets.Length);
				this._lastIndex = 0;
				this._count = 0;
				this._freeList = -1;
			}
			this._version++;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object contains the specified element.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.HashSet`1" /> object contains the specified element; otherwise, false.</returns>
		/// <param name="item">The element to locate in the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		// Token: 0x06000B6A RID: 2922 RVA: 0x0002CB08 File Offset: 0x0002AD08
		public bool Contains(T item)
		{
			if (this._buckets != null)
			{
				int num = 0;
				int num2 = this.InternalGetHashCode(item);
				HashSet<T>.Slot[] slots = this._slots;
				for (int i = this._buckets[num2 % this._buckets.Length] - 1; i >= 0; i = slots[i].next)
				{
					if (slots[i].hashCode == num2 && this._comparer.Equals(slots[i].value, item))
					{
						return true;
					}
					if (num >= slots.Length)
					{
						throw new InvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
					}
					num++;
				}
			}
			return false;
		}

		/// <summary>Copies the elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.HashSet`1" /> object. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="arrayIndex" /> is greater than the length of the destination <paramref name="array" />.</exception>
		// Token: 0x06000B6B RID: 2923 RVA: 0x0002CB96 File Offset: 0x0002AD96
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex, this._count);
		}

		/// <summary>Removes the specified element from a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <returns>true if the element is successfully found and removed; otherwise, false.  This method returns false if <paramref name="item" /> is not found in the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</returns>
		/// <param name="item">The element to remove.</param>
		// Token: 0x06000B6C RID: 2924 RVA: 0x0002CBA8 File Offset: 0x0002ADA8
		public bool Remove(T item)
		{
			if (this._buckets != null)
			{
				int num = this.InternalGetHashCode(item);
				int num2 = num % this._buckets.Length;
				int num3 = -1;
				int num4 = 0;
				HashSet<T>.Slot[] slots = this._slots;
				for (int i = this._buckets[num2] - 1; i >= 0; i = slots[i].next)
				{
					if (slots[i].hashCode == num && this._comparer.Equals(slots[i].value, item))
					{
						if (num3 < 0)
						{
							this._buckets[num2] = slots[i].next + 1;
						}
						else
						{
							slots[num3].next = slots[i].next;
						}
						slots[i].hashCode = -1;
						if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
						{
							slots[i].value = default(T);
						}
						slots[i].next = this._freeList;
						this._count--;
						this._version++;
						if (this._count == 0)
						{
							this._lastIndex = 0;
							this._freeList = -1;
						}
						else
						{
							this._freeList = i;
						}
						return true;
					}
					if (num4 >= slots.Length)
					{
						throw new InvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
					}
					num4++;
					num3 = i;
				}
			}
			return false;
		}

		/// <summary>Gets the number of elements that are contained in a set.</summary>
		/// <returns>The number of elements that are contained in the set.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002CD0A File Offset: 0x0002AF0A
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Gets a value indicating whether a collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0000B252 File Offset: 0x00009452
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns an enumerator that iterates through a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.HashSet`1.Enumerator" /> object for the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</returns>
		// Token: 0x06000B6F RID: 2927 RVA: 0x0002CD12 File Offset: 0x0002AF12
		public HashSet<T>.Enumerator GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000B70 RID: 2928 RVA: 0x0002CD1A File Offset: 0x0002AF1A
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06000B71 RID: 2929 RVA: 0x0002CD1A File Offset: 0x0002AF1A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B72 RID: 2930 RVA: 0x0002CD28 File Offset: 0x0002AF28
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this._version);
			info.AddValue("Comparer", this._comparer, typeof(IComparer<T>));
			info.AddValue("Capacity", (this._buckets == null) ? 0 : this._buckets.Length);
			if (this._buckets != null)
			{
				T[] array = new T[this._count];
				this.CopyTo(array);
				info.AddValue("Elements", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.HashSet`1" /> object is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B73 RID: 2931 RVA: 0x0002CDC0 File Offset: 0x0002AFC0
		public virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				return;
			}
			int @int = this._siInfo.GetInt32("Capacity");
			this._comparer = (IEqualityComparer<T>)this._siInfo.GetValue("Comparer", typeof(IEqualityComparer<T>));
			this._freeList = -1;
			if (@int != 0)
			{
				this._buckets = new int[@int];
				this._slots = new HashSet<T>.Slot[@int];
				T[] array = (T[])this._siInfo.GetValue("Elements", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The keys for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.AddIfNotPresent(array[i]);
				}
			}
			else
			{
				this._buckets = null;
			}
			this._version = this._siInfo.GetInt32("Version");
			this._siInfo = null;
		}

		/// <summary>Adds the specified element to a set.</summary>
		/// <returns>true if the element is added to the <see cref="T:System.Collections.Generic.HashSet`1" /> object; false if the element is already present.</returns>
		/// <param name="item">The element to add to the set.</param>
		// Token: 0x06000B74 RID: 2932 RVA: 0x0002CE9E File Offset: 0x0002B09E
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.HashSet`1" /> object to contain all elements that are present in itself, the specified collection, or both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06000B75 RID: 2933 RVA: 0x0002CEA8 File Offset: 0x0002B0A8
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T t in other)
			{
				this.AddIfNotPresent(t);
			}
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.HashSet`1" /> object to contain only elements that are present in that object and in the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06000B76 RID: 2934 RVA: 0x0002CF00 File Offset: 0x0002B100
		public void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return;
			}
			if (other == this)
			{
				return;
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					this.Clear();
					return;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					this.IntersectWithHashSetWithSameEC(hashSet);
					return;
				}
			}
			this.IntersectWithEnumerable(other);
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object and the specified collection contain the same elements.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x06000B77 RID: 2935 RVA: 0x0002CF64 File Offset: 0x0002B164
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other == this)
			{
				return true;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				return this._count == hashSet.Count && this.ContainsAllElements(hashSet);
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null && this._count == 0 && collection.Count > 0)
			{
				return false;
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.uniqueCount == this._count && elementCount.unfoundCount == 0;
		}

		/// <summary>Copies the elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to an array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.HashSet`1" /> object. The array must have zero-based indexing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		// Token: 0x06000B78 RID: 2936 RVA: 0x0002CFEF File Offset: 0x0002B1EF
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this._count);
		}

		/// <summary>Copies the specified number of elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.HashSet`1" /> object. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy to <paramref name="array" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="arrayIndex" /> is greater than the length of the destination <paramref name="array" />.-or-<paramref name="count" /> is greater than the available space from the <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06000B79 RID: 2937 RVA: 0x0002D000 File Offset: 0x0002B200
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Non negative number is required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Non negative number is required.");
			}
			if (arrayIndex > array.Length || count > array.Length - arrayIndex)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			int num = 0;
			int num2 = 0;
			while (num2 < this._lastIndex && num < count)
			{
				if (this._slots[num2].hashCode >= 0)
				{
					array[arrayIndex + num] = this._slots[num2].value;
					num++;
				}
				num2++;
			}
		}

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="T:System.Collections.Generic.HashSet`1" /> collection.</summary>
		/// <returns>The number of elements that were removed from the <see cref="T:System.Collections.Generic.HashSet`1" /> collection.</returns>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the elements to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x06000B7A RID: 2938 RVA: 0x0002D0B4 File Offset: 0x0002B2B4
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			for (int i = 0; i < this._lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0)
				{
					T value = this._slots[i].value;
					if (match(value) && this.Remove(value))
					{
						num++;
					}
				}
			}
			return num;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> object that is used to determine equality for the values in the set.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> object that is used to determine equality for the values in the set.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0002D11F File Offset: 0x0002B31F
		public IEqualityComparer<T> Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		/// <summary>Sets the capacity of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to the actual number of elements it contains, rounded up to a nearby, implementation-specific value.</summary>
		// Token: 0x06000B7C RID: 2940 RVA: 0x0002D128 File Offset: 0x0002B328
		public void TrimExcess()
		{
			if (this._count == 0)
			{
				this._buckets = null;
				this._slots = null;
				this._version++;
				return;
			}
			int prime = HashHelpers.GetPrime(this._count);
			HashSet<T>.Slot[] array = new HashSet<T>.Slot[prime];
			int[] array2 = new int[prime];
			int num = 0;
			for (int i = 0; i < this._lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0)
				{
					array[num] = this._slots[i];
					int num2 = array[num].hashCode % prime;
					array[num].next = array2[num2] - 1;
					array2[num2] = num + 1;
					num++;
				}
			}
			this._lastIndex = num;
			this._slots = array;
			this._buckets = array2;
			this._freeList = -1;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002D200 File Offset: 0x0002B400
		private int Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this._buckets = new int[prime];
			this._slots = new HashSet<T>.Slot[prime];
			return prime;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002D230 File Offset: 0x0002B430
		private void IncreaseCapacity()
		{
			int num = HashHelpers.ExpandPrime(this._count);
			if (num <= this._count)
			{
				throw new ArgumentException("HashSet capacity is too big.");
			}
			this.SetCapacity(num);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002D264 File Offset: 0x0002B464
		private void SetCapacity(int newSize)
		{
			HashSet<T>.Slot[] array = new HashSet<T>.Slot[newSize];
			if (this._slots != null)
			{
				Array.Copy(this._slots, 0, array, 0, this._lastIndex);
			}
			int[] array2 = new int[newSize];
			for (int i = 0; i < this._lastIndex; i++)
			{
				int num = array[i].hashCode % newSize;
				array[i].next = array2[num] - 1;
				array2[num] = i + 1;
			}
			this._slots = array;
			this._buckets = array2;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002D2E0 File Offset: 0x0002B4E0
		private bool AddIfNotPresent(T value)
		{
			if (this._buckets == null)
			{
				this.Initialize(0);
			}
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			int num3 = 0;
			HashSet<T>.Slot[] array = this._slots;
			for (int i = this._buckets[num2] - 1; i >= 0; i = array[i].next)
			{
				if (array[i].hashCode == num && this._comparer.Equals(array[i].value, value))
				{
					return false;
				}
				if (num3 >= array.Length)
				{
					throw new InvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
				}
				num3++;
			}
			int num4;
			if (this._freeList >= 0)
			{
				num4 = this._freeList;
				this._freeList = array[num4].next;
			}
			else
			{
				if (this._lastIndex == array.Length)
				{
					this.IncreaseCapacity();
					array = this._slots;
					num2 = num % this._buckets.Length;
				}
				num4 = this._lastIndex;
				this._lastIndex++;
			}
			array[num4].hashCode = num;
			array[num4].value = value;
			array[num4].next = this._buckets[num2] - 1;
			this._buckets[num2] = num4 + 1;
			this._count++;
			this._version++;
			return true;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002D438 File Offset: 0x0002B638
		private void AddValue(int index, int hashCode, T value)
		{
			int num = hashCode % this._buckets.Length;
			this._slots[index].hashCode = hashCode;
			this._slots[index].value = value;
			this._slots[index].next = this._buckets[num] - 1;
			this._buckets[num] = index + 1;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002D49C File Offset: 0x0002B69C
		private bool ContainsAllElements(IEnumerable<T> other)
		{
			foreach (T t in other)
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002D4F0 File Offset: 0x0002B6F0
		private void IntersectWithHashSetWithSameEC(HashSet<T> other)
		{
			for (int i = 0; i < this._lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0)
				{
					T value = this._slots[i].value;
					if (!other.Contains(value))
					{
						this.Remove(value);
					}
				}
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002D548 File Offset: 0x0002B748
		private unsafe void IntersectWithEnumerable(IEnumerable<T> other)
		{
			int lastIndex = this._lastIndex;
			int num = BitHelper.ToIntArrayLength(lastIndex);
			BitHelper bitHelper;
			checked
			{
				if (num <= 100)
				{
					bitHelper = new BitHelper(stackalloc int[unchecked((UIntPtr)num) * 4], num);
				}
				else
				{
					bitHelper = new BitHelper(new int[num], num);
				}
				foreach (T t in other)
				{
					int num2 = this.InternalIndexOf(t);
					if (num2 >= 0)
					{
						bitHelper.MarkBit(num2);
					}
				}
			}
			for (int i = 0; i < lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0 && !bitHelper.IsMarked(i))
				{
					this.Remove(this._slots[i].value);
				}
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002D618 File Offset: 0x0002B818
		private int InternalIndexOf(T item)
		{
			int num = 0;
			int num2 = this.InternalGetHashCode(item);
			HashSet<T>.Slot[] slots = this._slots;
			for (int i = this._buckets[num2 % this._buckets.Length] - 1; i >= 0; i = slots[i].next)
			{
				if (slots[i].hashCode == num2 && this._comparer.Equals(slots[i].value, item))
				{
					return i;
				}
				if (num >= slots.Length)
				{
					throw new InvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002D6A0 File Offset: 0x0002B8A0
		private unsafe HashSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			HashSet<T>.ElementCount elementCount;
			if (this._count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						num++;
					}
				}
				elementCount.uniqueCount = 0;
				elementCount.unfoundCount = num;
				return elementCount;
			}
			int num2 = BitHelper.ToIntArrayLength(this._lastIndex);
			BitHelper bitHelper;
			int num3;
			int num4;
			checked
			{
				if (num2 <= 100)
				{
					bitHelper = new BitHelper(stackalloc int[unchecked((UIntPtr)num2) * 4], num2);
				}
				else
				{
					bitHelper = new BitHelper(new int[num2], num2);
				}
				num3 = 0;
				num4 = 0;
			}
			foreach (T t2 in other)
			{
				int num5 = this.InternalIndexOf(t2);
				if (num5 >= 0)
				{
					if (!bitHelper.IsMarked(num5))
					{
						bitHelper.MarkBit(num5);
						num4++;
					}
				}
				else
				{
					num3++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			elementCount.uniqueCount = num4;
			elementCount.unfoundCount = num3;
			return elementCount;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		private static bool AreEqualityComparersEqual(HashSet<T> set1, HashSet<T> set2)
		{
			return set1.Comparer.Equals(set2.Comparer);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002D7CB File Offset: 0x0002B9CB
		private int InternalGetHashCode(T item)
		{
			if (item == null)
			{
				return 0;
			}
			return this._comparer.GetHashCode(item) & int.MaxValue;
		}

		// Token: 0x04000370 RID: 880
		private const int Lower31BitMask = 2147483647;

		// Token: 0x04000371 RID: 881
		private const int StackAllocThreshold = 100;

		// Token: 0x04000372 RID: 882
		private const int ShrinkThreshold = 3;

		// Token: 0x04000373 RID: 883
		private const string CapacityName = "Capacity";

		// Token: 0x04000374 RID: 884
		private const string ElementsName = "Elements";

		// Token: 0x04000375 RID: 885
		private const string ComparerName = "Comparer";

		// Token: 0x04000376 RID: 886
		private const string VersionName = "Version";

		// Token: 0x04000377 RID: 887
		private int[] _buckets;

		// Token: 0x04000378 RID: 888
		private HashSet<T>.Slot[] _slots;

		// Token: 0x04000379 RID: 889
		private int _count;

		// Token: 0x0400037A RID: 890
		private int _lastIndex;

		// Token: 0x0400037B RID: 891
		private int _freeList;

		// Token: 0x0400037C RID: 892
		private IEqualityComparer<T> _comparer;

		// Token: 0x0400037D RID: 893
		private int _version;

		// Token: 0x0400037E RID: 894
		private SerializationInfo _siInfo;

		// Token: 0x0200015B RID: 347
		internal struct ElementCount
		{
			// Token: 0x0400037F RID: 895
			internal int uniqueCount;

			// Token: 0x04000380 RID: 896
			internal int unfoundCount;
		}

		// Token: 0x0200015C RID: 348
		internal struct Slot
		{
			// Token: 0x04000381 RID: 897
			internal int hashCode;

			// Token: 0x04000382 RID: 898
			internal int next;

			// Token: 0x04000383 RID: 899
			internal T value;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0200015D RID: 349
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000B89 RID: 2953 RVA: 0x0002D7E9 File Offset: 0x0002B9E9
			internal Enumerator(HashSet<T> set)
			{
				this._set = set;
				this._index = 0;
				this._version = set._version;
				this._current = default(T);
			}

			/// <summary>Releases all resources used by a <see cref="T:System.Collections.Generic.HashSet`1.Enumerator" /> object.</summary>
			// Token: 0x06000B8A RID: 2954 RVA: 0x0000A01D File Offset: 0x0000821D
			public void Dispose()
			{
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.HashSet`1" /> collection.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06000B8B RID: 2955 RVA: 0x0002D814 File Offset: 0x0002BA14
			public bool MoveNext()
			{
				if (this._version != this._set._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				while (this._index < this._set._lastIndex)
				{
					if (this._set._slots[this._index].hashCode >= 0)
					{
						this._current = this._set._slots[this._index].value;
						this._index++;
						return true;
					}
					this._index++;
				}
				this._index = this._set._lastIndex + 1;
				this._current = default(T);
				return false;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.HashSet`1" /> collection at the current position of the enumerator.</returns>
			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002D8CF File Offset: 0x0002BACF
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator, as an <see cref="T:System.Object" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0002D8D7 File Offset: 0x0002BAD7
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._set._lastIndex + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06000B8E RID: 2958 RVA: 0x0002D90C File Offset: 0x0002BB0C
			void IEnumerator.Reset()
			{
				if (this._version != this._set._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04000384 RID: 900
			private HashSet<T> _set;

			// Token: 0x04000385 RID: 901
			private int _index;

			// Token: 0x04000386 RID: 902
			private int _version;

			// Token: 0x04000387 RID: 903
			private T _current;
		}
	}
}
