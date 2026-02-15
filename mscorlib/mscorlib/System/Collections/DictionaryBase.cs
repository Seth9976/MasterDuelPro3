using System;

namespace System.Collections
{
	/// <summary>Provides the abstract base class for a strongly typed collection of key/value pairs.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000709 RID: 1801
	[Serializable]
	public abstract class DictionaryBase : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000DC575 File Offset: 0x000DA775
		protected Hashtable InnerHashtable
		{
			get
			{
				if (this._hashtable == null)
				{
					this._hashtable = new Hashtable();
				}
				return this._hashtable;
			}
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x00002645 File Offset: 0x00000845
		protected IDictionary Dictionary
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000DC590 File Offset: 0x000DA790
		public int Count
		{
			get
			{
				if (this._hashtable != null)
				{
					return this._hashtable.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.DictionaryBase" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.DictionaryBase" /> object is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000DC5A7 File Offset: 0x000DA7A7
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.InnerHashtable.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.DictionaryBase" /> object has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.DictionaryBase" /> object has a fixed size; otherwise, false. The default is false.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x000DC5B4 File Offset: 0x000DA7B4
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.InnerHashtable.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.DictionaryBase" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.DictionaryBase" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06003864 RID: 14436 RVA: 0x000DC5C1 File Offset: 0x000DA7C1
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerHashtable.IsSynchronized;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x000DC5CE File Offset: 0x000DA7CE
		ICollection IDictionary.Keys
		{
			get
			{
				return this.InnerHashtable.Keys;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000DC5DB File Offset: 0x000DA7DB
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerHashtable.SyncRoot;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003867 RID: 14439 RVA: 0x000DC5E8 File Offset: 0x000DA7E8
		ICollection IDictionary.Values
		{
			get
			{
				return this.InnerHashtable.Values;
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.DictionaryBase" /> elements to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from the <see cref="T:System.Collections.DictionaryBase" /> instance. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.DictionaryBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.DictionaryBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003868 RID: 14440 RVA: 0x000DC5F5 File Offset: 0x000DA7F5
		public void CopyTo(Array array, int index)
		{
			this.InnerHashtable.CopyTo(array, index);
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="key">The key whose value to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.DictionaryBase" /> is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.DictionaryBase" /> has a fixed size. </exception>
		// Token: 0x170008D2 RID: 2258
		object IDictionary.this[object key]
		{
			get
			{
				object obj = this.InnerHashtable[key];
				this.OnGet(key, obj);
				return obj;
			}
			set
			{
				this.OnValidate(key, value);
				bool flag = true;
				object obj = this.InnerHashtable[key];
				if (obj == null)
				{
					flag = this.InnerHashtable.Contains(key);
				}
				this.OnSet(key, obj, value);
				this.InnerHashtable[key] = value;
				try
				{
					this.OnSetComplete(key, obj, value);
				}
				catch
				{
					if (flag)
					{
						this.InnerHashtable[key] = obj;
					}
					else
					{
						this.InnerHashtable.Remove(key);
					}
					throw;
				}
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.DictionaryBase" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.DictionaryBase" /> contains an element with the specified key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.DictionaryBase" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		// Token: 0x0600386B RID: 14443 RVA: 0x000DC6B0 File Offset: 0x000DA8B0
		bool IDictionary.Contains(object key)
		{
			return this.InnerHashtable.Contains(key);
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.DictionaryBase" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.DictionaryBase" /> is read-only.-or- The <see cref="T:System.Collections.DictionaryBase" /> has a fixed size. </exception>
		// Token: 0x0600386C RID: 14444 RVA: 0x000DC6C0 File Offset: 0x000DA8C0
		void IDictionary.Add(object key, object value)
		{
			this.OnValidate(key, value);
			this.OnInsert(key, value);
			this.InnerHashtable.Add(key, value);
			try
			{
				this.OnInsertComplete(key, value);
			}
			catch
			{
				this.InnerHashtable.Remove(key);
				throw;
			}
		}

		/// <summary>Clears the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600386D RID: 14445 RVA: 0x000DC714 File Offset: 0x000DA914
		public void Clear()
		{
			this.OnClear();
			this.InnerHashtable.Clear();
			this.OnClearComplete();
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.DictionaryBase" /> is read-only.-or- The <see cref="T:System.Collections.DictionaryBase" /> has a fixed size. </exception>
		// Token: 0x0600386E RID: 14446 RVA: 0x000DC730 File Offset: 0x000DA930
		void IDictionary.Remove(object key)
		{
			if (this.InnerHashtable.Contains(key))
			{
				object obj = this.InnerHashtable[key];
				this.OnValidate(key, obj);
				this.OnRemove(key, obj);
				this.InnerHashtable.Remove(key);
				try
				{
					this.OnRemoveComplete(key, obj);
				}
				catch
				{
					this.InnerHashtable.Add(key, obj);
					throw;
				}
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600386F RID: 14447 RVA: 0x000DC7A0 File Offset: 0x000DA9A0
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.DictionaryBase" />.</returns>
		// Token: 0x06003870 RID: 14448 RVA: 0x000DC7A0 File Offset: 0x000DA9A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		/// <summary>Gets the element with the specified key and value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the element with the specified key and value.</returns>
		/// <param name="key">The key of the element to get. </param>
		/// <param name="currentValue">The current value of the element associated with <paramref name="key" />. </param>
		// Token: 0x06003871 RID: 14449 RVA: 0x0006565F File Offset: 0x0006385F
		protected virtual object OnGet(object key, object currentValue)
		{
			return currentValue;
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to locate. </param>
		/// <param name="oldValue">The old value of the element associated with <paramref name="key" />. </param>
		/// <param name="newValue">The new value of the element associated with <paramref name="key" />. </param>
		// Token: 0x06003872 RID: 14450 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnSet(object key, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to insert. </param>
		/// <param name="value">The value of the element to insert. </param>
		// Token: 0x06003873 RID: 14451 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnInsert(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes before clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		// Token: 0x06003874 RID: 14452 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional custom processes before removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <param name="value">The value of the element to remove. </param>
		// Token: 0x06003875 RID: 14453 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnRemove(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes when validating the element with the specified key and value.</summary>
		/// <param name="key">The key of the element to validate. </param>
		/// <param name="value">The value of the element to validate. </param>
		// Token: 0x06003876 RID: 14454 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnValidate(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes after setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to locate. </param>
		/// <param name="oldValue">The old value of the element associated with <paramref name="key" />. </param>
		/// <param name="newValue">The new value of the element associated with <paramref name="key" />. </param>
		// Token: 0x06003877 RID: 14455 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnSetComplete(object key, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to insert. </param>
		/// <param name="value">The value of the element to insert. </param>
		// Token: 0x06003878 RID: 14456 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnInsertComplete(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes after clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		// Token: 0x06003879 RID: 14457 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional custom processes after removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to remove. </param>
		/// <param name="value">The value of the element to remove. </param>
		// Token: 0x0600387A RID: 14458 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnRemoveComplete(object key, object value)
		{
		}

		// Token: 0x04001E5F RID: 7775
		private Hashtable _hashtable;
	}
}
