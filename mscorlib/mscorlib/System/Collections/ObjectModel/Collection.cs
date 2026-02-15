using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.ObjectModel
{
	/// <summary>Provides the base class for a generic collection.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x02000734 RID: 1844
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.Collection`1" /> class that is empty.</summary>
		// Token: 0x06003A7E RID: 14974 RVA: 0x000E3D8C File Offset: 0x000E1F8C
		public Collection()
		{
			this.items = new List<T>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.Collection`1" /> class as a wrapper for the specified list.</summary>
		/// <param name="list">The list that is wrapped by the new collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null.</exception>
		// Token: 0x06003A7F RID: 14975 RVA: 0x000E3D9F File Offset: 0x000E1F9F
		public Collection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.items = list;
		}

		/// <summary>Gets the number of elements actually contained in the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>The number of elements actually contained in the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</returns>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003A80 RID: 14976 RVA: 0x000E3DB7 File Offset: 0x000E1FB7
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Generic.IList`1" /> wrapper around the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> wrapper around the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</returns>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003A81 RID: 14977 RVA: 0x000E3DC4 File Offset: 0x000E1FC4
		protected IList<T> Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />. </exception>
		// Token: 0x1700095B RID: 2395
		public T this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				if (this.items.IsReadOnly)
				{
					ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				}
				if (index >= this.items.Count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this.SetItem(index, value);
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.ObjectModel.Collection`1" />. </summary>
		/// <param name="item">The object to be added to the end of the <see cref="T:System.Collections.ObjectModel.Collection`1" />. The value can be null for reference types.</param>
		// Token: 0x06003A84 RID: 14980 RVA: 0x000E3E0C File Offset: 0x000E200C
		public void Add(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int count = this.items.Count;
			this.InsertItem(count, item);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		// Token: 0x06003A85 RID: 14981 RVA: 0x000E3E41 File Offset: 0x000E2041
		public void Clear()
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			this.ClearItems();
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ObjectModel.Collection`1" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ObjectModel.Collection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.ObjectModel.Collection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06003A86 RID: 14982 RVA: 0x000E3E5D File Offset: 0x000E205D
		public void CopyTo(T[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.ObjectModel.Collection`1" />; otherwise, false.</returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.ObjectModel.Collection`1" />. The value can be null for reference types.</param>
		// Token: 0x06003A87 RID: 14983 RVA: 0x000E3E6C File Offset: 0x000E206C
		public bool Contains(T item)
		{
			return this.items.Contains(item);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> for the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</returns>
		// Token: 0x06003A88 RID: 14984 RVA: 0x000E3E7A File Offset: 0x000E207A
		public IEnumerator<T> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="item" /> within the entire <see cref="T:System.Collections.ObjectModel.Collection`1" />, if found; otherwise, -1.</returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be null for reference types.</param>
		// Token: 0x06003A89 RID: 14985 RVA: 0x000E3E87 File Offset: 0x000E2087
		public int IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06003A8A RID: 14986 RVA: 0x000E3E95 File Offset: 0x000E2095
		public void Insert(int index, T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index > this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			this.InsertItem(index, item);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <returns>true if <paramref name="item" /> is successfully removed; otherwise, false.  This method also returns false if <paramref name="item" /> was not found in the original <see cref="T:System.Collections.ObjectModel.Collection`1" />.</returns>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.ObjectModel.Collection`1" />. The value can be null for reference types.</param>
		// Token: 0x06003A8B RID: 14987 RVA: 0x000E3EC8 File Offset: 0x000E20C8
		public bool Remove(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int num = this.items.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this.RemoveItem(num);
			return true;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06003A8C RID: 14988 RVA: 0x000E3F04 File Offset: 0x000E2104
		public void RemoveAt(int index)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index >= this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			this.RemoveItem(index);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		// Token: 0x06003A8D RID: 14989 RVA: 0x000E3F34 File Offset: 0x000E2134
		protected virtual void ClearItems()
		{
			this.items.Clear();
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06003A8E RID: 14990 RVA: 0x000E3F41 File Offset: 0x000E2141
		protected virtual void InsertItem(int index, T item)
		{
			this.items.Insert(index, item);
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06003A8F RID: 14991 RVA: 0x000E3F50 File Offset: 0x000E2150
		protected virtual void RemoveItem(int index)
		{
			this.items.RemoveAt(index);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06003A90 RID: 14992 RVA: 0x000E3F5E File Offset: 0x000E215E
		protected virtual void SetItem(int index, T item)
		{
			this.items[index] = item;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.Collection`1" />, this property always returns false.</returns>
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003A91 RID: 14993 RVA: 0x000E3F6D File Offset: 0x000E216D
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return this.items.IsReadOnly;
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003A92 RID: 14994 RVA: 0x000E3F7A File Offset: 0x000E217A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.Collection`1" />, this property always returns false.</returns>
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003A93 RID: 14995 RVA: 0x00033991 File Offset: 0x00031B91
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.ObjectModel.Collection`1" />, this property always returns the current instance.</returns>
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000E3F88 File Offset: 0x000E2188
		object ICollection.SyncRoot
		{
			get
			{
				ICollection collection = this.items as ICollection;
				if (collection == null)
				{
					return this;
				}
				return collection.SyncRoot;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-<paramref name="array" /> does not have zero-based indexing.-or-The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.-or-The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003A95 RID: 14997 RVA: 0x000E3FAC File Offset: 0x000E21AC
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.items.CopyTo(array2, index);
				return;
			}
			Type elementType = array.GetType().GetElementType();
			Type typeFromHandle = typeof(T);
			if (!elementType.IsAssignableFrom(typeFromHandle) && !typeFromHandle.IsAssignableFrom(elementType))
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			int count = this.items.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.items[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is set and <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x1700095F RID: 2399
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.Collection`1" />, this property always returns false.</returns>
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x000E3F6D File Offset: 0x000E216D
		bool IList.IsReadOnly
		{
			get
			{
				return this.items.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.  In the default implementation of <see cref="T:System.Collections.ObjectModel.Collection`1" />, this property always returns false.</returns>
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x000E4104 File Offset: 0x000E2304
		bool IList.IsFixedSize
		{
			get
			{
				IList list = this.items as IList;
				if (list != null)
				{
					return list.IsFixedSize;
				}
				return this.items.IsReadOnly;
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x06003A9A RID: 15002 RVA: 0x000E4134 File Offset: 0x000E2334
		int IList.Add(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			try
			{
				this.Add((T)((object)value));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
			}
			return this.Count - 1;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x06003A9B RID: 15003 RVA: 0x000E4198 File Offset: 0x000E2398
		bool IList.Contains(object value)
		{
			return Collection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x06003A9C RID: 15004 RVA: 0x000E41B0 File Offset: 0x000E23B0
		int IList.IndexOf(object value)
		{
			if (Collection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		/// <summary>Inserts an item into the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x06003A9D RID: 15005 RVA: 0x000E41C8 File Offset: 0x000E23C8
		void IList.Insert(int index, object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			try
			{
				this.Insert(index, (T)((object)value));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
			}
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is of a type that is not assignable to the <see cref="T:System.Collections.IList" />.</exception>
		// Token: 0x06003A9E RID: 15006 RVA: 0x000E4224 File Offset: 0x000E2424
		void IList.Remove(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (Collection<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000E4250 File Offset: 0x000E2450
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x04001EE7 RID: 7911
		private IList<T> items;
	}
}
