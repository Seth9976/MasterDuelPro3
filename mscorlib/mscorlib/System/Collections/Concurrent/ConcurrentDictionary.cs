using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe collection of key/value pairs that can be accessed by multiple threads concurrently. </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	// Token: 0x0200072D RID: 1837
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[Serializable]
	public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06003A2F RID: 14895 RVA: 0x000E28E4 File Offset: 0x000E0AE4
		private static bool IsValueWriteAtomic()
		{
			Type typeFromHandle = typeof(TValue);
			if (!typeFromHandle.IsValueType)
			{
				return true;
			}
			switch (Type.GetTypeCode(typeFromHandle))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Single:
				return true;
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Double:
				return IntPtr.Size == 8;
			default:
				return false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the default concurrency level, has the default initial capacity, and uses the default comparer for the key type.</summary>
		// Token: 0x06003A30 RID: 14896 RVA: 0x000E2953 File Offset: 0x000E0B53
		public ConcurrentDictionary()
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> class that is empty, has the default concurrency level and capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="comparer">The equality comparison implementation to use when comparing keys.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is null.</exception>
		// Token: 0x06003A31 RID: 14897 RVA: 0x000E2964 File Offset: 0x000E0B64
		public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, comparer)
		{
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000E2978 File Offset: 0x000E0B78
		private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				if (keyValuePair.Key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				TValue tvalue;
				if (!this.TryAddInternal(keyValuePair.Key, this._comparer.GetHashCode(keyValuePair.Key), keyValuePair.Value, false, false, out tvalue))
				{
					throw new ArgumentException("The source argument contains duplicate keys.");
				}
			}
			if (this._budget == 0)
			{
				this._budget = this._tables._buckets.Length / this._tables._locks.Length;
			}
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000E2A30 File Offset: 0x000E0C30
		internal ConcurrentDictionary(int concurrencyLevel, int capacity, bool growLockArray, IEqualityComparer<TKey> comparer)
		{
			if (concurrencyLevel < 1)
			{
				throw new ArgumentOutOfRangeException("concurrencyLevel", "The concurrencyLevel argument must be positive.");
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "The capacity argument must be greater than or equal to zero.");
			}
			if (capacity < concurrencyLevel)
			{
				capacity = concurrencyLevel;
			}
			object[] array = new object[concurrencyLevel];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new object();
			}
			int[] array2 = new int[array.Length];
			ConcurrentDictionary<TKey, TValue>.Node[] array3 = new ConcurrentDictionary<TKey, TValue>.Node[capacity];
			this._tables = new ConcurrentDictionary<TKey, TValue>.Tables(array3, array, array2);
			this._comparer = comparer ?? EqualityComparer<TKey>.Default;
			this._growLockArray = growLockArray;
			this._budget = array3.Length / array.Length;
		}

		/// <summary>Attempts to add the specified key and value to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>true if the key/value pair was added to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> successfully; false if the key already exists.</returns>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be  null for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003A34 RID: 14900 RVA: 0x000E2AD4 File Offset: 0x000E0CD4
		public bool TryAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			TValue tvalue;
			return this.TryAddInternal(key, this._comparer.GetHashCode(key), value, false, true, out tvalue);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> contains the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003A35 RID: 14901 RVA: 0x000E2B08 File Offset: 0x000E0D08
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		/// <summary>Attempts to remove and return the value that has the specified key from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>true if the object was removed successfully; otherwise, false.</returns>
		/// <param name="key">The key of the element to remove and return.</param>
		/// <param name="value">When this method returns, contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, or the default value of  the TValue type if <paramref name="key" /> does not exist. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		// Token: 0x06003A36 RID: 14902 RVA: 0x000E2B2C File Offset: 0x000E0D2C
		public bool TryRemove(TKey key, out TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return this.TryRemoveInternal(key, out value, false, default(TValue));
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000E2B58 File Offset: 0x000E0D58
		private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
		{
			int hashCode = this._comparer.GetHashCode(key);
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
				int num;
				int num2;
				ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(hashCode, out num, out num2, tables._buckets.Length, tables._locks.Length);
				object obj = tables._locks[num2];
				lock (obj)
				{
					if (tables != this._tables)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = tables._buckets[num];
					while (node2 != null)
					{
						if (hashCode == node2._hashcode && this._comparer.Equals(node2._key, key))
						{
							if (matchValue && !EqualityComparer<TValue>.Default.Equals(oldValue, node2._value))
							{
								value = default(TValue);
								return false;
							}
							if (node == null)
							{
								Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], node2._next);
							}
							else
							{
								node._next = node2._next;
							}
							value = node2._value;
							tables._countPerLock[num2]--;
							return true;
						}
						else
						{
							node = node2;
							node2 = node2._next;
						}
					}
				}
				break;
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Attempts to get the value associated with the specified key from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>true if the key was found in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />; otherwise, false.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the object from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> that has the specified key, or the default value of , if the operation failed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		// Token: 0x06003A38 RID: 14904 RVA: 0x000E2CAC File Offset: 0x000E0EAC
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return this.TryGetValueInternal(key, this._comparer.GetHashCode(key), out value);
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000E2CD0 File Offset: 0x000E0ED0
		private bool TryGetValueInternal(TKey key, int hashcode, out TValue value)
		{
			ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
			int bucket = ConcurrentDictionary<TKey, TValue>.GetBucket(hashcode, tables._buckets.Length);
			for (ConcurrentDictionary<TKey, TValue>.Node node = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[bucket]); node != null; node = node._next)
			{
				if (hashcode == node._hashcode && this._comparer.Equals(node._key, key))
				{
					value = node._value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Removes all keys and values from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		// Token: 0x06003A3A RID: 14906 RVA: 0x000E2D48 File Offset: 0x000E0F48
		public void Clear()
		{
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				ConcurrentDictionary<TKey, TValue>.Tables tables = new ConcurrentDictionary<TKey, TValue>.Tables(new ConcurrentDictionary<TKey, TValue>.Node[31], this._tables._locks, new int[this._tables._countPerLock.Length]);
				this._tables = tables;
				this._budget = Math.Max(1, tables._buckets.Length / tables._locks.Length);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000E2DD0 File Offset: 0x000E0FD0
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument is less than zero.");
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				int num2 = 0;
				int num3 = 0;
				while (num3 < this._tables._locks.Length && num2 >= 0)
				{
					num2 += this._tables._countPerLock[num3];
					num3++;
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException("The index is equal to or greater than the length of the array, or the number of elements in the dictionary is greater than the available space from index to the end of the destination array.");
				}
				this.CopyToPairs(array, index);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		/// <summary>Copies the key and value pairs stored in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> to a new array.</summary>
		/// <returns>A new array containing a snapshot of key and value pairs copied from the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		// Token: 0x06003A3C RID: 14908 RVA: 0x000E2E78 File Offset: 0x000E1078
		public KeyValuePair<TKey, TValue>[] ToArray()
		{
			int num = 0;
			checked
			{
				KeyValuePair<TKey, TValue>[] array;
				try
				{
					this.AcquireAllLocks(ref num);
					int num2 = 0;
					for (int i = 0; i < this._tables._locks.Length; i++)
					{
						num2 += this._tables._countPerLock[i];
					}
					if (num2 == 0)
					{
						array = Array.Empty<KeyValuePair<TKey, TValue>>();
					}
					else
					{
						KeyValuePair<TKey, TValue>[] array2 = new KeyValuePair<TKey, TValue>[num2];
						this.CopyToPairs(array2, 0);
						array = array2;
					}
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return array;
			}
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000E2EFC File Offset: 0x000E10FC
		private void CopyToPairs(KeyValuePair<TKey, TValue>[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this._tables._buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node._key, node._value);
					index++;
					node = node._next;
				}
			}
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000E2F54 File Offset: 0x000E1154
		private void CopyToEntries(DictionaryEntry[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this._tables._buckets)
			{
				while (node != null)
				{
					array[index] = new DictionaryEntry(node._key, node._value);
					index++;
					node = node._next;
				}
			}
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000E2FB8 File Offset: 0x000E11B8
		private void CopyToObjects(object[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this._tables._buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node._key, node._value);
					index++;
					node = node._next;
				}
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		// Token: 0x06003A40 RID: 14912 RVA: 0x000E3011 File Offset: 0x000E1211
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			ConcurrentDictionary<TKey, TValue>.Node[] buckets = this._tables._buckets;
			int num;
			for (int i = 0; i < buckets.Length; i = num + 1)
			{
				ConcurrentDictionary<TKey, TValue>.Node current;
				for (current = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref buckets[i]); current != null; current = current._next)
				{
					yield return new KeyValuePair<TKey, TValue>(current._key, current._value);
				}
				current = null;
				num = i;
			}
			yield break;
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000E3020 File Offset: 0x000E1220
		private bool TryAddInternal(TKey key, int hashcode, TValue value, bool updateIfExists, bool acquireLock, out TValue resultingValue)
		{
			checked
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables;
				bool flag;
				for (;;)
				{
					tables = this._tables;
					int num;
					int num2;
					ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(hashcode, out num, out num2, tables._buckets.Length, tables._locks.Length);
					flag = false;
					bool flag2 = false;
					try
					{
						if (acquireLock)
						{
							Monitor.Enter(tables._locks[num2], ref flag2);
						}
						if (tables != this._tables)
						{
							continue;
						}
						ConcurrentDictionary<TKey, TValue>.Node node = null;
						for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables._buckets[num]; node2 != null; node2 = node2._next)
						{
							if (hashcode == node2._hashcode && this._comparer.Equals(node2._key, key))
							{
								if (updateIfExists)
								{
									if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
									{
										node2._value = value;
									}
									else
									{
										ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2._key, value, hashcode, node2._next);
										if (node == null)
										{
											Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], node3);
										}
										else
										{
											node._next = node3;
										}
									}
									resultingValue = value;
								}
								else
								{
									resultingValue = node2._value;
								}
								return false;
							}
							node = node2;
						}
						Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], new ConcurrentDictionary<TKey, TValue>.Node(key, value, hashcode, tables._buckets[num]));
						tables._countPerLock[num2]++;
						if (tables._countPerLock[num2] > this._budget)
						{
							flag = true;
						}
					}
					finally
					{
						if (flag2)
						{
							Monitor.Exit(tables._locks[num2]);
						}
					}
					break;
				}
				if (flag)
				{
					this.GrowTable(tables);
				}
				resultingValue = value;
				return true;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value of the key/value pair at the specified index.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000944 RID: 2372
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				if (!this.TryGetValue(key, out tvalue))
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNotFoundException(key);
				}
				return tvalue;
			}
			set
			{
				if (key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				TValue tvalue;
				this.TryAddInternal(key, this._comparer.GetHashCode(key), value, true, true, out tvalue);
			}
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000399F7 File Offset: 0x00037BF7
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowKeyNotFoundException(object key)
		{
			throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x000E3217 File Offset: 0x000E1417
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowKeyNullException()
		{
			throw new ArgumentNullException("key");
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000E3224 File Offset: 0x000E1424
		public int Count
		{
			get
			{
				int num = 0;
				int countInternal;
				try
				{
					this.AcquireAllLocks(ref num);
					countInternal = this.GetCountInternal();
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return countInternal;
			}
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x000E3260 File Offset: 0x000E1460
		private int GetCountInternal()
		{
			int num = 0;
			for (int i = 0; i < this._tables._countPerLock.Length; i++)
			{
				num += this._tables._countPerLock[i];
			}
			return num;
		}

		/// <summary>Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> by using the specified function, if the key does not already exist.</summary>
		/// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value for the key as returned by valueFactory if the key was not in the dictionary.</returns>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="valueFactory">The function used to generate a value for the key</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="valueFactory" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003A48 RID: 14920 RVA: 0x000E32A0 File Offset: 0x000E14A0
		public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue;
			if (!this.TryGetValueInternal(key, hashCode, out tvalue))
			{
				this.TryAddInternal(key, hashCode, valueFactory(key), false, true, out tvalue);
			}
			return tvalue;
		}

		/// <summary>Adds a key/value pair to the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> if the key does not already exist.</summary>
		/// <returns>The value for the key. This will be either the existing value for the key if the key is already in the dictionary, or the new value if the key was not in the dictionary.</returns>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">the value to be added, if the key does not already exist</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003A49 RID: 14921 RVA: 0x000E32F8 File Offset: 0x000E14F8
		public TValue GetOrAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue;
			if (!this.TryGetValueInternal(key, hashCode, out tvalue))
			{
				this.TryAddInternal(key, hashCode, value, false, true, out tvalue);
			}
			return tvalue;
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x000E333A File Offset: 0x000E153A
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			if (!this.TryAdd(key, value))
			{
				throw new ArgumentException("The key already existed in the dictionary.");
			}
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x000E3354 File Offset: 0x000E1554
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			TValue tvalue;
			return this.TryRemove(key, out tvalue);
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A collection of keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003A4C RID: 14924 RVA: 0x000E336A File Offset: 0x000E156A
		public ICollection<TKey> Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000E336A File Offset: 0x000E156A
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		/// <summary>Gets a collection that contains the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A collection that contains the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />. </returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x000E3372 File Offset: 0x000E1572
		public ICollection<TValue> Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x000E3372 File Offset: 0x000E1572
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000E337A File Offset: 0x000E157A
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			((IDictionary<TKey, TValue>)this).Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000E3390 File Offset: 0x000E1590
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			TValue tvalue;
			return this.TryGetValue(keyValuePair.Key, out tvalue) && EqualityComparer<TValue>.Default.Equals(tvalue, keyValuePair.Value);
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003A52 RID: 14930 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000E33C4 File Offset: 0x000E15C4
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			if (keyValuePair.Key == null)
			{
				throw new ArgumentNullException("keyValuePair", "TKey is a reference type and item.Key is null.");
			}
			TValue tvalue;
			return this.TryRemoveInternal(keyValuePair.Key, out tvalue, true, keyValuePair.Value);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		// Token: 0x06003A54 RID: 14932 RVA: 0x000E3406 File Offset: 0x000E1606
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The object to use as the key.</param>
		/// <param name="value">The object to use as the value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type  of the <see cref="T:System.Collections.Generic.Dictionary`2" />. -or- <paramref name="value" /> is of a type that is not assignable to the type of values in the <see cref="T:System.Collections.Generic.Dictionary`2" />. -or-A value with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		/// <exception cref="T:System.OverflowException">The dictionary already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue" />).</exception>
		// Token: 0x06003A55 RID: 14933 RVA: 0x000E3410 File Offset: 0x000E1610
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (!(key is TKey))
			{
				throw new ArgumentException("The key was of an incorrect type for this dictionary.");
			}
			TValue tvalue;
			try
			{
				tvalue = (TValue)((object)value);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException("The value was of an incorrect type for this dictionary.");
			}
			((IDictionary<TKey, TValue>)this).Add((TKey)((object)key), tvalue);
		}

		/// <summary>Gets whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003A56 RID: 14934 RVA: 0x000E346C File Offset: 0x000E166C
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return key is TKey && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Provides a <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x06003A57 RID: 14935 RVA: 0x000E348C File Offset: 0x000E168C
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> has a fixed size; otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, this property always returns false.</returns>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x00033991 File Offset: 0x00031B91
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only; otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />, this property always returns false.</returns>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x00033991 File Offset: 0x00031B91
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys of the  <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
		/// <returns>An interface that contains the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003A5A RID: 14938 RVA: 0x000E336A File Offset: 0x000E156A
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003A5B RID: 14939 RVA: 0x000E3494 File Offset: 0x000E1694
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (key is TKey)
			{
				TValue tvalue;
				this.TryRemove((TKey)((object)key), out tvalue);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An interface that contains the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003A5C RID: 14940 RVA: 0x000E3372 File Offset: 0x000E1572
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValues();
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key, or  null if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</returns>
		/// <param name="key">The key of the value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is  null.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type or the value type of the <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" />.</exception>
		// Token: 0x1700094F RID: 2383
		object IDictionary.this[object key]
		{
			get
			{
				if (key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				TValue tvalue;
				if (key is TKey && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				if (!(key is TKey))
				{
					throw new ArgumentException("The key was of an incorrect type for this dictionary.");
				}
				if (!(value is TValue))
				{
					throw new ArgumentException("The value was of an incorrect type for this dictionary.");
				}
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" />. -or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06003A5F RID: 14943 RVA: 0x000E3548 File Offset: 0x000E1748
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument is less than zero.");
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
				int num2 = 0;
				int num3 = 0;
				while (num3 < tables._locks.Length && num2 >= 0)
				{
					num2 += tables._countPerLock[num3];
					num3++;
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException("The index is equal to or greater than the length of the array, or the number of elements in the dictionary is greater than the available space from index to the end of the destination array.");
				}
				KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
				if (array2 != null)
				{
					this.CopyToPairs(array2, index);
				}
				else
				{
					DictionaryEntry[] array3 = array as DictionaryEntry[];
					if (array3 != null)
					{
						this.CopyToEntries(array3, index);
					}
					else
					{
						object[] array4 = array as object[];
						if (array4 == null)
						{
							throw new ArgumentException("The array is multidimensional, or the type parameter for the set cannot be cast automatically to the type of the destination array.", "array");
						}
						this.CopyToObjects(array4, index);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentDictionary`2" /> this property always returns false.</returns>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>Always returns null.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x000E1BAB File Offset: 0x000DFDAB
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x000E363C File Offset: 0x000E183C
		private void GrowTable(ConcurrentDictionary<TKey, TValue>.Tables tables)
		{
			int num = 0;
			try
			{
				this.AcquireLocks(0, 1, ref num);
				if (tables == this._tables)
				{
					long num2 = 0L;
					for (int i = 0; i < tables._countPerLock.Length; i++)
					{
						num2 += (long)tables._countPerLock[i];
					}
					if (num2 < (long)(tables._buckets.Length / 4))
					{
						this._budget = 2 * this._budget;
						if (this._budget < 0)
						{
							this._budget = int.MaxValue;
						}
					}
					else
					{
						int num3 = 0;
						bool flag = false;
						object[] array;
						checked
						{
							try
							{
								num3 = tables._buckets.Length * 2 + 1;
								while (num3 % 3 == 0 || num3 % 5 == 0 || num3 % 7 == 0)
								{
									num3 += 2;
								}
								if (num3 > 2146435071)
								{
									flag = true;
								}
							}
							catch (OverflowException)
							{
								flag = true;
							}
							if (flag)
							{
								num3 = 2146435071;
								this._budget = int.MaxValue;
							}
							this.AcquireLocks(1, tables._locks.Length, ref num);
							array = tables._locks;
						}
						if (this._growLockArray && tables._locks.Length < 1024)
						{
							array = new object[tables._locks.Length * 2];
							Array.Copy(tables._locks, 0, array, 0, tables._locks.Length);
							for (int j = tables._locks.Length; j < array.Length; j++)
							{
								array[j] = new object();
							}
						}
						ConcurrentDictionary<TKey, TValue>.Node[] array2 = new ConcurrentDictionary<TKey, TValue>.Node[num3];
						int[] array3 = new int[array.Length];
						for (int k = 0; k < tables._buckets.Length; k++)
						{
							checked
							{
								ConcurrentDictionary<TKey, TValue>.Node next;
								for (ConcurrentDictionary<TKey, TValue>.Node node = tables._buckets[k]; node != null; node = next)
								{
									next = node._next;
									int num4;
									int num5;
									ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(node._hashcode, out num4, out num5, array2.Length, array.Length);
									array2[num4] = new ConcurrentDictionary<TKey, TValue>.Node(node._key, node._value, node._hashcode, array2[num4]);
									array3[num5]++;
								}
							}
						}
						this._budget = Math.Max(1, array2.Length / array.Length);
						this._tables = new ConcurrentDictionary<TKey, TValue>.Tables(array2, array, array3);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000E3884 File Offset: 0x000E1A84
		private static int GetBucket(int hashcode, int bucketCount)
		{
			return (hashcode & int.MaxValue) % bucketCount;
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000E388F File Offset: 0x000E1A8F
		private static void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount, int lockCount)
		{
			bucketNo = (hashcode & int.MaxValue) % bucketCount;
			lockNo = bucketNo % lockCount;
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000E38A3 File Offset: 0x000E1AA3
		private static int DefaultConcurrencyLevel
		{
			get
			{
				return PlatformHelper.ProcessorCount;
			}
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x000E38AC File Offset: 0x000E1AAC
		private void AcquireAllLocks(ref int locksAcquired)
		{
			if (CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentDictionary_AcquiringAllLocks(this._tables._buckets.Length);
			}
			this.AcquireLocks(0, 1, ref locksAcquired);
			this.AcquireLocks(1, this._tables._locks.Length, ref locksAcquired);
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x000E3900 File Offset: 0x000E1B00
		private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
		{
			object[] locks = this._tables._locks;
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				bool flag = false;
				try
				{
					Monitor.Enter(locks[i], ref flag);
				}
				finally
				{
					if (flag)
					{
						locksAcquired++;
					}
				}
			}
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x000E3950 File Offset: 0x000E1B50
		private void ReleaseLocks(int fromInclusive, int toExclusive)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				Monitor.Exit(this._tables._locks[i]);
			}
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000E3980 File Offset: 0x000E1B80
		private ReadOnlyCollection<TKey> GetKeys()
		{
			int num = 0;
			ReadOnlyCollection<TKey> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TKey> list = new List<TKey>(countInternal);
				for (int i = 0; i < this._tables._buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this._tables._buckets[i]; node != null; node = node._next)
					{
						list.Add(node._key);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TKey>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x000E3A18 File Offset: 0x000E1C18
		private ReadOnlyCollection<TValue> GetValues()
		{
			int num = 0;
			ReadOnlyCollection<TValue> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TValue> list = new List<TValue>(countInternal);
				for (int i = 0; i < this._tables._buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this._tables._buckets[i]; node != null; node = node._next)
					{
						list.Add(node._value);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TValue>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000E3AB0 File Offset: 0x000E1CB0
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
			this._serializationArray = this.ToArray();
			this._serializationConcurrencyLevel = tables._locks.Length;
			this._serializationCapacity = tables._buckets.Length;
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x000E3AEE File Offset: 0x000E1CEE
		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			this._serializationArray = null;
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000E3AF8 File Offset: 0x000E1CF8
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			KeyValuePair<TKey, TValue>[] serializationArray = this._serializationArray;
			ConcurrentDictionary<TKey, TValue>.Node[] array = new ConcurrentDictionary<TKey, TValue>.Node[this._serializationCapacity];
			int[] array2 = new int[this._serializationConcurrencyLevel];
			object[] array3 = new object[this._serializationConcurrencyLevel];
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = new object();
			}
			this._tables = new ConcurrentDictionary<TKey, TValue>.Tables(array, array3, array2);
			this.InitializeFromCollection(serializationArray);
			this._serializationArray = null;
		}

		// Token: 0x04001ED1 RID: 7889
		[NonSerialized]
		private volatile ConcurrentDictionary<TKey, TValue>.Tables _tables;

		// Token: 0x04001ED2 RID: 7890
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x04001ED3 RID: 7891
		[NonSerialized]
		private readonly bool _growLockArray;

		// Token: 0x04001ED4 RID: 7892
		[NonSerialized]
		private int _budget;

		// Token: 0x04001ED5 RID: 7893
		private KeyValuePair<TKey, TValue>[] _serializationArray;

		// Token: 0x04001ED6 RID: 7894
		private int _serializationConcurrencyLevel;

		// Token: 0x04001ED7 RID: 7895
		private int _serializationCapacity;

		// Token: 0x04001ED8 RID: 7896
		private static readonly bool s_isValueWriteAtomic = ConcurrentDictionary<TKey, TValue>.IsValueWriteAtomic();

		// Token: 0x0200072E RID: 1838
		private sealed class Tables
		{
			// Token: 0x06003A6F RID: 14959 RVA: 0x000E3B75 File Offset: 0x000E1D75
			internal Tables(ConcurrentDictionary<TKey, TValue>.Node[] buckets, object[] locks, int[] countPerLock)
			{
				this._buckets = buckets;
				this._locks = locks;
				this._countPerLock = countPerLock;
			}

			// Token: 0x04001ED9 RID: 7897
			internal readonly ConcurrentDictionary<TKey, TValue>.Node[] _buckets;

			// Token: 0x04001EDA RID: 7898
			internal readonly object[] _locks;

			// Token: 0x04001EDB RID: 7899
			internal volatile int[] _countPerLock;
		}

		// Token: 0x0200072F RID: 1839
		[Serializable]
		private sealed class Node
		{
			// Token: 0x06003A70 RID: 14960 RVA: 0x000E3B94 File Offset: 0x000E1D94
			internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
			{
				this._key = key;
				this._value = value;
				this._next = next;
				this._hashcode = hashcode;
			}

			// Token: 0x04001EDC RID: 7900
			internal readonly TKey _key;

			// Token: 0x04001EDD RID: 7901
			internal TValue _value;

			// Token: 0x04001EDE RID: 7902
			internal volatile ConcurrentDictionary<TKey, TValue>.Node _next;

			// Token: 0x04001EDF RID: 7903
			internal readonly int _hashcode;
		}

		// Token: 0x02000730 RID: 1840
		[Serializable]
		private sealed class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06003A71 RID: 14961 RVA: 0x000E3BBB File Offset: 0x000E1DBB
			internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
			{
				this._enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000E3BD0 File Offset: 0x000E1DD0
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

			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x06003A73 RID: 14963 RVA: 0x000E3C14 File Offset: 0x000E1E14
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000E3C3C File Offset: 0x000E1E3C
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17000956 RID: 2390
			// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000E3C61 File Offset: 0x000E1E61
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06003A76 RID: 14966 RVA: 0x000E3C6E File Offset: 0x000E1E6E
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06003A77 RID: 14967 RVA: 0x000E3C7B File Offset: 0x000E1E7B
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x04001EE0 RID: 7904
			private IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;
		}
	}
}
