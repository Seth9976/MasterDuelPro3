using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000A8 RID: 168
	[NullableContext(1)]
	[Nullable(0)]
	internal class DictionaryWrapper<[Nullable(2)] TKey, [Nullable(2)] TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IWrappedDictionary, IDictionary, ICollection
	{
		// Token: 0x06000578 RID: 1400 RVA: 0x0001E1ED File Offset: 0x0001C3ED
		public DictionaryWrapper(IDictionary dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._dictionary = dictionary;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001E207 File Offset: 0x0001C407
		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._genericDictionary = dictionary;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001E221 File Offset: 0x0001C421
		public DictionaryWrapper(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._readOnlyDictionary = dictionary;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001E23B File Offset: 0x0001C43B
		internal IDictionary<TKey, TValue> GenericDictionary
		{
			get
			{
				return this._genericDictionary;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001E243 File Offset: 0x0001C443
		public void Add(TKey key, TValue value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(key, value);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001E280 File Offset: 0x0001C480
		public bool ContainsKey(TKey key)
		{
			if (this._dictionary != null)
			{
				return this._dictionary.Contains(key);
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.ContainsKey(key);
			}
			return this.GenericDictionary.ContainsKey(key);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
		public ICollection<TKey> Keys
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Keys.Cast<TKey>().ToList<TKey>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Keys.ToList<TKey>();
				}
				return this.GenericDictionary.Keys;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001E310 File Offset: 0x0001C510
		public bool Remove(TKey key)
		{
			if (this._dictionary != null)
			{
				if (this._dictionary.Contains(key))
				{
					this._dictionary.Remove(key);
					return true;
				}
				return false;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this.GenericDictionary.Remove(key);
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001E368 File Offset: 0x0001C568
		public bool TryGetValue(TKey key, [Nullable(2)] out TValue value)
		{
			if (this._dictionary != null)
			{
				if (!this._dictionary.Contains(key))
				{
					value = default(TValue);
					return false;
				}
				value = (TValue)((object)this._dictionary[key]);
				return true;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this.GenericDictionary.TryGetValue(key, out value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001E3D4 File Offset: 0x0001C5D4
		public ICollection<TValue> Values
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Values.Cast<TValue>().ToList<TValue>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Values.ToList<TValue>();
				}
				return this.GenericDictionary.Values;
			}
		}

		// Token: 0x170000BA RID: 186
		public TValue this[TKey key]
		{
			get
			{
				if (this._dictionary != null)
				{
					return (TValue)((object)this._dictionary[key]);
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary[key];
				}
				return this.GenericDictionary[key];
			}
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				this.GenericDictionary[key] = value;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		public void Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				((IList)this._dictionary).Add(item);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			IDictionary<TKey, TValue> genericDictionary = this._genericDictionary;
			if (genericDictionary == null)
			{
				return;
			}
			genericDictionary.Add(item);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001E4FC File Offset: 0x0001C6FC
		public void Clear()
		{
			if (this._dictionary != null)
			{
				this._dictionary.Clear();
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.Clear();
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001E52C File Offset: 0x0001C72C
		public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				return ((IList)this._dictionary).Contains(item);
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.Contains(item);
			}
			return this.GenericDictionary.Contains(item);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001E57C File Offset: 0x0001C77C
		public void CopyTo([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this._dictionary != null)
			{
				using (IDictionaryEnumerator enumerator = this._dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DictionaryEntry entry = enumerator.Entry;
						array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)((object)entry.Key), (TValue)((object)entry.Value));
					}
					return;
				}
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001E618 File Offset: 0x0001C818
		public int Count
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Count;
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Count;
				}
				return this.GenericDictionary.Count;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001E64D File Offset: 0x0001C84D
		public bool IsReadOnly
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.IsReadOnly;
				}
				return this._readOnlyDictionary != null || this.GenericDictionary.IsReadOnly;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001E678 File Offset: 0x0001C878
		public bool Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				if (!this._dictionary.Contains(item.Key))
				{
					return true;
				}
				if (object.Equals(this._dictionary[item.Key], item.Value))
				{
					this._dictionary.Remove(item.Key);
					return true;
				}
				return false;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this.GenericDictionary.Remove(item);
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001E708 File Offset: 0x0001C908
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return (from DictionaryEntry de in this._dictionary
					select new KeyValuePair<TKey, TValue>((TKey)((object)de.Key), (TValue)((object)de.Value))).GetEnumerator();
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.GetEnumerator();
			}
			return this.GenericDictionary.GetEnumerator();
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001E771 File Offset: 0x0001C971
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001E779 File Offset: 0x0001C979
		void IDictionary.Add(object key, [Nullable(2)] object value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x170000BD RID: 189
		[Nullable(2)]
		object IDictionary.this[object key]
		{
			[return: Nullable(2)]
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary[(TKey)((object)key)];
				}
				return this.GenericDictionary[(TKey)((object)key)];
			}
			[param: Nullable(2)]
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				this.GenericDictionary[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001E84C File Offset: 0x0001CA4C
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return this._dictionary.GetEnumerator();
			}
			if (this._readOnlyDictionary != null)
			{
				return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this._readOnlyDictionary.GetEnumerator());
			}
			return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this.GenericDictionary.GetEnumerator());
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
		bool IDictionary.Contains(object key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey((TKey)((object)key));
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.ContainsKey((TKey)((object)key));
			}
			return this._dictionary.Contains(key);
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001E8ED File Offset: 0x0001CAED
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._genericDictionary == null && (this._readOnlyDictionary != null || this._dictionary.IsFixedSize);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001E90E File Offset: 0x0001CB0E
		ICollection IDictionary.Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Keys.ToList<TKey>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Keys.ToList<TKey>();
				}
				return this._dictionary.Keys;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001E94D File Offset: 0x0001CB4D
		public void Remove(object key)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Remove(key);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.Remove((TKey)((object)key));
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001E984 File Offset: 0x0001CB84
		ICollection IDictionary.Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Values.ToList<TValue>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Values.ToList<TValue>();
				}
				return this._dictionary.Values;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001E9C3 File Offset: 0x0001CBC3
		void ICollection.CopyTo(Array array, int index)
		{
			if (this._dictionary != null)
			{
				this._dictionary.CopyTo(array, index);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this.GenericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001E9FB File Offset: 0x0001CBFB
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._dictionary != null && this._dictionary.IsSynchronized;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001EA12 File Offset: 0x0001CC12
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001EA34 File Offset: 0x0001CC34
		public object UnderlyingDictionary
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary;
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary;
				}
				return this.GenericDictionary;
			}
		}

		// Token: 0x04000405 RID: 1029
		[Nullable(2)]
		private readonly IDictionary _dictionary;

		// Token: 0x04000406 RID: 1030
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly IDictionary<TKey, TValue> _genericDictionary;

		// Token: 0x04000407 RID: 1031
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly IReadOnlyDictionary<TKey, TValue> _readOnlyDictionary;

		// Token: 0x04000408 RID: 1032
		[Nullable(2)]
		private object _syncRoot;

		// Token: 0x020000A9 RID: 169
		[Nullable(0)]
		private readonly struct DictionaryEnumerator<[Nullable(2)] TEnumeratorKey, [Nullable(2)] TEnumeratorValue> : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600059A RID: 1434 RVA: 0x0001EA5A File Offset: 0x0001CC5A
			public DictionaryEnumerator([Nullable(new byte[] { 1, 0, 1, 1 })] IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, "e");
				this._e = e;
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001EA6E File Offset: 0x0001CC6E
			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001EA7C File Offset: 0x0001CC7C
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001EA98 File Offset: 0x0001CC98
			[Nullable(2)]
			public object Value
			{
				[NullableContext(2)]
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001EAB4 File Offset: 0x0001CCB4
			public object Current
			{
				get
				{
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = this._e.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this._e.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x0001EAFB File Offset: 0x0001CCFB
			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x0001EB08 File Offset: 0x0001CD08
			public void Reset()
			{
				this._e.Reset();
			}

			// Token: 0x04000409 RID: 1033
			[Nullable(new byte[] { 1, 0, 1, 1 })]
			private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;
		}
	}
}
