using System;

namespace System.Collections
{
	/// <summary>Provides the abstract base class for a strongly typed collection.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000708 RID: 1800
	[Serializable]
	public abstract class CollectionBase : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CollectionBase" /> class with the default initial capacity.</summary>
		// Token: 0x06003843 RID: 14403 RVA: 0x000DC249 File Offset: 0x000DA449
		protected CollectionBase()
		{
			this._list = new ArrayList();
		}

		/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> containing the list of elements in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> representing the <see cref="T:System.Collections.CollectionBase" /> instance itself.Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000DC25C File Offset: 0x000DA45C
		protected ArrayList InnerList
		{
			get
			{
				return this._list;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IList" /> containing the list of elements in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> representing the <see cref="T:System.Collections.CollectionBase" /> instance itself.</returns>
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x00002645 File Offset: 0x00000845
		protected IList List
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.CollectionBase" /> instance. This property cannot be overridden.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.CollectionBase" /> instance.Retrieving the value of this property is an O(1) operation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x000DC264 File Offset: 0x000DA464
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.CollectionBase" /> instance. This method cannot be overridden.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003847 RID: 14407 RVA: 0x000DC271 File Offset: 0x000DA471
		public void Clear()
		{
			this.OnClear();
			this.InnerList.Clear();
			this.OnClearComplete();
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.CollectionBase" /> instance. This method is not overridable.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003848 RID: 14408 RVA: 0x000DC28C File Offset: 0x000DA48C
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			object obj = this.InnerList[index];
			this.OnValidate(obj);
			this.OnRemove(index, obj);
			this.InnerList.RemoveAt(index);
			try
			{
				this.OnRemoveComplete(index, obj);
			}
			catch
			{
				this.InnerList.Insert(index, obj);
				throw;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.CollectionBase" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.CollectionBase" /> is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003849 RID: 14409 RVA: 0x000DC308 File Offset: 0x000DA508
		bool IList.IsReadOnly
		{
			get
			{
				return this.InnerList.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.CollectionBase" /> has a fixed size; otherwise, false. The default is false.</returns>
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000DC315 File Offset: 0x000DA515
		bool IList.IsFixedSize
		{
			get
			{
				return this.InnerList.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.CollectionBase" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.CollectionBase" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000DC322 File Offset: 0x000DA522
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.CollectionBase" />.</returns>
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600384C RID: 14412 RVA: 0x000DC32F File Offset: 0x000DA52F
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.CollectionBase" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.CollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Collections.CollectionBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.CollectionBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600384D RID: 14413 RVA: 0x000DC33C File Offset: 0x000DA53C
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		// Token: 0x170008C8 RID: 2248
		object IList.this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this.InnerList[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this.OnValidate(value);
				object obj = this.InnerList[index];
				this.OnSet(index, obj, value);
				this.InnerList[index] = value;
				try
				{
					this.OnSetComplete(index, obj, value);
				}
				catch
				{
					this.InnerList[index] = obj;
					throw;
				}
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.CollectionBase" /> contains a specific element.</summary>
		/// <returns>true if the <see cref="T:System.Collections.CollectionBase" /> contains the specified <paramref name="value" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.CollectionBase" />.</param>
		// Token: 0x06003850 RID: 14416 RVA: 0x000DC3F8 File Offset: 0x000DA5F8
		bool IList.Contains(object value)
		{
			return this.InnerList.Contains(value);
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <returns>The <see cref="T:System.Collections.CollectionBase" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.CollectionBase" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.CollectionBase" /> is read-only.-or-The <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</exception>
		// Token: 0x06003851 RID: 14417 RVA: 0x000DC408 File Offset: 0x000DA608
		int IList.Add(object value)
		{
			this.OnValidate(value);
			this.OnInsert(this.InnerList.Count, value);
			int num = this.InnerList.Add(value);
			try
			{
				this.OnInsertComplete(num, value);
			}
			catch
			{
				this.InnerList.RemoveAt(num);
				throw;
			}
			return num;
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.CollectionBase" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter was not found in the <see cref="T:System.Collections.CollectionBase" /> object.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.CollectionBase" /> is read-only.-or-The <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</exception>
		// Token: 0x06003852 RID: 14418 RVA: 0x000DC468 File Offset: 0x000DA668
		void IList.Remove(object value)
		{
			this.OnValidate(value);
			int num = this.InnerList.IndexOf(value);
			if (num < 0)
			{
				throw new ArgumentException("Cannot remove the specified item because it was not found in the specified Collection.");
			}
			this.OnRemove(num, value);
			this.InnerList.RemoveAt(num);
			try
			{
				this.OnRemoveComplete(num, value);
			}
			catch
			{
				this.InnerList.Insert(num, value);
				throw;
			}
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.CollectionBase" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.CollectionBase" />.</param>
		// Token: 0x06003853 RID: 14419 RVA: 0x000DC4D8 File Offset: 0x000DA6D8
		int IList.IndexOf(object value)
		{
			return this.InnerList.IndexOf(value);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.CollectionBase" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.CollectionBase" /> is read-only.-or-The <see cref="T:System.Collections.CollectionBase" /> has a fixed size.</exception>
		// Token: 0x06003854 RID: 14420 RVA: 0x000DC4E8 File Offset: 0x000DA6E8
		void IList.Insert(int index, object value)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this.OnValidate(value);
			this.OnInsert(index, value);
			this.InnerList.Insert(index, value);
			try
			{
				this.OnInsertComplete(index, value);
			}
			catch
			{
				this.InnerList.RemoveAt(index);
				throw;
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.CollectionBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003855 RID: 14421 RVA: 0x000DC558 File Offset: 0x000DA758
		public IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x06003856 RID: 14422 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x06003857 RID: 14423 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnInsert(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes when clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		// Token: 0x06003858 RID: 14424 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional custom processes when removing an element from the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
		// Token: 0x06003859 RID: 14425 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnRemove(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes when validating a value.</summary>
		/// <param name="value">The object to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x0600385A RID: 14426 RVA: 0x000DC565 File Offset: 0x000DA765
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
		}

		/// <summary>Performs additional custom processes after setting a value in the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x0600385B RID: 14427 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnSetComplete(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the element at <paramref name="index" />.</param>
		// Token: 0x0600385C RID: 14428 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		/// <summary>Performs additional custom processes after clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		// Token: 0x0600385D RID: 14429 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional custom processes after removing an element from the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
		// Token: 0x0600385E RID: 14430 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		// Token: 0x04001E5E RID: 7774
		private ArrayList _list;
	}
}
