using System;

namespace System.Collections.Specialized
{
	/// <summary>Implements IDictionary by using a <see cref="T:System.Collections.Specialized.ListDictionary" /> while the collection is small, and then switching to a <see cref="T:System.Collections.Hashtable" /> when the collection gets large.</summary>
	// Token: 0x020002FB RID: 763
	[Serializable]
	public class HybridDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new entry using the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x170003D1 RID: 977
		public object this[object key]
		{
			get
			{
				ListDictionary listDictionary = this.list;
				if (this.hashtable != null)
				{
					return this.hashtable[key];
				}
				if (listDictionary != null)
				{
					return listDictionary[key];
				}
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return null;
			}
			set
			{
				if (this.hashtable != null)
				{
					this.hashtable[key] = value;
					return;
				}
				if (this.list == null)
				{
					this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
					this.list[key] = value;
					return;
				}
				if (this.list.Count >= 8)
				{
					this.ChangeOver();
					this.hashtable[key] = value;
					return;
				}
				this.list[key] = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0005323F File Offset: 0x0005143F
		private ListDictionary List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
				}
				return this.list;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0005326C File Offset: 0x0005146C
		private void ChangeOver()
		{
			IDictionaryEnumerator enumerator = this.list.GetEnumerator();
			Hashtable hashtable;
			if (this.caseInsensitive)
			{
				hashtable = new Hashtable(13, StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				hashtable = new Hashtable(13);
			}
			while (enumerator.MoveNext())
			{
				hashtable.Add(enumerator.Key, enumerator.Value);
			}
			this.hashtable = hashtable;
			this.list = null;
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x000532D0 File Offset: 0x000514D0
		public int Count
		{
			get
			{
				ListDictionary listDictionary = this.list;
				if (this.hashtable != null)
				{
					return this.hashtable.Count;
				}
				if (listDictionary != null)
				{
					return listDictionary.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00053303 File Offset: 0x00051503
		public ICollection Keys
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Keys;
				}
				return this.List.Keys;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is read-only.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> has a fixed size.</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0001AE3D File Offset: 0x0001903D
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00053324 File Offset: 0x00051524
		public ICollection Values
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Values;
				}
				return this.List.Values;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.HybridDictionary" />. </exception>
		// Token: 0x0600126F RID: 4719 RVA: 0x00053348 File Offset: 0x00051548
		public void Add(object key, object value)
		{
			if (this.hashtable != null)
			{
				this.hashtable.Add(key, value);
				return;
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
				this.list.Add(key, value);
				return;
			}
			if (this.list.Count + 1 >= 9)
			{
				this.ChangeOver();
				this.hashtable.Add(key, value);
				return;
			}
			this.list.Add(key, value);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		// Token: 0x06001270 RID: 4720 RVA: 0x000533CE File Offset: 0x000515CE
		public void Clear()
		{
			if (this.hashtable != null)
			{
				Hashtable hashtable = this.hashtable;
				this.hashtable = null;
				hashtable.Clear();
			}
			if (this.list != null)
			{
				ListDictionary listDictionary = this.list;
				this.list = null;
				listDictionary.Clear();
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.HybridDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.HybridDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06001271 RID: 4721 RVA: 0x00053404 File Offset: 0x00051604
		public bool Contains(object key)
		{
			ListDictionary listDictionary = this.list;
			if (this.hashtable != null)
			{
				return this.hashtable.Contains(key);
			}
			if (listDictionary != null)
			{
				return listDictionary.Contains(key);
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.HybridDictionary" /> entries to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.HybridDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Specialized.HybridDictionary" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.HybridDictionary" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06001272 RID: 4722 RVA: 0x00053447 File Offset: 0x00051647
		public void CopyTo(Array array, int index)
		{
			if (this.hashtable != null)
			{
				this.hashtable.CopyTo(array, index);
				return;
			}
			this.List.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x06001273 RID: 4723 RVA: 0x0005346C File Offset: 0x0005166C
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this.hashtable != null)
			{
				return this.hashtable.GetEnumerator();
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
			}
			return this.list.GetEnumerator();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x06001274 RID: 4724 RVA: 0x000534BC File Offset: 0x000516BC
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.hashtable != null)
			{
				return this.hashtable.GetEnumerator();
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
			}
			return this.list.GetEnumerator();
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x06001275 RID: 4725 RVA: 0x0005350B File Offset: 0x0005170B
		public void Remove(object key)
		{
			if (this.hashtable != null)
			{
				this.hashtable.Remove(key);
				return;
			}
			if (this.list != null)
			{
				this.list.Remove(key);
				return;
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
		}

		// Token: 0x04000B50 RID: 2896
		private ListDictionary list;

		// Token: 0x04000B51 RID: 2897
		private Hashtable hashtable;

		// Token: 0x04000B52 RID: 2898
		private readonly bool caseInsensitive;
	}
}
