using System;
using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
	// Token: 0x02000275 RID: 629
	public class EventDescriptorCollection : ICollection, IEnumerable, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection. </param>
		// Token: 0x06000EDF RID: 3807 RVA: 0x000415ED File Offset: 0x0003F7ED
		public EventDescriptorCollection(EventDescriptor[] events)
		{
			if (events == null)
			{
				this._events = Array.Empty<EventDescriptor>();
			}
			else
			{
				this._events = events;
				this.Count = events.Length;
			}
			this._eventsOwned = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects. The collection is optionally read-only.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection. </param>
		/// <param name="readOnly">true to specify a read-only collection; otherwise, false.</param>
		// Token: 0x06000EE0 RID: 3808 RVA: 0x0004161C File Offset: 0x0003F81C
		public EventDescriptorCollection(EventDescriptor[] events, bool readOnly)
			: this(events)
		{
			this._readOnly = readOnly;
		}

		/// <summary>Gets the number of event descriptors in the collection.</summary>
		/// <returns>The number of event descriptors in the collection.</returns>
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0004162C File Offset: 0x0003F82C
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00041634 File Offset: 0x0003F834
		public int Count { get; private set; }

		/// <summary>Gets or sets the event with the specified index number.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified index number.</returns>
		/// <param name="index">The zero-based index number of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is not a valid index for <see cref="P:System.ComponentModel.EventDescriptorCollection.Item(System.Int32)" />. </exception>
		// Token: 0x17000321 RID: 801
		public virtual EventDescriptor this[int index]
		{
			get
			{
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				return this._events[index];
			}
		}

		/// <summary>Gets or sets the event with the specified name.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or null if the event does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set. </param>
		// Token: 0x17000322 RID: 802
		public virtual EventDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		/// <summary>Adds an <see cref="T:System.ComponentModel.EventDescriptor" /> to the end of the collection.</summary>
		/// <returns>The position of the <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EE5 RID: 3813 RVA: 0x00041668 File Offset: 0x0003F868
		public int Add(EventDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.Count + 1);
			EventDescriptor[] events = this._events;
			int count = this.Count;
			this.Count = count + 1;
			events[count] = value;
			return this.Count - 1;
		}

		/// <summary>Removes all objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EE6 RID: 3814 RVA: 0x000416B2 File Offset: 0x0003F8B2
		public void Clear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.Count = 0;
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>true if the collection contains the <paramref name="value" /> parameter given; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection. </param>
		// Token: 0x06000EE7 RID: 3815 RVA: 0x000416C9 File Offset: 0x0003F8C9
		public bool Contains(EventDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000EE8 RID: 3816 RVA: 0x000416D8 File Offset: 0x0003F8D8
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureEventsOwned();
			Array.Copy(this._events, 0, array, index, this.Count);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000416F4 File Offset: 0x0003F8F4
		private void EnsureEventsOwned()
		{
			if (!this._eventsOwned)
			{
				this._eventsOwned = true;
				if (this._events != null)
				{
					EventDescriptor[] array = new EventDescriptor[this.Count];
					Array.Copy(this._events, 0, array, 0, this.Count);
					this._events = array;
				}
			}
			if (this._needSort)
			{
				this._needSort = false;
				this.InternalSort(this._namedSort);
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0004175C File Offset: 0x0003F95C
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this._events.Length)
			{
				return;
			}
			if (this._events.Length == 0)
			{
				this.Count = 0;
				this._events = new EventDescriptor[sizeNeeded];
				return;
			}
			this.EnsureEventsOwned();
			EventDescriptor[] array = new EventDescriptor[Math.Max(sizeNeeded, this._events.Length * 2)];
			Array.Copy(this._events, 0, array, 0, this.Count);
			this._events = array;
		}

		/// <summary>Gets the description of the event with the specified name in the collection.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or null if the event does not exist.</returns>
		/// <param name="name">The name of the event to get from the collection. </param>
		/// <param name="ignoreCase">true if you want to ignore the case of the event; otherwise, false. </param>
		// Token: 0x06000EEB RID: 3819 RVA: 0x000417CC File Offset: 0x0003F9CC
		public virtual EventDescriptor Find(string name, bool ignoreCase)
		{
			EventDescriptor eventDescriptor = null;
			if (ignoreCase)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (string.Equals(this._events[i].Name, name, StringComparison.OrdinalIgnoreCase))
					{
						eventDescriptor = this._events[i];
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < this.Count; j++)
				{
					if (string.Equals(this._events[j].Name, name, StringComparison.Ordinal))
					{
						eventDescriptor = this._events[j];
						break;
					}
				}
			}
			return eventDescriptor;
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection. </param>
		// Token: 0x06000EEC RID: 3820 RVA: 0x00041845 File Offset: 0x0003FA45
		public int IndexOf(EventDescriptor value)
		{
			return Array.IndexOf<EventDescriptor>(this._events, value, 0, this.Count);
		}

		/// <summary>Inserts an <see cref="T:System.ComponentModel.EventDescriptor" /> to the collection at a specified index.</summary>
		/// <param name="index">The index within the collection in which to insert the <paramref name="value" /> parameter. </param>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to insert into the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EED RID: 3821 RVA: 0x0004185C File Offset: 0x0003FA5C
		public void Insert(int index, EventDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.Count + 1);
			if (index < this.Count)
			{
				Array.Copy(this._events, index, this._events, index + 1, this.Count - index);
			}
			this._events[index] = value;
			int count = this.Count;
			this.Count = count + 1;
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.EventDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EEE RID: 3822 RVA: 0x000418C4 File Offset: 0x0003FAC4
		public void Remove(EventDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			int num = this.IndexOf(value);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.EventDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.EventDescriptor" /> to remove. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EEF RID: 3823 RVA: 0x000418F4 File Offset: 0x0003FAF4
		public void RemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.Count - 1)
			{
				Array.Copy(this._events, index + 1, this._events, index, this.Count - index - 1);
			}
			this._events[this.Count - 1] = null;
			int count = this.Count;
			this.Count = count - 1;
		}

		/// <summary>Gets an enumerator for this <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</summary>
		/// <returns>An enumerator that implements <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06000EF0 RID: 3824 RVA: 0x00041959 File Offset: 0x0003FB59
		public IEnumerator GetEnumerator()
		{
			if (this._events.Length == this.Count)
			{
				return this._events.GetEnumerator();
			}
			return new EventDescriptorCollection.ArraySubsetEnumerator(this._events, this.Count);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06000EF1 RID: 3825 RVA: 0x00041988 File Offset: 0x0003FB88
		protected void InternalSort(string[] names)
		{
			if (this._events.Length == 0)
			{
				return;
			}
			this.InternalSort(this._comparer);
			if (names != null && names.Length != 0)
			{
				List<EventDescriptor> list = new List<EventDescriptor>(this._events);
				int num = 0;
				int num2 = this._events.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						EventDescriptor eventDescriptor = list[j];
						if (eventDescriptor != null && eventDescriptor.Name.Equals(names[i]))
						{
							this._events[num++] = eventDescriptor;
							list[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (list[k] != null)
					{
						this._events[num++] = list[k];
					}
				}
			}
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection. </param>
		// Token: 0x06000EF2 RID: 3826 RVA: 0x00041A53 File Offset: 0x0003FC53
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this._events, sorter);
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized.</summary>
		/// <returns>true if access to the collection is synchronized; otherwise, false.</returns>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x000027B6 File Offset: 0x000009B6
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00041A6B File Offset: 0x0003FC6B
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x00041A73 File Offset: 0x0003FC73
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0. -or-<paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x17000326 RID: 806
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException();
				}
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				this._events[index] = (EventDescriptor)value;
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EF9 RID: 3833 RVA: 0x00041AB7 File Offset: 0x0003FCB7
		int IList.Add(object value)
		{
			return this.Add((EventDescriptor)value);
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06000EFA RID: 3834 RVA: 0x00041AC5 File Offset: 0x0003FCC5
		bool IList.Contains(object value)
		{
			return this.Contains((EventDescriptor)value);
		}

		/// <summary>Removes all the items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EFB RID: 3835 RVA: 0x00041AD3 File Offset: 0x0003FCD3
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x06000EFC RID: 3836 RVA: 0x00041ADB File Offset: 0x0003FCDB
		int IList.IndexOf(object value)
		{
			return this.IndexOf((EventDescriptor)value);
		}

		/// <summary>Inserts an item to the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EFD RID: 3837 RVA: 0x00041AE9 File Offset: 0x0003FCE9
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (EventDescriptor)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EFE RID: 3838 RVA: 0x00041AF8 File Offset: 0x0003FCF8
		void IList.Remove(object value)
		{
			this.Remove((EventDescriptor)value);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000EFF RID: 3839 RVA: 0x00041B06 File Offset: 0x0003FD06
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00041B0F File Offset: 0x0003FD0F
		bool IList.IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00041B0F File Offset: 0x0003FD0F
		bool IList.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x040009F1 RID: 2545
		private EventDescriptor[] _events;

		// Token: 0x040009F2 RID: 2546
		private string[] _namedSort;

		// Token: 0x040009F3 RID: 2547
		private readonly IComparer _comparer;

		// Token: 0x040009F4 RID: 2548
		private bool _eventsOwned;

		// Token: 0x040009F5 RID: 2549
		private bool _needSort;

		// Token: 0x040009F6 RID: 2550
		private readonly bool _readOnly;

		/// <summary>Specifies an empty collection to use, rather than creating a new one with no items. This static field is read-only.</summary>
		// Token: 0x040009F7 RID: 2551
		public static readonly EventDescriptorCollection Empty = new EventDescriptorCollection(null, true);

		// Token: 0x02000276 RID: 630
		private class ArraySubsetEnumerator : IEnumerator
		{
			// Token: 0x06000F03 RID: 3843 RVA: 0x00041B25 File Offset: 0x0003FD25
			public ArraySubsetEnumerator(Array array, int count)
			{
				this._array = array;
				this._total = count;
				this._current = -1;
			}

			// Token: 0x06000F04 RID: 3844 RVA: 0x00041B42 File Offset: 0x0003FD42
			public bool MoveNext()
			{
				if (this._current < this._total - 1)
				{
					this._current++;
					return true;
				}
				return false;
			}

			// Token: 0x06000F05 RID: 3845 RVA: 0x00041B65 File Offset: 0x0003FD65
			public void Reset()
			{
				this._current = -1;
			}

			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00041B6E File Offset: 0x0003FD6E
			public object Current
			{
				get
				{
					if (this._current == -1)
					{
						throw new InvalidOperationException();
					}
					return this._array.GetValue(this._current);
				}
			}

			// Token: 0x040009F9 RID: 2553
			private readonly Array _array;

			// Token: 0x040009FA RID: 2554
			private readonly int _total;

			// Token: 0x040009FB RID: 2555
			private int _current;
		}
	}
}
