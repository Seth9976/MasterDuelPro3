using System;
using System.Collections;
using System.Collections.Generic;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeNamespaceImport" /> objects.</summary>
	// Token: 0x02000209 RID: 521
	[Serializable]
	public class CodeNamespaceImportCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000285 RID: 645
		public CodeNamespaceImport this[int index]
		{
			get
			{
				return (CodeNamespaceImport)this._data[index];
			}
			set
			{
				this._data[index] = value;
				this.SyncKeys();
			}
		}

		/// <summary>Gets the number of namespaces in the collection.</summary>
		/// <returns>The number of namespaces in the collection.</returns>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0003B241 File Offset: 0x00039441
		public int Count
		{
			get
			{
				return this._data.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.  This property always returns false.</returns>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000028AE File Offset: 0x00000AAE
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.  This property always returns false.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x000028AE File Offset: 0x00000AAE
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to add to the collection. </param>
		// Token: 0x06000C5B RID: 3163 RVA: 0x0003B24E File Offset: 0x0003944E
		public void Add(CodeNamespaceImport value)
		{
			if (!this._keys.ContainsKey(value.Namespace))
			{
				this._keys[value.Namespace] = value;
				this._data.Add(value);
			}
		}

		/// <summary>Clears the collection of members.</summary>
		// Token: 0x06000C5C RID: 3164 RVA: 0x0003B282 File Offset: 0x00039482
		public void Clear()
		{
			this._data.Clear();
			this._keys.Clear();
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0003B29C File Offset: 0x0003949C
		private void SyncKeys()
		{
			this._keys.Clear();
			foreach (object obj in this._data)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				this._keys[codeNamespaceImport.Namespace] = codeNamespaceImport;
			}
		}

		/// <summary>Gets an enumerator that enumerates the collection members.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that indicates the collection members.</returns>
		// Token: 0x06000C5E RID: 3166 RVA: 0x0003B30C File Offset: 0x0003950C
		public IEnumerator GetEnumerator()
		{
			return this._data.GetEnumerator();
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000289 RID: 649
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (CodeNamespaceImport)value;
				this.SyncKeys();
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0003B337 File Offset: 0x00039537
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false. This property always returns false. </returns>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  This property always returns null.</returns>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x000027B6 File Offset: 0x000009B6
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000C64 RID: 3172 RVA: 0x0003B33F File Offset: 0x0003953F
		void ICollection.CopyTo(Array array, int index)
		{
			this._data.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000C65 RID: 3173 RVA: 0x0003B34E File Offset: 0x0003954E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000C66 RID: 3174 RVA: 0x0003B356 File Offset: 0x00039556
		int IList.Add(object value)
		{
			return this._data.Add((CodeNamespaceImport)value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Collections.IList" />.</summary>
		// Token: 0x06000C67 RID: 3175 RVA: 0x0003B369 File Offset: 0x00039569
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the value is in the list; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000C68 RID: 3176 RVA: 0x0003B371 File Offset: 0x00039571
		bool IList.Contains(object value)
		{
			return this._data.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />. </summary>
		/// <returns>The index of <paramref name="value" /> if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000C69 RID: 3177 RVA: 0x0003B37F File Offset: 0x0003957F
		int IList.IndexOf(object value)
		{
			return this._data.IndexOf((CodeNamespaceImport)value);
		}

		/// <summary>Inserts an item in the <see cref="T:System.Collections.IList" /> at the specified position. </summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000C6A RID: 3178 RVA: 0x0003B392 File Offset: 0x00039592
		void IList.Insert(int index, object value)
		{
			this._data.Insert(index, (CodeNamespaceImport)value);
			this.SyncKeys();
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />. </summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000C6B RID: 3179 RVA: 0x0003B3AC File Offset: 0x000395AC
		void IList.Remove(object value)
		{
			this._data.Remove((CodeNamespaceImport)value);
			this.SyncKeys();
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.IList" />. </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06000C6C RID: 3180 RVA: 0x0003B3C5 File Offset: 0x000395C5
		void IList.RemoveAt(int index)
		{
			this._data.RemoveAt(index);
			this.SyncKeys();
		}

		// Token: 0x040008E3 RID: 2275
		private readonly ArrayList _data = new ArrayList();

		// Token: 0x040008E4 RID: 2276
		private readonly Dictionary<string, CodeNamespaceImport> _keys = new Dictionary<string, CodeNamespaceImport>(StringComparer.OrdinalIgnoreCase);
	}
}
