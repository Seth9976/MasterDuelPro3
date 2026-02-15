using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.ObjectModel
{
	/// <summary>Provides the abstract base class for a collection whose keys are embedded in the values.</summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TItem">The type of items in the collection.</typeparam>
	// Token: 0x02000736 RID: 1846
	[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public abstract class KeyedCollection<TKey, TItem> : Collection<TItem>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the default equality comparer.</summary>
		// Token: 0x06003ABF RID: 15039 RVA: 0x000E44CD File Offset: 0x000E26CD
		protected KeyedCollection()
			: this(null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer.</summary>
		/// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or null to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
		// Token: 0x06003AC0 RID: 15040 RVA: 0x000E44D7 File Offset: 0x000E26D7
		protected KeyedCollection(IEqualityComparer<TKey> comparer)
			: this(comparer, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer and creates a lookup dictionary when the specified threshold is exceeded.</summary>
		/// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or null to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
		/// <param name="dictionaryCreationThreshold">The number of elements the collection can hold without creating a lookup dictionary (0 creates the lookup dictionary when the first item is added), or –1 to specify that a lookup dictionary is never created.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dictionaryCreationThreshold" /> is less than –1.</exception>
		// Token: 0x06003AC1 RID: 15041 RVA: 0x000E44E4 File Offset: 0x000E26E4
		protected KeyedCollection(IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
			: base(new List<TItem>())
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			if (dictionaryCreationThreshold == -1)
			{
				dictionaryCreationThreshold = int.MaxValue;
			}
			if (dictionaryCreationThreshold < -1)
			{
				throw new ArgumentOutOfRangeException("dictionaryCreationThreshold", "The specified threshold for creating dictionary is out of range.");
			}
			this.comparer = comparer;
			this.threshold = dictionaryCreationThreshold;
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000E4533 File Offset: 0x000E2733
		private new List<TItem> Items
		{
			get
			{
				return (List<TItem>)base.Items;
			}
		}

		/// <summary>Gets the element with the specified key. </summary>
		/// <returns>The element with the specified key. If an element with the specified key is not found, an exception is thrown.</returns>
		/// <param name="key">The key of the element to get.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">An element with the specified key does not exist in the collection.</exception>
		// Token: 0x1700096C RID: 2412
		public TItem this[TKey key]
		{
			get
			{
				TItem titem;
				if (this.TryGetValue(key, out titem))
				{
					return titem;
				}
				throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
			}
		}

		/// <summary>Determines whether the collection contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06003AC4 RID: 15044 RVA: 0x000E4578 File Offset: 0x000E2778
		public bool Contains(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key);
			}
			foreach (TItem titem in this.Items)
			{
				if (this.comparer.Equals(this.GetKeyForItem(titem), key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000E4608 File Offset: 0x000E2808
		public bool TryGetValue(TKey key, out TItem item)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.dict != null)
			{
				return this.dict.TryGetValue(key, out item);
			}
			foreach (TItem titem in this.Items)
			{
				TKey keyForItem = this.GetKeyForItem(titem);
				if (keyForItem != null && this.comparer.Equals(key, keyForItem))
				{
					item = titem;
					return true;
				}
			}
			item = default(TItem);
			return false;
		}

		/// <summary>Gets the lookup dictionary of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <returns>The lookup dictionary of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />, if it exists; otherwise, null.</returns>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x000E46B4 File Offset: 0x000E28B4
		protected IDictionary<TKey, TItem> Dictionary
		{
			get
			{
				return this.dict;
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		// Token: 0x06003AC7 RID: 15047 RVA: 0x000E46BC File Offset: 0x000E28BC
		protected override void ClearItems()
		{
			base.ClearItems();
			if (this.dict != null)
			{
				this.dict.Clear();
			}
			this.keyCount = 0;
		}

		/// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
		/// <returns>The key for the specified element.</returns>
		/// <param name="item">The element from which to extract the key.</param>
		// Token: 0x06003AC8 RID: 15048
		protected abstract TKey GetKeyForItem(TItem item);

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06003AC9 RID: 15049 RVA: 0x000E46E0 File Offset: 0x000E28E0
		protected override void InsertItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			if (keyForItem != null)
			{
				this.AddKey(keyForItem, item);
			}
			base.InsertItem(index, item);
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.</summary>
		/// <param name="index">The index of the element to remove.</param>
		// Token: 0x06003ACA RID: 15050 RVA: 0x000E4710 File Offset: 0x000E2910
		protected override void RemoveItem(int index)
		{
			TKey keyForItem = this.GetKeyForItem(this.Items[index]);
			if (keyForItem != null)
			{
				this.RemoveKey(keyForItem);
			}
			base.RemoveItem(index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The zero-based index of the item to be replaced.</param>
		/// <param name="item">The new item.</param>
		// Token: 0x06003ACB RID: 15051 RVA: 0x000E4748 File Offset: 0x000E2948
		protected override void SetItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			TKey keyForItem2 = this.GetKeyForItem(this.Items[index]);
			if (this.comparer.Equals(keyForItem2, keyForItem))
			{
				if (keyForItem != null && this.dict != null)
				{
					this.dict[keyForItem] = item;
				}
			}
			else
			{
				if (keyForItem != null)
				{
					this.AddKey(keyForItem, item);
				}
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000E47C8 File Offset: 0x000E29C8
		private void AddKey(TKey key, TItem item)
		{
			if (this.dict != null)
			{
				this.dict.Add(key, item);
				return;
			}
			if (this.keyCount == this.threshold)
			{
				this.CreateDictionary();
				this.dict.Add(key, item);
				return;
			}
			if (this.Contains(key))
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key));
			}
			this.keyCount++;
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000E483C File Offset: 0x000E2A3C
		private void CreateDictionary()
		{
			this.dict = new Dictionary<TKey, TItem>(this.comparer);
			foreach (TItem titem in this.Items)
			{
				TKey keyForItem = this.GetKeyForItem(titem);
				if (keyForItem != null)
				{
					this.dict.Add(keyForItem, titem);
				}
			}
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x000E48B8 File Offset: 0x000E2AB8
		private void RemoveKey(TKey key)
		{
			if (this.dict != null)
			{
				this.dict.Remove(key);
				return;
			}
			this.keyCount--;
		}

		// Token: 0x04001EEA RID: 7914
		private readonly IEqualityComparer<TKey> comparer;

		// Token: 0x04001EEB RID: 7915
		private Dictionary<TKey, TItem> dict;

		// Token: 0x04001EEC RID: 7916
		private int keyCount;

		// Token: 0x04001EED RID: 7917
		private readonly int threshold;
	}
}
