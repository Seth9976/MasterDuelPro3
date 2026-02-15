using System;

namespace System.Collections.Specialized
{
	/// <summary>Implements a hash table with the key and the value strongly typed to be strings rather than objects.</summary>
	// Token: 0x02000307 RID: 775
	[Serializable]
	public class StringDictionary : IEnumerable
	{
		/// <summary>Gets the number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>The number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x000546C5 File Offset: 0x000528C5
		public virtual int Count
		{
			get
			{
				return this.contents.Count;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, Get returns null, and Set creates a new entry with the specified key.</returns>
		/// <param name="key">The key whose value to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x17000407 RID: 1031
		public virtual string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key.ToLowerInvariant()];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key.ToLowerInvariant()] = value;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <param name="key">The key of the entry to add. </param>
		/// <param name="value">The value of the entry to add. The value can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.StringDictionary" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only. </exception>
		// Token: 0x060012FA RID: 4858 RVA: 0x0005471A File Offset: 0x0005291A
		public virtual void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key.ToLowerInvariant(), value);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only. </exception>
		// Token: 0x060012FB RID: 4859 RVA: 0x0005473C File Offset: 0x0005293C
		public virtual void Clear()
		{
			this.contents.Clear();
		}

		/// <summary>Determines if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.StringDictionary" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The key is null. </exception>
		// Token: 0x060012FC RID: 4860 RVA: 0x00054749 File Offset: 0x00052949
		public virtual bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key.ToLowerInvariant());
		}

		/// <summary>Returns an enumerator that iterates through the string dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that iterates through the string dictionary.</returns>
		// Token: 0x060012FD RID: 4861 RVA: 0x0005476A File Offset: 0x0005296A
		public virtual IEnumerator GetEnumerator()
		{
			return this.contents.GetEnumerator();
		}

		/// <summary>Removes the entry with the specified key from the string dictionary.</summary>
		/// <param name="key">The key of the entry to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The key is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only. </exception>
		// Token: 0x060012FE RID: 4862 RVA: 0x00054777 File Offset: 0x00052977
		public virtual void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key.ToLowerInvariant());
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00054798 File Offset: 0x00052998
		internal void ReplaceHashtable(Hashtable useThisHashtableInstead)
		{
			this.contents = useThisHashtableInstead;
		}

		// Token: 0x04000B76 RID: 2934
		internal Hashtable contents = new Hashtable();
	}
}
