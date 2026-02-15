using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Contains a collection of strings to use for the auto-complete feature on certain Windows Forms controls. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000014 RID: 20
	public class AutoCompleteStringCollection : IList, ICollection
	{
		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000046 RID: 70 RVA: 0x00002ECC File Offset: 0x000010CC
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x00002F04 File Offset: 0x00001104
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AutoCompleteStringCollection.CollectionChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06000048 RID: 72 RVA: 0x00002F39 File Offset: 0x00001139
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.CollectionChanged == null)
			{
				return;
			}
			this.CollectionChanged(this, e);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" />.</summary>
		/// <returns>An enumerator that iterates through the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000049 RID: 73 RVA: 0x00002F51 File Offset: 0x00001151
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Copies the strings of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index. For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the strings copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600004A RID: 74 RVA: 0x00002F5E File Offset: 0x0000115E
		void ICollection.CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> .</summary>
		/// <returns>The number of items in the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002F6D File Offset: 0x0000116D
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D70 File Offset: 0x00000F70
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" />.</summary>
		/// <returns>Returns this <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002F7A File Offset: 0x0000117A
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Adds a string to the collection. For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The index at which the <paramref name="value" /> has been added. </returns>
		/// <param name="value">The string to be added to the collection</param>
		// Token: 0x0600004E RID: 78 RVA: 0x00002F7D File Offset: 0x0000117D
		int IList.Add(object value)
		{
			return this.Add((string)value);
		}

		/// <summary>Inserts a new <see cref="T:System.String" /> into the collection.</summary>
		/// <returns>The position in the collection where the <see cref="T:System.String" /> was added.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to add to the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600004F RID: 79 RVA: 0x00002F8B File Offset: 0x0000118B
		public int Add(string value)
		{
			int num = this.list.Add(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
			return num;
		}

		/// <summary>Removes all strings from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000050 RID: 80 RVA: 0x00002FA6 File Offset: 0x000011A6
		public void Clear()
		{
			this.list.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Determines where the collection contains a specified string. For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>true if <paramref name="value" /> is found in the collection; otherwise, false.</returns>
		/// <param name="value">The string to locate in the collection.</param>
		// Token: 0x06000051 RID: 81 RVA: 0x00002FC0 File Offset: 0x000011C0
		bool IList.Contains(object value)
		{
			return this.Contains((string)value);
		}

		/// <summary>Indicates whether the <see cref="T:System.String" /> exists within the collection.</summary>
		/// <returns>true if the <see cref="T:System.String" /> exists within the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> for which to search.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000052 RID: 82 RVA: 0x00002FCE File Offset: 0x000011CE
		public bool Contains(string value)
		{
			return this.list.Contains(value);
		}

		/// <summary>Determines the index of a specified string in the collection. For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The string to locate in the collection.</param>
		// Token: 0x06000053 RID: 83 RVA: 0x00002FDC File Offset: 0x000011DC
		int IList.IndexOf(object value)
		{
			return this.IndexOf((string)value);
		}

		/// <summary>Obtains the position of the specified string within the collection.</summary>
		/// <returns>The index for the specified item.</returns>
		/// <param name="value">The <see cref="T:System.String" /> for which to search.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000054 RID: 84 RVA: 0x00002FEA File Offset: 0x000011EA
		public int IndexOf(string value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Inserts an item to the collection at the specified index. For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The string to insert into the collection.</param>
		// Token: 0x06000055 RID: 85 RVA: 0x00002FF8 File Offset: 0x000011F8
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (string)value);
		}

		/// <summary>Inserts the string into a specific index in the collection.</summary>
		/// <param name="index">The position at which to insert the string.</param>
		/// <param name="value">The string to insert.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000056 RID: 86 RVA: 0x00003007 File Offset: 0x00001207
		public void Insert(int index, string value)
		{
			this.list.Insert(index, value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size. For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002D70 File Offset: 0x00000F70
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only. For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D70 File Offset: 0x00000F70
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific string from the collection. For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">The string to remove from the collection.</param>
		// Token: 0x06000059 RID: 89 RVA: 0x00003023 File Offset: 0x00001223
		void IList.Remove(object value)
		{
			this.Remove((string)value);
		}

		/// <summary>Removes a string from the collection. </summary>
		/// <param name="value">The <see cref="T:System.String" /> to remove.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600005A RID: 90 RVA: 0x00003031 File Offset: 0x00001231
		public void Remove(string value)
		{
			this.list.Remove(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, value));
		}

		/// <summary>Removes the string at the specified index.</summary>
		/// <param name="index">The zero-based index of the string to remove.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600005B RID: 91 RVA: 0x0000304C File Offset: 0x0000124C
		public void RemoveAt(int index)
		{
			string text = this[index];
			this.list.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, text));
		}

		/// <summary>Gets the element at a specified index. For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		// Token: 0x1700001E RID: 30
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (string)value;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.String" /> at the specified position.</returns>
		/// <param name="index">The index at which to get or set the <see cref="T:System.String" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001F RID: 31
		public string this[int index]
		{
			get
			{
				return (string)this.list[index];
			}
			set
			{
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, this.list[index]));
				this.list[index] = value;
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
			}
		}

		// Token: 0x04000089 RID: 137
		private ArrayList list;
	}
}
