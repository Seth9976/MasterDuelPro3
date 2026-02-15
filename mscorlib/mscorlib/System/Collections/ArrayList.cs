using System;
using System.Diagnostics;
using System.Threading;

namespace System.Collections
{
	/// <summary>Implements the <see cref="T:System.Collections.IList" /> interface using an array whose size is dynamically increased as required.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000719 RID: 1817
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ArrayList.ArrayListDebugView))]
	[Serializable]
	public class ArrayList : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06003927 RID: 14631 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal ArrayList(bool trash)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06003928 RID: 14632 RVA: 0x000DEA24 File Offset: 0x000DCC24
		public ArrayList()
		{
			this._items = Array.Empty<object>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that is empty and has the specified initial capacity.</summary>
		/// <param name="capacity">The number of elements that the new list can initially store. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06003929 RID: 14633 RVA: 0x000DEA38 File Offset: 0x000DCC38
		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format("'{0}' must be non-negative.", "capacity"));
			}
			if (capacity == 0)
			{
				this._items = Array.Empty<object>();
				return;
			}
			this._items = new object[capacity];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ArrayList" /> class that contains elements copied from the specified collection and that has the same initial capacity as the number of elements copied.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements are copied to the new list. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		// Token: 0x0600392A RID: 14634 RVA: 0x000DEA84 File Offset: 0x000DCC84
		public ArrayList(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "Collection cannot be null.");
			}
			int count = c.Count;
			if (count == 0)
			{
				this._items = Array.Empty<object>();
				return;
			}
			this._items = new object[count];
			this.AddRange(c);
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.ArrayList" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.ArrayList.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.ArrayList.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000904 RID: 2308
		// (set) Token: 0x0600392B RID: 14635 RVA: 0x000DEAD4 File Offset: 0x000DCCD4
		public virtual int Capacity
		{
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = new object[4];
				}
			}
		}

		/// <summary>Gets the number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The number of elements actually contained in the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x000DEB41 File Offset: 0x000DCD41
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.ArrayList" /> has a fixed size; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600392D RID: 14637 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.ArrayList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.ArrayList" /> is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600392E RID: 14638 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ArrayList" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600392F RID: 14639 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003930 RID: 14640 RVA: 0x000DEB49 File Offset: 0x000DCD49
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700090A RID: 2314
		public virtual object this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this._items[index];
			}
			set
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this._items[index] = value;
				this._version++;
			}
		}

		/// <summary>Creates an <see cref="T:System.Collections.ArrayList" /> wrapper for a specific <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> wrapper around the <see cref="T:System.Collections.IList" />.</returns>
		/// <param name="list">The <see cref="T:System.Collections.IList" /> to wrap.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003933 RID: 14643 RVA: 0x000DEBC8 File Offset: 0x000DCDC8
		public static ArrayList Adapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.IListWrapper(list);
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The <see cref="T:System.Collections.ArrayList" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003934 RID: 14644 RVA: 0x000DEBE0 File Offset: 0x000DCDE0
		public virtual int Add(object value)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			this._items[this._size] = value;
			this._version++;
			int size = this._size;
			this._size = size + 1;
			return size;
		}

		/// <summary>Adds the elements of an <see cref="T:System.Collections.ICollection" /> to the end of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be added to the end of the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be null, but it can contain elements that are null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003935 RID: 14645 RVA: 0x000DEC38 File Offset: 0x000DCE38
		public virtual void AddRange(ICollection c)
		{
			this.InsertRange(this._size, c);
		}

		/// <summary>Searches a range of elements in the sorted <see cref="T:System.Collections.ArrayList" /> for an element using the specified comparer and returns the zero-based index of the element.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <param name="index">The zero-based starting index of the range to search. </param>
		/// <param name="count">The length of the range to search. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be null. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- null to use the default comparer that is the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in the <see cref="T:System.Collections.ArrayList" />.-or- <paramref name="comparer" /> is null and neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="comparer" /> is null and <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003936 RID: 14646 RVA: 0x000DEC48 File Offset: 0x000DCE48
		public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return Array.BinarySearch(this._items, index, count, value, comparer);
		}

		/// <summary>Searches the entire sorted <see cref="T:System.Collections.ArrayList" /> for an element using the default comparer and returns the zero-based index of the element.</summary>
		/// <returns>The zero-based index of <paramref name="value" /> in the sorted <see cref="T:System.Collections.ArrayList" />, if <paramref name="value" /> is found; otherwise, a negative number, which is the bitwise complement of the index of the next element that is larger than <paramref name="value" /> or, if there is no larger element, the bitwise complement of <see cref="P:System.Collections.ArrayList.Count" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate. The value can be null. </param>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="value" /> nor the elements of <see cref="T:System.Collections.ArrayList" /> implement the <see cref="T:System.IComparable" /> interface. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is not of the same type as the elements of the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003937 RID: 14647 RVA: 0x000DECA3 File Offset: 0x000DCEA3
		public virtual int BinarySearch(object value)
		{
			return this.BinarySearch(0, this.Count, value, null);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003938 RID: 14648 RVA: 0x000DECB4 File Offset: 0x000DCEB4
		public virtual void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003939 RID: 14649 RVA: 0x000DECE8 File Offset: 0x000DCEE8
		public virtual object Clone()
		{
			ArrayList arrayList = new ArrayList(this._size);
			arrayList._size = this._size;
			arrayList._version = this._version;
			Array.Copy(this._items, 0, arrayList._items, 0, this._size);
			return arrayList;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.ArrayList" />; otherwise, false.</returns>
		/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600393A RID: 14650 RVA: 0x000DED34 File Offset: 0x000DCF34
		public virtual bool Contains(object item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			for (int j = 0; j < this._size; j++)
			{
				if (this._items[j] != null && this._items[j].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the beginning of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the number of elements that the destination <paramref name="array" /> can contain. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600393B RID: 14651 RVA: 0x000DED91 File Offset: 0x000DCF91
		public virtual void CopyTo(Array array)
		{
			this.CopyTo(array, 0);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600393C RID: 14652 RVA: 0x000DED9B File Offset: 0x000DCF9B
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		/// <summary>Copies a range of elements from the <see cref="T:System.Collections.ArrayList" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="index">The zero-based index in the source <see cref="T:System.Collections.ArrayList" /> at which copying begins. </param>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ArrayList" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <param name="count">The number of elements to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="arrayIndex" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.ArrayList.Count" /> of the source <see cref="T:System.Collections.ArrayList" />.-or- The number of elements from <paramref name="index" /> to the end of the source <see cref="T:System.Collections.ArrayList" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600393D RID: 14653 RVA: 0x000DEDD0 File Offset: 0x000DCFD0
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000DEE20 File Offset: 0x000DD020
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = ((this._items.Length == 0) ? 4 : (this._items.Length * 2));
				if (num > 2146435071)
				{
					num = 2146435071;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		/// <summary>Returns an enumerator for the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600393F RID: 14655 RVA: 0x000DEE6A File Offset: 0x000DD06A
		public virtual IEnumerator GetEnumerator()
		{
			return new ArrayList.ArrayListEnumeratorSimple(this);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.ArrayList" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003940 RID: 14656 RVA: 0x000DEE72 File Offset: 0x000DD072
		public virtual int IndexOf(object value)
		{
			return Array.IndexOf(this._items, value, 0, this._size);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003941 RID: 14657 RVA: 0x000DEE88 File Offset: 0x000DD088
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", "Insertion index was out of range. Must be non-negative and less than or equal to size.");
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = value;
			this._size++;
			this._version++;
		}

		/// <summary>Inserts the elements of a collection into the <see cref="T:System.Collections.ArrayList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the new elements should be inserted. </param>
		/// <param name="c">The <see cref="T:System.Collections.ICollection" /> whose elements should be inserted into the <see cref="T:System.Collections.ArrayList" />. The collection itself cannot be null, but it can contain elements that are null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003942 RID: 14658 RVA: 0x000DEF1C File Offset: 0x000DD11C
		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "Collection cannot be null.");
			}
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int count = c.Count;
			if (count > 0)
			{
				this.EnsureCapacity(this._size + count);
				if (index < this._size)
				{
					Array.Copy(this._items, index, this._items, index + count, this._size - index);
				}
				object[] array = new object[count];
				c.CopyTo(array, 0);
				array.CopyTo(this._items, index);
				this._size += count;
				this._version++;
			}
		}

		/// <summary>Returns a read-only <see cref="T:System.Collections.ArrayList" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Collections.ArrayList" /> wrapper around <paramref name="list" />.</returns>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003943 RID: 14659 RVA: 0x000DEFD0 File Offset: 0x000DD1D0
		public static ArrayList ReadOnly(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyArrayList(list);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.ArrayList" />. The value can be null. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003944 RID: 14660 RVA: 0x000DEFE8 File Offset: 0x000DD1E8
		public virtual void Remove(object obj)
		{
			int num = this.IndexOf(obj);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based index of the element to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ArrayList.Count" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003945 RID: 14661 RVA: 0x000DF008 File Offset: 0x000DD208
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = null;
			this._version++;
		}

		/// <summary>Removes a range of elements from the <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <param name="index">The zero-based starting index of the range of elements to remove. </param>
		/// <param name="count">The number of elements to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only.-or- The <see cref="T:System.Collections.ArrayList" /> has a fixed size. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003946 RID: 14662 RVA: 0x000DF084 File Offset: 0x000DD284
		public virtual void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (count > 0)
			{
				int i = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				while (i > this._size)
				{
					this._items[--i] = null;
				}
				this._version++;
			}
		}

		/// <summary>Reverses the order of the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003947 RID: 14663 RVA: 0x000DF134 File Offset: 0x000DD334
		public virtual void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		/// <summary>Reverses the order of the elements in the specified range.</summary>
		/// <param name="index">The zero-based starting index of the range to reverse. </param>
		/// <param name="count">The number of elements in the range to reverse. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not denote a valid range of elements in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003948 RID: 14664 RVA: 0x000DF144 File Offset: 0x000DD344
		public virtual void Reverse(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			Array.Reverse<object>(this._items, index, count);
			this._version++;
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003949 RID: 14665 RVA: 0x000DF1AA File Offset: 0x000DD3AA
		public virtual void Sort()
		{
			this.Sort(0, this.Count, Comparer.Default);
		}

		/// <summary>Sorts the elements in the entire <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- A null reference (Nothing in Visual Basic) to use the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600394A RID: 14666 RVA: 0x000DF1BE File Offset: 0x000DD3BE
		public virtual void Sort(IComparer comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		/// <summary>Sorts the elements in a range of elements in <see cref="T:System.Collections.ArrayList" /> using the specified comparer.</summary>
		/// <param name="index">The zero-based starting index of the range to sort. </param>
		/// <param name="count">The length of the range to sort. </param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing elements.-or- A null reference (Nothing in Visual Basic) to use the <see cref="T:System.IComparable" /> implementation of each element. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the <see cref="T:System.Collections.ArrayList" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.ArrayList" /> is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">An error occurred while comparing two elements.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600394B RID: 14667 RVA: 0x000DF1D0 File Offset: 0x000DD3D0
		public virtual void Sort(int index, int count, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			Array.Sort(this._items, index, count, comparer);
			this._version++;
		}

		/// <summary>Returns an <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> wrapper that is synchronized (thread safe).</returns>
		/// <param name="list">The <see cref="T:System.Collections.ArrayList" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600394C RID: 14668 RVA: 0x000DF237 File Offset: 0x000DD437
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncArrayList(list);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new <see cref="T:System.Object" /> array.</summary>
		/// <returns>An <see cref="T:System.Object" /> array containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600394D RID: 14669 RVA: 0x000DF250 File Offset: 0x000DD450
		public virtual object[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<object>();
			}
			object[] array = new object[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ArrayList" /> to a new array of the specified element type.</summary>
		/// <returns>An array of the specified element type containing copies of the elements of the <see cref="T:System.Collections.ArrayList" />.</returns>
		/// <param name="type">The element <see cref="T:System.Type" /> of the destination array to create and copy elements to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ArrayList" /> cannot be cast automatically to the specified type. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600394E RID: 14670 RVA: 0x000DF28C File Offset: 0x000DD48C
		public virtual Array ToArray(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Array array = Array.CreateInstance(type, this._size);
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x04001E91 RID: 7825
		private object[] _items;

		// Token: 0x04001E92 RID: 7826
		private int _size;

		// Token: 0x04001E93 RID: 7827
		private int _version;

		// Token: 0x04001E94 RID: 7828
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x0200071A RID: 1818
		[Serializable]
		private class IListWrapper : ArrayList
		{
			// Token: 0x0600394F RID: 14671 RVA: 0x000DF2CF File Offset: 0x000DD4CF
			internal IListWrapper(IList list)
			{
				this._list = list;
				this._version = 0;
			}

			// Token: 0x1700090B RID: 2315
			// (set) Token: 0x06003950 RID: 14672 RVA: 0x000DF2E5 File Offset: 0x000DD4E5
			public override int Capacity
			{
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
					}
				}
			}

			// Token: 0x1700090C RID: 2316
			// (get) Token: 0x06003951 RID: 14673 RVA: 0x000DF300 File Offset: 0x000DD500
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700090D RID: 2317
			// (get) Token: 0x06003952 RID: 14674 RVA: 0x000DF30D File Offset: 0x000DD50D
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x06003953 RID: 14675 RVA: 0x000DF31A File Offset: 0x000DD51A
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x06003954 RID: 14676 RVA: 0x000DF327 File Offset: 0x000DD527
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17000910 RID: 2320
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version++;
				}
			}

			// Token: 0x17000911 RID: 2321
			// (get) Token: 0x06003957 RID: 14679 RVA: 0x000DF35F File Offset: 0x000DD55F
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06003958 RID: 14680 RVA: 0x000DF36C File Offset: 0x000DD56C
			public override int Add(object obj)
			{
				int num = this._list.Add(obj);
				this._version++;
				return num;
			}

			// Token: 0x06003959 RID: 14681 RVA: 0x000DF388 File Offset: 0x000DD588
			public override void AddRange(ICollection c)
			{
				this.InsertRange(this.Count, c);
			}

			// Token: 0x0600395A RID: 14682 RVA: 0x000DF398 File Offset: 0x000DD598
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				int i = index;
				int num = index + count - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					int num3 = comparer.Compare(value, this._list[num2]);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 < 0)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				return ~i;
			}

			// Token: 0x0600395B RID: 14683 RVA: 0x000DF427 File Offset: 0x000DD627
			public override void Clear()
			{
				if (this._list.IsFixedSize)
				{
					throw new NotSupportedException("Collection was of a fixed size.");
				}
				this._list.Clear();
				this._version++;
			}

			// Token: 0x0600395C RID: 14684 RVA: 0x000DF45A File Offset: 0x000DD65A
			public override object Clone()
			{
				return new ArrayList.IListWrapper(this._list);
			}

			// Token: 0x0600395D RID: 14685 RVA: 0x000DF467 File Offset: 0x000DD667
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x0600395E RID: 14686 RVA: 0x000DF475 File Offset: 0x000DD675
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x0600395F RID: 14687 RVA: 0x000DF484 File Offset: 0x000DD684
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "arrayIndex", "Non-negative number required.");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				for (int i = index; i < index + count; i++)
				{
					array.SetValue(this._list[i], arrayIndex++);
				}
			}

			// Token: 0x06003960 RID: 14688 RVA: 0x000DF54A File Offset: 0x000DD74A
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06003961 RID: 14689 RVA: 0x000DF557 File Offset: 0x000DD757
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06003962 RID: 14690 RVA: 0x000DF565 File Offset: 0x000DD765
			public override void Insert(int index, object obj)
			{
				this._list.Insert(index, obj);
				this._version++;
			}

			// Token: 0x06003963 RID: 14691 RVA: 0x000DF584 File Offset: 0x000DD784
			public override void InsertRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", "Collection cannot be null.");
				}
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (c.Count > 0)
				{
					ArrayList arrayList = this._list as ArrayList;
					if (arrayList != null)
					{
						arrayList.InsertRange(index, c);
					}
					else
					{
						foreach (object obj in c)
						{
							this._list.Insert(index++, obj);
						}
					}
					this._version++;
				}
			}

			// Token: 0x06003964 RID: 14692 RVA: 0x000DF61C File Offset: 0x000DD81C
			public override void Remove(object value)
			{
				int num = this.IndexOf(value);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06003965 RID: 14693 RVA: 0x000DF63C File Offset: 0x000DD83C
			public override void RemoveAt(int index)
			{
				this._list.RemoveAt(index);
				this._version++;
			}

			// Token: 0x06003966 RID: 14694 RVA: 0x000DF658 File Offset: 0x000DD858
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (count > 0)
				{
					this._version++;
				}
				while (count > 0)
				{
					this._list.RemoveAt(index);
					count--;
				}
			}

			// Token: 0x06003967 RID: 14695 RVA: 0x000DF6CC File Offset: 0x000DD8CC
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					object obj = this._list[i];
					this._list[i++] = this._list[num];
					this._list[num--] = obj;
				}
				this._version++;
			}

			// Token: 0x06003968 RID: 14696 RVA: 0x000DF770 File Offset: 0x000DD970
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				object[] array = new object[count];
				this.CopyTo(index, array, 0, count);
				Array.Sort(array, 0, count, comparer);
				for (int i = 0; i < count; i++)
				{
					this._list[i + index] = array[i];
				}
				this._version++;
			}

			// Token: 0x06003969 RID: 14697 RVA: 0x000DF800 File Offset: 0x000DDA00
			public override object[] ToArray()
			{
				if (this.Count == 0)
				{
					return Array.Empty<object>();
				}
				object[] array = new object[this.Count];
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x0600396A RID: 14698 RVA: 0x000DF838 File Offset: 0x000DDA38
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				Array array = Array.CreateInstance(type, this._list.Count);
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x04001E95 RID: 7829
			private IList _list;
		}

		// Token: 0x0200071B RID: 1819
		[Serializable]
		private class SyncArrayList : ArrayList
		{
			// Token: 0x0600396B RID: 14699 RVA: 0x000DF879 File Offset: 0x000DDA79
			internal SyncArrayList(ArrayList list)
				: base(false)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17000912 RID: 2322
			// (set) Token: 0x0600396C RID: 14700 RVA: 0x000DF898 File Offset: 0x000DDA98
			public override int Capacity
			{
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list.Capacity = value;
					}
				}
			}

			// Token: 0x17000913 RID: 2323
			// (get) Token: 0x0600396D RID: 14701 RVA: 0x000DF8E0 File Offset: 0x000DDAE0
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x17000914 RID: 2324
			// (get) Token: 0x0600396E RID: 14702 RVA: 0x000DF928 File Offset: 0x000DDB28
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17000915 RID: 2325
			// (get) Token: 0x0600396F RID: 14703 RVA: 0x000DF935 File Offset: 0x000DDB35
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17000916 RID: 2326
			// (get) Token: 0x06003970 RID: 14704 RVA: 0x0000C091 File Offset: 0x0000A291
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000917 RID: 2327
			public override object this[int index]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[index];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x17000918 RID: 2328
			// (get) Token: 0x06003973 RID: 14707 RVA: 0x000DF9D4 File Offset: 0x000DDBD4
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06003974 RID: 14708 RVA: 0x000DF9DC File Offset: 0x000DDBDC
			public override int Add(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.Add(value);
				}
				return num;
			}

			// Token: 0x06003975 RID: 14709 RVA: 0x000DFA24 File Offset: 0x000DDC24
			public override void AddRange(ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.AddRange(c);
				}
			}

			// Token: 0x06003976 RID: 14710 RVA: 0x000DFA6C File Offset: 0x000DDC6C
			public override int BinarySearch(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(value);
				}
				return num;
			}

			// Token: 0x06003977 RID: 14711 RVA: 0x000DFAB4 File Offset: 0x000DDCB4
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(index, count, value, comparer);
				}
				return num;
			}

			// Token: 0x06003978 RID: 14712 RVA: 0x000DFB00 File Offset: 0x000DDD00
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06003979 RID: 14713 RVA: 0x000DFB48 File Offset: 0x000DDD48
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new ArrayList.SyncArrayList((ArrayList)this._list.Clone());
				}
				return obj;
			}

			// Token: 0x0600397A RID: 14714 RVA: 0x000DFB9C File Offset: 0x000DDD9C
			public override bool Contains(object item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(item);
				}
				return flag2;
			}

			// Token: 0x0600397B RID: 14715 RVA: 0x000DFBE4 File Offset: 0x000DDDE4
			public override void CopyTo(Array array)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array);
				}
			}

			// Token: 0x0600397C RID: 14716 RVA: 0x000DFC2C File Offset: 0x000DDE2C
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x0600397D RID: 14717 RVA: 0x000DFC74 File Offset: 0x000DDE74
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(index, array, arrayIndex, count);
				}
			}

			// Token: 0x0600397E RID: 14718 RVA: 0x000DFCC0 File Offset: 0x000DDEC0
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x0600397F RID: 14719 RVA: 0x000DFD08 File Offset: 0x000DDF08
			public override int IndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value);
				}
				return num;
			}

			// Token: 0x06003980 RID: 14720 RVA: 0x000DFD50 File Offset: 0x000DDF50
			public override void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06003981 RID: 14721 RVA: 0x000DFD98 File Offset: 0x000DDF98
			public override void InsertRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.InsertRange(index, c);
				}
			}

			// Token: 0x06003982 RID: 14722 RVA: 0x000DFDE0 File Offset: 0x000DDFE0
			public override void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x06003983 RID: 14723 RVA: 0x000DFE28 File Offset: 0x000DE028
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06003984 RID: 14724 RVA: 0x000DFE70 File Offset: 0x000DE070
			public override void RemoveRange(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveRange(index, count);
				}
			}

			// Token: 0x06003985 RID: 14725 RVA: 0x000DFEB8 File Offset: 0x000DE0B8
			public override void Reverse(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Reverse(index, count);
				}
			}

			// Token: 0x06003986 RID: 14726 RVA: 0x000DFF00 File Offset: 0x000DE100
			public override void Sort()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort();
				}
			}

			// Token: 0x06003987 RID: 14727 RVA: 0x000DFF48 File Offset: 0x000DE148
			public override void Sort(IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(comparer);
				}
			}

			// Token: 0x06003988 RID: 14728 RVA: 0x000DFF90 File Offset: 0x000DE190
			public override void Sort(int index, int count, IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(index, count, comparer);
				}
			}

			// Token: 0x06003989 RID: 14729 RVA: 0x000DFFD8 File Offset: 0x000DE1D8
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._list.ToArray();
				}
				return array;
			}

			// Token: 0x0600398A RID: 14730 RVA: 0x000E0020 File Offset: 0x000DE220
			public override Array ToArray(Type type)
			{
				object root = this._root;
				Array array;
				lock (root)
				{
					array = this._list.ToArray(type);
				}
				return array;
			}

			// Token: 0x04001E96 RID: 7830
			private ArrayList _list;

			// Token: 0x04001E97 RID: 7831
			private object _root;
		}

		// Token: 0x0200071C RID: 1820
		[Serializable]
		private class ReadOnlyArrayList : ArrayList
		{
			// Token: 0x0600398B RID: 14731 RVA: 0x000E0068 File Offset: 0x000DE268
			internal ReadOnlyArrayList(ArrayList l)
			{
				this._list = l;
			}

			// Token: 0x17000919 RID: 2329
			// (get) Token: 0x0600398C RID: 14732 RVA: 0x000E0077 File Offset: 0x000DE277
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700091A RID: 2330
			// (get) Token: 0x0600398D RID: 14733 RVA: 0x0000C091 File Offset: 0x0000A291
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700091B RID: 2331
			// (get) Token: 0x0600398E RID: 14734 RVA: 0x0000C091 File Offset: 0x0000A291
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700091C RID: 2332
			// (get) Token: 0x0600398F RID: 14735 RVA: 0x000E0084 File Offset: 0x000DE284
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x1700091D RID: 2333
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException("Collection is read-only.");
				}
			}

			// Token: 0x1700091E RID: 2334
			// (get) Token: 0x06003992 RID: 14738 RVA: 0x000E00AB File Offset: 0x000DE2AB
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x06003993 RID: 14739 RVA: 0x000E009F File Offset: 0x000DE29F
			public override int Add(object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06003994 RID: 14740 RVA: 0x000E009F File Offset: 0x000DE29F
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06003995 RID: 14741 RVA: 0x000E00B8 File Offset: 0x000DE2B8
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x1700091F RID: 2335
			// (set) Token: 0x06003996 RID: 14742 RVA: 0x000E009F File Offset: 0x000DE29F
			public override int Capacity
			{
				set
				{
					throw new NotSupportedException("Collection is read-only.");
				}
			}

			// Token: 0x06003997 RID: 14743 RVA: 0x000E009F File Offset: 0x000DE29F
			public override void Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06003998 RID: 14744 RVA: 0x000E00CA File Offset: 0x000DE2CA
			public override object Clone()
			{
				return new ArrayList.ReadOnlyArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06003999 RID: 14745 RVA: 0x000E00ED File Offset: 0x000DE2ED
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x0600399A RID: 14746 RVA: 0x000E00FB File Offset: 0x000DE2FB
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x0600399B RID: 14747 RVA: 0x000E010A File Offset: 0x000DE30A
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x0600399C RID: 14748 RVA: 0x000E011C File Offset: 0x000DE31C
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x0600399D RID: 14749 RVA: 0x000E0129 File Offset: 0x000DE329
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x0600399E RID: 14750 RVA: 0x000E009F File Offset: 0x000DE29F
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x0600399F RID: 14751 RVA: 0x000E009F File Offset: 0x000DE29F
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060039A0 RID: 14752 RVA: 0x000E009F File Offset: 0x000DE29F
			public override void Remove(object value)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060039A1 RID: 14753 RVA: 0x000E009F File Offset: 0x000DE29F
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060039A2 RID: 14754 RVA: 0x000E009F File Offset: 0x000DE29F
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060039A3 RID: 14755 RVA: 0x000E009F File Offset: 0x000DE29F
			public override void Reverse(int index, int count)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060039A4 RID: 14756 RVA: 0x000E009F File Offset: 0x000DE29F
			public override void Sort(int index, int count, IComparer comparer)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060039A5 RID: 14757 RVA: 0x000E0137 File Offset: 0x000DE337
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x060039A6 RID: 14758 RVA: 0x000E0144 File Offset: 0x000DE344
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x04001E98 RID: 7832
			private ArrayList _list;
		}

		// Token: 0x0200071D RID: 1821
		[Serializable]
		private sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x060039A7 RID: 14759 RVA: 0x000E0154 File Offset: 0x000DE354
			internal ArrayListEnumeratorSimple(ArrayList list)
			{
				this._list = list;
				this._index = -1;
				this._version = list._version;
				this._isArrayList = list.GetType() == typeof(ArrayList);
				this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
			}

			// Token: 0x060039A8 RID: 14760 RVA: 0x00019FBE File Offset: 0x000181BE
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x060039A9 RID: 14761 RVA: 0x000E01A8 File Offset: 0x000DE3A8
			public bool MoveNext()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._isArrayList)
				{
					if (this._index < this._list._size - 1)
					{
						object[] items = this._list._items;
						int num = this._index + 1;
						this._index = num;
						this._currentElement = items[num];
						return true;
					}
					this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
					this._index = this._list._size;
					return false;
				}
				else
				{
					if (this._index < this._list.Count - 1)
					{
						ArrayList list = this._list;
						int num = this._index + 1;
						this._index = num;
						this._currentElement = list[num];
						return true;
					}
					this._index = this._list.Count;
					this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
					return false;
				}
			}

			// Token: 0x17000920 RID: 2336
			// (get) Token: 0x060039AA RID: 14762 RVA: 0x000E028C File Offset: 0x000DE48C
			public object Current
			{
				get
				{
					object currentElement = this._currentElement;
					if (ArrayList.ArrayListEnumeratorSimple.s_dummyObject != currentElement)
					{
						return currentElement;
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					throw new InvalidOperationException("Enumeration already finished.");
				}
			}

			// Token: 0x060039AB RID: 14763 RVA: 0x000E02C8 File Offset: 0x000DE4C8
			public void Reset()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
				this._index = -1;
			}

			// Token: 0x04001E99 RID: 7833
			private ArrayList _list;

			// Token: 0x04001E9A RID: 7834
			private int _index;

			// Token: 0x04001E9B RID: 7835
			private int _version;

			// Token: 0x04001E9C RID: 7836
			private object _currentElement;

			// Token: 0x04001E9D RID: 7837
			private bool _isArrayList;

			// Token: 0x04001E9E RID: 7838
			private static object s_dummyObject = new object();
		}

		// Token: 0x0200071E RID: 1822
		internal class ArrayListDebugView
		{
		}
	}
}
