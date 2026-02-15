using System;
using System.Runtime.Serialization;
using System.Text;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of associated <see cref="T:System.String" /> keys and <see cref="T:System.String" /> values that can be accessed either with the key or with the index.</summary>
	// Token: 0x02000301 RID: 769
	[Serializable]
	public class NameValueCollection : NameObjectCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the default initial capacity and uses the default case-insensitive hash code provider and the default case-insensitive comparer.</summary>
		// Token: 0x0600129A RID: 4762 RVA: 0x00053C29 File Offset: 0x00051E29
		public NameValueCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the specified initial capacity and uses the default case-insensitive hash code provider and the default case-insensitive comparer.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600129B RID: 4763 RVA: 0x00053C31 File Offset: 0x00051E31
		public NameValueCollection(int capacity)
			: base(capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		// Token: 0x0600129C RID: 4764 RVA: 0x00053C3A File Offset: 0x00051E3A
		public NameValueCollection(IEqualityComparer equalityComparer)
			: base(equalityComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> object can contain.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600129D RID: 4765 RVA: 0x00053C43 File Offset: 0x00051E43
		public NameValueCollection(int capacity, IEqualityComparer equalityComparer)
			: base(capacity, equalityComparer)
		{
		}

		/// <summary>Copies the entries from the specified <see cref="T:System.Collections.Specialized.NameValueCollection" /> to a new <see cref="T:System.Collections.Specialized.NameValueCollection" /> with the specified initial capacity or the same initial capacity as the number of entries copied, whichever is greater, and using the default case-insensitive hash code provider and the default case-insensitive comparer.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> can contain.</param>
		/// <param name="col">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to copy to the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is null.</exception>
		// Token: 0x0600129E RID: 4766 RVA: 0x00053C4D File Offset: 0x00051E4D
		public NameValueCollection(int capacity, NameValueCollection col)
			: base(capacity, (col != null) ? col.Comparer : null)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			base.Comparer = col.Comparer;
			this.Add(col);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is serializable and uses the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		// Token: 0x0600129F RID: 4767 RVA: 0x00053C83 File Offset: 0x00051E83
		protected NameValueCollection(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Resets the cached arrays of the collection to null.</summary>
		// Token: 0x060012A0 RID: 4768 RVA: 0x00053C8D File Offset: 0x00051E8D
		protected void InvalidateCachedArrays()
		{
			this._all = null;
			this._allKeys = null;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00053CA0 File Offset: 0x00051EA0
		private static string GetAsOneString(ArrayList list)
		{
			int num = ((list != null) ? list.Count : 0);
			if (num == 1)
			{
				return (string)list[0];
			}
			if (num > 1)
			{
				StringBuilder stringBuilder = new StringBuilder((string)list[0]);
				for (int i = 1; i < num; i++)
				{
					stringBuilder.Append(',');
					stringBuilder.Append((string)list[i]);
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00053D14 File Offset: 0x00051F14
		private static string[] GetAsStringArray(ArrayList list)
		{
			int num = ((list != null) ? list.Count : 0);
			if (num == 0)
			{
				return null;
			}
			string[] array = new string[num];
			list.CopyTo(0, array, 0, num);
			return array;
		}

		/// <summary>Copies the entries in the specified <see cref="T:System.Collections.Specialized.NameValueCollection" /> to the current <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="c">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to copy to the current <see cref="T:System.Collections.Specialized.NameValueCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null.</exception>
		// Token: 0x060012A3 RID: 4771 RVA: 0x00053D48 File Offset: 0x00051F48
		public void Add(NameValueCollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			this.InvalidateCachedArrays();
			int count = c.Count;
			for (int i = 0; i < count; i++)
			{
				string key = c.GetKey(i);
				string[] values = c.GetValues(i);
				if (values != null)
				{
					for (int j = 0; j < values.Length; j++)
					{
						this.Add(key, values[j]);
					}
				}
				else
				{
					this.Add(key, null);
				}
			}
		}

		/// <summary>Adds an entry with the specified name and value to the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to add. The key can be null.</param>
		/// <param name="value">The <see cref="T:System.String" /> value of the entry to add. The value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only. </exception>
		// Token: 0x060012A4 RID: 4772 RVA: 0x00053DB8 File Offset: 0x00051FB8
		public virtual void Add(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only.");
			}
			this.InvalidateCachedArrays();
			ArrayList arrayList = (ArrayList)base.BaseGet(name);
			if (arrayList == null)
			{
				arrayList = new ArrayList(1);
				if (value != null)
				{
					arrayList.Add(value);
				}
				base.BaseAdd(name, arrayList);
				return;
			}
			if (value != null)
			{
				arrayList.Add(value);
			}
		}

		/// <summary>Gets the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" /> combined into one comma-separated list.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains a comma-separated list of the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry that contains the values to get. The key can be null.</param>
		// Token: 0x060012A5 RID: 4773 RVA: 0x00053E14 File Offset: 0x00052014
		public virtual string Get(string name)
		{
			return NameValueCollection.GetAsOneString((ArrayList)base.BaseGet(name));
		}

		/// <summary>Gets the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry that contains the values to get. The key can be null.</param>
		// Token: 0x060012A6 RID: 4774 RVA: 0x00053E27 File Offset: 0x00052027
		public virtual string[] GetValues(string name)
		{
			return NameValueCollection.GetAsStringArray((ArrayList)base.BaseGet(name));
		}

		/// <summary>Sets the value of an entry in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to add the new value to. The key can be null.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the new value to add to the specified entry. The value can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060012A7 RID: 4775 RVA: 0x00053E3C File Offset: 0x0005203C
		public virtual void Set(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException("Collection is read-only.");
			}
			this.InvalidateCachedArrays();
			base.BaseSet(name, new ArrayList(1) { value });
		}

		/// <summary>Removes the entries with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to remove. The key can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060012A8 RID: 4776 RVA: 0x00053E79 File Offset: 0x00052079
		public virtual void Remove(string name)
		{
			this.InvalidateCachedArrays();
			base.BaseRemove(name);
		}

		/// <summary>Gets or sets the entry with the specified key in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the comma-separated list of values associated with the specified key, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to locate. The key can be null.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only and the operation attempts to modify the collection. </exception>
		// Token: 0x170003EA RID: 1002
		public string this[string name]
		{
			get
			{
				return this.Get(name);
			}
			set
			{
				this.Set(name, value);
			}
		}

		/// <summary>Gets the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> combined into one comma-separated list.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains a comma-separated list of the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="index">The zero-based index of the entry that contains the values to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x060012AB RID: 4779 RVA: 0x00053E9B File Offset: 0x0005209B
		public virtual string Get(int index)
		{
			return NameValueCollection.GetAsOneString((ArrayList)base.BaseGet(index));
		}

		/// <summary>Gets the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="index">The zero-based index of the entry that contains the values to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x060012AC RID: 4780 RVA: 0x00053EAE File Offset: 0x000520AE
		public virtual string[] GetValues(int index)
		{
			return NameValueCollection.GetAsStringArray((ArrayList)base.BaseGet(index));
		}

		/// <summary>Gets the key at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the key at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, null.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x060012AD RID: 4781 RVA: 0x00053EC1 File Offset: 0x000520C1
		public virtual string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		/// <summary>Gets the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the comma-separated list of values at the specified index of the collection.</returns>
		/// <param name="index">The zero-based index of the entry to locate in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x170003EB RID: 1003
		public string this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		/// <summary>Gets all the keys in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains all the keys of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00053ED3 File Offset: 0x000520D3
		public virtual string[] AllKeys
		{
			get
			{
				if (this._allKeys == null)
				{
					this._allKeys = base.BaseGetAllKeys();
				}
				return this._allKeys;
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00053EEF File Offset: 0x000520EF
		internal NameValueCollection(DBNull dummy)
			: base(dummy)
		{
		}

		// Token: 0x04000B66 RID: 2918
		private string[] _all;

		// Token: 0x04000B67 RID: 2919
		private string[] _allKeys;
	}
}
