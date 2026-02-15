using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a collection of key/value pairs that are organized based on the hash code of the key.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200071F RID: 1823
	[DebuggerTypeProxy(typeof(Hashtable.HashtableDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Hashtable : IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
	{
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000E0306 File Offset: 0x000DE506
		private static ConditionalWeakTable<object, SerializationInfo> SerializationInfoTable
		{
			get
			{
				return LazyInitializer.EnsureInitialized<ConditionalWeakTable<object, SerializationInfo>>(ref Hashtable.s_serializationInfoTable);
			}
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal Hashtable(bool trash)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the default initial capacity, load factor, hash code provider, and comparer.</summary>
		// Token: 0x060039AF RID: 14767 RVA: 0x000E0312 File Offset: 0x000DE512
		public Hashtable()
			: this(0, 1f)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, and the default load factor, hash code provider, and comparer.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x060039B0 RID: 14768 RVA: 0x000E0320 File Offset: 0x000DE520
		public Hashtable(int capacity)
			: this(capacity, 1f)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity and load factor, and the default hash code provider and comparer.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="capacity" /> is causing an overflow.</exception>
		// Token: 0x060039B1 RID: 14769 RVA: 0x000E0330 File Offset: 0x000DE530
		public Hashtable(int capacity, float loadFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Non-negative number required.");
			}
			if (loadFactor < 0.1f || loadFactor > 1f)
			{
				throw new ArgumentOutOfRangeException("loadFactor", SR.Format("Load factor needs to be between 0.1 and 1.0.", 0.1, 1.0));
			}
			this._loadFactor = 0.72f * loadFactor;
			double num = (double)((float)capacity / this._loadFactor);
			if (num > 2147483647.0)
			{
				throw new ArgumentException("Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table.", "capacity");
			}
			int num2 = ((num > 3.0) ? HashHelpers.GetPrime((int)num) : 3);
			this._buckets = new Hashtable.bucket[num2];
			this._loadsize = (int)(this._loadFactor * (float)num2);
			this._isWriterInProgress = false;
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, load factor, and <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x060039B2 RID: 14770 RVA: 0x000E0408 File Offset: 0x000DE608
		public Hashtable(int capacity, float loadFactor, IEqualityComparer equalityComparer)
			: this(capacity, loadFactor)
		{
			this._keycomparer = equalityComparer;
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the default initial capacity and load factor, and the specified hash code provider and comparer.</summary>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" /> object.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x060039B3 RID: 14771 RVA: 0x000E0419 File Offset: 0x000DE619
		[Obsolete("Please use Hashtable(IEqualityComparer) instead.")]
		public Hashtable(IHashCodeProvider hcp, IComparer comparer)
			: this(0, 1f, hcp, comparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the default initial capacity and load factor, and the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" /> object.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		// Token: 0x060039B4 RID: 14772 RVA: 0x000E0429 File Offset: 0x000DE629
		public Hashtable(IEqualityComparer equalityComparer)
			: this(0, 1f, equalityComparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, hash code provider, comparer, and the default load factor.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x060039B5 RID: 14773 RVA: 0x000E0438 File Offset: 0x000DE638
		[Obsolete("Please use Hashtable(int, IEqualityComparer) instead.")]
		public Hashtable(int capacity, IHashCodeProvider hcp, IComparer comparer)
			: this(capacity, 1f, hcp, comparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity and <see cref="T:System.Collections.IEqualityComparer" />, and the default load factor.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x060039B6 RID: 14774 RVA: 0x000E0448 File Offset: 0x000DE648
		public Hashtable(int capacity, IEqualityComparer equalityComparer)
			: this(capacity, 1f, equalityComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the default load factor, hash code provider, and comparer.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x060039B7 RID: 14775 RVA: 0x000E0457 File Offset: 0x000DE657
		public Hashtable(IDictionary d)
			: this(d, 1f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the specified load factor, and the default hash code provider and comparer.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x060039B8 RID: 14776 RVA: 0x000E0465 File Offset: 0x000DE665
		public Hashtable(IDictionary d, float loadFactor)
			: this(d, loadFactor, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the default load factor, and the specified hash code provider and comparer. This API is obsolete. For an alternative, see <see cref="M:System.Collections.Hashtable.#ctor(System.Collections.IDictionary,System.Collections.IEqualityComparer)" />.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x060039B9 RID: 14777 RVA: 0x000E0470 File Offset: 0x000DE670
		[Obsolete("Please use Hashtable(IDictionary, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, IHashCodeProvider hcp, IComparer comparer)
			: this(d, 1f, hcp, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to a new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the default load factor and the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x060039BA RID: 14778 RVA: 0x000E0480 File Offset: 0x000DE680
		public Hashtable(IDictionary d, IEqualityComparer equalityComparer)
			: this(d, 1f, equalityComparer)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class using the specified initial capacity, load factor, hash code provider, and comparer.</summary>
		/// <param name="capacity">The approximate number of elements that the <see cref="T:System.Collections.Hashtable" /> object can initially contain. </param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x060039BB RID: 14779 RVA: 0x000E048F File Offset: 0x000DE68F
		[Obsolete("Please use Hashtable(int, float, IEqualityComparer) instead.")]
		public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
			: this(capacity, loadFactor)
		{
			if (hcp != null || comparer != null)
			{
				this._keycomparer = new CompatibleComparer(hcp, comparer);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the specified load factor, hash code provider, and comparer.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="hcp">The <see cref="T:System.Collections.IHashCodeProvider" /> object that supplies the hash codes for all keys in the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider, which is each key's implementation of <see cref="M:System.Object.GetHashCode" />. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> object to use to determine whether two keys are equal.-or- null to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x060039BC RID: 14780 RVA: 0x000E04B0 File Offset: 0x000DE6B0
		[Obsolete("Please use Hashtable(IDictionary, float, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
			: this((d != null) ? d.Count : 0, loadFactor, hcp, comparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Dictionary cannot be null.");
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Hashtable" /> class by copying the elements from the specified dictionary to the new <see cref="T:System.Collections.Hashtable" /> object. The new <see cref="T:System.Collections.Hashtable" /> object has an initial capacity equal to the number of elements copied, and uses the specified load factor and <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> object to copy to a new <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object that defines the hash code provider and the comparer to use with the <see cref="T:System.Collections.Hashtable" />.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="M:System.Object.GetHashCode" /> and the default comparer is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="loadFactor" /> is less than 0.1.-or- <paramref name="loadFactor" /> is greater than 1.0. </exception>
		// Token: 0x060039BD RID: 14781 RVA: 0x000E050C File Offset: 0x000DE70C
		public Hashtable(IDictionary d, float loadFactor, IEqualityComparer equalityComparer)
			: this((d != null) ? d.Count : 0, loadFactor, equalityComparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Dictionary cannot be null.");
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Collections.Hashtable" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x060039BE RID: 14782 RVA: 0x000E0563 File Offset: 0x000DE763
		protected Hashtable(SerializationInfo info, StreamingContext context)
		{
			Hashtable.SerializationInfoTable.Add(this, info);
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000E0578 File Offset: 0x000DE778
		private uint InitHash(object key, int hashsize, out uint seed, out uint incr)
		{
			uint num = (uint)(this.GetHash(key) & int.MaxValue);
			seed = num;
			incr = 1U + seed * 101U % (uint)(hashsize - 1);
			return num;
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="key">The key of the element to add. </param>
		/// <param name="value">The value of the element to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Hashtable" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Hashtable" /> is read-only.-or- The <see cref="T:System.Collections.Hashtable" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060039C0 RID: 14784 RVA: 0x000E05A5 File Offset: 0x000DE7A5
		public virtual void Add(object key, object value)
		{
			this.Insert(key, value, true);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Hashtable" /> is read-only. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060039C1 RID: 14785 RVA: 0x000E05B0 File Offset: 0x000DE7B0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public virtual void Clear()
		{
			if (this._count == 0 && this._occupancy == 0)
			{
				return;
			}
			this._isWriterInProgress = true;
			for (int i = 0; i < this._buckets.Length; i++)
			{
				this._buckets[i].hash_coll = 0;
				this._buckets[i].key = null;
				this._buckets[i].val = null;
			}
			this._count = 0;
			this._occupancy = 0;
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060039C2 RID: 14786 RVA: 0x000E0640 File Offset: 0x000DE840
		public virtual object Clone()
		{
			Hashtable.bucket[] buckets = this._buckets;
			Hashtable hashtable = new Hashtable(this._count, this._keycomparer);
			hashtable._version = this._version;
			hashtable._loadFactor = this._loadFactor;
			hashtable._count = 0;
			int i = buckets.Length;
			while (i > 0)
			{
				i--;
				object key = buckets[i].key;
				if (key != null && key != buckets)
				{
					hashtable[key] = buckets[i].val;
				}
			}
			return hashtable;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Hashtable" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060039C3 RID: 14787 RVA: 0x000E06BF File Offset: 0x000DE8BF
		public virtual bool Contains(object key)
		{
			return this.ContainsKey(key);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Hashtable" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Hashtable" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060039C4 RID: 14788 RVA: 0x000E06C8 File Offset: 0x000DE8C8
		public virtual bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			Hashtable.bucket[] buckets = this._buckets;
			uint num2;
			uint num3;
			uint num = this.InitHash(key, buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = buckets[num5];
				if (bucket.key == null)
				{
					break;
				}
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					return true;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= buckets.Length)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000E0768 File Offset: 0x000DE968
		private void CopyKeys(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					array.SetValue(key, arrayIndex++);
				}
			}
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000E07B0 File Offset: 0x000DE9B0
		private void CopyEntries(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(key, buckets[num].val);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.Hashtable" /> elements to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Hashtable" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Hashtable" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Hashtable" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060039C7 RID: 14791 RVA: 0x000E0814 File Offset: 0x000DEA14
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
			this.CopyEntries(array, arrayIndex);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000E0884 File Offset: 0x000DEA84
		private void CopyValues(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					array.SetValue(buckets[num].val, arrayIndex++);
				}
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Hashtable" /> is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.Hashtable" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000922 RID: 2338
		public virtual object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				Hashtable.bucket[] buckets = this._buckets;
				uint num2;
				uint num3;
				uint num = this.InitHash(key, buckets.Length, out num2, out num3);
				int num4 = 0;
				int num5 = (int)(num2 % (uint)buckets.Length);
				Hashtable.bucket bucket;
				for (;;)
				{
					SpinWait spinWait = default(SpinWait);
					for (;;)
					{
						int version = this._version;
						bucket = buckets[num5];
						if (!this._isWriterInProgress && version == this._version)
						{
							break;
						}
						spinWait.SpinOnce();
					}
					if (bucket.key == null)
					{
						break;
					}
					if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
					{
						goto Block_5;
					}
					num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)buckets.Length));
					if (bucket.hash_coll >= 0 || ++num4 >= buckets.Length)
					{
						goto IL_00CA;
					}
				}
				return null;
				Block_5:
				return bucket.val;
				IL_00CA:
				return null;
			}
			set
			{
				this.Insert(key, value, false);
			}
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000E09BC File Offset: 0x000DEBBC
		private void expand()
		{
			int num = HashHelpers.ExpandPrime(this._buckets.Length);
			this.rehash(num);
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000E09DE File Offset: 0x000DEBDE
		private void rehash()
		{
			this.rehash(this._buckets.Length);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000E09EE File Offset: 0x000DEBEE
		private void UpdateVersion()
		{
			this._version++;
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000E0A04 File Offset: 0x000DEC04
		private void rehash(int newsize)
		{
			this._occupancy = 0;
			Hashtable.bucket[] array = new Hashtable.bucket[newsize];
			for (int i = 0; i < this._buckets.Length; i++)
			{
				Hashtable.bucket bucket = this._buckets[i];
				if (bucket.key != null && bucket.key != this._buckets)
				{
					int num = bucket.hash_coll & int.MaxValue;
					this.putEntry(array, bucket.key, bucket.val, num);
				}
			}
			this._isWriterInProgress = true;
			this._buckets = array;
			this._loadsize = (int)(this._loadFactor * (float)newsize);
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060039CF RID: 14799 RVA: 0x000E0AA5 File Offset: 0x000DECA5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060039D0 RID: 14800 RVA: 0x000E0AA5 File Offset: 0x000DECA5
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		/// <summary>Returns the hash code for the specified key.</summary>
		/// <returns>The hash code for <paramref name="key" />.</returns>
		/// <param name="key">The <see cref="T:System.Object" /> for which a hash code is to be returned. </param>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x060039D1 RID: 14801 RVA: 0x000E0AAE File Offset: 0x000DECAE
		protected virtual int GetHash(object key)
		{
			if (this._keycomparer != null)
			{
				return this._keycomparer.GetHashCode(key);
			}
			return key.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Hashtable" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Hashtable" /> is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Hashtable" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Hashtable" /> has a fixed size; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Hashtable" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Hashtable" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Compares a specific <see cref="T:System.Object" /> with a specific key in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>true if <paramref name="item" /> and <paramref name="key" /> are equal; otherwise, false.</returns>
		/// <param name="item">The <see cref="T:System.Object" /> to compare with <paramref name="key" />. </param>
		/// <param name="key">The key in the <see cref="T:System.Collections.Hashtable" /> to compare with <paramref name="item" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="item" /> is null.-or- <paramref name="key" /> is null. </exception>
		// Token: 0x060039D5 RID: 14805 RVA: 0x000E0ACB File Offset: 0x000DECCB
		protected virtual bool KeyEquals(object item, object key)
		{
			if (this._buckets == item)
			{
				return false;
			}
			if (item == key)
			{
				return true;
			}
			if (this._keycomparer != null)
			{
				return this._keycomparer.Equals(item, key);
			}
			return item != null && item.Equals(key);
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000E0B00 File Offset: 0x000DED00
		public virtual ICollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new Hashtable.KeyCollection(this);
				}
				return this._keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060039D7 RID: 14807 RVA: 0x000E0B1C File Offset: 0x000DED1C
		public virtual ICollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new Hashtable.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000E0B38 File Offset: 0x000DED38
		private void Insert(object key, object nvalue, bool add)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			if (this._count >= this._loadsize)
			{
				this.expand();
			}
			else if (this._occupancy > this._loadsize && this._count > 100)
			{
				this.rehash();
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this._buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = -1;
			int num6 = (int)(num2 % (uint)this._buckets.Length);
			for (;;)
			{
				if (num5 == -1 && this._buckets[num6].key == this._buckets && this._buckets[num6].hash_coll < 0)
				{
					num5 = num6;
				}
				if (this._buckets[num6].key == null || (this._buckets[num6].key == this._buckets && ((long)this._buckets[num6].hash_coll & (long)((ulong)(-2147483648))) == 0L))
				{
					break;
				}
				if ((long)(this._buckets[num6].hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(this._buckets[num6].key, key))
				{
					goto Block_12;
				}
				if (num5 == -1 && this._buckets[num6].hash_coll >= 0)
				{
					Hashtable.bucket[] buckets = this._buckets;
					int num7 = num6;
					buckets[num7].hash_coll = buckets[num7].hash_coll | int.MinValue;
					this._occupancy++;
				}
				num6 = (int)(((long)num6 + (long)((ulong)num3)) % (long)((ulong)this._buckets.Length));
				if (++num4 >= this._buckets.Length)
				{
					goto Block_16;
				}
			}
			if (num5 != -1)
			{
				num6 = num5;
			}
			this._isWriterInProgress = true;
			this._buckets[num6].val = nvalue;
			this._buckets[num6].key = key;
			Hashtable.bucket[] buckets2 = this._buckets;
			int num8 = num6;
			buckets2[num8].hash_coll = buckets2[num8].hash_coll | (int)num;
			this._count++;
			this.UpdateVersion();
			this._isWriterInProgress = false;
			return;
			Block_12:
			if (add)
			{
				throw new ArgumentException(SR.Format("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", this._buckets[num6].key, key));
			}
			this._isWriterInProgress = true;
			this._buckets[num6].val = nvalue;
			this.UpdateVersion();
			this._isWriterInProgress = false;
			return;
			Block_16:
			if (num5 != -1)
			{
				this._isWriterInProgress = true;
				this._buckets[num5].val = nvalue;
				this._buckets[num5].key = key;
				Hashtable.bucket[] buckets3 = this._buckets;
				int num9 = num5;
				buckets3[num9].hash_coll = buckets3[num9].hash_coll | (int)num;
				this._count++;
				this.UpdateVersion();
				this._isWriterInProgress = false;
				return;
			}
			throw new InvalidOperationException("Hashtable insert failed. Load factor too high. The most common cause is multiple threads writing to the Hashtable simultaneously.");
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000E0E08 File Offset: 0x000DF008
		private void putEntry(Hashtable.bucket[] newBuckets, object key, object nvalue, int hashcode)
		{
			uint num = (uint)(1 + hashcode * 101 % (newBuckets.Length - 1));
			int num2 = hashcode % newBuckets.Length;
			while (newBuckets[num2].key != null && newBuckets[num2].key != this._buckets)
			{
				if (newBuckets[num2].hash_coll >= 0)
				{
					int num3 = num2;
					newBuckets[num3].hash_coll = newBuckets[num3].hash_coll | int.MinValue;
					this._occupancy++;
				}
				num2 = (int)(((long)num2 + (long)((ulong)num)) % (long)((ulong)newBuckets.Length));
			}
			newBuckets[num2].val = nvalue;
			newBuckets[num2].key = key;
			int num4 = num2;
			newBuckets[num4].hash_coll = newBuckets[num4].hash_coll | hashcode;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Hashtable" /> is read-only.-or- The <see cref="T:System.Collections.Hashtable" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060039DA RID: 14810 RVA: 0x000E0EBC File Offset: 0x000DF0BC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this._buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)this._buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = this._buckets[num5];
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					break;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)this._buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= this._buckets.Length)
				{
					return;
				}
			}
			this._isWriterInProgress = true;
			Hashtable.bucket[] buckets = this._buckets;
			int num6 = num5;
			buckets[num6].hash_coll = buckets[num6].hash_coll & int.MinValue;
			if (this._buckets[num5].hash_coll != 0)
			{
				this._buckets[num5].key = this._buckets;
			}
			else
			{
				this._buckets[num5].key = null;
			}
			this._buckets[num5].val = null;
			this._count--;
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060039DB RID: 14811 RVA: 0x000E0FFA File Offset: 0x000DF1FA
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

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000E101C File Offset: 0x000DF21C
		public virtual int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Returns a synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>A synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <param name="table">The <see cref="T:System.Collections.Hashtable" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="table" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060039DD RID: 14813 RVA: 0x000E1024 File Offset: 0x000DF224
		public static Hashtable Synchronized(Hashtable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			return new Hashtable.SyncHashtable(table);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060039DE RID: 14814 RVA: 0x000E103C File Offset: 0x000DF23C
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				int version = this._version;
				info.AddValue("LoadFactor", this._loadFactor);
				info.AddValue("Version", this._version);
				IEqualityComparer keycomparer = this._keycomparer;
				if (keycomparer == null)
				{
					info.AddValue("Comparer", null, typeof(IComparer));
					info.AddValue("HashCodeProvider", null, typeof(IHashCodeProvider));
				}
				else if (keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = keycomparer as CompatibleComparer;
					info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
					info.AddValue("HashCodeProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				}
				else
				{
					info.AddValue("KeyComparer", keycomparer, typeof(IEqualityComparer));
				}
				info.AddValue("HashSize", this._buckets.Length);
				object[] array = new object[this._count];
				object[] array2 = new object[this._count];
				this.CopyKeys(array, 0);
				this.CopyValues(array2, 0);
				info.AddValue("Keys", array, typeof(object[]));
				info.AddValue("Values", array2, typeof(object[]));
				if (this._version != version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Hashtable" /> is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060039DF RID: 14815 RVA: 0x000E11D8 File Offset: 0x000DF3D8
		public virtual void OnDeserialization(object sender)
		{
			if (this._buckets != null)
			{
				return;
			}
			SerializationInfo serializationInfo;
			Hashtable.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			int num = 0;
			IComparer comparer = null;
			IHashCodeProvider hashCodeProvider = null;
			object[] array = null;
			object[] array2 = null;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num2 <= 1613443821U)
				{
					if (num2 != 891156946U)
					{
						if (num2 != 1228509323U)
						{
							if (num2 == 1613443821U)
							{
								if (name == "Keys")
								{
									array = (object[])serializationInfo.GetValue("Keys", typeof(object[]));
								}
							}
						}
						else if (name == "KeyComparer")
						{
							this._keycomparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
						}
					}
					else if (name == "Comparer")
					{
						comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
					}
				}
				else if (num2 <= 2484309429U)
				{
					if (num2 != 2370642523U)
					{
						if (num2 == 2484309429U)
						{
							if (name == "HashCodeProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashCodeProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Values")
					{
						array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
					}
				}
				else if (num2 != 3356145248U)
				{
					if (num2 == 3483216242U)
					{
						if (name == "LoadFactor")
						{
							this._loadFactor = serializationInfo.GetSingle("LoadFactor");
						}
					}
				}
				else if (name == "HashSize")
				{
					num = serializationInfo.GetInt32("HashSize");
				}
			}
			this._loadsize = (int)(this._loadFactor * (float)num);
			if (this._keycomparer == null && (comparer != null || hashCodeProvider != null))
			{
				this._keycomparer = new CompatibleComparer(hashCodeProvider, comparer);
			}
			this._buckets = new Hashtable.bucket[num];
			if (array == null)
			{
				throw new SerializationException("The keys for this dictionary are missing.");
			}
			if (array2 == null)
			{
				throw new SerializationException("The values for this dictionary are missing.");
			}
			if (array.Length != array2.Length)
			{
				throw new SerializationException("The keys and values arrays have different sizes.");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					throw new SerializationException("One of the serialized keys is null.");
				}
				this.Insert(array[i], array2[i], true);
			}
			this._version = serializationInfo.GetInt32("Version");
			Hashtable.SerializationInfoTable.Remove(this);
		}

		// Token: 0x04001E9F RID: 7839
		private Hashtable.bucket[] _buckets;

		// Token: 0x04001EA0 RID: 7840
		private int _count;

		// Token: 0x04001EA1 RID: 7841
		private int _occupancy;

		// Token: 0x04001EA2 RID: 7842
		private int _loadsize;

		// Token: 0x04001EA3 RID: 7843
		private float _loadFactor;

		// Token: 0x04001EA4 RID: 7844
		private volatile int _version;

		// Token: 0x04001EA5 RID: 7845
		private volatile bool _isWriterInProgress;

		// Token: 0x04001EA6 RID: 7846
		private ICollection _keys;

		// Token: 0x04001EA7 RID: 7847
		private ICollection _values;

		// Token: 0x04001EA8 RID: 7848
		private IEqualityComparer _keycomparer;

		// Token: 0x04001EA9 RID: 7849
		private object _syncRoot;

		// Token: 0x04001EAA RID: 7850
		private static ConditionalWeakTable<object, SerializationInfo> s_serializationInfoTable;

		// Token: 0x02000720 RID: 1824
		private struct bucket
		{
			// Token: 0x04001EAB RID: 7851
			public object key;

			// Token: 0x04001EAC RID: 7852
			public object val;

			// Token: 0x04001EAD RID: 7853
			public int hash_coll;
		}

		// Token: 0x02000721 RID: 1825
		[Serializable]
		private class KeyCollection : ICollection, IEnumerable
		{
			// Token: 0x060039E0 RID: 14816 RVA: 0x000E14BE File Offset: 0x000DF6BE
			internal KeyCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x060039E1 RID: 14817 RVA: 0x000E14D0 File Offset: 0x000DF6D0
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < this._hashtable._count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._hashtable.CopyKeys(array, arrayIndex);
			}

			// Token: 0x060039E2 RID: 14818 RVA: 0x000E1545 File Offset: 0x000DF745
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 1);
			}

			// Token: 0x1700092A RID: 2346
			// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000E1553 File Offset: 0x000DF753
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x1700092B RID: 2347
			// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000E1560 File Offset: 0x000DF760
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x1700092C RID: 2348
			// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000E156D File Offset: 0x000DF76D
			public virtual int Count
			{
				get
				{
					return this._hashtable._count;
				}
			}

			// Token: 0x04001EAE RID: 7854
			private Hashtable _hashtable;
		}

		// Token: 0x02000722 RID: 1826
		[Serializable]
		private class ValueCollection : ICollection, IEnumerable
		{
			// Token: 0x060039E6 RID: 14822 RVA: 0x000E157A File Offset: 0x000DF77A
			internal ValueCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x060039E7 RID: 14823 RVA: 0x000E158C File Offset: 0x000DF78C
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < this._hashtable._count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._hashtable.CopyValues(array, arrayIndex);
			}

			// Token: 0x060039E8 RID: 14824 RVA: 0x000E1601 File Offset: 0x000DF801
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 2);
			}

			// Token: 0x1700092D RID: 2349
			// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000E160F File Offset: 0x000DF80F
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x1700092E RID: 2350
			// (get) Token: 0x060039EA RID: 14826 RVA: 0x000E161C File Offset: 0x000DF81C
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x1700092F RID: 2351
			// (get) Token: 0x060039EB RID: 14827 RVA: 0x000E1629 File Offset: 0x000DF829
			public virtual int Count
			{
				get
				{
					return this._hashtable._count;
				}
			}

			// Token: 0x04001EAF RID: 7855
			private Hashtable _hashtable;
		}

		// Token: 0x02000723 RID: 1827
		[Serializable]
		private class SyncHashtable : Hashtable, IEnumerable
		{
			// Token: 0x060039EC RID: 14828 RVA: 0x000E1636 File Offset: 0x000DF836
			internal SyncHashtable(Hashtable table)
				: base(false)
			{
				this._table = table;
			}

			// Token: 0x060039ED RID: 14829 RVA: 0x000E1646 File Offset: 0x000DF846
			internal SyncHashtable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x060039EE RID: 14830 RVA: 0x000145B3 File Offset: 0x000127B3
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x17000930 RID: 2352
			// (get) Token: 0x060039EF RID: 14831 RVA: 0x000E1655 File Offset: 0x000DF855
			public override int Count
			{
				get
				{
					return this._table.Count;
				}
			}

			// Token: 0x17000931 RID: 2353
			// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000E1662 File Offset: 0x000DF862
			public override bool IsReadOnly
			{
				get
				{
					return this._table.IsReadOnly;
				}
			}

			// Token: 0x17000932 RID: 2354
			// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000E166F File Offset: 0x000DF86F
			public override bool IsFixedSize
			{
				get
				{
					return this._table.IsFixedSize;
				}
			}

			// Token: 0x17000933 RID: 2355
			// (get) Token: 0x060039F2 RID: 14834 RVA: 0x0000C091 File Offset: 0x0000A291
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000934 RID: 2356
			public override object this[object key]
			{
				get
				{
					return this._table[key];
				}
				set
				{
					object syncRoot = this._table.SyncRoot;
					lock (syncRoot)
					{
						this._table[key] = value;
					}
				}
			}

			// Token: 0x17000935 RID: 2357
			// (get) Token: 0x060039F5 RID: 14837 RVA: 0x000E16D8 File Offset: 0x000DF8D8
			public override object SyncRoot
			{
				get
				{
					return this._table.SyncRoot;
				}
			}

			// Token: 0x060039F6 RID: 14838 RVA: 0x000E16E8 File Offset: 0x000DF8E8
			public override void Add(object key, object value)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Add(key, value);
				}
			}

			// Token: 0x060039F7 RID: 14839 RVA: 0x000E1734 File Offset: 0x000DF934
			public override void Clear()
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Clear();
				}
			}

			// Token: 0x060039F8 RID: 14840 RVA: 0x000E1780 File Offset: 0x000DF980
			public override bool Contains(object key)
			{
				return this._table.Contains(key);
			}

			// Token: 0x060039F9 RID: 14841 RVA: 0x000E178E File Offset: 0x000DF98E
			public override bool ContainsKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				return this._table.ContainsKey(key);
			}

			// Token: 0x060039FA RID: 14842 RVA: 0x000E17B0 File Offset: 0x000DF9B0
			public override void CopyTo(Array array, int arrayIndex)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x060039FB RID: 14843 RVA: 0x000E17FC File Offset: 0x000DF9FC
			public override object Clone()
			{
				object syncRoot = this._table.SyncRoot;
				object obj;
				lock (syncRoot)
				{
					obj = Hashtable.Synchronized((Hashtable)this._table.Clone());
				}
				return obj;
			}

			// Token: 0x060039FC RID: 14844 RVA: 0x000E1854 File Offset: 0x000DFA54
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x060039FD RID: 14845 RVA: 0x000E1854 File Offset: 0x000DFA54
			public override IDictionaryEnumerator GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x17000936 RID: 2358
			// (get) Token: 0x060039FE RID: 14846 RVA: 0x000E1864 File Offset: 0x000DFA64
			public override ICollection Keys
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection keys;
					lock (syncRoot)
					{
						keys = this._table.Keys;
					}
					return keys;
				}
			}

			// Token: 0x17000937 RID: 2359
			// (get) Token: 0x060039FF RID: 14847 RVA: 0x000E18B0 File Offset: 0x000DFAB0
			public override ICollection Values
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection values;
					lock (syncRoot)
					{
						values = this._table.Values;
					}
					return values;
				}
			}

			// Token: 0x06003A00 RID: 14848 RVA: 0x000E18FC File Offset: 0x000DFAFC
			public override void Remove(object key)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Remove(key);
				}
			}

			// Token: 0x06003A01 RID: 14849 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void OnDeserialization(object sender)
			{
			}

			// Token: 0x04001EB0 RID: 7856
			protected Hashtable _table;
		}

		// Token: 0x02000724 RID: 1828
		[Serializable]
		private class HashtableEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06003A02 RID: 14850 RVA: 0x000E1948 File Offset: 0x000DFB48
			internal HashtableEnumerator(Hashtable hashtable, int getObjRetType)
			{
				this._hashtable = hashtable;
				this._bucket = hashtable._buckets.Length;
				this._version = hashtable._version;
				this._current = false;
				this._getObjectRetType = getObjRetType;
			}

			// Token: 0x06003A03 RID: 14851 RVA: 0x00019FBE File Offset: 0x000181BE
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x17000938 RID: 2360
			// (get) Token: 0x06003A04 RID: 14852 RVA: 0x000E1981 File Offset: 0x000DFB81
			public virtual object Key
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					return this._currentKey;
				}
			}

			// Token: 0x06003A05 RID: 14853 RVA: 0x000E199C File Offset: 0x000DFB9C
			public virtual bool MoveNext()
			{
				if (this._version != this._hashtable._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				while (this._bucket > 0)
				{
					this._bucket--;
					object key = this._hashtable._buckets[this._bucket].key;
					if (key != null && key != this._hashtable._buckets)
					{
						this._currentKey = key;
						this._currentValue = this._hashtable._buckets[this._bucket].val;
						this._current = true;
						return true;
					}
				}
				this._current = false;
				return false;
			}

			// Token: 0x17000939 RID: 2361
			// (get) Token: 0x06003A06 RID: 14854 RVA: 0x000E1A46 File Offset: 0x000DFC46
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._currentKey, this._currentValue);
				}
			}

			// Token: 0x1700093A RID: 2362
			// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000E1A6C File Offset: 0x000DFC6C
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
						return this._currentKey;
					}
					if (this._getObjectRetType == 2)
					{
						return this._currentValue;
					}
					return new DictionaryEntry(this._currentKey, this._currentValue);
				}
			}

			// Token: 0x1700093B RID: 2363
			// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000E1AC2 File Offset: 0x000DFCC2
			public virtual object Value
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentValue;
				}
			}

			// Token: 0x06003A09 RID: 14857 RVA: 0x000E1AE0 File Offset: 0x000DFCE0
			public virtual void Reset()
			{
				if (this._version != this._hashtable._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._current = false;
				this._bucket = this._hashtable._buckets.Length;
				this._currentKey = null;
				this._currentValue = null;
			}

			// Token: 0x04001EB1 RID: 7857
			private Hashtable _hashtable;

			// Token: 0x04001EB2 RID: 7858
			private int _bucket;

			// Token: 0x04001EB3 RID: 7859
			private int _version;

			// Token: 0x04001EB4 RID: 7860
			private bool _current;

			// Token: 0x04001EB5 RID: 7861
			private int _getObjectRetType;

			// Token: 0x04001EB6 RID: 7862
			private object _currentKey;

			// Token: 0x04001EB7 RID: 7863
			private object _currentValue;
		}

		// Token: 0x02000725 RID: 1829
		internal class HashtableDebugView
		{
		}
	}
}
